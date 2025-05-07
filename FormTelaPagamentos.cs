using projetointegrador.Classes;
using projetointegrador.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Linq;
using System.ComponentModel;

namespace projetointegrador
{
    public partial class FrmTelaPagamentos : Form
    {
        private Dictionary<Button, Panel> mapaBotoesPaineis;
        private Color corAtiva = Color.White;
        private Color corInativa = Color.Black;
        private Button botaoAtivo = null;
        private Dictionary<Button, Panel> mapaBotoesPainelVisual;
        private Dictionary<Button, Color> coresAtivas;
        private Eventos eventos;
        private ErrorProvider errorProvider = new ErrorProvider();
        private string nomeOriginal;
        private string emailOriginal;
        private Color _confirmDefaultBack;
        private Color _confirmDefaultFore;

        public FrmTelaPagamentos()
        {
            InitializeComponent();
            errorProvider.BlinkRate = 0;
            InicializarEventos();    // Registra eventos iniciais, como o Load
            InicializarPanels();     // Adiciona o evento de clique aos painéis de entrada de dados
            dataGridViewContas.SetDoubleBuffered(true);
            // guarda o estilo original
            _confirmDefaultBack = btnConfirmarAlteracao.BackColor;
            _confirmDefaultFore = btnConfirmarAlteracao.ForeColor;

            var dummy = dataGridViewContas.TopLeftHeaderCell;
            var dummy2 = dataGridViewHistorico.TopLeftHeaderCell;
        }

        // Adiciona o evento de carregamento do formulário
        private void InicializarEventos()
        {
            this.Load += FrmTelaPagamentos_Load;
            eventos = new Eventos();
        }

        // Adiciona eventos de clique aos painéis de entrada, para focar automaticamente no campo interno
        private void InicializarPanels()
        {
            List<Panel> listaPanels = new List<Panel>
            {
                panelNomeCompleto, panelPerfilEmail, panelSenhaAtual, panelNovaSenha, panelConfirmeSenha
            };

            foreach (Panel panel in listaPanels)
            {
                panel.Click += eventos.PanelClique;
            }
        }

        // Evento disparado quando o formulário carrega
        private void FrmTelaPagamentos_Load(object sender, EventArgs e)
        {
            ArredondarPaineis();       // Aplica cantos arredondados aos painéis
            InicializarMapeamentos(); // Mapeia os botões com os painéis e cores
            AtivarBotao(btnDashboard); // Define o botão inicial como ativo (Dashboard)          
            CarregarContas(); // Carrega as contas no DataGridView
            CarregarHistorico(); // Carrega o historico de  contas no DataGridView;
            Dashboard();
            Relatorios();

            dataGridViewContas.Sort(dataGridViewContas.Columns["Id"], ListSortDirection.Descending);
            dataGridViewHistorico.Sort(dataGridViewHistorico.Columns["IdHistorico"], ListSortDirection.Descending);

            // Certifica-se de que o usuário está logado
            if (Sessao.UsuarioLogado != null)
            {
                var usuario = Database.BuscarDadosUsuario(Sessao.UsuarioLogado.Id);
                if (usuario != null)
                {
                    txtPerfilNome.Text = usuario.Nome;
                    txtPerfilEmail.Text = usuario.Email;
                    txtSenhaAtual.Text = usuario.Senha;
                }
            }

            dataGridViewContas.ClearSelection();
        }

        // Aplica cantos arredondados aos painéis da interface
        private void ArredondarPaineis()
        {
            var paineisSemBorda = new List<Panel>
            {
                panelContas, panelDashboard, panelHistorico, panelRelatorios,
                panelPerfilUsuario, panelPesquisarConta, panelUsuario
            };

            var paineisComBorda = new List<Panel>
            {
                panelNomeCompleto, panelPerfilEmail, panelSenhaAtual,
                panelNovaSenha, panelConfirmeSenha
            };

            UIHelperSemBorda.ArredondarPaineis(paineisSemBorda, 4);
            UIHelperComBorda.ArredondarPaineis(paineisComBorda, 8);
        }

        // Cria os dicionários que ligam botões aos painéis correspondentes e definem as cores ativas
        private void InicializarMapeamentos()
        {
            mapaBotoesPaineis = new Dictionary<Button, Panel>
            {
                { btnDashboard, telaDashboard },
                { btnContas, TelaContas },
                { btnRelatorios, TelaRelatorios },
                { btnHistorico, TelaHistorico },
                { btnPerfildoUsuario, TelaUsuario }
            };

            mapaBotoesPainelVisual = new Dictionary<Button, Panel>
            {
                { btnDashboard, panelDashboard },
                { btnContas, panelContas },
                { btnRelatorios, panelRelatorios },
                { btnHistorico, panelHistorico },
                { btnPerfildoUsuario, panelPerfilUsuario }
            };

            coresAtivas = new Dictionary<Button, Color>
            {
                { btnDashboard, Color.FromArgb(198, 60, 69) },
                { btnContas, Color.FromArgb(138, 199, 220) },
                { btnRelatorios, Color.FromArgb(198, 60, 69) },
                { btnHistorico, Color.FromArgb(232, 189, 59) },
                { btnPerfildoUsuario, Color.FromArgb(10, 52, 85) }
            };
        }

