using CSVReader.Data.Interfaces;
using CSVReader.Models;

namespace CSVReader.Data.Repositories
{
    public class CSVRepository: ICSVReader
    {
        public void AddEntity(object person)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Person> GetAllPersons()
        {
            throw new NotImplementedException();
        }

        public Person GetPerson(int id)
        {
            throw new NotImplementedException();
        }

        public bool SaveAll()
        {
            throw new NotImplementedException();
        }

    }
}
