using CSVReader.Models;

namespace CSVReader.Data.Interfaces
{
    public interface ICSVReader
    {
        Task<int> UploadCvsToDb(IFormFile cvsFile);
        public Task<IEnumerable<Person>> GetAllPersonsAsync();



    }
}
