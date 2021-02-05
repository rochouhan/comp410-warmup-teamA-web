using System.Collections.Generic;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
public class RootStructure
{
    public string user_id { get; set; } 
    public double latitude { get; set; } 
    public double longitude { get; set; } 
    public long time { get; set; } 
    public List<OtherInfoStructure> other_info { get; set; } 
}
