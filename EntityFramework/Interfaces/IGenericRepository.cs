namespace EntityFramework.Interfaces
{
    /// <summary>
    /// Определяет методы получения данных
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Получить все объекты
        /// </summary>
        /// <returns>Список всех объектов</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Получить объект по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор объекта</param>
        /// <returns>Объект</returns>
        T? GetById(int id);

        /// <summary>
        /// Удлаить объект
        /// </summary>
        /// <param name="id">Идентификатор объекта</param>
        /// <returns>Объект удален, да/нет</returns>
        bool Remove(int id);

        /// <summary>
        /// Добавить объект
        /// </summary>
        /// <param name="entity">Объект</param>
        void Add(in T entity);

        /// <summary>
        /// Сохранить изменения
        /// </summary>
        /// <returns>Количество сохраненных строк</returns>
        int Save();

        //public T Select(Expression<Func<T, bool>> predicate);        
    }
}
