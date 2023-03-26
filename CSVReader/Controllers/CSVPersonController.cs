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

namespace CSVReader.Controllers
{
    [Route("People")]
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

        // POST: CSVPerson/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.



    }
}
