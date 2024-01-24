using AutoMapper;
using Azure;
using Library_magmt.DTO;
using Library_magmt.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.ComponentModel;

using Container = Microsoft.Azure.Cosmos.Container;

namespace Library_magmt.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase


    {
        public Container container;
        public IMapper _mapper;
        public StudentController(IMapper mapper)
        {
            container = GetContainer();
            _mapper = mapper;
        }

        //signUpStudent
        [HttpPost]
        public async Task<IActionResult> SignUpStudent(StudentModel studentmodel)
        {
            try
            {

                //stp 1 convert studentModel to studententity
                Student student = new Student();
            student.StudRollNo = studentmodel.StudRollNo;  //manual mapping
            student.StudPrnNo = studentmodel.StudPrnNo;
            student.StudName = studentmodel.StudName.ToLower(); ;
            student.StudMoNo = studentmodel.StudMoNo;
            student.StudEmail = studentmodel.StudEmail.ToLower();
           
            student.StudPassword = studentmodel.StudPassword;
            //stp 2 Assign madetory fields
            student.Id = Guid.NewGuid().ToString();
            student.UId = student.Id;
            student.DocumentType = "student";
            student.CreatedBy = "Mayur-Fegade UId "; // UID for who created thks data
            student.CreatedByName = "Mayur-Fegade";
            student.CreatedOn = DateTime.Now;
            student.UpdatedBy = "Mayur-Fegade";
            student.UpdatedByName = "Mayur-Fegade";
            student.UpdatedOn = DateTime.Now;
            student.Version = 1;
            student.Active = true;
            student.Archieved = false;

            //stp 3 Add data to database
            Student response = await container.CreateItemAsync(student);

            //stp4 return model to ui(reverse mapping )(DRY)

           /* StudentModel model = new StudentModel();
                model.UId = student.UId;
            model.StudRollNo = response.StudRollNo;
            model.StudPrnNo = response.StudPrnNo;
            model.StudName = response.StudName;
            model.StudMoNo = response.StudMoNo;
            model.StudEmail = response.StudEmail;
            //model.StudUserName = response.StudUserName;
            model.StudPassword = response.StudPassword;
*/
                //auto mapping
                var model = _mapper.Map<StudentModel>(response);

                return Ok(model);
            }
            catch (Exception ex)
            {

                return BadRequest("Data Adding Failed" + ex);
            }
        }

        //GetAllStudent
        [HttpGet]
        public async Task<IActionResult> GetAllStudent()
        {
            try
            {
                //stp 1 get all student
                var studentlist = container.GetItemLinqQueryable<Student>(true).Where(q => q.DocumentType == "student" && q.Archieved == false && q.Active == true).AsEnumerable().ToList();

            //stp 2 mapping all data
            List<StudentModel> studentModelist = new List<StudentModel>();
            foreach (var student in studentlist)
            {
              /*  StudentModel model = new StudentModel();
                model.UId = student.UId;
                model.StudRollNo = student.StudRollNo;
                model.StudName = student.StudName;
                model.StudMoNo = student.StudMoNo;
                model.StudEmail = student.StudEmail;

                model.StudPrnNo = student.StudPrnNo;
              */      //auto mapping
                    var model = _mapper.Map<StudentModel>(student);

                    studentModelist.Add(model);
            }
            return Ok(studentModelist);
            }
            catch (Exception ex)
            {

                return BadRequest("Data Adding Failed" + ex);
            }

        }

        //LoginStudent
        [HttpGet]
        public async Task<IActionResult> LoginStudent(string studEmail,string studPassword)
        {
                try
                {
                   
                    
                    var student = container.GetItemLinqQueryable<Student>(true).Where(s => s.StudEmail == studEmail.ToLower() &&s.StudPassword==studPassword && s.DocumentType == "student" && s.Archieved == false && s.Active == true).AsEnumerable().FirstOrDefault();
                    if (student == null)
                {
                    return NotFound("wrong email or password" );
                }
           
                            var response = new { student.StudName };


            return Ok($"student Login Successfully{response}");


        }
            catch (Exception ex)
            {

                return BadRequest("Data Adding Failed" + ex);
    }

}

        //updateStudent
        [HttpPut]
        public async Task<IActionResult> UpdateStudent(StudentModel studentModel)
        {
                try
                {
                    // var exsudent = container.GetItemLinqQueryable<Task>(true).Where(q => q.UId == taskModel.UId && q.DocumentType == "student" && q.Archieved == false && q.Active == true).AsEnumerable().FirstOrDefault();

                    var existingstudent = container.GetItemLinqQueryable<Student>(true).Where(q => q.UId == studentModel.UId && q.DocumentType == "student" && q.Archieved == false && q.Active == true).AsEnumerable().FirstOrDefault();


                if (existingstudent == null)
                {
                    return NotFound("plz check uid ");
                }
                else { 
                   
                    existingstudent.Archieved = true;

                    await container.ReplaceItemAsync(existingstudent, existingstudent.Id);


                    existingstudent.Id = Guid.NewGuid().ToString();
                    existingstudent.UpdatedBy = "Mayur UId";
                    existingstudent.UpdatedByName = "Mayur";
                    existingstudent.UpdatedOn = DateTime.Now;
                     existingstudent.Version = existingstudent.Version + 1;
                    //existingstudent.Version = 1;
                    existingstudent.Active = true;
                    existingstudent.Archieved = false;

                    existingstudent.StudRollNo = studentModel.StudRollNo;
                    existingstudent.StudEmail = studentModel.StudEmail;
                    existingstudent.StudName = studentModel.StudName;
                    existingstudent.StudPrnNo = studentModel.StudPrnNo;
                    existingstudent.StudMoNo = studentModel.StudMoNo;
                    existingstudent.StudPassword = studentModel.StudPassword;




                    existingstudent = await container.CreateItemAsync(existingstudent);

                   /* StudentModel model = new StudentModel();
                    model.UId = existingstudent.UId;
                    model.StudRollNo = existingstudent.StudRollNo;
                    model.StudEmail = existingstudent.StudEmail;
                    model.StudName = existingstudent.StudName;
                    model.StudPrnNo = existingstudent.StudPrnNo;
                    model.StudMoNo = existingstudent.StudMoNo;
                    model.StudPassword = existingstudent.StudPassword;
*/
                    //auto mapping
                    var model = _mapper.Map<StudentModel>(existingstudent);

                    return Ok(model);
                }
        }
            catch (Exception ex)
            {

                     return BadRequest("Data Adding Failed" + ex);
            }

          }

        //deleteStudent
        [HttpDelete]
        public async Task<IActionResult> DeleteStudent(string StudentUId)
        {
            try
            {
                var student = container.GetItemLinqQueryable<Student>(true).Where(q => q.UId == StudentUId && q.DocumentType == "student" && q.Archieved == false && q.Active == true).AsEnumerable().FirstOrDefault();
                if (student == null)
                {
                    return NotFound("Student not found");
                }
                else
                {
                    student.Active = false;
                    await container.ReplaceItemAsync(student, student.Id);


                    return Ok($"student record Delete successfully");
                }
            }
            catch (Exception ex)
            {

                return BadRequest("Data Adding Failed" + ex);
            }

        }

        private Container GetContainer()
        {
            string? URI = Environment.GetEnvironmentVariable("cosmos-uri");
            string? PrimaryKey = Environment.GetEnvironmentVariable("auth-token");
            string? DatabaseName= Environment.GetEnvironmentVariable("database-name");
            string? ContainerName= Environment.GetEnvironmentVariable("container-name");
            CosmosClient cosmosClient = new CosmosClient(URI, PrimaryKey);
            Database db = cosmosClient.GetDatabase(DatabaseName);
            Container container = db.GetContainer(ContainerName);
            return container;
        }



    }
}
