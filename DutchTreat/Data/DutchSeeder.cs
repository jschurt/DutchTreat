﻿using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchSeeder
    {
        private readonly DutchContext _ctx;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly UserManager<StoreUser> _userManager;

        public DutchSeeder(DutchContext ctx, IHostingEnvironment hostingEnvironment, UserManager<StoreUser> userManager)
        {
            _ctx = ctx;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            //check if database exists...
            _ctx.Database.EnsureCreated();

            StoreUser user = await _userManager.FindByEmailAsync("julio@email.com");
            if(user == null)
            {
                user = new StoreUser() {
                    FirstName = "Julio",
                    LastName = "Schurt",
                    Email = "julio@email.com",
                    UserName = "julio@email.com"
                };

                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if(result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("COuld not create new user in seeder.");
                }
            }

            if(!_ctx.Products.Any())
            {
                //Need to create sample data
                var filepath = Path.Combine(_hostingEnvironment.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(filepath);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                _ctx.Products.AddRange(products);

                var order = _ctx.Orders
                    .Where(o => o.Id == 1)
                    .FirstOrDefault();

                if(order != null)
                {
                    order.User = user;
                    order.Items = new List<OrderItem>()
                    {
                        new OrderItem() {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    };
                }

                _ctx.SaveChanges();
            }
            
        }

    }
}
