namespace Tamagottagettemall
{

    /// <summary>
    /// Scene structure
    /// </summary>
    public interface ISceneBase
    {

        /// <summary>
        /// Starts up all the scene elements
        /// </summary>
        /// <param name="game">Game manager</param>
        void Start(Game game);

        /// <summary>
        /// Handles the logical part of the scene
        /// </summary>
        /// <param name="game">Game manager</param>
        void Update(Game game);

        /// <summary>
        /// Handles the drawing part of the scene
        /// </summary>
        /// <param name="game">Game manger</param>
        void Draw(Game game);

        /// <summary>
        /// Disposes of all scene elements
        /// </summary>
        /// <param name="game">Game manager</param>
        void Terminate(Game game);

    }

}