using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MultasSociais.Lib.Models
{
    public interface ITalao
    {
        Task<GrupoDeMultas> ObterMaisNovos();
        Task<GrupoDeMultas> ObterMaisMultados();
        Task<Multa> ObterPorId(int id);
        Task<IEnumerable<Multa>> PegarMaisMultas(GrupoDeMultas grupo, int iniciarEm, uint quantidade);
        Task<bool> MarcarMultaAsync(Multa multa);
        Task<bool> MultarAsync(MultaNova multa);
    }

    public partial class Talao : ITalao
    {
        private static GrupoDeMultas maisNovos;
        private static GrupoDeMultas maisMultados;
        private const string urlBase = "http://multassociais.net/";
        private const string urlBaseTeste = "http://testes.multassociais.net/";
        const string urlMaisNovos = "multas.json";
        const string urlMaisNovosComPaginacao = "multas.json?p={0}";
        const string urlMaisMultados = "mais_multados.json";
        const string urlMaisMultadosComPaginacao = "mais_multados.json?p={0}";
        const string urlMulta = "multas/{0}.json";
        const string urlMarcarMulta = "multas/{0}/like";
        public async Task<GrupoDeMultas> ObterMaisNovos()
        {
            return maisNovos ?? (maisNovos = await ObterGrupo(ObterUrl(urlMaisNovos), TipoGrupo.MaisNovos));
        }

        private string ObterUrl(string url)
        {
#if DEBUG
            return urlBaseTeste + url;
#else
            return urlBase + url;
#endif
        }

        public async Task<GrupoDeMultas> ObterMaisMultados()
        {
            return maisMultados ?? (maisMultados = await ObterGrupo(ObterUrl(urlMaisMultados), TipoGrupo.MaisMultados));
        }

        public async Task<GrupoDeMultas> ObterGrupo(string url, TipoGrupo tipoGrupo)
        {
            var grupo = new GrupoDeMultas(tipoGrupo);
            var multas = await url.Obter<IEnumerable<Multa>>();
            //foreach (var multa in multas)
            //{
            //    multa.Descricao =
            //        "Mussum ipsum cacilds, vidis litro abertis. Consetis adipiscings elitis. Pra lá , depois divoltis porris, paradis. Paisis, filhis, espiritis santis. Mé faiz elementum girarzis, nisi eros vermeio, in elementis mé pra quem é amistosis quis leo. Manduma pindureta quium dia nois paga. Sapien in monti palavris qui num significa nadis i pareci latim. Interessantiss quisso pudia ce receita de bolis, mais bolis eu num gostis.\nSuco de cevadiss, é um leite divinis, qui tem lupuliz, matis, aguis e fermentis. Interagi no mé, cursus quis, vehicula ac nisi. Aenean vel dui dui. Nullam leo erat, aliquet quis tempus a, posuere ut mi. Ut scelerisque neque et turpis posuere pulvinar pellentesque nibh ullamcorper. Pharetra in mattis molestie, volutpat elementum justo. Aenean ut ante turpis. Pellentesque laoreet mé vel lectus scelerisque interdum cursus velit auctor. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam ac mauris lectus, non scelerisque augue. Aenean justo massa.\nCasamentiss faiz malandris se pirulitá, Nam liber tempor cum soluta nobis eleifend option congue nihil imperdiet doming id quod mazim placerat facer possim assum. Lorem ipsum dolor sit amet, consectetuer Ispecialista im mé intende tudis nuam golada, vinho, uiski, carirí, rum da jamaikis, só num pode ser mijis. Adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat.\nCevadis im ampola pa arma uma pindureta. Nam varius eleifend orci, sed viverra nisl condimentum ut. Donec eget justis enim. Atirei o pau no gatis. Viva Forevis aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Copo furadis é disculpa de babadis, arcu quam euismod magna, bibendum egestas augue arcu ut est. Delegadis gente finis. In sit amet mattis porris, paradis. Paisis, filhis, espiritis santis. Mé faiz elementum girarzis. Pellentesque viverra accumsan ipsum elementum gravidis.\nForevis aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Copo furadis é disculpa de babadis, arcu quam euismod magna, bibendum egestas augue arcu ut est. Etiam ultricies tincidunt ligula, sed accumsan sapien mollis et. Delegadis gente finis. In sit amet mattis porris, paradis. Paisis, filhis, espiritis santis. Mé faiz elementum girarzis. Pellentesque viverra accumsan ipsum elementum gravida. Quisque vitae metus id massa tincidunt iaculis sed sed purus. Vestibulum viverra lobortis faucibus. Vestibulum et turpis.\nVitis e adipiscing enim. Nam varius eleifend orci, sed viverra nisl condimentum ut. Donec eget justo enim. Atirei o pau no gatis. Quisque dignissim felis quis sapien ullamcorper varius tempor sem varius. Vivamus lobortis posuere facilisis. Sed auctor eros ac sapien sagittis accumsan. Integer semper accumsan arcu, at aliquam nisl sollicitudin non. Nullam pellentesque metus nec libero laoreet vitae vestibulum ante ultricies. Phasellus non mollis purus. Integer vel lacus dolor. Proin eget mi nec mauris convallis ullamcorper vel ac nulla. Nulla et semper metus.";
            //}
            grupo.Add(multas);
            return grupo;
        }

        public async Task<IEnumerable<Multa>> PegarMaisMultas(GrupoDeMultas grupo, int iniciarEm, uint quantidade)
        {
            var numeroDePaginas = Math.Ceiling(Convert.ToDecimal(quantidade)/10);
            var urlBase = ObterUrl(grupo.TipoGrupo == TipoGrupo.MaisNovos ? urlMaisNovosComPaginacao : urlMaisMultadosComPaginacao);
            var novasMultas = new List<Multa>();
            for (int i = 0; i < numeroDePaginas; i++)
            {
                var urlConsulta = string.Format(urlBase, iniciarEm + (i*10));
                var multas = (await urlConsulta.Obter<IEnumerable<Multa>>()).ToArray();
                novasMultas.AddRange(multas);
            }
            grupo.Add(novasMultas);
            return novasMultas;
        }

        public async Task<bool> MarcarMultaAsync(Multa multa)
        {
            var urlMarcarMultaComId = string.Format(urlMarcarMulta, multa.Id);
            var statusCode = await ObterUrl(urlMarcarMultaComId).Postar();
            return statusCode == HttpStatusCode.OK;
        }

        public async Task<bool> MultarAsync(MultaNova multa)
        {
            //todo: implementar
            await Task.Delay(2000);
            return true;
        }

        public async Task<Multa> ObterPorId(int id)
        {
            var multa = await ObterUrl(urlMulta).Obter<Multa>();
            return multa;
        }
    }
}