﻿/*
 * Mightyena - A Gen-III Pokémon Save Editor
 * (C) Mika Molenkamp, 2017.
*/

using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Mightyena {

    public partial class FormMain : Form {

        private string saveFilePath;
        private bool dirty;
        //private readonly Button[] PcBoxButtons;

        public FormMain() {
            InitializeComponent();

            // generate the 30 buttons on the PC Box page
            //PcBoxButtons = new Button[30];
            for (int i = 0; i < 30; i++) {
                Button btn = new Button();
                btn.Size = new Size(92, 32);
                btn.Location = new Point(i % 5 * 97, i / 5 * 37);
                btn.Tag = i;
                btn.Text = "#" + i;
                btn.TextAlign = ContentAlignment.MiddleRight;
                btn.Click += BoxButton_Click;
                btn.Paint += BoxButton_Paint;
                btn.MouseEnter += BoxButton_MouseEnter;
                btn.MouseLeave += BoxButton_MouseLeave;
                //PcBoxButtons[i] = boxbt;
                pnlBoxButtons.Controls.Add(btn);
            }
        }

        private void FormMain_Load(object sender, EventArgs e) {
            Species.Load();
        }

        private void mnuFileExit_Click(object sender, EventArgs e) {
            Close();
        }

        private void OpenFile(string filename) {
            saveFilePath = filename;

            var format = CultureInfo.InvariantCulture.NumberFormat;
            Gen3Save sav = Gen3Save.FromFile(filename);
            Gen3Save.Inst = sav;

            // trainer page
            txtName.Text = sav.Name;
            cmbGender.SelectedIndex = sav.Gender;
            txtTrainerID.Text = (sav.TrainerID & 0xFFFF).ToString("D5", format);
            txtSecretID.Text = ((sav.TrainerID & 0xFFFF0000) >> 16).ToString("D5", format);
            nudMoney.Value = sav.Money;
            nudCoins.Value = sav.Coins;
            lblDebugInfo.Text = $"{sav.GameCode} | {sav.SaveIndexDesc} | {sav.SecurityKey:D10}";

            // box page
            nudBoxActive.Value = sav.BoxActive + 1;
            txtBoxName.Text = Gen3Save.Inst.BoxNames[sav.BoxActive];
            fraParty.Invalidate();

            // enable editing and saving controls
            dirty = false;
            tabs.Enabled = true;
            mnuFileSave.Enabled = false;
            mnuFileSaveAs.Enabled = true;
        }

        private void MakeDirty() {
            dirty = true;
            mnuFileSave.Enabled = true;
        }

        private void SaveFile() {
            Gen3Save.Inst.Save(saveFilePath);
            mnuFileSave.Enabled = false;
            dirty = false;
        }

        private void mnuHelpAbout_Click(object sender, EventArgs e) {
            FormAbout frm = new FormAbout();
            frm.ShowDialog();
        }

        private void PartyButton_Click(object sender, EventArgs e) {
            Button self = (Button)sender;
            int partyIndex = int.Parse((string)self.Tag);

            FormPokemonEdit frm = new FormPokemonEdit();
            frm.Target = Gen3Save.Inst.Team[partyIndex];
            if (frm.ShowDialog() == DialogResult.OK) {
                self.Invalidate();
                MakeDirty();
            }
        }

        private void PartyButton_Paint(object sender, PaintEventArgs e) {
            // must have an open save
            if (Gen3Save.Inst == null) return;

            Button self = (Button)sender;
            int partyIndex = int.Parse((string)self.Tag);

            Gen3Pokemon mon = Gen3Save.Inst.Team[partyIndex];
            if (mon.Exists) {
                // draw a picture of this mon
                self.Text = String.Empty;
                Utils.DrawPokemonSprite(e.Graphics, mon.Species.DexNumber, mon.Shiny);
            } else {
                self.Text = "(empty)";
            }
        }

        private void PartyButton_MouseEnter(object sender, EventArgs e) {
            int partyIndex = int.Parse((string)((Button)sender).Tag);

            // show info about this mon
            Gen3Pokemon mon = Gen3Save.Inst.Team[partyIndex];
            lblPartyHoverInfo.Text = mon.Exists
                ? mon.ToString()
                : "(empty)";
        }

        private void PartyButton_MouseLeave(object sender, EventArgs e) {
            lblPartyHoverInfo.Text = String.Empty;
        }

        private void BoxButton_Paint(object sender, PaintEventArgs e) {
            Button self = (Button)sender;
            int index = (int)self.Tag;
            int boxNo = (int)nudBoxActive.Value;

            Gen3Pokemon mon = Gen3Save.Inst.Box[(boxNo - 1) * 30 + index];
            if (mon.Exists) {
                self.Text = mon.Nickname;
                self.TextAlign = ContentAlignment.MiddleRight;
                Utils.DrawPokemonIcon(e.Graphics, mon.Species.DexNumber);
            } else {
                self.Text = "empty";
                self.TextAlign = ContentAlignment.MiddleCenter;
            }
        }

        private void BoxButton_Click(object sender, EventArgs e) {
            Button self = (Button)sender;
            int index = (int)self.Tag;
            int boxNo = (int)nudBoxActive.Value;

            FormPokemonEdit frm = new FormPokemonEdit();
            frm.Target = Gen3Save.Inst.Box[(boxNo - 1) * 30 + index];
            if (frm.ShowDialog() == DialogResult.OK) {
                self.Invalidate();
                MakeDirty();
            }
        }

        private void BoxButton_MouseEnter(object sender, EventArgs e) {
            int index = (int)((Button)sender).Tag;
            int boxNo = (int)nudBoxActive.Value;

            Gen3Pokemon mon = Gen3Save.Inst.Box[(boxNo - 1) * 30 + index];
            lblBoxHoverInfo.Text = mon.Exists
                ? mon.ToString()
                : "(empty)";
        }

        private void BoxButton_MouseLeave(object sender, EventArgs e) {
            lblBoxHoverInfo.Text = String.Empty;
        }

        private void nudBoxActive_ValueChanged(object sender, EventArgs e) {
            txtBoxName.Text = Gen3Save.Inst.BoxNames[(int)nudBoxActive.Value - 1];
            pnlBoxButtons.Invalidate();
        }

        private void mnuFileLoad_Click(object sender, EventArgs e) {
            if (dlgOpen.ShowDialog() == DialogResult.OK)
                OpenFile(dlgOpen.FileName);
        }

        private void mnuFileSave_Click(object sender, EventArgs e) {
            SaveFile();
        }

        private void mnuFileSaveAs_Click(object sender, EventArgs e) {
            if (dlgSaveAs.ShowDialog() != DialogResult.OK) return;

            saveFilePath = dlgSaveAs.FileName;
            SaveFile();
        }

        private void MakeDirtyEventHandler(object sender, EventArgs e) {
            MakeDirty();
        }

    }

}
