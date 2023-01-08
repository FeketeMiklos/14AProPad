using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ProPad
{
    [Table("notes")]
    public class Note
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsCoded { get; set; } // adj majd értéket neki
        public string Password { get; set; }

        public Note()
        {

        }

        public Note(string title, string text, bool isSaved)
        {
            Title = title;
            Text = text;
            //IsCoded = isCoded;
        }
    }
}
