/*
 * Zapojení
 * 
 * Karta 1
 * bit 0 -> Hlavní ulice Červená - rovně
 * bit 1 -> Hlavní ulice Žlutá   - rovně
 * bit 2 -> Hlavní ulice Zelená  - rovně
 * bit 3 -> Hlavní ulice Červená - rovně
 * bit 4 -> Hlavní ulice Žlutá   - rovně
 * bit 5 -> Hlavní ulice Zelená  - rovně
 * bit 6 -> Chodci hlavní Červená
 * bit 7 -> Chodci hlavní Zelená
 * 
 * Karta 2
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Krizovatka
{
    public partial class Form1 : Form
    {
        // 0 => Stát      (Červená)         -> dlouhý interval
        // 1 => Jeď       (Zelená)          -> dlouhý interval
        // 2 => Připravit (Červená + Žlutá) -> krátký interval
        // 3 => Pozor     (Žlutá)           -> krátký interval
        // 4 => Servis    (= OFF)           -> krátký interval
        // Definice sekvence stavů semaforů křižovatky
        private readonly byte[] sekvence_hlavni_silnice =   { 1, 1, 1, 3, 0, 0, 0, 0, 0, 0, 0, 2 };
        private readonly byte[] sekvence_hlavni_prechod =   { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0 };
        private readonly byte[] sekvence_hlavni_vlevo =     { 0, 0, 0, 2, 1, 1, 1, 3, 0, 0, 0, 0 };
        private readonly byte[] sekvence_vedlejsi_silnice = { 0, 0, 0, 0, 0, 0, 0, 2, 1, 1, 1, 3 };
        private readonly byte[] sekvence_vedlejsi_vpravo =  { 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0 };
        private readonly byte[] sekvence_vedlejsi_prechod = { 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        private readonly byte[] sekvence_off =   { 4, 4 };
        private readonly byte[] sekvence_night = { 3, 4 };

        private Semafor hlavni_silnice;
        private Semafor hlavni_prechod;
        private Semafor hlavni_vlevo;

        private Semafor vedlejsi_silnice;
        private Semafor vedlejsi_vpravo;
        private Semafor vedlejsi_prechod;
        
        private Label[] red_hlavni_silnice;
        private Label[] yellow_hlavni_silnice;
        private Label[] green_hlavni_silnice;

        private Label[] red_hlavni_prechod;
        private Label[] green_hlavni_prechod;

        private Label[] red_hlavni_vlevo;
        private Label[] yellow_hlavni_vlevo;
        private Label[] green_hlavni_vlevo;

        private Label[] red_vedlejsi_silnice;
        private Label[] yellow_vedlejsi_silnice;
        private Label[] green_vedlejsi_silnice;

        private Label[] red_vedlejsi_prechod;
        private Label[] green_vedlejsi_prechod;

        private Label[] green_vedlejsi_vpravo;

        // zapojeni tlacitka
        private byte input_bit_night = 0;
        private byte input_bit_day = 1;
        private byte input_card = 0;

        public Form1()
        {
            // Inicializace formuláře 
            InitializeComponent();

            // Vytvoření semaforů     
            /*
             * Zapojení semaforů
             * Card 0
             * bit 1 -> hlavni rovne cervena
             * bit 2 -> hlavni rovne zluta
             * bit 3 -> hlavni rovne zelena
             * bit 4 -> hlavni vlevo cervena
             * bit 5 -> hlavni vlevo zluta
             * bit 6 -> hlavni vlevo zelena
             * bit 7 -> hlavni prechod cervena
             * bit 8 -> hlavni prechod zelena
             * Card 1
             * bit 1 -> vedlejsi prechod cervena
             * bit 2 -> vedlejsi prechod zelena
             * bit 3 -> vedlejsi rovne cervena
             * bit 4 -> vedlejsi rovne zluta
             * bit 5 -> vedlejsi rovne zelena
             * bit 6 -> vedlejsi vpravo zelena
             * bit 7 -> [nezapojeno]
             * bit 8 -> [nezapojeno]
             */
            hlavni_silnice = new Semafor(0, 1, 2, 3, sekvence_hlavni_silnice);
            hlavni_vlevo = new Semafor(0, 4, 5, 6, sekvence_hlavni_vlevo);
            hlavni_prechod = new Semafor(0, 7, 0, 8, sekvence_hlavni_prechod);
            vedlejsi_prechod = new Semafor(1, 1, 0, 2, sekvence_vedlejsi_prechod);
            vedlejsi_silnice = new Semafor(1, 3, 4, 5, sekvence_vedlejsi_silnice);
            vedlejsi_vpravo = new Semafor(1, 0, 0, 6, sekvence_vedlejsi_vpravo);

            // pole grafickych prvků
            red_hlavni_silnice = new Label[] { red_hlavni_silnice1, red_hlavni_silnice2 };
            yellow_hlavni_silnice = new Label[] { yellow_hlavni_silnice1, yellow_hlavni_silnice2 };
            green_hlavni_silnice = new Label[] { green_hlavni_silnice1, green_hlavni_silnice2 };

            red_hlavni_prechod = new Label[] { red_hlavni_prechod1, red_hlavni_prechod2, red_hlavni_prechod3, red_hlavni_prechod4 };
            green_hlavni_prechod = new Label[] { green_hlavni_prechod1, green_hlavni_prechod2, green_hlavni_prechod3, green_hlavni_prechod4 };

            red_hlavni_vlevo = new Label[] { red_hlavni_vlevo1, red_hlavni_vlevo2 };
            yellow_hlavni_vlevo = new Label[] { yellow_hlavni_vlevo1, yellow_hlavni_vlevo2 };
            green_hlavni_vlevo = new Label[] { green_hlavni_vlevo1, green_hlavni_vlevo2 };

            red_vedlejsi_silnice = new Label[] { red_vedlejsi_silnice1, red_vedlejsi_silnice2 };
            yellow_vedlejsi_silnice = new Label[] { yellow_vedlejsi_silnice1, yellow_vedlejsi_silnice2 };
            green_vedlejsi_silnice = new Label[] { green_vedlejsi_silnice1, green_vedlejsi_silnice2 };

            red_vedlejsi_prechod = new Label[] { red_vedlejsi_prechod1, red_vedlejsi_prechod2, red_vedlejsi_prechod3, red_vedlejsi_prechod4 };
            green_vedlejsi_prechod = new Label[] { green_vedlejsi_prechod1, green_vedlejsi_prechod2, green_vedlejsi_prechod3, green_vedlejsi_prechod4 };

            green_vedlejsi_vpravo = new Label[] { green_vedlejsi_vpravo1, green_vedlejsi_vpravo2 };

            // Přiřazení obsluh k událostem
            timer1.Tick += new System.EventHandler(hlavni_silnice.HandleTick);
            timer1.Tick += new System.EventHandler(hlavni_prechod.HandleTick);
            timer1.Tick += new System.EventHandler(hlavni_vlevo.HandleTick);
            timer1.Tick += new System.EventHandler(vedlejsi_silnice.HandleTick);
            timer1.Tick += new System.EventHandler(vedlejsi_vpravo.HandleTick);
            timer1.Tick += new System.EventHandler(vedlejsi_prechod.HandleTick);
            timer1.Tick += new System.EventHandler(ButtonInput);
            timer1.Tick += new System.EventHandler(Redraw);

            check_box_night.CheckedChanged += new System.EventHandler(ChangeDayTime);

            // Start timer
            timer1.Enabled = true;
        }

        private void ChangeDayTime(object sender, EventArgs e)
        {
            if (check_box_night.Checked)
            {
                hlavni_silnice.sequence = sekvence_night;
                hlavni_prechod.sequence = sekvence_off;
                hlavni_vlevo.sequence = sekvence_night;
                vedlejsi_silnice.sequence = sekvence_night;
                vedlejsi_vpravo.sequence = sekvence_off;
                vedlejsi_prechod.sequence = sekvence_off;
            }
            else
            {
                hlavni_silnice.sequence = sekvence_hlavni_silnice;
                hlavni_prechod.sequence = sekvence_hlavni_prechod;
                hlavni_vlevo.sequence = sekvence_hlavni_vlevo;
                vedlejsi_silnice.sequence = sekvence_vedlejsi_silnice;
                vedlejsi_vpravo.sequence = sekvence_vedlejsi_vpravo;
                vedlejsi_prechod.sequence = sekvence_vedlejsi_prechod;
            }
        }
        private void ButtonInput(object sender, EventArgs e)
        {
            K8055N.SetCurrentDevice(input_card);
            bool input_night = K8055N.ReadDigitalChannel(input_bit_night);
            bool input_day = K8055N.ReadDigitalChannel(input_bit_day);

            // tlacitko aktivni v log´. 0
            if (!(input_night) && input_day)
            {
                check_box_night.Checked = true;
            }
            else if (input_night && !(input_day))
            {
                check_box_night.Checked = false;
            }
        }
        private void Redraw(object sender, EventArgs e)
        {
            // hlavni_silnice
            switch (hlavni_silnice.State)
            {
                case 0:
                    foreach (Label lbl in red_hlavni_silnice) lbl.BackColor = Color.Red;
                    foreach (Label lbl in yellow_hlavni_silnice) lbl.BackColor = Color.Black;
                    foreach (Label lbl in green_hlavni_silnice) lbl.BackColor = Color.Black;
                    break;
                case 1:
                    foreach (Label lbl in red_hlavni_silnice) lbl.BackColor = Color.Black;
                    foreach (Label lbl in yellow_hlavni_silnice) lbl.BackColor = Color.Black;
                    foreach (Label lbl in green_hlavni_silnice) lbl.BackColor = Color.Green;
                    break;
                case 2:
                    foreach (Label lbl in red_hlavni_silnice) lbl.BackColor = Color.Red;
                    foreach (Label lbl in yellow_hlavni_silnice) lbl.BackColor = Color.Yellow;
                    foreach (Label lbl in green_hlavni_silnice) lbl.BackColor = Color.Black;
                    break;
                case 3:
                    foreach (Label lbl in red_hlavni_silnice) lbl.BackColor = Color.Black;
                    foreach (Label lbl in yellow_hlavni_silnice) lbl.BackColor = Color.Yellow;
                    foreach (Label lbl in green_hlavni_silnice) lbl.BackColor = Color.Black;
                    break;
                case 4:
                    foreach (Label lbl in red_hlavni_silnice) lbl.BackColor = Color.Black;
                    foreach (Label lbl in yellow_hlavni_silnice) lbl.BackColor = Color.Black;
                    foreach (Label lbl in green_hlavni_silnice) lbl.BackColor = Color.Black;
                    break;
            }
            // hlavni_prechod
            switch (hlavni_prechod.State)
            {
                case 0:
                    foreach (Label lbl in red_hlavni_prechod) lbl.BackColor = Color.Red;                    
                    foreach (Label lbl in green_hlavni_prechod) lbl.BackColor = Color.Black;
                    break;
                case 1:
                    foreach (Label lbl in red_hlavni_prechod) lbl.BackColor = Color.Black;
                    foreach (Label lbl in green_hlavni_prechod) lbl.BackColor = Color.Green;
                    break;
                case 4:
                    foreach (Label lbl in red_hlavni_prechod) lbl.BackColor = Color.Black;
                    foreach (Label lbl in green_hlavni_prechod) lbl.BackColor = Color.Black;
                    break;
            }
            // hlavni_vlevo
            switch (hlavni_vlevo.State)
            {
                case 0:
                    foreach (Label lbl in red_hlavni_vlevo) lbl.BackColor = Color.Red;
                    foreach (Label lbl in yellow_hlavni_vlevo) lbl.BackColor = Color.Black;
                    foreach (Label lbl in green_hlavni_vlevo) lbl.BackColor = Color.Black;
                    break;
                case 1:
                    foreach (Label lbl in red_hlavni_vlevo) lbl.BackColor = Color.Black;
                    foreach (Label lbl in yellow_hlavni_vlevo) lbl.BackColor = Color.Black;
                    foreach (Label lbl in green_hlavni_vlevo) lbl.BackColor = Color.Green;
                    break;
                case 2:
                    foreach (Label lbl in red_hlavni_vlevo) lbl.BackColor = Color.Red;
                    foreach (Label lbl in yellow_hlavni_vlevo) lbl.BackColor = Color.Yellow;
                    foreach (Label lbl in green_hlavni_vlevo) lbl.BackColor = Color.Black;
                    break;
                case 3:
                    foreach (Label lbl in red_hlavni_vlevo) lbl.BackColor = Color.Black;
                    foreach (Label lbl in yellow_hlavni_vlevo) lbl.BackColor = Color.Yellow;
                    foreach (Label lbl in green_hlavni_vlevo) lbl.BackColor = Color.Black;
                    break;
                case 4:
                    foreach (Label lbl in red_hlavni_vlevo) lbl.BackColor = Color.Black;
                    foreach (Label lbl in yellow_hlavni_vlevo) lbl.BackColor = Color.Black;
                    foreach (Label lbl in green_hlavni_vlevo) lbl.BackColor = Color.Black;
                    break;
            }
            // vedlejsi_silnice
            switch (vedlejsi_silnice.State)
            {
                case 0:
                    foreach (Label lbl in red_vedlejsi_silnice) lbl.BackColor = Color.Red;
                    foreach (Label lbl in yellow_vedlejsi_silnice) lbl.BackColor = Color.Black;
                    foreach (Label lbl in green_vedlejsi_silnice) lbl.BackColor = Color.Black;
                    break;
                case 1:
                    foreach (Label lbl in red_vedlejsi_silnice) lbl.BackColor = Color.Black;
                    foreach (Label lbl in yellow_vedlejsi_silnice) lbl.BackColor = Color.Black;
                    foreach (Label lbl in green_vedlejsi_silnice) lbl.BackColor = Color.Green;
                    break;
                case 2:
                    foreach (Label lbl in red_vedlejsi_silnice) lbl.BackColor = Color.Red;
                    foreach (Label lbl in yellow_vedlejsi_silnice) lbl.BackColor = Color.Yellow;
                    foreach (Label lbl in green_vedlejsi_silnice) lbl.BackColor = Color.Black;
                    break;
                case 3:
                    foreach (Label lbl in red_vedlejsi_silnice) lbl.BackColor = Color.Black;
                    foreach (Label lbl in yellow_vedlejsi_silnice) lbl.BackColor = Color.Yellow;
                    foreach (Label lbl in green_vedlejsi_silnice) lbl.BackColor = Color.Black;
                    break;
                case 4:
                    foreach (Label lbl in red_vedlejsi_silnice) lbl.BackColor = Color.Black;
                    foreach (Label lbl in yellow_vedlejsi_silnice) lbl.BackColor = Color.Black;
                    foreach (Label lbl in green_vedlejsi_silnice) lbl.BackColor = Color.Black;
                    break;
            }
            // vedlejsi_vpravo
            switch (vedlejsi_vpravo.State)
            {
                case 4: // stejná reakce jako na stav 0
                case 0:
                    foreach (Label lbl in green_vedlejsi_vpravo) lbl.BackColor = Color.Black;
                    break;
                case 1:
                    foreach (Label lbl in green_vedlejsi_vpravo) lbl.BackColor = Color.Green;
                    break;                               
            }
            // vedlejsi_prechod 
            switch (vedlejsi_prechod.State)
            {
                case 0:
                    foreach (Label lbl in red_vedlejsi_prechod) lbl.BackColor = Color.Red;
                    foreach (Label lbl in green_vedlejsi_prechod) lbl.BackColor = Color.Black;
                    break;
                case 1:
                    foreach (Label lbl in red_vedlejsi_prechod) lbl.BackColor = Color.Black;
                    foreach (Label lbl in green_vedlejsi_prechod) lbl.BackColor = Color.Green;
                    break;
                case 4:
                    foreach (Label lbl in red_vedlejsi_prechod) lbl.BackColor = Color.Black;
                    foreach (Label lbl in green_vedlejsi_prechod) lbl.BackColor = Color.Black;
                    break;
            }
        }
    }
}
