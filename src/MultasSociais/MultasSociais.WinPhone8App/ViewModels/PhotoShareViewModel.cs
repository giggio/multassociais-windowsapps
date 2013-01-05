using System.Windows.Media.Imaging;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Media.PhoneExtensions;
using MultasSociais.Lib.Models;

namespace MultasSociais.WinPhone8App.ViewModels
{
    public class PhotoShareViewModel : BaseScreen
    {
        private readonly ITalao talao;
        private string fileId;
        private BitmapImage imagem;

        public PhotoShareViewModel(ITalao talao)
        {
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

            var bitmapImage = new BitmapImage();
            bitmapImage.SetSource(photoFromLibrary.GetImage());
            Imagem = bitmapImage;
            base.OnActivate();
        }

        public BitmapImage Imagem
        {
            get { return imagem; }
            set
            {
                if (value == imagem) return;
                imagem = value; 
                NotifyOfPropertyChange();
            }
        }
    }
}
