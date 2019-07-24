/*
 *To be renamed, but otherwise in charge of probabilities for vehicles entering and leaving.
 */
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
