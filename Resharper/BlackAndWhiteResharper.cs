using System.Drawing;
using System.Drawing.Imaging;

namespace Resharper
{
    internal class BlackAndWhiteResharper
    {
        private static void Main()
        {
            var image = new Bitmap("..\\..\\image.jpg");

            for (var y = 0; y < image.Height; ++y)
            {
                for (var x = 0; x < image.Width; ++x)
                {
                    var pixel = image.GetPixel(x, y);

                    var newPixelComponentValue = (int)(0.3 * pixel.R + 0.59 * pixel.G + 0.11 * pixel.B);
                    var newColor = Color.FromArgb(newPixelComponentValue, newPixelComponentValue, newPixelComponentValue);

                    image.SetPixel(x, y, newColor);
                }
            }

            image.Save("out.jpg", ImageFormat.Jpeg);
        }
    }
}