using System;
using System.Windows.Input;

namespace Tamagottagettemall
{

    /// <summary>
    /// Scene where the actual game happens
    /// </summary>
    public class SceneGame : ISceneBase
    {

        /// <summary>
        /// Pet art that will be loaded from a file
        /// </summary>
        protected string _PetArt;

        /// <summary>
        /// Actions string roulette
        /// </summary>
        protected string _ActionsString = "      Eat       Nap       Run       Play";

        /// <summary>
        /// Box where the actions will be displayed
        /// </summary>
        protected string _ActionsBox = "██████████████████████\n█ ◄                ► █\n██████████████████████";

        /// <summary>
        /// Selected action
        /// </summary>
        protected int _ActionSelected;

        /// <summary>
        /// How many actions there are left
        /// </summary>
        protected int _ActionsLeft;

        /// <summary>
        /// Animation manager to help with the screen animations
        /// </summary>
        protected SimpleAnimation _Animation;

        /// <summary>
        /// Current scene step
        /// </summary>
        protected byte _Step;

        /// <summary>
        /// Starts the scene elements
        /// </summary>
        /// <param name="game">Game manager</param>
        public void Start(Game game)
        {
            // Loads the pet art from a file
            _PetArt = Utils.StringReadFile("Pet.txt");
            // Sets the selected action to the first one
            _ActionSelected = 0;
            // This first animation is the "Day #" fadein
            // It goes from 0 (totally transparent) and goes to 1 (totally opaque)
            // It takes 500 milliseconds to complete the animation and starts fast, goes through fast and arrives slow
            _Animation = new SimpleAnimation(0.0f, 1.0f, 500, Smoothness.Arrival, () =>
            {
                // When its done, it waits for 1000 milliseconds
                _Animation.Reset(1.0f, 1.0f, 1000, Smoothness.None, () =>
                {
                    // Then it fades out in 500 milliseconds
                    _Animation.Reset(1.0f, 0.0f, 500, Smoothness.Start, () =>
                    {
                        // When it's done it goes to the next step
                        ++_Step;
                    });
                });
            });
            // Sets the amount of actions left to 2
            _ActionsLeft = 2;
        }

        /// <summary>
        /// Handles the logical part of the scene
        /// </summary>
        /// <param name="game">Game manager</param>
        public void Update(Game game)
        {
            // Updates the animation flow
            _Animation.Update(game.DeltaMilliseconds);

            // Gets if any keyboard key was just 'pressed'
            KeyboardKey keyboardKey = game.KeyboardManager.AnyTriggered;

            // If no key was pressed
            if (keyboardKey == null)
                // Then does nothing
                return;

            // If Escape was pressed
            if (keyboardKey.Key == Key.Escape)
                // Moves back to the title scene
                game.SceneManager.NextScene = new SceneTitle();

            // Checks at what scene step it is
            switch (_Step)
            {
                // This step is when you're selecting what action you're taking
                case 1:
                    bool actionChanged = false;

                    switch (keyboardKey.Key)
                    {
                        // If the Left Arrow was pressed
                        case Key.Left:
                            // Moves to the left option
                            --_ActionSelected;
                            actionChanged = true;
                            break;
                        // If Right Arrow was pressed
                        case Key.Right:
                            // Moves to the right option
                            ++_ActionSelected;
                            actionChanged = true;
                            break;
                        // If Enter was pressed
                        case Key.Enter:
                            // Runs the action
                            switch (_ActionSelected)
                            {
                                case 0:
                                    GameGlobals.Pet.Eat();
                                    break;
                                case 1:
                                    GameGlobals.Pet.Sleep();
                                    break;
                                case 2:
                                    GameGlobals.Pet.Exercise();
                                    break;
                                default:
                                    GameGlobals.Pet.Play();
                                    break;
                            }

                            // If the pet is fine
                            if (
                                (GameGlobals.Pet.Hunger    <  1.0f) &&
                                (GameGlobals.Pet.Energy    > -1.0f) &&
                                (GameGlobals.Pet.Fat       <  1.0f) &&
                                (GameGlobals.Pet.Happiness > -1.0f)
                            )
                            {
                                // Subtracts one from the actions left
                                --_ActionsLeft;
                                // Moves to the feedback screen
                                ++_Step;
                            }
                            // If anything about the pet is too critical
                            else
                            {
                                // Fades out to the gameover scene
                                _Animation = new SimpleAnimation(1.0f, 0.0f, 500, Smoothness.Start, () =>
                                {
                                    game.SceneManager.NextScene = new SceneGameover();
                                });
                                _Step = 4;
                            }


                            break;
                    }

                    // If you moved to a different action
                    if (actionChanged)
                    {
                        _ActionSelected %= 4;

                        while (_ActionSelected < 0)
                            _ActionSelected += 4;

                        // Do a scrolling animation to the right item
                        _Animation.Reset(
                            _Animation.Value,
                            _ActionSelected,
                            250,
                            Smoothness.StartArrival
                        );
                    }

                    break;
                // This is the feedback part
                case 2:
                    // If there's still an action left
                    if (_ActionsLeft == 1)
                        // Goes back to action selection part
                        --_Step;
                    // Otherwise
                    else
                    {
                        // Fades out to the report scene
                        _Animation.Reset(1.0f, 0.0f, 500, Smoothness.Start, () => game.SceneManager.NextScene = new SceneReport());
                        ++_Step;
                    }
                    break;
            }

        }

