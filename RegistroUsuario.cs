using projetointegrador.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace projetointegrador
{
    public partial class FrmCadastro : Form
    {
        #region Campos e Variáveis
        private List<string> perguntas;
        private Dictionary<Control, Label> mapaLabels = new Dictionary<Control, Label>();
        private ErrorProvider errorProvider = new ErrorProvider();
        private Eventos eventos;
        #endregion
        public FrmCadastro()
        {
            InitializeComponent();
            cbPrimeiraPergunta.SelectedIndexChanged += cbPrimeiraPergunta_SelectedIndexChanged;
            errorProvider.BlinkRate = 0;

            InicializarLabels();
            eventos = new Eventos(mapaLabels, errorProvider);
            InicializarEventos();
            InicializarPerguntas();
            InicializarPerguntasRecuperacao();
            InicializarPanels();
        }

        #region Inicialização de Componentes

        private void InicializarLabels()
        {
            mapaLabels.Add(cbPrimeiraPergunta, lblPrimeiraPergunta);
            mapaLabels.Add(cbSegundaPergunta, lblSegundaPergunta);
            mapaLabels.Add(txtPrimeiraResposta, lblPrimeiraResposta);
            mapaLabels.Add(txtSegundaResposta, lblSegundaResposta);
            mapaLabels.Add(txtNome, lblNome);
            mapaLabels.Add(txtEmail, lblEmail);
            mapaLabels.Add(txtSenha, lblSenha);
            mapaLabels.Add(txtConfirmarSenha, lblConfirmarSenha);
            mapaLabels.Add(txtEmailLogin, lblEmailLogin);
            mapaLabels.Add(txtSenhaLogin, lblSenhaLogin);
            mapaLabels.Add(txtInformarEmail, lblInformarEmail);
            mapaLabels.Add(cbRecuperar1Pergunta, lblRecuperar1Pergunta);
            mapaLabels.Add(cbRecuperar2Pergunta, lblRecuperar2Pergunta);
            mapaLabels.Add(txtRecuperar1Resposta, lblRecuperar1Resposta);
            mapaLabels.Add(txtRecuperar2Resposta, lblRecuperar2Resposta);
            mapaLabels.Add(txtNovaSenha, lblNovaSenha);
            mapaLabels.Add(txtConfirmeNovaSenha, lblConfirmeNovaSenha);

        }

        private void InicializarEventos()
        {
            this.Load += FrmCadastro_Load;

            eventos = new Eventos(mapaLabels, errorProvider);
            eventos.AdicionarEventos(txtPrimeiraResposta);
            eventos.AdicionarEventos(txtSegundaResposta);
            eventos.AdicionarEventos(cbPrimeiraPergunta);
            eventos.AdicionarEventos(cbSegundaPergunta);
            eventos.AdicionarEventos(txtNome);
            eventos.AdicionarEventos(txtEmail);
            eventos.AdicionarEventos(txtSenha);
            eventos.AdicionarEventos(txtConfirmarSenha);
            eventos.AdicionarEventos(txtEmailLogin);
            eventos.AdicionarEventos(txtSenhaLogin);
            eventos.AdicionarEventos(txtInformarEmail);
            eventos.AdicionarEventos(cbRecuperar1Pergunta);
            eventos.AdicionarEventos(cbRecuperar2Pergunta);
            eventos.AdicionarEventos(txtRecuperar1Resposta);
            eventos.AdicionarEventos(txtRecuperar2Resposta);
            eventos.AdicionarEventos(txtNovaSenha);
            eventos.AdicionarEventos(txtConfirmeNovaSenha);
        }

        private void InicializarPanels()
        {
            List<Panel> listaPanels = new List<Panel>
            {
                panelNome,
                panelEmail,
                panelSenha,
                panelConfirmar,
                panelPrimeiraPergunta,
                panelPrimeiraResposta,
                panelSegundaPergunta,
                panelSegundaResposta,
                panelEmailLogin,
                panelSenhaLogin,
                panelInformarEmail,
                panelRecuperar1Pergunta,
                panelRecuperar2Pergunta,
                panelRecuperar1Resposta,
                panelRecuperar1Resposta,
                panelNovaSenha,
                panelConfirmeNovaSenha
            };

            foreach (Panel panel in listaPanels)
            {
                panel.Click += eventos.PanelClique;
            }
        }

        private void InicializarPerguntas()
        {
            perguntas = new List<string>
            {
                "Qual era o nome do seu animal de estimação?",
                "Qual é o nome da cidade onde você nasceu?",
                "Qual é o seu filme favorito?",
                "Qual é o nome do seu livro favorito?",
                "Qual é o nome do seu cantor ou banda favorita?",
                "Qual é o seu prato favorito?"
            };

            cbPrimeiraPergunta.Items.AddRange(perguntas.ToArray());
            cbSegundaPergunta.Items.AddRange(perguntas.ToArray());
        }

        private void InicializarPerguntasRecuperacao()
        {
            cbRecuperar1Pergunta.Items.AddRange(perguntas.ToArray());
            cbRecuperar2Pergunta.Items.AddRange(perguntas.ToArray());
        }

        #endregion

        #region Eventos do Formulário

        private void FrmCadastro_Load(object sender, EventArgs e)
        {
            List<Panel> listaDePaineis = new List<Panel>()
            {
                panelNome, panelEmail, panelSenha, panelConfirmar,
                panelPrimeiraPergunta, panelPrimeiraResposta,
                panelSegundaPergunta, panelSegundaResposta, panelEmailLogin, panelSenhaLogin
            };

            UIHelperComBorda.ArredondarPaineis(listaDePaineis, 10);

            panelSeguranca.Location = panelConta.Location;
        }

        #endregion

        #region Validação e Registro

        private void btnProximo_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();
            bool erro = false;

            // Validações já existentes…
            if (!Validacoes.ValidaNome(txtNome.Text))
            {
                errorProvider.SetError(txtNome, "Espaço em branco. Por favor, informe seu nome");
                erro = true;
            }

            if (!Validacoes.ValidaEmail(txtEmail.Text))
            {
                errorProvider.SetError(txtEmail, "Formato de email inválido.");
                erro = true;
            }

            // *** Nova validação de existência de e-mail ***
            else if (Database.EmailExiste(txtEmail.Text))
            {
                errorProvider.SetError(txtEmail, "Este e-mail já está cadastrado.");
                erro = true;
            }

            // Continua com as demais…
            if (!Validacoes.ValidaSenha(txtSenha.Text))
            {
                errorProvider.SetError(txtSenha, "Senha inválida (deve ter 6 caracteres).");
                erro = true;
            }

            if (string.IsNullOrWhiteSpace(txtConfirmarSenha.Text))
            {
                errorProvider.SetError(txtConfirmarSenha, "Confirme sua senha.");
                erro = true;
            }
            else if (txtSenha.Text != txtConfirmarSenha.Text)
            {
                errorProvider.SetError(txtConfirmarSenha, "Senhas não coincidem.");
                erro = true;
            }

            if (erro) return;

            // Se tudo OK, avança de painel…
            btnConta.BackColor = Color.Transparent;
            btnSeguranca.BackColor = Color.White;
            panelConta.Visible = false;
            panelSeguranca.Visible = true;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();
            bool erro = false;

            if (!Validacoes.ValidaCombo(cbPrimeiraPergunta))
            {
                errorProvider.SetError(cbPrimeiraPergunta, "Selecione uma pergunta.");
                erro = true;
            }

            if (!Validacoes.ValidaCombo(cbSegundaPergunta))
            {
                errorProvider.SetError(cbSegundaPergunta, "Selecione uma pergunta.");
                erro = true;
            }

            if (!Validacoes.ValidaResposta(txtPrimeiraResposta.Text))
            {
                errorProvider.SetError(txtPrimeiraResposta, "Informe a resposta de segurança.");
                erro = true;
            }

            if (!Validacoes.ValidaResposta(txtSegundaResposta.Text))
            {
                errorProvider.SetError(txtSegundaResposta, "Informe a resposta de segurança.");
                erro = true;
            }

            if (erro) return;

            UsuarioModel novoUsuario = new UsuarioModel
            {
                Email = txtEmail.Text,
                Nome = txtNome.Text,
                Senha = txtSenha.Text,
                Pergunta_Rec_1 = cbPrimeiraPergunta.SelectedItem.ToString(),
                Pergunta_Rec_2 = cbSegundaPergunta.SelectedItem.ToString(),
                Resposta_Rec_1 = txtPrimeiraResposta.Text,
                Resposta_Rec_2 = txtSegundaResposta.Text
            };

            if (Database.SalvarUsuario(novoUsuario))
            {
                MessageBox.Show("Cadastro realizado com sucesso!");
                panelLogin.Visible = true;
                panelRegistro.Visible = false;
            }
            else
            {
                MessageBox.Show("Algo deu errado");
            }

            panelSeguranca.Visible = false;
            panelConta.Visible = true;
            btnSeguranca.BackColor = Color.Transparent;
            btnConta.BackColor = Color.White;
        }
        #endregion

        #region Login

        private void btnLogar_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();
            bool erro = false;

            string email = txtEmailLogin.Text;
            string senha = txtSenhaLogin.Text;

            if (!Validacoes.ValidaLoginEmail(email))
            {
                errorProvider.SetError(txtEmailLogin, "E-mail inválido.");
                erro = true;
            }

            if (!Validacoes.ValidaLoginSenha(senha))
            {
                errorProvider.SetError(txtSenhaLogin, "Senha inválida. Mínimo 6 caracteres.");
                erro = true;
            }

            if (erro) return;

            int idUsuario = Database.BuscarIdDoUsuario(email, senha);
            if (idUsuario != -1)
            {
                Sessao.UsuarioLogado = new UsuarioModel();
                Sessao.UsuarioLogado.Id = idUsuario;

                FrmTelaPagamentos pagamentos = new FrmTelaPagamentos();
                pagamentos.Show();

                this.Hide();
            }
            else
            {
                errorProvider.SetError(txtSenhaLogin, "E-mail ou senha incorretos..");
                errorProvider.SetError(txtEmailLogin, "E-mail ou senha incorretos..");
            }
        }

        #endregion

        #region ComboBox de Perguntas

        private void cbPrimeiraPergunta_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizarItensComboSegunda();
        }

        private void cbRecuperar1Pergunta_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizarRecuperarSegundaPergunta();
        }

        private void AtualizarRecuperarSegundaPergunta()
        {
            string perguntaSelecionada = cbRecuperar1Pergunta.SelectedItem as string;
            string selecaoAnterior = cbRecuperar2Pergunta.SelectedItem as string;

            cbRecuperar2Pergunta.Items.Clear();

            foreach (var pergunta in perguntas)
            {
                if (pergunta != perguntaSelecionada)
                {
                    cbRecuperar2Pergunta.Items.Add(pergunta);
                }
            }

            if (!string.IsNullOrEmpty(selecaoAnterior) &&
                cbRecuperar2Pergunta.Items.Contains(selecaoAnterior))
            {
                cbRecuperar2Pergunta.SelectedItem = selecaoAnterior;
            }
            else
            {
                cbRecuperar2Pergunta.SelectedIndex = -1;
            }
        }


        private void AtualizarItensComboSegunda()
        {
            string perguntaSelecionada = cbPrimeiraPergunta.SelectedItem as string;
            string selecaoAnterior = cbSegundaPergunta.SelectedItem as string;

            cbSegundaPergunta.Items.Clear();

            for (int i = 0; i < perguntas.Count; i++)
            {
                if (perguntas[i] != perguntaSelecionada)
                {
                    cbSegundaPergunta.Items.Add(perguntas[i]);
                }
            }

            if (!string.IsNullOrEmpty(selecaoAnterior) &&
                cbSegundaPergunta.Items.Contains(selecaoAnterior))
            {
                cbSegundaPergunta.SelectedItem = selecaoAnterior;
            }
            else
            {
                cbSegundaPergunta.SelectedIndex = -1;
            }
        }

        #endregion

        #region Navegação e Links

        private void btnConta_Click(object sender, EventArgs e)
        {
            btnConta.BackColor = Color.White;
            btnSeguranca.BackColor = Color.Transparent;

            panelConta.Visible = true;
            panelSeguranca.Visible = false;
        }

        private void btnSeguranca_BackColorChanged(object sender, EventArgs e)
        {
            if (btnSeguranca.BackColor == Color.White)
            {
                btnSeguranca.FlatAppearance.MouseOverBackColor = Color.LightGray;
                btnSeguranca.FlatAppearance.MouseDownBackColor = Color.LightGray;
            }
            else
            {
                btnSeguranca.FlatAppearance.MouseOverBackColor = Color.Transparent;
                btnSeguranca.FlatAppearance.MouseDownBackColor = Color.Transparent;
            }
        }

        private void linklblRegistrar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panelLogin.Visible = false;
            panelRegistro.Visible = true;
        }

        private void lblDesejaLogar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panelLogin.Visible = true;
            panelRegistro.Visible = false;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panelLogin.Visible = true;
            panelRegistro.Visible = false;
        }

        #endregion

        #region Recuperação de Senha
        private void linklblEsqueceuSenha_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panelLogin.Visible = false;
            panelRecConta.Visible = true;
        }

        private void linkLabelVoltarLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panelLogin.Visible = true;
            panelRecConta.Visible = false;
        }

        private void btnRecuperacaoProximo_Click(object sender, EventArgs e)
        {
            // Limpa possíveis erros anteriores
            errorProvider.Clear();

            bool erro = false;

            // Validação dos campos com ErrorProvider
            if (string.IsNullOrEmpty(txtInformarEmail.Text.Trim()))
            {
                errorProvider.SetError(txtInformarEmail, "Informe o e-mail.");
                erro = true;
            }

            if (cbRecuperar1Pergunta.SelectedIndex == -1)
            {
                errorProvider.SetError(cbRecuperar1Pergunta, "Selecione uma pergunta.");
                erro = true;
            }

            if (cbRecuperar2Pergunta.SelectedIndex == -1)
            {
                errorProvider.SetError(cbRecuperar2Pergunta, "Selecione uma pergunta.");
                erro = true;
            }

            if (string.IsNullOrEmpty(txtRecuperar1Resposta.Text.Trim()))
            {
                errorProvider.SetError(txtRecuperar1Resposta, "Informe a resposta.");
                erro = true;
            }

            if (string.IsNullOrEmpty(txtRecuperar2Resposta.Text.Trim()))
            {
                errorProvider.SetError(txtRecuperar2Resposta, "Informe a resposta.");
                erro = true;
            }

            if (erro)
            {
                return;
            }

            // Coleta os dados informados
            string email = txtInformarEmail.Text.Trim();
            string perguntaInput1 = cbRecuperar1Pergunta.SelectedItem as string;
            string respostaInput1 = txtRecuperar1Resposta.Text.Trim();
            string perguntaInput2 = cbRecuperar2Pergunta.SelectedItem as string;
            string respostaInput2 = txtRecuperar2Resposta.Text.Trim();

            // Chama o método para verificar os dados de recuperação no banco
            bool dadosValidos = Database.VerificarDadosRecuperacao(email, perguntaInput1, respostaInput1, perguntaInput2, respostaInput2);

            if (dadosValidos)
            {
                panelPasso1Recuperar.Visible = false;
                btnRecuperarSenha.BackColor = Color.Transparent;
                panelPasso2Recuperar.Visible = true;
                btnNovaSenha.BackColor = Color.White;
                lblSubtituloRecuperacao.Text = "Informe sua nova senha";
            }
            else
            {
                // Define os erros nos controles para indicar que os dados informados estão incorretos
                errorProvider.SetError(txtInformarEmail, "E-mail ou dados de segurança incorretos.");
                errorProvider.SetError(cbRecuperar1Pergunta, "Dados incorretos.");
                errorProvider.SetError(cbRecuperar2Pergunta, "Dados incorretos.");
                errorProvider.SetError(txtRecuperar1Resposta, "Dados incorretos.");
                errorProvider.SetError(txtRecuperar2Resposta, "Dados incorretos.");
            }
        }

        private void btnRecuperarSenha_Click(object sender, EventArgs e)
        {
            panelPasso1Recuperar.Visible = true;
            panelPasso2Recuperar.Visible = false;
            btnRecuperarSenha.BackColor = Color.White;
            btnNovaSenha.BackColor = Color.Transparent;
        }

        private void btnConfirmarRecuperacao_Click(object sender, EventArgs e)
        {
            errorProvider.Clear(); // limpa erros anteriores
            bool erro = false;

            if (!Validacoes.ValidaSenha(txtNovaSenha.Text))
            {
                errorProvider.SetError(txtNovaSenha, "Senha inválida (deve ter 6 caracteres).");
                erro = true;
            }

            if (string.IsNullOrWhiteSpace(txtConfirmeNovaSenha.Text))
            {
                errorProvider.SetError(txtConfirmeNovaSenha, "Confirme sua senha.");
                erro = true;
            }
            else if (txtNovaSenha.Text != txtConfirmeNovaSenha.Text)
            {
                errorProvider.SetError(txtConfirmeNovaSenha, "Senhas não coincidem.");
                erro = true;
            }

            if (erro) return;

            // Aqui você chama a atualização no banco de dados
            string novaSenha = txtNovaSenha.Text;
            string email = txtInformarEmail.Text.Trim(); // ou pegue o email salvo na recuperação

            bool atualizou = Database.AtualizarSenha(email, novaSenha);

            if (atualizou)
            {
                panelLogin.Visible = true;
                panelRecConta.Visible = false;
            }
            else
            {
                MessageBox.Show("Erro ao atualizar a senha.");
            }
        }
        #endregion

        #region Enter para Avançar

        // --- Login ---
        private void TxtEmailLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;    
                txtSenhaLogin.Focus();        
            }
        }

        private void TxtEmailSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;   
                btnLogar.PerformClick();      
            }
        }

        // --- Cadastro ---
        private void txtNome_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtEmail.Focus();
            }
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtSenha.Focus();
            }
        }

        private void txtSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtConfirmarSenha.Focus();
            }
        }

        private void txtConfirmarSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnProximo.PerformClick();

                // só faz se o panelSeguranca foi exibido
                if (panelSeguranca.Visible)
                {
                    cbPrimeiraPergunta.Focus();
                    cbPrimeiraPergunta.DroppedDown = true;
                }
            }
        }

        private void cbPrimeiraPergunta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtPrimeiraResposta.Focus();
            }
        }

        private void txtPrimeiraResposta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbSegundaPergunta.Focus();
                cbSegundaPergunta.DroppedDown = true;
            }
        }

        private void cbSegundaPergunta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtSegundaResposta.Focus();
            }
        }

        private void txtSegundaResposta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnRegistrar.PerformClick();
            }
        }

        // --- Recuperação de Senha ---
        private void txtInformarEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbRecuperar1Pergunta.Focus();
                cbRecuperar1Pergunta.DroppedDown = true;
            }
        }

        private void cbRecuperar1Pergunta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtRecuperar1Resposta.Focus();
            }
        }

        private void txtRecuperar1Resposta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbRecuperar2Pergunta.Focus();
                cbRecuperar2Pergunta.DroppedDown = true;
            }
        }

        private void cbRecuperar2Pergunta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtRecuperar2Resposta.Focus();
            }
        }

        private void txtRecuperar2Resposta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnRecuperacaoProximo.PerformClick();
            }
        }

        private void txtNovaSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtConfirmeNovaSenha.Focus();
            }
        }

        private void txtConfirmeNovaSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnConfirmarRecuperacao.PerformClick();
            }
        }

        #endregion
    }
}



