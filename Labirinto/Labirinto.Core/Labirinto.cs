using System.Collections.Generic;
using System.IO;
namespace Labirinto.Core
{
    public class Labirinto
    {
        private static volatile Labirinto _labirintoInstance;

        public Ponto[,] Pontos { get; private set; }
        public Ponto PontoInicial { get; private set; }
        public Ponto PontoFinal { get; private set; }
        public Stack<Ponto> MelhorRota { get; private set; }

        public int MenorDistancia { get; private set; }
        public int NumLinhas { get; private set; }
        public int NumColunas { get; private set; }

        private Labirinto(){}

        public static Labirinto GetInstance()
        {
            return _labirintoInstance ?? (_labirintoInstance = new Labirinto());
        }

        public void LoadMap(FileInfo fileName)
        {
            this.Pontos = new FileMap(fileName.FullName).Read();
            this.PontoInicial = this.RetornaPontoInicial();
            this.SetaNumeroLinhasColunas();
        }

        public bool PodeMover(Movimento movimento, Ponto ponto)
        {
            if (movimento.Equals(Movimento.Subir))
            {
                if (ponto.Linha - 1 >= 0)
                {
                    Ponto proximoPonto = this.Pontos[ponto.Linha - 1, ponto.Coluna];

                    if (proximoPonto.Tipo.Equals(TipoPonto.Campo) || proximoPonto.Tipo.Equals(TipoPonto.Fim) || proximoPonto.Tipo.Equals(TipoPonto.Inicio))
                        return true;
                }
                return false;
            }
            if (movimento.Equals(Movimento.Descer))
            {
                if (ponto.Linha + 1 <= this.NumLinhas - 1)
                {
                    Ponto proximoPonto = this.Pontos[ponto.Linha + 1, ponto.Coluna];

                    if (proximoPonto.Tipo.Equals(TipoPonto.Campo) || proximoPonto.Tipo.Equals(TipoPonto.Fim) || proximoPonto.Tipo.Equals(TipoPonto.Inicio))
                        return true;
                }
                return false;
            }
            if (movimento.Equals(Movimento.Direita))
            {
                if (ponto.Coluna + 1 <= this.NumColunas - 1)
                {
                    Ponto proximoPonto = this.Pontos[ponto.Linha, ponto.Coluna + 1];

                    if (proximoPonto.Tipo.Equals(TipoPonto.Campo) || proximoPonto.Tipo.Equals(TipoPonto.Fim) || proximoPonto.Tipo.Equals(TipoPonto.Inicio))
                        return true;
                }
                return false;
            }
            else if (movimento.Equals(Movimento.Esquerda))
            {
                if (ponto.Coluna - 1 >= 0)
                {
                    Ponto proximoPonto = this.Pontos[ponto.Linha, ponto.Coluna - 1];

                    if (proximoPonto.Tipo.Equals(TipoPonto.Campo) || proximoPonto.Tipo.Equals(TipoPonto.Fim) || proximoPonto.Tipo.Equals(TipoPonto.Inicio))
                        return true;

                }
                return false;
            }
            return false;
        }

        private Ponto RetornaPontoInicial()
        {
            foreach (Ponto p in this.Pontos)
                if (p.Tipo.Equals(TipoPonto.Inicio))
                    return p;
            return null;
        }

        public void SetaNumeroLinhasColunas()
        {
            this.NumColunas = 0;
            this.NumLinhas = 0;

            foreach (Ponto p in this.Pontos)
            {
                this.NumLinhas = (p.Linha + 1 > this.NumLinhas) ? p.Linha + 1 : this.NumLinhas;
                this.NumColunas = (p.Coluna + 1 > this.NumColunas) ? p.Coluna + 1 : this.NumColunas;
            }
        }

        public void ResetaDistanciaEStatus()
        {
            foreach (Ponto p in this.Pontos)
            {
                p.Distancia = null;
                p.StatusAtualPonto = StatusPonto.NaoVisitado;
            }
            this.PontoFinal = null;
        }

        public void VerificaPossiveisRotas()
        {
            Queue<Ponto> queue = new Queue<Ponto>();
            queue.Enqueue(this.PontoInicial);

            while (queue.Count > 0)
            {
                Ponto p = queue.Dequeue();
                p.StatusAtualPonto = StatusPonto.JaVisitado;

                if (!p.Distancia.HasValue)
                    p.Distancia = 0;

                if (p.Tipo.Equals(TipoPonto.Fim))
                    this.PontoFinal = p;

                foreach (Ponto item in this.RetornaPontosAdjacentes(p))
                {
                    if (item.StatusAtualPonto.Equals(StatusPonto.NaoVisitado))
                    {
                        item.StatusAtualPonto = StatusPonto.Fila;
                        item.Distancia = p.Distancia.Value + 1;
                        queue.Enqueue(item);
                    }
                }
            }
        }

        public void DeterminaMelhorRota()
        {
            this.MelhorRota = new Stack<Ponto>();
            Ponto pontoAtual = this.PontoFinal;
            this.MelhorRota.Push(pontoAtual);

            while (!pontoAtual.Equals(PontoInicial))
            {
                foreach (Ponto item in this.RetornaPontosAdjacentes(pontoAtual))
                {
                    if (pontoAtual.Equals(null))
                        pontoAtual = item;
                    else if (item.Distancia < pontoAtual.Distancia)
                        pontoAtual = item;
                }
                this.MelhorRota.Push(pontoAtual);
            }
        }

        public List<Ponto> RetornaPontosAdjacentes(Ponto p)
        {
            List<Ponto> listaPontosAdjacentes = new List<Ponto>();

            if (this.PodeMover(Movimento.Descer, p))
                listaPontosAdjacentes.Add(this.Pontos[p.Linha + 1, p.Coluna]);
            if (this.PodeMover(Movimento.Direita, p))
                listaPontosAdjacentes.Add(this.Pontos[p.Linha, p.Coluna + 1]);
            if (this.PodeMover(Movimento.Esquerda, p))
                listaPontosAdjacentes.Add(this.Pontos[p.Linha, p.Coluna - 1]);
            if (this.PodeMover(Movimento.Subir, p))
                listaPontosAdjacentes.Add(this.Pontos[p.Linha - 1, p.Coluna]);

            return listaPontosAdjacentes;
        }
    }
}
