
using System;

namespace mindfulness;

public class BreathingActivity : Activity
{
    public BreathingActivity()
        : base(
            "Breathing",
            "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    protected override void PerformActivity()
    {
        int elapsedSeconds = 0;
        bool breatheIn = true;

        while (elapsedSeconds < Duration)
        {
            int breathDuration = Math.Min(
                4, Duration - elapsedSeconds);

            if (breatheIn)
            {
                Console.WriteLine("\nBreathe in...");
            }
            else
            {
                Console.WriteLine("\nBreathe out...");
            }

            ShowCountDown(breathDuration);

            elapsedSeconds += breathDuration;
            breatheIn = !breatheIn;
        }
    }
}
