using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MultasSociais.Lib.Models
{
    public interface ITalao
    {
        Task<GrupoDeMultas> ObterMaisNovos();
        Task<GrupoDeMultas> ObterMaisMultados();
        Task<Multa> ObterPorId(int id);
    }
    public class Talao : ITalao
    {
       public async Task<GrupoDeMultas> ObterMaisNovos()
       {
           var request = WebRequest.CreateHttp("http://multassociais.net/multas.json");
           var response = await Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, null);
           var responseContent = await new StreamReader(response.GetResponseStream()).ReadToEndAsync();
           var multas = JsonConvert.DeserializeObject<IEnumerable<Multa>>(responseContent);
           var grupo = new GrupoDeMultas {Itens = multas, TipoGrupo = TipoGrupo.MaisNovos, Nome = "Mais novos"};
           foreach (var multa in multas)
           {
               multa.Grupo = grupo;
           } 
           return grupo;
       }
       public async Task<GrupoDeMultas> ObterMaisMultados()
       {
           var request = WebRequest.CreateHttp("http://multassociais.net/multas.json");
           var response = await Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, null);
           var responseContent = await new StreamReader(response.GetResponseStream()).ReadToEndAsync();
           var multas = JsonConvert.DeserializeObject<IEnumerable<Multa>>(responseContent);
           var grupo = new GrupoDeMultas { Itens = multas, TipoGrupo = TipoGrupo.MaisMultados, Nome = "Mais multados" };
           foreach (var multa in multas)
           {
               multa.Grupo = grupo;
           } 
           return grupo;
       }

        public async Task<Multa> ObterPorId(int id)
        {
            var request = WebRequest.CreateHttp(string.Format("http://multassociais.net/multas/{0}.json", id));
            var response = await Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, null);
            var responseContent = await new StreamReader(response.GetResponseStream()).ReadToEndAsync();
            var multa = JsonConvert.DeserializeObject<Multa>(responseContent);
            return multa;
        }

#if DEBUG

        public static IEnumerable<GrupoDeMultas> Grupos
        {
            get
            {
                return new List<GrupoDeMultas> { MaisNovos, MaisMultados };
            }
        }
        public static GrupoDeMultas MaisNovos { get; set; }
        public static GrupoDeMultas MaisMultados { get; set; }
        static Talao()
        {
            MaisNovos = new GrupoDeMultas
                            {
                                Nome = "Mais novos",
                                TipoGrupo = TipoGrupo.MaisNovos
                            };
            MaisNovos.Itens = new List<Multa>
                      {
                          new Multa{ Id = 1, DataOcorrencia = new DateTime(2012,1,2), NumeroDeMultas = 3, Placa = "ABC 1234", VideoUrl = "", FotoUrl = "http://s3.amazonaws.com/msociais/fotos/15/original/fotoblur.png?1324341452", Grupo = MaisNovos, Descricao = "Mussum ipsum cacilds, vidis litro abertis. Consetis adipiscings elitis. Pra lá , depois divoltis porris, paradis. Paisis, filhis, espiritis santis. Mé faiz elementum girarzis, nisi eros vermeio, in elementis mé pra quem é amistosis quis leo. Manduma pindureta quium dia nois paga. Sapien in monti palavris qui num significa nadis i pareci latim. Interessantiss quisso pudia ce receita de bolis, mais bolis eu num gostis."},
                          new Multa{ Id = 2, DataOcorrencia = new DateTime(2012,3,4), NumeroDeMultas = 5, Placa = "DEF 1234", VideoUrl = "", FotoUrl = "http://s3.amazonaws.com/msociais/fotos/14/original/IMG_8539.JPG?1324307845", Grupo = MaisNovos, Descricao = "Suco de cevadiss, é um leite divinis, qui tem lupuliz, matis, aguis e fermentis. Interagi no mé, cursus quis, vehicula ac nisi. Aenean vel dui dui. Nullam leo erat, aliquet quis tempus a, posuere ut mi. Ut scelerisque neque et turpis posuere pulvinar pellentesque nibh ullamcorper. Pharetra in mattis molestie, volutpat elementum justo. Aenean ut ante turpis. Pellentesque laoreet mé vel lectus scelerisque interdum cursus velit auctor. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam ac mauris lectus, non scelerisque augue. Aenean justo massa."},
                      };
            MaisMultados = new GrupoDeMultas
                               {
                                   Nome = "Mais multados",
                                   TipoGrupo = TipoGrupo.MaisMultados
                               };
            MaisMultados.Itens = new List<Multa>
                      {
                          new Multa{ Id = 3, DataOcorrencia = new DateTime(2012,1,2), NumeroDeMultas = 3, Placa = "ABC 1234", VideoUrl = "", FotoUrl = "http://s3.amazonaws.com/msociais/fotos/15/original/fotoblur.png?1324341452", Grupo = MaisMultados, Descricao = "Mussum ipsum cacilds, vidis litro abertis. Consetis adipiscings elitis. Pra lá , depois divoltis porris, paradis. Paisis, filhis, espiritis santis. Mé faiz elementum girarzis, nisi eros vermeio, in elementis mé pra quem é amistosis quis leo. Manduma pindureta quium dia nois paga. Sapien in monti palavris qui num significa nadis i pareci latim. Interessantiss quisso pudia ce receita de bolis, mais bolis eu num gostis."},
                          new Multa{ Id = 4, DataOcorrencia = new DateTime(2012,3,4), NumeroDeMultas = 5, Placa = "DEF 1234", VideoUrl = "", FotoUrl = "http://s3.amazonaws.com/msociais/fotos/14/original/IMG_8539.JPG?1324307845", Grupo = MaisMultados, Descricao = "Suco de cevadiss, é um leite divinis, qui tem lupuliz, matis, aguis e fermentis. Interagi no mé, cursus quis, vehicula ac nisi. Aenean vel dui dui. Nullam leo erat, aliquet quis tempus a, posuere ut mi. Ut scelerisque neque et turpis posuere pulvinar pellentesque nibh ullamcorper. Pharetra in mattis molestie, volutpat elementum justo. Aenean ut ante turpis. Pellentesque laoreet mé vel lectus scelerisque interdum cursus velit auctor. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam ac mauris lectus, non scelerisque augue. Aenean justo massa."},
                      };
        }
#endif
    }


}
