using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celebrity.Model.Entities.SubEntities
{
    public class CelebrityDataShow
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Date_Of_Birth { get; set; } = string.Empty;
        public string Town { get; set; } = string.Empty;
        public List<string> Movies { get; set; }
    }
}
