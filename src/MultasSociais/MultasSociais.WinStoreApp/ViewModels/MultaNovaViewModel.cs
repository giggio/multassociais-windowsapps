using System;
using System.Threading.Tasks;
using Caliburn.Micro;
using MultasSociais.Lib.Models;
using Windows.Media.Capture;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Media.Imaging;

namespace MultasSociais.WinStoreApp.ViewModels
{
    public class MultaNovaViewModel : ViewModelBase
    {
        private bool multando;
        public MultaNovaViewModel(INavigationService navigationService, ITalao talao) : base(navigationService, talao)
        {
            MontarDadosDaMulta();
        }

        private void MontarDadosDaMulta()
        {
            dadosDaMulta = new DadosDaMultaViewModel();
            dadosDaMulta.PropertyChanged += (s, e) => { if (e.PropertyName == "IsValid") NotifyOfPropertyChange("CanMultar"); };
        }

        public bool CanMultar { get { return dadosDaMulta.IsValid && !multando; } }

        public async Task Multar()
        {
            Multando = true;
            await Task.Delay(5000);
        }

        public void Cancelar()
        {
            GoBack();
        }

        public async Task Fotografar()
        {
            var dialog = new CameraCaptureUI();
            var storageFile = await dialog.CaptureFileAsync(CameraCaptureUIMode.Photo);
            var stream = await storageFile.OpenReadAsync();
            await dadosDaMulta.ExibirImagem(stream, true);

        }
        public async Task EscolherFoto()
        {
            var picker = new FileOpenPicker
                             {
                                 SuggestedStartLocation = PickerLocationId.PicturesLibrary,
                                 ViewMode = PickerViewMode.Thumbnail
                             };

            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            picker.FileTypeFilter.Add(".gif");

            var file = await picker.PickSingleFileAsync();
            if (file == null) return;

            var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
            await dadosDaMulta.ExibirImagem(stream, true);
        }

        public bool Multando
        {
            get { return multando; }
            set
            {
                multando = value;
                dadosDaMulta.IsEnabled = !multando;
                NotifyOfPropertyChange();
            }
        }

        private DadosDaMultaViewModel dadosDaMulta;
        public DadosDaMultaViewModel DadosDaMulta { get { return dadosDaMulta; } set { dadosDaMulta = value; NotifyOfPropertyChange(); } }
    }
}
