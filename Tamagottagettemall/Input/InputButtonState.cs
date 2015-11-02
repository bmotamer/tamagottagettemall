namespace Tamagottagettemall
{

    /// <summary>
    /// Button pressed state
    /// </summary>
    public enum InputButtonState
    {
        None      = 0,
        Triggered = 1,
        Pressed   = 2,
        Repeated  = 4,
        Released  = 8
    }

}