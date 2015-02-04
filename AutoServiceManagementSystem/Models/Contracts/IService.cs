using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServiceManagementSystem.Contracts
{
    public interface IService
    {
        string Name { get; set; }
        decimal Price { get; set; }
    }
}
