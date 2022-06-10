using IntroSE.Backend.Fronted.ServiceLayer;
using IntroSE.Kanban.Backend.ServiceLayer;
using System.Text.Json;

namespace BackendTests
{
    internal class BoardTests
    {
        private readonly BoardService board;

        public BoardTests(BoardService board)
        {
            this.board = board;
        }

        public void RunAddBoardTests()
        {
            //attempt to add new Board with title 'testBoard' - should succeed
            //assuming that there is no Board exist with this title
            string res1 = board.addBoard("guy@gmail.com", "testBoard");
            var response1 = JsonSerializer.Deserialize<Response>(res1);
            if (response1 != null && response1.ErrorMessage != null)
            {
                Console.WriteLine(response1.ErrorMessage);
            }
            else if (response1 != null)
            {
                Console.WriteLine("Board added successfully");
            }

            //attempt to add new Board with title that already exist in the kanban - should return Error
            //assuming that Board with this title already exist 
            string res2 = board.addBoard("guy@gmail.com", "testBoard");
            var response2 = JsonSerializer.Deserialize<Response>(res2);
            if (response2 != null && response2.ErrorMessage != null)
            {
                Console.WriteLine(response2.ErrorMessage);
            }
            else if (response2 != null)
            {
                Console.WriteLine("Board added successfully");
            }

            //attempt to add new Board with an empty title - should return Error
            string res3 = board.addBoard("guy@gmail.com", "");
            var response3 = JsonSerializer.Deserialize<Response>(res3);
            if (response3 != null && response3.ErrorMessage != null)
            {
                Console.WriteLine(response3.ErrorMessage);
            }
            else if (response3 != null)
            {
                Console.WriteLine("Board added successfully");
            }
        }

        public void RunRemoveBoardsTests()
        {
            //attempt to remove board - should succeed
            //assuming that Board with this title does exist 
            string res13 = board.removeBoard("guy@gmail.com", "testBoard");
            var response13 = JsonSerializer.Deserialize<Response>(res13);
            if (response13 != null && response13.ErrorMessage != null)
            {
                Console.WriteLine(response13.ErrorMessage);
            }
            else if (response13 != null)
            {
                Console.WriteLine("Board removed successfully");
            }

            //attempt to remove board - should return Error
            //assuming that Board with this title does not exist 
            string res14 = board.removeBoard("guy@gmail.com", "testBoard");
            var response14 = JsonSerializer.Deserialize<Response>(res14);
            if (response14 != null && response14.ErrorMessage != null)
            {
                Console.WriteLine(response14.ErrorMessage);
            }
            else if (response14 != null)
            {
                Console.WriteLine("Board removed successfully");
            }

            //attempt to remove board when given empty title - should return Error
            string res15 = board.removeBoard("guy@gmail.com", "");
            var response15 = JsonSerializer.Deserialize<Response>(res15);
            if (response15 != null && response15.ErrorMessage != null)
            {
                Console.WriteLine(response15.ErrorMessage);
            }
            else if (response15 != null)
            {
                Console.WriteLine("Board removed successfully");
            }
        }

