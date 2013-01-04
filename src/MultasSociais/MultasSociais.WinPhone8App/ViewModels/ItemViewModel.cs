using System;
using System.ComponentModel;

namespace MultasSociais.WinPhone8App.ViewModels
{
    public class ItemViewModel : INotifyPropertyChanged
    {
        private string id;
        public string ID
        {
            get { return id; }
            set
            {
                if (value == id) return;
                id = value;
                NotifyPropertyChanged("ID");
            }
        }

        private string lineOne;
        public string LineOne
        {
            get { return lineOne; }
            set
            {
                if (value == lineOne) return;
                lineOne = value;
                NotifyPropertyChanged("LineOne");
            }
        }

        private string lineTwo;
        public string LineTwo
        {
            get { return lineTwo; }
            set
            {
                if (value == lineTwo) return;
                lineTwo = value;
                NotifyPropertyChanged("LineTwo");
            }
        }

        private string lineThree;
        public string LineThree
        {
            get { return lineThree; }
            set
            {
                if (value == lineThree) return;
                lineThree = value;
                NotifyPropertyChanged("LineThree");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}