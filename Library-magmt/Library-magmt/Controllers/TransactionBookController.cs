using AutoMapper;
using Library_magmt.DTO;
using Library_magmt.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.Reflection;

namespace Library_magmt.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TransactionBookController : ControllerBase
    {
        
        public Container container;
        public IMapper _mapper;
        public TransactionBookController(IMapper mapper)
        {
            container = GetContainer();

            _mapper = mapper;
        }



        //IssueBook
        [HttpGet]
        public async Task<IActionResult> IssueBook(string studentname,string bookname)
        {
            try { 
            var student = container.GetItemLinqQueryable<Student>(true).Where(s => s.StudName == studentname&&s.Active==true && s.Archieved== false).AsEnumerable().FirstOrDefault();

            var book = container.GetItemLinqQueryable<Book>(true).Where(b => b.BookName == bookname && b.IsAvaliable==true && b.Active==true && b.Archieved==false).AsEnumerable().FirstOrDefault();
    
            if (student == null || book == null)
            {
                return NotFound("Student or Book not found.");
            }
            if (book.BookQuantity < 1)
            {
                return NotFound("Book is Borrowed by other student or book is not avaliable");
            }
            else { 
            //stp 1 convert TransactionBookModel to transactionbookentity
            TransactionBook tbook = new TransactionBook();
            tbook.StudName = studentname;  //manual mapping
            tbook.BookName = bookname;
            tbook.IsBookIssue = true;
            tbook.IssueDate = DateTime.Now;
            tbook.Isreturn = false;
            tbook.ReturnDate = DateTime.Now.AddDays(7);

            //stp 2 Assign madetory fields
            tbook.Id = Guid.NewGuid().ToString();
            tbook.UId = tbook.Id;
            tbook.DocumentType = "transaction";
            tbook.CreatedBy = "mayur UId ";
            tbook.CreatedByName = "mayur";
            tbook.CreatedOn = DateTime.Now;
            tbook.UpdatedBy = "";
            tbook.UpdatedByName = "";
            tbook.UpdatedOn = DateTime.Now;
            tbook.Version = 1;
            tbook.Active = true;
            tbook.Archieved = false;

            book.BookQuantity = book.BookQuantity - 1;
            //stp 3 Add data to database
             await container.CreateItemAsync(tbook);

         
            return Ok( $" Book issued successfully to {tbook.StudName}");
        }
            }
            catch (Exception ex)
            {

                return BadRequest("Data Adding Failed" + ex);
            }
        }
        //returnBooks
        [HttpGet]
        public async Task<IActionResult> ReturnBook(string studentname, string bookname)
        {
            try { 
          
            var transaction= container.GetItemLinqQueryable<TransactionBook>(true).Where(b => b.BookName == bookname && b.StudName == studentname && b.IsBookIssue == true).AsEnumerable().FirstOrDefault();

                if (transaction == null )
            {
                return NotFound("book is not  issued to this student");
            }
            else
            {
                
                //stp 1 convert bokModel to bookentity
                TransactionBook tbook = new TransactionBook();
                tbook.StudName = studentname;  
                tbook.BookName = bookname;
                tbook.IsBookIssue = false;

                tbook.Isreturn = true;
                tbook.ReturnDate = DateTime.Now;

                //stp 2 Assign madetory fields
                tbook.Id = Guid.NewGuid().ToString();
                tbook.UId = tbook.Id;
                tbook.DocumentType = "transaction";
                tbook.CreatedBy = "mayur UId ";
                tbook.CreatedByName = "mayur";
                tbook.CreatedOn = DateTime.Now;
                tbook.UpdatedBy = "";
                tbook.UpdatedByName = "";
                tbook.UpdatedOn = DateTime.Now;
                tbook.Version = 1;
                tbook.Active = true;               
                tbook.Archieved = false;

                //Book book = new Book();
                //book.UId=tbook.UId;
               // book.BookQuantity = book.BookQuantity + 1;
               //book.version=book.version+1;
                //stp 3 Add data to database
                await container.CreateItemAsync(tbook);

              
                return Ok($"Book returned successfully by {tbook.StudName}");
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
            string? DatabaseName = Environment.GetEnvironmentVariable("database-name");
            string? ContainerName = Environment.GetEnvironmentVariable("container-name");
            CosmosClient cosmosClient = new CosmosClient(URI, PrimaryKey);
            Database db = cosmosClient.GetDatabase(DatabaseName);
            Container container = db.GetContainer(ContainerName);
            return container;
        }
    }
}
