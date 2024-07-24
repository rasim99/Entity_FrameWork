using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_FrameWork.Entities
{
    public class Group : BaseEntity
    {
        public string Name { get; set; }
        public int Limit { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
