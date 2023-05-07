
using Game2048.Model;

namespace Game2048.Models
{
    internal class MainModel : Changed
    {
        private int? _barNum8;
        public int? BarNum8
        {
            get { return _barNum8; }
            set
            {
                _barNum8 = value;
                OnPropertyChanged("BarNum8");
            }
        }
    }
}
