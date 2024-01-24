using AutoMapper;
using Library_magmt.DTO;
using Library_magmt.Entities;

namespace Library_magmt.Common
{
    public class AutomapperProfile:Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Book,BookModel>().ReverseMap();
            CreateMap<TransactionBook, TransactionModel>().ReverseMap();
            CreateMap<Librarian,LibrarianModel>().ReverseMap();
            CreateMap<Student,StudentModel>().ReverseMap();
        }
    }
}
