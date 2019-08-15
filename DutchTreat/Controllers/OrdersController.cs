using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]
    //usando o scheme abaixo, estou indicando que nao vou utilizar
    //cookies. vou utilizar apenas JWT (para autorizacao em de webapis)
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : Controller
    {

        private readonly IDutchRepository _repository;
        private readonly ILogger<OrdersController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<StoreUser> _userManager;

        public OrdersController(IDutchRepository repository, ILogger<OrdersController> logger, IMapper mapper, UserManager<StoreUser> userManager)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }  //constructor

        [HttpGet]
        public IActionResult Get(bool includeItems = true)
        {

            try
            {
                var username = User.Identity.Name;
                var results = _repository.GetAllOrdersByUser(username, includeItems);

                return Ok(_mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get orders: {ex.Message}");
                return BadRequest("Failed to get orders");
            }

        } //Get

        [HttpGet("{id:int}")]
        public  IActionResult Get(int id)
        {
            try
            {
                var order = _repository.GetOrderById(User.Identity.Name, id);
                if (order != null)
                {
                    return Ok(_mapper.Map<Order, OrderViewModel>(order));
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get orders: {ex.Message}");
                return BadRequest("Failed to get orders");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody]OrderViewModel model)
        {
            //add it to the db
            try
            {
                if (ModelState.IsValid)
                {

                    var newOrder = _mapper.Map<OrderViewModel, Order>(model);

                    if(newOrder.OrderDate == DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Now;
                    }

                    var currentUser = await _userManager.FindByEmailAsync(User.Identity.Name);

                    newOrder.User = currentUser;

                    _repository.AddEntity(newOrder);

                    if (_repository.SaveAll())
                    {
                        return Created($"/api/orders/{newOrder.Id}", _mapper.Map<Order, OrderViewModel>(newOrder));
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to save the new order: {ex.Message}");
                return BadRequest($"Failed to save the new order: {ex.Message}");
            }
            return Ok();
        }

    }
}
