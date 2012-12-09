using System;
using System.Linq;
using System.Threading.Tasks;
using Caliburn.Micro;
using MultasSociais.Lib;
using MultasSociais.Lib.Models;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.DataTransfer.ShareTarget;
using Windows.Storage;
using Windows.Storage.Streams;

namespace MultasSociais.WinStoreApp.ViewModels
{
    public class ShareTargetViewModel : ViewModelBase
    {
        private HttpUpload.FileInfo fileInfo;
        
        public ShareTargetViewModel(INavigationService navigationService, ITalao talao) : base(navigationService, talao)
        {
            MontarDadosDaMulta();
        }

        private void MontarDadosDaMulta()
        {
            dadosDaMulta = new DadosDaMultaViewModel();
            dadosDaMulta.PropertyChanged += (s, e) => { if (e.PropertyName == "IsValid") NotifyOfPropertyChange("CanShare"); };
        }

        protected async override void OnInitialize()
        {
            base.OnInitialize();

            await TentarObterThumbnail();

            await ObterImagem();
        }

        private async Task TentarObterThumbnail()
        {
            var shareProperties = ShareOperation.Data.Properties;
            if (shareProperties.Thumbnail == null) return;
            var stream = await shareProperties.Thumbnail.OpenReadAsync();
            await dadosDaMulta.ExibirImagem(stream);
        }

        private async Task ObterImagem()
        {
            IRandomAccessStream streamCloned = null;
            if (ShareOperation.Data.Contains(StandardDataFormats.Bitmap))
            {
                var sharedBitmapRandomAccessStreamReference = await ShareOperation.Data.GetBitmapAsync();
                var stream = await sharedBitmapRandomAccessStreamReference.OpenReadAsync();
                streamCloned = stream.CloneStream();
                fileInfo = new HttpUpload.FileInfo
                               {
                                   FileName = ObterNomeAleatorioDeArquivo(stream.ContentType),
                                   ContentType = stream.ContentType,
                                   Buffer = await stream.GetByteFromFileAsync(),
                                   ParamName = "multa[foto]"
                               };
            }
            else if (ShareOperation.Data.Contains(StandardDataFormats.StorageItems))
            {
                var storageItems = await ShareOperation.Data.GetStorageItemsAsync();
                var storageFile = (StorageFile) storageItems.FirstOrDefault();
                if (storageFile == null) return;
                var stream = await storageFile.OpenReadAsync();
                streamCloned = stream.CloneStream();
                fileInfo = new HttpUpload.FileInfo
                               {
                                   FileName = storageFile.Name,
                                   ContentType = stream.ContentType,
                                   Buffer = await stream.GetByteFromFileAsync(),
                                   ParamName = "multa[foto]"
                               };
            }
            await dadosDaMulta.ExibirImagem(streamCloned);
        }

        private string ObterNomeAleatorioDeArquivo(string contentType)
        {
            var nomeAleatorio = "arq" + new Random().Next(1000000000, int.MaxValue).ToString();
            var ext = contentType.Split('/')[1];
            var nome = string.Format("{0}.{1}", nomeAleatorio, ext);
            return nome;
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
            ShareOperation.ReportStarted();
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
                    await Task.Delay(1000);
                    ShareOperation.ReportCompleted();
                }
                else
                {
                    ShareOperation.ReportError("Não foi possível multar, favor tentar mais tarde.");
                }
            }
            catch (Exception ex)
            {
                ShareOperation.ReportError("Não foi possível multar, ocorreu um erro, favor tentar mais tarde.\nErro:" + ex.Message);
            }

        }

        public static ShareOperation ShareOperation { get; set; }
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
        private DadosDaMultaViewModel dadosDaMulta;
        public DadosDaMultaViewModel DadosDaMulta { get { return dadosDaMulta; } set { dadosDaMulta = value; NotifyOfPropertyChange(); } }
    }
}