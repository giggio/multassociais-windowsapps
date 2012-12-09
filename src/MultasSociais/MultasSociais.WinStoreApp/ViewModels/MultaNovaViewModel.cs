using System;
using System.Threading.Tasks;
using Caliburn.Micro;
using MultasSociais.Lib;
using MultasSociais.Lib.Models;
using Windows.Media.Capture;
using Windows.Storage.Pickers;
using Windows.UI.Popups;

namespace MultasSociais.WinStoreApp.ViewModels
{
    public class MultaNovaViewModel : ViewModelBase
    {
        private HttpUpload.FileInfo fileInfo; 
        
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
                    await new MessageDialog("Multado com sucesso!").ShowAsync();
                    GoBack();
                }
                else
                {
                    await new MessageDialog("Não foi possível multar, favor tentar mais tarde.").ShowAsync();
                    Multando = false;
                }
            }
            catch (Exception ex)
            {
                new MessageDialog("Não foi possível multar, ocorreu um erro, favor tentar mais tarde.\nErro:" + ex.Message).ShowAsync();
                Multando = false;
            }
        }

        public void Cancelar()
        {
            GoBack();
        }

        public async Task Fotografar()
        {
            var dialog = new CameraCaptureUI();
            var storageFile = await dialog.CaptureFileAsync(CameraCaptureUIMode.Photo);
            if (storageFile == null) return;
            var stream = await storageFile.OpenReadAsync();
            var streamCloned = stream.CloneStream();
            await dadosDaMulta.ExibirImagem(streamCloned, true);

            fileInfo = new HttpUpload.FileInfo
            {
                FileName = ObterNomeAleatorioDeArquivo(stream.ContentType),
                ContentType = stream.ContentType,
                Buffer = await stream.GetByteFromFileAsync(),
                ParamName = "multa[foto]"
            };
        }
        
        private string ObterNomeAleatorioDeArquivo(string contentType)
        {
            var nomeAleatorio = "arq" + new Random().Next(1000000000, int.MaxValue).ToString();
            var ext = contentType.Split('/')[1];
            var nome = string.Format("{0}.{1}", nomeAleatorio, ext);
            return nome;
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

            var storageFile = await picker.PickSingleFileAsync();
            if (storageFile == null) return;

            var stream = await storageFile.OpenAsync(Windows.Storage.FileAccessMode.Read);
            var streamCloned = stream.CloneStream();
            await dadosDaMulta.ExibirImagem(stream, true);

            fileInfo = new HttpUpload.FileInfo
            {
                FileName = storageFile.Name,
                ContentType = storageFile.ContentType,
                Buffer = await streamCloned.GetByteFromFileAsync(),
                ParamName = "multa[foto]"
            };
        }

        private bool multando;
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
        private bool multadoComSucesso;
        public bool MultadoComSucesso { get { return multadoComSucesso; } set { multadoComSucesso = value; NotifyOfPropertyChange(); } }
    }
}
