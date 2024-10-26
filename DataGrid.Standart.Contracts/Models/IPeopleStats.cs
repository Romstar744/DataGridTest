namespace DataGrid.Standart.Contracts.Models
{
    public interface IPeopleStats
    {
        int Count { get; }

        int FemaleCount { get; }

        int MaleCount { get; }

        int ExpelledCount { get; }

        int DebtCount { get; }

        decimal AverageRate { get; }
    }
}
