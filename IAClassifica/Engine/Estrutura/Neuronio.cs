using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Estrutura
{
    public class Neuronio
    {
        public float[] Weight { get; set; }
        public float Bias { get; set; }
        public Random Random = new Random();
        public Neuronio(int quantidadeEntrada, float Bias)
        {
            this.Weight = new float[quantidadeEntrada];
            this.Bias = Bias;
            this.loadWeight();
        }
        public void loadWeight()
        {
            for (int i = 0; i < Weight.Length; i++)
            {
                Weight[i] = (float)Random.NextDouble();
            }
        }
        public float Somatoria(float[] X)
        {
            float sum = 0;
            for (int i = 0; i < Weight.Length; i++)
            {
                sum += Weight[i] * X[i];
            }
            return sum + Bias;
        }

        
        public void AjustaPeso(float[] X, float erro) {

            for (int i = 0; i < Weight.Length; i++) {

                Weight[i] -= erro * X[i];
                Console.WriteLine(Weight[i]);
            }
            Bias -= erro;
            
        }
    }
}
