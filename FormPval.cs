/*
 * Mightyena - A Gen-III Pokémon Save Editor
 * (C) Mika Molenkamp, 2017.
*/

using System;
using System.Threading;
using System.Windows.Forms;

namespace Mightyena {

    public partial class FormPval : Form {

        private struct PvalRequires {
            public int ThreadNo;
            public bool CheckShiny, CheckGender, CheckNature, CheckAbility;
            public bool Shiny;
            public bool Ability;
            public bool Gender;
            public int Nature;
        }

        public Species Species { get; set; }
        public uint OTID { get; set; }
        public uint PVal { get; set; }

        private Thread[] threads;
        private bool init, threadStop;
        private readonly object threadLock = new object();

        public FormPval() {
            init = true;
            InitializeComponent();
            cmbNature.SelectedIndex = Utils.RandInt(0, cmbNature.Items.Count);
            init = false;
        }

        private void FormPval_FormClosing(object sender, FormClosingEventArgs e) {
            // inform threads to stop working
            lock (threadLock) {
                threadStop = true;
            }
        }
        
        private void OnGeneratorDone() {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void FormPval_Load(object sender, EventArgs e) {
            // cannot require gender for species with no gender ratio
            if (Species.GenderRatio == 0 || Species.GenderRatio >= 254) {
                optGenderDc.Checked = true;
                fraGender.Enabled = false;
            }
            // cannot require ability for species with only one ability
            if (!Species.HasSecondAbility) {
                optAbilityDc.Checked = true;
                fraAbility.Enabled = false;
            }
        }
        
        private void GeneratorWorker(object state) {
            // keep generating random IDs until we find a match
            PvalRequires req = (PvalRequires)state;
            Random rng = new Random(unchecked((int)(DateTime.Now.Ticks + req.ThreadNo * 3989)));

            // precalculate some shiny formula vars
            ushort otid = (ushort)(OTID & 0xFFFF);
            ushort scid = (ushort)((OTID & 0xFFFF0000) >> 16);

            byte[] buffer = new byte[4];
            while (true) {
                // check if we should stop
                if (threadStop) return;
                // generate pval
                rng.NextBytes(buffer);
                uint result = BitConverter.ToUInt32(buffer, 0);

                // check basic stats
                bool genderOK = !req.CheckGender || (result & 0xFF) < Species.GenderRatio == req.Gender;
                bool abilityOK = !req.CheckAbility || ((result & 0x1) > 0) == req.Ability;
                bool natureOK = !req.CheckNature || (int)(result % 25) == req.Nature;

                // calculate shiny state
                bool shinyOK;
                if (req.CheckShiny) {
                    ushort p1 = (ushort)((result & 0xFFFF0000) >> 16);
                    ushort p2 = (ushort)(result & 0xFFFF);
                    shinyOK = ((otid ^ scid ^ p1 ^ p2) < 8) == req.Shiny;
                } else {
                    shinyOK = true;
                }

                // check if this pval matches requirements
                if (genderOK && abilityOK && natureOK && shinyOK) {
                    // we're done!
                    lock (threadLock) {
                        if (threadStop) return;
                        threadStop = true;
                        PVal = result;
                        BeginInvoke(new Action(OnGeneratorDone));
                    }
                }
            }
        }

        private void cmdAccept_Click(object sender, EventArgs e) {
            // lock UI
            cmdAccept.Enabled = false;
            fraGender.Enabled = false;
            prgLoading.Visible = true;

            // assemble requirements into struct
            PvalRequires req = new PvalRequires();
            req.CheckShiny = !optShinyDc.Checked;
            req.CheckGender = !optGenderDc.Checked;
            req.CheckNature = !optNatureDc.Checked;
            req.CheckAbility = !optAbilityDc.Checked;
            req.Ability = optAbility2.Checked;
            req.Shiny = optShinyYes.Checked;
            req.Nature = cmbNature.SelectedIndex;
            req.Gender = optFemale.Checked;

            // generate pval by brute force: just spam random pvals until we
            // find one that matches the requirements

            // start threads to occupy all cpu cores
            threadStop = false;
            threads = new Thread[Environment.ProcessorCount];
            for (int i = 0; i < 1; i++) {
                req.ThreadNo = i; // to offset the RNG on each thread

                threads[i] = new Thread(GeneratorWorker);
                threads[i].IsBackground = true;
                threads[i].Start(req);
            }
        }

        private void cmbNature_SelectedIndexChanged(object sender, EventArgs e) {
            if (init) return;
            optNatureReq.Checked = true;
        }

    }

}
