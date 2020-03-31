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
        Semafor hlavni_silnice;
        Semafor hlavni_prechod;
        Semafor hlavni_vlevo;

        Semafor vedlejsi_silnice;
        Semafor vedlejsi_vpravo;
        Semafor vedlejsi_prechod;
        
        Label[] red_hlavni_silnice;
        Label[] yellow_hlavni_silnice;
        Label[] green_hlavni_silnice;

        Label[] red_hlavni_prechod;
        Label[] green_hlavni_prechod;

        Label[] red_hlavni_vlevo;
        Label[] yellow_hlavni_vlevo;
        Label[] green_hlavni_vlevo;

        Label[] red_vedlejsi_silnice;
        Label[] yellow_vedlejsi_silnice;
        Label[] green_vedlejsi_silnice;

        Label[] red_vedlejsi_prechod;
        Label[] green_vedlejsi_prechod;

        Label[] green_vedlejsi_vpravo;

        public Form1()
        {
            // Inicializace formuláře 
            InitializeComponent();
        
            // Vytvoření semaforů              
            hlavni_silnice = new Semafor(1, 2, 3, 4, new byte[] { 1, 3, 0, 0, 0, 2 });
            hlavni_vlevo = new Semafor(1, 2, 3, 4, new byte[] { 0, 0, 0, 2, 1, 3 });
            hlavni_prechod = new Semafor(1, 2, 3, 4, new byte[] { 0, 0, 1, 0, 0, 0 });
            vedlejsi_prechod = new Semafor(1, 2, 3, 4, new byte[] { 1, 0, 0, 0, 0, 0 });
            vedlejsi_silnice = new Semafor(1, 2, 3, 4, new byte[] { 0, 2, 1, 3, 0, 0 });
            vedlejsi_vpravo = new Semafor(1, 2, 3, 4, new byte[] { 0, 0, 0, 0, 1, 0 });

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
            timer1.Tick += new System.EventHandler(Redraw);

            // Start timer
            timer1.Enabled = true;
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
