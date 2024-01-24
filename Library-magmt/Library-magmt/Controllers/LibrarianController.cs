using AutoMapper;
using Library_magmt.DTO;
using Library_magmt.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;


namespace Library_magmt.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LibrarianController : ControllerBase
    {


        public Container container;
        public IMapper _mapper;
        public LibrarianController(IMapper mapper)
        {
            container = GetContainer();
            _mapper = mapper;
        }

        //signuplibrarian
        [HttpPost]
        public async Task<IActionResult> SignUpLibrarian(LibrarianModel librarianmodel)
        {
            try
            {
                //stp 1 convert librarianModel to librarianentity
                Librarian librarian = new Librarian();
                librarian.LibrarianName = librarianmodel.LibrarianName;  //manual mapping
                librarian.LibrarianMoNo = librarianmodel.LibrarianMoNo;
                librarian.LibrarianEmail = librarianmodel.LibrarianEmail.ToLower();
                librarian.LibrarianPassword = librarianmodel.LibrarianPassword;
                //stp 2 Assign madetory fields
                librarian.Id = Guid.NewGuid().ToString();
                librarian.UId = librarian.Id;
                librarian.DocumentType = "librarian";
                librarian.CreatedBy = "Mayur-Fegade UId "; // UID for who created thks data
                librarian.CreatedByName = "Mayur-Fegade";
                librarian.CreatedOn = DateTime.Now;
                librarian.UpdatedBy = "Mayur-Fegade";
                librarian.UpdatedByName = "Mayur-Fegade";
                librarian.UpdatedOn = DateTime.Now;
                librarian.Version = 1;
                librarian.Active = true;
                librarian.Archieved = false;

                //stp 3 Add data to database
                Librarian response = await container.CreateItemAsync(librarian);

                //stp4 return model to ui(reverse mapping )(DRY)

               /* LibrarianModel model = new LibrarianModel();
                model.UId= librarian.UId;
                model.LibrarianName = response.LibrarianName;
                model.LibrarianMoNo = response.LibrarianMoNo;
                model.LibrarianEmail = response.LibrarianEmail;
                model.LibrarianPassword = response.LibrarianPassword;

*/

                //auto mapping
                var model = _mapper.Map<LibrarianModel>(response);

                return Ok(model);
            }
            catch (Exception ex)
            {

                return BadRequest("Data Adding Failed" + ex);
            }

        }


        //loginlibrarian
        [HttpGet]
        public async Task<IActionResult> LoginLibrarian(string librarianEmail, string librarianPassword)
        {

            try
            {


                var librarian = container.GetItemLinqQueryable<Librarian>(true).Where(s => s.LibrarianEmail == librarianEmail.ToLower() && s.LibrarianPassword == librarianPassword && s.DocumentType == "librarian" && s.Archieved == false && s.Active == true).AsEnumerable().FirstOrDefault();
                if (librarian == null) {
                    return NotFound("emailid or password wrong");
                }

                var response = new { librarian.UId };


                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest("Data Adding Failed" + ex);
            }



        }

        //updatelibrarian
        [HttpPut]
        public async Task<IActionResult> UpdateLibrarian(LibrarianModel librarianModel)
        {
            try
            {
              
                var existinglibrarian = container.GetItemLinqQueryable<Librarian>(true).Where(q => q.UId == librarianModel.UId && q.DocumentType == "librarian" && q.Archieved == false && q.Active == true).AsEnumerable().FirstOrDefault();
                if (existinglibrarian == null)
                {
                    return NotFound("Enter all values");
                }
                else
                {
                    existinglibrarian.Archieved = true;
                    await container.ReplaceItemAsync(existinglibrarian, existinglibrarian.Id);


                    existinglibrarian.Id = Guid.NewGuid().ToString();
                    existinglibrarian.UpdatedBy = "Mayur UId";
                    existinglibrarian.UpdatedByName = "Mayur";
                    existinglibrarian.UpdatedOn = DateTime.Now;
                    existinglibrarian.Version = existinglibrarian.Version + 1;
                    existinglibrarian.Active = true;
                    existinglibrarian.Archieved = false;

                    existinglibrarian.LibrarianName = librarianModel.LibrarianName;
                    existinglibrarian.LibrarianMoNo = librarianModel.LibrarianMoNo;
                    existinglibrarian.LibrarianEmail = librarianModel.LibrarianEmail.ToLower();
                    existinglibrarian.LibrarianPassword = librarianModel.LibrarianPassword;




                    existinglibrarian = await container.CreateItemAsync(existinglibrarian);

                   /* LibrarianModel model = new LibrarianModel();
                    model.UId = existinglibrarian.UId;
                    model.LibrarianName = existinglibrarian.LibrarianName;
                    model.LibrarianMoNo = existinglibrarian.LibrarianMoNo;
                    model.LibrarianEmail = existinglibrarian.LibrarianEmail;
                    model.LibrarianPassword = existinglibrarian.LibrarianPassword;
*/                  
                   //auto mapping
                    var model = _mapper.Map<LibrarianModel>(existinglibrarian);



                    return Ok(model);
                }
            }
            catch (Exception ex)
            {

                return BadRequest("Data Adding Failed" + ex);
            }

        }


        //deleteLibrarian
        [HttpDelete]
        public async Task<IActionResult> DeleteLibrarian(string LibrarianUId)
        {
            try
            {
                var librarian = container.GetItemLinqQueryable<Librarian>(true).Where(q => q.UId == LibrarianUId && q.DocumentType == "librarian" && q.Archieved == false && q.Active == true).AsEnumerable().FirstOrDefault();
                if (librarian == null)
                {
                    return NotFound("librarian not  found by uid");
                }
                else
                {
                    librarian.Active = false;
                    await container.ReplaceItemAsync(librarian, librarian.Id);


                    return Ok("librarian delte sucessfully");
                }
            }
            catch (Exception ex)
            {

                return BadRequest("Data Adding Failed" + ex);
            }
        }

        //showallBooks
        [HttpGet]
        public async Task<IActionResult> ShowAllBooks()
        {
            try { 
            //stp 1 get all Books
            var Booklist = container.GetItemLinqQueryable<Book>(true).Where(q => q.DocumentType == "book" && q.Archieved == false && q.Active == true).AsEnumerable().ToList();
                if (Booklist==null) {
                    return NotFound("there is no books");
                }
            //stp 2 mapping all data
            List<BookModel> bookModelist = new List<BookModel>();
            foreach (var book in Booklist)
            {
               /* BookModel model = new BookModel();
               model.UId = book.UId;
                model.BookAuthor = book.BookAuthor;
                model.BookName = book.BookName;
                model.IsAvaliable = book.IsAvaliable;
                    model.BookQuantity = book.BookQuantity;
                 */   //  Auto Mapping
                    var model = _mapper.Map<BookModel>(book);
                    bookModelist.Add(model);
            }
            return Ok(bookModelist);
            }
            catch (Exception ex)
            {

                return BadRequest("Data Adding Failed" + ex);
            }

        }
        // Show Borrow Books
        [HttpGet]
        public async Task<IActionResult> ShowBorrowedBooks()
        {
            try { 
            //stp 1 get all issue books
            var Booklist = container.GetItemLinqQueryable<TransactionBook>(true).Where(q => q.DocumentType == "transaction"  && q.Archieved == false && q.Active == true && q.IsBookIssue == true).AsEnumerable().ToList();

                if (Booklist == null)
                {
                    return NotFound("there is no issue books");
                }
            //stp 2 mapping all data
            List<TransactionModel> bookModelist = new List<TransactionModel>();
            foreach (var book in Booklist)
            {
              /*  TransactionModel model = new TransactionModel();
                model.BookName = book.BookName;
                model.StudName = book.StudName;
                model.IsBookIssue = book.IsBookIssue;
                model.IssueDate = book.IssueDate;
                model.Isreturn = book.Isreturn;
*/
                    var model = _mapper.Map<TransactionModel>(book);

                    bookModelist.Add(model);
            }
            return Ok(bookModelist);
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
