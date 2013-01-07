using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Microsoft.Xna.Framework.Media;
using MultasSociais.Lib;
using MultasSociais.Lib.Models;

namespace MultasSociais.WinPhone8App.ViewModels
{
    public class PhotoShareViewModel : BaseScreen
    {
        private readonly INavigationService navigationService;
        private readonly ITalao talao;
        private string fileId;
        private HttpUpload.FileInfo fileInfo;

        public PhotoShareViewModel(INavigationService navigationService, ITalao talao)
        {
            this.navigationService = navigationService;
            this.talao = talao;
        }

        public string FileId
        {
            get { return fileId; }
            set
            {
                if (value == fileId) return;
                fileId = value;
                NotifyOfPropertyChange();
            }
        }

        protected override void OnActivate()
        {
            var library = new MediaLibrary();
            var photoFromLibrary = library.GetPictureFromToken(FileId);

            var image = photoFromLibrary.GetImage();
            var buffer = LerTudo(image);
            dadosDaMulta.ExibirImagem(image, true);
            
            fileInfo = new HttpUpload.FileInfo
            {
                FileName = photoFromLibrary.Name,
                ContentType = "image/jpeg",
                Buffer = buffer,
                ParamName = "multa[foto]"
            };

            dadosDaMulta.PropertyChanged += (sender, e) => { if (e.PropertyName == "IsValid") NotifyOfPropertyChange("CanShare"); };

            base.OnActivate();
        }

        public static byte[] LerTudo(Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                var buffer = new byte[8192];
                int bytesRead;
                while ((bytesRead = stream.Read(buffer, 0, 8192)) > 0)
                {
                    memoryStream.Write(buffer, 0, bytesRead);
                }
                return memoryStream.ToArray();
            }
        }

        public bool CanShare
        {
            get
            {
                return dadosDaMulta.IsValid && !sharing;
            }
        }

        public async Task Share()
        {
            Sharing = true;
            var multa = new CriarMultaNova
            {
                Descricao = dadosDaMulta.Descricao,
                Placa = dadosDaMulta.Placa,
                VideoUrl = dadosDaMulta.VideoUrl
            };
            multa.SetaDataOcorrencia(dadosDaMulta.DataOcorrencia);
            try
            {
                MultadoComSucesso = await talao.MultarAsync(multa, fileInfo);
                if (MultadoComSucesso)
                {
                    Sharing = false;
                    //todo:remover messageboxes pq é o lugar errado (viewmodel)
                    MessageBox.Show("Multado com sucesso!");
                    navigationService.UriFor<MainViewModel>().Navigate();
                }
                else
                {
                    MessageBox.Show("Não foi possível multar, favor tentar mais tarde.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível multar, ocorreu um erro, favor tentar mais tarde.\nErro:" + ex.Message);
            }

        }

        private bool sharing;
        public bool Sharing
        {
            get { return sharing; }
            set
            {
                sharing = value;
                dadosDaMulta.IsEnabled = !sharing;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange("CanShare");
            }
        }
        private bool multadoComSucesso;
        public bool MultadoComSucesso { get { return multadoComSucesso; } set { multadoComSucesso = value; NotifyOfPropertyChange(); } }
        private DadosDaMultaViewModel dadosDaMulta = new DadosDaMultaViewModel();
        public DadosDaMultaViewModel DadosDaMulta { get { return dadosDaMulta; } set { dadosDaMulta = value; NotifyOfPropertyChange(); } }
    }
}
