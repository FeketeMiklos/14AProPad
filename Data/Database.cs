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
            connection.CreateTableAsync<Settings>().Wait();
            if (connection.Table<Settings>().CountAsync().Result == 0)
            {

                SetSettings(new Settings(12, "Alap", Colors.White.ToHex()));
            }
        }

        public ObservableCollection<Note> GetAllNotes()
        {
            return new ObservableCollection<Note>(connection.Table<Note>().ToListAsync().Result);
        }

        public void SaveNote(Note note)
        {
            if (connection.Table<Note>().Where(n => n.ID == note.ID).CountAsync().Result > 0)
            {
                connection.UpdateAsync(note).Wait();
            }
            else
            {
                connection.InsertAsync(note).Wait();
            }
        }

        public Task<int> DeleteNote(Note note)
        {
            return connection.DeleteAsync(note);
        }

        public Task<int> UpdateNote(Note note)
        {
            return connection.UpdateAsync(note);
        }

        public Note GetNote(int id)
        {
            return connection.FindAsync<Note>(id).Result;
        }


        public Settings GetSettings()
        {
            return connection.FindAsync<Settings>(1).Result;
        }

        public void SetSettings(Settings settings)
        {
            connection.DeleteAllAsync<Settings>().Wait();
            connection.InsertAsync(settings).Wait();
        }
    }
}
