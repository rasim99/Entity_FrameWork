using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_FrameWork.Entities
{
    public class Teacher : BaseEntity
    {
        public string Name { get; set; }
        public string Surame { get; set; }

        public ICollection<Group> Groups { get; set; }
    }
}
