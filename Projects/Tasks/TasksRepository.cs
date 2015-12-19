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
        public DictionariesTasks DictionariesTasks { get; private set; }
		public BusinessTripsTasks BusinessTripsTasks { get; private set; }

        public TasksRepository()
        {
            this.UsersTasks = new UsersTasks();
            this.DictionariesTasks = new DictionariesTasks();
            this.BusinessTripsTasks = new BusinessTripsTasks();
        }
    }
}
