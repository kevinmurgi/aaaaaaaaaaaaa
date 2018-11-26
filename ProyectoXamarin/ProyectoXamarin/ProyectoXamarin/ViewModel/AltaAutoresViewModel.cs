using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ProyectoXamarin.ViewModel
{
    class AltaAutoresViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
