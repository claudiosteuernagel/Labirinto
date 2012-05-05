using Labirinto.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Labirinto.Test
{
    [TestClass]
    public class LabirintoTest
    {
        protected Core.Labirinto LabirintoInstance;

        [TestInitialize]
        public void Setup()
        {
            FileInfo file = new FileInfo(string.Format("{0}\\{1}", AppDomain.CurrentDomain.BaseDirectory, "Labirinto.txt"));

            LabirintoInstance = Core.Labirinto.GetInstance();
            LabirintoInstance.LoadMap(file);
        }

        [TestMethod]
        public void ValidaGetInstance()
        {
            var actual = Core.Labirinto.GetInstance();
            Assert.AreNotEqual(null, actual);
        }

        [TestMethod]
        public void ValidaNumeroPontos()
        {
            Assert.IsTrue(LabirintoInstance.Pontos.Length == 296);
        }

        [TestMethod]
        public void ValidaNumeroLinhasColunas()
        {
            var actual = Core.Labirinto.GetInstance();
            Assert.AreEqual(8, actual.NumLinhas);
            Assert.AreEqual(37, actual.NumColunas);
        }

        [TestMethod]
        public void TestaCalculoDistancia()
        {
            LabirintoInstance.ResetaDistanciaEStatus();
            LabirintoInstance.VerificaPossiveisRotas();

            Assert.AreNotEqual(null, LabirintoInstance.PontoFinal);

            Assert.AreEqual(0, LabirintoInstance.Pontos[0, 33].Distancia);
            Assert.AreEqual(1, LabirintoInstance.Pontos[1, 33].Distancia);
            Assert.AreEqual(42, LabirintoInstance.Pontos[6, 1].Distancia);

        }

        [TestMethod]
        public void ValidaRotasPossiveis()
        {
            LabirintoInstance.ResetaDistanciaEStatus();
            LabirintoInstance.VerificaPossiveisRotas();

            Assert.AreNotEqual(null, LabirintoInstance.PontoFinal);

            Assert.AreEqual(StatusPonto.JaVisitado, LabirintoInstance.Pontos[0, 33].StatusAtualPonto);
            Assert.AreEqual(StatusPonto.JaVisitado, LabirintoInstance.Pontos[1, 33].StatusAtualPonto);
            Assert.AreEqual(StatusPonto.JaVisitado, LabirintoInstance.Pontos[6, 1].StatusAtualPonto);
        }

        [TestMethod]
        public void ValidaMelhorRota()
        {
            LabirintoInstance.ResetaDistanciaEStatus();
            LabirintoInstance.VerificaPossiveisRotas();
            LabirintoInstance.DeterminaMelhorRota();

            Assert.AreEqual(43, LabirintoInstance.MelhorRota.Count);            
        }

        [TestMethod]
        public void ValidaLabirintoSemSaida()
        {
            FileInfo file = new FileInfo(string.Format("{0}\\{1}", AppDomain.CurrentDomain.BaseDirectory, "LabirintoSemSaida.txt"));
            LabirintoInstance = Core.Labirinto.GetInstance();
            LabirintoInstance.ResetaDistanciaEStatus();
            LabirintoInstance.LoadMap(file);
            LabirintoInstance.VerificaPossiveisRotas();

            Assert.IsNull(LabirintoInstance.PontoFinal);
        }
    }
}
