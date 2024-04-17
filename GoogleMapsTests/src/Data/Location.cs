namespace PlaywrightTests.Data;

public class Location{

    public readonly string Geolocation;
    public readonly string Address;
    
    public Location(string geolocation, string address){
        Geolocation = geolocation;
        Address = address;
    }
}