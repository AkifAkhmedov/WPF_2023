
using System;

namespace Task01.Models
{
    internal class MainModel : Changed
    {
        private string _ReadName;
        public string ReadName
        {
            get { return _ReadName; }
            set
            {
                _ReadName = value;
                OnPropertyChanged("ReadName");
            }
        }

        private DateTime _dateTime { get; set; }
        public DateTime DateTime
        {
            get { return _dateTime; }
            set
            {
                _dateTime = value;
                OnPropertyChanged("DateTime");
            }
        }



    }
}
