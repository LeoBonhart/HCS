using System;
using System.Collections.Generic;
using System.Text;

namespace HCS.Models
{
	public enum MenuItemType
	{
		Calculation,
		Setting
	}
	public class HomeMenuItem
	{
		public MenuItemType Id { get; set; }

		public string Title { get; set; }
	}
}
