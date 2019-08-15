using DutchTreat.Data;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        private readonly INullMailService _nullMailService;
        private readonly IDutchRepository _repository;

        public AppController(INullMailService nullMailService, IDutchRepository repository)
        {
            _nullMailService = nullMailService;
            _repository = repository;
        }

        public IActionResult Index()
        {
            var results = _repository.GetAllProducts();
            return View();
        } //Index

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            ViewBag.Title = "Contact Us";
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            ViewBag.Title = "Contact Us";

            if(ModelState.IsValid)
            {
                //send email
                _nullMailService.SendMessage("teste@teste.com", model.Subject, $"From {model.Name} - {model.Email}, Message: {model.Message}");
                ViewBag.UserMessage = "Mail sent!";
                ModelState.Clear();
            }

            return View();
        }

        public IActionResult About()
        {

            ViewBag.Title = "About Us";

            return View();

        }

        [Authorize]
        public IActionResult Shop()
        {
            //var results = _repository.GetAllProducts();
            //return View(results);

            return View();

        } //Shop

    }
}
