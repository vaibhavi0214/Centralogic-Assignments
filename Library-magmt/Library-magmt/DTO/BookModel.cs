using Newtonsoft.Json;

namespace Library_magmt.DTO
{
    public class BookModel
    {
        [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
        public string? UId { get; set; }

        [JsonProperty(PropertyName = "bookName", NullValueHandling = NullValueHandling.Ignore)]
        public string? BookName { get; set; }

        [JsonProperty(PropertyName = "bookAuthor", NullValueHandling = NullValueHandling.Ignore)]
        public string? BookAuthor { get; set; }

        [JsonProperty(PropertyName = "isAvaliable", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsAvaliable { get; set; }

        [JsonProperty(PropertyName = "bookQuantity", NullValueHandling = NullValueHandling.Ignore)]
        public int BookQuantity { get; set; }




    }
}
