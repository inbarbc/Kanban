using BackendTests;
using IntroSE.Backend.Fronted.BusinessLayer;
using IntroSE.Backend.Fronted.ServiceLayer;
using Kanban.BackendTests.Program;

namespace IntroSE.ForumSystem.Frontend
{
    class Program
    {
        public static void Main(string[] args)
        {
            
            UserController uc = new UserController();
            UserService user = new UserService(uc);
            BoardService board = new BoardService(uc);

            UserTests userTests = new UserTests(user);
            BoardTests boardTests = new BoardTests(board);
            TaskTests taskTests = new TaskTests(board);
            ColumnTests columnTests = new ColumnTests(board);
            DalTests datTests = new DalTests(board, user);

            datTests.RunTestsLoadData();
            Console.WriteLine("---------------------------------------------------------------------------");
            userTests.RunTestsRegister();
            Console.WriteLine("---------------------------------------------------------------------------");
            userTests.RunTestsLogin();
            Console.WriteLine("---------------------------------------------------------------------------");
            boardTests.RunAddBoardTests();
            Console.WriteLine("---------------------------------------------------------------------------");
            userTests.RunTestsGetUserBoards();
            Console.WriteLine("---------------------------------------------------------------------------");
            boardTests.RunTestsGetBoardName();
            Console.WriteLine("---------------------------------------------------------------------------");
            boardTests.RunTestsJoinBoard();
            Console.WriteLine("---------------------------------------------------------------------------");
            boardTests.RunTestsLeaveBoard();
            Console.WriteLine("---------------------------------------------------------------------------");
            boardTests.RunAddTaskTests();
            Console.WriteLine("---------------------------------------------------------------------------");
            taskTests.RunTestsAssignTask();
            Console.WriteLine("---------------------------------------------------------------------------");
            boardTests.RunAdvanceTaskTests();
            Console.WriteLine("---------------------------------------------------------------------------");
            userTests.RunTestsInProgressTasks();
            Console.WriteLine("---------------------------------------------------------------------------");
            boardTests.RunGetColumnTests();
            Console.WriteLine("---------------------------------------------------------------------------");
            columnTests.RunTestsLimit();
            Console.WriteLine("---------------------------------------------------------------------------");
            columnTests.RunTestsGetLimit();
            Console.WriteLine("---------------------------------------------------------------------------");
            columnTests.RunTestsGetName();
            Console.WriteLine("---------------------------------------------------------------------------");
            taskTests.RunTestsUpdateTaskDueDate();
            Console.WriteLine("---------------------------------------------------------------------------");
            taskTests.RunTestsUpdateTaskTitle();
            Console.WriteLine("---------------------------------------------------------------------------");
            taskTests.RunTestsUpdateTaskDescription();
            Console.WriteLine("---------------------------------------------------------------------------");
            userTests.RunTestsInProgressTasks();
            Console.WriteLine("---------------------------------------------------------------------------");
            boardTests.RunRemoveBoardsTests();
            Console.WriteLine("---------------------------------------------------------------------------");
            userTests.RunTestsLogout();
            Console.WriteLine("---------------------------------------------------------------------------");
            datTests.RunTestsDeleteData();
        }
    }
}
