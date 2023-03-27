using CSVReader.Models;
using Microsoft.EntityFrameworkCore;

namespace CSVReader.Data.Interfaces
{
    public interface ICSVReader
    {
        Task<int> UploadCvsToDb(IFormFile cvsFile);
        public Task<IEnumerable<Person>> GetAllPersonsAsync();

        public  Task<Person?> GetPersonById(int? id);
        public Task<Person> UpdatePerson(Person person);
        public Task<Person> DeleteAsync(int id);
    }
}
