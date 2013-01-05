using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using MultasSociais.WinPhone8App.Infra;

namespace MultasSociais.WinPhone8App.Models
{
    public class MultasRealizadas : IMultasRealizadas
    {
        private readonly IObjectStorageHelper<ListaDeMultasRealizadas> objectStorageHelper;
        private static ListaDeMultasRealizadas multasRealizadas;

        public MultasRealizadas(IObjectStorageHelper<ListaDeMultasRealizadas> objectStorageHelper)
        {
            this.objectStorageHelper = objectStorageHelper;
            if (multasRealizadas == null)
            {
                var loadedTask = objectStorageHelper.LoadAsync();
                loadedTask.ContinueWith(task =>
                    {
                        multasRealizadas = task.Result ?? new ListaDeMultasRealizadas();
                    });
            }
        }

        public async Task Adicionar(MultaRealizada multaRealizada)
        {
            multasRealizadas.Add(multaRealizada);
            await objectStorageHelper.SaveAsync(multasRealizadas);
        }

        public bool FoiMultado(int multaId)
        {
            var foiMultado = multasRealizadas.Count(m => m.Id == multaId) > 0;
            return foiMultado;
        }
    }
    
    public interface IMultasRealizadas
    {
        Task Adicionar(MultaRealizada multaRealizada);
        bool FoiMultado(int multaId);
    }

    public class MultaRealizada
    {
        public int Id { get; set; }
    }

    /// <remarks>
    /// Esse tipo só existe porque o <see cref="ObjectStorageHelper{T}"/> usa o nome do tipo completo para salvar, e isso pode gerar paths muitos compridos, o que estoura o limite do windows. Com uma classe não genérica o path fica menor.
    /// </remarks>
    public class ListaDeMultasRealizadas : List<MultaRealizada> { }
}