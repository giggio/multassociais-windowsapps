using System;
using FluentAssertions;
using MultasSociais.Lib.Models;
using NUnit.Framework;

namespace Testes.Unidade
{
    [TestFixture]
    public class MultaVaziaTestes
    {
        private GrupoDeMultas grupo;
        private Multa multa;

        [TestFixtureSetUp]
        public void CriaMulta()
        {
            grupo = new GrupoDeMultas(TipoGrupo.MaisNovos);
            multa = new Multa { Id = 2, DataOcorrencia = DateTime.Now.AddSeconds(-10), NumeroDeMultas = 0, Placa = "", VideoUrl = "", FotoUrl = "", Grupo = grupo, Descricao = "" };
            grupo.Add(multa);
        }

        [Test]
        public void DataDescritaCorreta()
        {
            multa.DataDescrita.Should().Be("Hoje");
        }
        [Test]
        public void NumeroDeMultasDescritaCorreta()
        {
            multa.NumeroDeMultasDescrita.Should().Be("Nenhuma multa");
        }
        [Test]
        public void DescricaoDescritaCorreta()
        {
            multa.Descricao.Should().Be("Nenhuma descrição informada");
        }
        [Test]
        public void PlacaDescritaCorreta()
        {
            multa.Placa.Should().Be("não informada");
        }
        [Test]
        public void FotoUrlCorreta()
        {
            multa.FotoUrl.Should().Be("ms-appx:///Assets/SemImagem.png");
        }
    }
}
