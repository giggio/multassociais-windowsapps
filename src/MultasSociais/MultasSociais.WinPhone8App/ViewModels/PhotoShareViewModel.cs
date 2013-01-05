using MultasSociais.Lib.Models;

namespace MultasSociais.WinPhone8App.ViewModels
{
    public class PhotoShareViewModel : BaseScreen
    {
        private readonly ITalao talao;
        private string fileId;

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
    }
}
