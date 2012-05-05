using System.Collections.Generic;
using System.IO;

namespace Labirinto.Core
{
    public class FileMap
    {
        private string NomeArquivo { get; set; }

        public FileMap(string nomeArquivo)
        {
            this.NomeArquivo = nomeArquivo;
        }

        public Ponto[,] Read()
        {
            List<Ponto> pontos = new List<Ponto>();
            Ponto[,] matrizLabirinto = null;

            FileInfo file = new FileInfo(this.NomeArquivo);

            if (file.Exists)
            {
                int linha = 0;
                int coluna = 0;

                using (StreamReader sr = new StreamReader(file.FullName))
                {
                    while (!sr.EndOfStream)
                    {
                        char[] line = sr.ReadLine().ToCharArray();

                        foreach (char item in line)
                        {
                            TipoPonto tipoPonto;

                            switch (item.ToString().ToUpper())
                            {
                                case "#":
                                    tipoPonto = TipoPonto.Parede;
                                    break;
                                case ".":
                                    tipoPonto = TipoPonto.Campo;
                                    break;
                                case "I":
                                    tipoPonto = TipoPonto.Inicio;
                                    break;
                                case "F":
                                    tipoPonto = TipoPonto.Fim;
                                    break;
                                default:
                                    tipoPonto = TipoPonto.Parede;
                                    break;
                            }

                            pontos.Add(new Ponto(linha, coluna, tipoPonto));
                            coluna++;
                        }
                        linha++;
                        coluna = 0;
                    }
                }

                int linhas = pontos[pontos.Count - 1].Linha + 1;
                int colunas = pontos[pontos.Count - 1].Coluna + 1;

                matrizLabirinto = new Ponto[linhas, colunas];
                pontos.ForEach(a => matrizLabirinto[a.Linha, a.Coluna] = a);
            }


            return matrizLabirinto;
        }

        public void Save(Ponto[,] pontosLabirinto)
        {
            FileInfo file = new FileInfo(this.NomeArquivo);

            if (file.Exists)
                file.Delete();

            using (StreamWriter sw = new StreamWriter(file.FullName))
            {
                int linha = 0;
                foreach (Ponto p in pontosLabirinto)
                {
                    if (linha != p.Linha)
                        sw.WriteLine();
                    linha = p.Linha;

                    switch (p.Tipo)
                    {
                        case TipoPonto.Parede:
                            sw.Write("#");
                            break;
                        case TipoPonto.Campo:
                            sw.Write(".");
                            break;
                        case TipoPonto.Inicio:
                            sw.Write("I");
                            break;
                        case TipoPonto.Fim:
                            sw.Write("F");
                            break;
                        default:
                            sw.Write("#");
                            break;
                    }
                }
            }
        }
    }
}
