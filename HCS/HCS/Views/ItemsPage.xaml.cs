using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using HCS.Models;
using HCS.Views;
using HCS.ViewModels;
using System.Globalization;

namespace HCS.Views
{
	// Learn more about making custom code visible in the Xamarin.Forms previewer
	// by visiting https://aka.ms/xamarinforms-previewer
	[DesignTimeVisible(false)]
	public partial class ItemsPage : ContentPage
	{
		ItemsViewModel viewModel;
		public ItemsPage()
		{
			InitializeComponent();

			BindingContext = viewModel = new ItemsViewModel();

			if (Application.Current.Properties.ContainsKey("FirstUse"))
			{
				
			}
			else
			{
				Application.Current.Properties["FirstUse"] = false;
				Splash();
			}
		}

		async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
		{
			var item = args.SelectedItem as Item;
			if (item == null)
				return;

			await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

			// Manually deselect item.
			ItemsListView.SelectedItem = null;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (viewModel != null && viewModel.Items.Count == 0)
				viewModel.LoadItemsCommand.Execute(null);
		}

		Image splashImage;
		private async void Splash()
		{
			var mainPage = this.Content;
			NavigationPage.SetHasNavigationBar(this, false);

			var sub = new AbsoluteLayout();
			splashImage = new Image
			{
				Source = "splash_screen_logo.png",
				WidthRequest = 118,
				HeightRequest = 118
			};
			AbsoluteLayout.SetLayoutFlags(splashImage, AbsoluteLayoutFlags.PositionProportional);
			AbsoluteLayout.SetLayoutBounds(splashImage, new Rectangle(0.5, 0.52, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
			sub.Children.Add(splashImage);
			sub.BackgroundColor = Color.FromHex("#429de3");
			this.Content = sub;

			await splashImage.ScaleTo(1, 2000);
			await splashImage.ScaleTo(0.9, 1500, Easing.Linear);
			await splashImage.ScaleTo(15, 100, Easing.Linear);

			NavigationPage.SetHasNavigationBar(this, true);
			this.Content = mainPage;
			viewModel.LoadItemsCommand.Execute(null);
		}

	}

}