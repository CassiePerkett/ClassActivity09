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

namespace App2.Resources.Model
{
    public class ViewHolder: Java.Lang.Object
    {
        public TextView txtName { get; set; }
    }
    public class ListViewAdapter: BaseAdapter
    {
        private Activity activity;
        private List<Shopping> listShopping;
        public ListViewAdapter(Activity activity, List<Shopping> listShopping)
        {
            this.activity = activity;
            this.listShopping = listShopping;
        }
        public override int Count
        {
            get { return listShopping.Count; }
        }
        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }
        public override long GetItemId(int position)
        {
            return listShopping[position].Id;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.ShoppingList, parent, false);
            var txtName = view.FindViewById<TextView>(Resource.Id.txtView_Name);     
            txtName.Text = listShopping[position].Name;         
            return view;
        }
    }
}