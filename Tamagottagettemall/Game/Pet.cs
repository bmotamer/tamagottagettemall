using System;

namespace Tamagottagettemall
{

    /// <summary>
    /// Tamagotchi pet
    /// </summary>
    public class Pet
    {

        /// <summary>
        /// Pet's name
        /// </summary>
        protected string _Name;

        /// <summary>
        /// How hungry the pet is
        /// </summary>
        protected float _Hunger;

        /// <summary>
        /// How much energy the pet has
        /// </summary>
        protected float _Energy;

        /// <summary>
        /// How much fat the pet has
        /// </summary>
        protected float _Fat;

        /// <summary>
        /// How happy the pet is
        /// </summary>
        protected float _Happiness;

        // These are just amounts that are added or subtracted depending on what action is done
        // See more below
        protected const float SmallAmount = 0.0625f;
        protected const float MediumAmount = 0.125f;
        protected const float LargeAmount = 0.25f;

        /// <summary>
        /// Gets or sets the pet name
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set
            {
                // The pet can't be null or empty
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException();

                // If it's not empty, just sets it
                _Name = value;
            }
        }

        /// <summary>
        /// Gets the pet's hunger
        /// </summary>
        public float Hunger
        {
            get { return _Hunger; }
            // It has a protected set that clamps the value from -1 to 1
            protected set { _Hunger = Utils.FloatClamp(value, -1.0f, 1.0f); }
        }

        /// <summary>
        /// Gets the pet's energy
        /// </summary>
        public float Energy
        {
            get { return _Energy; }
            // It has a protected set that clamps the value from -1 to 1
            protected set { _Energy = Utils.FloatClamp(value, -1.0f, 1.0f); }
        }

        /// <summary>
        /// Gets how fat the pet is
        /// </summary>
        public float Fat
        {
            get { return _Fat; }
            // It has a protected set that clamps the value from -1 to 1
            protected set { _Fat = Utils.FloatClamp(value, -1.0f, 1.0f); }
        }

        /// <summary>
        /// Gets how happy the pet is
        /// </summary>
        public float Happiness
        {
            get { return _Happiness; }
            // It has a protected set that clamps the value from -1 to 1
            protected set { _Happiness = Utils.FloatClamp(value, -1.0f, 1.0f); }
        }

        /// <summary>
        /// Creates an instance of a pet
        /// </summary>
        /// <param name="name">Pet's name</param>
        public Pet(string name)
        {
            // Sets its name
            Name = name;

            // Make the initial status random
            Random random = new Random(Guid.NewGuid().GetHashCode());
            Hunger    = (float)(0.25 * random.NextDouble());
            Energy    = (float)(0.25 * random.NextDouble());
            Fat       = (float)(0.25 * random.NextDouble());
            Happiness = (float)(0.25 * random.NextDouble());
        }

        /// <summary>
        /// Makes the pet eat
        /// </summary>
        public void Eat()
        {
            Hunger    -= LargeAmount;
            Energy    += SmallAmount;
            Fat       += MediumAmount;
            Happiness += SmallAmount;
        }

        /// <summary>
        /// Makes the pet sleep
        /// </summary>
        public void Sleep()
        {
            Hunger    += MediumAmount;
            Energy    += LargeAmount;
            Fat       -= SmallAmount;
            Happiness += SmallAmount;
        }

        /// <summary>
        /// Makes the pet exercise
        /// </summary>
        public void Exercise()
        {
            Hunger    += SmallAmount;
            Energy    -= LargeAmount;
            Fat       -= MediumAmount;
            Happiness -= MediumAmount;
        }

        /// <summary>
        /// Plays with the pet
        /// </summary>
        public void Play()
        {
            Hunger    += SmallAmount;
            Energy    -= MediumAmount;
            Fat       -= SmallAmount;
            Happiness += LargeAmount;
        }

        /// <summary>
        /// Gets the pet status
        /// </summary>
        public string Status
        {
            get
            {
                // Gets positives values for each value
                float absHunger    = Math.Abs(_Hunger);
                float absEnergy    = Math.Abs(_Energy);
                float absFat       = Math.Abs(_Fat);
                float absHappiness = Math.Abs(_Happiness);

                // First I was meant to do something like, if you had too much or too little you'd gameover
                // The logic is here, but it's not being used as it was supposed
                // --- Gets the most critical status
                switch (Utils.FloatMaxIndex(absHunger, absEnergy, absFat, absHappiness))
                {
                    // If the hunger is critical
                    case 0:
                        // If it's too low, it means the pet is getting over fed
                        if (_Hunger < 0.0f)
                        {
                            if (_Hunger > -0.25)
                                return "*burp*";
                            else if (_Hunger > -0.5)
                                return "I feel sick";
                            else if (_Hunger > -0.75)
                                return "*vomits*";
                            else
                                return "STOP FEEDING ME";
                        }
                        // If it's too high, it means the pet is getting hungry
                        else
                        {
                            if (_Hunger < 0.25)
                                return "I'm a bit hungry";
                            else if (_Hunger < 0.5)
                                return "I'm quite hungry";
                            else if (_Hunger < 0.75)
                                return "I'm very hungry!";
                            else
                                return "PLEASE FEED ME";
                        }
                    // If the energy is critical
                    case 1:
                        // If the energy is too low, the pet is getting tired
                        if (_Energy < 0.0f)
                        {
                            if (_Energy > -0.25)
                                return "*yawn*";
                            else if (_Energy > -0.5)
                                return "I'm tired";
                            else if (_Energy > -0.75)
                                return "I'm sleepy";
                            else
                                return "Zzz";
                        }
                        // If the energy is too high, the pet is getting hyperactive
                        else
                        {
                            if (_Energy < 0.25)
                                return "*tuturu*";
                            else if (_Energy < 0.5)
                                return "Did I drink coffee?";
                            else if (_Energy < 0.75)
                                return "DING DONG DING DONG";
                            else
                                return "WOOOOOOOOOOOOOOOOOO";
                        }
                    // If the fat is critical
                    case 2:
                        // If it's too low, the pet is getting skinny
                        if (_Fat < 0.0f)
                        {
                            if (_Fat > -0.25)
                                return "Losing weight!";
                            else if (_Fat > -0.5)
                                return "I feel skinny";
                            else if (_Fat > -0.75)
                                return "I'm way too skinny!";
                            else
                                return "PLEASE HELP";
                        }
                        // If it's too high, the pet is getting fat
                        else
                        {
                            if (_Fat < 0.25)
                                return "Chubby!";
                            else if (_Fat < 0.5)
                                return "I'm fat!";
                            else if (_Fat < 0.75)
                                return "I'm getting obese!";
                            else
                                return "I'M GOING TO EXPLODE!";
                        }
                    // If the happiness is the critical one
                    default:
                        // If it's too low, the pet is sad
                        if (_Happiness < 0.0f)
                        {
                            if (_Happiness > -0.25)
                                return "I feel a little sad";
                            else if (_Happiness > -0.5)
                                return "*sad face*";
                            else if (_Happiness > -0.75)
                                return "*crying*";
                            else
                                return "*crying intesifies*";
                        }
                        // If it's too high, the pet is too happy ༼ つ ◕_◕ ༽つ
                        else
                        {
                            if (_Happiness < 0.25)
                                return "Yay";
                            else if (_Happiness < 0.5)
                                return "YAY";
                            else if (_Happiness < 0.75)
                                return "Yaay";
                            else
                                return "Yaaay";
                        }
                }
            }
        }

    }

}