        private void AtivarBotao(Button botao)
        {
            if (botaoAtivo != null)
            {
                // Reseta o botão anterior para o estilo inativo
                botaoAtivo.ForeColor = corInativa;
                botaoAtivo.Image = UIHelperColorIcon.TransformarIconeCor(botaoAtivo.Image, corInativa);
                mapaBotoesPainelVisual[botaoAtivo].BackColor = Color.White;

                // Esconde todos os painéis
                foreach (var painel in mapaBotoesPaineis.Values)
                    painel.Visible = false;
            }

            // Ativa o botão clicado
            botaoAtivo = botao;
            botaoAtivo.ForeColor = corAtiva;
            botaoAtivo.Image = UIHelperColorIcon.TransformarIconeCor(botao.Image, corAtiva);
            mapaBotoesPainelVisual[botao].BackColor = coresAtivas[botao];

            // Exibe o painel correspondente e traz para frente
            var painelSelecionado = mapaBotoesPaineis[botao];

            // Usando BeginInvoke para garantir que a operação ocorra depois da finalização do redimensionamento
            this.BeginInvoke(new Action(() =>
            {
                painelSelecionado.Visible = true;
                painelSelecionado.BringToFront();
            }));
        }

        // Evento de clique dos botões do menu lateral
        private void BtnMenu_Click(object sender, EventArgs e)
        {
            if (sender is Button botao)
            {
                AtivarBotao(botao);
            }
        }

        // Abre o formulário de cadastro de conta com um overlay escuro por trás
        private void btnAdicionarConta_Click(object sender, EventArgs e)
        {
            using (FormOverlay overlay = new FormOverlay(this))
            {
                overlay.Show();

                using (FrmCadastroConta cadastro = new FrmCadastroConta())
                {
                    // ASSINA O EVENTO AQUI 
                    cadastro.ContaSalvaComSucesso += Cadastro_ContaSalvaComSucesso;

                    cadastro.StartPosition = FormStartPosition.CenterScreen;
                    cadastro.ShowDialog();
                }
                overlay.Hide();
            }
        }

        private void Cadastro_ContaSalvaComSucesso(object sender, EventArgs e)
        {
            CarregarContas(); // Atualiza a DataGridView com as novas contas salvas
            AtualizarGrafico();                    // seu pie de categorias
            AtualizarGraficoReceitaDespesaPie();   // novo pie de Receita×Despesa
            Relatorios();

            dataGridViewContas.Sort(dataGridViewContas.Columns["Id"], ListSortDirection.Descending);
            dataGridViewHistorico.Sort(dataGridViewHistorico.Columns["IdHistorico"], ListSortDirection.Descending);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridViewContas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione uma conta para editar.",
                                 "Atenção",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Warning);
                return;
            }

            // Obtenção da primeira linha selecionada
            DataGridViewRow row = dataGridViewContas.SelectedRows[0];

            // Criando um objeto explicito do tipo ContaModel
            ContaModel conta = new ContaModel
            {
                Id = Convert.ToInt32(row.Cells["Id"].Value),
                NomeCliente = row.Cells["Cliente"].Value.ToString(),
                DataVencimento = DateTime.ParseExact(
                                     row.Cells["DataVencimento"].Value.ToString(),
                                     "dd/MM/yyyy",
                                     CultureInfo.InvariantCulture),
                NomeConta = row.Cells["NomeConta"].Value.ToString(),
                ValorConta = Convert.ToDecimal(row.Cells["valor"].Value),
                TipoConta = row.Cells["Tipo"].Value.ToString(),
                Categoria = row.Cells["Categoria"].Value.ToString(),
                Descricao = row.Cells["Descricao"].Value.ToString(),
                Situacao = row.Cells["Status"].Value.ToString()
            };

            // Utilizando o formulário FrmCadastroConta explicitamente
            using (FrmCadastroConta frm = new FrmCadastroConta(conta))
            {
                frm.ContaSalvaComSucesso += Cadastro_ContaSalvaComSucesso;
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
        }

        public void CarregarContas()
        {
            List<ContaModel> contas = Database.BuscarContasNaoPagas(Models.Sessao.UsuarioLogado.Id);
            dataGridViewContas.Rows.Clear();

            foreach (ContaModel conta in contas)
            {
                int rowIndex = dataGridViewContas.Rows.Add();
                DataGridViewRow row = dataGridViewContas.Rows[rowIndex];

                row.Cells["Id"].Value = conta.Id;
                row.Cells["Cliente"].Value = conta.NomeCliente;
                row.Cells["DataVencimento"].Value = conta.DataVencimento.ToShortDateString();
                row.Cells["NomeConta"].Value = conta.NomeConta;
                row.Cells["Valor"].Value = conta.ValorConta;
                row.Cells["Tipo"].Value = conta.TipoConta;
                row.Cells["Categoria"].Value = conta.Categoria;
                row.Cells["Descricao"].Value = conta.Descricao;
                row.Cells["Status"].Value = conta.Situacao;
            }
        }

