using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace TestTask.Models
{
    /// <summary>
    /// Перечисление статусов выполнения
    /// </summary>
    public enum Status
    {
        Pending,
        Processing,
        Success,
        Error
    }
    public class TaskInfo
    {
        /// <summary>
        /// Статус выполнения
        /// </summary>
        public Status State { get; set; }
        /// <summary>
        /// Время создания процесса
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// Время запуска процесса
        /// </summary>
        public DateTime Started { get; set; }
        /// <summary>
        /// Время конца процесса
        /// </summary>
        public DateTime Finished { get; set; }

        /// <summary>
        /// Комментарий к статусу
        /// </summary>
        public string ProcessingInfo { get; set; }

        /// <summary>
        /// Полное кол-во строк
        /// </summary>
        public int TotalRows { get; set; }
        /// <summary>
        /// Кол-во удачно перенесенных строк
        /// </summary>
        public int SuccesedRows { get; set; }
        /// <summary>
        /// Кол-во строк с ошибкой
        /// </summary>
        public int FailedRows { get; set; }

        private const int _WAIT_START = 5;
        private const int _WAIT_FINISH = 20;
        private const int _ERROR_SECOND = 27;
        private const int _FAILED_ROWS_COUNT = 10;

        /// <summary>
        /// Конструктор
        /// </summary>
        public TaskInfo()
        {
            Created = DateTime.Now;
            TotalRows = new Random().Next(100) + 100;
            State = Status.Pending;
            ProcessingInfo = "Pending";
        }

        /// <summary>
        /// Фейковый процесс выполнения
        /// </summary>
        public void FakeProcess()
        {
            DateTime start = DateTime.Now;

            // Дельта вызова метода от создания задачи
            double deltaSeconds = GetDeltaSeconds(start);

            // Начинаем выполнение задачи через 5 секунд
            if (deltaSeconds > _WAIT_START && State == Status.Pending)
            {
                Started = Created.AddSeconds(_WAIT_START);
                State = Status.Processing;
                ProcessingInfo = "Processing";
            }

            // Если прошло 20 секунд - заканчиваем выполнение задачи
            if (deltaSeconds > _WAIT_FINISH && (State != Status.Success && State != Status.Error))
            {
                Finished = DateTime.Now;

                // Если прошло больше положенного времени - Error
                if ((start - Started).TotalSeconds > _ERROR_SECOND)
                {
                    StateError();
                    return;
                }

                StateSuccess();
            }
        }

        /// <summary>
        /// Фейковый процесс выполнения с задержкой
        /// </summary>
        public void FakeProcess2()
        {
            if (State == Status.Success || State == Status.Error) return;

            // Стартуем таймер
            Stopwatch sw = Stopwatch.StartNew();
            // Выполнение задачи началось
            Started = DateTime.Now;

            // Задаем задержку
            Random r = new Random();
            TimeSpan delayTime = TimeSpan.FromSeconds(r.Next(1, 30));
            Thread.Sleep(delayTime);

            // Выполнение задачи закончилось
            Finished = DateTime.Now;

            //Останавливаем таймер
            sw.Stop();

            double totalSeconds = TimeSpan.FromMilliseconds((double)sw.ElapsedMilliseconds).TotalSeconds;
            // Если прошло больше ожидаемого времени - Error
            if (totalSeconds > _WAIT_FINISH)
            {
                StateError();
                return;
            }

            StateSuccess();
        }


        /// <summary>
        /// Возвращает кол-во секунд из разницы нынешнего времени и создания хадачи
        /// </summary>
        /// <param name="now"></param>
        /// <returns></returns>
        private double GetDeltaSeconds(DateTime now)
        {
            return (now - Created).TotalSeconds;
        }

        /// <summary>
        /// Меняет статус на ERROR
        /// </summary>
        private void StateError()
        {
            State = Status.Error;
            ProcessingInfo = "We have an ERROR";
            FailedRows = _FAILED_ROWS_COUNT;
            SuccesedRows = TotalRows - FailedRows;
        }

        /// <summary>
        /// Меняет статус на SUCCESS
        /// </summary>
        private void StateSuccess()
        {
            State = Status.Success;
            ProcessingInfo = "DONE";
            SuccesedRows = TotalRows;
        }
    }
}
