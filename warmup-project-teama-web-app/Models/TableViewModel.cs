using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace warmup_project_teama_web_app.Models
{
    public class TableViewModel
    {
        public List<Entry> table;

        public TableViewModel(List<Entry> table)
        {
            this.table = table;
        }
    }
}