        public void CarregarContasComFiltro(string filtro)
        {
            List<ContaModel> contas = Database.BuscarContasNaoPagasComFiltro(Models.Sessao.UsuarioLogado.Id, filtro);
            dataGridViewContas.Rows.Clear();

            foreach (ContaModel conta in contas)
            {
                int rowIndex = dataGridViewContas.Rows.Add();
                DataGridViewRow row = dataGridViewContas.Rows[rowIndex];

                row.Cells["Id"].Value = conta.Id;
                row.Cells["Cliente"].Value = conta.NomeCliente;
                row.Cells["DataVencimento"].Value = conta.DataVencimento.ToShortDateString();
                row.Cells["NomeConta"].Value = conta.NomeConta;
                row.Cells["Valor"].Value = conta.ValorConta;
                row.Cells["Tipo"].Value = conta.TipoConta;
                row.Cells["Categoria"].Value = conta.Categoria;
                row.Cells["Descricao"].Value = conta.Descricao;
                row.Cells["Status"].Value = conta.Situacao;
            }
        }

        private void CarregarHistorico()
        {
            List<ContaModel> contasPagas = Database.BuscarContasPagas(Models.Sessao.UsuarioLogado.Id);
            dataGridViewHistorico.Rows.Clear();

            foreach (ContaModel conta in contasPagas)
            {
                int rowIndex = dataGridViewHistorico.Rows.Add();
                DataGridViewRow row = dataGridViewHistorico.Rows[rowIndex];

                row.Cells["IdHistorico"].Value = conta.Id;
                row.Cells["ClienteHistorico"].Value = conta.NomeCliente;
                row.Cells["DataVencimentoHistorico"].Value = conta.DataVencimento.ToShortDateString();
                row.Cells["NomeContaHistorico"].Value = conta.NomeConta;
                row.Cells["ValorHistorico"].Value = conta.ValorConta;
                row.Cells["TipoHistorico"].Value = conta.TipoConta;
                row.Cells["CategoriaHistorico"].Value = conta.Categoria;
                row.Cells["DescricaoHistorico"].Value = conta.Descricao;

                row.Cells["StatusHistorico"].Value = "Pago";
            }
        }

        public void CarregarHistoricoComFiltro(string filtro)
        {
            List<ContaModel> historicoFiltrado = Database.BuscarHistoricoComFiltro(Models.Sessao.UsuarioLogado.Id, filtro);
            dataGridViewHistorico.Rows.Clear();

            foreach (ContaModel conta in historicoFiltrado)
            {
                int rowIndex = dataGridViewHistorico.Rows.Add();
                var row = dataGridViewHistorico.Rows[rowIndex];

                row.Cells["IdHistorico"].Value = conta.Id;
                row.Cells["ClienteHistorico"].Value = conta.NomeCliente;
                row.Cells["DataVencimentoHistorico"].Value = conta.DataVencimento.ToShortDateString();
                row.Cells["NomeContaHistorico"].Value = conta.NomeConta;
                row.Cells["ValorHistorico"].Value = conta.ValorConta;
                row.Cells["TipoHistorico"].Value = conta.TipoConta;
                row.Cells["CategoriaHistorico"].Value = conta.Categoria;
                row.Cells["DescricaoHistorico"].Value = conta.Descricao;
                row.Cells["StatusHistorico"].Value = "Pago";
            }
        }

        // Fecha a aplicação quando esta tela for encerrada
        private void FrmTelaPagamentos_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void SuccessTimer_Tick(object sender, EventArgs e)
        {
            txtAlteracao.Visible = false; // Esconde a mensagem
            successTimer.Stop(); // Para o Timer
        }

        private void ErrorTimer_Tick(object sender, EventArgs e)
        {
            errorProvider.SetError(txtPerfilNome, string.Empty);
            errorProvider.SetError(txtPerfilEmail, string.Empty);
            errorProvider.SetError(txtNovaSenha, string.Empty);

            btnAlterarNome.Visible = true;
            btnAlterarEmail.Visible = true;

            errorTimer.Stop();
        }

        private void btnEsconderSenha_Click(object sender, EventArgs e)
        {
            if (txtSenhaAtual.PasswordChar == '\0')
            {
                btnVerSenha.BringToFront();
                txtSenhaAtual.PasswordChar = '*';
            }
        }

        private void btnVerSenha_Click(object sender, EventArgs e)
        {
            if (txtSenhaAtual.PasswordChar == '*')
            {
                btnEsconderSenha.BringToFront();
                txtSenhaAtual.PasswordChar = '\0';
            }
        }

        private void btnEsconderNovaSenha_Click(object sender, EventArgs e)
        {
            if (txtNovaSenha.PasswordChar == '\0')
            {
                btnVerNovaSenha.BringToFront();
                txtNovaSenha.PasswordChar = '*';
            }
        }

        private void btnVerNovaSenha_Click(object sender, EventArgs e)
        {
            if (txtNovaSenha.PasswordChar == '*')
            {
                btnEsconderNovaSenha.BringToFront();
                txtNovaSenha.PasswordChar = '\0';
            }
        }

        private void btnVerConfirmarSenha_Click(object sender, EventArgs e)
        {
            if (txtConfirmeSenha.PasswordChar == '*')
            {
                txtConfirmeSenha.PasswordChar = '\0';
                btnEsconderConfirmarSenha.BringToFront();
            }
        }

        private void btnEsconderConfirmarSenha_Click(object sender, EventArgs e)
        {
            if (txtConfirmeSenha.PasswordChar == '\0')
            {
                txtConfirmeSenha.PasswordChar = '*';
                btnVerConfirmarSenha.BringToFront();
            }
        }

