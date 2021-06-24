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
    [Activity(Label = "SearchCoachesActivity")]
    public class SearchCoachesActivity : AppCompatActivity
    {
        private EditText keyWord;
        private ListView results;
        
        private ISearchCoachesPresenter _presenter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_search);
            _presenter = new SearchCoachesPresenter(this);
            ConnectViews();
        }

        public ArrayAdapter<string> ResultsAdapter { get; private set; }

        private void ConnectViews()
        {
            keyWord = FindViewById<EditText>(Resource.Id.keyWord);
            results = FindViewById<ListView>(Resource.Id.results);
            results.ItemClick += Results_ItemClick;

            RefreshAdapter();

            keyWord.TextChanged += KeyWord_TextChanged;
        }

        private async void KeyWord_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            _presenter.KeyWord = keyWord.Text;

            await _presenter.GetCoachesAsync();

            RefreshAdapter();
        }

        private void RefreshAdapter()
        {
            ResultsAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, _presenter.CoachesUserNames);
            results.Adapter = ResultsAdapter;
            
        }

        private void Results_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            _presenter.VisitCoachProfile(e.Position);
        }
    }
}