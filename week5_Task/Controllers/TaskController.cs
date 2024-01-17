using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using week5_Task.DTO;
using week5_Task.Entities;
using Task = week5_Task.Entities.Task;


namespace week5_Task.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        public string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "TaskDB";
        public string ContainerName = "TaskContainer";


        public Container container;


        public TaskController()
        {
            container = GetContainer();
        }

        [HttpPost]
        public async Task<IActionResult> AddTask(TaskModel taskmodel) {
                //conversion taskmodel 
                Task task = new Task();
                    //Manual map
                   task.TaskNo = taskmodel.TaskNo;  
            task.TaskName = taskmodel.TaskName;
            task.TaskDetails = taskmodel.TaskDetails;
            //Assigned neccessary fields
            task.Id = Guid.NewGuid().ToString();
            task.UId = task.Id;
            task.DocumentType = "task";
            task.CreatedBy = "Vaibhavi UId "; // UID
            task.CreatedByName = "Vaibhavi";
            task.CreatedOn = DateTime.Now;
            task.UpdatedBy = "Vaibhavi";
            task.UpdatedByName = "Vaibhavi";
            task.UpdatedOn = DateTime.Now;
            task.Version = 1;
            task.Active= true;
            task.Archieved = false;

            //Adding data to database
           Task response =await container.CreateItemAsync(task);

            //Reverse mapping

            TaskModel model = new TaskModel();
            model.UId = response.UId;
            model.TaskName = response.TaskName;
            model.TaskDetails = response.TaskDetails;

        return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetAllTask()
        {
           
            var tasklist = container.GetItemLinqQueryable<Task>(true).Where(q => q.DocumentType == "task" && q.Archieved == false && q.Active == true).AsEnumerable().ToList();
               
            //Mapping data
            List<TaskModel> taskModelist = new List<TaskModel>();
            foreach (var task in tasklist)
            {
                TaskModel model = new TaskModel();
                model.UId = task.UId;
                model.TaskNo= task.TaskNo;
                model.TaskName = task.TaskName;
                model.TaskDetails = task.TaskDetails;
                taskModelist.Add(model);
            }
            return Ok(taskModelist);

        }

        [HttpPost]
        public async Task<IActionResult> GetTaskByUId(string taskUId)
        {
            //Get all student
            var task = container.GetItemLinqQueryable<Task>(true).Where(q =>  q.UId==taskUId && q.DocumentType == "task" && q.Archieved==false && q.Active==true).AsEnumerable().FirstOrDefault();
            
            //Mapping data
           
                TaskModel model = new TaskModel();
                model.UId = task.UId;
            model.TaskNo = task.TaskNo;
            model.TaskName = task.TaskName;
                model.TaskDetails = task.TaskDetails;
              
  
            return Ok(model);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateTask(TaskModel taskModel)
        {
           

            var existingtaskdata = container.GetItemLinqQueryable<Task>(true).Where(q => q.UId == taskModel.UId && q.DocumentType == "task" && q.Archieved == false && q.Active == true).AsEnumerable().FirstOrDefault();
           
            existingtaskdata.Archieved = true;
            await container.ReplaceItemAsync(existingtaskdata,existingtaskdata.Id);


            existingtaskdata.Id=Guid.NewGuid().ToString();
            existingtaskdata.UpdatedBy = "Vaibhavi UId";
            existingtaskdata.UpdatedByName = "Vaibhavi";
            existingtaskdata.UpdatedOn = DateTime.Now;
            existingtaskdata.Version = existingtaskdata.Version + 1;
            existingtaskdata.Active = true;
            existingtaskdata.Archieved = false;

            existingtaskdata.TaskNo = taskModel.TaskNo;
            existingtaskdata.TaskName = taskModel.TaskName;
            existingtaskdata.TaskDetails = taskModel.TaskDetails;




            existingtaskdata= await container.CreateItemAsync(existingtaskdata);

            TaskModel model = new TaskModel();
            model.UId = existingtaskdata.UId;
            model.TaskName = existingtaskdata.TaskName;
            model.TaskDetails = existingtaskdata.TaskDetails;


           


            return Ok(model);

        }

        [HttpPost]
        public async Task<IActionResult> DeleteTask(string taskUId)
        {
            var task = container.GetItemLinqQueryable<Task>(true).Where(q => q.UId == taskUId && q.DocumentType == "task" && q.Archieved == false && q.Active == true).AsEnumerable().FirstOrDefault();

            task.Active= false;
            await container.ReplaceItemAsync(task, task.Id);


            return Ok(true);
        
        }

            private Container GetContainer()
        {
            CosmosClient cosmosClient = new CosmosClient(URI,PrimaryKey); 
            Database db = cosmosClient.GetDatabase(DatabaseName);
            Container container = db.GetContainer(ContainerName);
            return container;
        }
    }
}
