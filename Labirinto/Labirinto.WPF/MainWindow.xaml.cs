using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Labirinto.Core;
using Microsoft.Win32;

namespace Labirinto.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CarregarMapaClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog {Filter = "TXT Files (*.TXT)|*.txt"};
            ofd.ShowDialog();

            FileInfo file = new FileInfo(ofd.FileName);

            if (file.Exists)
            {
                Core.Labirinto labirinto = Core.Labirinto.GetInstance();
                labirinto.LoadMap(file);

                foreach (Ponto p in labirinto.Pontos)
                {
                    Canvas cnv = new Canvas();
                    cnv.Height = 17;
                    cnv.Width = 17;

                    cnv.UseLayoutRounding = true;
                    cnv.HorizontalAlignment = HorizontalAlignment.Left;
                    cnv.VerticalAlignment = VerticalAlignment.Top;
                    cnv.Margin = new Thickness((p.Coluna * (cnv.Width + 1)), (p.Linha * (cnv.Height + 1)), 0, 0);

                    if (p.Tipo == TipoPonto.Campo)
                        cnv.Background = Brushes.WhiteSmoke;
                    else if (p.Tipo == TipoPonto.Parede)
                        cnv.Background = Brushes.DarkGray;
                    else if (p.Tipo == TipoPonto.Inicio)
                        cnv.Background = Brushes.Yellow;
                    else if (p.Tipo == TipoPonto.Fim)
                        cnv.Background = Brushes.Red;

                    grid1.Children.Add(cnv);

                }
                this.btnVerificarRotas.IsEnabled = true;
                this.btnMelhotRota.IsEnabled = false;

                this.Width = (labirinto.NumColunas * 19);
                this.Height = (labirinto.NumLinhas * 19) + 195;
            }
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
        }

        private void VerificarRotasClick(object sender, RoutedEventArgs e)
        {
            Core.Labirinto labirinto = Core.Labirinto.GetInstance();
            labirinto.VerificaPossiveisRotas();

            if (labirinto.PontoFinal != null)
            {
                this.btnMelhotRota.IsEnabled = true;
                this.label3.Content = "Sim";
                this.label3.IsEnabled = true;
            }
            else
            {
                this.label3.Content = "Não";
                this.label3.IsEnabled = true;
            }
        }

        private void ExibirMelhorRotaClick(object sender, RoutedEventArgs e)
        {
            Core.Labirinto labirinto = Core.Labirinto.GetInstance();
            labirinto.DeterminaMelhorRota();

            grid1.Children.Clear();

            foreach (Ponto p in labirinto.Pontos)
            {
                Canvas cnv = new Canvas();
                cnv.Height = 17;
                cnv.Width = 17;

                cnv.UseLayoutRounding = true;
                cnv.HorizontalAlignment = HorizontalAlignment.Left;
                cnv.VerticalAlignment = VerticalAlignment.Top;
                cnv.Margin = new Thickness((p.Coluna * (cnv.Width + 1)), (p.Linha * (cnv.Height + 1)), 0, 0);

                if (p.Tipo == TipoPonto.Campo)
                    cnv.Background = Brushes.WhiteSmoke;
                else if (p.Tipo == TipoPonto.Parede)
                    cnv.Background = Brushes.DarkGray;
                else if (p.Tipo == TipoPonto.Inicio)
                    cnv.Background = Brushes.Yellow;
                else if (p.Tipo == TipoPonto.Fim)
                    cnv.Background = Brushes.Red;

                if (labirinto.MelhorRota.Contains(p) && p.Tipo == TipoPonto.Campo)
                    cnv.Background = Brushes.GreenYellow;

                grid1.Children.Add(cnv);

                this.label5.Content = labirinto.MelhorRota.Count - 1;
                this.label5.IsEnabled = true;
            }
        }
    }
}
