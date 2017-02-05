namespace Mightyena {
    partial class FormMain {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.tabs = new System.Windows.Forms.TabControl();
            this.tbpGeneral = new System.Windows.Forms.TabPage();
            this.lblDebugInfo = new System.Windows.Forms.Label();
            this.fraParty = new System.Windows.Forms.GroupBox();
            this.lblPartyHoverInfo = new System.Windows.Forms.Label();
            this.cmdParty6 = new System.Windows.Forms.Button();
            this.cmdParty5 = new System.Windows.Forms.Button();
            this.cmdParty1 = new System.Windows.Forms.Button();
            this.cmdParty2 = new System.Windows.Forms.Button();
            this.cmdParty4 = new System.Windows.Forms.Button();
            this.cmdParty3 = new System.Windows.Forms.Button();
            this.fraTrainer = new System.Windows.Forms.GroupBox();
            this.nudCoins = new System.Windows.Forms.NumericUpDown();
            this.nudMoney = new System.Windows.Forms.NumericUpDown();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.cmbGender = new System.Windows.Forms.ComboBox();
            this.lblGender = new System.Windows.Forms.Label();
            this.lblTrainerID = new System.Windows.Forms.Label();
            this.lblCoins = new System.Windows.Forms.Label();
            this.txtTrainerID = new System.Windows.Forms.TextBox();
            this.lblSecretID = new System.Windows.Forms.Label();
            this.lblMoney = new System.Windows.Forms.Label();
            this.txtSecretID = new System.Windows.Forms.TextBox();
            this.tbpBags = new System.Windows.Forms.TabPage();
            this.picSelectedItem = new System.Windows.Forms.PictureBox();
            this.nudSelectedItemQuantity = new System.Windows.Forms.NumericUpDown();
            this.cmbSelectedItem = new System.Windows.Forms.ComboBox();
            this.pnlBag = new System.Windows.Forms.Panel();
            this.optBagBerry = new System.Windows.Forms.RadioButton();
            this.optBagTM = new System.Windows.Forms.RadioButton();
            this.optBagBalls = new System.Windows.Forms.RadioButton();
            this.optBagKey = new System.Windows.Forms.RadioButton();
            this.optBagItem = new System.Windows.Forms.RadioButton();
            this.optBagPC = new System.Windows.Forms.RadioButton();
            this.tbpBoxes = new System.Windows.Forms.TabPage();
            this.pnlBoxButtons = new System.Windows.Forms.Panel();
            this.lblBoxHoverInfo = new System.Windows.Forms.Label();
            this.mnsMenu = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.mnsFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileBackup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnsFileSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.dlgSaveAs = new System.Windows.Forms.SaveFileDialog();
            this.dlgImport = new System.Windows.Forms.OpenFileDialog();
            this.pnlBoxNumbers = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdBoxSettings = new System.Windows.Forms.Button();
            this.tabs.SuspendLayout();
            this.tbpGeneral.SuspendLayout();
            this.fraParty.SuspendLayout();
            this.fraTrainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCoins)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMoney)).BeginInit();
            this.tbpBags.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSelectedItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSelectedItemQuantity)).BeginInit();
            this.tbpBoxes.SuspendLayout();
            this.pnlBoxButtons.SuspendLayout();
            this.mnsMenu.SuspendLayout();
            this.pnlBoxNumbers.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tbpGeneral);
            this.tabs.Controls.Add(this.tbpBags);
            this.tabs.Controls.Add(this.tbpBoxes);
            this.tabs.Enabled = false;
            this.tabs.Location = new System.Drawing.Point(8, 32);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(504, 312);
            this.tabs.TabIndex = 2;
            // 
            // tbpGeneral
            // 
            this.tbpGeneral.Controls.Add(this.lblDebugInfo);
            this.tbpGeneral.Controls.Add(this.fraParty);
            this.tbpGeneral.Controls.Add(this.fraTrainer);
            this.tbpGeneral.Location = new System.Drawing.Point(4, 22);
            this.tbpGeneral.Name = "tbpGeneral";
            this.tbpGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tbpGeneral.Size = new System.Drawing.Size(496, 286);
            this.tbpGeneral.TabIndex = 0;
            this.tbpGeneral.Text = "General";
            this.tbpGeneral.UseVisualStyleBackColor = true;
            // 
            // lblDebugInfo
            // 
            this.lblDebugInfo.Enabled = false;
            this.lblDebugInfo.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDebugInfo.Location = new System.Drawing.Point(184, 248);
            this.lblDebugInfo.Name = "lblDebugInfo";
            this.lblDebugInfo.Size = new System.Drawing.Size(296, 24);
            this.lblDebugInfo.TabIndex = 27;
            this.lblDebugInfo.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // fraParty
            // 
            this.fraParty.Controls.Add(this.lblPartyHoverInfo);
            this.fraParty.Controls.Add(this.cmdParty6);
            this.fraParty.Controls.Add(this.cmdParty5);
            this.fraParty.Controls.Add(this.cmdParty1);
            this.fraParty.Controls.Add(this.cmdParty2);
            this.fraParty.Controls.Add(this.cmdParty4);
            this.fraParty.Controls.Add(this.cmdParty3);
            this.fraParty.Location = new System.Drawing.Point(192, 16);
            this.fraParty.Name = "fraParty";
            this.fraParty.Size = new System.Drawing.Size(288, 224);
            this.fraParty.TabIndex = 26;
            this.fraParty.TabStop = false;
            this.fraParty.Text = "Party";
            // 
            // lblPartyHoverInfo
            // 
            this.lblPartyHoverInfo.Location = new System.Drawing.Point(16, 200);
            this.lblPartyHoverInfo.Name = "lblPartyHoverInfo";
            this.lblPartyHoverInfo.Size = new System.Drawing.Size(256, 16);
            this.lblPartyHoverInfo.TabIndex = 26;
            this.lblPartyHoverInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cmdParty6
            // 
            this.cmdParty6.Location = new System.Drawing.Point(192, 112);
            this.cmdParty6.Name = "cmdParty6";
            this.cmdParty6.Size = new System.Drawing.Size(80, 80);
            this.cmdParty6.TabIndex = 25;
            this.cmdParty6.Tag = "5";
            this.cmdParty6.UseVisualStyleBackColor = true;
            this.cmdParty6.Click += new System.EventHandler(this.PartyButton_Click);
            this.cmdParty6.Paint += new System.Windows.Forms.PaintEventHandler(this.PartyButton_Paint);
            this.cmdParty6.MouseEnter += new System.EventHandler(this.PartyButton_MouseEnter);
            this.cmdParty6.MouseLeave += new System.EventHandler(this.PartyButton_MouseLeave);
            // 
            // cmdParty5
            // 
            this.cmdParty5.Location = new System.Drawing.Point(104, 112);
            this.cmdParty5.Name = "cmdParty5";
            this.cmdParty5.Size = new System.Drawing.Size(80, 80);
            this.cmdParty5.TabIndex = 24;
            this.cmdParty5.Tag = "4";
            this.cmdParty5.UseVisualStyleBackColor = true;
            this.cmdParty5.Click += new System.EventHandler(this.PartyButton_Click);
            this.cmdParty5.Paint += new System.Windows.Forms.PaintEventHandler(this.PartyButton_Paint);
            this.cmdParty5.MouseEnter += new System.EventHandler(this.PartyButton_MouseEnter);
            this.cmdParty5.MouseLeave += new System.EventHandler(this.PartyButton_MouseLeave);
            // 
            // cmdParty1
            // 
            this.cmdParty1.Location = new System.Drawing.Point(16, 24);
            this.cmdParty1.Name = "cmdParty1";
            this.cmdParty1.Size = new System.Drawing.Size(80, 80);
            this.cmdParty1.TabIndex = 18;
            this.cmdParty1.Tag = "0";
            this.cmdParty1.UseVisualStyleBackColor = true;
            this.cmdParty1.Click += new System.EventHandler(this.PartyButton_Click);
            this.cmdParty1.Paint += new System.Windows.Forms.PaintEventHandler(this.PartyButton_Paint);
            this.cmdParty1.MouseEnter += new System.EventHandler(this.PartyButton_MouseEnter);
            this.cmdParty1.MouseLeave += new System.EventHandler(this.PartyButton_MouseLeave);
            // 
            // cmdParty2
            // 
            this.cmdParty2.Location = new System.Drawing.Point(104, 24);
            this.cmdParty2.Name = "cmdParty2";
            this.cmdParty2.Size = new System.Drawing.Size(80, 80);
            this.cmdParty2.TabIndex = 21;
            this.cmdParty2.Tag = "1";
            this.cmdParty2.UseVisualStyleBackColor = true;
            this.cmdParty2.Click += new System.EventHandler(this.PartyButton_Click);
            this.cmdParty2.Paint += new System.Windows.Forms.PaintEventHandler(this.PartyButton_Paint);
            this.cmdParty2.MouseEnter += new System.EventHandler(this.PartyButton_MouseEnter);
            this.cmdParty2.MouseLeave += new System.EventHandler(this.PartyButton_MouseLeave);
            // 
            // cmdParty4
            // 
            this.cmdParty4.Location = new System.Drawing.Point(16, 112);
            this.cmdParty4.Name = "cmdParty4";
            this.cmdParty4.Size = new System.Drawing.Size(80, 80);
            this.cmdParty4.TabIndex = 23;
            this.cmdParty4.Tag = "3";
            this.cmdParty4.UseVisualStyleBackColor = true;
            this.cmdParty4.Click += new System.EventHandler(this.PartyButton_Click);
            this.cmdParty4.Paint += new System.Windows.Forms.PaintEventHandler(this.PartyButton_Paint);
            this.cmdParty4.MouseEnter += new System.EventHandler(this.PartyButton_MouseEnter);
            this.cmdParty4.MouseLeave += new System.EventHandler(this.PartyButton_MouseLeave);
            // 
            // cmdParty3
            // 
            this.cmdParty3.Location = new System.Drawing.Point(192, 24);
            this.cmdParty3.Name = "cmdParty3";
            this.cmdParty3.Size = new System.Drawing.Size(80, 80);
            this.cmdParty3.TabIndex = 22;
            this.cmdParty3.Tag = "2";
            this.cmdParty3.UseVisualStyleBackColor = true;
            this.cmdParty3.Click += new System.EventHandler(this.PartyButton_Click);
            this.cmdParty3.Paint += new System.Windows.Forms.PaintEventHandler(this.PartyButton_Paint);
            this.cmdParty3.MouseEnter += new System.EventHandler(this.PartyButton_MouseEnter);
            this.cmdParty3.MouseLeave += new System.EventHandler(this.PartyButton_MouseLeave);
            // 
            // fraTrainer
            // 
            this.fraTrainer.Controls.Add(this.nudCoins);
            this.fraTrainer.Controls.Add(this.nudMoney);
            this.fraTrainer.Controls.Add(this.lblName);
            this.fraTrainer.Controls.Add(this.txtName);
            this.fraTrainer.Controls.Add(this.cmbGender);
            this.fraTrainer.Controls.Add(this.lblGender);
            this.fraTrainer.Controls.Add(this.lblTrainerID);
            this.fraTrainer.Controls.Add(this.lblCoins);
            this.fraTrainer.Controls.Add(this.txtTrainerID);
            this.fraTrainer.Controls.Add(this.lblSecretID);
            this.fraTrainer.Controls.Add(this.lblMoney);
            this.fraTrainer.Controls.Add(this.txtSecretID);
            this.fraTrainer.Location = new System.Drawing.Point(16, 16);
            this.fraTrainer.Name = "fraTrainer";
            this.fraTrainer.Size = new System.Drawing.Size(152, 256);
            this.fraTrainer.TabIndex = 19;
            this.fraTrainer.TabStop = false;
            this.fraTrainer.Text = "Trainer";
            // 
            // nudCoins
            // 
            this.nudCoins.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudCoins.Location = new System.Drawing.Point(8, 224);
            this.nudCoins.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudCoins.Name = "nudCoins";
            this.nudCoins.Size = new System.Drawing.Size(136, 20);
            this.nudCoins.TabIndex = 18;
            this.nudCoins.ThousandsSeparator = true;
            this.nudCoins.ValueChanged += new System.EventHandler(this.nudCoins_ValueChanged);
            // 
            // nudMoney
            // 
            this.nudMoney.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudMoney.Location = new System.Drawing.Point(8, 176);
            this.nudMoney.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudMoney.Name = "nudMoney";
            this.nudMoney.Size = new System.Drawing.Size(136, 20);
            this.nudMoney.TabIndex = 17;
            this.nudMoney.ThousandsSeparator = true;
            this.nudMoney.ValueChanged += new System.EventHandler(this.nudMoney_ValueChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(8, 16);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(74, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Trainer Name:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(8, 32);
            this.txtName.MaxLength = 7;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(136, 20);
            this.txtName.TabIndex = 1;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // cmbGender
            // 
            this.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGender.FormattingEnabled = true;
            this.cmbGender.Items.AddRange(new object[] {
            "Male",
            "Female"});
            this.cmbGender.Location = new System.Drawing.Point(8, 80);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(136, 21);
            this.cmbGender.TabIndex = 4;
            this.cmbGender.SelectedIndexChanged += new System.EventHandler(this.cmbGender_SelectedIndexChanged);
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.Location = new System.Drawing.Point(8, 64);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(45, 13);
            this.lblGender.TabIndex = 2;
            this.lblGender.Text = "Gender:";
            // 
            // lblTrainerID
            // 
            this.lblTrainerID.AutoSize = true;
            this.lblTrainerID.Location = new System.Drawing.Point(8, 112);
            this.lblTrainerID.Name = "lblTrainerID";
            this.lblTrainerID.Size = new System.Drawing.Size(57, 13);
            this.lblTrainerID.TabIndex = 4;
            this.lblTrainerID.Text = "Trainer ID:";
            // 
            // lblCoins
            // 
            this.lblCoins.AutoSize = true;
            this.lblCoins.Location = new System.Drawing.Point(8, 208);
            this.lblCoins.Name = "lblCoins";
            this.lblCoins.Size = new System.Drawing.Size(36, 13);
            this.lblCoins.TabIndex = 16;
            this.lblCoins.Text = "Coins:";
            // 
            // txtTrainerID
            // 
            this.txtTrainerID.Location = new System.Drawing.Point(8, 128);
            this.txtTrainerID.MaxLength = 5;
            this.txtTrainerID.Name = "txtTrainerID";
            this.txtTrainerID.Size = new System.Drawing.Size(64, 20);
            this.txtTrainerID.TabIndex = 5;
            this.txtTrainerID.TextChanged += new System.EventHandler(this.txtTrainerID_TextChanged);
            // 
            // lblSecretID
            // 
            this.lblSecretID.AutoSize = true;
            this.lblSecretID.Location = new System.Drawing.Point(80, 112);
            this.lblSecretID.Name = "lblSecretID";
            this.lblSecretID.Size = new System.Drawing.Size(55, 13);
            this.lblSecretID.TabIndex = 6;
            this.lblSecretID.Text = "Secret ID:";
            // 
            // lblMoney
            // 
            this.lblMoney.AutoSize = true;
            this.lblMoney.Location = new System.Drawing.Point(8, 160);
            this.lblMoney.Name = "lblMoney";
            this.lblMoney.Size = new System.Drawing.Size(42, 13);
            this.lblMoney.TabIndex = 14;
            this.lblMoney.Text = "Money:";
            // 
            // txtSecretID
            // 
            this.txtSecretID.Location = new System.Drawing.Point(80, 128);
            this.txtSecretID.MaxLength = 5;
            this.txtSecretID.Name = "txtSecretID";
            this.txtSecretID.Size = new System.Drawing.Size(64, 20);
            this.txtSecretID.TabIndex = 7;
            this.txtSecretID.TextChanged += new System.EventHandler(this.txtTrainerID_TextChanged);
            // 
            // tbpBags
            // 
            this.tbpBags.Controls.Add(this.picSelectedItem);
            this.tbpBags.Controls.Add(this.nudSelectedItemQuantity);
            this.tbpBags.Controls.Add(this.cmbSelectedItem);
            this.tbpBags.Controls.Add(this.pnlBag);
            this.tbpBags.Controls.Add(this.optBagBerry);
            this.tbpBags.Controls.Add(this.optBagTM);
            this.tbpBags.Controls.Add(this.optBagBalls);
            this.tbpBags.Controls.Add(this.optBagKey);
            this.tbpBags.Controls.Add(this.optBagItem);
            this.tbpBags.Controls.Add(this.optBagPC);
            this.tbpBags.Location = new System.Drawing.Point(4, 22);
            this.tbpBags.Name = "tbpBags";
            this.tbpBags.Size = new System.Drawing.Size(496, 286);
            this.tbpBags.TabIndex = 2;
            this.tbpBags.Text = "Bags";
            this.tbpBags.UseVisualStyleBackColor = true;
            // 
            // picSelectedItem
            // 
            this.picSelectedItem.Location = new System.Drawing.Point(12, 252);
            this.picSelectedItem.Name = "picSelectedItem";
            this.picSelectedItem.Size = new System.Drawing.Size(30, 30);
            this.picSelectedItem.TabIndex = 13;
            this.picSelectedItem.TabStop = false;
            this.picSelectedItem.Paint += new System.Windows.Forms.PaintEventHandler(this.picSelectedItem_Paint);
            // 
            // nudSelectedItemQuantity
            // 
            this.nudSelectedItemQuantity.Enabled = false;
            this.nudSelectedItemQuantity.Location = new System.Drawing.Point(216, 256);
            this.nudSelectedItemQuantity.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.nudSelectedItemQuantity.Name = "nudSelectedItemQuantity";
            this.nudSelectedItemQuantity.Size = new System.Drawing.Size(48, 20);
            this.nudSelectedItemQuantity.TabIndex = 12;
            this.nudSelectedItemQuantity.ValueChanged += new System.EventHandler(this.nudSelectedItemQuantity_ValueChanged);
            // 
            // cmbSelectedItem
            // 
            this.cmbSelectedItem.Enabled = false;
            this.cmbSelectedItem.FormattingEnabled = true;
            this.cmbSelectedItem.Location = new System.Drawing.Point(48, 256);
            this.cmbSelectedItem.Name = "cmbSelectedItem";
            this.cmbSelectedItem.Size = new System.Drawing.Size(160, 21);
            this.cmbSelectedItem.TabIndex = 11;
            this.cmbSelectedItem.SelectedIndexChanged += new System.EventHandler(this.cmbSelectedItem_SelectedIndexChanged);
            // 
            // pnlBag
            // 
            this.pnlBag.Location = new System.Drawing.Point(16, 48);
            this.pnlBag.Name = "pnlBag";
            this.pnlBag.Size = new System.Drawing.Size(464, 192);
            this.pnlBag.TabIndex = 10;
            // 
            // optBagBerry
            // 
            this.optBagBerry.Appearance = System.Windows.Forms.Appearance.Button;
            this.optBagBerry.Location = new System.Drawing.Point(416, 8);
            this.optBagBerry.Name = "optBagBerry";
            this.optBagBerry.Size = new System.Drawing.Size(72, 24);
            this.optBagBerry.TabIndex = 5;
            this.optBagBerry.Text = "Berries";
            this.optBagBerry.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.optBagBerry.UseVisualStyleBackColor = true;
            this.optBagBerry.CheckedChanged += new System.EventHandler(this.optBagBerry_CheckedChanged);
            // 
            // optBagTM
            // 
            this.optBagTM.Appearance = System.Windows.Forms.Appearance.Button;
            this.optBagTM.Location = new System.Drawing.Point(336, 8);
            this.optBagTM.Name = "optBagTM";
            this.optBagTM.Size = new System.Drawing.Size(72, 24);
            this.optBagTM.TabIndex = 4;
            this.optBagTM.Text = "TM / HMs";
            this.optBagTM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.optBagTM.UseVisualStyleBackColor = true;
            this.optBagTM.CheckedChanged += new System.EventHandler(this.optBagTM_CheckedChanged);
            // 
            // optBagBalls
            // 
            this.optBagBalls.Appearance = System.Windows.Forms.Appearance.Button;
            this.optBagBalls.Location = new System.Drawing.Point(256, 8);
            this.optBagBalls.Name = "optBagBalls";
            this.optBagBalls.Size = new System.Drawing.Size(72, 24);
            this.optBagBalls.TabIndex = 3;
            this.optBagBalls.Text = "Poké Balls";
            this.optBagBalls.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.optBagBalls.UseVisualStyleBackColor = true;
            this.optBagBalls.CheckedChanged += new System.EventHandler(this.optBagBalls_CheckedChanged);
            // 
            // optBagKey
            // 
            this.optBagKey.Appearance = System.Windows.Forms.Appearance.Button;
            this.optBagKey.Location = new System.Drawing.Point(176, 8);
            this.optBagKey.Name = "optBagKey";
            this.optBagKey.Size = new System.Drawing.Size(72, 24);
            this.optBagKey.TabIndex = 2;
            this.optBagKey.Text = "Key Items";
            this.optBagKey.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.optBagKey.UseVisualStyleBackColor = true;
            this.optBagKey.CheckedChanged += new System.EventHandler(this.optBagKey_CheckedChanged);
            // 
            // optBagItem
            // 
            this.optBagItem.Appearance = System.Windows.Forms.Appearance.Button;
            this.optBagItem.Checked = true;
            this.optBagItem.Location = new System.Drawing.Point(96, 8);
            this.optBagItem.Name = "optBagItem";
            this.optBagItem.Size = new System.Drawing.Size(72, 24);
            this.optBagItem.TabIndex = 1;
            this.optBagItem.TabStop = true;
            this.optBagItem.Text = "Items";
            this.optBagItem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.optBagItem.UseVisualStyleBackColor = true;
            this.optBagItem.CheckedChanged += new System.EventHandler(this.optBagItem_CheckedChanged);
            // 
            // optBagPC
            // 
            this.optBagPC.Appearance = System.Windows.Forms.Appearance.Button;
            this.optBagPC.Location = new System.Drawing.Point(8, 8);
            this.optBagPC.Name = "optBagPC";
            this.optBagPC.Size = new System.Drawing.Size(72, 24);
            this.optBagPC.TabIndex = 0;
            this.optBagPC.Text = "PC";
            this.optBagPC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.optBagPC.UseVisualStyleBackColor = true;
            this.optBagPC.CheckedChanged += new System.EventHandler(this.optBagPC_CheckedChanged);
            // 
            // tbpBoxes
            // 
            this.tbpBoxes.Controls.Add(this.pnlBoxNumbers);
            this.tbpBoxes.Controls.Add(this.pnlBoxButtons);
            this.tbpBoxes.Location = new System.Drawing.Point(4, 22);
            this.tbpBoxes.Name = "tbpBoxes";
            this.tbpBoxes.Size = new System.Drawing.Size(496, 286);
            this.tbpBoxes.TabIndex = 3;
            this.tbpBoxes.Text = "PC Boxes";
            this.tbpBoxes.UseVisualStyleBackColor = true;
            // 
            // pnlBoxButtons
            // 
            this.pnlBoxButtons.Controls.Add(this.lblBoxHoverInfo);
            this.pnlBoxButtons.Location = new System.Drawing.Point(8, 40);
            this.pnlBoxButtons.Name = "pnlBoxButtons";
            this.pnlBoxButtons.Size = new System.Drawing.Size(480, 240);
            this.pnlBoxButtons.TabIndex = 6;
            // 
            // lblBoxHoverInfo
            // 
            this.lblBoxHoverInfo.Location = new System.Drawing.Point(104, 0);
            this.lblBoxHoverInfo.Name = "lblBoxHoverInfo";
            this.lblBoxHoverInfo.Size = new System.Drawing.Size(272, 24);
            this.lblBoxHoverInfo.TabIndex = 27;
            this.lblBoxHoverInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // mnsMenu
            // 
            this.mnsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuHelp});
            this.mnsMenu.Location = new System.Drawing.Point(0, 0);
            this.mnsMenu.Name = "mnsMenu";
            this.mnsMenu.Size = new System.Drawing.Size(521, 24);
            this.mnsMenu.TabIndex = 3;
            this.mnsMenu.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileLoad,
            this.mnuFileSave,
            this.mnuFileSaveAs,
            this.mnsFileSep1,
            this.mnuFileBackup,
            this.mnsFileSep2,
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "File";
            // 
            // mnuFileLoad
            // 
            this.mnuFileLoad.Image = global::Mightyena.Properties.Resources.open;
            this.mnuFileLoad.Name = "mnuFileLoad";
            this.mnuFileLoad.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mnuFileLoad.Size = new System.Drawing.Size(192, 22);
            this.mnuFileLoad.Text = "Load Battery...";
            this.mnuFileLoad.Click += new System.EventHandler(this.mnuFileLoad_Click);
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Enabled = false;
            this.mnuFileSave.Image = global::Mightyena.Properties.Resources.save;
            this.mnuFileSave.Name = "mnuFileSave";
            this.mnuFileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuFileSave.Size = new System.Drawing.Size(192, 22);
            this.mnuFileSave.Text = "Save Battery";
            this.mnuFileSave.Click += new System.EventHandler(this.mnuFileSave_Click);
            // 
            // mnuFileSaveAs
            // 
            this.mnuFileSaveAs.Enabled = false;
            this.mnuFileSaveAs.Image = global::Mightyena.Properties.Resources.save_as;
            this.mnuFileSaveAs.Name = "mnuFileSaveAs";
            this.mnuFileSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.S)));
            this.mnuFileSaveAs.Size = new System.Drawing.Size(192, 22);
            this.mnuFileSaveAs.Text = "Save As...";
            this.mnuFileSaveAs.Click += new System.EventHandler(this.mnuFileSaveAs_Click);
            // 
            // mnsFileSep1
            // 
            this.mnsFileSep1.Name = "mnsFileSep1";
            this.mnsFileSep1.Size = new System.Drawing.Size(189, 6);
            // 
            // mnuFileBackup
            // 
            this.mnuFileBackup.Checked = true;
            this.mnuFileBackup.CheckOnClick = true;
            this.mnuFileBackup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuFileBackup.Name = "mnuFileBackup";
            this.mnuFileBackup.Size = new System.Drawing.Size(192, 22);
            this.mnuFileBackup.Text = "Auto Backup";
            // 
            // mnsFileSep2
            // 
            this.mnsFileSep2.Name = "mnsFileSep2";
            this.mnsFileSep2.Size = new System.Drawing.Size(189, 6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(192, 22);
            this.mnuFileExit.Text = "Exit";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuHelp.Text = "Help";
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Size = new System.Drawing.Size(116, 22);
            this.mnuHelpAbout.Text = "About...";
            this.mnuHelpAbout.Click += new System.EventHandler(this.mnuHelpAbout_Click);
            // 
            // dlgOpen
            // 
            this.dlgOpen.Filter = "Battery Save Files (*.sav)|*.sav";
            this.dlgOpen.Title = "Load Battery";
            // 
            // dlgSaveAs
            // 
            this.dlgSaveAs.Filter = "Battery Save Files (*.sav)|*.sav";
            this.dlgSaveAs.Title = "Save Battery As";
            // 
            // dlgImport
            // 
            this.dlgImport.Filter = "Pokémon Data Files (*.pkm)|*.pkm";
            this.dlgImport.Title = "Import Pokémon into Empty Slot";
            // 
            // pnlBoxNumbers
            // 
            this.pnlBoxNumbers.Controls.Add(this.cmdBoxSettings);
            this.pnlBoxNumbers.Controls.Add(this.label1);
            this.pnlBoxNumbers.Location = new System.Drawing.Point(8, 8);
            this.pnlBoxNumbers.Name = "pnlBoxNumbers";
            this.pnlBoxNumbers.Size = new System.Drawing.Size(480, 24);
            this.pnlBoxNumbers.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 224);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(464, 16);
            this.label1.TabIndex = 27;
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cmdBoxSettings
            // 
            this.cmdBoxSettings.Location = new System.Drawing.Point(456, 0);
            this.cmdBoxSettings.Name = "cmdBoxSettings";
            this.cmdBoxSettings.Size = new System.Drawing.Size(24, 24);
            this.cmdBoxSettings.TabIndex = 28;
            this.cmdBoxSettings.Text = "...";
            this.cmdBoxSettings.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 353);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.mnsMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnsMenu;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mightyena: Gen-III Pokémon Save Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.tabs.ResumeLayout(false);
            this.tbpGeneral.ResumeLayout(false);
            this.fraParty.ResumeLayout(false);
            this.fraTrainer.ResumeLayout(false);
            this.fraTrainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCoins)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMoney)).EndInit();
            this.tbpBags.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picSelectedItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSelectedItemQuantity)).EndInit();
            this.tbpBoxes.ResumeLayout(false);
            this.pnlBoxButtons.ResumeLayout(false);
            this.mnsMenu.ResumeLayout(false);
            this.mnsMenu.PerformLayout();
            this.pnlBoxNumbers.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tbpGeneral;
        private System.Windows.Forms.MenuStrip mnsMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileLoad;
        private System.Windows.Forms.ToolStripMenuItem mnuFileSave;
        private System.Windows.Forms.ToolStripSeparator mnsFileSep1;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpAbout;
        private System.Windows.Forms.TabPage tbpBags;
        private System.Windows.Forms.TabPage tbpBoxes;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.TextBox txtSecretID;
        private System.Windows.Forms.Label lblSecretID;
        private System.Windows.Forms.TextBox txtTrainerID;
        private System.Windows.Forms.Label lblTrainerID;
        private System.Windows.Forms.Label lblCoins;
        private System.Windows.Forms.Label lblMoney;
        private System.Windows.Forms.ComboBox cmbGender;
        private System.Windows.Forms.Button cmdParty1;
        private System.Windows.Forms.GroupBox fraTrainer;
        private System.Windows.Forms.Label lblDebugInfo;
        private System.Windows.Forms.GroupBox fraParty;
        private System.Windows.Forms.Button cmdParty6;
        private System.Windows.Forms.Button cmdParty5;
        private System.Windows.Forms.Button cmdParty2;
        private System.Windows.Forms.Button cmdParty4;
        private System.Windows.Forms.Button cmdParty3;
        private System.Windows.Forms.Panel pnlBoxButtons;
        private System.Windows.Forms.Label lblPartyHoverInfo;
        private System.Windows.Forms.ToolStripMenuItem mnuFileSaveAs;
        private System.Windows.Forms.Label lblBoxHoverInfo;
        private System.Windows.Forms.NumericUpDown nudCoins;
        private System.Windows.Forms.NumericUpDown nudMoney;
        private System.Windows.Forms.OpenFileDialog dlgOpen;
        private System.Windows.Forms.SaveFileDialog dlgSaveAs;
        private System.Windows.Forms.RadioButton optBagBerry;
        private System.Windows.Forms.RadioButton optBagTM;
        private System.Windows.Forms.RadioButton optBagBalls;
        private System.Windows.Forms.RadioButton optBagKey;
        private System.Windows.Forms.RadioButton optBagItem;
        private System.Windows.Forms.RadioButton optBagPC;
        private System.Windows.Forms.Panel pnlBag;
        private System.Windows.Forms.ComboBox cmbSelectedItem;
        private System.Windows.Forms.PictureBox picSelectedItem;
        private System.Windows.Forms.NumericUpDown nudSelectedItemQuantity;
        private System.Windows.Forms.ToolStripMenuItem mnuFileBackup;
        private System.Windows.Forms.ToolStripSeparator mnsFileSep2;
        private System.Windows.Forms.OpenFileDialog dlgImport;
        private System.Windows.Forms.Panel pnlBoxNumbers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdBoxSettings;
    }
}

