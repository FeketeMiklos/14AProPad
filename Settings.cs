using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProPad
{
    // 1 recordot tárolok belőle a db-ben, a jelenlegi beállításokkal
    // Az ID-ja mindig 1 lesz
    // ha változnak a beállítások, írd felül az 1. recordot
    [Table("settings")]
    public class Settings
    {

        public int FontSize { get; set; }

        public int UIFontSize { get; set; }

        public string FontFamily { get; set; }

        public string TextColor { get; set; }

        public Settings()
        {

        }

        public Settings(int fontSize, int uiFontSize, string fontFamily, string textColor)
        {
            FontSize = fontSize;
            UIFontSize = uiFontSize;
            FontFamily = fontFamily;
            TextColor = textColor;
        }
    }
}
