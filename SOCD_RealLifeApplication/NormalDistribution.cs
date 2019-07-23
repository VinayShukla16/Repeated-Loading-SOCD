using System;
namespace SOCD_RealLifeApplication
{
    public class NormalDistribution
    {
        public static int probability()
        {
            var rand = new Random();
            return rand.Next(0, 10);
        }
    }
}
