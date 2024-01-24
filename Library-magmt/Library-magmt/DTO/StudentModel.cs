using Newtonsoft.Json;

namespace Library_magmt.DTO
{
    public class StudentModel

    {

        // Class  Feilds / Properties

        [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
        public string? UId { get; set; }

        [JsonProperty(PropertyName = "studRollNo", NullValueHandling = NullValueHandling.Ignore)]
        public int StudRollNo { get; set; }

        [JsonProperty(PropertyName = "studPrnNo", NullValueHandling = NullValueHandling.Ignore)]
        public int StudPrnNo { get; set; }


        [JsonProperty(PropertyName = "studName", NullValueHandling = NullValueHandling.Ignore)]
        public string? StudName { get; set; }

        [JsonProperty(PropertyName = "studMoNo", NullValueHandling = NullValueHandling.Ignore)]
        public int StudMoNo { get; set; }

        [JsonProperty(PropertyName = "studEmail", NullValueHandling = NullValueHandling.Ignore)]
        public string? StudEmail { get; set; }




        [JsonProperty(PropertyName = "studPassword", NullValueHandling = NullValueHandling.Ignore)]
        public string? StudPassword { get; set; }

    }
}
