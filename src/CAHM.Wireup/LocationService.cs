using System;
using System.Web;
using CAHM.ViewModels;

namespace CAHM.Wireup
{
    public class LocationService : ILocationService
    {
        private readonly HttpSessionStateBase _session;
        private const string Key = "CAHM.Wireup.LocationService.Location";

        public LocationService(HttpSessionStateBase session)
        {
            _session = session;
        }

        public void UpdateLocation(Location location)
        {
            if (location == null)
                return;
            if (location.Latitude == null || location.Latitude == 0)
                return;
            if (location.Longitude == null || location.Longitude == 0)
                return;

            _session[Key] = location;
        }

        public Location GetLocation()
        {
            return _session[Key] as Location;
        }
    }
}
