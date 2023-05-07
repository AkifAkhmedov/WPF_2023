using Game2048.Model;

namespace Game2048.Models
{
    public class ValueScore : Changed
    {

        private int _score;
        public int Score
        {
            get { return _score; }
            set
            {
                _score = value;
                OnPropertyChanged("Score");
            }
        }
    }
}
