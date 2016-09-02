using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace NotepadAppAssessment
{
    public class Notes
    {
        [PrimaryKey, AutoIncrement]
        public int NoteID { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime Date { get; set; }

        public Notes()
        {

        }
    }
}