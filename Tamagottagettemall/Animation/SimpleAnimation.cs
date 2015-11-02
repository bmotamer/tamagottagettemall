using System;

namespace Tamagottagettemall
{

    /// <summary>
    /// Simple animation that goes from a value to another
    /// </summary>
    public class SimpleAnimation
    {

        /// <summary>
        /// Animation progress
        /// </summary>
        protected float _Current;

        /// <summary>
        /// From what value the animation comes from
        /// </summary>
	    public float From;

        /// <summary>
        /// To what value the animation goes to
        /// </summary>
	    public float To;

        /// <summary>
        /// Animation duration
        /// </summary>
	    public float Time;

        /// <summary>
        /// Animation easing
        /// </summary>
	    public Smoothness Smoothness;

        /// <summary>
        /// Callback action
        /// </summary>
	    public Action Action;
	    
        /// <summary>
        /// Creates a simple animation that goes from a value to another
        /// </summary>
        /// <param name="from">From what value the animation comes from</param>
        /// <param name="to">To what value the animation goes to</param>
        /// <param name="time">Animation duration</param>
        /// <param name="smoothness">Animation easing</param>
        /// <param name="action">Callback action</param>
	    public SimpleAnimation(
		    float      from       = 0.0f,
		    float      to         = 0.0f,
		    float      time       = 0.0f,
		    Smoothness smoothness = Smoothness.None,
		    Action     action     = null
		    )
	    {
		    Reset(from, to, time, smoothness, action);
	    }
	    
        /// <summary>
        /// Resets the animation settings
        /// </summary>
        /// <param name="from">From what value the animation comes from</param>
        /// <param name="to">To what value the animation goes to</param>
        /// <param name="time">Animation duration</param>
        /// <param name="smoothness">Animation easing</param>
        /// <param name="action">Callback action</param>
	    public void Reset(
		    float      from       = 0.0f,
		    float      to         = 0.0f,
		    float      time       = 0.0f,
		    Smoothness smoothness = Smoothness.None,
		    Action     action     = null
		    )
	    {
            // Applies the new settings
		    From       = from;
		    To         = to;
		    Time       = time;
		    Smoothness = smoothness;
		    Action     = action;
		    
            // Resets the animation progress
		    _Current   = 0.0f;
	    }
	    
        /// <summary>
        /// Gets if the animation completed
        /// </summary>
	    public bool Finished { get { return _Current >= 1.0f; } }
	    
        /// <summary>
        /// Gets or sets the current value according to the animation progress
        /// TODO: use a sine and cosine table to improve performance
        /// </summary>
	    public float Value
	    {
		    get
		    {
                // Checks what kind of easing it's being used
			    switch (Smoothness)
			    {
                    // If it's smooth at the start
			        case Smoothness.Start:
                        // Then calculates the result using a cosine wave
				        return (float)(From + (To - From) * (1.0f - Math.Cos(0.5f * _Current * Math.PI)));
                    // If it's smooth at the end
			        case Smoothness.Arrival:
                        // Then calculates the result using a sine wave
				        return (float)(From + (To - From) * Math.Sin(0.5f * _Current * Math.PI));
                    // If it's smooth at the start and end
			        case Smoothness.StartArrival:
                        // Then calculates the result using half of a cosine wave
				        return (float)(From + (To - From) * 0.5f * (1.0f - Math.Cos(_Current * Math.PI)));
                    // If there's no easing
			        default:
                        // Lerp the values
				        return From + (To - From) * _Current;
			    }
		    }
		    set
		    {
                // If it's setting a value, makes sure that it's not out of bounds
			    switch (From > To ? (value >= From ? 1 : value <= To ? 2 : 0) : (value <= From ? 1 : value >= To ? 2 : 0))
			    {
                    // Case 1 happens when the value given is lower than the start value
			        case 1:
                        // Resets the progress to 0%
				        _Current = 0.0f;
				        break;
                    // Case 2 happens when the value given is higher than the end value
			        case 2:
                        // Resets the progress to 100%
				        _Current = 1.0f;
				        break;
                    // Default case happens when the given value is inbounds
			        default:
				        switch (Smoothness)
				        {
                            // If it's smooth at the start
				            case Smoothness.Start:
                                // Figures out at what progress percentage of the animation the given value must be using arc of cosine
					            _Current = (float)(Math.Acos(1.0f - (value - From) / (To - From)) * 2 / Math.PI);
					            break;
                            // If it's smooth at the end
				            case Smoothness.Arrival:
                                // Using arc of sine
					            _Current = (float)(Math.Asin((value - From) / (To - From)) * 2 / Math.PI);
					            break;
                            // If it's smooth at the start and end
				            case Smoothness.StartArrival:
                                // Using half the arc of cosine
					            _Current = (float)(Math.Acos(1.0f - (value - From) / (To - From) * 2) / Math.PI);
					            break;
                            // If there's no easing
				            default:
					            _Current = (value - From) / (To - From);
					            break;
				        }
                        break;
			    }
		    }
	    }
	    
        /// <summary>
        /// Updates the animation progress
        /// </summary>
        /// <param name="timeStep"></param>
	    public virtual void Update(float timeStep)
	    {
            // If the animation has any duration
		    if (Time > 0.0f)
		    {
                // Calculates the new progress
			    float temp = _Current + timeStep / Time;
			    
                // If there's any callback action set and the animation is about to reach 100%
			    if ((Action != null) && (_Current < 1.0f) && (temp >= 1.0f))
			    {
                    // Saves the current progress
				    float current = _Current;

                    // Calls the action
				    Action.Invoke();

                    // If the action changed the progress
				    if (_Current != current)
                        // Then don't apply the new progress
					    return;
			    }
			    
                // Applies the new progress
			    _Current = Math.Min(temp, 1.0f);
		    }
            // If the duration is 0
		    else
                // Then the animation is completed
			    _Current = 1.0f;
	    }
	    
    }

}