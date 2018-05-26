namespace MonitorRet
{
    partial class Monitor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Monitor));
            this.btnEventosStop = new System.Windows.Forms.Button();
            this.btnEventosWait = new System.Windows.Forms.Button();
            this.btnEventosStart = new System.Windows.Forms.Button();
            this.dtpEventosEmissao = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.chkEventosTodasLojas = new System.Windows.Forms.CheckBox();
            this.cmbEventosLoja = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ltvEventosHistorico = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.lblEventosVersaoBancoServer = new System.Windows.Forms.Label();
            this.lblEventosVersaoSistema = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblEventosServidor = new System.Windows.Forms.Label();
            this.btnThreadSair = new System.Windows.Forms.Button();
            this.btnMininizaMonitor = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.sockets = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // btnEventosStop
            // 
            this.btnEventosStop.BackColor = System.Drawing.Color.Black;
            this.btnEventosStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEventosStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEventosStop.ForeColor = System.Drawing.Color.Red;
            this.btnEventosStop.Location = new System.Drawing.Point(0, 521);
            this.btnEventosStop.Name = "btnEventosStop";
            this.btnEventosStop.Size = new System.Drawing.Size(834, 40);
            this.btnEventosStop.TabIndex = 14;
            this.btnEventosStop.Text = "[ F5 ] - STOP";
            this.btnEventosStop.UseVisualStyleBackColor = false;
            this.btnEventosStop.Click += new System.EventHandler(this.btnThreadStop_Click);
            // 
            // btnEventosWait
            // 
            this.btnEventosWait.BackColor = System.Drawing.Color.Black;
            this.btnEventosWait.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEventosWait.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEventosWait.ForeColor = System.Drawing.Color.Yellow;
            this.btnEventosWait.Location = new System.Drawing.Point(0, 480);
            this.btnEventosWait.Name = "btnEventosWait";
            this.btnEventosWait.Size = new System.Drawing.Size(834, 40);
            this.btnEventosWait.TabIndex = 15;
            this.btnEventosWait.Text = "[ F3 ] - WAIT";
            this.btnEventosWait.UseVisualStyleBackColor = false;
            this.btnEventosWait.Click += new System.EventHandler(this.btnThreadWait_Click);
            // 
            // btnEventosStart
            // 
            this.btnEventosStart.BackColor = System.Drawing.Color.Black;
            this.btnEventosStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEventosStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEventosStart.ForeColor = System.Drawing.Color.Lime;
            this.btnEventosStart.Location = new System.Drawing.Point(0, 439);
            this.btnEventosStart.Name = "btnEventosStart";
            this.btnEventosStart.Size = new System.Drawing.Size(834, 40);
            this.btnEventosStart.TabIndex = 16;
            this.btnEventosStart.Text = "[ F2 ] - START";
            this.btnEventosStart.UseVisualStyleBackColor = false;
            this.btnEventosStart.Click += new System.EventHandler(this.btnThreadStart_Click);
            // 
            // dtpEventosEmissao
            // 
            this.dtpEventosEmissao.Enabled = false;
            this.dtpEventosEmissao.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEventosEmissao.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEventosEmissao.Location = new System.Drawing.Point(67, 31);
            this.dtpEventosEmissao.Name = "dtpEventosEmissao";
            this.dtpEventosEmissao.Size = new System.Drawing.Size(94, 20);
            this.dtpEventosEmissao.TabIndex = 178;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Lime;
            this.label3.Location = new System.Drawing.Point(2, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 177;
            this.label3.Text = "EMISSÃO";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.Lime;
            this.label27.Location = new System.Drawing.Point(-1, 61);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(784, 13);
            this.label27.TabIndex = 270;
            this.label27.Text = "_________________________________________________________________________________" +
    "______________________________";
            // 
            // chkEventosTodasLojas
            // 
            this.chkEventosTodasLojas.AutoSize = true;
            this.chkEventosTodasLojas.BackColor = System.Drawing.Color.Black;
            this.chkEventosTodasLojas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEventosTodasLojas.ForeColor = System.Drawing.Color.Lime;
            this.chkEventosTodasLojas.Location = new System.Drawing.Point(349, 7);
            this.chkEventosTodasLojas.Name = "chkEventosTodasLojas";
            this.chkEventosTodasLojas.Size = new System.Drawing.Size(68, 17);
            this.chkEventosTodasLojas.TabIndex = 273;
            this.chkEventosTodasLojas.Text = "TODAS";
            this.chkEventosTodasLojas.UseVisualStyleBackColor = false;
            this.chkEventosTodasLojas.CheckedChanged += new System.EventHandler(this.chkEventosTodasLojas_CheckedChanged);
            // 
            // cmbEventosLoja
            // 
            this.cmbEventosLoja.BackColor = System.Drawing.Color.Black;
            this.cmbEventosLoja.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEventosLoja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEventosLoja.ForeColor = System.Drawing.Color.Lime;
            this.cmbEventosLoja.FormattingEnabled = true;
            this.cmbEventosLoja.Location = new System.Drawing.Point(67, 4);
            this.cmbEventosLoja.Name = "cmbEventosLoja";
            this.cmbEventosLoja.Size = new System.Drawing.Size(279, 21);
            this.cmbEventosLoja.TabIndex = 272;
            this.cmbEventosLoja.SelectedIndexChanged += new System.EventHandler(this.cmbEventosLoja_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Black;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Lime;
            this.label4.Location = new System.Drawing.Point(26, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 271;
            this.label4.Text = "LOJA";
            // 
            // ltvEventosHistorico
            // 
            this.ltvEventosHistorico.AllowColumnReorder = true;
            this.ltvEventosHistorico.BackColor = System.Drawing.Color.Black;
            this.ltvEventosHistorico.ForeColor = System.Drawing.Color.Lime;
            this.ltvEventosHistorico.FullRowSelect = true;
            this.ltvEventosHistorico.GridLines = true;
            this.ltvEventosHistorico.LabelEdit = true;
            this.ltvEventosHistorico.Location = new System.Drawing.Point(2, 76);
            this.ltvEventosHistorico.Name = "ltvEventosHistorico";
            this.ltvEventosHistorico.Size = new System.Drawing.Size(832, 363);
            this.ltvEventosHistorico.TabIndex = 274;
            this.ltvEventosHistorico.UseCompatibleStateImageBehavior = false;
            this.ltvEventosHistorico.View = System.Windows.Forms.View.Details;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Lime;
            this.label1.Location = new System.Drawing.Point(426, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 275;
            this.label1.Text = "VERSÃO BANCO";
            // 
            // lblEventosVersaoBancoServer
            // 
            this.lblEventosVersaoBancoServer.AutoSize = true;
            this.lblEventosVersaoBancoServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEventosVersaoBancoServer.ForeColor = System.Drawing.Color.Lime;
            this.lblEventosVersaoBancoServer.Location = new System.Drawing.Point(531, 8);
            this.lblEventosVersaoBancoServer.Name = "lblEventosVersaoBancoServer";
            this.lblEventosVersaoBancoServer.Size = new System.Drawing.Size(0, 13);
            this.lblEventosVersaoBancoServer.TabIndex = 276;
            // 
            // lblEventosVersaoSistema
            // 
            this.lblEventosVersaoSistema.AutoSize = true;
            this.lblEventosVersaoSistema.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEventosVersaoSistema.ForeColor = System.Drawing.Color.Lime;
            this.lblEventosVersaoSistema.Location = new System.Drawing.Point(531, 28);
            this.lblEventosVersaoSistema.Name = "lblEventosVersaoSistema";
            this.lblEventosVersaoSistema.Size = new System.Drawing.Size(0, 13);
            this.lblEventosVersaoSistema.TabIndex = 278;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Lime;
            this.label6.Location = new System.Drawing.Point(414, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 13);
            this.label6.TabIndex = 277;
            this.label6.Text = "VERSÃO SISTEMA";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Lime;
            this.label2.Location = new System.Drawing.Point(458, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 279;
            this.label2.Text = "SERVIDOR";
            // 
            // lblEventosServidor
            // 
            this.lblEventosServidor.AutoSize = true;
            this.lblEventosServidor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEventosServidor.ForeColor = System.Drawing.Color.Lime;
            this.lblEventosServidor.Location = new System.Drawing.Point(530, 47);
            this.lblEventosServidor.Name = "lblEventosServidor";
            this.lblEventosServidor.Size = new System.Drawing.Size(0, 13);
            this.lblEventosServidor.TabIndex = 280;
            // 
            // btnThreadSair
            // 
            this.btnThreadSair.BackColor = System.Drawing.Color.Black;
            this.btnThreadSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThreadSair.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThreadSair.ForeColor = System.Drawing.Color.Red;
            this.btnThreadSair.Location = new System.Drawing.Point(734, 1);
            this.btnThreadSair.Name = "btnThreadSair";
            this.btnThreadSair.Size = new System.Drawing.Size(100, 73);
            this.btnThreadSair.TabIndex = 281;
            this.btnThreadSair.Text = "[ F11 ]     SAIR";
            this.btnThreadSair.UseVisualStyleBackColor = false;
            this.btnThreadSair.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnMininizaMonitor
            // 
            this.btnMininizaMonitor.BackColor = System.Drawing.Color.Black;
            this.btnMininizaMonitor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMininizaMonitor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMininizaMonitor.ForeColor = System.Drawing.Color.Yellow;
            this.btnMininizaMonitor.Location = new System.Drawing.Point(633, 1);
            this.btnMininizaMonitor.Name = "btnMininizaMonitor";
            this.btnMininizaMonitor.Size = new System.Drawing.Size(100, 73);
            this.btnMininizaMonitor.TabIndex = 282;
            this.btnMininizaMonitor.Text = "[ F10 ]  MININIZAR";
            this.btnMininizaMonitor.UseVisualStyleBackColor = false;
            this.btnMininizaMonitor.Click += new System.EventHandler(this.btnMininizaMonitor_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "Theard Ativa";
            this.notifyIcon1.BalloonTipTitle = "Monitor";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Monitor Conector";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Monitor_MouseDoubleClick);
            // 
            // sockets
            // 
            this.sockets.WorkerReportsProgress = true;
            this.sockets.WorkerSupportsCancellation = true;
            this.sockets.DoWork += new System.ComponentModel.DoWorkEventHandler(this.sockets_DoWork);
            // 
            // Monitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(835, 562);
            this.Controls.Add(this.btnMininizaMonitor);
            this.Controls.Add(this.btnThreadSair);
            this.Controls.Add(this.lblEventosServidor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblEventosVersaoSistema);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblEventosVersaoBancoServer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ltvEventosHistorico);
            this.Controls.Add(this.chkEventosTodasLojas);
            this.Controls.Add(this.cmbEventosLoja);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.dtpEventosEmissao);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnEventosStart);
            this.Controls.Add(this.btnEventosWait);
            this.Controls.Add(this.btnEventosStop);
            this.ForeColor = System.Drawing.Color.Lime;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Monitor";
            this.Opacity = 0.8D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Monitor";
            this.Load += new System.EventHandler(this.Monitor_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Monitor_KeyDown);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Monitor_MouseDoubleClick);
            this.Resize += new System.EventHandler(this.Monitor_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEventosStop;
        private System.Windows.Forms.Button btnEventosWait;
        private System.Windows.Forms.Button btnEventosStart;
        private System.Windows.Forms.DateTimePicker dtpEventosEmissao;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.CheckBox chkEventosTodasLojas;
        private System.Windows.Forms.ComboBox cmbEventosLoja;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView ltvEventosHistorico;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblEventosVersaoBancoServer;
        private System.Windows.Forms.Label lblEventosVersaoSistema;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblEventosServidor;
        private System.Windows.Forms.Button btnThreadSair;
        private System.Windows.Forms.Button btnMininizaMonitor;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.ComponentModel.BackgroundWorker sockets;
    }
}