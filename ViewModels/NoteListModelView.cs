using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProPad.ViewModels
{
    internal class NoteListModelView
    {
        public ObservableCollection<Note> notes { get; set; }
        public NoteListModelView()
        {
            notes = new ObservableCollection<Note>();
        }



    }
}
