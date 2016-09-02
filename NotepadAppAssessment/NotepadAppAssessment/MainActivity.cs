using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.IO;

namespace NotepadAppAssessment
{
    [Activity(Label = "NotepadAppAssessment")]
    public class MainActivity : Activity
    {
        DatabaseManager objDM;

        EditText txtSearch;

        Button btnAdd;

        ListView lvListNotes;
        List<Notes> myList;

        static string dbName = "notepadapp.sqlite";
        string dbPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), dbName);

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            CopyDatabase();

            txtSearch = FindViewById<EditText>(Resource.Id.txtSearch);
            txtSearch.TextChanged += TxtSearch_TextChanged;

            btnAdd = FindViewById<Button>(Resource.Id.btnAdd);
            btnAdd.Click += BtnAdd_Click;

            objDM = new DatabaseManager();
            myList = objDM.ViewAll();

            lvListNotes = FindViewById<ListView>(Resource.Id.lvListNotes);
            lvListNotes.Adapter = new DataAdapter(this, myList);
            lvListNotes.ItemClick += LvListNotes_ItemClick;   
           
                                                               
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {           
            StartActivity(typeof(AddNote));
        }

        private void TxtSearch_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (txtSearch.Text == "")
            {
                objDM = new DatabaseManager();
                myList = objDM.ViewAll();
                lvListNotes.Adapter = new DataAdapter(this, myList);
            }
            if (txtSearch.Text != "")
            {
                objDM = new DatabaseManager();
                myList = objDM.SearchNotes(txtSearch.Text);
                lvListNotes.Adapter = new DataAdapter(this, myList);
            }
        }

        private void LvListNotes_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var ListItem = myList[e.Position];
            var edititem = new Intent(this, typeof(EditItem));

            edititem.PutExtra("NoteID", ListItem.NoteID);
            edititem.PutExtra("Title", ListItem.Title);
            edititem.PutExtra("Details", ListItem.Details);
                        
            StartActivity(edititem);
        }

        public void CopyDatabase()
        {
            // check if your db has already been extracted
            if (!File.Exists(dbPath))
            {
                using (BinaryReader br = new BinaryReader(Assets.Open(dbName)))
                {
                    using (BinaryWriter bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
                    {
                        byte[] buffer = new byte[2048];
                        int len = 0;
                        while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            bw.Write(buffer, 0, len);
                        }
                    }
                }
            }
        }
    }
}

