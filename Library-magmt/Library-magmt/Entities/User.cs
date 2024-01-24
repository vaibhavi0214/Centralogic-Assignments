using Newtonsoft.Json;

namespace Library_magmt.Entities
{
    public class User
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
        public string UId { get; set; }

        [JsonProperty(PropertyName = "dType", NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentType { get; set; }


        [JsonProperty(PropertyName = "createdBy", NullValueHandling = NullValueHandling.Ignore)]
        public string CreatedBy { get; set; }

        [JsonProperty(PropertyName = "createdByName", NullValueHandling = NullValueHandling.Ignore)]
        public string CreatedByName { get; set; }

        [JsonProperty(PropertyName = "createdOn", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(PropertyName = "updatedBy", NullValueHandling = NullValueHandling.Ignore)]
        public string UpdatedBy { get; set; }

        [JsonProperty(PropertyName = "updatedByName", NullValueHandling = NullValueHandling.Ignore)]
        public string UpdatedByName { get; set; }

        [JsonProperty(PropertyName = "updatedOn", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime UpdatedOn { get; set; }

        [JsonProperty(PropertyName = "version", NullValueHandling = NullValueHandling.Ignore)]
        public int Version { get; set; }

        [JsonProperty(PropertyName = "active", NullValueHandling = NullValueHandling.Ignore)]
        public bool Active { get; set; }

        [JsonProperty(PropertyName = "archieved", NullValueHandling = NullValueHandling.Ignore)]
        public bool Archieved { get; set; }


    }
    public class Student:User
    {

        

        // Class  Feilds / Properties


        [JsonProperty(PropertyName = "studRollNo", NullValueHandling = NullValueHandling.Ignore)]
        public int StudRollNo { get; set; }

        [JsonProperty(PropertyName = "studPrnNo", NullValueHandling = NullValueHandling.Ignore)]
        public int StudPrnNo { get; set; }

        [JsonProperty(PropertyName = "studName", NullValueHandling = NullValueHandling.Ignore)]
        public string StudName { get; set; }

        [JsonProperty(PropertyName = "studMoNo", NullValueHandling = NullValueHandling.Ignore)]
        public int StudMoNo { get; set; }

        [JsonProperty(PropertyName = "studEmail", NullValueHandling = NullValueHandling.Ignore)]
        public string StudEmail { get; set; }

        [JsonProperty(PropertyName = "studPassword", NullValueHandling = NullValueHandling.Ignore)]
        public string StudPassword { get; set; }

    }

    public class Librarian:User
    {
        // class fields
        [JsonProperty(PropertyName = "librarianName", NullValueHandling = NullValueHandling.Ignore)]
        public string LibrarianName { get; set; }

        [JsonProperty(PropertyName = "librarianMoNo", NullValueHandling = NullValueHandling.Ignore)]
        public int LibrarianMoNo { get; set; }

        [JsonProperty(PropertyName = "librarianEmail", NullValueHandling = NullValueHandling.Ignore)]
        public string LibrarianEmail { get; set; }

        [JsonProperty(PropertyName = "librarianPassword", NullValueHandling = NullValueHandling.Ignore)]
        public string LibrarianPassword { get; set; }


    }

    public class Book : User
    {
        // class fields

        [JsonProperty(PropertyName = "bookName", NullValueHandling = NullValueHandling.Ignore)]
        public string BookName { get; set; }


        [JsonProperty(PropertyName = "bookAuthor", NullValueHandling = NullValueHandling.Ignore)]
        public string BookAuthor { get; set; }

        [JsonProperty(PropertyName = "isAvaliable", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsAvaliable { get; set; }


        [JsonProperty(PropertyName = "bookQuantity", NullValueHandling = NullValueHandling.Ignore)]
        public int BookQuantity { get; set; }

    }

   public class TransactionBook : User
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
