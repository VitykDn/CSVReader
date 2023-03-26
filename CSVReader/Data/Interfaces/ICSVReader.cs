using CSVReader.Models;

namespace CSVReader.Data.Interfaces
{
    public interface ICSVReader
    {
        void AddEntity(object person);
        IEnumerable<Person> GetAllPersons();

        Person GetPerson(int id);

        bool SaveAll();
    }
}
