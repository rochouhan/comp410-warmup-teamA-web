using Newtonsoft.Json;

/// <summary>
/// Structure used to parse the JSON response from the database when making a query. 
/// Specifically for the list of additional information.
/// </summary>
public class OtherInfoStructure
{
    [JsonProperty(PropertyName = "content-type")]
    public string contentType { get; set; } 
    public string value { get; set; } 
    public string name { get; set; }
}