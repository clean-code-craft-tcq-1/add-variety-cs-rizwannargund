
namespace TypewiseAlert
{
    public interface ICoolingLimits
    {
        int[] GetLimits();
    }
    public enum CoolingType
    {
        PASSIVE_COOLING,
        HI_ACTIVE_COOLING,
        MED_ACTIVE_COOLING
    };
}
