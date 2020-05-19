using Newtonsoft.Json;

namespace AzureDevOpsVariableGroups.Entities
{
    public class AuthorizeVariableGroup
    {        
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("authorized")]
        public bool Authorized { get; set; }
    }
}