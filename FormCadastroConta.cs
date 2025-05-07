using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using projetointegrador.Models;

namespace projetointegrador
{
    public partial class FrmCadastroConta : Form
    {
        #region Campos

        private Dictionary<Control, Label> mapaLabels = new Dictionary<Control, Label>();
        private ErrorProvider errorProvider = new ErrorProvider();
        private Eventos eventos;
        public event EventHandler ContaSalvaComSucesso;
        private ContaModel _contaEmEdicao;

        #endregion

        #region Construtor

        public FrmCadastroConta()
        {
            InitializeComponent();
            errorProvider.BlinkRate = 0;

            InicializarLabels();
            InicializarEventos();
            InicializarPanels();

            txtDataVencimento.TextChanged += eventos.FormatarData;
            txtDataVencimento.KeyPress += eventos.PermitirSomenteNumeros;
            txtValor.KeyPress += eventos.PermitirSomenteValor;
        }

        public FrmCadastroConta(ContaModel conta) : this()
        {
            _contaEmEdicao = conta;
        }

        #endregion

        #region Inicialização

        private void InicializarLabels()
        {
            mapaLabels.Add(txtDataVencimento, lblInformeDataVenc);
            mapaLabels.Add(txtNomeConta, lblInformeNomeConta);
            mapaLabels.Add(txtNomeCliente, lblInformeCliente);
            mapaLabels.Add(txtValor, lblInformeValor);
            mapaLabels.Add(txtDescricao, lblDescrevaConta);
            mapaLabels.Add(cbCategoria, lblInformeCategoria);
            mapaLabels.Add(cbTipoConta, lblInformeTipo);
        }

        private void InicializarEventos()
        {
            this.Load += FrmCadastroConta_Load;

            eventos = new Eventos(mapaLabels, errorProvider);
            eventos.AdicionarEventos(txtDataVencimento);
            eventos.AdicionarEventos(txtNomeConta);
            eventos.AdicionarEventos(txtNomeCliente);
            eventos.AdicionarEventos(txtValor);
            eventos.AdicionarEventos(txtDescricao);
            eventos.AdicionarEventos(cbCategoria);
            eventos.AdicionarEventos(cbTipoConta);
        }

        private void InicializarPanels()
        {
            List<Panel> listaPanels = new List<Panel>
            {
                panelNomeConta, panelCategoria, paneldataVencimento, panelDescricao, panelNomeCliente, panelTipo, panelValor
            };

            foreach (Panel panel in listaPanels)
            {
                panel.Click += eventos.PanelClique;
            }
        }

        private void FrmCadastroConta_Load(object sender, EventArgs e)
        {
            timerMensagemConta.Tick += TimerMensagemConta_Tick;

            List<Panel> listaDePaineis = new List<Panel>
            {
                panelCategoria, paneldataVencimento, panelNomeCliente, panelNomeConta, panelTipo, panelValor, panelDescricao
            };

            UIHelperSemBorda.ArredondarPaineis(listaDePaineis, 5);

            if (_contaEmEdicao != null)
            {
                txtNomeCliente.Text = _contaEmEdicao.NomeCliente;
                txtDataVencimento.Text = _contaEmEdicao.DataVencimento.ToString("dd/MM/yyyy");
                txtNomeConta.Text = _contaEmEdicao.NomeConta;
                txtValor.Text = _contaEmEdicao.ValorConta.ToString("F2");
                cbTipoConta.SelectedItem = _contaEmEdicao.TipoConta;
                cbCategoria.SelectedItem = _contaEmEdicao.Categoria;
                txtDescricao.Text = _contaEmEdicao.Descricao;
                btnSalvarCadastro.Text = "Atualizar";
                lblAdicionarConta.Text = "Editar Conta";
            }

            eventos.VerificarCamposComTexto();
        }

        #endregion

        #region Botões

        private void btnFecharCadastro_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelarCadastro_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSalvarCadastro_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();
            bool erro = false;

            #region Validações

            if (!Validacoes.ValidaDataVencimento(txtDataVencimento.Text))
            {
                errorProvider.SetError(txtDataVencimento, "Informe uma data de vencimento válida (ex: 25/04/2025).");
                erro = true;
            }

            if (!Validacoes.ValidaNomeConta(txtNomeConta.Text))
            {
                errorProvider.SetError(txtNomeConta, "Digite o nome da conta (ex: Luz, Água, Internet...).");
                erro = true;
            }

