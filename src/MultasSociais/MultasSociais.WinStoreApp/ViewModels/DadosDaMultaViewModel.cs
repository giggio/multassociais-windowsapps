using System;
using System.Threading.Tasks;
using Caliburn.Micro;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace MultasSociais.WinStoreApp.ViewModels
{
    public class DadosDaMultaViewModel : Screen
    {
        public bool IsValid { get { return videoUrlIsValid && descricaoIsValid && dataOcorrenciaIsValid; } }
        private DateTime dataOcorrencia = DateTime.Now;
        public DateTime DataOcorrencia { get { return dataOcorrencia; } set { dataOcorrencia = value; NotifyOfPropertyChange(); } }
        private string descricao;
        public string Descricao { get { return descricao; } set { descricao = value; NotifyOfPropertyChange(); } }
        private string placa;
        public string Placa { get { return placa; } set { placa = value; NotifyOfPropertyChange(); } }
        private string videoUrl;
        public string VideoUrl { get { return videoUrl; } set { videoUrl = value; NotifyOfPropertyChange(); } }
        private bool showImage;
        public bool ShowImage { get { return showImage; } set { showImage = value; NotifyOfPropertyChange(); } }
        private BitmapImage image;
        public BitmapImage Image { get { return image; } set { image = value; NotifyOfPropertyChange(); } }
        private bool videoUrlIsValid = true;
        public bool VideoUrlIsValid { get { return videoUrlIsValid; } set { videoUrlIsValid = value; NotifyOfPropertyChange("IsValid"); } }
        private bool descricaoIsValid;
        public bool DescricaoIsValid { get { return descricaoIsValid; } set { descricaoIsValid = value; NotifyOfPropertyChange("IsValid"); } }
        private bool dataOcorrenciaIsValid = true;
        public bool DataOcorrenciaIsValid { get { return dataOcorrenciaIsValid; } set { dataOcorrenciaIsValid = value; NotifyOfPropertyChange("IsValid"); } }
        private bool isEnabled = true;
        public bool IsEnabled { get { return isEnabled; } set { isEnabled = value; NotifyOfPropertyChange(); } }

        public async Task ExibirImagem(IRandomAccessStream stream, bool forcar = false)
        {
            if (!forcar && Image != null) return;
            Image = new BitmapImage();
            await Image.SetSourceAsync(stream);
            ShowImage = true;
        }
    }
}
