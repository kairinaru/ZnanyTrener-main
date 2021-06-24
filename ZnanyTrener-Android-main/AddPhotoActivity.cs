using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZnanyTrener_Android.Presenters;

namespace ZnanyTrener_Android
{
    [Activity(Label = "AddPhotoActivity")]
    public class AddPhotoActivity : AppCompatActivity
    {
        private Button addPhotoBtn;
        private IAddPhotoPresenter _presenter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_add_photo);
            addPhotoBtn = FindViewById<Button>(Resource.Id.addPhotoBtn);
            _presenter = new AddPhotoPresenter(this);
            // Create your application here
            addPhotoBtn.Click += async (s, e) => { await _presenter.AddPhotoAsync(); };
        }
    }
}