using Labirinto.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;

namespace Labirinto.Test
{
    [TestClass]
    public class FileMapTest
    {
        protected FileMap FileInstance;

        [TestInitialize]
        public void Setup()
        {
            FileInfo file = new FileInfo(string.Format("{0}\\{1}", AppDomain.CurrentDomain.BaseDirectory, "Labirinto.txt"));
            FileInstance = new FileMap(file.FullName);
        }

        [TestMethod]
        public void ValidaSeEstaLendoArquivoETodosCaracteres()
        {
            Ponto[,] matrizLabirinto = FileInstance.Read();
            Assert.AreEqual(296, matrizLabirinto.Length);
        }

        [TestMethod]
        public void ValidaLeituraPontoInicio()
        {
            Ponto[,] matrizLabirinto = FileInstance.Read();
            Ponto expected = new Ponto(0, 33, TipoPonto.Inicio);

            Assert.AreEqual(expected.StatusAtualPonto, matrizLabirinto[0, 33].StatusAtualPonto);
            Assert.AreEqual(expected.Linha, matrizLabirinto[0, 33].Linha);
            Assert.AreEqual(expected.Tipo, matrizLabirinto[0, 33].Tipo);
            Assert.AreEqual(expected.Coluna, matrizLabirinto[0, 33].Coluna);
        }

        [TestMethod]
        public void ValidaLeituraPontoFim()
        {
            Ponto[,] matrizLabirinto = FileInstance.Read();
            Ponto expected = new Ponto(6, 1, TipoPonto.Fim);

            Assert.AreEqual(expected.StatusAtualPonto, matrizLabirinto[6, 1].StatusAtualPonto);
            Assert.AreEqual(expected.Linha, matrizLabirinto[6, 1].Linha);
            Assert.AreEqual(expected.Tipo, matrizLabirinto[6, 1].Tipo);
            Assert.AreEqual(expected.Coluna, matrizLabirinto[6, 1].Coluna);
        }

        [TestMethod]
        public void ValidaGravacaoArquivo()
        {
            Ponto[,] matrizLabirinto = FileInstance.Read();

            FileMap fileToSave = new FileMap("LabirintoSave.txt");
            fileToSave.Save(matrizLabirinto);

            FileMap fileToRead = new FileMap("LabirintoSave.txt");
            Ponto[,] matrizLeitura2 = fileToRead.Read();

            foreach (Ponto p in matrizLeitura2)
                Assert.AreEqual(matrizLabirinto[p.Linha, p.Coluna].Tipo, matrizLeitura2[p.Linha, p.Coluna].Tipo);
        }
    }
}
