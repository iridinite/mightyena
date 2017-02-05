/*
 * Mightyena - A Gen-III Pokémon Save Editor
 * (C) Mika Molenkamp, 2017.
*/

using System.Windows.Forms;

namespace Mightyena {

    public partial class FormBoxRename : Form {

        public string NewName { get { return txtNewName.Text; } }

        public FormBoxRename(int box, string current) {
            InitializeComponent();
            lblPrompt.Text = $"What would you like to name Box {box}?";
            txtCurrentName.Text = current;
            txtNewName.Focus();
        }

        private void txtNewName_TextChanged(object sender, System.EventArgs e) {
            cmdOK.Enabled = txtNewName.TextLength > 0;
        }

    }

}
