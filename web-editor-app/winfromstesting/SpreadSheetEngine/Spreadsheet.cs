using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    public class SpreadSheet : INotifyPropertyChanged
    {
        private int rowCount;
        private int columnCount;
        public Cell[,] spreadsheet;
        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        private void CellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Cell cell = sender as Cell; //this cell has just changed

            if (cell.Text.StartsWith("="))
            {
                cell.Value = ComputeExpression(cell);
                //ComputeExpression(cell);



                Tuple<int, int> temp = Tuple.Create(cell.RowIndex, cell.ColumnIndex);
                PropertyChanged(temp, new PropertyChangedEventArgs("Cell"));
            }
            else
            {
                spreadsheet[cell.RowIndex, cell.ColumnIndex].Text = cell.Text;
                cell.Value = cell.Text;
            }

        }

        public int ColumnCount { get => columnCount; }
        public int RowCount { get => rowCount; }
        public SpreadSheet(int rows, int cols)
        {
            rowCount = rows;
            columnCount = cols;
            spreadsheet = new Cell[rows, cols];

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    spreadsheet[row, col] = new SpreadSheetCell(row, col);

                    //spreadsheet subscribes to every cell
                    spreadsheet[row, col].PropertyChanged += CellPropertyChanged;
                }
            }
        }

        public Cell GetCell(int row, int col)
        {
            return spreadsheet[row, col];
        }

        public class SpreadSheetCell : Cell
        {
            public SpreadSheetCell(int row, int col) : base(row, col)
            {
            }
        }

        private string ComputeExpression(Cell cell)
        {
            List<char> alphabet = new List<char> { };
            for (char letter = 'A'; letter <= 'Z'; letter++)
            {
                alphabet.Add(letter);
            }
            var charArray = cell.Text.ToCharArray();


            int row = alphabet.FindIndex(a => a == (charArray[1]));
            int col = Int32.Parse(charArray[2].ToString());




            return spreadsheet[row, col].Text;
        }

    }
}