        public void RunAddTaskTests() {
            //attempt to add new task with title 'titleTest' - should succeed
            //assuming that the numbers of tasks in the column we using did not reach his limit
            string res4 = board.addTask("guy@gmail.com", "testBoard", "titleTest", "tests", new DateTime(2022, 04, 27, 23, 59, 59));
            var response4 = JsonSerializer.Deserialize<Response>(res4);
            if (response4 != null && response4.ErrorMessage != null)
            {
                Console.WriteLine(response4.ErrorMessage);
            }
            else if (response4 != null)
            {
                Console.WriteLine("Task added successfully");
            }

            //attempt to add new task with title 'titleTest'(already exist) - should secceed
            //assuming that task with the title 'titleTest' already exist in the column and it is ok
            string res5 = board.addTask("guy@gmail.com", "testBoard", "titleTest", "tests", new DateTime(2020, 04, 27, 23, 59, 59));
            var response5 = JsonSerializer.Deserialize<Response>(res5);
            if (response5 != null && response5.ErrorMessage != null)
            {
                Console.WriteLine(response5.ErrorMessage);
            }
            else if (response5 != null)
            {
                Console.WriteLine("Task added successfully");
            }

            //attempt to add new task with empty title - should return Error
            //assuming that it is illeagl to add new task without title
            string res6 = board.addTask("guy@gmail.com", "testBoard", "", "tests", new DateTime(2022, 04, 27, 23, 59, 59));
            var response6 = JsonSerializer.Deserialize<Response>(res6);
            if (response6 != null && response6.ErrorMessage != null)
            {
                Console.WriteLine(response6.ErrorMessage);
            }
            else if (response6 != null)
            {
                Console.WriteLine("Task added successfully");
            }

            //attempt to add new Task with empty description - should succeed
            //assuming that the numbers of tasks in the column we using did not reach his limit
            string res7 = board.addTask("guy@gmail.com", "testBoard", "titleTest2", "", new DateTime(2022, 04, 27, 23, 59, 59));
            var response7 = JsonSerializer.Deserialize<Response>(res7);
            if (response7 != null && response7.ErrorMessage != null)
            {
                Console.WriteLine(response7.ErrorMessage);
            }
            else if (response7 != null)
            {
                Console.WriteLine("Task added successfully");
            }
            //attempt to add new Task with empty due date - should succeed
            //assuming that the numbers of tasks in the column we using did not reach his limit
            string res8 = board.addTask("guy@gmail.com", "testBoard", "titleTest3", "tests", new DateTime());
            var response8 = JsonSerializer.Deserialize<Response>(res8);
            if (response8 != null && response8.ErrorMessage != null)
            {
                Console.WriteLine(response8.ErrorMessage);
            }
            else if (response8 != null)
            {
                Console.WriteLine("Task added successfully");
            }

        }
        public void RunAdvanceTaskTests() {
            //attempt to advance task to the next column - should succeeed
            //assuming that the task and the columns ID are exist(the next column as well)
            string res9 = board.advanceTask("guy@gmail.com", "testBoard", 0, 1);
            var response9 = JsonSerializer.Deserialize<Response>(res9);
            if (response9 != null && response9.ErrorMessage != null)
            {
                Console.WriteLine(response9.ErrorMessage);
            }
            else if (response9 != null)
            {
                Console.WriteLine("Task advanced successfully");
            }

            //attempt to advance task to the next column - should succeeed
            //assuming that the task and the columns ID are exist(the next column as well)
            string res10 = board.advanceTask("guy@gmail.com", "testBoard", 0, 2);
            var response10 = JsonSerializer.Deserialize<Response>(res10);
            if (response10 != null && response10.ErrorMessage != null)
            {
                Console.WriteLine(response10.ErrorMessage);
            }
            else if (response10 != null)
            {
                Console.WriteLine("Task advanced successfully");
            }

            //attempt to recieve the name of a specefic column (according to ID) - should succeed
            //assuming that column with this Id does exist
        }
        public void RunGetColumnTests() {
            string res11 = board.getColumn("guy@gmail.com", "testBoard", 0);
            var response11 = JsonSerializer.Deserialize<Response>(res11);
            if (response11 != null && response11.ErrorMessage != null)
            {
                Console.WriteLine(response11.ErrorMessage);
            }
            else if (response11 != null)
            {
                Console.WriteLine("column returned successfully");
            }

            //attempt to recieve the name of a specefic column (according to ID) that does not exist - should return Error
            //assuming that column with this Id does not exist
            string res12 = board.getColumn("guy@gmail.com", "testBoard", 10);
            var response12 = JsonSerializer.Deserialize<Response>(res12);
            if (response12 != null && response12.ErrorMessage != null)
            {
                Console.WriteLine(response12.ErrorMessage);
            }
            else if (response12 != null)
            {
                Console.WriteLine("column returned successfully");
            }
        }

        public void RunTestsGetBoardName()
        {
            //attempt to recieve the name of boardID 1 - should succeed
            //assuming that column with this Id does not exist
            string res13 = board.GetBoardName(1);
            var response13 = JsonSerializer.Deserialize<Response>(res13);
            if (response13 != null && response13.ErrorMessage != null)
            {
                Console.WriteLine(response13.ErrorMessage);
            }
            else if (response13 != null)
            {
                Console.WriteLine("boad name returned successfully");
            }

            //attempt to recieve the name of boardID 100 - should returned error
            //assuming that column with this Id does not exist
            string res14 = board.GetBoardName(100);
            var response14 = JsonSerializer.Deserialize<Response>(res14);
            if (response14 != null && response14.ErrorMessage != null)
            {
                Console.WriteLine(response14.ErrorMessage);
            }
            else if (response14 != null)
            {
                Console.WriteLine("boad name returned successfully");
            }
        }

        public void RunTestsJoinBoard()
        {
            //attempt to join Board of another user - should succeed
            string res1 = board.JoinBoard("ido@gmail.com", 1);
            var response1 = JsonSerializer.Deserialize<Response>(res1);
            if (response1 != null && response1.ErrorMessage != null)
            {
                Console.WriteLine(response1.ErrorMessage);
            }
            else if (response1 != null)
            {
                Console.WriteLine("joined boad successfully");
            }
        }

        public void RunTestsLeaveBoard()
        {
            //attempt to leave Board of another user - should succeed
            string res1 = board.LeaveBoard("ido@gmail.com", 1);
            var response1 = JsonSerializer.Deserialize<Response>(res1);
            if (response1 != null && response1.ErrorMessage != null)
            {
                Console.WriteLine(response1.ErrorMessage);
            }
            else if (response1 != null)
            {
                Console.WriteLine("left boad successfully");
            }
        }

        public void RunTestsTransferOwnership()
        {
            //attempt to change Board owner to another user - should succeed
            string res1 = board.ChangeOwner("guy@gmail.com", "ido@gmail.com", "testBoard");
            var response1 = JsonSerializer.Deserialize<Response>(res1);
            if (response1 != null && response1.ErrorMessage != null)
            {
                Console.WriteLine(response1.ErrorMessage);
            }
            else if (response1 != null)
            {
                Console.WriteLine("boad  ownership transfered successfully");
            }
        }
    }
}