        private void btnAlterarNome_Click(object sender, EventArgs e)
        {
            nomeOriginal = txtPerfilNome.Text;

            // Libera o campo para edição
            txtPerfilNome.ReadOnly = false;
            txtPerfilNome.Enabled = true;

            // Limpa o campo para que o usuário digite um novo nome
            txtPerfilNome.Clear();

            // Define o foco no campo
            txtPerfilNome.Focus();

            // Torna o botão de confirmação visível
            btnConfirmarNome.Visible = true;
            btnAlterarNome.Visible = false;
        }

        private void btnConfirmarNome_Click(object sender, EventArgs e)
        {
            if (!Validacoes.ValidaNome(txtPerfilNome.Text))
            {
                // Se a validação falhar, exibe a mensagem de erro
                errorProvider.SetError(txtPerfilNome, "Nome inválido! Insira pelo menos 2 caracteres.");
                errorTimer.Start();

                // Restaura o nome original no campo
                txtPerfilNome.Text = nomeOriginal;

                // Bloqueia novamente o campo para evitar edição
                txtPerfilNome.ReadOnly = true;
                txtPerfilNome.Enabled = false;

                // Oculta o botão de confirmação, pois a alteração não foi aceita
                btnConfirmarNome.Visible = false;

            }
            else
            {
                // Se a validação passar, limpa o erro
                errorProvider.SetError(txtPerfilNome, string.Empty);

                // Atualiza o nomeOriginal com o novo nome digitado
                nomeOriginal = txtPerfilNome.Text;

                // Bloqueia o campo e desabilita a edição
                txtPerfilNome.ReadOnly = true;
                txtPerfilNome.Enabled = false;

                // Oculta o botão de confirmação, pois o novo nome já está confirmado
                btnConfirmarNome.Visible = false;
                btnAlterarNome.Visible = true;

                btnConfirmarAlteracao.BackColor = Color.FromArgb(10, 52, 85);
                btnConfirmarAlteracao.ForeColor = Color.White;
                btnConfirmarAlteracao.Enabled = true;
            }
        }

        private void btnAlterarEmail_Click(object sender, EventArgs e)
        {
            emailOriginal = txtPerfilEmail.Text;

            // Libera o campo para edição
            txtPerfilEmail.ReadOnly = false;
            txtPerfilEmail.Enabled = true;

            // Limpa o campo para novo valor
            txtPerfilEmail.Clear();

            // Define o foco no campo
            txtPerfilEmail.Focus();

            // Mostra o botão de confirmação e oculta o de alteração
            btnConfirmarEmail.Visible = true;
            btnAlterarEmail.Visible = false;
        }

        private void btnConfirmarEmail_Click(object sender, EventArgs e)
        {
            if (!Validacoes.ValidaEmail(txtPerfilEmail.Text))
            {
                // Exibe mensagem de erro
                errorProvider.SetError(txtPerfilEmail, "Email inválido! Insira um email válido.");
                errorTimer.Start();

                // Restaura o valor original
                txtPerfilEmail.Text = emailOriginal;

                // Bloqueia novamente o campo
                txtPerfilEmail.ReadOnly = true;
                txtPerfilEmail.Enabled = false;

                // Oculta o botão de confirmação
                btnConfirmarEmail.Visible = false;
            }
            else
            {
                // Limpa erro
                errorProvider.SetError(txtPerfilEmail, string.Empty);

                // Atualiza o valor original
                emailOriginal = txtPerfilEmail.Text;

                // Bloqueia novamente o campo
                txtPerfilEmail.ReadOnly = true;
                txtPerfilEmail.Enabled = false;

                // Oculta o botão de confirmação e mostra o botão de alteração
                btnConfirmarEmail.Visible = false;
                btnAlterarEmail.Visible = true;

                btnConfirmarAlteracao.BackColor = Color.FromArgb(10, 52, 85);
                btnConfirmarAlteracao.ForeColor = Color.White;
                btnConfirmarAlteracao.Enabled = true;
            }
        }

        private void txtNovaSenha_TextChanged(object sender, EventArgs e)
        {
            ValidarSenhas();

            if (txtNovaSenha.Text.Length > 0)
            {
                btnVerNovaSenha.Visible = true;
                btnEsconderNovaSenha.Visible = true;
            }
            else
            {
                btnVerNovaSenha.Visible = false;
                btnEsconderNovaSenha.Visible = false;
            }
        }

        private void txtConfirmeSenha_TextChanged(object sender, EventArgs e)
        {
            ValidarSenhas();
            if (txtNovaSenha.Text.Length > 0)
            {
                btnVerConfirmarSenha.Visible = true;
                btnEsconderConfirmarSenha.Visible = true;
            }
            else
            {
                btnVerConfirmarSenha.Visible = false;
                btnEsconderConfirmarSenha.Visible = false;
            }
        }

