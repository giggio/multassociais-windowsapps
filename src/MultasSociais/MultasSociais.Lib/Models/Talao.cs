using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultasSociais.Lib.Models
{
    public interface ITalao
    {
        Task<GrupoDeMultas> ObterMaisNovos();
        Task<GrupoDeMultas> ObterMaisMultados();
        Task<Multa> ObterPorId(int id);
    }

    public partial class Talao : ITalao
    {
        private static GrupoDeMultas maisNovos;
        private static GrupoDeMultas maisMultados;
        public async Task<GrupoDeMultas> ObterMaisNovos()
        {
            return maisNovos ?? (maisNovos = await ObterGrupo("http://multassociais.net/multas.json", TipoGrupo.MaisNovos));
        }

        public async Task<GrupoDeMultas> ObterMaisMultados()
        {
            return maisMultados ?? (maisMultados = await ObterGrupo("http://multassociais.net/multas.json", TipoGrupo.MaisMultados));
        }

        public async Task<GrupoDeMultas> ObterGrupo(string url, TipoGrupo tipoGrupo)
        {
            var grupo = new GrupoDeMultas(tipoGrupo);
            var multas = await url.Obter<IEnumerable<Multa>>();
            grupo.Add(multas);
            return grupo;
        }

        public async Task<Multa> ObterPorId(int id)
        {
            var multa = await "http://multassociais.net/multas/{0}.json".Obter<Multa>();
            return multa;
        }
    }
}