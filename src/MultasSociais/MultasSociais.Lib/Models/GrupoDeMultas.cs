using System.Collections.Generic;
using System.Linq;

namespace MultasSociais.Lib.Models
{
    public class GrupoDeMultas
    {
        private readonly List<Multa> itens = new List<Multa>();
        private IEnumerable<Multa> itensTop = new List<Multa>();

        public GrupoDeMultas(TipoGrupo tipoGrupo)
        {
            Nome = ObterNome(tipoGrupo);
            TipoGrupo = tipoGrupo;
        }

        public void Add(IEnumerable<Multa> multas)
        {
            itens.AddRange(multas);
            foreach (var multa in multas)
                multa.Grupo = this;
            itensTop = (from m in itens
                       orderby m.DataOcorrencia descending
                       select m).Take(4);
        }
        public void Add(params Multa[] multas)
        {
            Add((IEnumerable<Multa>) multas);
        }

        public IEnumerable<Multa> Itens { get { return itens; } }
        public IEnumerable<Multa> ItensTop { get { return itensTop; } }
        public string Nome { get; private set; }
        public TipoGrupo TipoGrupo { get; private set; }

        public static string ObterNome(TipoGrupo tipoGrupo)
        {
            var nome = "";
            switch (tipoGrupo)
            {
                case TipoGrupo.MaisNovos:
                    nome = "Mais multados";
                    break;
                case TipoGrupo.MaisMultados:
                    nome = "Mais novos";
                    break;
            }
            return nome;
        }
    }
}