        private void ValidarSenhas()
        {
            bool senhaValida = Validacoes.ValidaSenha(txtNovaSenha.Text);
            bool senhasIguais = txtNovaSenha.Text == txtConfirmeSenha.Text;

            // Validação da nova senha
            if (!senhaValida)
            {
                errorProvider.SetError(txtNovaSenha, "A senha deve atender aos requisitos mínimos.");
            }
            else
            {
                errorProvider.SetError(txtNovaSenha, "");
            }

            // Validação da confirmação
            if (!senhasIguais)
            {
                errorProvider.SetError(txtConfirmeSenha, "As senhas não coincidem.");
            }
            else
            {
                errorProvider.SetError(txtConfirmeSenha, "");
            }

            // Habilita ou desabilita o botão de confirmação
            if (senhaValida && senhasIguais)
            {
                btnConfirmarAlteracao.BackColor = Color.FromArgb(10, 52, 85);
                btnConfirmarAlteracao.ForeColor = Color.White;
                btnConfirmarAlteracao.Enabled = true;
            }
            else
            {
                btnConfirmarAlteracao.BackColor = Color.White;
                btnConfirmarAlteracao.ForeColor = Color.FromArgb(10, 52, 85);
                btnConfirmarAlteracao.Enabled = false;
            }
        }

        private void btnConfirmarAlteracao_Click(object sender, EventArgs e)
        {
            // Verificar se os botões de confirmação de nome ou email estão visíveis
            if (btnConfirmarNome.Visible || btnConfirmarEmail.Visible)
            {
                // Exibir mensagem de erro e impedir alteração
                txtAlteracao.Text = "Você não pode alterar o nome ou email enquanto os botões de confirmação estiverem visíveis.";
                txtAlteracao.ForeColor = Color.Red;
                txtAlteracao.Visible = true; // Torna o Label visível
                return; // Interrompe a execução do método
            }

            // Criação do modelo de usuário
            UsuarioModel usuarioAlterado = new UsuarioModel
            {
                Nome = txtPerfilNome.Text,
                Email = txtPerfilEmail.Text,
                Senha = txtConfirmeSenha.Text
            };

            // Tentando atualizar o usuário
            bool sucesso = Database.AtualizarUsuarioDinamico(usuarioAlterado);

            if (sucesso)
            {
                // Mensagem de sucesso
                txtAlteracao.Text = "Dados atualizados com sucesso!";
                txtAlteracao.ForeColor = Color.Green; // Cor verde para sucesso
                txtAlteracao.Visible = true; // Torna o Label visível

                successTimer.Start(); // Inicia o timer para esconder após 3 segundos
                ResetConfirmButton();
            }
            else
            {
                // Mensagem de erro
                txtAlteracao.Text = "Erro ao atualizar os dados!";
                txtAlteracao.ForeColor = Color.Red; // Cor vermelha para erro
                txtAlteracao.Visible = true; // Torna o Label visível

                errorTimer.Start(); // Inicia o timer de erro
            }
        }

        private void ResetConfirmButton()
        {
            btnConfirmarAlteracao.Enabled = false;
            btnConfirmarAlteracao.BackColor = _confirmDefaultBack;
            btnConfirmarAlteracao.ForeColor = _confirmDefaultFore;
        }

        private void dataGridViewContas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewContas.Rows[e.RowIndex];

                string id = row.Cells[0].Value.ToString();
                string nomecliente = row.Cells[1].Value.ToString();
                string datavencimento = row.Cells[2].Value.ToString();
                string nomeconta = row.Cells[3].Value.ToString();
                string valor = row.Cells[4].Value.ToString();
                string tipo = row.Cells[5].Value.ToString();
                string categoria = row.Cells[6].Value.ToString();
                string descricao = row.Cells[7].Value.ToString();
                string status = row.Cells[8].Value.ToString();

                // Ativar botões
                btnPagar.Enabled = true;
                btnEditar.Enabled = true;
                btnApagar.Enabled = true;

                // Mudar cor de fundo dos botões
                Color fundo = Color.FromArgb(10, 52, 85);
                btnPagar.BackColor = fundo;
                btnEditar.BackColor = fundo;
                btnApagar.BackColor = fundo;

                // Adicionar ícones (você precisa ter essas imagens no seu projeto!)
                btnPagar.Enabled = true;
                btnPagar.BackColor = Color.FromArgb(10, 52, 85);
                btnPagar.Image = RecolorImage(btnPagar.Image, ColorTranslator.FromHtml("#10B981"));

                btnEditar.Enabled = true;
                btnEditar.BackColor = Color.FromArgb(10, 52, 85);
                btnEditar.Image = RecolorImage(btnEditar.Image, ColorTranslator.FromHtml("#5898A0"));

