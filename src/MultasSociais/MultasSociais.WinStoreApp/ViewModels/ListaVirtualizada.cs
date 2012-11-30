using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace MultasSociais.WinStoreApp.ViewModels
{
    public class ListaVirtualizada<T> : ObservableCollection<T>, ISupportIncrementalLoading
    {
        private readonly Func<uint, Task<IEnumerable<T>>> obterMaisItens;
        private bool hasMoreItems = true;

        public ListaVirtualizada(ObservableCollection<T> colecaoOriginal, Func<uint, Task<IEnumerable<T>>> obterMaisItens) : base(colecaoOriginal)
        {
            this.obterMaisItens = obterMaisItens;
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return AsyncInfo.Run(c => LoadMoreItemsAsync(c, count));
        }

        private async Task<LoadMoreItemsResult> LoadMoreItemsAsync(CancellationToken cancellationToken, uint count)
        {
            if (obterMaisItens == null) return new LoadMoreItemsResult{Count = 0};

            var itensObtidos = (await obterMaisItens(count)).ToArray();
            foreach (var item in itensObtidos)
            {
                Add(item);
            }
            hasMoreItems = itensObtidos.Length > count;
            return new LoadMoreItemsResult { Count = (uint)itensObtidos.Length };
        }
        public bool HasMoreItems
        {
            get { return hasMoreItems; }
        }
    }
}