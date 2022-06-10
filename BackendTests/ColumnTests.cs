using System.Text.Json;
using IntroSE.Backend.Fronted.ServiceLayer;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace BackendTests
{
    internal class ColumnTests
    {
        private readonly BoardService column;

        public ColumnTests(BoardService board)
        {
            this.column = board;
        }

        public void RunTestsLimit()
        {
            //attempt to limit column 0 with invalid limit - should return Error
            //assuming that column with this ID exist
            //assuming the limit must be bigger than 0
            string res1 = column.LimitColumn("guy@gmail.com", "testBoard",1, -2);
            Response? response1 = JsonSerializer.Deserialize<Response>(res1);

            if (response1 != null && response1.ErrorMessage != null)
            {
                Console.WriteLine(response1.ErrorMessage);
            }
            else if (response1 != null)
            {
                Console.WriteLine("limit column applied successfully");
            }

            //attempt to limit column 0 with invalid limit - should return Error
            //assuming that column with this ID exist
            //assuming the limit must be bigger than 1
            string res2 = column.LimitColumn("guy@gmail.com", "testBoard", 1, 0);
            Response? response2 = JsonSerializer.Deserialize<Response>(res2);

            if (response2 != null && response2.ErrorMessage != null)
            {
                Console.WriteLine(response2.ErrorMessage);
            }
            else if (response2 != null)
            {
                Console.WriteLine("limit column applied successfully");
            }

            //attempt to limit column 0 to 20 tasks - should succeed
            //assuming that column with this ID exist
            string res3 = column.LimitColumn("guy@gmail.com", "testBoard", 1, 20);
            Response? response3 = JsonSerializer.Deserialize<Response>(res3);

            if (response3 != null && response3.ErrorMessage != null)
            {
                Console.WriteLine(response3.ErrorMessage);
            }
            else if (response3 != null)
            {
                Console.WriteLine("limit column applied successfully");
            }

            //attempt to limit column 0 to 1 tasks - should return Error
            //assuming that column with this ID exist and contains more tasks than the new limit(1)
            string res4 = column.LimitColumn("guy@gmail.com", "testBoard", 1, 1);
            Response? response4 = JsonSerializer.Deserialize<Response>(res4);

            if (response4 != null && response4.ErrorMessage != null)
            {
                Console.WriteLine(response4.ErrorMessage);
            }
            else if (response4 != null)
            {
                Console.WriteLine("limit column applied successfully");
            }
        }

        public void RunTestsGetLimit()
        {
            //attempt to recieve the column limit of column 0 - should succeed
            //assunming that the limit of column 0 is 1
            string res5 = column.GetColumnLimit("guy@gmail.com", "testBoard", 1);
            Response? response5 = JsonSerializer.Deserialize<Response>(res5);

            if (response5 != null && response5.ErrorMessage != null && (string)response5.ReturnValue != "1")
            {
                Console.WriteLine(response5.ErrorMessage);
            }
            else
            {
                Console.WriteLine("limit column returned successfully");
            }
        }

        public void RunTestsGetName()
        {
            //attempt to recieve the name of column 0 - should succeed
            //assuming that the name of column 0 is 'BackLog'
            string res6 = column.GetColumnName("guy@gmail.com", "testBoard", 1);
            Response? response6 = JsonSerializer.Deserialize<Response>(res6);

            if (response6 != null && response6.ErrorMessage != null && (string)response6.ReturnValue != "BackLog")
            {
                Console.WriteLine(response6.ErrorMessage);
            }
            else
            {
                Console.WriteLine("column name returned successfully");
            }
        }
    }
}