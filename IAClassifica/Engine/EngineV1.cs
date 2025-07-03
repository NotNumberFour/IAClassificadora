using Engine.Estrutura;
using Engine.Helper;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace Engine
{
    public class EngineV1
    {
        public int Epocas = 10;
        List<Camada> camadas = new List<Camada>
        {
            new Camada(10,28*28),
            new Camada(10,10),
        };
        List<FImage> imagens = new FImage().CarregarImagemTexto("C:/GIT/C3/Classificação/Engine/Dataset/emnist-byclass-train.csv");
        public float TaxaAprendizado = 0.1f;

        public int Prever(float[] inputs)
        {
            float[][] result = new float[camadas.Count][];
            float[] ativacao = inputs;
            for (int i = 0; i < camadas.Count; i++)
            {

                ativacao = camadas[i].Somatoria(ativacao);
                if (i == camadas.Count - 1)
                {
                    result[i] = Functions.Softmax(ativacao);
                }
                else result[i] = ativacao;

            }

            return Array.IndexOf(result.Last(), result.Last().Max());

        }
        public void Treinar()
        {
            for (int epocas = 0; epocas < Epocas; epocas++)
            {
                Console.WriteLine($"Época {epocas + 1}/{Epocas}");
                for (int image = 0; image < imagens.Count; image++)
                {
                    float[][] result = new float[camadas.Count][];
                    float[] ativacao = imagens[image].ValorImagem;
                    for (int i = 0; i < camadas.Count; i++)
                    {

                        ativacao = camadas[i].Somatoria(ativacao, !(i == camadas.Count - 1));
                        if(i == camadas.Count - 1)
                        {
                            result[i] = Functions.Softmax(ativacao);
                        }else result[i] = ativacao;

                    }

                    int previsto = Array.IndexOf(result.Last(), result.Last().Max());

                    float[] erro = new float[result.Last().Length];
                    for (int i = 0; i < erro.Length; i++)
                    {
                        erro[i] = (i == imagens[image].ValorReal) ? (result.Last()[i] - 1) : result.Last()[i];
                    }

                    for (int camadaIndex = camadas.Count - 1; camadaIndex >= 0; camadaIndex--)
                    {
                        var camada = camadas[camadaIndex];
                        float[] saidaAtual = result[camadaIndex];
                        float[] entradaAtual = (camadaIndex == 0) ? imagens[image].ValorImagem : result[camadaIndex - 1];

                        float[] derivada = (camadaIndex == camadas.Count - 1)
                            ? Enumerable.Repeat(1.0f, saidaAtual.Length).ToArray()
                            : Functions.SigmoidDerivada(saidaAtual);

                        double[] erroAnterior = new double[camada.Neuronio[0].Weight.Length];

                        
                        for (int neuronioIndex = 0; neuronioIndex < camada.Neuronio.Count; neuronioIndex++)
                        {
                            camada.AjustaNeuronios(entradaAtual, TaxaAprendizado , erro , derivada);
                        }

                    }
                }
            }
        }
    }
}
