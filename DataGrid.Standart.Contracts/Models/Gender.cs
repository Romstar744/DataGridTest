using System.ComponentModel;

namespace DataGrid.Standart.Contracts.Models
{
    /// <summary>
    /// Пол
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("Мужской")]
        Male = 1,

        /// <summary>
        /// 
        /// </summary>
        [Description("Женский")]
        Female = 2,

        /// <summary>
        /// 
        /// </summary>
        [Description("Псина")]
        Dog = 3,

        /// <summary>
        /// 
        /// </summary>
        [Description("Котик")]
        Cat = 4,
    }
}
