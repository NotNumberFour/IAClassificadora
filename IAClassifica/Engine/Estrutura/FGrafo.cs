using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Estrutura
{
    public static class FGrafo
    {
        public static void GerarGraficoTreinamento(List<double> acuracias, List<double> losses, string caminhoArquivo)
        {
            var model = new PlotModel { Title = "Treinamento da Rede Neural" };

            var acuraciaSerie = new LineSeries { Title = "Acurácia", Color = OxyColors.Blue };
            var lossSerie = new LineSeries { Title = "Erro (Loss)", Color = OxyColors.Red };

            for (int i = 0; i < acuracias.Count; i++)
            {
                acuraciaSerie.Points.Add(new DataPoint(i, acuracias[i]));
                lossSerie.Points.Add(new DataPoint(i, losses[i]));
            }

            model.Series.Add(acuraciaSerie);
            model.Series.Add(lossSerie);

            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Épocas" });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Valor" });

            using var stream = File.Create(caminhoArquivo);
            var pngExporter = new PngExporter { Width = 800, Height = 600 };
            pngExporter.Export(model, stream);


        }

        public static void GerarMapaPesos16x16(Camada camada, string path, int epoca, int neuronioIndex)
        {
            

            int largura = 28 * 10;
            int altura = 28 * 10;

            var imagem = new System.Drawing.Bitmap(largura, altura);
            using var g = System.Drawing.Graphics.FromImage(imagem);

            var pesos = camada.Neuronio[neuronioIndex].Weight;
            double min = pesos.Min(), max = pesos.Max();

            for (int i = 0; i < pesos.Length; i++)
            {
                int x = i % 28;
                int y = i / 28;

                double val = (pesos[i] - min) / (max - min + 1e-8); // normaliza entre 0 e 1
                int cor = (int)(val * 255);
                var brush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(cor, cor, cor));

                g.FillRectangle(brush, x * 10, y * 10, 10, 10);
            }

            Directory.CreateDirectory(path);
            string nome = Path.Combine(path, $"pesos_epoca{epoca}_neuronio{neuronioIndex}.png");
            imagem.Save(nome, System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
