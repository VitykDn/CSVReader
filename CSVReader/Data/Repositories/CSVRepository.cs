using CsvHelper;
using CSVReader.Data.Interfaces;
using CSVReader.Data.Mappings;
using CSVReader.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CSVReader.Data.Repositories
{
    public class CSVRepository: ICSVReader
    {
        private readonly ILogger<CSVRepository> _logger;
        private readonly ApplicationDbContext _context;

        public CSVRepository(ILogger<CSVRepository> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<int> UploadCvsToDb(IFormFile cvsFile)
        {

            var persons = new List<Person>();
            using (var reader = new StreamReader(cvsFile.OpenReadStream()))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<PersonMap>();
                    persons = csv.GetRecords<Person>().ToList();
                }
            }

             await _context.Persons.AddRangeAsync(persons);
             return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Person>> GetAllPersonsAsync() => await _context.Persons.ToListAsync();





    }
}
