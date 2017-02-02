/*
 * Mightyena - A Gen-III Pokémon Save Editor
 * (C) Mika Molenkamp, 2017.
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Mightyena {

    public partial class FormPokemonEdit : Form {

        public Gen3Pokemon Target { get; }

        private bool init;

        public FormPokemonEdit(Gen3Pokemon editTarget) {
            init = true;
            InitializeComponent();

            // copy the target pokemon so we can freely change stats without affecting the original
            byte[] targetFrame = new byte[100];
            Target = new Gen3Pokemon(targetFrame, 0, false);
            editTarget.CopyTo(Target);

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

        private void FormPokemonEdit_Load(object sender, EventArgs e) {
            // copy out all info on this 'mon and fill in the form
            this.Text = "Editing " + Target.Nickname;
            UpdateEverything();
            init = false;
        }

        private void picSprite_Paint(object sender, PaintEventArgs e) {
            // draw species sprite
            Utils.DrawPokemonSprite(e.Graphics, cmbSpecies.SelectedIndex + 1, Utils.GetIsShiny(Target.OTID, Target.Personality));
        }

        private void picItem_Paint(object sender, PaintEventArgs e) {
            Utils.DrawItemIcon(e.Graphics, cmbItem.SelectedIndex, 0, 0);
        }

        private void UpdateEverything() {
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

            nudFriendship.Value = Target.Friendship;
            chkMark0.Checked = (Target.Markings & 0x1) > 0;
            chkMark1.Checked = (Target.Markings & 0x2) > 0;
            chkMark2.Checked = (Target.Markings & 0x4) > 0;
            chkMark3.Checked = (Target.Markings & 0x8) > 0;

            cmbPokeBall.SelectedIndex = ((Target.Origins & 0x7800) >> 11) - 1;
            cmbGameOfOrigin.SelectedIndex = (Target.Origins & 0x0780) >> 7;
            cmbMetLocation.SelectedIndex = Target.MetLocation;
            nudLevelMet.Value = Target.Origins & 0x003F;

            chkFatefulEncounter.Checked = Target.FatefulEncounter;
            chkEgg.Checked = (Target.Genes & 0x40000000) > 0;

            nudPokerusStrain.Value = Target.PokeRus & 0xF;
            nudPokerusDays.Value = (Target.PokeRus & 0xF0) >> 4;
            Pokerus_ValueChanged(null, EventArgs.Empty); // make sure the text is updated

            UpdateDynamicStats();
        }

        private void UpdateDynamicStats() {
            // update OT and Pval
            var format = CultureInfo.InvariantCulture.NumberFormat;
            txtTrainerID.Text = (Target.OTID & 0xFFFF).ToString("D5", format);
            txtSecretID.Text = ((Target.OTID & 0xFFFF0000) >> 16).ToString("D5", format);
            txtPval.Text = Target.Personality.ToString("D10", format);

            // calculate gender based on gender ratio
            byte genderRatio = Species.ByDexNumber((ushort)(cmbSpecies.SelectedIndex + 1)).GenderRatio;
            if (genderRatio == 0) {
                lblGender.Text = "Male";
            } else if (genderRatio == 254) {
                lblGender.Text = "Female";
            } else if (genderRatio == 255) {
                lblGender.Text = "Genderless";
            } else {
                lblGender.Text = (Target.Personality & 0xFF) >= genderRatio ? "Male" : "Female";
            }

            // update info
            lblAbility.Text = (Target.Personality & 0x1) == 0 ? "Primary" : "Secondary";
            lblShiny.Text = Utils.GetIsShiny(Target.OTID, Target.Personality) ? "Yes" : "No";
            lblNature.Text = Utils.NatureNames[(int)(Target.Personality % 25)];

            // redraw picture, shiny state may have changed
            picSprite.Invalidate();
        }

        private void txtPval_Leave(object sender, EventArgs e) {
            // validate info, if it's a valid uint, replace pval
            uint pval;
            var format = CultureInfo.InvariantCulture.NumberFormat;
            if (uint.TryParse(txtPval.Text, NumberStyles.Integer, format, out pval)) {
                Target.Personality = pval;
                txtPval.BackColor = Color.White;
                UpdateDynamicStats();
            } else {
                txtPval.BackColor = Color.IndianRed;
            }
        }

        private void cmbSpecies_SelectedIndexChanged(object sender, EventArgs e) {
            if (init) return;
            UpdateDynamicStats();
        }

        private void nudLevel_ValueChanged(object sender, EventArgs e) {
            if (init) return;
            nudExp.Value = Utils.GetExpForLevel(Target.Species.ExpGroup, (int)nudLevel.Value);
        }

        private void nudExp_ValueChanged(object sender, EventArgs e) {
            if (init) return;
            nudLevel.Value = Utils.GetLevelForExp(Target.Species.ExpGroup, (uint)nudExp.Value);
        }

        private void cmdEditPval_Click(object sender, EventArgs e) {
            // show a dialog where the user can generate a PVal
            FormPval frm = new FormPval();
            frm.Species = Species.ByDexNumber((ushort)(cmbSpecies.SelectedIndex + 1));
            frm.OTID = Target.OTID;
            frm.PVal = Target.Personality;

            if (frm.ShowDialog() != DialogResult.OK) return;
            Target.Personality = frm.PVal;
            UpdateDynamicStats();
        }

        private void OTBoxes_Leave(object sender, EventArgs e) {
            // either of the OT ID boxes lost focus, validate input and change ID if we can
            ushort tid, sid;
            var format = CultureInfo.InvariantCulture.NumberFormat;

            if (ushort.TryParse(txtTrainerID.Text, NumberStyles.Integer, format, out tid)) {
                txtTrainerID.BackColor = Color.White;

                if (ushort.TryParse(txtSecretID.Text, NumberStyles.Integer, format, out sid)) {
                    txtSecretID.BackColor = Color.White;
                    Target.OTID = ((uint)sid << 16) | tid;
                    UpdateDynamicStats();

                } else {
                    txtSecretID.BackColor = Color.IndianRed;
                }

            } else {
                txtTrainerID.BackColor = Color.IndianRed;
            }
        }

        private void cmdSetOT_Click(object sender, EventArgs e) {
            // set OT data to this save's trainer
            Target.OTID = Gen3Save.Inst.TrainerID;
            txtTrainerName.Text = Gen3Save.Inst.Name;
            cmbTrainerGender.SelectedIndex = Gen3Save.Inst.Gender;
            UpdateDynamicStats();
        }

        private void cmdGenerateIV_Click(object sender, EventArgs e) {
            Button self = (Button)sender;
            cmsGenerateIV.Show(self, new Point(0, self.Size.Height));
        }

        private void cmdGenerateEV_Click(object sender, EventArgs e) {
            Button self = (Button)sender;
            cmsGenerateEV.Show(self, new Point(0, self.Size.Height));
        }

        private void mnuIV31_Click(object sender, EventArgs e) {
            nudIVHP.Value = 31;
            nudIVAttack.Value = 31;
            nudIVDefense.Value = 31;
            nudIVSpeed.Value = 31;
            nudIVSpAttack.Value = 31;
            nudIVSpDefense.Value = 31;
        }

        private void mnuIV0_Click(object sender, EventArgs e) {
            nudIVHP.Value = 0;
            nudIVAttack.Value = 0;
            nudIVDefense.Value = 0;
            nudIVSpeed.Value = 0;
            nudIVSpAttack.Value = 0;
            nudIVSpDefense.Value = 0;
        }

        private void RandomizeIV(int min, int max) {
            max++; // upperbound in rng is exclusive
            nudIVHP.Value = Utils.RandInt(min, max);
            nudIVAttack.Value = Utils.RandInt(min, max);
            nudIVDefense.Value = Utils.RandInt(min, max);
            nudIVSpeed.Value = Utils.RandInt(min, max);
            nudIVSpAttack.Value = Utils.RandInt(min, max);
            nudIVSpDefense.Value = Utils.RandInt(min, max);
        }

        private void mnuIVRandNatural_Click(object sender, EventArgs e) {
            RandomizeIV(0, 31);
        }

        private void mnuIVRandPoor_Click(object sender, EventArgs e) {
            RandomizeIV(0, 12);
        }

        private void mnuIVRandAverage_Click(object sender, EventArgs e) {
            RandomizeIV(6, 24);
        }

        private void mnuIVRandGood_Click(object sender, EventArgs e) {
            RandomizeIV(12, 31);
        }

        private void mnuEV255_Click(object sender, EventArgs e) {
            nudEVHP.Value = 255;
            nudEVAttack.Value = 255;
            nudEVDefense.Value = 255;
            nudEVSpeed.Value = 255;
            nudEVSpAttack.Value = 255;
            nudEVSpDefense.Value = 255;
        }

        private void mnuEV0_Click(object sender, EventArgs e) {
            nudEVHP.Value = 0;
            nudEVAttack.Value = 0;
            nudEVDefense.Value = 0;
            nudEVSpeed.Value = 0;
            nudEVSpAttack.Value = 0;
            nudEVSpDefense.Value = 0;
        }

        private void mnuEVRedist520_Click(object sender, EventArgs e) {
            List<int> statlist = new List<int> { 0, 1, 2, 3, 4, 5 };
            List<int> lottery = new List<int>(21);
            int[] evs = new int[6];

            // randomly prioritize stats
            for (int i = 6; i > 0; i--) {
                // get a random stat and give it a number of lots equal to i
                int stat = statlist[Utils.RandInt(0, statlist.Count)];
                for (int j = 0; j < i; j++) lottery.Add(stat);
                // make sure this stat doesn't get picked again
                statlist.Remove(stat);
            }

            // now randomly assign 520 EV points using the lottery
            for (int i = 0; i < 520; i++)
                evs[lottery[Utils.RandInt(0, lottery.Count)]]++;

            // and save the generated EVs
            nudEVHP.Value = evs[0];
            nudEVAttack.Value = evs[1];
            nudEVDefense.Value = evs[2];
            nudEVSpeed.Value = evs[3];
            nudEVSpAttack.Value = evs[4];
            nudEVSpDefense.Value = evs[5];
        }

        private void Save() {
            Target.Nickname.SetValue(txtNickname.Text);
            Target.OTName.SetValue(txtTrainerName.Text);

            Target.SpeciesIndex = Species.ByDexNumber((ushort)(cmbSpecies.SelectedIndex + 1)).SpeciesIndex;
            Target.Lang = (Language)Enum.Parse(typeof(Language), (string)cmbLanguage.SelectedItem);
            Target.HeldItem = (ushort)cmbItem.SelectedIndex;
            Target.Experience = (uint)nudExp.Value;

            Target.Move1 = (ushort)cmbMove1.SelectedIndex;
            Target.Move2 = (ushort)cmbMove2.SelectedIndex;
            Target.Move3 = (ushort)cmbMove3.SelectedIndex;
            Target.Move4 = (ushort)cmbMove4.SelectedIndex;
            Target.PP1 = (byte)nudPP1.Value;
            Target.PP2 = (byte)nudPP2.Value;
            Target.PP3 = (byte)nudPP3.Value;
            Target.PP4 = (byte)nudPP4.Value;
            Target.PPBonuses =
                (byte)(cmbPPB1.SelectedIndex | (cmbPPB2.SelectedIndex << 2) | (cmbPPB3.SelectedIndex << 4) | (cmbPPB4.SelectedIndex << 6));
            cmbPPB1.SelectedIndex = Target.PPBonuses & 0x3;
            cmbPPB2.SelectedIndex = (Target.PPBonuses & 0xC) >> 2;
            cmbPPB3.SelectedIndex = (Target.PPBonuses & 0x30) >> 4;
            cmbPPB4.SelectedIndex = (Target.PPBonuses & 0xC0) >> 6;

            Target.IVHP = (byte)nudIVHP.Value;
            Target.IVAttack = (byte)nudIVAttack.Value;
            Target.IVDefense = (byte)nudIVDefense.Value;
            Target.IVSpAttack = (byte)nudIVSpAttack.Value;
            Target.IVSpDefense = (byte)nudIVSpDefense.Value;
            Target.IVSpeed = (byte)nudIVSpeed.Value;

            Target.EVHP = (byte)nudEVHP.Value;
            Target.EVAttack = (byte)nudEVAttack.Value;
            Target.EVDefense = (byte)nudEVDefense.Value;
            Target.EVSpAttack = (byte)nudEVSpAttack.Value;
            Target.EVSpDefense = (byte)nudEVSpDefense.Value;
            Target.EVSpeed = (byte)nudEVSpeed.Value;

            Target.Friendship = (byte)nudFriendship.Value;
            Target.Markings = (byte)(
                (chkMark0.Checked ? 0x1 : 0) |
                (chkMark1.Checked ? 0x2 : 0) |
                (chkMark2.Checked ? 0x4 : 0) |
                (chkMark3.Checked ? 0x8 : 0));

            Target.Origins = (ushort)
                ((cmbTrainerGender.SelectedIndex << 15) |
                ((cmbPokeBall.SelectedIndex + 1) << 11) |
                (cmbGameOfOrigin.SelectedIndex << 7) |
                (ushort)nudLevelMet.Value);
            Target.MetLocation = (byte)cmbMetLocation.SelectedIndex;

            Target.FatefulEncounter = chkFatefulEncounter.Checked;
            Target.Genes = (Target.Genes & ~0xC0000000)
                           | (Target.Species.HasSecondAbility ? ((Target.Personality & 0x1) << 31) : 0) // ability flag
                           | ((chkEgg.Checked ? 1U : 0U) << 30); // egg flag

            Target.PokeRus = (byte)((int)nudPokerusStrain.Value | ((int)nudPokerusDays.Value << 4));

            Target.Save();
        }

        private void cmdAccept_Click(object sender, EventArgs e) {
            // save all information back to the target Pokémon entry
            Save();

            DialogResult = DialogResult.OK;
            Close();
        }

        private void Pokerus_ValueChanged(object sender, EventArgs e) {
            if (nudPokerusStrain.Value == 0) {
                fraPokerus.Text = "Pokérus (Not Infected)";
            } else if (nudPokerusDays.Value == 0) {
                fraPokerus.Text = "Pokérus (Cured)";
            } else {
                fraPokerus.Text = "Pokérus (Infected)";
            }
        }

        private void chkEgg_CheckedChanged(object sender, EventArgs e) {
            lblFriendship.Text = chkEgg.Checked ? "Egg Cycles:" : "Friendship:";
        }

        private void cmbItem_SelectedIndexChanged(object sender, EventArgs e) {
            // if item is selected, open some room for the item sprite
            if (cmbItem.SelectedIndex > 0) {
                cmbItem.Size = new Size(104, 21);
                cmbItem.Location = new Point(272, 72);
            } else {
                cmbItem.Size = new Size(128, 21);
                cmbItem.Location = new Point(248, 72);
            }
            // redraw item sprite
            cmbItem.SelectionStart = 0;
            cmbItem.SelectionLength = 0;
            picItem.Invalidate();
        }

        private void cmdImport_Click(object sender, EventArgs e) {
            // user picks a file
            if (dlgImport.ShowDialog() != DialogResult.OK) return;

            // read the pkm file and replace the edit target
            var filemon = Gen3Pokemon.FromFile(dlgImport.FileName);
            filemon.CopyTo(Target);
            Target.Save(); // recalculate stats

            // refresh all UI elements
            init = true;
            UpdateEverything();
            init = false;
        }

        private void cmdExport_Click(object sender, EventArgs e) {
            // user picks a save location
            if (dlgExport.ShowDialog() != DialogResult.OK) return;

            // write the editing target to disk
            Target.Export(dlgExport.FileName);
        }

    }

}
