using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory_Management.Domain.Entities;

namespace Inventory_Management.Application.Interface
{
    public interface IJwtService
    {
        public string GenerateToken(User user);
    }
}
