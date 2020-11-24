using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HCS.Services
{
	public interface IDataStore<TKey ,TItem, TItems, TTariffs>
	{
		Task<string> UpdateItemAsync(TItem item);
		Task<TItem> GetItemAsync(TKey id);
		Task<TItems> GetItemsAsync(bool forceRefresh = false);
		Task<TTariffs> GetTariffsAsync(bool forceRefresh = false);
		Task<bool> CalculateTariffAsync(TItem item);
		Task<string> UpdateTariffsAsync();
	}
}
