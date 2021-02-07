using Newtonsoft.Json;

public class OtherInfoStructure
{
    [JsonProperty(PropertyName = "content-type")]
    public string contentType { get; set; } 
    public string value { get; set; } 
    public string name { get; set; }
}