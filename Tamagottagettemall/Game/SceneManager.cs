namespace Tamagottagettemall
{

    /// <summary>
    /// Controls the scene flow
    /// </summary>
    public class SceneManager
    {

        /// <summary>
        /// Current scene
        /// </summary>
        protected ISceneBase _CurrentScene;

        /// <summary>
        /// If the current scene has started
        /// </summary>
        protected bool _HasStarted;

        /// <summary>
        /// Gets or sets the next scene
        /// </summary>
        public ISceneBase NextScene;

        /// <summary>
        /// Gets the current scene
        /// </summary>
        public ISceneBase CurrentScene { get { return _CurrentScene; } }

        /// <summary>
        /// Gets if there's a current scene running or if there's one on the way
        /// </summary>
        public bool IsRunning { get { return (_CurrentScene != null) || (NextScene != null);} }

        /// <summary>
        /// Creates a new scene manager
        /// </summary>
        /// <param name="firstScene">First scene</param>
        public SceneManager(ISceneBase firstScene)
        {
            NextScene = firstScene;
        }

        /// <summary>
        /// Updates the scene flow
        /// </summary>
        /// <param name="game">Game manager</param>
        public void Update(Game game)
        {
            // If the next scene is the current scene
            if (_CurrentScene == NextScene)
            {
                // Then just keep it updated
                if (_CurrentScene != null)
                    _CurrentScene.Update(game);
            }
            // If the next scene is something different, but the current one has started
            else if (_HasStarted)
            {
                // Then first terminates the current one
                _CurrentScene.Terminate(game);
                _HasStarted = false;
            }
            // If the next scene is something different and the current one hasn't started or just terminated
            else
            {
                // Goes to the next one
                _CurrentScene = NextScene;
                // And start it
                if (_CurrentScene != null)
                    _CurrentScene.Start(game);
                _HasStarted = true;
            }
        }

        /// <summary>
        /// Draws the current scene
        /// </summary>
        /// <param name="game">Game manager</param>
        public void Draw(Game game)
        {
            if (_CurrentScene != null)
                _CurrentScene.Draw(game);
        }

    }

}