using System;
using System.Windows.Input;

namespace Tamagottagettemall
{

    /// <summary>
    /// Defines the title screen
    /// </summary>
    public class SceneTitle : ISceneBase
    {

        /// <summary>
        /// ASCII art that will go from the right side of the screen to the left
        /// </summary>
        protected string _TitleArt;

        /// <summary>
        /// Animation manager to help with the screen animations
        /// </summary>
        protected SimpleAnimation _Animation;

        /// <summary>
        /// What step of the scene we are (see more below)
        /// </summary>
        protected byte _Step;

        /// <summary>
        /// Starts the scene elements
        /// </summary>
        /// <param name="game">Game manager</param>
        public void Start(Game game)
        {
            // Sets the game frame rate to 60
            game.DesiredFrameRate = 60;
            // Changes the title of the game
            Console.Title = "Tamagottagettemall";

            // Loads the ASCII art from a file
            _TitleArt = Utils.StringReadFile("Title.txt");

            // Sets the current step of the scene to 0
            _Step = 0;

            // This first animation handles the ASCII title animation that goes from right to left
            // This animation will represent the X-coordinate of the art position
            // It goes from 32 (right limit) and goes all the way to -360 (the text is 360 characters wide, so -360 would be the left limit)
            // The animation will take 8000 milliseconds and it will start slow, go through fast and arrives slow
            _Animation = new SimpleAnimation(32, -360, 8000, Smoothness.StartArrival, () =>
            {
                // When it's done, goes to the next step of the scene
                ++_Step;

                // Another animation happens now
                // This animation represents the fade in of the small title from the title screen
                // It goes from 0 (totally transparent) to 1 (totally opaque)
                // It takes 500 milliseconds and starts fast, goes through fast and arrives slow
                _Animation.Reset(0.0f, 1.0f, 500, Smoothness.Arrival, () =>
                {
                    // When it's done, goes to the next step of the scene
                    ++_Step;

                    // We will set up the "PRESS START" animation now
                    // As it's gonna flash, we have to make a show animation and a hide animation
                    Action hideAction = null;
                    Action showAction = null;

                    // The show animation will make the "PRESS START" visible for 500 milliseconds
                    // This animation goes from 1 to 1 because the text is meant to be totally opaque this whole time
                    // No need for smoothness, because the values are not varying
                    showAction = () =>
                    {
                        // And when the animation is done, calls the hide animation
                        _Animation.Reset(1.0f, 1.0f, 500, Smoothness.None, hideAction);
                    };

                    // The hide animation will make the "PRESS START" invisible for 1000 milliseconds
                    // This animation goes from 0 to 0 because the text is meant to be totally transparent this whole time
                    // No need for smoothness, because the values are not varying
                    hideAction = () =>
                    {
                        // And when the animation is done, calls the show animation
                        // You may notice by now that the animation calls another after it's done - that's how it loops
                        _Animation.Reset(0.0f, 0.0f, 1000, Smoothness.None, showAction);
                    };

                    // First it starts with the show animation
                    showAction();
                });
                
            });
        }

        /// <summary>
        /// Handles the logical part of the scene
        /// </summary>
        /// <param name="game">Game manager</param>
        public void Update(Game game)
        {
            // Gets if any key was 'just pressed'
            KeyboardKey key = game.KeyboardManager.AnyTriggered;

            // If no key was 'just pressed'
            if (key == null)
                // Just updates the animation flow
                _Animation.Update(game.DeltaMilliseconds);
            // If Esc was pressed
            else if (key.Key == Key.Escape)
                // Closes the game
                Environment.Exit(0);
            // If it's not showing "PRESS START" and you pressed some key
            else if (_Step != 2)
                // Skips the current animation by forcing it to progress by its duration
                _Animation.Update(_Animation.Time);
            // If it's showing "PRESS START" and you pressed Enter
            else if (key.Key == Key.Enter)
            {
                // Goes to the step where it fades out the title and move on to the game
                ++_Step;
                // Changes the current animation to the title fade out animation
                // This animation goes from 1 (totally opaque) to 0 (totally transparent)
                // It takes 500 milliseconds and starts slow, goes through fast and arrives fast
                _Animation.Reset(1.0f, 0.0f, 500, Smoothness.Start, () =>
                {
                    // When it's completed, move on to the naming scene
                    game.SceneManager.NextScene = new SceneName();
                });
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

            // Checks what step we are to draw the right thing
            switch (_Step)
            {
                // This step is the one that the ASCII text art goes from right to left
                case 0:
                    // It draws the art on the screen
                    // The X offset is the animation value because only that axis is the one that's going to move
                    // The Y offset (3) is constant
                    // This animation value in this case goes from 32 to -360
                    consoleScreen.DrawText((int)Math.Round(animationValue), 3, _TitleArt);
                    break;
                // This step happens when the text is fading in
                case 1:
                // This step happens when the text is fading out
                case 3:
                    // The fade animation is actually a little trick
                    ConsoleColor titleColor;

                    // The animation value in this part goes from 0 to 1 or 1 to 0
                    // So we check the value to use an appropriate color
                    if (animationValue < 0.25f)
                        // If the value is too low, we set the text color to black
                        titleColor = ConsoleColor.Black;
                    // If it's reaching the middle of the animation
                    else if (animationValue < 0.5f)
                        // Sets it to dark gray
                        titleColor = ConsoleColor.DarkGray;
                    // If it's almost done
                    else if (animationValue < 0.75f)
                        // Sets it to gray
                        titleColor = ConsoleColor.Gray;
                    // When it's really almost there
                    else
                        // Just set it to white
                        titleColor = ConsoleColor.White;

                    // The standard draw text function uses black background and white foreground by default
                    // So if the fourth argument is given, you can make something different happen
                    consoleScreen.DrawText(7, 4, "Tamagottagettemall\n  by Bruno Tamer", (x, y, i, block, a, b, c, chr) =>
                    {
                        // On this case, we use the color that we 'calculated' before
                        return new ConsoleBlock(ConsoleColor.Black, titleColor, chr);
                    });

                    break;
                // If it's on the "PRESS START" part
                default:
                    // Draws the fully opaque title
                    consoleScreen.DrawText(7, 4, "Tamagottagettemall\n  by Bruno Tamer");

                    // The animation value in this case means wether the press start is visible or not
                    // If it is
                    if (animationValue == 1.0f)
                        // Then just draw it
                        consoleScreen.DrawText(11, 12, "PRESS START");

                    break;
            }

            // When we're done drawing everything we needed, updates the screen
            consoleScreen.Show();
        }

        /// <summary>
        /// Disposes of all scene elements
        /// </summary>
        /// <param name="game">Game manager</param>
        public void Terminate(Game game)
        {
        }

    }

}