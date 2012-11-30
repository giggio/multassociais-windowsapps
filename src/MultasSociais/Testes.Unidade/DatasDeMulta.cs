using System;
using FluentAssertions;
using MultasSociais.Lib.Models;
using NUnit.Framework;

namespace Testes.Unidade
{
    [TestFixture]
    public class DatasDeMultas
    {
        [Test]
        public void TresDiasAtras()
        {
            var tresDiasAtras = DateTime.UtcNow.AddDays(-3).AddMinutes(-10);
            var multa = new Multa { DataOcorrencia = tresDiasAtras };
            multa.DataDescrita.Should().Be("3 dias atrás");
        }
        [Test]
        public void Hoje()
        {
            var hoje = DateTime.UtcNow.AddMinutes(-10);
            var multa = new Multa { DataOcorrencia = hoje };
            multa.DataDescrita.Should().Be("Hoje");
        }
        [Test]
        public void Ontem()
        {
            var ontem = DateTime.UtcNow.AddDays(-1).AddMinutes(-10);
            var multa = new Multa { DataOcorrencia = ontem };
            multa.DataDescrita.Should().Be("Ontem");
        }

    }
}
