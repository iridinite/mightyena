namespace Mightyena {
    partial class ItemBox {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.SuspendLayout();
            // 
            // ItemBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.MaximumSize = new System.Drawing.Size(30, 30);
            this.MinimumSize = new System.Drawing.Size(30, 30);
            this.Name = "ItemBox";
            this.Size = new System.Drawing.Size(30, 30);
            this.Click += new System.EventHandler(this.ItemBox_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ItemBox_Paint);
            this.MouseEnter += new System.EventHandler(this.ItemBox_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.ItemBox_MouseLeave);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
