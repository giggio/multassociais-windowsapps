using System;
using FluentAssertions;
using MultasSociais.Lib.Models;
using NUnit.Framework;
using Newtonsoft.Json;

namespace Testes.Unidade
{
    [TestFixture]
    public class DesserializacaoMulta
    {
        private Multa multa;

        [TestFixtureSetUp]
        public void Desserializa()
        {
            var multaJson =
@"{
        ""data_ocorrencia"": ""2012-11-25T23:56:00-02:00"",
        ""descricao"": ""Bateu atrás"",
        ""id"": 739,
        ""likes"": 14,
        ""placa"": ""ABC 123"",
        ""video"": ""http://www.youtube.com/algo"",
        ""foto_url"": ""http://s3.amazonaws.com/msociais/fotos/739/original/-2583207578701085183.jpg?1353895021""
    }";
            multa = JsonConvert.DeserializeObject<Multa>(multaJson);
        }
        [Test]
        public void DataOcorrenciaDesserializa()
        {
            multa.DataOcorrencia.Should().Be(new DateTime(2012, 11, 25, 23, 56, 0));
        }
        [Test]
        public void DescricaoDesserializa()
        {
            multa.Descricao.Should().Be("Bateu atrás");
        }
        [Test]
        public void FotoUrlDesserializa()
        {
            multa.FotoUrl.Should().Be("http://s3.amazonaws.com/msociais/fotos/739/original/-2583207578701085183.jpg?1353895021");
        }
        [Test]
        public void IdDesserializa()
        {
            multa.Id.Should().Be(739);
        }
        [Test]
        public void NumeroDeMultasDesserializa()
        {
            multa.NumeroDeMultas.Should().Be(14);
        }
        [Test]
        public void PlacaDesserializa()
        {
            multa.Placa.Should().Be("ABC 123");
        }
        [Test]
        public void VideoUrlDesserializa()
        {
            multa.VideoUrl.Should().Be("http://www.youtube.com/algo");
        }
    }
}
