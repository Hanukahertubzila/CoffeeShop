using CoffeeShop.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.ViewModels
{
    public class ProfileWithOrdersViewModel
    {
        public Profile Profile { get; set; }

        public List<Order> Orders { get; set; }
    }
}