        /// <summary>
        /// Handles the drawing part of the scene
        /// </summary>
        /// <param name="game">Game manager</param>
        public void Draw(Game game)
        {
            // Game is a generic class, so make sure to convert it to ConsoleGame
            ConsoleGame consoleGame = (ConsoleGame)game;
            // ConsoleGame has a screen to draw whereas Game doesn't
            ConsoleScreen consoleScreen = consoleGame.Screen;

            // Saves the animation value for later use
            float animationValue = _Animation.Value;

            // Clears the whole screen
            consoleScreen.Clear();

            // Checks at what scene step it is
            switch (_Step)
            {
                // This is the part where the current day is displayed
                case 0:
                    // Builds the day string
                    // It's GameGlobals.Days + 1 because that variable starts at 0
                    string       dayText = string.Format("Day {0}", GameGlobals.Days + 1);
                    ConsoleColor dayTextColor;

                    // Gets the right color for the fade animation
                    if (animationValue < 0.25f)
                        dayTextColor = ConsoleColor.Black;
                    else if (animationValue < 0.5f)
                        dayTextColor = ConsoleColor.DarkGray;
                    else if (animationValue < 0.75f)
                        dayTextColor = ConsoleColor.Gray;
                    else
                        dayTextColor = ConsoleColor.White;

                    // Centers and draws the text on the screen
                    consoleScreen.DrawText((32 - dayText.Length) / 2, 8, dayText, (x, y, i, block, a, b, c, chr) =>
                    {
                        return new ConsoleBlock(ConsoleColor.Black, dayTextColor, chr);
                    });
                    break;
                // This is the part where you select an action
                case 1:
                    // Draws the pet art on the screen
                    consoleScreen.DrawText(1, 1, _PetArt);
                    // Draws "Please select an action" and the amount of actions you have left
                    consoleScreen.DrawText(5, 10, string.Format("Please select an action\nYou have {0} actions left", _ActionsLeft));
                    // Draws the action box
                    consoleScreen.DrawText(5, 12, _ActionsBox);
                    // Draws the current selected action
                    // Animation value in this case points to the string index of the selected action
                    consoleScreen.DrawText(8, 13, Utils.StringRoulette(_ActionsString, 16, (int)Math.Round(animationValue * 10)));
                    break;
                // This is the part where you see the pet feedback
                case 2:
                // This is the part where it's moving to the report scene
                case 3:
                // This is the part where it's moving to the gameover scene
                case 4:

                    ConsoleColor textColor;
                    Func<int, int, int, ConsoleBlock, int, int, int, char, ConsoleBlock> func = null;

                    // If it is on the the feedback step
                    if (_Step == 2)
                        // Then just uses white for the texts
                        textColor = ConsoleColor.White;
                    else
                    {
                        // If it's on any other step, it means that it's fading out
                        // Then we have to get the appropriate color for the texts
                        if (animationValue < 0.25f)
                            textColor = ConsoleColor.Black;
                        else if (animationValue < 0.5f)
                            textColor = ConsoleColor.DarkGray;
                        else if (animationValue < 0.75f)
                            textColor = ConsoleColor.Gray;
                        else
                            textColor = ConsoleColor.White;

                        // We have to prepare a function to color the texts to later user
                        func = (x, y, i, block, a, b, c, chr) =>
                        {
                            return new ConsoleBlock(ConsoleColor.Black, textColor, chr);
                        };
                    }

                    // Draws the pet art on the screen
                    consoleScreen.DrawText(1, 1, _PetArt, func);

                    // Gets the pet name and status
                    string petName   = GameGlobals.Pet.Name;
                    string petStatus = GameGlobals.Pet.Status;
                    
                    // And their length
                    int petNameLength   = petName.Length;
                    int petStatusLength = petStatus.Length;

                    // And adjust them on the screen in order to center both
                    if (petNameLength > petStatusLength)
                        consoleScreen.DrawText(
                            (32 - petNameLength) / 2,
                            9,
                            petName + "\n" + Utils.StringPadBoth(petStatus, petNameLength),
                            func
                        );
                    else
                        consoleScreen.DrawText(
                            (32 - petStatusLength) / 2,
                            9,
                            Utils.StringPadBoth(petName, petStatusLength) + "\n" + petStatus,
                            func
                        );

                    // If it's on the feedback part, tells the player that they can proceed to select another action
                    if (_Step == 2)
                        consoleScreen.DrawText(10, 14, "PRESS ANY KEY");

                    break;
            }

            // When we're done drawing everything we needed, updates the screen
            consoleScreen.Show();
        }

        public void Terminate(Game game)
        {
        }

    }

}