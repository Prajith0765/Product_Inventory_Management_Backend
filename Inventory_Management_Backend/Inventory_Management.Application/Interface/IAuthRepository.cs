using Inventory_Management.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management.Application.Interface
{
    public interface IAuthRepository
    {
        Task<User?> ValidateUserAsync(string username);
    }
}
