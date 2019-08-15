using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace DutchTreat.Controllers
{
    //Este eh um subcontroller de orders
    [Route("/api/orders/{orderid}/items")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderItemsController : Controller
    {

        private readonly IDutchRepository _repository;
        private readonly ILogger<OrderItemsController> _logger;
        private readonly IMapper _mapper;

        public OrderItemsController(IDutchRepository repository, ILogger<OrderItemsController> logger, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        } //constructor

        [HttpGet]
        ///api/orders/1/items
        public IActionResult Get(int orderId)
        {

            var order = _repository.GetOrderById(User.Identity.Name, orderId);
            if (order != null)
                return Ok(_mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items));

            return NotFound();
        } //Get

        [HttpGet("{id}")]
        ///api/orders/1/items/1234
        public IActionResult Get(int orderId, int id)
        {

            var order = _repository.GetOrderById(User.Identity.Name, orderId);
            if (order != null)
            {
                var item = order.Items.Where(i => i.Id == id).FirstOrDefault();
                if(item != null)
                    return Ok(_mapper.Map<OrderItem, OrderItemViewModel>(item));
            }
            return NotFound();
        } //Get



    } //class
} //namespace
