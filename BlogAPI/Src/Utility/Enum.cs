using System.Text.Json.Serialization;

namespace BlogAPI.Src.Utility
{
    public class Enum
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum UserType
        {
            ADMIN,
            NORMAL
        }
    }
}
