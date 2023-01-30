using Microsoft.AspNetCore.Connections;

namespace TestTask.Models
{
    /// <summary>
    /// Набор параметров, для подключения к БД
    /// </summary>
    public class Connector
    {
        /// <summary>
        /// Адрес, куда подключаться
        /// </summary>
        public string Adress { get; set; }

        /// <summary>
        /// Порт подключения
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Тип сервера
        /// </summary>
        public string ServerType { get; set; }

    }
}
