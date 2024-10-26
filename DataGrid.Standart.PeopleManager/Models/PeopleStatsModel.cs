using DataGrid.Standart.Contracts;
using DataGrid.Standart.Contracts.Models;

namespace DataGrid.Standart.PeopleManager.Models
{
    public class PeopleStatsModel : IPeopleStats
    {
        public int Count { get; set; }

        public int FemaleCount { get; set; }

        public int MaleCount { get; set; }

        public int ExpelledCount { get; set; }

        public int DebtCount { get; set; }

        public decimal AverageRate { get; set; }
    }
}
