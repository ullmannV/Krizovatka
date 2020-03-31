using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krizovatka
{
    class Light
    {
        public byte BitNumber { get; private set; }
        public Light(byte bit_number)
        {
            BitNumber = bit_number;
        }
        public void SetBit()
        {
            K8055N.SetDigitalChannel(BitNumber);
        }

        public void ClearBit()
        {
            K8055N.ClearDigitalChannel(BitNumber);
        }
    }
}
