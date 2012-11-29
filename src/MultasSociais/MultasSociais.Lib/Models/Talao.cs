﻿using System;
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

    public class Talao : ITalao
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
            MaisNovos = new GrupoDeMultas(TipoGrupo.MaisNovos);
            MaisNovos.Add(
                          new Multa{ Id = 1, DataOcorrencia = new DateTime(2012,1,2), NumeroDeMultas = 3, Placa = "ABC 1234", VideoUrl = "", FotoUrl = "http://s3.amazonaws.com/msociais/fotos/15/original/fotoblur.png?1324341452", Grupo = MaisNovos, Descricao = "Mussum ipsum cacilds, vidis litro abertis. Consetis adipiscings elitis. Pra lá , depois divoltis porris, paradis. Paisis, filhis, espiritis santis. Mé faiz elementum girarzis, nisi eros vermeio, in elementis mé pra quem é amistosis quis leo. Manduma pindureta quium dia nois paga. Sapien in monti palavris qui num significa nadis i pareci latim. Interessantiss quisso pudia ce receita de bolis, mais bolis eu num gostis."},
                          new Multa{ Id = 2, DataOcorrencia = new DateTime(2012,3,4), NumeroDeMultas = 5, Placa = "DEF 1234", VideoUrl = "", FotoUrl = "http://s3.amazonaws.com/msociais/fotos/14/original/IMG_8539.JPG?1324307845", Grupo = MaisNovos, Descricao = "Suco de cevadiss, é um leite divinis, qui tem lupuliz, matis, aguis e fermentis. Interagi no mé, cursus quis, vehicula ac nisi. Aenean vel dui dui. Nullam leo erat, aliquet quis tempus a, posuere ut mi. Ut scelerisque neque et turpis posuere pulvinar pellentesque nibh ullamcorper. Pharetra in mattis molestie, volutpat elementum justo. Aenean ut ante turpis. Pellentesque laoreet mé vel lectus scelerisque interdum cursus velit auctor. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam ac mauris lectus, non scelerisque augue. Aenean justo massa."},
                          new Multa{ Id = 3, DataOcorrencia = new DateTime(2012,5,6), NumeroDeMultas = 5, Placa = "DEF 1234", VideoUrl = "", FotoUrl = "http://s3.amazonaws.com/msociais/fotos/249/original/4.jpg?1325644450", Grupo = MaisNovos, Descricao = "Suco de cevadiss, é um leite divinis, qui tem lupuliz, matis, aguis e fermentis. Interagi no mé, cursus quis, vehicula ac nisi. Aenean vel dui dui. Nullam leo erat, aliquet quis tempus a, posuere ut mi. Ut scelerisque neque et turpis posuere pulvinar pellentesque nibh ullamcorper. Pharetra in mattis molestie, volutpat elementum justo. Aenean ut ante turpis. Pellentesque laoreet mé vel lectus scelerisque interdum cursus velit auctor. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam ac mauris lectus, non scelerisque augue. Aenean justo massa."},
                          new Multa{ Id = 4, DataOcorrencia = new DateTime(2012,7,8), NumeroDeMultas = 5, Placa = "DEF 1234", VideoUrl = "", FotoUrl = "http://s3.amazonaws.com/msociais/fotos/18/original/ComLurb.jpg?1324320866", Grupo = MaisNovos, Descricao = "Suco de cevadiss, é um leite divinis, qui tem lupuliz, matis, aguis e fermentis. Interagi no mé, cursus quis, vehicula ac nisi. Aenean vel dui dui. Nullam leo erat, aliquet quis tempus a, posuere ut mi. Ut scelerisque neque et turpis posuere pulvinar pellentesque nibh ullamcorper. Pharetra in mattis molestie, volutpat elementum justo. Aenean ut ante turpis. Pellentesque laoreet mé vel lectus scelerisque interdum cursus velit auctor. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam ac mauris lectus, non scelerisque augue. Aenean justo massa."},
                          new Multa{ Id = 5, DataOcorrencia = new DateTime(2012,1,2), NumeroDeMultas = 3, Placa = "ABC 1234", VideoUrl = "", FotoUrl = "http://s3.amazonaws.com/msociais/fotos/15/original/fotoblur.png?1324341452", Grupo = MaisNovos, Descricao = "Mussum ipsum cacilds, vidis litro abertis. Consetis adipiscings elitis. Pra lá , depois divoltis porris, paradis. Paisis, filhis, espiritis santis. Mé faiz elementum girarzis, nisi eros vermeio, in elementis mé pra quem é amistosis quis leo. Manduma pindureta quium dia nois paga. Sapien in monti palavris qui num significa nadis i pareci latim. Interessantiss quisso pudia ce receita de bolis, mais bolis eu num gostis."},
                          new Multa{ Id = 6, DataOcorrencia = new DateTime(2012,3,4), NumeroDeMultas = 5, Placa = "DEF 1234", VideoUrl = "", FotoUrl = "http://s3.amazonaws.com/msociais/fotos/14/original/IMG_8539.JPG?1324307845", Grupo = MaisNovos, Descricao = "Suco de cevadiss, é um leite divinis, qui tem lupuliz, matis, aguis e fermentis. Interagi no mé, cursus quis, vehicula ac nisi. Aenean vel dui dui. Nullam leo erat, aliquet quis tempus a, posuere ut mi. Ut scelerisque neque et turpis posuere pulvinar pellentesque nibh ullamcorper. Pharetra in mattis molestie, volutpat elementum justo. Aenean ut ante turpis. Pellentesque laoreet mé vel lectus scelerisque interdum cursus velit auctor. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam ac mauris lectus, non scelerisque augue. Aenean justo massa."},
                          new Multa{ Id = 7, DataOcorrencia = new DateTime(2012,5,6), NumeroDeMultas = 5, Placa = "DEF 1234", VideoUrl = "", FotoUrl = "http://s3.amazonaws.com/msociais/fotos/249/original/4.jpg?1325644450", Grupo = MaisNovos, Descricao = "Suco de cevadiss, é um leite divinis, qui tem lupuliz, matis, aguis e fermentis. Interagi no mé, cursus quis, vehicula ac nisi. Aenean vel dui dui. Nullam leo erat, aliquet quis tempus a, posuere ut mi. Ut scelerisque neque et turpis posuere pulvinar pellentesque nibh ullamcorper. Pharetra in mattis molestie, volutpat elementum justo. Aenean ut ante turpis. Pellentesque laoreet mé vel lectus scelerisque interdum cursus velit auctor. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam ac mauris lectus, non scelerisque augue. Aenean justo massa."},
                          new Multa{ Id = 8, DataOcorrencia = new DateTime(2012,7,8), NumeroDeMultas = 5, Placa = "DEF 1234", VideoUrl = "", FotoUrl = "http://s3.amazonaws.com/msociais/fotos/18/original/ComLurb.jpg?1324320866", Grupo = MaisNovos, Descricao = "Suco de cevadiss, é um leite divinis, qui tem lupuliz, matis, aguis e fermentis. Interagi no mé, cursus quis, vehicula ac nisi. Aenean vel dui dui. Nullam leo erat, aliquet quis tempus a, posuere ut mi. Ut scelerisque neque et turpis posuere pulvinar pellentesque nibh ullamcorper. Pharetra in mattis molestie, volutpat elementum justo. Aenean ut ante turpis. Pellentesque laoreet mé vel lectus scelerisque interdum cursus velit auctor. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam ac mauris lectus, non scelerisque augue. Aenean justo massa."}
                      );
            MaisMultados = new GrupoDeMultas(TipoGrupo.MaisMultados);
            MaisMultados.Add(
                          new Multa{ Id = 11, DataOcorrencia = new DateTime(2012,9,10), NumeroDeMultas = 3, Placa = "ABC 1234", VideoUrl = "", FotoUrl = "http://s3.amazonaws.com/msociais/fotos/17/original/Caminh%C3%A3o.jpg?1324320800", Grupo = MaisMultados, Descricao = "Mussum ipsum cacilds, vidis litro abertis. Consetis adipiscings elitis. Pra lá , depois divoltis porris, paradis. Paisis, filhis, espiritis santis. Mé faiz elementum girarzis, nisi eros vermeio, in elementis mé pra quem é amistosis quis leo. Manduma pindureta quium dia nois paga. Sapien in monti palavris qui num significa nadis i pareci latim. Interessantiss quisso pudia ce receita de bolis, mais bolis eu num gostis."},
                          new Multa{ Id = 12, DataOcorrencia = new DateTime(2012,11,12), NumeroDeMultas = 5, Placa = "DEF 1234", VideoUrl = "", FotoUrl = "http://s3.amazonaws.com/msociais/fotos/60/original/111111111111111.jpg?1324663401", Grupo = MaisMultados, Descricao = "Suco de cevadiss, é um leite divinis, qui tem lupuliz, matis, aguis e fermentis. Interagi no mé, cursus quis, vehicula ac nisi. Aenean vel dui dui. Nullam leo erat, aliquet quis tempus a, posuere ut mi. Ut scelerisque neque et turpis posuere pulvinar pellentesque nibh ullamcorper. Pharetra in mattis molestie, volutpat elementum justo. Aenean ut ante turpis. Pellentesque laoreet mé vel lectus scelerisque interdum cursus velit auctor. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam ac mauris lectus, non scelerisque augue. Aenean justo massa."},
                          new Multa{ Id = 13, DataOcorrencia = new DateTime(2012,1,13), NumeroDeMultas = 5, Placa = "DEF 1234", VideoUrl = "", FotoUrl = "http://s3.amazonaws.com/msociais/fotos/233/original/335405_302499939794014_100001020322706_921790_967861513_o.jpg?1325192483", Grupo = MaisMultados, Descricao = "Suco de cevadiss, é um leite divinis, qui tem lupuliz, matis, aguis e fermentis. Interagi no mé, cursus quis, vehicula ac nisi. Aenean vel dui dui. Nullam leo erat, aliquet quis tempus a, posuere ut mi. Ut scelerisque neque et turpis posuere pulvinar pellentesque nibh ullamcorper. Pharetra in mattis molestie, volutpat elementum justo. Aenean ut ante turpis. Pellentesque laoreet mé vel lectus scelerisque interdum cursus velit auctor. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam ac mauris lectus, non scelerisque augue. Aenean justo massa."},
                          new Multa{ Id = 14, DataOcorrencia = new DateTime(2012,2,14), NumeroDeMultas = 5, Placa = "DEF 1234", VideoUrl = "", FotoUrl = "http://s3.amazonaws.com/msociais/fotos/240/original/IMG00831.jpg?1325603870", Grupo = MaisMultados, Descricao = "Suco de cevadiss, é um leite divinis, qui tem lupuliz, matis, aguis e fermentis. Interagi no mé, cursus quis, vehicula ac nisi. Aenean vel dui dui. Nullam leo erat, aliquet quis tempus a, posuere ut mi. Ut scelerisque neque et turpis posuere pulvinar pellentesque nibh ullamcorper. Pharetra in mattis molestie, volutpat elementum justo. Aenean ut ante turpis. Pellentesque laoreet mé vel lectus scelerisque interdum cursus velit auctor. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam ac mauris lectus, non scelerisque augue. Aenean justo massa."},
                          new Multa { Id = 15, DataOcorrencia = new DateTime(2012, 9, 10), NumeroDeMultas = 3, Placa = "ABC 1234", VideoUrl = "", FotoUrl = "http://s3.amazonaws.com/msociais/fotos/17/original/Caminh%C3%A3o.jpg?1324320800", Grupo = MaisMultados, Descricao = "Mussum ipsum cacilds, vidis litro abertis. Consetis adipiscings elitis. Pra lá , depois divoltis porris, paradis. Paisis, filhis, espiritis santis. Mé faiz elementum girarzis, nisi eros vermeio, in elementis mé pra quem é amistosis quis leo. Manduma pindureta quium dia nois paga. Sapien in monti palavris qui num significa nadis i pareci latim. Interessantiss quisso pudia ce receita de bolis, mais bolis eu num gostis." },
                          new Multa { Id = 16, DataOcorrencia = new DateTime(2012, 11, 12), NumeroDeMultas = 5, Placa = "DEF 1234", VideoUrl = "", FotoUrl = "http://s3.amazonaws.com/msociais/fotos/60/original/111111111111111.jpg?1324663401", Grupo = MaisMultados, Descricao = "Suco de cevadiss, é um leite divinis, qui tem lupuliz, matis, aguis e fermentis. Interagi no mé, cursus quis, vehicula ac nisi. Aenean vel dui dui. Nullam leo erat, aliquet quis tempus a, posuere ut mi. Ut scelerisque neque et turpis posuere pulvinar pellentesque nibh ullamcorper. Pharetra in mattis molestie, volutpat elementum justo. Aenean ut ante turpis. Pellentesque laoreet mé vel lectus scelerisque interdum cursus velit auctor. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam ac mauris lectus, non scelerisque augue. Aenean justo massa." },
                          new Multa { Id = 17, DataOcorrencia = new DateTime(2012, 1, 13), NumeroDeMultas = 5, Placa = "DEF 1234", VideoUrl = "", FotoUrl = "http://s3.amazonaws.com/msociais/fotos/233/original/335405_302499939794014_100001020322706_921790_967861513_o.jpg?1325192483", Grupo = MaisMultados, Descricao = "Suco de cevadiss, é um leite divinis, qui tem lupuliz, matis, aguis e fermentis. Interagi no mé, cursus quis, vehicula ac nisi. Aenean vel dui dui. Nullam leo erat, aliquet quis tempus a, posuere ut mi. Ut scelerisque neque et turpis posuere pulvinar pellentesque nibh ullamcorper. Pharetra in mattis molestie, volutpat elementum justo. Aenean ut ante turpis. Pellentesque laoreet mé vel lectus scelerisque interdum cursus velit auctor. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam ac mauris lectus, non scelerisque augue. Aenean justo massa." },
                          new Multa { Id = 18, DataOcorrencia = new DateTime(2012, 2, 14), NumeroDeMultas = 5, Placa = "DEF 1234", VideoUrl = "", FotoUrl = "http://s3.amazonaws.com/msociais/fotos/240/original/IMG00831.jpg?1325603870", Grupo = MaisMultados, Descricao = "Suco de cevadiss, é um leite divinis, qui tem lupuliz, matis, aguis e fermentis. Interagi no mé, cursus quis, vehicula ac nisi. Aenean vel dui dui. Nullam leo erat, aliquet quis tempus a, posuere ut mi. Ut scelerisque neque et turpis posuere pulvinar pellentesque nibh ullamcorper. Pharetra in mattis molestie, volutpat elementum justo. Aenean ut ante turpis. Pellentesque laoreet mé vel lectus scelerisque interdum cursus velit auctor. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam ac mauris lectus, non scelerisque augue. Aenean justo massa." }
                      );
        }
#endif
    }


}
