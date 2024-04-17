namespace PlaywrightTests.Data;

public class Location{

    public readonly string Geolocation;
    public readonly string Address;
    public readonly string AddressMatch;
    
    public Location(string geolocation, string address, string addressMatch){
        Geolocation = geolocation;
        Address = address;
        AddressMatch = addressMatch;
    }
}