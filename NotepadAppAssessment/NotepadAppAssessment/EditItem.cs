using System;
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
    [Activity(Label = "EditItem")]
    public class EditItem : Activity
    {
        DatabaseManager objDM;

        int NoteID;
        string Title, Details;

        TextView txtTitle, txtDetails;

        Button btnEdit, btnDelete, btnBack;                     

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here
            SetContentView(Resource.Layout.EditItem);

            txtTitle = FindViewById<TextView>(Resource.Id.txtTitle);
            txtDetails = FindViewById<TextView>(Resource.Id.txtDetails);

            btnEdit = FindViewById<Button>(Resource.Id.btnEdit);
            btnDelete = FindViewById<Button>(Resource.Id.btnDelete);
            btnBack = FindViewById<Button>(Resource.Id.btnBack);

            btnEdit.Click += OnBtnEditClick;
            btnDelete.Click += OnBtnDeleteClick;
            btnBack.Click += BtnBack_Click;

            NoteID = Intent.GetIntExtra("NoteID", 0);
            Title = Intent.GetStringExtra("Title");
            Details = Intent.GetStringExtra("Details");            

            txtTitle.Text = Title;
            txtDetails.Text = Details;

            objDM = new DatabaseManager();
        }                

        public void OnBtnEditClick(object sender, EventArgs e)
        {
            try
            {
                objDM.EditItem(txtTitle.Text, txtDetails.Text, NoteID);
                Toast.MakeText(this, "Note Edited", ToastLength.Long).Show();
                this.Finish();
                StartActivity(typeof(MainActivity));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Occurred:" + ex.Message);
            }
        }

        public void OnBtnDeleteClick(object sender, EventArgs e)
        {
            try
            {
                objDM.DeleteItem(NoteID);
                Toast.MakeText(this, "Note Deleted", ToastLength.Long).Show();
                this.Finish();
                StartActivity(typeof(MainActivity));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Occurred:" + ex.Message);
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Finish();
            StartActivity(typeof(MainActivity));
        }
    }
}