/*
 * Mightyena - A Gen-III Pokémon Save Editor
 * (C) Mika Molenkamp, 2017.
*/

using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace Mightyena {

    public partial class FormMain : Form {

        private string saveFilePath;
        private string saveFileShort;

        private bool dirty;
        private bool canMakeDirty;

        private readonly ItemBox[] ItemBoxes;
        private readonly RadioButton[] BoxNumButtons;
        private int selectedBox;

        public FormMain() {
            InitializeComponent();

            // generate box number buttons
            BoxNumButtons = new RadioButton[14];
            for (int i = 0; i < 14; i++) {
                RadioButton rad = new RadioButton();
                rad.Appearance = Appearance.Button;
                rad.Size = new Size(28, 24);
                rad.Location = new Point(i * 30, 0);
                rad.Text = (i + 1).ToString();
                rad.TextAlign = ContentAlignment.MiddleCenter;
                rad.Tag = i;
                rad.CheckedChanged += (sender, args) => {
                    // if button got checked, update selected box ID and redraw
                    RadioButton self = (RadioButton)sender;
                    if (!self.Checked) return;
                    selectedBox = (int)self.Tag;
                    pnlBoxButtons.Invalidate();
                };
                pnlBoxNumbers.Controls.Add(rad);
                BoxNumButtons[i] = rad;
            }

            // generate the 30 buttons on the PC Box page
            for (int i = 0; i < 30; i++) {
                Button btn = new Button();
                btn.Size = new Size(92, 32);
                btn.Location = new Point(i % 5 * 97, i / 5 * 37 + 24);
                btn.Tag = i;
                btn.Text = "#" + i;
                btn.TextAlign = ContentAlignment.MiddleRight;
                btn.Click += BoxButton_Click;
                btn.Paint += BoxButton_Paint;
                btn.MouseEnter += BoxButton_MouseEnter;
                btn.MouseLeave += BoxButton_MouseLeave;
                pnlBoxButtons.Controls.Add(btn);
            }

            // generate controls for item slots
            cmbSelectedItem.Items.AddRange(Utils.ItemNames.ToArray());
            ItemBoxes = new ItemBox[64]; // largest bag is 64 entries (TM/HM in RSE)
            for (int i = 0; i < 64; i++) {
                ItemBox itb = new ItemBox();
                itb.Location = new Point(i % 13 * 36, i / 13 * 36);
                itb.BoxNo = i;
                itb.ItemClick += ItemBox_Click;
                itb.Visible = false;
                ItemBoxes[i] = itb;
                pnlBag.Controls.Add(itb);
            }
        }

        private void FormMain_Load(object sender, EventArgs e) {
            Species.Load();
            Nature.Load();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e) {
            if (!dirty) return;

            DialogResult dr = MessageBox.Show($"Save changes to {saveFileShort}?", "Mightyena",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
            switch (dr) {
                case DialogResult.Cancel:
                    e.Cancel = true;
                    return;
                case DialogResult.Yes:
                    SaveFile();
                    break;
            }
        }

        private void mnuFileExit_Click(object sender, EventArgs e) {
            Close();
        }

        private void OpenFile(string filename, string shortname) {
            var format = CultureInfo.InvariantCulture.NumberFormat;

            // try to load the save file
            Gen3Save sav;
            try {
                sav = Gen3Save.FromFile(filename);
                if (sav == null) return;

            } catch (InvalidDataException ex) {
                // show the error message
                MessageBox.Show(ex.Message, "Mightyena", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            canMakeDirty = false;
            Gen3Save.Inst = sav;

            saveFilePath = filename;
            saveFileShort = shortname;

            // trainer page
            txtName.Text = sav.Name;
            cmbGender.SelectedIndex = sav.Gender;
            txtTrainerID.Text = (sav.TrainerID & 0xFFFF).ToString("D5", format);
            txtSecretID.Text = ((sav.TrainerID & 0xFFFF0000) >> 16).ToString("D5", format);
            nudMoney.Value = sav.Money;
            nudCoins.Value = sav.Coins;
            lblDebugInfo.Text = $"{sav.GameCode} | {sav.SaveIndexDesc} | {sav.SecurityKey:D10}";

            // items page
            optBagItem.Checked = true;
            ShowBag(sav.ItemsPocket);

            // box page
            BoxNumButtons[sav.BoxActive].Checked = true;
            fraParty.Invalidate();

            // enable editing and saving controls
            dirty = false;
            tabs.Enabled = true;
            mnuFileSave.Enabled = false;
            mnuFileSaveAs.Enabled = true;
            this.Text = saveFileShort + " - Mightyena";
            canMakeDirty = true;
        }

        private void MakeDirty() {
            if (!canMakeDirty) return;

            dirty = true;
            mnuFileSave.Enabled = true;
            this.Text = "* " + saveFileShort + " - Mightyena";
        }

        private void SaveFile() {
            // make a backup of the previous file
            if (mnuFileBackup.Checked)
                File.Copy(saveFilePath, Path.ChangeExtension(saveFilePath, "sav.bak"), true);
            // write the new file
            Gen3Save.Inst.Save(saveFilePath);
            mnuFileSave.Enabled = false;
            dirty = false;
            this.Text = saveFileShort + " - Mightyena";
        }

        private bool EditPokemon(Gen3Pokemon mon, bool allowDelete = true) {
            if (mon.Exists) {
                // edit an existing 'mon slot
                FormPokemonEdit frm = new FormPokemonEdit(mon);
                frm.CanDelete = allowDelete;
                if (frm.ShowDialog() != DialogResult.OK) return false;

                // copy the edited pokemon
                frm.Target.CopyTo(mon);
            } else {
                // this slot is empty, user can import a file into the slot
                if (dlgImport.ShowDialog() != DialogResult.OK) return false;

                Gen3Pokemon tempmon = Gen3Pokemon.FromFile(dlgImport.FileName);
                if (tempmon == null) return false;

                FormPokemonEdit frm = new FormPokemonEdit(tempmon);
                frm.CanDelete = false; // the slot is already empty
                if (frm.ShowDialog() != DialogResult.OK) return false;

                // copy the edited pokemon to the real box slot
                frm.Target.CopyTo(mon);
            }

            return true;
        }

        private void mnuHelpAbout_Click(object sender, EventArgs e) {
            FormAbout frm = new FormAbout();
            frm.ShowDialog();
        }

        private void ItemBox_Click(int boxNo) {
            // select this new box
            int oldSelect = ItemBox.Selection;
            ItemBox.Selection = boxNo;
            // remove selection outline from the previously selected box
            if (oldSelect > -1)
                ItemBoxes[oldSelect].Invalidate();

            // update editing controls
            Gen3Item item = ItemBoxes[ItemBox.Selection].Item;
            Debug.Assert(item != null, "ItemBox has null Gen3Item ref");
            canMakeDirty = false;

            cmbSelectedItem.SelectedIndex = item.Index;
            nudSelectedItemQuantity.Value = item.Quantity;
            cmbSelectedItem.Enabled = true;
            nudSelectedItemQuantity.Enabled = item.Index > 0;
            picSelectedItem.Visible = true;
            picSelectedItem.Invalidate();

            canMakeDirty = true;
        }

        private void PartyButton_Click(object sender, EventArgs e) {
            Button self = (Button)sender;
            int partyIndex = int.Parse((string)self.Tag);
            Gen3Pokemon[] team = Gen3Save.Inst.Team;

            if (!EditPokemon(team[partyIndex],
                Gen3Save.Inst.TeamSize > 1)) // forbid deleting if this is last party member
                return;
            MakeDirty();

            // shift party pokemon so there are no empty slots in between
            uint teamSize;
            while (true) {
                teamSize = 0U;
                int hole = -1;
                for (int i = 0; i < 6; i++) {
                    if (team[i].Exists)
                        teamSize++;
                    // check if this entry is a hole
                    if (!team[i].Exists && hole == -1) {
                        hole = i;
                        continue;
                    }
                    // found an entry and there is a hole
                    if (team[i].Exists && hole != -1) {
                        // swap the entries
                        team[i].CopyTo(team[hole]);
                        team[i].Delete();
                        break;
                    }
                }

                // if there were no holes in the previous run, then exit
                if (hole == -1 || hole >= teamSize)
                    break;
            }
            // save the new number of party members
            Gen3Save.Inst.TeamSize = teamSize;
            // because we may have swapped mons around, redraw the entire party
            fraParty.Invalidate();
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

            Gen3Pokemon mon = Gen3Save.Inst.Box[selectedBox * 30 + index];
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

            if (EditPokemon(Gen3Save.Inst.Box[selectedBox * 30 + index])) {
                // refresh form
                self.Invalidate();
                MakeDirty();
            }
        }

        private void BoxButton_MouseEnter(object sender, EventArgs e) {
            int index = (int)((Button)sender).Tag;

            Gen3Pokemon mon = Gen3Save.Inst.Box[selectedBox * 30 + index];
            lblBoxHoverInfo.Text = mon.Exists
                ? mon.ToString()
                : "(empty)";
        }

        private void BoxButton_MouseLeave(object sender, EventArgs e) {
            lblBoxHoverInfo.Text = String.Empty;
        }

        private void mnuFileLoad_Click(object sender, EventArgs e) {
            if (dlgOpen.ShowDialog() == DialogResult.OK)
                OpenFile(dlgOpen.FileName, dlgOpen.SafeFileName);
        }

        private void mnuFileSave_Click(object sender, EventArgs e) {
            SaveFile();
        }

        private void mnuFileSaveAs_Click(object sender, EventArgs e) {
            if (dlgSaveAs.ShowDialog() != DialogResult.OK) return;

            saveFilePath = dlgSaveAs.FileName;
            SaveFile();
        }

        private void cmbSelectedItem_SelectedIndexChanged(object sender, EventArgs e) {
            // avoid making edits while changing item pages
            if (!canMakeDirty || !cmbSelectedItem.Enabled) return;
            // if No Item is selected, cannot modify quantity
            if (cmbSelectedItem.SelectedIndex > 0) {
                nudSelectedItemQuantity.Enabled = true;
                nudSelectedItemQuantity.Value = Math.Max(1, nudSelectedItemQuantity.Value);
            } else {
                nudSelectedItemQuantity.Enabled = false;
                nudSelectedItemQuantity.Value = 0;
            }
            // update item entry with new selection
            ItemBoxes[ItemBox.Selection].Item.Index = (ushort)cmbSelectedItem.SelectedIndex;
            ItemBoxes[ItemBox.Selection].Invalidate();
            picSelectedItem.Invalidate();
            MakeDirty();
        }

        private void nudSelectedItemQuantity_ValueChanged(object sender, EventArgs e) {
            // avoid making edits while changing item pages
            if (!canMakeDirty || !cmbSelectedItem.Enabled) return;

            // if quantity is zero, set type to No Item
            if (nudSelectedItemQuantity.Value == 0)
                cmbSelectedItem.SelectedIndex = 0;

            ItemBoxes[ItemBox.Selection].Item.Quantity = (ushort)nudSelectedItemQuantity.Value;
            ItemBoxes[ItemBox.Selection].Invalidate();
            MakeDirty();
        }

        private void ShowBag(Gen3Item[] bag) {
            // clear selection
            ItemBox.Selection = -1;
            // update and hide/show required boxes
            int len = bag.Length; // cache
            for (int i = 0; i < 64; i++) {
                ItemBoxes[i].Item = i < len ? bag[i] : null;
                ItemBoxes[i].Visible = i < len;
                ItemBoxes[i].Invalidate();
            }
            // make sure to disable editing controls until a box is selected
            cmbSelectedItem.Enabled = false;
            cmbSelectedItem.SelectedIndex = 0;
            nudSelectedItemQuantity.Enabled = false;
            picSelectedItem.Visible = false;
        }

        private void optBagPC_CheckedChanged(object sender, EventArgs e) {
            if (!optBagPC.Checked) return;
            ShowBag(Gen3Save.Inst.ItemsPC);
        }

        private void optBagItem_CheckedChanged(object sender, EventArgs e) {
            if (!optBagItem.Checked) return;
            ShowBag(Gen3Save.Inst.ItemsPocket);
        }

        private void optBagKey_CheckedChanged(object sender, EventArgs e) {
            if (!optBagKey.Checked) return;
            ShowBag(Gen3Save.Inst.ItemsKey);
        }

        private void optBagBalls_CheckedChanged(object sender, EventArgs e) {
            if (!optBagBalls.Checked) return;
            ShowBag(Gen3Save.Inst.ItemsBall);
        }

        private void optBagTM_CheckedChanged(object sender, EventArgs e) {
            if (!optBagTM.Checked) return;
            ShowBag(Gen3Save.Inst.ItemsTM);
        }

        private void optBagBerry_CheckedChanged(object sender, EventArgs e) {
            if (!optBagBerry.Checked) return;
            ShowBag(Gen3Save.Inst.ItemsBerry);
        }

        private void picSelectedItem_Paint(object sender, PaintEventArgs e) {
            if (ItemBox.Selection > -1)
                Utils.DrawItemIcon(e.Graphics, cmbSelectedItem.SelectedIndex, 0, 0);
        }

        private void txtName_TextChanged(object sender, EventArgs e) {
            if (!canMakeDirty) return;
            Gen3Save.Inst.Name.SetValue(txtName.Text);
            MakeDirty();
        }

        private void cmbGender_SelectedIndexChanged(object sender, EventArgs e) {
            if (!canMakeDirty) return;
            Gen3Save.Inst.Gender = (byte)cmbGender.SelectedIndex;
            MakeDirty();
        }

        private void txtTrainerID_TextChanged(object sender, EventArgs e) {
            if (!canMakeDirty) return;

            // either of the OT ID boxes lost focus, validate input and change ID if we can
            ushort tid, sid;
            var format = CultureInfo.InvariantCulture.NumberFormat;

            if (ushort.TryParse(txtTrainerID.Text, NumberStyles.Integer, format, out tid)) {
                txtTrainerID.BackColor = Color.White;

                if (ushort.TryParse(txtSecretID.Text, NumberStyles.Integer, format, out sid)) {
                    txtSecretID.BackColor = Color.White;
                    Gen3Save.Inst.TrainerID = ((uint)sid << 16) | tid;
                    MakeDirty();

                } else {
                    txtSecretID.BackColor = Color.IndianRed;
                }

            } else {
                txtTrainerID.BackColor = Color.IndianRed;
            }
        }

        private void nudMoney_ValueChanged(object sender, EventArgs e) {
            if (!canMakeDirty) return;
            Gen3Save.Inst.Money = (uint)nudMoney.Value;
            MakeDirty();
        }

        private void nudCoins_ValueChanged(object sender, EventArgs e) {
            if (!canMakeDirty) return;
            Gen3Save.Inst.Coins = (ushort)nudCoins.Value;
            MakeDirty();
        }

        private void cmdBoxSettings_Click(object sender, EventArgs e) {
            mnuBoxName.Text = Gen3Save.Inst.BoxNames[selectedBox];
            cmsBoxSettings.Show(cmdBoxSettings, new Point(0, cmdBoxSettings.Height));
        }

        private void mnuBoxRename_Click(object sender, EventArgs e) {
            FormBoxRename frm = new FormBoxRename(selectedBox + 1, mnuBoxName.Text);
            if (frm.ShowDialog() == DialogResult.OK) {
                Gen3Save.Inst.BoxNames[selectedBox].SetValue(frm.NewName);
                MakeDirty();
            }
        }

    }

}
