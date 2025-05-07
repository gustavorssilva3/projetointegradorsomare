using System;
using System.Drawing;
using System.Windows.Forms;

public class FormOverlay : Form
{
    public FormOverlay(Form parent)
    {
        this.FormBorderStyle = FormBorderStyle.None;
        this.StartPosition = FormStartPosition.Manual;
        this.Bounds = parent.Bounds;
        this.BackColor = Color.Black;
        this.Opacity = 0.8;
        this.ShowInTaskbar = false;
    }
}
