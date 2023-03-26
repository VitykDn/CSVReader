using CsvHelper.Configuration;
using CSVReader.Models;

namespace CSVReader.Data.Mappings
{
    public sealed class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Map(m => m.Name).Index(0);
            Map(m => m.DateOfBirth).Index(1).TypeConverterOption.Format("dd/MM/yyyy");
            Map(m => m.IsMarried).Index(2);
            Map(m => m.Phone).Index(3);
            Map(m => m.Salary).Index(4);
        }
    }
}
