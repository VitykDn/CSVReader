using CsvHelper;
using CSVReader.Data.Interfaces;
using CSVReader.Data.Mappings;
using CSVReader.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace CSVReader.Data.Repositories
{
    public class CSVRepository : ICSVReader
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

            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading CVS file to database");
                throw;
            }
        }

        public async Task<IEnumerable<Person>> GetAllPersonsAsync() => await _context.Persons.ToListAsync();

        public async Task<Person?> GetById(int? id)
            => id is null ? throw new ArgumentException("Person not exist") : await _context.Persons
            .SingleOrDefaultAsync(p => p.Id == id);

        public async Task<Person?> GetPersonById(int? id)
         => id is null ? throw new ArgumentException("Person not exist") : await _context.Persons
            .SingleOrDefaultAsync(p => p.Id == id);

        public async Task<Person> DeleteAsync(int id)
        {
            try
            {
                if (id == 0)
                    throw new ArgumentNullException(nameof(id), "Id not specified");

                var existingPerson = await GetById(id);
                if (existingPerson == null)
                    throw new ArgumentException("Person does not exist", nameof(id));

                _context.Persons.Remove(existingPerson);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Deleted person with Id: {id}");

                return existingPerson;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting person with Id: {id}");
                throw;
            }
        }

        public async Task<Person> UpdatePerson(Person person)
        {
            try
            {
                var existingPerson = _context.Persons.Find(person.Id);

                if (existingPerson != null)
                {
                    existingPerson.Name = person.Name;
                    existingPerson.IsMarried = person.IsMarried;
                    existingPerson.Phone = person.Phone;
                    existingPerson.DateOfBirth = person.DateOfBirth;
                    existingPerson.Salary = person.Salary;

                    _context.Persons.Update(existingPerson);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation($"Updated person with Id: {person.Id}");

                    return existingPerson;
                }
                else
                    throw new ArgumentException("Person does not exist", nameof(person.Id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating person with Id: {person.Id}");
                throw;
            }
        }
    }
}
