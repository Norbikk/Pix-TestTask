namespace TestTask.Models
{
    /// <summary>
    /// Задание на перенос
    /// </summary>
    public class TransferTask
    {
        /// <summary>
        /// ID задания
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Запрос
        /// </summary>
        public Request Request { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        public TaskInfo Status { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public TransferTask()
        {
            Id = Guid.NewGuid().ToString();
            Status = new TaskInfo();
        }
    }
}
