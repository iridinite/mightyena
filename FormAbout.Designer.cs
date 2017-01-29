namespace Mightyena {
    partial class FormAbout {
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblCredits = new System.Windows.Forms.Label();
            this.cmdClose = new System.Windows.Forms.Button();
            this.picWoof = new System.Windows.Forms.PictureBox();
            this.lblVersion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picWoof)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(112, 24);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(254, 13);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Mightyena - A Gen-III Pokémon Save Editor";
            // 
            // lblCredits
            // 
            this.lblCredits.Location = new System.Drawing.Point(112, 64);
            this.lblCredits.Name = "lblCredits";
            this.lblCredits.Size = new System.Drawing.Size(256, 80);
            this.lblCredits.TabIndex = 2;
            this.lblCredits.Text = "Programmed by Mika Molenkamp, for the lolz and for educational purposes.\r\n\r\nContr" +
    "ibute to the project on GitHub:\r\ngithub.com/iridinite/mightyena\r\n";
            // 
            // cmdClose
            // 
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Image = global::Mightyena.Properties.Resources.accept;
            this.cmdClose.Location = new System.Drawing.Point(256, 168);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(112, 24);
            this.cmdClose.TabIndex = 3;
            this.cmdClose.Text = "Indeed";
            this.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // picWoof
            // 
            this.picWoof.Location = new System.Drawing.Point(16, 16);
            this.picWoof.Name = "picWoof";
            this.picWoof.Size = new System.Drawing.Size(80, 80);
            this.picWoof.TabIndex = 0;
            this.picWoof.TabStop = false;
            this.picWoof.Paint += new System.Windows.Forms.PaintEventHandler(this.picWoof_Paint);
            // 
            // lblVersion
            // 
            this.lblVersion.Location = new System.Drawing.Point(112, 40);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(256, 16);
            this.lblVersion.TabIndex = 4;
            this.lblVersion.Text = "Version";
            // 
            // FormAbout
            // 
            this.AcceptButton = this.cmdClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdClose;
            this.ClientSize = new System.Drawing.Size(386, 209);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.lblCredits);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.picWoof);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAbout";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mightyena";
            ((System.ComponentModel.ISupportInitialize)(this.picWoof)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picWoof;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCredits;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Label lblVersion;
    }
}