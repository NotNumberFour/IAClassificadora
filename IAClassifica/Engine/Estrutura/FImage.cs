using System.Drawing;

namespace Engine.Estrutura
{
    public class FImage
    {
        public string NomeImagem { get; set; }
        public string ValorTexto { get; set; }
        public float ValorReal { get; set; }
        public float[] ValorImagem { get; set; }

        public void CarregarImagem(string url)
        {
            try
            {
                Bitmap image = new Bitmap(url);
                string nome = Path.GetFileNameWithoutExtension(url);
                string valorTexto = nome.Split("_Value_")[1];
                if (image is null || nome is null) throw new Exception("Nenhuma Imagem Encontrada");

                float[] pixeis = new float[image.Width * image.Height];

                for (int y = 0; y < image.Height; y++)
                {
                    for (int x = 0; x < image.Width; x++)
                    {
                        Color color = image.GetPixel(x, y);

                        pixeis[y * image.Width + x] = (color.R + color.G + color.B) / 3;
                    }
                }

                this.NomeImagem = nome;
                this.ValorTexto = valorTexto;
                this.ValorImagem = pixeis;

            }
            catch (Exception Error)
            {
                Console.WriteLine(Error);
                throw;
            }
        }
        public List<FImage> CarregarImagemTexto(string url)
        {
             List<FImage> fImages = new List<FImage>();
            try
            {
                using (StreamReader reader = new StreamReader(url))
                {
                    int count = 0;
                    while (!reader.EndOfStream)
                    {
                        if(count < 697932)
                        {
                            string linha = reader.ReadLine();
                            string[] valores = linha.Split(',');

                            FImage fImage = new FImage();
                            fImage.NomeImagem = Path.GetFileNameWithoutExtension(url);
                            fImage.ValorTexto = valores[0];
                            fImage.ValorReal = int.Parse(valores[0]);
                            var newValores = valores.Skip(1).ToArray();
                            fImage.ValorImagem = newValores.Select(x => float.Parse(x)).ToArray();
                            fImages.Add(fImage);
                            count++;
                        }
                        else
                        {
                            break;
                        }
                        

                    }
                }
                return fImages;

            }
            catch (Exception Error)
            {
                Console.WriteLine(Error);
                throw;
            }
        }

    }
}
