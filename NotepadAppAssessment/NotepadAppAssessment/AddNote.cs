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
    [Activity(Label = "NotepadAppAssessment")]
    public class AddNote : Activity
    {
        DatabaseManager objDM = new DatabaseManager();

        EditText txtTitle, txtDetails;

        Button btnAdd;        

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here
            SetContentView(Resource.Layout.AddItem);

            btnAdd = FindViewById<Button>(Resource.Id.btnAdd);

            txtTitle = FindViewById<EditText>(Resource.Id.txtTitle);
            txtDetails = FindViewById<EditText>(Resource.Id.txtDetails);

            btnAdd.Click += BtnAdd_Click;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (txtTitle.Text != "" && txtDetails.Text != "")
            {
                objDM.AddItem(txtTitle.Text, txtDetails.Text);
                Toast.MakeText(this, "Note Added", ToastLength.Long).Show();
                this.Finish();
                StartActivity(typeof(MainActivity));
            }
        }
    }
}