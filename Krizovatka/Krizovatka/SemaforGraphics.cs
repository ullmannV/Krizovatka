using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Krizovatka
{
    class SemaforGraphics : Semafor 
    {
        public SemaforGraphics(byte[] sequence)
        {
            clock_count = 0;
            sequence_index = 0;     

            this.sequence = sequence;
            State = this.sequence[0];
        }
        protected override void ChangeLight(byte state)
        {
            MessageBox.Show("Hello");            
        }
    }
}
