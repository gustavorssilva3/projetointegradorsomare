using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections.Generic;

public static class UIHelperComBorda
{
    public static void ArredondarPaineis(IEnumerable<Panel> paineis, int borderRadius)
    {
        foreach (Panel panel in paineis)
        {
            // Garante que o evento não seja assinado múltiplas vezes
            panel.Paint -= Panel_Paint;
            panel.Paint += Panel_Paint;
            panel.Tag = borderRadius;

            panel.Invalidate();
        }
    }

    private static void Panel_Paint(object sender, PaintEventArgs e)
    {
        if (sender is Panel panel && panel.Tag is int borderRadius)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int borderSize = 1;
            RectangleF rect = new RectangleF(borderSize, borderSize,
                                             panel.Width - borderSize * 2,
                                             panel.Height - borderSize * 2);

            using (GraphicsPath path = CriarCaminhoArredondado(rect, borderRadius))
            using (Brush brush = new SolidBrush(panel.BackColor))
            using (Pen pen = new Pen(Color.FromArgb(100, Color.Gray), 1.5f)) // Borda mais suave
            {
                panel.Region = new Region(path);
                e.Graphics.FillPath(brush, path);
                e.Graphics.DrawPath(pen, path);
            }
        }
    }

    private static GraphicsPath CriarCaminhoArredondado(RectangleF rect, int radius)
    {
        float d = radius * 2;
        GraphicsPath path = new GraphicsPath();

        path.AddArc(rect.X, rect.Y, d, d, 180, 90);
        path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
        path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
        path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
        path.CloseFigure();

        return path;
    }
}

