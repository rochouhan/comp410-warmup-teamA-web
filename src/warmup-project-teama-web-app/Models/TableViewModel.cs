using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace warmup_project_teama_web_app.Models
{
    /// <summary>
    /// Viewmodel that represents the query data table
    /// </summary>
    public class TableViewModel
    {
        /// <summary>
        /// List of entries used to populate the table
        /// </summary>
        public List<Entry> table;

        /// <summary>
        /// Initial constructor
        /// </summary>
        public TableViewModel()
        {
            table = new List<Entry>();
        }
        /// <summary>
        /// Constructor that takes in the list of entries
        /// </summary>
        /// <param name="table">A list of entries, can be null</param>
        public TableViewModel(List<Entry> table)
        {
            this.table = table;
        }

    }
}
