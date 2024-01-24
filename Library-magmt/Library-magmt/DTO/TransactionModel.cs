using Newtonsoft.Json;

namespace Library_magmt.DTO
{
    public class TransactionModel
    {
        [JsonProperty(PropertyName = "bookName", NullValueHandling = NullValueHandling.Ignore)]
        public string? BookName { get; set; }

        [JsonProperty(PropertyName = "studName", NullValueHandling = NullValueHandling.Ignore)]
        public string? StudName { get; set; }

        [JsonProperty(PropertyName = "isBookIssue", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsBookIssue { get; set; }


        [JsonProperty(PropertyName = "issueDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime IssueDate { get; set; }

        [JsonProperty(PropertyName = "isreturn", NullValueHandling = NullValueHandling.Ignore)]
        public bool Isreturn { get; set; }

        [JsonProperty(PropertyName = "returndate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime ReturnDate { get; set; }



    }
}
