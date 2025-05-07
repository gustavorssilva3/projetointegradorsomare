using System.Drawing;
using System.Drawing.Imaging;

namespace projetointegrador
{
    public static class UIHelperColorIcon
    {
        public static Image TransformarIconeCor(Image imgOriginal, Color cor)
        {
            Bitmap novaImagem = new Bitmap(imgOriginal.Width, imgOriginal.Height);
            using (Graphics g = Graphics.FromImage(novaImagem))
            {
                ColorMatrix cm = new ColorMatrix(new float[][]
                {
                    new float[] {0, 0, 0, 0, 0},
                    new float[] {0, 0, 0, 0, 0},
                    new float[] {0, 0, 0, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {cor.R / 255f, cor.G / 255f, cor.B / 255f, 0, 1}
                });
                ImageAttributes ia = new ImageAttributes();
                ia.SetColorMatrix(cm);
                g.DrawImage(imgOriginal, new Rectangle(0, 0, imgOriginal.Width, imgOriginal.Height),
                    0, 0, imgOriginal.Width, imgOriginal.Height, GraphicsUnit.Pixel, ia);
            }
            return novaImagem;
        }
    }
}

