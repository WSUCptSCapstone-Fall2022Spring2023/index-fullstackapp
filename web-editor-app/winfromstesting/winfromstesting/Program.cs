// See https://aka.ms/new-console-template for more information
using SpreadsheetEngine;
using System.ComponentModel;

SpreadSheet spreadSheet;
spreadSheet = new SpreadSheet(50, 26);


//PropertyChangedEventHandler PropertyChanged = spreadSheet.PropertyChanged;





spreadSheet.PropertyChanged += MyClass_PropertyChanged;


//Console.WriteLine(spreadSheet.GetCell(0,0).Text);

spreadSheet.GetCell(1, 2).Text = "bannnanas";
spreadSheet.GetCell(0, 0).Text = "=B2";




static void MyClass_PropertyChanged(object sender, PropertyChangedEventArgs e)
{



    Tuple<int, int> temp = sender as Tuple<int, int>;

    int row = temp.Item1;
    int col = temp.Item2;

    Console.Write("we need to update cell: " + "[" + row + "," + col + "]");
}

Console.WriteLine("with value: " + spreadSheet.GetCell(0, 0).Value);

Console.WriteLine(spreadSheet.GetCell(0, 0).Value);
Console.WriteLine(spreadSheet.GetCell(0, 0).Value);
Console.WriteLine(spreadSheet.GetCell(0, 0).Value);
