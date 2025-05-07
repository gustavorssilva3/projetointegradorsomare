namespace projetointegrador
{
    partial class FrmCadastroConta
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblAdicionarConta = new System.Windows.Forms.Label();
            this.btnFecharCadastro = new System.Windows.Forms.Button();
            this.lblDataVencimento = new System.Windows.Forms.Label();
            this.lblNomeConta = new System.Windows.Forms.Label();
            this.lblValor = new System.Windows.Forms.Label();
            this.lblTipoConta = new System.Windows.Forms.Label();
            this.lblCategoria = new System.Windows.Forms.Label();
            this.lblNomeCliente = new System.Windows.Forms.Label();
            this.lblDescricao = new System.Windows.Forms.Label();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.btnSalvarCadastro = new System.Windows.Forms.Button();
            this.btnCancelarCadastro = new System.Windows.Forms.Button();
            this.panelValor = new System.Windows.Forms.Panel();
            this.lblInformeValor = new System.Windows.Forms.Label();
            this.txtValor = new System.Windows.Forms.TextBox();
            this.panelTipo = new System.Windows.Forms.Panel();
            this.lblInformeTipo = new System.Windows.Forms.Label();
            this.cbTipoConta = new System.Windows.Forms.ComboBox();
            this.panelCategoria = new System.Windows.Forms.Panel();
            this.lblInformeCategoria = new System.Windows.Forms.Label();
            this.cbCategoria = new System.Windows.Forms.ComboBox();
            this.panelNomeCliente = new System.Windows.Forms.Panel();
            this.lblInformeCliente = new System.Windows.Forms.Label();
            this.txtNomeCliente = new System.Windows.Forms.TextBox();
            this.panelNomeConta = new System.Windows.Forms.Panel();
            this.lblInformeNomeConta = new System.Windows.Forms.Label();
            this.txtNomeConta = new System.Windows.Forms.TextBox();
            this.lblDescrevaConta = new System.Windows.Forms.Label();
            this.panelDescricao = new System.Windows.Forms.Panel();
            this.lblContaSalva = new System.Windows.Forms.Label();
            this.timerMensagemConta = new System.Windows.Forms.Timer(this.components);
            this.paneldataVencimento = new System.Windows.Forms.Panel();
            this.lblInformeDataVenc = new System.Windows.Forms.Label();
            this.txtDataVencimento = new System.Windows.Forms.TextBox();
            this.panelValor.SuspendLayout();
            this.panelTipo.SuspendLayout();
            this.panelCategoria.SuspendLayout();
            this.panelNomeCliente.SuspendLayout();
            this.panelNomeConta.SuspendLayout();
            this.panelDescricao.SuspendLayout();
            this.paneldataVencimento.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblAdicionarConta
            // 
            this.lblAdicionarConta.AutoSize = true;
            this.lblAdicionarConta.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdicionarConta.Location = new System.Drawing.Point(26, 16);
            this.lblAdicionarConta.Name = "lblAdicionarConta";
            this.lblAdicionarConta.Size = new System.Drawing.Size(183, 29);
            this.lblAdicionarConta.TabIndex = 0;
            this.lblAdicionarConta.Text = "Adicionar Conta";
            // 
            // btnFecharCadastro
            // 
            this.btnFecharCadastro.BackColor = System.Drawing.Color.Transparent;
            this.btnFecharCadastro.FlatAppearance.BorderSize = 0;
            this.btnFecharCadastro.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnFecharCadastro.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnFecharCadastro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFecharCadastro.Image = global::projetointegrador.Properties.Resources.icons8_close_28;
            this.btnFecharCadastro.Location = new System.Drawing.Point(568, 20);
            this.btnFecharCadastro.Name = "btnFecharCadastro";
            this.btnFecharCadastro.Size = new System.Drawing.Size(28, 28);
            this.btnFecharCadastro.TabIndex = 1;
            this.btnFecharCadastro.UseVisualStyleBackColor = false;
            this.btnFecharCadastro.Click += new System.EventHandler(this.btnFecharCadastro_Click);
            // 
            // lblDataVencimento
            // 
            this.lblDataVencimento.AutoSize = true;
            this.lblDataVencimento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataVencimento.Location = new System.Drawing.Point(26, 63);
            this.lblDataVencimento.Name = "lblDataVencimento";
            this.lblDataVencimento.Size = new System.Drawing.Size(130, 16);
            this.lblDataVencimento.TabIndex = 2;
            this.lblDataVencimento.Text = "Data de Vencimento";
            // 
            // lblNomeConta
            // 
            this.lblNomeConta.AutoSize = true;
            this.lblNomeConta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomeConta.Location = new System.Drawing.Point(322, 63);
            this.lblNomeConta.Name = "lblNomeConta";
            this.lblNomeConta.Size = new System.Drawing.Size(102, 16);
            this.lblNomeConta.TabIndex = 3;
            this.lblNomeConta.Text = "Nome da Conta";
            // 
            // lblValor
            // 
            this.lblValor.AutoSize = true;
            this.lblValor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValor.Location = new System.Drawing.Point(26, 119);
            this.lblValor.Name = "lblValor";
            this.lblValor.Size = new System.Drawing.Size(40, 16);
            this.lblValor.TabIndex = 4;
            this.lblValor.Text = "Valor";
            // 
            // lblTipoConta
            // 
            this.lblTipoConta.AutoSize = true;
            this.lblTipoConta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoConta.Location = new System.Drawing.Point(322, 120);
            this.lblTipoConta.Name = "lblTipoConta";
            this.lblTipoConta.Size = new System.Drawing.Size(93, 16);
            this.lblTipoConta.TabIndex = 5;
            this.lblTipoConta.Text = "Tipo de Conta";
            // 
            // lblCategoria
            // 
            this.lblCategoria.AutoSize = true;
            this.lblCategoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategoria.Location = new System.Drawing.Point(26, 178);
            this.lblCategoria.Name = "lblCategoria";
            this.lblCategoria.Size = new System.Drawing.Size(67, 16);
            this.lblCategoria.TabIndex = 6;
            this.lblCategoria.Text = "Categoria";
            // 
            // lblNomeCliente
            // 
            this.lblNomeCliente.AutoSize = true;
            this.lblNomeCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomeCliente.Location = new System.Drawing.Point(322, 179);
            this.lblNomeCliente.Name = "lblNomeCliente";
            this.lblNomeCliente.Size = new System.Drawing.Size(108, 16);
            this.lblNomeCliente.TabIndex = 7;
            this.lblNomeCliente.Text = "Nome do Cliente";
            // 
            // lblDescricao
            // 
            this.lblDescricao.AutoSize = true;
            this.lblDescricao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescricao.Location = new System.Drawing.Point(26, 240);
            this.lblDescricao.Name = "lblDescricao";
            this.lblDescricao.Size = new System.Drawing.Size(70, 16);
            this.lblDescricao.TabIndex = 8;
            this.lblDescricao.Text = "Descricao";
            // 
            // txtDescricao
            // 
            this.txtDescricao.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDescricao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescricao.Location = new System.Drawing.Point(10, 6);
            this.txtDescricao.Multiline = true;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Size = new System.Drawing.Size(531, 155);
            this.txtDescricao.TabIndex = 15;
            this.txtDescricao.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDescricao_KeyDown);
            // 
            // btnSalvarCadastro
            // 
            this.btnSalvarCadastro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(52)))), ((int)(((byte)(85)))));
            this.btnSalvarCadastro.FlatAppearance.BorderSize = 0;
            this.btnSalvarCadastro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvarCadastro.ForeColor = System.Drawing.Color.White;
            this.btnSalvarCadastro.Location = new System.Drawing.Point(488, 458);
            this.btnSalvarCadastro.Name = "btnSalvarCadastro";
            this.btnSalvarCadastro.Size = new System.Drawing.Size(97, 33);
            this.btnSalvarCadastro.TabIndex = 16;
            this.btnSalvarCadastro.Text = "Salvar";
            this.btnSalvarCadastro.UseVisualStyleBackColor = false;
            this.btnSalvarCadastro.Click += new System.EventHandler(this.btnSalvarCadastro_Click);
            // 
            // btnCancelarCadastro
            // 
            this.btnCancelarCadastro.Location = new System.Drawing.Point(382, 458);
            this.btnCancelarCadastro.Name = "btnCancelarCadastro";
            this.btnCancelarCadastro.Size = new System.Drawing.Size(98, 33);
            this.btnCancelarCadastro.TabIndex = 17;
            this.btnCancelarCadastro.Text = "Cancelar";
            this.btnCancelarCadastro.UseVisualStyleBackColor = true;
            this.btnCancelarCadastro.Click += new System.EventHandler(this.btnCancelarCadastro_Click);
            // 
            // panelValor
            // 
            this.panelValor.BackColor = System.Drawing.Color.White;
            this.panelValor.Controls.Add(this.lblInformeValor);
            this.panelValor.Controls.Add(this.txtValor);
            this.panelValor.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.panelValor.Location = new System.Drawing.Point(26, 139);
            this.panelValor.Name = "panelValor";
            this.panelValor.Size = new System.Drawing.Size(265, 30);
            this.panelValor.TabIndex = 23;
            // 
            // lblInformeValor
            // 
            this.lblInformeValor.AutoSize = true;
            this.lblInformeValor.BackColor = System.Drawing.Color.White;
            this.lblInformeValor.Enabled = false;
            this.lblInformeValor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInformeValor.ForeColor = System.Drawing.Color.Gray;
            this.lblInformeValor.Location = new System.Drawing.Point(7, 8);
            this.lblInformeValor.Name = "lblInformeValor";
            this.lblInformeValor.Size = new System.Drawing.Size(138, 15);
            this.lblInformeValor.TabIndex = 1;
            this.lblInformeValor.Text = "Informe o valor da conta";
            // 
            // txtValor
            // 
            this.txtValor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtValor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValor.Location = new System.Drawing.Point(4, 9);
            this.txtValor.Name = "txtValor";
            this.txtValor.Size = new System.Drawing.Size(242, 14);
            this.txtValor.TabIndex = 4;
            this.txtValor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtValor_KeyDown);
            // 
            // panelTipo
            // 
            this.panelTipo.BackColor = System.Drawing.Color.White;
            this.panelTipo.Controls.Add(this.lblInformeTipo);
            this.panelTipo.Controls.Add(this.cbTipoConta);
            this.panelTipo.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.panelTipo.Location = new System.Drawing.Point(321, 139);
            this.panelTipo.Name = "panelTipo";
            this.panelTipo.Size = new System.Drawing.Size(265, 30);
            this.panelTipo.TabIndex = 22;
            // 
            // lblInformeTipo
            // 
            this.lblInformeTipo.AutoSize = true;
            this.lblInformeTipo.BackColor = System.Drawing.Color.White;
            this.lblInformeTipo.Enabled = false;
            this.lblInformeTipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInformeTipo.ForeColor = System.Drawing.Color.Gray;
            this.lblInformeTipo.Location = new System.Drawing.Point(4, 8);
            this.lblInformeTipo.Name = "lblInformeTipo";
            this.lblInformeTipo.Size = new System.Drawing.Size(182, 15);
            this.lblInformeTipo.TabIndex = 1;
            this.lblInformeTipo.Text = "Informe se é receita ou despesa";
            // 
            // cbTipoConta
            // 
            this.cbTipoConta.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cbTipoConta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoConta.DropDownWidth = 262;
            this.cbTipoConta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbTipoConta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTipoConta.FormattingEnabled = true;
            this.cbTipoConta.Items.AddRange(new object[] {
            "Receita",
            "Despesa"});
            this.cbTipoConta.Location = new System.Drawing.Point(5, 4);
            this.cbTipoConta.Name = "cbTipoConta";
            this.cbTipoConta.Size = new System.Drawing.Size(242, 23);
            this.cbTipoConta.TabIndex = 3;
            this.cbTipoConta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbTipoConta_KeyDown);
            // 
            // panelCategoria
            // 
            this.panelCategoria.BackColor = System.Drawing.Color.White;
            this.panelCategoria.Controls.Add(this.lblInformeCategoria);
            this.panelCategoria.Controls.Add(this.cbCategoria);
            this.panelCategoria.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.panelCategoria.Location = new System.Drawing.Point(26, 198);
            this.panelCategoria.Name = "panelCategoria";
            this.panelCategoria.Size = new System.Drawing.Size(265, 30);
            this.panelCategoria.TabIndex = 24;
            // 
            // lblInformeCategoria
            // 
            this.lblInformeCategoria.AutoSize = true;
            this.lblInformeCategoria.BackColor = System.Drawing.Color.White;
            this.lblInformeCategoria.Enabled = false;
            this.lblInformeCategoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInformeCategoria.ForeColor = System.Drawing.Color.Gray;
            this.lblInformeCategoria.Location = new System.Drawing.Point(8, 7);
            this.lblInformeCategoria.Name = "lblInformeCategoria";
            this.lblInformeCategoria.Size = new System.Drawing.Size(221, 15);
            this.lblInformeCategoria.TabIndex = 1;
            this.lblInformeCategoria.Text = "Informe a categoria da conta (opcional)";
            // 
            // cbCategoria
            // 
            this.cbCategoria.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cbCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCategoria.DropDownWidth = 262;
            this.cbCategoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbCategoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCategoria.FormattingEnabled = true;
            this.cbCategoria.Items.AddRange(new object[] {
            "Nenhum",
            "Salário",
            "Impostos",
            "Fornecedores",
            "Empréstimos",
            "Alimentação",
            "Transporte",
            "Aluguel",
            "Energia Elétrica",
            "Internet",
            "Telefone",
            "Água",
            "Serviços de Terceiros",
            "Locação de Equipamentos"});
            this.cbCategoria.Location = new System.Drawing.Point(3, 4);
            this.cbCategoria.Name = "cbCategoria";
            this.cbCategoria.Size = new System.Drawing.Size(242, 23);
            this.cbCategoria.TabIndex = 2;
            this.cbCategoria.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbCategoria_KeyDown);
            // 
            // panelNomeCliente
            // 
            this.panelNomeCliente.BackColor = System.Drawing.Color.White;
            this.panelNomeCliente.Controls.Add(this.lblInformeCliente);
            this.panelNomeCliente.Controls.Add(this.txtNomeCliente);
            this.panelNomeCliente.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.panelNomeCliente.Location = new System.Drawing.Point(321, 198);
            this.panelNomeCliente.Name = "panelNomeCliente";
            this.panelNomeCliente.Size = new System.Drawing.Size(265, 30);
            this.panelNomeCliente.TabIndex = 21;
            // 
            // lblInformeCliente
            // 
            this.lblInformeCliente.AutoSize = true;
            this.lblInformeCliente.BackColor = System.Drawing.Color.White;
            this.lblInformeCliente.Enabled = false;
            this.lblInformeCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInformeCliente.ForeColor = System.Drawing.Color.Gray;
            this.lblInformeCliente.Location = new System.Drawing.Point(8, 7);
            this.lblInformeCliente.Name = "lblInformeCliente";
            this.lblInformeCliente.Size = new System.Drawing.Size(150, 15);
            this.lblInformeCliente.TabIndex = 18;
            this.lblInformeCliente.Text = "Informe o nome do cliente";
            // 
            // txtNomeCliente
            // 
            this.txtNomeCliente.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNomeCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomeCliente.Location = new System.Drawing.Point(5, 9);
            this.txtNomeCliente.Name = "txtNomeCliente";
            this.txtNomeCliente.Size = new System.Drawing.Size(242, 14);
            this.txtNomeCliente.TabIndex = 5;
            this.txtNomeCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNomeCliente_KeyDown);
            // 
            // panelNomeConta
            // 
            this.panelNomeConta.BackColor = System.Drawing.Color.White;
            this.panelNomeConta.Controls.Add(this.lblInformeNomeConta);
            this.panelNomeConta.Controls.Add(this.txtNomeConta);
            this.panelNomeConta.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.panelNomeConta.Location = new System.Drawing.Point(321, 79);
            this.panelNomeConta.Name = "panelNomeConta";
            this.panelNomeConta.Size = new System.Drawing.Size(265, 30);
            this.panelNomeConta.TabIndex = 25;
            // 
            // lblInformeNomeConta
            // 
            this.lblInformeNomeConta.AutoSize = true;
            this.lblInformeNomeConta.BackColor = System.Drawing.Color.White;
            this.lblInformeNomeConta.Enabled = false;
            this.lblInformeNomeConta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInformeNomeConta.ForeColor = System.Drawing.Color.Gray;
            this.lblInformeNomeConta.Location = new System.Drawing.Point(7, 7);
            this.lblInformeNomeConta.Name = "lblInformeNomeConta";
            this.lblInformeNomeConta.Size = new System.Drawing.Size(144, 15);
            this.lblInformeNomeConta.TabIndex = 1;
            this.lblInformeNomeConta.Text = "Informe o nome da conta";
            // 
            // txtNomeConta
            // 
            this.txtNomeConta.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNomeConta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomeConta.Location = new System.Drawing.Point(4, 9);
            this.txtNomeConta.Name = "txtNomeConta";
            this.txtNomeConta.Size = new System.Drawing.Size(242, 14);
            this.txtNomeConta.TabIndex = 4;
            this.txtNomeConta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNomeConta_KeyDown);
            // 
            // lblDescrevaConta
            // 
            this.lblDescrevaConta.AutoSize = true;
            this.lblDescrevaConta.BackColor = System.Drawing.Color.White;
            this.lblDescrevaConta.Enabled = false;
            this.lblDescrevaConta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescrevaConta.ForeColor = System.Drawing.Color.Gray;
            this.lblDescrevaConta.Location = new System.Drawing.Point(8, 9);
            this.lblDescrevaConta.Name = "lblDescrevaConta";
            this.lblDescrevaConta.Size = new System.Drawing.Size(159, 15);
            this.lblDescrevaConta.TabIndex = 18;
            this.lblDescrevaConta.Text = "Descreva a conta (opcional)";
            // 
            // panelDescricao
            // 
            this.panelDescricao.BackColor = System.Drawing.Color.White;
            this.panelDescricao.Controls.Add(this.lblDescrevaConta);
            this.panelDescricao.Controls.Add(this.txtDescricao);
            this.panelDescricao.Location = new System.Drawing.Point(26, 259);
            this.panelDescricao.Name = "panelDescricao";
            this.panelDescricao.Size = new System.Drawing.Size(560, 167);
            this.panelDescricao.TabIndex = 26;
            // 
            // lblContaSalva
            // 
            this.lblContaSalva.AutoSize = true;
            this.lblContaSalva.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContaSalva.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(52)))), ((int)(((byte)(85)))));
            this.lblContaSalva.Location = new System.Drawing.Point(33, 464);
            this.lblContaSalva.Name = "lblContaSalva";
            this.lblContaSalva.Size = new System.Drawing.Size(0, 24);
            this.lblContaSalva.TabIndex = 27;
            // 
            // timerMensagemConta
            // 
            this.timerMensagemConta.Interval = 3000;
            // 
            // paneldataVencimento
            // 
            this.paneldataVencimento.BackColor = System.Drawing.Color.White;
            this.paneldataVencimento.Controls.Add(this.lblInformeDataVenc);
            this.paneldataVencimento.Controls.Add(this.txtDataVencimento);
            this.paneldataVencimento.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.paneldataVencimento.Location = new System.Drawing.Point(26, 79);
            this.paneldataVencimento.Name = "paneldataVencimento";
            this.paneldataVencimento.Size = new System.Drawing.Size(265, 30);
            this.paneldataVencimento.TabIndex = 24;
            // 
            // lblInformeDataVenc
            // 
            this.lblInformeDataVenc.AutoSize = true;
            this.lblInformeDataVenc.BackColor = System.Drawing.Color.White;
            this.lblInformeDataVenc.Enabled = false;
            this.lblInformeDataVenc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInformeDataVenc.ForeColor = System.Drawing.Color.Gray;
            this.lblInformeDataVenc.Location = new System.Drawing.Point(8, 7);
            this.lblInformeDataVenc.Name = "lblInformeDataVenc";
            this.lblInformeDataVenc.Size = new System.Drawing.Size(171, 15);
            this.lblInformeDataVenc.TabIndex = 1;
            this.lblInformeDataVenc.Text = "Informe a data de Vencimento";
            // 
            // txtDataVencimento
            // 
            this.txtDataVencimento.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDataVencimento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataVencimento.Location = new System.Drawing.Point(5, 9);
            this.txtDataVencimento.Name = "txtDataVencimento";
            this.txtDataVencimento.Size = new System.Drawing.Size(242, 14);
            this.txtDataVencimento.TabIndex = 4;
            this.txtDataVencimento.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDataVencimento_KeyDown);
            // 
            // FrmCadastroConta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(614, 521);
            this.ControlBox = false;
            this.Controls.Add(this.lblContaSalva);
            this.Controls.Add(this.panelDescricao);
            this.Controls.Add(this.panelNomeConta);
            this.Controls.Add(this.paneldataVencimento);
            this.Controls.Add(this.panelValor);
            this.Controls.Add(this.panelTipo);
            this.Controls.Add(this.panelCategoria);
            this.Controls.Add(this.panelNomeCliente);
            this.Controls.Add(this.btnCancelarCadastro);
            this.Controls.Add(this.btnSalvarCadastro);
            this.Controls.Add(this.lblDescricao);
            this.Controls.Add(this.lblNomeCliente);
            this.Controls.Add(this.lblCategoria);
            this.Controls.Add(this.lblTipoConta);
            this.Controls.Add(this.lblValor);
            this.Controls.Add(this.lblNomeConta);
            this.Controls.Add(this.lblDataVencimento);
            this.Controls.Add(this.btnFecharCadastro);
            this.Controls.Add(this.lblAdicionarConta);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCadastroConta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormCadastroConta";
            this.Load += new System.EventHandler(this.FrmCadastroConta_Load);
            this.panelValor.ResumeLayout(false);
            this.panelValor.PerformLayout();
            this.panelTipo.ResumeLayout(false);
            this.panelTipo.PerformLayout();
            this.panelCategoria.ResumeLayout(false);
            this.panelCategoria.PerformLayout();
            this.panelNomeCliente.ResumeLayout(false);
            this.panelNomeCliente.PerformLayout();
            this.panelNomeConta.ResumeLayout(false);
            this.panelNomeConta.PerformLayout();
            this.panelDescricao.ResumeLayout(false);
            this.panelDescricao.PerformLayout();
            this.paneldataVencimento.ResumeLayout(false);
            this.paneldataVencimento.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAdicionarConta;
        private System.Windows.Forms.Button btnFecharCadastro;
        private System.Windows.Forms.Label lblDataVencimento;
        private System.Windows.Forms.Label lblNomeConta;
        private System.Windows.Forms.Label lblValor;
        private System.Windows.Forms.Label lblTipoConta;
        private System.Windows.Forms.Label lblCategoria;
        private System.Windows.Forms.Label lblNomeCliente;
        private System.Windows.Forms.Label lblDescricao;
        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.Button btnSalvarCadastro;
        private System.Windows.Forms.Button btnCancelarCadastro;
        private System.Windows.Forms.Panel panelValor;
        private System.Windows.Forms.Label lblInformeValor;
        private System.Windows.Forms.TextBox txtValor;
        private System.Windows.Forms.Panel panelTipo;
        private System.Windows.Forms.Label lblInformeTipo;
        private System.Windows.Forms.ComboBox cbTipoConta;
        private System.Windows.Forms.Panel panelCategoria;
        private System.Windows.Forms.Label lblInformeCategoria;
        private System.Windows.Forms.ComboBox cbCategoria;
        private System.Windows.Forms.Panel panelNomeCliente;
        private System.Windows.Forms.Label lblInformeCliente;
        private System.Windows.Forms.TextBox txtNomeCliente;
        private System.Windows.Forms.Panel panelNomeConta;
        private System.Windows.Forms.Label lblInformeNomeConta;
        private System.Windows.Forms.TextBox txtNomeConta;
        private System.Windows.Forms.Label lblDescrevaConta;
        private System.Windows.Forms.Panel panelDescricao;
        private System.Windows.Forms.Label lblContaSalva;
        private System.Windows.Forms.Timer timerMensagemConta;
        private System.Windows.Forms.Panel paneldataVencimento;
        private System.Windows.Forms.Label lblInformeDataVenc;
        private System.Windows.Forms.TextBox txtDataVencimento;
    }
}