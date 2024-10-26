using System;

namespace DataGrid.Standart.Contracts.Models
{
    /// <summary>
    /// Студент
    /// </summary>
    public class Person
    {
        public Guid Id { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        public string Name { get; set; }

        /// <inheritdoc cref="Models.Gender"/>
        public Gender Gender { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime BirthDay { get; set; }

        /// <summary>
        /// Средняя оценка
        /// </summary>
        public decimal AvgRate { get; set; }

        /// <summary>
        /// Признак отчисления
        /// </summary>
        public bool Expelled { get; set; }

        /// <summary>
        /// Признак задолженности
        /// </summary>
        public bool Debt { get; set; }
    }
}
