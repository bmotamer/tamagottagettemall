namespace Tamagottagettemall
{

    /// <summary>
    /// Defines a generic input button manager
    /// </summary>
    public abstract class InputButton
    {

        /// <summary>
        /// Button pressed state
        /// </summary>
        public InputButtonState State { get; protected set; }

        /// <summary>
        /// Can the button get to repeated state
        /// </summary>
        public bool IsRepeatEnabled;

        /// <summary>
        /// Repeat state timer
        /// </summary>
        public int RepeatTimer { get; protected set; }

        /// <summary>
        /// Gets if the button got to the 'repeated' cycle
        /// </summary>
        public bool IsRepeating { get; protected set; }

        /// <summary>
        /// Delay until the button gets to repeated state
        /// </summary>
        public int RepeatDelay;

        /// <summary>
        /// Button repeat interval
        /// </summary>
        public int RepeatInterval;

        /// <summary>
        /// Creates a generic input button manager
        /// </summary>
        /// <param name="game">Game manager</param>
        /// <param name="isDown">Is the button being pressed</param>
        public InputButton(Game game, bool isDown)
        {
            // Repeating is enabled by default
            IsRepeatEnabled = true;
            // Sets the repeat delay to 1 second
            RepeatDelay = 1000;
            // And the repeat interval to 0.5 seconds
            RepeatInterval = 500;
            // If the button is initially being pressed, set it as pressed, otherwise 'unpressed'
            State = isDown ? InputButtonState.Pressed : InputButtonState.None;
        }

        /// <summary>
        /// Updates the button state
        /// </summary>
        /// <param name="game">Game manager</param>
        /// <param name="isDown">Is the button being pressed</param>
        public void Update(Game game, bool isDown)
        {
            // If the button is being pressed
            if (isDown)
            {
                // First checks its previous state
                switch (State)
                {
                    // If it was previously 'just pressed'
                    case InputButtonState.Triggered:
                        // Changes it to pressed
                        State = InputButtonState.Pressed;
                        break;
                    // If it was pressed, do nothing
                    case InputButtonState.Pressed:
                        break;
                    // If it was repeated
                    case InputButtonState.Repeated:
                        // Change it to pressed
                        State = InputButtonState.Pressed;
                        break;
                    // If it was 'unpressed'
                    default:
                        // Change it to 'just pressed'
                        State = InputButtonState.Triggered;
                        break;
                }

                // If repeating is enabled
                if (IsRepeatEnabled)
                {
                    // If the repeating timer is 0
                    if (RepeatTimer <= 0)
                        // If the repeating cycle started, set the timer to the interval, otherwise to the delay
                        RepeatTimer = IsRepeating ? RepeatInterval : RepeatDelay;

                    // Decreases the timer
                    RepeatTimer -= game.DeltaMilliseconds;

                    // When it reaches 0
                    if (RepeatTimer <= 0)
                    {
                        // Changes the state to repeated
                        State = InputButtonState.Repeated;
                        // And set that the repeating cycle started
                        IsRepeating = true;
                    }
                }
                // If repeating is disabled
                else
                {
                    // Then sets the repeating cycle flag to false
                    IsRepeating = false;
                    // And resets the timer
                    RepeatTimer = 0;
                }
            }
            // If the button is 'unpressed'
            else
            {
                // Checks it previous state
                switch (State)
                {
                    // If it was previously 'unpressed'
                    case InputButtonState.None:
                        // Do nothing
                        break;
                    // If it was previously 'just released'
                    case InputButtonState.Released:
                        // Sets its state to 'unpressed'
                        State = InputButtonState.None;
                        break;
                    // If it was 'just pressed', pressed, repeated or whatever
                    default:
                        // Changes it to released
                        State = InputButtonState.Released;
                        break;
                }

                // Also sets the repeating cycle flag to false
                IsRepeating = false;
                // And resets the timer
                RepeatTimer = 0;
            }
        }

    }

}