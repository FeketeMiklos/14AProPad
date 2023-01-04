using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProPad.ViewModels
{

    class adatok
    {
        public string Cap { get; private set; }
        public string Fs { get; private set; }
        public bool Secret { get; private set; }

        public adatok()
        {

        }

        public adatok(string cap, string fs, bool secret)
        {
            Cap = cap;
            Fs = fs;
            Secret = secret;
        }
    }

    internal class NoteListModelView
    {
        public ObservableCollection<adatok> caps { get; set; }
        public NoteListModelView()
        {
            caps = new ObservableCollection<adatok>();
        }



    }
}
