using System.Collections.Generic;
using System.Threading.Tasks;
using MultasSociais.Lib.Models;
using System.Linq;
using WinRtUtility;

namespace MultasSociais.WinStoreApp.Models
{
    public class MultasRealizadas : IMultasRealizadas
    {
        private static readonly ObjectStorageHelper<ListaDeMultasRealizadas> objectStorageHelper = new ObjectStorageHelper<ListaDeMultasRealizadas>(StorageType.Roaming);
        private static ListaDeMultasRealizadas multasRealizadas;
        public static async Task Inicializar()
        {
            if (multasRealizadas == null)
            {
                multasRealizadas = await objectStorageHelper.LoadAsync() ?? new ListaDeMultasRealizadas();
            }
        }

        public async Task Adicionar(MultaRealizada multaRealizada)
        {
            multasRealizadas.Add(multaRealizada);
            await objectStorageHelper.SaveAsync(multasRealizadas);
        }
        public bool FoiMultado(Multa multa)
        {
            var foiMultado = multasRealizadas.Count(m => m.Id == multa.Id) > 0;
            return foiMultado;
        }
    }

    public interface IMultasRealizadas
    {
        Task Adicionar(MultaRealizada multaRealizada);
        bool FoiMultado(Multa multa);
    }

    /// <remarks>
    /// Esse tipo só existe porque o <see cref="ObjectStorageHelper{T}"/> usa o nome do tipo completo para salvar, e isso pode gerar paths muitos compridos, o que estoura o limite do windows. Com uma classe não genérica o path fica menor.
    /// </remarks>
    public class ListaDeMultasRealizadas : List<MultaRealizada> { }
}
