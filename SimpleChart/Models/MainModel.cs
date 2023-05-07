

using SimpleChart.Model;

namespace SimpleChart.Models
{
    internal class MainModel : Changed
    {

        private int _num = 10;
        public int Num
        {
            get { return _num; }
            set
            {
                _num = value;
                OnPropertyChanged("Num");
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
    }
}
