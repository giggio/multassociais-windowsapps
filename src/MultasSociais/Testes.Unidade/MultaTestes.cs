using System;
using System.Collections.Generic;
using FluentAssertions;
using MultasSociais.Lib.Models;
using NUnit.Framework;

namespace Testes.Unidade
{
    [TestFixture]
    public class MultaTestes
    {
        private GrupoDeMultas grupo;
        private Multa multa;

        [TestFixtureSetUp]
        public void CriaMulta()
        {
            grupo = new GrupoDeMultas("Mais novos", TipoGrupo.MaisNovos);
            var tresDiasAtras = DateTime.UtcNow.AddDays(-3).AddMinutes(-10);
            multa = new Multa { Id = 1, DataOcorrencia = tresDiasAtras, NumeroDeMultas = 7, Placa = "ABC 1234", VideoUrl = "", FotoUrl = "http://s3.amazonaws.com/msociais/fotos/15/original/fotoblur.png?1324341452", Grupo = grupo, Descricao = "Mussum ipsum cacilds, vidis litro abertis. Consetis adipiscings elitis. Pra lá , depois divoltis porris, paradis. Paisis, filhis, espiritis santis. Mé faiz elementum girarzis, nisi eros vermeio, in elementis mé pra quem é amistosis quis leo. Manduma pindureta quium dia nois paga. Sapien in monti palavris qui num significa nadis i pareci latim. Interessantiss quisso pudia ce receita de bolis, mais bolis eu num gostis." };
            grupo.Add(multa);
        }

        [Test]
        public void DataDescritaCorreta()
        {
            multa.DataDescrita.Should().Be("3 dias atrás");
        }
        [Test]
        public void NumeroDeMultasDescritaCorreta()
        {
            multa.NumeroDeMultasDescrita.Should().Be("7 multas");
        }
    }
}
