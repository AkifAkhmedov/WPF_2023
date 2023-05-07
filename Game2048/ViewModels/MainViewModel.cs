
using Game2048.Models;
using System;
using System.Collections.ObjectModel;
namespace Game2048.ViewModels
{
    internal class MainViewModel
    {
        public MainModel Main { get; set; } = new MainModel();
        //public Cell Cell { get; set; } = new Cell();

        

        public MainViewModel() {
            Cell cell1 = new Cell();
            Cell cell2 = new Cell();
            Cell cell3 = new Cell();
            Cell cell4 = new Cell();

            Cell cell5 = new Cell();
            Cell cell6 = new Cell();
            Cell cell7 = new Cell();
            Cell cell8 = new Cell();

            Cell cell9 = new Cell();
            Cell cell10 = new Cell();
            Cell cell11 = new Cell();
            Cell cell12 = new Cell();

            Cell cell13 = new Cell();
            Cell cell14 = new Cell();
            Cell cell15 = new Cell();
            Cell cell16 = new Cell();

            cell1._Right = cell2;
            cell1._Down = cell5;

            cell2._Down = cell6;
            cell2._Right = cell3;
            cell2._Left = cell1;

            cell3._Right = cell4;
            cell3._Left = cell2;
            cell3._Down = cell7;

            cell4._Left = cell3;
            cell4._Down = cell8;

            cell5._Right = cell6;
            cell5._Up = cell1;
            cell5._Down = cell9;

            cell6._Right = cell7;
            cell6._Left = cell5;
            cell6._Up = cell2;
            cell6._Down = cell10;

            cell7._Right = cell8;
            cell7._Left = cell6;
            cell7._Down = cell11;
            cell7._Up = cell3;

            cell8._Left = cell7;
            cell8._Down = cell12;
            cell8._Up = cell4;

            cell9._Right = cell10;
            cell9._Down = cell13;
            cell9._Up = cell5;

            cell10._Right = cell11;
            cell10._Left = cell9;
            cell10._Down = cell14;
            cell10._Up = cell6;

            cell11._Right = cell12;
            cell11._Left = cell10;
            cell11._Down = cell15;
            cell11._Up = cell7;

            cell12._Left = cell11;
            cell12._Down = cell16;
            cell12._Up = cell8;

            cell13._Right = cell14;
            cell13._Up = cell9;

            cell14._Right = cell15;
            cell14._Left = cell13;
            cell14._Up = cell10;

            cell15._Right = cell16;
            cell15._Left = cell14;
            cell15._Up = cell11;

            cell16._Left = cell15;
            cell16._Up = cell12;
            listCell.Add(cell1);
            listCell.Add(cell2);
            listCell.Add(cell3);
            listCell.Add(cell4);
            listCell.Add(cell5);
            listCell.Add(cell6);
            listCell.Add(cell7);
            listCell.Add(cell8);
            listCell.Add(cell9);
            listCell.Add(cell10);
            listCell.Add(cell11);
            listCell.Add(cell12);
            listCell.Add(cell13);
            listCell.Add(cell14);
            listCell.Add(cell15);
            listCell.Add(cell16);
        }

        public ObservableCollection<Cell> listCell { get; set; } = new();

        public void RndBar()
        {
            Random random= new Random();

            random.Next(0,15);
            int y = random.Next(0,15);
            if (listCell[y].BarNum == null) listCell[y].BarNum = 2;
            else
            {
                RndBar();
            }
        }


        public void Right()
        {
            for (int x = 0; x < listCell.Count; x++)
            {
                listCell[x].Right();
            }
            RndBar();
        }
        public void Left()
        {
            for (int x = listCell.Count - 1; x >= 0; x--)
            {
                listCell[x].Left();
            }
            RndBar();
        }
        public void Up()
        {
            for (int x = listCell.Count - 1; x >= 0; x--)
            {
                listCell[x].Up();
            }
            RndBar();
        }
        public void Down()
        {
            for (int x = 0; x < listCell.Count; x++)
            {
                listCell[x].Down();
            }
            RndBar();
        }
    }
}
