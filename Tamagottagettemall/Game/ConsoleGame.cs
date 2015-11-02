namespace Tamagottagettemall
{

    /// <summary>
    /// Controls the console game cycle
    /// </summary>
    public class ConsoleGame : Game
    {

        /// <summary>
        /// Console screen
        /// </summary>
        public readonly ConsoleScreen Screen;

        /// <summary>
        /// Creates a new console game manager
        /// </summary>
        /// <param name="width">Screen width</param>
        /// <param name="height">Screen height</param>
        /// <param name="firstScene">First scene</param>
        public ConsoleGame(
            int width,
            int height,
            ISceneBase firstScene
        ) : base(firstScene)
        {
            Screen = new ConsoleScreen(width, height);
        }

    }

}