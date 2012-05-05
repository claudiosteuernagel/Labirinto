namespace Labirinto.Core
{
    public class Ponto
    {
        public int Linha { get; private set; }
        public int Coluna { get; private set; }
        public TipoPonto Tipo { get; private set; }
        public StatusPonto StatusAtualPonto { get; set; }
        public int? Distancia { get; set; }

        public Ponto(int linha, int coluna, TipoPonto tipo)
        {
            this.Coluna = coluna;
            this.Linha = linha;
            this.Tipo = tipo;
            this.StatusAtualPonto = StatusPonto.NaoVisitado;
        }
    }
}
