using System;
using System.Diagnostics;
using System.Threading;

namespace Tamagottagettemall
{

    /// <summary>
    /// Controls the game cycle
    /// TODO: separate Update and Draw into different Threads
    /// TODO: if desired frame rate is null, make sure to use the Environment.TickCount
    /// TODO: make a InputManager class
    /// </summary>
    public class Game : IDisposable
    {

        /// <summary>
        /// Scene flow manager
        /// </summary>
        protected SceneManager _SceneManager;

        /// <summary>
        /// Keyboard input manager
        /// </summary>
        protected KeyboardManager _KeyboardManager;

        /// <summary>
        /// Delay between cycles
        /// </summary>
        protected int _Timeout;

        /// <summary>
        /// Desired frame rate
        /// </summary>
        protected int? _DesiredFrameRate;

        /// <summary>
        /// Actual frame rate
        /// </summary>
        protected int _FrameRate;

        /// <summary>
        /// Last elapsed milliseconds
        /// </summary>
        protected int _DeltaMilliseconds;

        /// <summary>
        /// Gets the scene flow manager
        /// </summary>
        public SceneManager SceneManager { get { return _SceneManager; } }

        /// <summary>
        /// Gets the keyboard input manager
        /// </summary>
        public KeyboardManager KeyboardManager { get { return _KeyboardManager; } }

        /// <summary>
        /// Gets the last elapsed milliseconds
        /// </summary>
        public int DeltaMilliseconds { get { return _DeltaMilliseconds; } }

        /// <summary>
        /// Gets or sets the desired frame rate
        /// NOTICE: it's highly recommended to set one, specially it the game is time-based
        /// </summary>
        public int? DesiredFrameRate
        {
            get { return _DesiredFrameRate; }
            set
            {
                // If the new value is not null
                if (value.HasValue)
                {
                    int desiredFrameRate = value.Value;

                    // Make sure its not 0 or any value below that
                    if (desiredFrameRate <= 0)
                        throw new ArgumentException();

                    // Applies the new desired frame rate
                    _DesiredFrameRate = desiredFrameRate;
                    // Updates the delay between cycles
                    _Timeout = (int)Math.Round(1000.0f / desiredFrameRate);
                }
                // If it is null
                else
                    // Then there's not limit for the frame rate
                    _DesiredFrameRate = null;
            }
        }

        /// <summary>
        /// Gets the actual frame rate
        /// </summary>
        public int FrameRate { get { return _FrameRate; } }

        /// <summary>
        /// Creates a new game manager
        /// </summary>
        /// <param name="firstScene">First scene</param>
        public Game(ISceneBase firstScene)
        {
            _SceneManager    = new SceneManager(firstScene);
            _KeyboardManager = new KeyboardManager(this);
        }

        /// <summary>
        /// Runs the game while there's a scene running
        /// </summary>
        public void Run()
        {
            // Creates a stopwatch to measure how long things are taken so it can stabilize the timeout
            Stopwatch stopWatch = new Stopwatch();
            int       timeout;

            // Creates some variables to count the frame rate
            int frameCount      = 0;
            int frameCountTimer = 0;

            // Keeps the game running while there's a scene running
            while (_SceneManager.IsRunning)
            {
                // Reset the stopwatch to 0
                stopWatch.Reset();
                // Start tracking time
                stopWatch.Start();
                // Updates the user keyboard input
                _KeyboardManager.Update(this);
                // Updates the current scene
                _SceneManager.Update(this);
                // Draws the current scene
                _SceneManager.Draw(this);
                // Stops tracking time
                stopWatch.Stop();

                // Saves how long it took
                _DeltaMilliseconds = (int)stopWatch.ElapsedMilliseconds;

                // If a desired frame rate was set
                if (_DesiredFrameRate.HasValue)
                {
                    // Then tries to calculate a stabilized delay
                    // TODO: risky, underflows can happen
                    timeout = _Timeout - _DeltaMilliseconds;

                    // If the delay is over 0, it means the game is running smoothly
                    if (timeout > 0)
                    {
                        // Adds the delay to the elapsed time
                        _DeltaMilliseconds += timeout;
                        // Waits
                        Thread.Sleep(timeout);
                    }
                }

                // FPS means frames per second, so adds the elapsed time to the timer
                frameCountTimer += _DeltaMilliseconds;

                // 1 second is equals to 1000 milliseconds, so if the timer has reached this value
                if (frameCountTimer >= 1000)
                {
                    while (frameCountTimer >= 1000)
                        frameCountTimer -= 1000;

                    // That means that it should save the frame count to the frame rate as a second has passed
                    _FrameRate = frameCount;
                    // Resets the fame count so it can count again
                    frameCount = 0;
                }
                // If it hasn't yet
                else
                    // Just keep counting frames
                    ++frameCount;
            }
        }

        /// <summary>
        /// Disposes of all things used by this instance
        /// </summary>
        public void Dispose()
        {
            _SceneManager = null;
        }

    }

}