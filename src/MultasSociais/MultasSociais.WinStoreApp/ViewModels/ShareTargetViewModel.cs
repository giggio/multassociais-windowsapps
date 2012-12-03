using System;
using System.Linq;
using System.Threading.Tasks;
using Caliburn.Micro;
using MultasSociais.Lib.Models;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.DataTransfer.ShareTarget;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace MultasSociais.WinStoreApp.ViewModels
{
    public class ShareTargetViewModel : ViewModelBase
    {
        public static ShareOperation ShareOperation { get; set; }

        public ShareTargetViewModel(INavigationService navigationService, ITalao talao) : base(navigationService, talao) {}

        protected async override void OnInitialize()
        {
            base.OnInitialize();

            var shareProperties = ShareOperation.Data.Properties;
            if (shareProperties.Thumbnail != null)
            {
                var stream = await shareProperties.Thumbnail.OpenReadAsync();
                Image = new BitmapImage();
                await Image.SetSourceAsync(stream);
                ShowImage = true;
            }

            if (ShareOperation.Data.Contains(StandardDataFormats.Bitmap))
            {
                var sharedBitmap = await ShareOperation.Data.GetBitmapAsync();
                Image = new BitmapImage();
                await Image.SetSourceAsync(await sharedBitmap.OpenReadAsync());
                ShowImage = true;
            }
            
            if (ShareOperation.Data.Contains(StandardDataFormats.StorageItems))
            {
                var storageItems = await ShareOperation.Data.GetStorageItemsAsync();
                StorageFile storageItem = null;
                if (storageItems.Count > 0)
                    storageItem = (StorageFile) storageItems.First();
                if (storageItem != null)
                {
                    Image = new BitmapImage();
                    await Image.SetSourceAsync(await storageItem.OpenReadAsync());
                    ShowImage = true;
                }
            }
        }

        public async Task Share()
        {
            Sharing = true;
            ShareOperation.ReportStarted();
            var multa = new MultaNova
                            {
                                DataOcorrencia = dataOcorrencia,
                                Descricao = descricao,
                                Placa = placa,
                                VideoUrl = videoUrl
                            };
            MultadoComSucesso = await talao.MultarAsync(multa);
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

        private DateTime dataOcorrencia;
        public DateTime DataOcorrencia { get { return dataOcorrencia; } set { dataOcorrencia = value; NotifyOfPropertyChange(); } }
        private string descricao;
        public string Descricao { get { return descricao; } set { descricao = value; NotifyOfPropertyChange(); } }
        private string placa;
        public string Placa { get { return placa; } set { placa = value; NotifyOfPropertyChange(); } }
        private string videoUrl;
        public string VideoUrl { get { return videoUrl; } set { videoUrl = value; NotifyOfPropertyChange(); } }
        private bool multadoComSucesso;
        public bool MultadoComSucesso { get { return multadoComSucesso; } set { multadoComSucesso = value; NotifyOfPropertyChange(); } }
        private bool sharing;
        public bool Sharing { get { return sharing; } set { sharing = value; NotifyOfPropertyChange(); } }
        private bool showImage;
        public bool ShowImage { get { return showImage; } set { showImage = value; NotifyOfPropertyChange(); } }
        private BitmapImage image;
        public BitmapImage Image { get { return image; } set { image = value; NotifyOfPropertyChange(); } }
    }
}