namespace TestTask.Models
{
    /// <summary>
    /// Класс запроса
    /// </summary>
    public class Request
    {
        /// <summary>
        /// Набор параметров, для подключения
        /// </summary>
        public Connector Connector { get; set; }

        /// <summary>
        /// Имя таблицы
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Набор данных таблицы
        /// </summary>
        public List<FieldType> Fields { get; set; } 
    }
}
