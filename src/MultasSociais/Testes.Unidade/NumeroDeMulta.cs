using System;
using FluentAssertions;
using MultasSociais.Lib.Models;
using NUnit.Framework;

namespace Testes.Unidade
{
    [TestFixture]
    public class NumeroDeMultas
    {
        [Test]
        public void UmaMulta()
        {
            var multa = new Multa { NumeroDeMultas = 1 };
            multa.NumeroDeMultasDescrita.Should().Be("Uma multa");
        }
        [Test]
        public void NenhumaMulta()
        {
            var multa = new Multa { NumeroDeMultas = 0 };
            multa.NumeroDeMultasDescrita.Should().Be("Nenhuma multa");
        }
        [Test]
        public void DuasMultas()
        {
            var multa = new Multa { NumeroDeMultas = 2 };
            multa.NumeroDeMultasDescrita.Should().Be("2 multas");
        }

    }
}
