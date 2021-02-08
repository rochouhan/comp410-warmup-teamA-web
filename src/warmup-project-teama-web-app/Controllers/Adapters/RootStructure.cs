using System.Collections.Generic;

/// <summary>
/// Structure used to parse the root of the JSON response received from the database when making a query.
/// </summary>
public class RootStructure
{
    public string user_id { get; set; } 
    public double latitude { get; set; } 
    public double longitude { get; set; } 
    public long time { get; set; } 
    public List<OtherInfoStructure> other_info { get; set; } 
}
