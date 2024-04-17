namespace PlaywrightTests.Data;

public static class TestData{

    public static IEnumerable<Location> GetLocations(){
        yield return new Location("51.33247614463385, 12.40709919932379", "Eilenburger Str. 27, 04317 Leipzig");
        yield return new Location("51.33419947089127, 12.405759861784535", "Charlottenstraße 5, 04317 Leipzig");
        yield return new Location("27.29282932688367, -81.36412032990955", "403 S Oak Ave, Lake Placid, FL 33852, USA");
        yield return new Location("51.34546824986753, 12.382017251901123", "Willy-Brandt-Platz 5, 04109 Leipzig");
    }

    public static IEnumerable<Location> GetInvalidLocations(int amount = -1){
        yield return new Location("91.000000 180.000000", "Eilenburger Str. 27, 04317 Leipzig");
    }
}