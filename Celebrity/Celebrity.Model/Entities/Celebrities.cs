using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celebrity.Model.Entities
{
    public class Celebrities
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Date_Of_Birth { get; set; }
        public int IdTown { get; set; }
        public List<string> Movies { get; set; }
    }
}
