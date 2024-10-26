using DataGrid.Standart.PeopleManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataGrid.Standart.Contracts;
using DataGrid.Standart.Contracts.Models;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace DataGrid.Standart.PeopleManager
{
    public class PeopleManager : IPeopleManager
    {
        private IPeopleStorage peopleStorage;
        private readonly ILogger logger;
        private const string StopwatchTemplate = "{0} выполнялся {1} мс";

        public PeopleManager(IPeopleStorage peopleStorage, ILogger logger)
        {

            this.logger = logger;
            this.peopleStorage = peopleStorage;
        }

        public async Task<Person> AddAsync(Person person)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var result = await peopleStorage.AddAsync(person);

            stopwatch.Stop();
            logger.LogInformation(string.Format(StopwatchTemplate, nameof(AddAsync), stopwatch.ElapsedMilliseconds));
            return result;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await peopleStorage.DeleteAsync(id);
            if (result)
            {
                logger.LogInformation($"Пользователь с идентификатором {id} удален");
            }
            else
            {
				logger.LogInformation($"Не уадлось удалить пользователя с идентификатром {id}");

			}
			return result;
        }

        public Task EditAsync(Person person)
            => peopleStorage.EditAsync(person);

        public Task<IReadOnlyCollection<Person>> GetAllAsync()
            => peopleStorage.GetAllAsync();

        public async Task<IPeopleStats> GetStatsAsync()
        {
            var result = await peopleStorage.GetAllAsync();
            return new PeopleStatsModel
            {
                Count = result.Count,
                MaleCount = result.Where(x => x.Gender == Gender.Male).Count(),
                FemaleCount = result.Where(x => x.Gender == Gender.Female).Count(),
                DebtCount = result.Where(x => x.Debt).Count(),
                ExpelledCount = result.Where(x => x.Expelled).Count(),
                AverageRate = result.DefaultIfEmpty(new Person()).Average(x => x.AvgRate),
            };
        }
    }
}
