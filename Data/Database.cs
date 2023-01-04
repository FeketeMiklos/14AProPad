using ProPad;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ProPad.Data
{
    public class Database
    {
        readonly SQLiteAsyncConnection connection;

        public Database(string dbPath)
        {
            connection = new SQLiteAsyncConnection(dbPath);
            connection.CreateTableAsync<Note>().Wait();
        }

        public ObservableCollection<Note> GetAllNotes()
        {
            return new ObservableCollection<Note>(connection.Table<Note>().ToListAsync().Result);
        }

        public void SaveNote(Note note)
        {
            connection.InsertAsync(note);
        }

        public bool DeleteNote(Note selected)
        {
            return connection.DeleteAsync(selected).Result == 1;
        }
    }
}
