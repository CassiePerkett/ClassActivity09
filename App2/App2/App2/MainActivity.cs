using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using App2.Resources.Helper;
using App2.Resources.Model;
using System.Collections.Generic;
using Android.Views;

namespace App2
{
    [Activity(Label = "App2", MainLauncher = true)]
    public class MainActivity : Activity
    {
        ListView lstViewData;
        List<Shopping> listSource = new List<Shopping>();
        Database db;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            //Create Database  
            db = new Database();
            db.createDatabase();
            lstViewData = FindViewById<ListView>(Resource.Id.listView);
            var edtName = FindViewById<EditText>(Resource.Id.edtName);
            var btnAdd = FindViewById<Button>(Resource.Id.btnAdd);
            var btnEdit = FindViewById<Button>(Resource.Id.btnEdit);
            var btnRemove = FindViewById<Button>(Resource.Id.btnRemove);
    
            //Load Data  
            LoadData();
            //Event  
            btnAdd.Click += delegate
            {
                LayoutInflater layoutInflater = LayoutInflater.From(this);
                View view = layoutInflater.Inflate(Resource.Layout.UserInput, null);
                var userdata = view.FindViewById<EditText>(Resource.Id.EN);
                Android.Support.V7.App.AlertDialog.Builder alertbuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
                alertbuilder.SetView(view);
                alertbuilder.SetCancelable(false)
                .SetPositiveButton("Submit", delegate
                {
                    Shopping shopping = new Shopping()
                    {
                        Name = userdata.Text
                    };
                    db.insertIntoTable(shopping);
                    LoadData();
                    Toast.MakeText(this, "Submit Ibput: " + userdata.Text , ToastLength.Short);
                })
                .SetNegativeButton("Cancel", delegate
                {
                    alertbuilder.Dispose();
                });
                Android.Support.V7.App.AlertDialog dialog = alertbuilder.Create();
                dialog.Show();




            };
            btnEdit.Click += delegate
            {
                Shopping shopping = new Shopping()
                {
                    Id = int.Parse(edtName.Tag.ToString()),
                    Name = edtName.Text,                   
                };
                db.updateTable(shopping);
                LoadData();
            };
            btnRemove.Click += delegate
            {
                Shopping shopping = new Shopping()
                {
                    Id = int.Parse(edtName.Tag.ToString()),
                    Name = edtName.Text,                  
                };
                db.removeTable(shopping);
                LoadData();
            };
            lstViewData.ItemClick += (s, e) =>
            {
                //Set Backround for selected item  
                for (int i = 0; i < lstViewData.Count; i++)
                {
                    if (e.Position == i)
                        lstViewData.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Black);
                    else
                        lstViewData.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Transparent);
                }
                //Binding Data  
                var txtName = e.View.FindViewById<TextView>(Resource.Id.txtView_Name);                          
                edtName.Tag = e.Id;            
            };
        }
        private void LoadData()
        {
            listSource = db.selectTable();
            var adapter = new ListViewAdapter(this, listSource);
            lstViewData.Adapter = adapter;
        }
    }
}  
