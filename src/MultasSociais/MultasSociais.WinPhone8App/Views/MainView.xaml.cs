using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.Phone.Controls;
using MultasSociais.Lib.Models;

namespace MultasSociais.WinPhone8App.Views
{
    public partial class MainView
    {
        public MainView()
        {
            InitializeComponent();
        }

        private const int carregarQuandoFaltaremXMultas = 20;
        private Dictionary<object,int> realized = new Dictionary<object, int>();
        private void LongListSelector_OnItemRealized(object sender, ItemRealizationEventArgs e)
        {
            if (((dynamic) DataContext).IsLoading) return;
            var longListSelector = (LongListSelector) sender;
            var multas = (ObservableCollection<Multa>)longListSelector.ItemsSource;
            if (multas == null) return;
            if (!realized.ContainsKey(longListSelector)) realized.Add(longListSelector, -1);
            realized[longListSelector]++;
            var numeroDeMultasCarregadas = multas.Count;
            //Debug.WriteLine("Realized: " + realized[longListSelector]);
            if (numeroDeMultasCarregadas < carregarQuandoFaltaremXMultas)
            {//se tem menos itens do que o minimo pra paginar, então acabaram os itens, retorna
                return;
            }
            var multaAtual = (Multa) e.Container.Content;
            var posicaoDaMultaCarregadaAgora = multas.IndexOf(multaAtual);
            Debug.WriteLine("posicao sendo carregada: {0}, numero de multas carregas: {1}", posicaoDaMultaCarregadaAgora, numeroDeMultasCarregadas);
            if (numeroDeMultasCarregadas - posicaoDaMultaCarregadaAgora < carregarQuandoFaltaremXMultas)
            {
                var grupoDeMultas = (GrupoDeMultas)longListSelector.DataContext;
                ((dynamic)DataContext).CarregarMultas(grupoDeMultas);
            }
        }
    }
}