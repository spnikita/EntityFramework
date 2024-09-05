namespace EntityFramework.Models
{
    /// <summary>
    /// Базовый класс сущности
    /// </summary>
    internal abstract class Base
    {
        /// <summary>
        /// Идентификатор объекта
        /// </summary>
        public int Id { get; set; }
    }
}
