using Microsoft.AspNetCore.Mvc;
using TestTask.Models;
using TaskInfo = TestTask.Models.TaskInfo;

namespace TestTask.Controllers
{

    /// <summary>
    /// Контроллер переноса данных
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TransferDataController : ControllerBase
    {
        /// <summary>
        /// Список задач
        /// </summary>
        public static List<TransferTask> Tasks = new List<TransferTask>();

        /// <summary>
        /// Возвращает ID задачи
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("CreateTask")]
        public string CreateTask(Request req)
        {
            TransferTask task = new TransferTask();
            task.Request = req;
            Tasks.Add(task);

            return task.Id;
        }

        /// <summary>
        /// Возвращает статус выполнения задачи по кнопке 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetStatusN1")]
        public ActionResult<TaskInfo> GetStatus(string id)
        {
            TaskInfo status = GetTaskInfo(id);

            if (status == null) return BadRequest();

            status.FakeProcess();

            return Ok(status);
        }

        /// <summary>
        /// Возвращает статус выполнения задачи c рандомной задержкой
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetStatusN2")]
        public ActionResult<TaskInfo> GetStatus2(string id)
        {
            TaskInfo status = GetTaskInfo(id);

            if (status == null) return BadRequest();

            status.FakeProcess2();

            return Ok(status);
        }

        /// <summary>
        /// Возвращает информацию всех задач
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetAllTasks")]
        public ActionResult<List<TaskInfo>> GetAllTasks()
        {
            return Ok(Tasks);
        }

        private TaskInfo GetTaskInfo(string id)
        {
            IEnumerable<TransferTask> tasks = Tasks.Where(x => x.Id == id);

            TransferTask? task = tasks.FirstOrDefault();

            return task?.Status;
        }

    }
}
