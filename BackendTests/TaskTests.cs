using IntroSE.Backend.Fronted.ServiceLayer;
using IntroSE.Kanban.Backend.ServiceLayer;
using System.Text.Json;

namespace BackendTests
{
    internal class TaskTests
    {
        private readonly TaskService t;

        public TaskTests(TaskService t)
        {
            this.t = t;
        }

        public void RunTestsUpdateTaskDueDate()
        {
            //attempt to update task due date - should succeed
            //assuming that the column and task ID are exist
            string res26 = t.UpdateTaskDueDate("guy@gmail.com", new DateTime(2024, 04, 27, 23, 59, 59), "testBoard",1,1);
            var response26 = JsonSerializer.Deserialize<Response>(res26);

            if (response26 != null && response26.ErrorMessage != null)
            {
                Console.WriteLine(response26.ErrorMessage);
            }
            else if (response26 != null)
            {
                Console.WriteLine("Task due date updated successfully");
            }

            //attempt to update task due date to a date that already passed - should return Error
            //assuming that the column and task ID are exist
            string res27 = t.UpdateTaskDueDate("guy@gmail.com", new DateTime(2020, 04, 27, 23, 59, 59), "testBoard",1,1);
            var response27 = JsonSerializer.Deserialize<Response>(res27);

            if (response27 != null && response27.ErrorMessage != null)
            {
                Console.WriteLine(response27.ErrorMessage);
            }
            else if (response27 != null)
            {
                Console.WriteLine("Task due date updated successfully");
            }

            //attempt to update task due date to an empty due date (without due date) - should return Error
            //cannot update the dueDate to null
            string res28 = t.UpdateTaskDueDate("guy@gmail.com", new DateTime(), "testBoard",1,1);
            var response28 = JsonSerializer.Deserialize<Response>(res28);

            if (response28 != null && response28.ErrorMessage != null)
            {
                Console.WriteLine(response28.ErrorMessage);
            }
            else if (response28 != null)
            {
                Console.WriteLine("Task due date updated successfully");
            }
        }
        public void RunTestsUpdateTaskTitle()
        {
            //attempt to update Task title to a new title - should succeed
            //assuming that the column and task ID are exist
            string res29 = t.UpdateTaskTitle("guy@gmail.com", "testBoard",1,1,"newTitle");
            var response29 = JsonSerializer.Deserialize<Response>(res29);

            if (response29 != null && response29.ErrorMessage != null)
            {
                Console.WriteLine(response29.ErrorMessage);
            }
            else if (response29 != null)
            {
                Console.WriteLine("Task title updated successfully");
            }

            //attempt to update Task title to an empty title - should return Error
            //assuming that the column and task ID are exist
            string res30 = t.UpdateTaskTitle("guy@gmail.com", "testBoard",1,1,"");
            var response30 = JsonSerializer.Deserialize<Response>(res30);

            if (response30 != null && response30.ErrorMessage != null)
            {
                Console.WriteLine(response30.ErrorMessage);
            }
            else if (response30 != null)
            {
                Console.WriteLine("Task title updated successfully");
            }
        }
        public void RunTestsUpdateTaskDescription()
        {
            //attempt to update Task description - should succeed
            //assuming that the column and task ID are exist
            string res31 = t.UpdateTaskDescription("guy@gmail.com", "testBoard", 1, 1, "new description");
            var response31 = JsonSerializer.Deserialize<Response>(res31);

            if (response31 != null && response31.ErrorMessage != null)
            {
                Console.WriteLine(response31.ErrorMessage);
            }
            else if (response31 != null)
            {
                Console.WriteLine("Task description updated successfully");
            }

            //attempt to update Task description to an empty description - should succeed
            //assuming that the column and task ID are exist
            string res32 = t.UpdateTaskDescription("guy@gmail.com", "testBoard", 1, 1, "");
            var response32 = JsonSerializer.Deserialize<Response>(res32);

            if (response32 != null && response32.ErrorMessage != null)
            {
                Console.WriteLine(response32.ErrorMessage);
            }
            else if (response32 != null)
            {
                Console.WriteLine("Task description updated successfully");
            }
        }

        public void RunTestsAssignTask()
        {
            //attempt to assign Task 1 to another user - should succeed
            string res1 = t.AssignTask("guy@gmail.com", "testBoard", 1, 1, "ido@gmail.com");
            var response1 = JsonSerializer.Deserialize<Response>(res1);

            if (response1 != null && response1.ErrorMessage != null)
            {
                Console.WriteLine(response1.ErrorMessage);
            }
            else if (response1 != null)
            {
                Console.WriteLine("Task description updated successfully");
            }
        }
    }
}