using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyAppsStudio.Delegacje.Tasks
{
    public class TasksRepository : ITasksRepository
    {
        public UsersTasks UsersTasks { get; private set; }

        public TasksRepository()
        {
            this.UsersTasks = new UsersTasks();
        }
    }
}
