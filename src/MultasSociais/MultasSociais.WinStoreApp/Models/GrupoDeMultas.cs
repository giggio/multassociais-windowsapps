using System.Collections.Generic;

namespace MultasSociais.WinStoreApp.Models
{
    public class GrupoDeMultas
    {
        public IEnumerable<Multa> Itens { get; set; }
        public string Nome { get; set; }
    }
}
