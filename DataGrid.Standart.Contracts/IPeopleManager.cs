using DataGrid.Standart.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataGrid.Standart.Contracts
{
    public interface IPeopleManager
    {
        Task<IReadOnlyCollection<Person>> GetAllAsync();

        Task<Person> AddAsync(Person person);

        Task EditAsync(Person person);

        Task<bool> DeleteAsync(Guid id);

        Task<IPeopleStats> GetStatsAsync();
    }
}
