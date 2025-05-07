using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace projetointegrador
{
    public class Eventos
    {
        private Dictionary<Control, Label> mapaLabels;
        private ErrorProvider errorProvider;
        private bool formatandoData;

        public Eventos(Dictionary<Control, Label> mapaLabels, ErrorProvider errorProvider)
        {
            this.mapaLabels = mapaLabels;
            this.errorProvider = errorProvider;
        }

        public Eventos()
        {
            this.mapaLabels = new Dictionary<Control, Label>();
            this.errorProvider = null;
        }

        public void VerificarCamposComTexto()
        {
            foreach (var par in mapaLabels)
            {
                var ctrl = par.Key;
                var lbl = par.Value;

                bool temTexto = false;

                if (ctrl is TextBox txt)
                    temTexto = !string.IsNullOrWhiteSpace(txt.Text);
                else if (ctrl is ComboBox cb)
                    temTexto = cb.SelectedItem != null && !string.IsNullOrWhiteSpace(cb.Text);

                lbl.Visible = !temTexto; // se tem texto: esconde o label (como placeholder)
            }
        }

        public void AdicionarEventos(Control ctrl)
        {
            ctrl.Enter += Controle_Enter;
            ctrl.Leave += Controle_Leave;

            if (ctrl is ComboBox combo)
            {
                combo.DropDown += Controle_Enter;
                combo.DropDownClosed += Controle_Leave;
            }
        }

        private void Controle_Enter(object sender, EventArgs e)
        {
            Control ctrl = sender as Control;
            if (ctrl == null)
                return;

            Label lbl = EncontrarLabel(ctrl);
            if (lbl != null)
                lbl.Visible = false;
        }

        private void Controle_Leave(object sender, EventArgs e)
        {
            Control ctrl = sender as Control;
            if (ctrl == null || !string.IsNullOrWhiteSpace(ctrl.Text))
                return;

            Label lbl = EncontrarLabel(ctrl);
            if (lbl != null)
                lbl.Visible = true;
        }

        private Label EncontrarLabel(Control ctrl)
        {
            if (mapaLabels.TryGetValue(ctrl, out Label label))
                return label;

            string nomeLabel = "lbl" + ctrl.Name.Substring(3);
            Control parent = ctrl;
            while (parent != null)
            {
                Control[] encontrados = parent.Controls.Find(nomeLabel, true);
                if (encontrados.Length > 0)
                    return encontrados[0] as Label;

                parent = parent.Parent;
            }

            return null;
        }

        public void PanelClique(object sender, EventArgs e)
        {
            if (sender is Panel painel)
            {
                foreach (Control controle in painel.Controls)
                {
                    if (controle is TextBox)
                    {
                        controle.Focus();
                        return;
                    }
                    if (controle is ComboBox combo)
                    {
                        combo.Focus();
                        combo.DroppedDown = true;
                        return;
                    }
                }
            }
        }

        // amarre este método ao evento TextChanged do seu TextBox
        public void FormatarData(object sender, EventArgs e)
        {
            if (formatandoData) return;
            formatandoData = true;

            if (sender is TextBox txt)
            {
                int posicaoCursor = txt.SelectionStart;

                // Remove tudo que não for dígito
                string digitos = new string(txt.Text.Where(char.IsDigit).ToArray());
                if (digitos.Length > 8)
                    digitos = digitos.Substring(0, 8);

                var sb = new StringBuilder();
                int novoCursor = posicaoCursor;
                int cont = 0;

                // Insere dia
                if (digitos.Length - cont > 0)
                {
                    int len = Math.Min(2, digitos.Length - cont);
                    sb.Append(digitos.Substring(cont, len));
                    cont += len;
                    if (len == 2 && digitos.Length > 2)
                    {
                        sb.Append('/');
                        if (posicaoCursor > cont) novoCursor++;
                    }
                }

                // Insere mês
                if (digitos.Length - cont > 0)
                {
                    int len = Math.Min(2, digitos.Length - cont);
                    sb.Append(digitos.Substring(cont, len));
                    cont += len;
                    if (len == 2 && digitos.Length > cont)
                    {
                        sb.Append('/');
                        if (posicaoCursor > cont) novoCursor++;
                    }
                }

                // Insere ano
                if (digitos.Length - cont > 0)
                {
                    sb.Append(digitos.Substring(cont));
                }

                txt.Text = sb.ToString();
                // Ajusta posição do cursor
                txt.SelectionStart = Math.Min(novoCursor, txt.Text.Length);
            }

            formatandoData = false;
        }
            public void PermitirSomenteNumeros(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public void PermitirSomenteValor(object sender, KeyPressEventArgs e)
        {
            if (sender is TextBox txt)
            {
                if (char.IsControl(e.KeyChar))
                    return;

                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',')
                {
                    e.Handled = true;
                    return;
                }

                if (e.KeyChar == '.' || e.KeyChar == ',')
                {
                    if (txt.SelectionStart == 0)
                    {
                        e.Handled = true;
                        return;
                    }

                    if (txt.Text.Contains('.') || txt.Text.Contains(','))
                    {
                        e.Handled = true;
                        return;
                    }

                    return;
                }

                int posSeparador = txt.Text.IndexOf('.') >= 0
                    ? txt.Text.IndexOf('.')
                    : txt.Text.IndexOf(',');

                if (posSeparador >= 0 && txt.SelectionStart > posSeparador)
                {
                    int casasDecimais = txt.Text.Length - posSeparador - 1;

                    if (casasDecimais >= 2 && !txt.SelectedText.Contains(".") && !txt.SelectedText.Contains(","))
                    {
                        e.Handled = true;
                    }
                }
            }
        }
    }
}
