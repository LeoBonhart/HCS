using System;
using System.ComponentModel;
using HCS.Models;
using PropertyChanged;

namespace HCS.ViewModels
{
	public class ItemDetailViewModel : BaseViewModel
	{
		public Item Item { get; set; }

		public ItemDetailViewModel(Item item)
		{
			Title = item.Text;
			Item = new Item(item);
		}
	}
}
