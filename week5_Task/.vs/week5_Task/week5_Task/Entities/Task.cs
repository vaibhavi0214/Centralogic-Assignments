﻿using Newtonsoft.Json;

namespace week5Task.Entities
{
    public class Task
    {
        // Mandatory Feilds
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



        // Class  Feilds / Properties


        [JsonProperty(PropertyName = "taskNo", NullValueHandling = NullValueHandling.Ignore)]
        public int TaskNo { get; set; }


        [JsonProperty(PropertyName = "taskname", NullValueHandling = NullValueHandling.Ignore)]
        public string TaskName { get; set; }            


         [JsonProperty(PropertyName = "taskdetails", NullValueHandling = NullValueHandling.Ignore)]
         public string TaskDetails { get; set; }

    }
}
