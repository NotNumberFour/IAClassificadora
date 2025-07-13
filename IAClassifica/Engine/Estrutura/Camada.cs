using Engine.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Estrutura
{
    public class Camada
    {
        public List<Neuronio> Neuronio { get; set; } = new List<Neuronio>();
        
        public Camada(int QuantidadeNeuronio,int QuantidadeEntrada)
        {
            for (int i = 0; i < QuantidadeNeuronio; i++) {

                Neuronio.Add(new Neuronio(QuantidadeEntrada, 0.01f));
            }
        }

        public float[] Somatoria(float[] x,bool sigmoid = true)
        {
            float[] result = new float[x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                for (int j = 0; j < Neuronio.Count; j++)
                {
                    
                        result[i] = Neuronio[j].Somatoria(x);
                  
                }
            }

            return result;
        }

        public void AjustaNeuronios(float[] x,float learnRate, float[] erro, float[] derivada) {

            for (int neuronioItem = 0; neuronioItem < Neuronio.Count; neuronioItem++) {
                Neuronio[neuronioItem].AjustaPeso(x,(learnRate * erro[neuronioItem] * derivada[neuronioItem]));
            }

        }

    }
}
