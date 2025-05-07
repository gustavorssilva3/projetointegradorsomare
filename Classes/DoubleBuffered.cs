using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projetointegrador.Classes
{
    public static class ControlExtensions
    {
        public static void SetDoubleBuffered(this Control control, bool value)
        {
            // Usa reflexão para acessar a propriedade "DoubleBuffered" não-pública
            var prop = control.GetType().GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            if (prop != null)
                prop.SetValue(control, value, null);
        }
    }
}
