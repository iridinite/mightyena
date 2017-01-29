/*
 * Mightyena - A Gen-III Pokémon Save Editor
 * (C) Mika Molenkamp, 2017.
*/

using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace Mightyena {

    public partial class FormAbout : Form {

        private static readonly List<int> PictureCandidates = new List<int> {
            58,  // Growlithe
            59,  // Arcanine
            136, // Flareon
            157, // Typhlosion
            172, // Pichu
            196, // Espeon
            261, // Poochyena
        };

        private readonly int chosenPokemon;

        public FormAbout() {
            InitializeComponent();
            lblVersion.Text = "Version " + Assembly.GetExecutingAssembly().GetName().Version;

            // tiny chance to show some random 'mon
            chosenPokemon = Utils.RandInt(9) < 1
                ? PictureCandidates[Utils.RandInt(PictureCandidates.Count)]
                : 262; // program namesake
        }

        private void picWoof_Paint(object sender, PaintEventArgs e) {
            Utils.DrawPokemonSprite(e.Graphics, chosenPokemon);
        }

        private void cmdClose_Click(object sender, System.EventArgs e) {
            Close();
        }

    }

}
