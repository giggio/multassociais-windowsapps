using System.Collections.Generic;

namespace MultasSociais.Lib.Models
{
    public class GrupoDeMultas
    {
        public IEnumerable<Multa> Itens { get; set; }
        public string Nome { get; set; }
        public TipoGrupo TipoGrupo { get; set; }
    }
}
