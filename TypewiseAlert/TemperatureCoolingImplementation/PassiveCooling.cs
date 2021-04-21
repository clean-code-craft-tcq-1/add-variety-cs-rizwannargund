
namespace TypewiseAlert
{
    [CustomAttribute("PASSIVE_COOLING", value = 0)]
    public class PassiveCooling : CoolingLimits, ICoolingLimits
    {
        public int[] GetLimits()
        {
           return new int[] { Lower = 0, Higher = 35 };
        }
    }
}
