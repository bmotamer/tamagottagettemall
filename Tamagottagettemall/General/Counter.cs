using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagottagettemall
{

    public enum Smoothness
    {
	    None         = 0,
	    Start        = 1,
	    Arrival      = 2,
	    StartArrival = 3
    }

    public class Counter
    {
	
	    public float      From;
	    public float      To;
	    public float      Time;
	    public Smoothness Smoothness;
	    public Action     Action;
	
	    protected float _Current;
	
	    public Counter(
		    float      from       = 0.0f,
		    float      to         = 0.0f,
		    float      time       = 0.0f,
		    Smoothness smoothness = Smoothness.None,
		    Action     action     = null
		    )
	    {
		    Reset(from, to, time, smoothness, action);
	    }
	
	    public void Reset(
		    float      from       = 0.0f,
		    float      to         = 0.0f,
		    float      time       = 0.0f,
		    Smoothness smoothness = Smoothness.None,
		    Action     action     = null
		    )
	    {
		    From       = from;
		    To         = to;
		    Time       = time;
		    Smoothness = smoothness;
		    Action     = action;
		
		    _Current   = 0.0f;
	    }
	
	    public bool Finished { get { return _Current >= 1.0f; } }
	
	    public float Value
	    {
		    get
		    {
			    switch (Smoothness)
			    {
			    case Smoothness.Start:
				    return (float)(From + (To - From) * (1.0f - Math.Cos(0.5f * _Current * Math.PI)));
			    case Smoothness.Arrival:
				    return (float)(From + (To - From) * Math.Sin(0.5f * _Current * Math.PI));
			    case Smoothness.StartArrival:
				    return (float)(From + (To - From) * 0.5f * (1.0f - Math.Cos(_Current * Math.PI)));
			    default:
				    return From + (To - From) * _Current;
			    }
		    }
		    set
		    {
			    switch (From > To ? (value >= From ? 1 : value <= To ? 2 : 0) : (value <= From ? 1 : value >= To ? 2 : 0))
			    {
			    case 1:
				    _Current = 0.0f;
				    break;
			    case 2:
				    _Current = 1.0f;
				    break;
			    default:
				    switch (Smoothness)
				    {
				    case Smoothness.Start:
					    _Current = (float)(Math.Acos(1.0f - (value - From) / (To - From)) * 2 / Math.PI);
					    break;
				    case Smoothness.Arrival:
					    _Current = (float)(Math.Asin((value - From) / (To - From)) * 2 / Math.PI);
					    break;
				    case Smoothness.StartArrival:
					    _Current = (float)(Math.Acos(1.0f - (value - From) / (To - From) * 2) / Math.PI);
					    break;
				    default:
					    _Current = (value - From) / (To - From);
					    break;
				    }
				    break;
			    }
		    }
	    }
	
	    public virtual void Update(float timeStep)
	    {
		    if (Time > 0.0f)
		    {
			    float temp = _Current + timeStep / Time;
			
			    if ((Action != null) && (_Current < 1.0f) && (temp >= 1.0f))
			    {
				    float current = _Current;
				    Action.Invoke();
				    if (_Current != current)
					    return;
			    }
			
			    _Current = Math.Min(temp, 1.0f);
		    }
		    else
			    _Current = 1.0f;
	    }
	
    }
}
