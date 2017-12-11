using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSyntax
{
    public class Person
    {
        public int Id { get; set; }
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Age { get; set; }

        public int TypeId { get; set; }
        public override string ToString()
        {
            return string.Format("Id:{2},Name:{0}, Age:{1}, TypeId:{3}", this.Name, this.Age, this.Id, this.TypeId);
        }
    }

    public class WorkType
    {
        public int TypeId { get; set; }
        public string TypeName { get; set; }
    }
}
