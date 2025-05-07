using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

public static class UIHelperSemBorda
{
    public static void ArredondarPaineis(IEnumerable<Panel> paineis, int borderRadius)
    {
        foreach (Panel panel in paineis)
        {
            panel.Paint += (sender, e) =>
            {
                // Qualidade alta no render
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                e.Graphics.CompositingQuality = CompositingQuality.HighQuality;

                RectangleF rect = new RectangleF(0, 0, panel.Width, panel.Height);

                using (GraphicsPath path = CriarCaminhoArredondado(rect, borderRadius))
                {
                    panel.Region = new Region(path);
                    e.Graphics.Clear(panel.BackColor); // Fundo liso
                }
            };

            panel.Invalidate(); // Força redesenho
        }
    }

    private static GraphicsPath CriarCaminhoArredondado(RectangleF rect, int radius)
    {
        float d = radius * 2;
        GraphicsPath path = new GraphicsPath();

        path.StartFigure();
        path.AddArc(rect.X, rect.Y, d, d, 180, 90);
        path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
        path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
        path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
        path.CloseFigure();

        return path;
    }
}

