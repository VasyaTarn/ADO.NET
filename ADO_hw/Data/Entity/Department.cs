using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_hw.Data.Entity
{
    public class Department
    {
        public Guid Id { get; set; }
        public String Name { get; set; } = null!;
        public DateTime? DeleteDt { get; set; }


        public IEnumerable<Manager> MainManagers { get; set; }
        public List<Manager> SecManagers { get; set; }
    }
}
