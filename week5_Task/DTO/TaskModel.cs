using Newtonsoft.Json;

namespace week5_Task.DTO
{
    public class TaskModel
    {  
        [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
        public string UId { get; set; }

        [JsonProperty(PropertyName = "taskNo", NullValueHandling = NullValueHandling.Ignore)]
        public int TaskNo { get; set; }


        [JsonProperty(PropertyName = "taskname", NullValueHandling = NullValueHandling.Ignore)]
        public string TaskName { get; set; }

        [JsonProperty(PropertyName = "taskdetails", NullValueHandling = NullValueHandling.Ignore)]
        public string TaskDetails { get; set; }
    }
}