            if (!Validacoes.ValidaNomeCliente(txtNomeCliente.Text))
            {
                errorProvider.SetError(txtNomeCliente, "Informe o nome do cliente responsável por esta conta.");
                erro = true;
            }

            if (!Validacoes.ValidaValor(txtValor.Text))
            {
                errorProvider.SetError(txtValor, "Informe um valor válido (ex: 89,99).");
                erro = true;
            }

            if (!Validacoes.ValidaTipoConta(cbTipoConta))
            {
                errorProvider.SetError(cbTipoConta, "Selecione o tipo da conta (ex: Despesa ou Receita).");
                erro = true;
            }

            if (erro) return;

            #endregion

            #region Montagem do objeto

            ContaModel novaConta = new ContaModel
            {
                NomeCliente = txtNomeCliente.Text,
                DataVencimento = DateTime.Parse(txtDataVencimento.Text),
                NomeConta = txtNomeConta.Text,
                IdCadastro = Sessao.UsuarioLogado.Id
            };

            if (_contaEmEdicao != null)
            {
                novaConta.Id = _contaEmEdicao.Id; // Caso seja uma edição, setamos o Id
            }

            // Converte o valor para decimal
            string valorTexto = txtValor.Text.Trim();
            decimal valorDecimal;

            if (valorTexto.Contains(","))
                valorDecimal = decimal.Parse(valorTexto, new CultureInfo("pt-BR"));
            else if (valorTexto.Contains("."))
                valorDecimal = decimal.Parse(valorTexto, CultureInfo.InvariantCulture);
            else
                valorDecimal = decimal.Parse(valorTexto);

            novaConta.ValorConta = valorDecimal;

            // Definindo Tipo e Categoria
            if (cbTipoConta.SelectedItem == null)
            {
                novaConta.TipoConta = null;
                novaConta.Categoria = null;
            }
            else
            {
                string tipoSelecionado = cbTipoConta.SelectedItem.ToString();
                novaConta.TipoConta = tipoSelecionado;
                if (cbCategoria.SelectedItem == null || cbCategoria.SelectedItem.ToString() == "Nenhum")
                {
                    novaConta.Categoria = null;
                }
                else
                {
                    novaConta.Categoria = cbCategoria.SelectedItem.ToString();
                }
            }

            // Definindo Descrição
            if (string.IsNullOrWhiteSpace(txtDescricao.Text))
            {
                novaConta.Descricao = null;
            }
            else
            {
                novaConta.Descricao = txtDescricao.Text.Trim();
            }
            novaConta.Situacao = DateTime.Now.Date > novaConta.DataVencimento.Date ? "Atrasada" : "Pendente";

            #endregion

            #region Salvar no banco

            bool sucesso;
            if (novaConta.Id > 0)
            {
                sucesso = Database.AtualizarConta(novaConta); // Faz o UPDATE
            }
            else
            {
                sucesso = Database.SalvarConta(novaConta); // Faz o INSERT
            }

            if (sucesso)
            {
                LimparCamposConta();

                lblContaSalva.Text = "Conta salva com sucesso!";
                lblContaSalva.Visible = true;
                timerMensagemConta.Start();

                // Dispara o evento para notificar outros forms sobre o sucesso da conta salva
                ContaSalvaComSucesso?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show("Algo deu errado ao salvar a conta.");
            }

            #endregion
        }

        #endregion

        #region Métodos auxiliares

        private void TimerMensagemConta_Tick(object sender, EventArgs e)
        {
            lblContaSalva.Visible = false;
            timerMensagemConta.Stop();
        }

        private void LimparCamposConta()
        {
            txtNomeCliente.Clear();
            txtDataVencimento.Clear();
            txtNomeConta.Clear();
            txtValor.Clear();
            cbTipoConta.SelectedIndex = -1;
            cbCategoria.SelectedIndex = -1;
            txtDescricao.Clear();
        }

        #endregion

        #region Enter para Avançar

        private void txtDataVencimento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtNomeConta.Focus();
            }
        }

        private void txtNomeConta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtValor.Focus();
            }
        }

        private void txtValor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbTipoConta.Focus();
                cbTipoConta.DroppedDown = true;
            }
        }

        private void cbTipoConta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbCategoria.Focus();
                cbCategoria.DroppedDown = true;
            }
        }

        private void cbCategoria_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtNomeCliente.Focus();
            }
        }

        private void txtNomeCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtDescricao.Focus();
            }
        }

        private void txtDescricao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnSalvarCadastro.PerformClick();
            }
        }
        #endregion
    }
}


