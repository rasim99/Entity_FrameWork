using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_FrameWork.Entities
{
    public class Student : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public int GroupId {  get; set; }
        public Group Group { get; set; }
    }
}
