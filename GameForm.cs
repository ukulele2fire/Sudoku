using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku
{
    public partial class GameForm : Form
    {
        private static Board myBoard;
        private static int[,] myMatrix;
        private static TextBox[,] allTextBoxes;
        public GameForm()
        {
            InitializeComponent();
            myBoard = new Board();
            myMatrix = new int[9, 9];
            allTextBoxes = new TextBox[,]{
                { textBox1,
                textBox2,
                textBox3,
                textBox4,
                textBox5,
                textBox6,
                textBox7,
                textBox8,
                textBox9 },
                { textBox10,
                textBox11,
                textBox12,
                textBox13,
                textBox14,
                textBox15,
                textBox16,
                textBox17,
                textBox18 },
                { textBox19,
                textBox20,
                textBox21,
                textBox22,
                textBox23,
                textBox24,
                textBox25,
                textBox26,
                textBox27 },
                { textBox28,
                textBox29,

                textBox30,
                textBox31,
                textBox32,
                textBox33,
                textBox34,
                textBox35,
                textBox36 },
                { textBox37,
                textBox38,
                textBox39,

                textBox40,
                textBox41,
                textBox42,
                textBox43,
                textBox44,
                textBox45 },
                { textBox46,
                textBox47,
                textBox48,
                textBox49,

                textBox50,
                textBox51,
                textBox52,
                textBox53,
                textBox54 },
                { textBox55,
                textBox56,
                textBox57,
                textBox58,
                textBox59,

                textBox60,
                textBox61,
                textBox62,
                textBox63 },
                { textBox64,
                textBox65,
                textBox66,
                textBox67,
                textBox68,
                textBox69,

                textBox70,
                textBox71,
                textBox72 },
                { textBox73,
                textBox74,
                textBox75,
                textBox76,
                textBox77,
                textBox78,
                textBox79,

                textBox80,
                textBox81 }
            };
        }

        private void TextChanged(object sender, EventArgs e)
        {
            TextBox text = sender as TextBox;
            string tag = text.Tag.ToString();
            int square;
            char box;

            bool parsed = int.TryParse(tag.Substring(0, 2), out square);
            if (!parsed)
            {
                square = int.Parse(tag.Substring(0, 1));
                box = tag[1];
            }
            else
                box = tag[2];

            int row = square / 9;
            int col = square % 9;

            if (textValid(text.Text, row, col))
                myMatrix[row, col] = int.Parse(text.Text);
            else
                text.Text = "";

        }

        private bool textValid(string text, int row, int col)
        {
            return (isNumber(text) && noMistake(text, row, col));
        }

        private bool isNumber(string text)
        {
            int num;
            bool passed = int.TryParse(text, out num);
            if (!passed)
                return false;
            for (int i = 1; i <= 9; i++)
            {
                if (num == i)
                    return true;
            }
            return false;
        }

        private bool noMistake(string text, int r, int c)
        {
            int num = int.Parse(text);
            int[,] copy = myMatrix.Clone() as int[,];
            copy[r, c] = num;
            Board test = new Board(copy);
            return test.checkBoard();

        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
            myMatrix = new int[9, 9];
            myBoard = new Board(myMatrix);
        }

        private void solveBtn_Click(object sender, EventArgs e)
        {
            myBoard.myMatrix = myMatrix;
            myBoard.printBoard();
            myBoard.solve();
            myBoard.printBoard();
            updateBoard();
        }

        //translate matrix to gui
        public static void updateBoard()
        {
            int i = 0;
            myMatrix = myBoard.myMatrix;
            foreach (TextBox box in allTextBoxes)
            {
                int r = i / 9;
                int c = i % 9;
                if (myMatrix[r, c] != 0)
                    box.Text = myMatrix[r, c] + "";
                else
                    box.Text = " ";
                i++;
            }
        }

        private void ClearTextBoxes()
        {
            Action<Control.ControlCollection> func = null;

            func = (controls) =>
            {
                foreach (Control control in controls)
                    if (control is TextBox)
                        (control as TextBox).Clear();
                    else
                        func(control.Controls);
            };

            func(Controls);
        }

        private void KeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            string tag = text.Tag.ToString();
            int square;

            bool parsed = int.TryParse(tag.Substring(0, 2), out square);
            if (!parsed)
                square = int.Parse(tag.Substring(0, 1));

            int row = square / 9;
            int col = square % 9;

            switch (e.KeyCode)
            {
                case Keys.Down:
                    row++;
                    break;
                case Keys.Left:
                    col--;
                    break;
                case Keys.Right:
                    col++;
                    break;
                case Keys.Up:
                    row--;
                    break;
                default:
                    return;
            }
            if (col >= 9)
            {
                row++;
                col = 0;
            }
            if (col < 0)
            {
                row--;
                col = 8;
            }
            if (row < 0)
                row = 8;
            if (row >= 9)
                row = 0;

            allTextBoxes[row, col].Focus();
        }

        private void fillBtn_Click(object sender, EventArgs e)
        {
            myBoard.fillBoard();
            myMatrix = myBoard.myMatrix;
            updateBoard();
        }

        
    }
}
