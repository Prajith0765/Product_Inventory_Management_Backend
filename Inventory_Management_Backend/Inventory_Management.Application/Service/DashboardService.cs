using Inventory_Management.Application.DTO;
using Inventory_Management.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management.Application.Service
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _repository;

        public DashboardService(IDashboardRepository repository)
        {
            _repository = repository;
        }

        public IList<DashboardSummaryDTO> GetDashboardSummary()
        {
             return _repository.GetDashboardSummary();
  
        }
    }
}
