using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projetointegrador.Models
{
    public class CustomTextBox : TextBox
    {
        public CustomTextBox()
        {
            this.Multiline = true; // Permite altura personalizada
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.AutoSize = false; // Evita que a altura seja ajustada automaticamente
            this.TextAlign = HorizontalAlignment.Left; // Alinhamento horizontal
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // Código para centralizar verticalmente
            const int EM_SETRECT = 0xB3;
            Rectangle rect = new Rectangle(1, 8, this.Width - 2, this.Height - 2); // Ajuste fino da posição do texto
            SendMessage(this.Handle, EM_SETRECT, 0, ref rect);
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, ref Rectangle lParam);
    }
}
