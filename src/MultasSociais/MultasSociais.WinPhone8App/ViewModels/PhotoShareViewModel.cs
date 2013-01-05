using MultasSociais.Lib.Models;

namespace MultasSociais.WinPhone8App.ViewModels
{
    public class PhotoShareViewModel : BaseScreen
    {
        private readonly ITalao talao;

        public PhotoShareViewModel(ITalao talao)
        {
            this.talao = talao;
        }
    }
}
