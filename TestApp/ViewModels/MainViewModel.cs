using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TestApp.Command;

namespace TestApp.ViewModels
{
    internal class MainViewModel
    {


        private RelayCommand? _authorizeCommand;
        public RelayCommand AuthorizeCommand
        {
            get
            {
                return _authorizeCommand ?? (_authorizeCommand = new RelayCommand(obj => {
                    MessageText();
                }));
            }
        }


       public void MessageText() { MessageBox.Show("dsdfsd"); }
    }
}
