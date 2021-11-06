using Newtonsoft.Json;

namespace SaveUser.Model
{
    public class User
    {
        public int Id { get; set; }

        [JsonProperty("firstname")]
        public string FirstName { get; set; }

        [JsonProperty("lastname")]
        public string LastName { get; set; }

        [JsonProperty("mail")]
        public string Mail { get; set; }
    }
}
