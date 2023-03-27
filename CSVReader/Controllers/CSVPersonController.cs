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
using Newtonsoft.Json.Linq;

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
            try
            {
                var people = await _repository.GetAllPersonsAsync();
                return View(people);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the Index method.");
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadCvs(IFormFile file)
        {
            try
            {
                await _repository.UploadCvsToDb(file);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while uploading CSV file.");

                TempData["ErrorMessage"] = "An error occurred while uploading the CSV file.";

                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update([Bind("Id,Name,IsMarried,Phone,DateOfBirth,Salary")] Person person)
        {
            ModelState["IsMarried"].Errors.Clear();
            if (ModelState.IsValid)
            {
                try
                {
                    var updatedPerson = await _repository.UpdatePerson(person);
                    return Ok(updatedPerson);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred in the Update method.");
                    return BadRequest("Error while updating data");
                }

                return Ok();
            }
            else
            {
                _logger.LogError("Model State is not valid");
                return BadRequest("Model State is not valid");
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the person with ID {0}.", id);
                return RedirectToAction(nameof(Index));
            }
        }





    }
}
