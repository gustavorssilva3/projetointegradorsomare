using System;
using System.Net.Mail;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Globalization;

namespace projetointegrador
{
    public static class Validacoes
    {
        public static bool ValidaNome(string nome)
        {
            return !string.IsNullOrWhiteSpace(nome) && nome.Trim().Length >= 2;
        }

        public static bool ValidaEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string padrao = @"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z]{2,}$";
            return Regex.IsMatch(email, padrao, RegexOptions.IgnoreCase);
        }

        public static bool ValidaSenha(string senha)
        {
            return !string.IsNullOrWhiteSpace(senha) && senha.Length >= 6;
        }

        public static bool ValidaResposta(string resposta)
        {
            return !string.IsNullOrWhiteSpace(resposta);
        }

        public static bool ValidaCombo(ComboBox combo)
        {
            return combo.SelectedIndex >= 0;
        }

        /// <summary>
        /// Validacoes de cadastro de conta.
        /// </summary>
        public static bool ValidaDataVencimento(string data)
        {
            if (!DateTime.TryParse(data, out DateTime dataConvertida))
                return false;

            if (dataConvertida < DateTime.Today.AddYears(-120))
                return false;

            return true;
        }

        public static bool ValidaNomeConta(string nomeConta)
        {
            return !string.IsNullOrWhiteSpace(nomeConta) && nomeConta.Trim().Length >= 2;
        }

        public static bool ValidaNomeCliente(string nomeCliente)
        {
            return !string.IsNullOrWhiteSpace(nomeCliente) && nomeCliente.Trim().Length >= 2;
        }

        public static bool ValidaValor(string valor)
        {
            // Tenta com vírgula primeiro
            if (decimal.TryParse(valor, NumberStyles.Number, new CultureInfo("pt-BR"), out decimal resultado))
                return resultado > 0;

            // Se falhar, tenta com ponto
            if (decimal.TryParse(valor, NumberStyles.Number, CultureInfo.InvariantCulture, out resultado))
                return resultado > 0;

            return false;
        }

        public static bool ValidaTipoConta(ComboBox combo)
        {
            return combo.SelectedIndex >= 0;
        }

        /// Validacoes de login de usuario.
        internal static bool ValidaLoginEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string padrao = @"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z]{2,}$";
            return Regex.IsMatch(email, padrao, RegexOptions.IgnoreCase);
        }

        internal static bool ValidaLoginSenha(string senha)
        {
            return !string.IsNullOrWhiteSpace(senha) && senha.Length >= 6;
        }
    }
}
