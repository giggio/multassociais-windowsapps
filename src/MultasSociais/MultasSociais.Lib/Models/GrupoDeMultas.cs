using System.Collections.Generic;

namespace MultasSociais.Lib.Models
{
    public class GrupoDeMultas
    {
        private readonly List<Multa> itens = new List<Multa>();

        public GrupoDeMultas(TipoGrupo tipoGrupo) : this(GrupoDeMultas.ObterNome(tipoGrupo), tipoGrupo) { }

        public GrupoDeMultas(string nome, TipoGrupo tipoGrupo)
        {
            Nome = nome;
            TipoGrupo = tipoGrupo;
        }
        public void Add(IEnumerable<Multa> multas)
        {
            itens.AddRange(multas);
            foreach (var multa in multas)
                multa.Grupo = this;
        }
        public void Add(params Multa[] multas)
        {
            Add((IEnumerable<Multa>) multas);
        }

        public IEnumerable<Multa> Itens { get { return itens; } }
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
