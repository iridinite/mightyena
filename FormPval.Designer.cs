namespace Mightyena {
    partial class FormPval {
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
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdAccept = new System.Windows.Forms.Button();
            this.fraGender = new System.Windows.Forms.GroupBox();
            this.optGenderDc = new System.Windows.Forms.RadioButton();
            this.optFemale = new System.Windows.Forms.RadioButton();
            this.optMale = new System.Windows.Forms.RadioButton();
            this.optAbilityDc = new System.Windows.Forms.RadioButton();
            this.optAbility2 = new System.Windows.Forms.RadioButton();
            this.optAbility1 = new System.Windows.Forms.RadioButton();
            this.cmbNature = new System.Windows.Forms.ComboBox();
            this.optNatureDc = new System.Windows.Forms.RadioButton();
            this.optNatureReq = new System.Windows.Forms.RadioButton();
            this.optShinyDc = new System.Windows.Forms.RadioButton();
            this.optShinyNo = new System.Windows.Forms.RadioButton();
            this.optShinyYes = new System.Windows.Forms.RadioButton();
            this.prgLoading = new System.Windows.Forms.ProgressBar();
            this.fraAbility = new System.Windows.Forms.GroupBox();
            this.fraNature = new System.Windows.Forms.GroupBox();
            this.fraShiny = new System.Windows.Forms.GroupBox();
            this.fraGender.SuspendLayout();
            this.fraAbility.SuspendLayout();
            this.fraNature.SuspendLayout();
            this.fraShiny.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Image = global::Mightyena.Properties.Resources.decline;
            this.cmdCancel.Location = new System.Drawing.Point(208, 320);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(104, 24);
            this.cmdCancel.TabIndex = 27;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // cmdAccept
            // 
            this.cmdAccept.Image = global::Mightyena.Properties.Resources.accept;
            this.cmdAccept.Location = new System.Drawing.Point(96, 320);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(104, 24);
            this.cmdAccept.TabIndex = 26;
            this.cmdAccept.Text = "Accept";
            this.cmdAccept.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdAccept.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdAccept.UseVisualStyleBackColor = true;
            this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // fraGender
            // 
            this.fraGender.Controls.Add(this.optGenderDc);
            this.fraGender.Controls.Add(this.optFemale);
            this.fraGender.Controls.Add(this.optMale);
            this.fraGender.Location = new System.Drawing.Point(16, 16);
            this.fraGender.Name = "fraGender";
            this.fraGender.Size = new System.Drawing.Size(296, 56);
            this.fraGender.TabIndex = 33;
            this.fraGender.TabStop = false;
            this.fraGender.Text = "Gender";
            // 
            // optGenderDc
            // 
            this.optGenderDc.AutoSize = true;
            this.optGenderDc.Checked = true;
            this.optGenderDc.Location = new System.Drawing.Point(184, 24);
            this.optGenderDc.Name = "optGenderDc";
            this.optGenderDc.Size = new System.Drawing.Size(75, 17);
            this.optGenderDc.TabIndex = 3;
            this.optGenderDc.TabStop = true;
            this.optGenderDc.Text = "Don\'t Care";
            this.optGenderDc.UseVisualStyleBackColor = true;
            // 
            // optFemale
            // 
            this.optFemale.AutoSize = true;
            this.optFemale.Location = new System.Drawing.Point(96, 24);
            this.optFemale.Name = "optFemale";
            this.optFemale.Size = new System.Drawing.Size(59, 17);
            this.optFemale.TabIndex = 2;
            this.optFemale.Text = "Female";
            this.optFemale.UseVisualStyleBackColor = true;
            // 
            // optMale
            // 
            this.optMale.AutoSize = true;
            this.optMale.Location = new System.Drawing.Point(16, 24);
            this.optMale.Name = "optMale";
            this.optMale.Size = new System.Drawing.Size(48, 17);
            this.optMale.TabIndex = 1;
            this.optMale.Text = "Male";
            this.optMale.UseVisualStyleBackColor = true;
            // 
            // optAbilityDc
            // 
            this.optAbilityDc.AutoSize = true;
            this.optAbilityDc.Checked = true;
            this.optAbilityDc.Location = new System.Drawing.Point(184, 24);
            this.optAbilityDc.Name = "optAbilityDc";
            this.optAbilityDc.Size = new System.Drawing.Size(75, 17);
            this.optAbilityDc.TabIndex = 3;
            this.optAbilityDc.TabStop = true;
            this.optAbilityDc.Text = "Don\'t Care";
            this.optAbilityDc.UseVisualStyleBackColor = true;
            // 
            // optAbility2
            // 
            this.optAbility2.AutoSize = true;
            this.optAbility2.Location = new System.Drawing.Point(96, 24);
            this.optAbility2.Name = "optAbility2";
            this.optAbility2.Size = new System.Drawing.Size(76, 17);
            this.optAbility2.TabIndex = 2;
            this.optAbility2.Text = "Secondary";
            this.optAbility2.UseVisualStyleBackColor = true;
            // 
            // optAbility1
            // 
            this.optAbility1.AutoSize = true;
            this.optAbility1.Location = new System.Drawing.Point(16, 24);
            this.optAbility1.Name = "optAbility1";
            this.optAbility1.Size = new System.Drawing.Size(59, 17);
            this.optAbility1.TabIndex = 1;
            this.optAbility1.Text = "Primary";
            this.optAbility1.UseVisualStyleBackColor = true;
            // 
            // cmbNature
            // 
            this.cmbNature.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNature.FormattingEnabled = true;
            this.cmbNature.Items.AddRange(new object[] {
            "Hardy",
            "Lonely",
            "Brave",
            "Adamant",
            "Naughty",
            "Bold",
            "Docile",
            "Relaxed",
            "Impish",
            "Lax",
            "Timid",
            "Hasty",
            "Serious",
            "Jolly",
            "Naive",
            "Modest",
            "Mild",
            "Quiet",
            "Bashful",
            "Rash",
            "Calm",
            "Gentle",
            "Sassy",
            "Careful",
            "Quirky"});
            this.cmbNature.Location = new System.Drawing.Point(80, 22);
            this.cmbNature.Name = "cmbNature";
            this.cmbNature.Size = new System.Drawing.Size(88, 21);
            this.cmbNature.TabIndex = 25;
            this.cmbNature.SelectedIndexChanged += new System.EventHandler(this.cmbNature_SelectedIndexChanged);
            // 
            // optNatureDc
            // 
            this.optNatureDc.AutoSize = true;
            this.optNatureDc.Checked = true;
            this.optNatureDc.Location = new System.Drawing.Point(184, 24);
            this.optNatureDc.Name = "optNatureDc";
            this.optNatureDc.Size = new System.Drawing.Size(75, 17);
            this.optNatureDc.TabIndex = 3;
            this.optNatureDc.TabStop = true;
            this.optNatureDc.Text = "Don\'t Care";
            this.optNatureDc.UseVisualStyleBackColor = true;
            // 
            // optNatureReq
            // 
            this.optNatureReq.AutoSize = true;
            this.optNatureReq.Location = new System.Drawing.Point(16, 24);
            this.optNatureReq.Name = "optNatureReq";
            this.optNatureReq.Size = new System.Drawing.Size(63, 17);
            this.optNatureReq.TabIndex = 1;
            this.optNatureReq.Text = "Must be";
            this.optNatureReq.UseVisualStyleBackColor = true;
            // 
            // optShinyDc
            // 
            this.optShinyDc.AutoSize = true;
            this.optShinyDc.Checked = true;
            this.optShinyDc.Location = new System.Drawing.Point(184, 24);
            this.optShinyDc.Name = "optShinyDc";
            this.optShinyDc.Size = new System.Drawing.Size(75, 17);
            this.optShinyDc.TabIndex = 3;
            this.optShinyDc.TabStop = true;
            this.optShinyDc.Text = "Don\'t Care";
            this.optShinyDc.UseVisualStyleBackColor = true;
            // 
            // optShinyNo
            // 
            this.optShinyNo.AutoSize = true;
            this.optShinyNo.Location = new System.Drawing.Point(96, 24);
            this.optShinyNo.Name = "optShinyNo";
            this.optShinyNo.Size = new System.Drawing.Size(71, 17);
            this.optShinyNo.TabIndex = 2;
            this.optShinyNo.Text = "Not Shiny";
            this.optShinyNo.UseVisualStyleBackColor = true;
            // 
            // optShinyYes
            // 
            this.optShinyYes.AutoSize = true;
            this.optShinyYes.Location = new System.Drawing.Point(16, 24);
            this.optShinyYes.Name = "optShinyYes";
            this.optShinyYes.Size = new System.Drawing.Size(51, 17);
            this.optShinyYes.TabIndex = 1;
            this.optShinyYes.Text = "Shiny";
            this.optShinyYes.UseVisualStyleBackColor = true;
            // 
            // prgLoading
            // 
            this.prgLoading.Location = new System.Drawing.Point(24, 280);
            this.prgLoading.Name = "prgLoading";
            this.prgLoading.Size = new System.Drawing.Size(280, 23);
            this.prgLoading.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.prgLoading.TabIndex = 31;
            this.prgLoading.Visible = false;
            // 
            // fraAbility
            // 
            this.fraAbility.Controls.Add(this.optAbilityDc);
            this.fraAbility.Controls.Add(this.optAbility2);
            this.fraAbility.Controls.Add(this.optAbility1);
            this.fraAbility.Location = new System.Drawing.Point(16, 80);
            this.fraAbility.Name = "fraAbility";
            this.fraAbility.Size = new System.Drawing.Size(296, 56);
            this.fraAbility.TabIndex = 34;
            this.fraAbility.TabStop = false;
            this.fraAbility.Text = "Ability";
            // 
            // fraNature
            // 
            this.fraNature.Controls.Add(this.cmbNature);
            this.fraNature.Controls.Add(this.optNatureDc);
            this.fraNature.Controls.Add(this.optNatureReq);
            this.fraNature.Location = new System.Drawing.Point(16, 144);
            this.fraNature.Name = "fraNature";
            this.fraNature.Size = new System.Drawing.Size(296, 56);
            this.fraNature.TabIndex = 35;
            this.fraNature.TabStop = false;
            this.fraNature.Text = "Nature";
            // 
            // fraShiny
            // 
            this.fraShiny.Controls.Add(this.optShinyDc);
            this.fraShiny.Controls.Add(this.optShinyNo);
            this.fraShiny.Controls.Add(this.optShinyYes);
            this.fraShiny.Location = new System.Drawing.Point(16, 208);
            this.fraShiny.Name = "fraShiny";
            this.fraShiny.Size = new System.Drawing.Size(296, 56);
            this.fraShiny.TabIndex = 35;
            this.fraShiny.TabStop = false;
            this.fraShiny.Text = "Shininess";
            // 
            // FormPval
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 361);
            this.Controls.Add(this.fraShiny);
            this.Controls.Add(this.fraNature);
            this.Controls.Add(this.fraAbility);
            this.Controls.Add(this.prgLoading);
            this.Controls.Add(this.fraGender);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdAccept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPval";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Generate Personality Value";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPval_FormClosing);
            this.Load += new System.EventHandler(this.FormPval_Load);
            this.fraGender.ResumeLayout(false);
            this.fraGender.PerformLayout();
            this.fraAbility.ResumeLayout(false);
            this.fraAbility.PerformLayout();
            this.fraNature.ResumeLayout(false);
            this.fraNature.PerformLayout();
            this.fraShiny.ResumeLayout(false);
            this.fraShiny.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdAccept;
        private System.Windows.Forms.GroupBox fraGender;
        private System.Windows.Forms.RadioButton optGenderDc;
        private System.Windows.Forms.RadioButton optFemale;
        private System.Windows.Forms.RadioButton optMale;
        private System.Windows.Forms.RadioButton optShinyDc;
        private System.Windows.Forms.RadioButton optShinyNo;
        private System.Windows.Forms.RadioButton optShinyYes;
        private System.Windows.Forms.RadioButton optNatureDc;
        private System.Windows.Forms.RadioButton optNatureReq;
        private System.Windows.Forms.ComboBox cmbNature;
        private System.Windows.Forms.RadioButton optAbilityDc;
        private System.Windows.Forms.RadioButton optAbility2;
        private System.Windows.Forms.RadioButton optAbility1;
        private System.Windows.Forms.ProgressBar prgLoading;
        private System.Windows.Forms.GroupBox fraAbility;
        private System.Windows.Forms.GroupBox fraNature;
        private System.Windows.Forms.GroupBox fraShiny;
    }
}