using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Estrutura
{
    public class Neuronio
    {
        public required float[] Weight { get; set; }
        public float Bias { get; set; }


        public float Somatoria(float[] X)
        {
            float sum = 0;
            for (int i = 0; i < Weight.Length; i++)
            {
                sum += Weight[i] * X[i];
            }
            return sum + Bias;
        }

        public float Sigmoid(float z)
        {
            double result = 1 / (1 + Math.Exp(-z));
            return float.Parse(result.ToString());
        }
    }
}
