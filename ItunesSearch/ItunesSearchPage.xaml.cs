using Xamarin.Forms;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ItunesSearch
{
    public partial class ItunesSearchPage : ContentPage
    {
		private HttpClient _client = new HttpClient();
		private const string URL = "https://itunes.apple.com/search?term=";

		public ItunesSearchPage()
		{
			InitializeComponent();
		}

		protected override async void OnAppearing()
		{
			listView.ItemsSource = await FetchItunesAsync("Prince");
			base.OnAppearing();
		}

		public async Task<List<Itune>> FetchItunesAsync(string term)
		{
			var jsonSerializerSettings = new JsonSerializerSettings();
			jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;

			string response = await _client.GetStringAsync(URL + term);
			var result = JsonConvert.DeserializeObject<Payload>(response, jsonSerializerSettings).Results;

			return result;
		}

		async void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
		{
			listView.ItemsSource = await FetchItunesAsync(e.NewTextValue);
		}
    }
}