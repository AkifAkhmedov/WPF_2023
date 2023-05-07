
using Game2048.Model;

namespace Game2048.Models
{
    internal class Cell : Changed
    {
        private string _colorBar = "#FFCDC1B4";
        public string ColorBar
        {
            get { return _colorBar; }
            set
            {
                _colorBar = value;
                OnPropertyChanged("ColorBar");
            }
        }
        public Cell _Right { get; set; }
        public Cell _Left { get; set; }
        public Cell _Up { get; set; }
        public Cell _Down { get; set; }
        
        private int? _barNum;
        public int? BarNum
        {
            get { return _barNum; }
            set
            {
                _barNum = value;
                OnPropertyChanged("BarNum");
                if (value == 2) ColorBar = "#FFEEE4DA";
                else if (value == 4)
                {
                    TotalScore.ValueScore.Score += 4;
                    ColorBar = "#FFEEE1C9";
                }
                else if (value == 8) ColorBar = "#FFF3B27A";
                else if (value == 16) ColorBar = "#FFF69664";
                else if (value == 32) ColorBar = "#FFF77C5F";
                else if (value == 64) ColorBar = "#FFF75F3B";
                else if (value == 128) ColorBar = "#FFEDD073";
                else if (value == 256) ColorBar = "#FFEDE873";
                else if (value == 512) ColorBar = "#FF73C9ED";
                else if (value == 1048) ColorBar = "#FF8D34EA";
                else if (value == 2048) ColorBar = "#FF2C0FFF";
                else if (value == null) ColorBar = "#FFCDC1B4";
             
            }
        }
        public void Right()
        {
            Action(_Left);
        }
        public void Left()
        {
            Action(_Right);
        } 
        public void Up()
        {
            Action(_Down);
        }
        public void Down()
        {
            Action(_Up);
        }

        public void Action (Cell cell) 
        {
            if (cell != null)
            {
                if (BarNum != null)
                {
                    if (BarNum == cell.BarNum)
                    {
                        BarNum += cell.BarNum;
                        cell.BarNum = null;
                    }
                }
                else
                {
                   BarNum= cell.BarNum;
                    cell.BarNum = null;
                }
            }
        }
    }
}
