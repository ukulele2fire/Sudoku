using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Sudoku
{

    static class Program
    {
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GameForm());
            int[,] matrix = new int[9, 9];
            Board test = new Board();
            test.solve();
            test.printBoard();
        }
    }
}
