using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using HCS.Models;
using HCS.Services;

namespace HCS.ViewModels
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public IDataStore<ItemsType,Item, Items, Tariffs> DataStore => DependencyService.Get<IDataStore<ItemsType,Item, Items, Tariffs>>();

		public bool IsBusy { get; set; }
		public string Title { get; set; }
		public string Total { get; set; }
 
	}
}
