using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krizovatka
{
    class Semafor
    {
        // Vlastnost indikující stav činnosti semaforu 
        // 0 => Stát      (Červená)         -> dlouhý interval
        // 1 => Jeď       (Zelená)          -> dlouhý interval
        // 2 => Připravit (Červená + Žlutá) -> krátký interval
        // 3 => Pozor     (Žlutá)           -> krátký interval
        // 4 => Servis    (= OFF)           -> krátký interval
        public byte State { get; protected set; }

        // Adresa karty na kterou je semafor připojen
        private byte Card { get; set; }
        // Bit dané barvy semaforu
        private Light Red { get; set; }
        private Light Yellow { get; set; }
        private Light Green { get; set; }
        
        protected byte sequence_index;
        
        // Pole obsahující sekvenci
        public byte[] sequence { get; set; }
                    
        protected Semafor()
        {
            // prázdný konstruktor pro případ dědění
        }
        public Semafor(byte card, byte bit_red, byte bit_yellow, byte bit_green, byte[] sequence)
        {
            sequence_index = 0;
            
            // inicializace informaci o hardwaru
            Card = card;
            Red = new Light(bit_red);
            Yellow = new Light(bit_yellow);
            Green = new Light(bit_green);

            this.sequence = sequence;
            State = this.sequence[0];
        }

        public void HandleTick(object sender, EventArgs e)
        {
            // Test zda sekvence skončila
            if (++sequence_index >= sequence.Length)
            {
                sequence_index = 0;
            }
            State = sequence[sequence_index]; // Aktualizuj hodnotu
            ChangeLight(sequence[sequence_index]);      
        }
               

        protected virtual void ChangeLight(byte state)
        {         

            K8055N.SetCurrentDevice(Card); // Spojení se správnou kartou
            
            // 0 => Stát      (Červená)         -> dlouhý interval
            // 1 => Jeď       (Zelená)          -> dlouhý interval
            // 2 => Připravit (Červená + Žlutá) -> krátký interval
            // 3 => Pozor     (Žlutá)           -> krátký interval
            // 4 => Servis    (= OFF)           -> krátký interval
            switch (state)
            {
                case 0:
                    Red?.SetBit();
                    Yellow?.ClearBit();
                    Green?.ClearBit();
                    break;
                case 1:
                    Red?.ClearBit();
                    Yellow?.ClearBit();
                    Green?.SetBit();
                    break;
                case 2:
                    Red?.SetBit();
                    Yellow?.SetBit();
                    Green?.ClearBit();
                    break;
                case 3:
                    Red?.ClearBit();
                    Yellow?.SetBit();
                    Green?.ClearBit();
                    break;
                case 4:
                    Red?.ClearBit();
                    Yellow?.ClearBit();
                    Green?.ClearBit();
                    break;
            }
        }
    }
}
