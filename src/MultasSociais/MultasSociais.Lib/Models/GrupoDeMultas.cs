using System.Collections.Generic;

namespace MultasSociais.Lib.Models
{
    public class GrupoDeMultas
    {
        private readonly List<Multa> itens = new List<Multa>();

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
    }
}
