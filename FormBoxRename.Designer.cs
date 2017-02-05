namespace Mightyena {
    partial class FormBoxRename {
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
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.lblPrompt = new System.Windows.Forms.Label();
            this.txtCurrentName = new System.Windows.Forms.TextBox();
            this.lblCurrentName = new System.Windows.Forms.Label();
            this.txtNewName = new System.Windows.Forms.TextBox();
            this.lblNewName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Enabled = false;
            this.cmdOK.Image = global::Mightyena.Properties.Resources.accept;
            this.cmdOK.Location = new System.Drawing.Point(56, 136);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(104, 24);
            this.cmdOK.TabIndex = 4;
            this.cmdOK.Text = "Accept";
            this.cmdOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdOK.UseVisualStyleBackColor = true;
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Image = global::Mightyena.Properties.Resources.decline;
            this.cmdCancel.Location = new System.Drawing.Point(168, 136);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(104, 24);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // lblPrompt
            // 
            this.lblPrompt.Location = new System.Drawing.Point(16, 16);
            this.lblPrompt.Name = "lblPrompt";
            this.lblPrompt.Size = new System.Drawing.Size(256, 32);
            this.lblPrompt.TabIndex = 6;
            this.lblPrompt.Text = "Prompt";
            // 
            // txtCurrentName
            // 
            this.txtCurrentName.Location = new System.Drawing.Point(104, 56);
            this.txtCurrentName.MaxLength = 8;
            this.txtCurrentName.Name = "txtCurrentName";
            this.txtCurrentName.ReadOnly = true;
            this.txtCurrentName.Size = new System.Drawing.Size(168, 20);
            this.txtCurrentName.TabIndex = 2;
            // 
            // lblCurrentName
            // 
            this.lblCurrentName.Location = new System.Drawing.Point(16, 56);
            this.lblCurrentName.Name = "lblCurrentName";
            this.lblCurrentName.Size = new System.Drawing.Size(88, 20);
            this.lblCurrentName.TabIndex = 8;
            this.lblCurrentName.Text = "Current Name:";
            this.lblCurrentName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNewName
            // 
            this.txtNewName.Location = new System.Drawing.Point(104, 80);
            this.txtNewName.MaxLength = 8;
            this.txtNewName.Name = "txtNewName";
            this.txtNewName.Size = new System.Drawing.Size(168, 20);
            this.txtNewName.TabIndex = 1;
            this.txtNewName.TextChanged += new System.EventHandler(this.txtNewName_TextChanged);
            // 
            // lblNewName
            // 
            this.lblNewName.Location = new System.Drawing.Point(16, 80);
            this.lblNewName.Name = "lblNewName";
            this.lblNewName.Size = new System.Drawing.Size(88, 20);
            this.lblNewName.TabIndex = 10;
            this.lblNewName.Text = "New Name:";
            this.lblNewName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormBoxRename
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(289, 177);
            this.Controls.Add(this.lblNewName);
            this.Controls.Add(this.txtNewName);
            this.Controls.Add(this.lblCurrentName);
            this.Controls.Add(this.txtCurrentName);
            this.Controls.Add(this.lblPrompt);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormBoxRename";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Rename Box";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label lblPrompt;
        private System.Windows.Forms.TextBox txtCurrentName;
        private System.Windows.Forms.Label lblCurrentName;
        private System.Windows.Forms.TextBox txtNewName;
        private System.Windows.Forms.Label lblNewName;
    }
}