using System.Collections;
using System.Collections.Generic;
using ARLocation;
using UnityEngine;

public class HomeOrUQLakes : MonoBehaviour
{
    public double HomeLatitude;
    public double HomeLongitude;
    public double UQLakesLatitude;
    public double UQLakesLongitude;
    public PlaceAtLocation placeAtLocation;

    // Start is called before the first frame update
    void Start()
    {
        UpdatePlaceAtLocation();
    }

    public void UpdatePlaceAtLocation()
    {
        var useHomeCoordinates = GameObject
            .FindGameObjectWithTag("StateManager")
            .GetComponent<EnvironmentManager>()
            .useHomeCoordinates;
        // If PlaceAtLocation component is not set, assume this component belongs to a GameObject with one.
        placeAtLocation ??= gameObject.GetComponent<PlaceAtLocation>();
        // Update the GameObject's real-world location.
        placeAtLocation.Location = new Location()
        {
            Latitude = useHomeCoordinates ? HomeLatitude : UQLakesLatitude,
            Longitude = useHomeCoordinates ? HomeLongitude : UQLakesLongitude,
            Altitude = 0,
            AltitudeMode = AltitudeMode.GroundRelative
        };
    }
}
