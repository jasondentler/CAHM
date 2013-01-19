using CAHM.ViewModels;

namespace CAHM
{
    public interface ILocationService
    {

        void UpdateLocation(Location location);
        Location GetLocation();

    }
}
