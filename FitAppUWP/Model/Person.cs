using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitAppUWP.Model
{
    public class Person : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _name;

        public string Name
        {
            get { return _name;  }
            set
            {
                if(_name != value)
                {
                    _name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
                }
            }
        }
        private int _age;
        public int Age
        {
            get { return _age; }
            set
            {
                if (_age != value)
                {
                    _age = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Age)));
                }
            }
        }

    }
}
