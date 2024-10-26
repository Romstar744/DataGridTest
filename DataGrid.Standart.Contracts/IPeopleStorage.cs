using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataGrid.Standart.Contracts;
using DataGrid.Standart.Contracts.Models;

namespace DataGrid.Standart.Contracts
{
    /// <summary>
    /// Storage of students
    /// </summary>
    public interface IPeopleStorage
    {
        /// <summary>
        /// Получает список <see cref="Person"/>
        /// </summary>
        Task<IReadOnlyCollection<Person>> GetAllAsync();

        Task<Person> AddAsync(Person person);

        Task EditAsync(Person person);

        Task<bool> DeleteAsync(Guid id);
    }
}
