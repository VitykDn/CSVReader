using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CSVReader.Data;
using CSVReader.Models;
using CSVReader.Data.Repositories;
using CSVReader.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace CSVReader.Controllers
{
    public class CSVPersonController : Controller
    {
        private readonly ICSVReader _repository;
        private readonly ILogger<CSVPersonController> _logger;

        public CSVPersonController(ICSVReader repository, ILogger<CSVPersonController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        // GET: CSVPerson
        public async Task<IActionResult> Index()
        {
            var people = await _repository.GetAllPersonsAsync();
            return View(people);
        }

        [HttpPost]
        public async Task<IActionResult> UploadCvs(IFormFile file)
        {
            await _repository.UploadCvsToDb(file);
            return RedirectToAction("Index");
        }
        //[HttpPost]
        //public async Task<IActionResult> Update(int id, string Name, bool IsMarried, string Phone, DateTime DateOfBirth, decimal Salary)
        //{
        //    Person person = await _repository.GetPersonById(id);


        //    person.Name = Name;
        //    person.IsMarried = IsMarried;
        //    person.Phone = Phone;
        //    person.DateOfBirth = DateOfBirth;
        //    person.Salary = Salary;

        //    _repository.UpdatePerson(person);

        //    return Ok(person);
        //}

        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }





    }
}
