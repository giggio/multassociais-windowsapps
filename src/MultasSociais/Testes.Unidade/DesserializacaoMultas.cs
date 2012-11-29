using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using MultasSociais.Lib.Models;
using NUnit.Framework;
using Newtonsoft.Json;

namespace Testes.Unidade
{
    [TestFixture]
    public class DesserializacaoMultas
    {
        private Multa[] multas;
        private Multa multa1;
        private Multa multa2;

        [TestFixtureSetUp]
        public void Desserializa()
        {
            var multaJson =
@"[{
        ""data_ocorrencia"": ""2012-11-25T23:56:00-02:00"",
        ""descricao"": ""Bateu atrás"",
        ""id"": 739,
        ""likes"": 14,
        ""placa"": ""ABC 123"",
        ""video"": ""http://www.youtube.com/algo"",
        ""foto_url"": ""http://s3.amazonaws.com/msociais/fotos/739/original/-2583207578701085183.jpg?1353895021""
},
{
        ""data_ocorrencia"": ""2011-11-25T23:56:00-02:00"",
        ""descricao"": ""2Bateu atrás"",
        ""id"": 2739,
        ""likes"": 214,
        ""placa"": ""DBC 123"",
        ""video"": ""http://www.youtube.com/algo2"",
        ""foto_url"": ""http://s3.amazonaws.com/msociais2/fotos/739/original/-2583207578701085183.jpg?1353895021""
}]";
            multas = JsonConvert.DeserializeObject<IEnumerable<Multa>>(multaJson).ToArray();
            multa1 = multas[0];
            multa2 = multas[1];
        }
        [Test]
        public void Desserializam2Multas()
        {
            multas.Count().Should().Be(2);
        }
        [Test]
        public void DataOcorrencia1Desserializa()
        {
            multa1.DataOcorrencia.Should().Be(new DateTime(2012, 11, 25, 23, 56, 0));
        }
        [Test]
        public void Descricao1Desserializa()
        {
            multa1.Descricao.Should().Be("Bateu atrás");
        }
        [Test]
        public void FotoUrl1Desserializa()
        {
            multa1.FotoUrl.Should().Be("http://s3.amazonaws.com/msociais/fotos/739/original/-2583207578701085183.jpg?1353895021");
        }
        [Test]
        public void Id1Desserializa()
        {
            multa1.Id.Should().Be(739);
        }
        [Test]
        public void NumeroDeMultas1Desserializa()
        {
            multa1.NumeroDeMultas.Should().Be(14);
        }
        [Test]
        public void Placa1Desserializa()
        {
            multa1.Placa.Should().Be("ABC 123");
        }
        [Test]
        public void VideoUrl1Desserializa()
        {
            multa1.VideoUrl.Should().Be("http://www.youtube.com/algo");
        }
        [Test]
        public void DataOcorrencia2Desserializa()
        {
            multa2.DataOcorrencia.Should().Be(new DateTime(2011, 11, 25, 23, 56, 0));
        }
        [Test]
        public void Descricao2Desserializa()
        {
            multa2.Descricao.Should().Be("2Bateu atrás");
        }
        [Test]
        public void FotoUrl2Desserializa()
        {
            multa2.FotoUrl.Should().Be("http://s3.amazonaws.com/msociais2/fotos/739/original/-2583207578701085183.jpg?1353895021");
        }
        [Test]
        public void Id2Desserializa()
        {
            multa2.Id.Should().Be(2739);
        }
        [Test]
        public void NumeroDeMultas2Desserializa()
        {
            multa2.NumeroDeMultas.Should().Be(214);
        }
        [Test]
        public void Placa2Desserializa()
        {
            multa2.Placa.Should().Be("DBC 123");
        }
        [Test]
        public void VideoUrl2Desserializa()
        {
            multa2.VideoUrl.Should().Be("http://www.youtube.com/algo2");
        }
    }
}