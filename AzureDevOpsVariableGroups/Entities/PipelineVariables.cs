

using Newtonsoft.Json;

namespace AzureDevOpsVariableGroups.Entities {
    public partial class PipelineVariables {
        [JsonProperty ("variables")]
        public Variables Variables { get; set; }

        [JsonProperty ("type")]
        public string Type { get; set; }

        [JsonProperty ("name")]
        public string Name { get; set; }

        [JsonProperty ("description")]
        public string Description { get; set; }
    }

    public partial class Variables {
        [JsonProperty ("UserId")]
        public VariableGroupProviderData UserId { get; set; }

        [JsonProperty ("SecretKey")]
        public VariableGroupProviderData SecretKey { get; set; }     
    }

    public partial class VariableGroupProviderData {
        [JsonProperty ("value")]
        public string Value { get; set; }

        [JsonProperty ("isSecret")]
        public bool IsSecret { get; set; } = true;
    }

}