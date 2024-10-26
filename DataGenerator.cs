using DataGrid.Standart.Contracts.Models;
using System;

namespace DataGrid
{
    internal class DataGenerator
    {
        public static Person CreatePerson(Action<Person> settings = null)
        {
            var result = new Person
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                BirthDay = DateTime.Now.AddYears(-16),
            };

            settings?.Invoke(result);

            return result;
        }
    }
}
