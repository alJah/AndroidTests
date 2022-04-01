using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AndroidTests
{
    internal class UpdatedView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        public UpdatedView()
        {

        }
    }
}
