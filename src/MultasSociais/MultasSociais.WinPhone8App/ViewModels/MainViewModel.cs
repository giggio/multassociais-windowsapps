using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using Caliburn.Micro;
using MultasSociais.Lib.Models;

namespace MultasSociais.WinPhone8App.ViewModels
{
    public class MainViewModel : BaseScreen
    {
        readonly INavigationService navigationService;
        private readonly ITalao talao;

        public MainViewModel(INavigationService navigationService, ITalao talao)
        {
            this.navigationService = navigationService;
            this.talao = talao;
        }

        protected async override void OnInitialize()
        {
            try
            {
                var grupos = new BindableCollection<GrupoDeMultas> { await talao.ObterMaisNovos(), await talao.ObterMaisMultados() };
                ConfigurarNumeroDeItensAExibir(grupos);
                Grupos = grupos;
                IsLoading = false;
            }
            catch (WebException)
            {
                //todo: remover messagebox
                //todo: colocar botão de reload
                MessageBox.Show("Você está desconectado. Tente novamente mais tarde.");
            }
            base.OnInitialize();
        }

        private void ConfigurarNumeroDeItensAExibir(IEnumerable<GrupoDeMultas> grupos)
        {
            var top = 10;
            foreach (var grupo in grupos)
            {
                grupo.TopCount = top;
            }
        }

        private bool isLoading = true;
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                NotifyOfPropertyChange();
            }
        }

        private BindableCollection<GrupoDeMultas> grupos;
        public BindableCollection<GrupoDeMultas> Grupos
        {
            set
            {
                grupos = value;
                NotifyOfPropertyChange();
            }
            get
            {
                return grupos;
            }
        }

        public void GoToItem(Multa multa)
        {
            navigationService.UriFor<DetailsViewModel>()
                .WithParam(x => x.DataDescrita, multa.DataDescrita)
                .WithParam(x => x.DataOcorrencia, multa.DataOcorrencia)
                .WithParam(x => x.Descricao, multa.Descricao)
                .WithParam(x => x.FotoUrl, multa.FotoUrl)
                .WithParam(x => x.Id, multa.Id)
                .WithParam(x => x.NumeroDeMultas, multa.NumeroDeMultas)
                .WithParam(x => x.NumeroDeMultasDescrita, multa.NumeroDeMultasDescrita)
                .WithParam(x => x.Placa, multa.Placa)
                .WithParam(x => x.VideoUrl, multa.VideoUrl)
                .Navigate();
            
        }
    }
}
