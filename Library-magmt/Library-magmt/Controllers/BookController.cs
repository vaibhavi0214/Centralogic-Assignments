using AutoMapper;
using Azure;
using Library_magmt.DTO;
using Library_magmt.Entities;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.Data;

namespace Library_magmt.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public Container container;
        public IMapper _mapper;
        public BookController(IMapper mapper)
        {
            container = GetContainer();
            _mapper = mapper;
        }
        //AddBook
        [HttpPost]
        public async Task<IActionResult> AddBook(BookModel bookmodel)
        {
            try { 
            Book book = new Book();
            book.BookAuthor = bookmodel.BookAuthor;
            book.BookName = bookmodel.BookName;
            book.BookQuantity = bookmodel.BookQuantity; 
            book.IsAvaliable = bookmodel.IsAvaliable;

               

            book.Id = Guid.NewGuid().ToString();
            book.UId = book.Id;
            book.DocumentType = "book";
            book.CreatedBy = "mayur UId "; 
            book.CreatedByName = "mayur";
            book.CreatedOn = DateTime.Now;
            book.UpdatedBy = "";
            book.UpdatedByName = "";
            book.UpdatedOn = DateTime.Now;
            book.Version = 1;
            book.Active = true;
            book.Archieved = false;

            //stp 3 Add data to database
            Book response = await container.CreateItemAsync(book);

            
                var model = _mapper.Map<BookModel>(response);


                return Ok(model);
            }
            catch (Exception ex)
            {

                return BadRequest("Data Adding Failed" + ex);
            }
        }
                //SearchBook
                [HttpGet]
                public async Task<IActionResult> SearchBook(string Search)
                {
            try { 
                    //stp 1 get all student

                    var book = container.GetItemLinqQueryable<Book>(true).Where(s => s.BookName == Search || s.BookAuthor == Search|| s.DocumentType == "book" && s.Archieved == false && s.Active == true).AsEnumerable().FirstOrDefault();
                        if(book == null)
                {
                    return NotFound("Book not found");
                }
                    //stp 2 mapping all data
                    BookModel model = new BookModel();
                      model.UId = book.UId;
               
                    model.BookName = book.BookName;
                    model.BookAuthor = book.BookAuthor;
                     model.IsAvaliable=book.IsAvaliable;
                    model.BookQuantity=book.BookQuantity;

                    return Ok(model);
            }
            catch (Exception ex)
            {

                return BadRequest("Data Adding Failed" + ex);
            }



        } 
                   //UpdateBook
                [HttpPut]
                public async Task<IActionResult> UpdateBook(BookModel bookModel)
                {
            try {
                
                
                    var existingbook = container.GetItemLinqQueryable<Book>(true).Where(q => q.UId == bookModel.UId && q.DocumentType == "book" && q.Archieved == false && q.Active == true).AsEnumerable().FirstOrDefault();
                if (existingbook == null)
                {
                    return (NotFound("pz enter uId"));
                }
                    
                    existingbook.Archieved = true;
                    await container.ReplaceItemAsync(existingbook, existingbook.Id);


                    existingbook.Id = Guid.NewGuid().ToString();
                    existingbook.UpdatedBy = "Mayur UId";
                    existingbook.UpdatedByName = "Mayur";
                    existingbook.UpdatedOn = DateTime.Now;
                existingbook.Version = existingbook.Version + 1;
               // existingbook.Version = 1;
                existingbook.Active = true;
                    existingbook.Archieved = false;

                   
                    existingbook.BookName = bookModel.BookName;
                    existingbook.BookAuthor = bookModel.BookAuthor;
                    existingbook.IsAvaliable = bookModel.IsAvaliable;
                    existingbook.BookQuantity = bookModel.BookQuantity; 



                    existingbook = await container.CreateItemAsync(existingbook);

                
                var model = _mapper.Map<BookModel>(existingbook);



                return Ok(model);
            }
            catch (Exception ex)
            {

                return BadRequest("Data Adding Failed" + ex);
            }

        }

        //DeleteBook
        [HttpDelete]
        public async Task<IActionResult> DeleteBook(string BookUId)
        {
            try { 
            var book = container.GetItemLinqQueryable<Book>(true).Where(q => q.UId == BookUId && q.DocumentType == "book" && q.Archieved == false && q.Active == true).AsEnumerable().FirstOrDefault();
            if (book == null)
                {
                    return NotFound("please enter correct BookUID");
                }
            book.Active = false;
                book.IsAvaliable = false;
            await container.ReplaceItemAsync(book, book.Id);


            return Ok("Book deleted Successfully");
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



/*
 feature :

1-  user : [ I can be able to login and singup ] 
      steps : 1 students must able to singup ( add student )
      steps : 2 Stundets must be able to log in ( request : username and pass .... ) return (uid):

need : student (dtype)   - CRUD
       librarian      - CRUD  

Deadline : Tomarrow (3 Jan ) 


2 - Book  
Features : 

1- Add Book , Delete , Update , Issue book , return book , Request (xyz) 
2 -  search by book-name , author , subject (get)


librarian : 
show books in library 
show borrowed books 
show total books 

Need : Book CRUD

 */
