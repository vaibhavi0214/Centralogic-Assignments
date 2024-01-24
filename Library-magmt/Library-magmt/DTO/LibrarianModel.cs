using Newtonsoft.Json;

namespace Library_magmt.DTO
{
    public class LibrarianModel
    {
        // class fields
        [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
        public string UId { get; set; }

        [JsonProperty(PropertyName = "librarianName", NullValueHandling = NullValueHandling.Ignore)]
        public string LibrarianName { get; set; }


        [JsonProperty(PropertyName = "librarianMoNo", NullValueHandling = NullValueHandling.Ignore)]
        public int LibrarianMoNo { get; set; }

        [JsonProperty(PropertyName = "librarianEmail", NullValueHandling = NullValueHandling.Ignore)]
        public string LibrarianEmail { get; set; }


        
        [JsonProperty(PropertyName = "librarianPassword", NullValueHandling = NullValueHandling.Ignore)]
        public string LibrarianPassword { get; set; }

    }
}
