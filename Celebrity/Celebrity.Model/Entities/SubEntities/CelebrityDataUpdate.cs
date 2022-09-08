using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celebrity.Model.Entities.SubEntities
{
    public class CelebrityDataUpdate
    {
        public string Name { get; set; }
        public string Date_Of_Birth { get; set; } 
        public string Town { get; set; } 
        public List<string> MovieAdd { get; set; }
        public List<string> MovieDelete { get; set; }
    }
}
