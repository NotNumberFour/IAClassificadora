using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Helper
{
    public static class Functions
    {
        public static float Sigmoid(float z)
        {
            float result = 1.0f / (1.0f + MathF.Exp(-z));
            return result;
        }

        public static float[] Sigmoid(float[] z)
        {
            float[] results = new float[z.Length];
            for (int i = 0; i < z.Length; i++)
            {
                results[i] = 1.0f / (1.0f + MathF.Exp(-z[i]));
                
            }
            return results;
        }
        public static float[] SigmoidDerivada(float[] saida)
        {
            return saida.Select(x => (float)(x * (1 - x))).ToArray();
        }

        public static float[] Softmax(float[] entrada)
        {
            double max = entrada.Max();
            var exp = entrada.Select(v => Math.Exp(v - max)).ToArray();
            double sumExp = exp.Sum();
            return exp.Select(v => (float)(v / sumExp)).ToArray();
        }


    }
}
