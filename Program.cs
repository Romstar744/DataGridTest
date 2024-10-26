using DataGrid.Standart.PeopleManager;
using DataGrid.Standart.Storage.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataGrid
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var factory = LoggerFactory.Create(builder => builder.AddDebug()); 
            var logger = factory.CreateLogger(nameof(DataGrid));

            var storage = new MemoryPeopleStorage();
            var manager = new PeopleManager(storage, logger);

            Application.Run(new Form1(manager));
        }
    }
}