                btnApagar.Enabled = true;
                btnApagar.BackColor = Color.FromArgb(10, 52, 85);
                btnApagar.Image = RecolorImage(btnApagar.Image, ColorTranslator.FromHtml("#C63C45"));
            }
        }

        private Image RecolorImage(Image original, Color newColor)
        {
            Bitmap recolored = new Bitmap(original.Width, original.Height);
            using (Graphics g = Graphics.FromImage(recolored))
            {
                var colorMatrix = new System.Drawing.Imaging.ColorMatrix(new float[][]
                {
            new float[] {0, 0, 0, 0, 0},
            new float[] {0, 0, 0, 0, 0},
            new float[] {0, 0, 0, 0, 0},
            new float[] {0, 0, 0, 1, 0},
            new float[] {
                newColor.R / 255f,
                newColor.G / 255f,
                newColor.B / 255f,
                0, 1
            }
                });

                var attributes = new System.Drawing.Imaging.ImageAttributes();
                attributes.SetColorMatrix(colorMatrix);

                g.DrawImage(original,
                    new Rectangle(0, 0, recolored.Width, recolored.Height),
                    0, 0, original.Width, original.Height,
                    GraphicsUnit.Pixel,
                    attributes);
            }
            return recolored;
        }

        private void Control_MouseDown(object sender, MouseEventArgs e)
        {
            // Se clicou fora do DataGridView, desmarcar a seleção
            if (!dataGridViewContas.Bounds.Contains(e.Location))
            {
                ResetarSelecao();
            }
        }

        private void TelaContas_MouseDown(object sender, MouseEventArgs e)
        {
            // Se clicou fora do DataGridView, desmarcar
            if (!dataGridViewContas.Bounds.Contains(e.Location))
            {
                dataGridViewContas.ClearSelection();
                ResetarSelecao();
            }
        }

        private void ResetarSelecao()
        {
            dataGridViewContas.ClearSelection();

            // Resetar os botões
            btnPagar.Enabled = false;
            btnPagar.BackColor = SystemColors.Control;
            btnPagar.Image = Properties.Resources.checkgray;

            btnEditar.Enabled = false;
            btnEditar.BackColor = SystemColors.Control;
            btnEditar.Image = Properties.Resources.pencilgray;

            btnApagar.Enabled = false;
            btnApagar.BackColor = SystemColors.Control;
            btnApagar.Image = Properties.Resources.trashcangray;
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridViewContas.SelectedRows[0];
            string idConta = row.Cells[0].Value.ToString();

            DialogResult confirm = MessageBox.Show(
                "Tem certeza que deseja marcar esta conta como PAGA?",
                "Confirmar Pagamento",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
            {
                bool sucesso = Database.AtualizarSituacaoConta(idConta, "Pago");

                if (sucesso)
                {
                    row.Cells[8].Value = "Pago";
                    MessageBox.Show("Conta marcada como paga com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CarregarContas();
                    CarregarHistorico();
                }
                else
                {
                    MessageBox.Show("Falha ao atualizar o status da conta no banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtPesquisarConta_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtPesquisarConta.Text.Trim();

            if (!string.IsNullOrEmpty(filtro))
                CarregarContasComFiltro(filtro);
            else
                CarregarContas(); // volta pro modo sem filtro
        }

        private void txtPesquisarHistorico_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtPesquisarHistorico.Text.Trim();

            if (!string.IsNullOrEmpty(filtro))
                CarregarHistoricoComFiltro(filtro);
            else
                CarregarHistorico(); // volta ao histórico completo
        }

        private void dataGridViewContas_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Verifica se a coluna que estamos manipulando é a coluna "Situação"
            if (dataGridViewContas.Columns[e.ColumnIndex].Name == "Status")
            {
                // Verifica se o valor da célula é "Atrasada"
                if (e.Value != null && e.Value.ToString() == "Atrasada")
                {
                    // Altera a cor do texto para a cor personalizada RGB(198, 48, 69) quando "Atrasada"
                    e.CellStyle.ForeColor = Color.FromArgb(198, 48, 69);
                }
                else
                {
                    // Restaura a cor padrão RGB(10, 52, 85) caso não seja "Atrasada"
                    e.CellStyle.ForeColor = Color.FromArgb(10, 52, 85);
                }
            }
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            // Confirmação
            DialogResult confirmacao = MessageBox.Show("Tem certeza que deseja apagar esta conta?",
                                                       "Confirmação",
                                                       MessageBoxButtons.YesNo,
                                                       MessageBoxIcon.Question);

            if (confirmacao == DialogResult.Yes)
            {
                // Pega o ID da conta selecionada
                int idConta = Convert.ToInt32(dataGridViewContas.SelectedRows[0].Cells["Id"].Value);

                // Chama método no banco
                bool apagado = Database.ApagarConta(idConta);

                if (apagado)
                {
                    MessageBox.Show("Conta apagada com sucesso.",
                                    "Sucesso",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    CarregarContas(); // Atualiza a lista após apagar
                }
                else
                {
                    MessageBox.Show("Erro ao apagar a conta.",
                                    "Erro",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
           "Tem certeza que deseja sair e voltar à tela de login?",
           "Confirmação",
           MessageBoxButtons.YesNo,
           MessageBoxIcon.Question
       );

            if (resultado == DialogResult.Yes)
            {
                Models.Sessao.UsuarioLogado = null;
                Application.Restart(); // Reinicia toda a aplicação
            }
        }

        // DASHBOARD
        private void Dashboard()
        {
            PreencherFiltros();
            PreencherClientes();
            comboVisualizacao.SelectedIndex = 0;
            comboTipo.SelectedIndex = 0;
            comboMes.SelectedIndex = DateTime.Now.Month - 1;
            AtualizarGrafico();
            AtualizarGraficoReceitaDespesaPie();   // desenha o segundo pie na inicialização
        }

        private void PreencherFiltros()
        {
            // Tipo e Visualização (via Designer, só ajusta índice)
            comboTipo.SelectedIndex = 0;
            comboVisualizacao.SelectedIndex = 0;

            // Mês (via Designer, só ajusta índice)
            if (comboMes.Items.Count >= DateTime.Now.Month)
                comboMes.SelectedIndex = DateTime.Now.Month - 1;

            // Ano (dinâmico) — aqui está CarregarAnos embutido
            comboAno.Items.Clear();
            foreach (string ano in Database.ObterAnosDisponiveis())
                comboAno.Items.Add(ano);
            comboAno.SelectedItem = DateTime.Now.Year.ToString();
        }

        private void FiltrosAlterados(object sender, EventArgs e)
        {
            if (comboAno.SelectedItem == null
                || comboTipo.SelectedItem == null
                || comboVisualizacao.SelectedItem == null)
                return;

            AtualizarGrafico();
            AtualizarGraficoReceitaDespesaPie();
        }

        private void AtualizarGrafico()
        {
            int ano = int.Parse(comboAno.SelectedItem.ToString());
            string tipo = comboTipo.SelectedItem.ToString();
            bool isAnual = comboVisualizacao.SelectedItem.ToString() == "Anual";
            int idUsuario = Models.Sessao.UsuarioLogado.Id;

            string nomeCliente = "Todos"; 
            if (comboCliente.SelectedItem != null)
            {
                nomeCliente = comboCliente.SelectedItem.ToString();
            }

            Dictionary<string, decimal> dados;
            if (isAnual)
            {
                dados = Database.ObterTotaisPorCategoriaAnual(ano, tipo, idUsuario, nomeCliente);
            }
            else
            {
                int mes = comboMes.SelectedIndex + 1;
                dados = Database.ObterTotaisPorCategoriaMensal(ano, mes, tipo, idUsuario, nomeCliente);
            }

            RenderizarGrafico(dados, ano, comboMes.SelectedIndex + 1, tipo, isAnual);
        }

        private void RenderizarGrafico(Dictionary<string, decimal> dados, int ano, int mes, string tipo, bool isAnual)
        {
            string titulo;
            if (isAnual)
                titulo = tipo + "s por Categoria - " + ano;
            else
            {
                DateTimeFormatInfo dtfi = new CultureInfo("pt-BR").DateTimeFormat;
                if (mes < 1 || mes > 12) mes = 1;
                string nomeMes = CultureInfo.CurrentCulture
                    .TextInfo
                    .ToTitleCase(dtfi.GetMonthName(mes));
                nomeMes = CultureInfo.CurrentCulture.TextInfo
                    .ToTitleCase(nomeMes.ToLower());
                titulo = tipo + "s por Categoria - " + ano + " " + nomeMes;
            }
            // assume que no Designer você criou um Title na posição 0
            chartCategoria.Titles[0].Text = titulo;

            // --- 2) Alimenta os pontos da série Pie que você criou no Designer ---
            Series pieSeries = chartCategoria.Series[0];
            pieSeries.Points.Clear();

            foreach (KeyValuePair<string, decimal> kv in dados)
            {
                int idx = pieSeries.Points.AddXY(kv.Key, kv.Value);
                pieSeries.Points[idx].LegendText =
                    kv.Key + " - R$ " + kv.Value.ToString("N2");
            }
        }

        private void AtualizarGraficoReceitaDespesaPie()
        {
            int ano = int.Parse(comboAno.SelectedItem.ToString());
            bool isAnual = comboVisualizacao.SelectedItem.ToString() == "Anual";
            int mes = comboMes.SelectedIndex + 1;
            int idUsuario = Models.Sessao.UsuarioLogado.Id;

            // Verifica se há um item selecionado no comboCliente
            string nomeCliente = "Todos";  // Valor padrão para "Todos os clientes"
            if (comboCliente.SelectedItem != null)
            {
                nomeCliente = comboCliente.SelectedItem.ToString();
            }

            // Agora passa o nome do cliente, mesmo que seja "Todos"
            Dictionary<int, decimal> recPeriodo = Database.ObterSomaPorPeriodo(
                ano,
                isAnual ? 0 : mes,
                "Receita",
                isAnual,
                idUsuario,
                nomeCliente); // Passando o nome do cliente

            Dictionary<int, decimal> despPeriodo = Database.ObterSomaPorPeriodo(
                ano,
                isAnual ? 0 : mes,
                "Despesa",
                isAnual,
                idUsuario,
                nomeCliente); // Passando o nome do cliente

            decimal totalRec = recPeriodo.Values.Sum();
            decimal totalDesp = despPeriodo.Values.Sum();

            Series s = chartReceitaDespesa.Series["PieRD"];
            s.Points.Clear();

            int iR = s.Points.AddXY("Receita", totalRec);
            s.Points[iR].LegendText = $"Receita – R$ {totalRec:N2}";

            int iD = s.Points.AddXY("Despesa", totalDesp);
            s.Points[iD].LegendText = $"Despesa – R$ {totalDesp:N2}";
        }

        private void PreencherClientes()
        {
            int idUsuario = Models.Sessao.UsuarioLogado.Id;

            comboCliente.Items.Add("Todos os clientes");

            // Obtém os clientes do banco de dados
            List<string> clientes = Database.ObterClientesDoUsuario(idUsuario);
            foreach (string cliente in clientes)
            {
                comboCliente.Items.Add(cliente);
            }

            // Define o valor inicial como "Todos os clientes"
            comboCliente.SelectedIndex = 0;

            // Forçar a atualização da interface do ComboBox (evitar problemas de UI)
            comboCliente.Refresh();
        }

        /* ======= Relatorios e Analises ==========*/

        private void Relatorios()
        {
            CarregarAnosDisponiveis();
            // força comboMesRelatorio para o mês atual
            if (comboMesRelatorio.Items.Count >= DateTime.Now.Month)
                comboMesRelatorio.SelectedIndex = DateTime.Now.Month - 1;

            AtualizarLabelsTotaisRelatorio();
        }

        private void comboAnoRelatorio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboAnoRelatorio.SelectedItem != null)
            {
                int anoSelecionado = int.Parse(comboAnoRelatorio.SelectedItem.ToString());
                CarregarGrafico(anoSelecionado);
            }
        }

        private void CarregarAnosDisponiveis()
        {
            try
            {
                List<int> anos = Database.GetAvailableYears();
                comboAnoRelatorio.Items.Clear();

                int anoAtual = DateTime.Now.Year;
                bool temAtual = anos.Contains(anoAtual);

                foreach (int ano in anos)
                    comboAnoRelatorio.Items.Add(ano.ToString());

                if (temAtual)
                    comboAnoRelatorio.SelectedItem = anoAtual.ToString();
                else if (comboAnoRelatorio.Items.Count > 0)
                    comboAnoRelatorio.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar anos: " + ex.Message);
            }
        }

        private void CarregarGrafico(int ano)
        {
            try
            {
                int idUsuario = Models.Sessao.UsuarioLogado.Id;  // <-- Pegando usuário logado

                chart1.Series["Receita"].Points.Clear();
                chart1.Series["Despesa"].Points.Clear();
                chart1.Titles[0].Text = "Rendimento Geral – " + ano;

                var valores = Database.GetMonthlyTotals(ano, idUsuario);  // <-- Agora filtrando por usuário

                foreach (var kv in valores)
                {
                    string label = $"{GetMesNome(kv.Key)}/{(ano % 100):00}";
                    chart1.Series["Receita"].Points.AddXY(label, kv.Value.receita);
                    chart1.Series["Despesa"].Points.AddXY(label, kv.Value.despesa);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message);
            }
        }

        private string GetMesNome(int mes)
        {
            string[] meses = { "Jan", "Fev", "Mar", "Abr", "Mai", "Jun",
                               "Jul", "Ago", "Set", "Out", "Nov", "Dez" };
            return meses[mes - 1];
        }

        private string GetMesNomeInteiro(int mes)
        {
            string[] meses = { "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho",
                               "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" };
            return meses[mes - 1];
        }

        private void comboMesRelatorio_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizarLabelsTotaisRelatorio();
        }

        private void AtualizarLabelsTotaisRelatorio()
        {
            // Só continua se ano e mês estiverem selecionados
            if (comboAnoRelatorio.SelectedItem == null || comboMesRelatorio.SelectedItem == null)
                return;

            int ano = int.Parse(comboAnoRelatorio.SelectedItem.ToString());
            int mes = comboMesRelatorio.SelectedIndex + 1;
            int idUsuario = Models.Sessao.UsuarioLogado.Id;
            string nomeCliente = comboCliente.SelectedItem?.ToString() ?? "Todos";

            // -- Receita --
            var dictRec = Database.ObterSomaPorPeriodo(
                ano, mes, "Receita", isAnual: false, idUsuario, nomeCliente);
            decimal totalRec = dictRec.Values.Sum();

            // -- Despesa --
            var dictDesp = Database.ObterSomaPorPeriodo(
                ano, mes, "Despesa", isAnual: false, idUsuario, nomeCliente);
            decimal totalDesp = dictDesp.Values.Sum();

            // Formata valores
            var cultura = new CultureInfo("pt-BR");
            string nomeMes = GetMesNomeInteiro(mes); // seu método para pegar nome do mês

            // Atualiza os labels de VALORES
            lblValorTotalReceita.Text = totalRec.ToString("C2", cultura);
            lblValorTotalDespesa.Text = totalDesp.ToString("C2", cultura);

            decimal saldo = totalRec - totalDesp;
            lblValorReceitaMenosDespesa.Text = saldo.ToString("C2", cultura);

            // Define a cor do label de saldo
            if (saldo >= 0)
                lblValorReceitaMenosDespesa.ForeColor = ColorTranslator.FromHtml("#10B981"); // Verde
            else
                lblValorReceitaMenosDespesa.ForeColor = ColorTranslator.FromHtml("#F43F5E");  // Vermelho

            // Atualiza os labels de TÍTULOS
            lblTotalReceita.Text = $"Receita Total – {nomeMes}";
            lblTotalDespesa.Text = $"Despesa Total – {nomeMes}";
            lblReceitaMenosDespesa.Text = $"Lucro ou Prejuízo – {nomeMes}";

            // Depois de mudar o texto, centraliza
            lblTotalReceita.Left = (panelTituloTotalReceita.Width - lblTotalReceita.Width) / 2;
            lblTotalDespesa.Left = (panelTituloTotalDespesa.Width - lblTotalDespesa.Width) / 2;
            lblReceitaMenosDespesa.Left = (PanelTituloReceitaMenosDespesa.Width - lblReceitaMenosDespesa.Width) / 2;

            lblValorTotalReceita.Left = (panelTotalReceita.Width - lblValorTotalReceita.Width) / 2;
            lblValorTotalDespesa.Left = (panelTotalDespesa.Width - lblValorTotalDespesa.Width) / 2;
            lblValorReceitaMenosDespesa.Left = (PanelReceitaMenosDespesa.Width - lblValorReceitaMenosDespesa.Width) / 2;

        }
    }
}
