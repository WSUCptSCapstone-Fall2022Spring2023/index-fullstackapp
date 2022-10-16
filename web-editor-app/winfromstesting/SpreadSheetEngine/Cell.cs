using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace SpreadsheetEngine
{
    public abstract class Cell : INotifyPropertyChanged
    {
        private readonly int rowIndex;
        private readonly int columnIndex;
        protected string text;
        protected string value;


        //event = list of delegated
        //this is a list of observers/listeners/subscribers
        
        //what should be subscribed to this event?? 
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public Cell(int row, int col)
        {
            rowIndex = row;
            columnIndex = col;
        }

        public int RowIndex { get => rowIndex; }
        public int ColumnIndex { get => columnIndex; }
        public string Text
        {
            get { return text; }

            set //internal?
            {
                if (this.text == value)
                {
                    return;
                }
                else
                {
                    text = value;
                    //fire property chnaged event
                    //allows the cell to notify anything that subscribes to this event that the “Text” property has changed.
                    //tell ui that text has changed
                    PropertyChanged(this, new PropertyChangedEventArgs("Text"));

                    //NotifyPropertyChanged("Text");
                }
            }
        }
        public string Value
        {
            get { return value; }

            protected internal set
            {
                if (this.value == value)
                {
                    return;
                }
                else
                {
                    this.value = value;
                }
                //PropertyChanged(this, new PropertyChangedEventArgs("Value"));
            }
        }




    }
}
