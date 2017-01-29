/*
 * Mightyena - A Gen-III Pokémon Save Editor
 * (C) Mika Molenkamp, 2017.
*/

using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Mightyena {

    public partial class FormPokemonEdit : Form {
        
        public Gen3Pokemon Target { get; set; }

        private uint currentOTID;
        private uint currentPVal;

        private bool init;

        public FormPokemonEdit() {
            init = true;
            InitializeComponent();
            
            // initialize comboboxes with lists of possible items
            object[] movelist = Utils.MoveNames.ToArray();
            cmbSpecies.Items.AddRange(Species.SpeciesNames.ToArray());
            cmbMetLocation.Items.AddRange(Utils.LocationNames.ToArray());
            cmbItem.Items.AddRange(Utils.ItemNames.ToArray());
            cmbMove1.Items.AddRange(movelist);
            cmbMove2.Items.AddRange(movelist);
            cmbMove3.Items.AddRange(movelist);
            cmbMove4.Items.AddRange(movelist);
        }

        private void FormPokemonEdit_Load(object sender, System.EventArgs e) {
            // copy out all info on this 'mon and fill in the form
            this.Text = "Editing " + Target.Nickname;

            txtNickname.Text = Target.Nickname;
            txtTrainerName.Text = Target.OTName;
            cmbTrainerGender.SelectedIndex = (Target.Origins & 0x8000) >> 15;

            cmbSpecies.SelectedIndex = Target.Species.DexNumber - 1;
            cmbLanguage.SelectedItem = Target.Lang.ToString();
            cmbItem.SelectedIndex = Target.HeldItem;
            nudLevel.Value = Utils.GetLevelForExp(Target.Species.ExpGroup, Target.Experience);
            nudExp.Value = Target.Experience;

            cmbMove1.SelectedIndex = Target.Move1;
            cmbMove2.SelectedIndex = Target.Move2;
            cmbMove3.SelectedIndex = Target.Move3;
            cmbMove4.SelectedIndex = Target.Move4;
            nudPP1.Value = Target.PP1;
            nudPP2.Value = Target.PP2;
            nudPP3.Value = Target.PP3;
            nudPP4.Value = Target.PP4;
            cmbPPB1.SelectedIndex = Target.PPBonuses & 0x3;
            cmbPPB2.SelectedIndex = (Target.PPBonuses & 0xC) >> 2;
            cmbPPB3.SelectedIndex = (Target.PPBonuses & 0x30) >> 4;
            cmbPPB4.SelectedIndex = (Target.PPBonuses & 0xC0) >> 6;

            nudIVHP.Value = Target.IVHP;
            nudIVAttack.Value = Target.IVAttack;
            nudIVDefense.Value = Target.IVDefense;
            nudIVSpAttack.Value = Target.IVSpAttack;
            nudIVSpDefense.Value = Target.IVSpDefense;
            nudIVSpeed.Value = Target.IVSpeed;

            nudEVHP.Value = Target.EVHP;
            nudEVAttack.Value = Target.EVAttack;
            nudEVDefense.Value = Target.EVDefense;
            nudEVSpAttack.Value = Target.EVSpAttack;
            nudEVSpDefense.Value = Target.EVSpDefense;
            nudEVSpeed.Value = Target.EVSpeed;

            cmbPokeBall.SelectedIndex = ((Target.Origins & 0x7800) >> 11) - 1;
            cmbGameOfOrigin.SelectedIndex = (Target.Origins & 0x0780) >> 7;
            cmbMetLocation.SelectedIndex = Target.MetLocation;
            nudLevelMet.Value = Target.Origins & 0x003F;

            chkFatefulEncounter.Checked = Target.FatefulEncounter;

            currentOTID = Target.OTID;
            currentPVal = Target.Personality;
            UpdateDynamicStats();

            init = false;
        }

        private void picSprite_Paint(object sender, PaintEventArgs e) {
            // draw species sprite
            Utils.DrawPokemonSprite(
                e.Graphics,
                cmbSpecies.SelectedIndex + 1,
                Utils.GetIsShiny(currentOTID, currentPVal));
        }

        private void UpdateDynamicStats() {
            // update OT and Pval
            var format = CultureInfo.InvariantCulture.NumberFormat;
            txtTrainerID.Text = (currentOTID & 0xFFFF).ToString("D5", format);
            txtSecretID.Text = ((currentOTID & 0xFFFF0000) >> 16).ToString("D5", format);
            txtPval.Text = currentPVal.ToString("D10", format);

            // calculate gender based on gender ratio
            byte genderRatio = Species.ByDexNumber((ushort)(cmbSpecies.SelectedIndex + 1)).GenderRatio;
            if (genderRatio == 0) {
                lblGender.Text = "Male";
            } else if (genderRatio == 254) {
                lblGender.Text = "Female";
            } else if (genderRatio == 255) {
                lblGender.Text = "Genderless";
            } else {
                lblGender.Text = (currentPVal & 0xFF) >= genderRatio ? "Male" : "Female";
            }

            // update info
            lblAbility.Text = (currentPVal & 0x1) == 0 ? "Primary" : "Secondary";
            lblShiny.Text = Utils.GetIsShiny(currentOTID, currentPVal) ? "Yes" : "No";
            lblNature.Text = Utils.NatureNames[(int)(currentPVal % 25)];

            // redraw picture, shiny state may have changed
            picSprite.Invalidate();
        }

        private void txtPval_Leave(object sender, System.EventArgs e) {
            // validate info, if it's a valid uint, replace pval
            var format = CultureInfo.InvariantCulture.NumberFormat;
            if (uint.TryParse(txtPval.Text, NumberStyles.Integer, format, out currentPVal)) {
                txtPval.BackColor = Color.White;
                UpdateDynamicStats();
            } else {
                txtPval.BackColor = Color.IndianRed;
            }
        }

        private void cmbSpecies_SelectedIndexChanged(object sender, System.EventArgs e) {
            if (init) return;
            UpdateDynamicStats();
        }

        private void nudLevel_ValueChanged(object sender, System.EventArgs e) {
            if (init) return;
            nudExp.Value = Utils.GetExpForLevel(Target.Species.ExpGroup, (int)nudLevel.Value);
        }

        private void nudExp_ValueChanged(object sender, System.EventArgs e) {
            if (init) return;
            nudLevel.Value = Utils.GetLevelForExp(Target.Species.ExpGroup, (uint)nudExp.Value);
        }

        private void cmdEditPval_Click(object sender, System.EventArgs e) {
            // show a dialog where the user can generate a PVal
            FormPval frm = new FormPval();
            frm.Species = Species.ByDexNumber((ushort)(cmbSpecies.SelectedIndex + 1));
            frm.OTID = currentOTID;
            frm.PVal = currentPVal;

            if (frm.ShowDialog() != DialogResult.OK) return;
            currentPVal = frm.PVal;
            UpdateDynamicStats();
        }

        private void OTBoxes_Leave(object sender, System.EventArgs e) {
            // either of the OT ID boxes lost focus, validate input and change ID if we can
            ushort tid, sid;
            var format = CultureInfo.InvariantCulture.NumberFormat;

            if (ushort.TryParse(txtTrainerID.Text, NumberStyles.Integer, format, out tid)) {
                txtTrainerID.BackColor = Color.White;

                if (ushort.TryParse(txtSecretID.Text, NumberStyles.Integer, format, out sid)) {
                    txtSecretID.BackColor = Color.White;
                    currentOTID = ((uint)sid << 16) | tid;
                    UpdateDynamicStats();

                } else {
                    txtSecretID.BackColor = Color.IndianRed;
                }

            } else {
                txtTrainerID.BackColor = Color.IndianRed;
            }
        }

        private void cmdSetOT_Click(object sender, System.EventArgs e) {
            // set OT data to this save's trainer
            currentOTID = Gen3Save.Inst.TrainerID;
            txtTrainerName.Text = Gen3Save.Inst.Name;
            cmbTrainerGender.SelectedIndex = Gen3Save.Inst.Gender;
            UpdateDynamicStats();
        }

    }

}
