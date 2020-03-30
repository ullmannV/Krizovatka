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
using System.Text;

namespace TechnologickeDemoKrizovatka
{
    class Semafor
    {
        // Vlastnost indikující stav činnosti semaforu 
        // 0 => Stát      (Červená)
        // 1 => Jeď       (Zelená)
        // 2 => Připravit (Červená + Žlutá)
        // 3 => Pozor     (Žlutá)
        // 4 => Servis    (= OFF) 
        public byte State { get; private set; }

        // Adresa karty na kterou je semafor připojen
        public byte Card { get; private set; }
        // Bit dané barvy semaforu
        public byte Red { get; private set; }
        public byte Yellow { get; private set; }
        public byte Green { get; private set; }
        
        private byte clock_count;
        private byte sequence_index;
        
        // Pole obsahující sekvenci
        private byte[] sequence;

        // Konstanty pro vnitřní použití třídy
        private const byte LONG_INTERVAL = 5;
        private const byte SHORT_INTERVAL = 2;
        
        public Semafor(byte[] sequence)
        {
            clock_count = 0;
            sequence_index = 0;

            this.sequence = sequence;
            State = this.sequence[0];
        }

        public void HandleTick(object sender, EventArgs e)
        {
            clock_count++;

            // Dlouhé intervaly
            if (State < 2)
            {
                IncrementSequenceIndex(LONG_INTERVAL);
            }
            else // Krátké intervaly
            {
                IncrementSequenceIndex(SHORT_INTERVAL);
            }
        }

        private void IncrementSequenceIndex(byte interval)
        {
            if (clock_count == interval)
            {
                clock_count = 0; // reset clock counter

                // Test zda sekvence skončila
                if (++sequence_index == sequence.Length)
                {
                    sequence_index = 0;
                }
                ChangeLight(sequence[sequence_index]);
            }
        }

        private void ChangeLight(byte state)
        {
            this.State = state; // Aktualizuj hodnotu

        }
    }
}
