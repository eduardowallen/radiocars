using System;
using System.Collections.Generic;
using System.Text;

namespace radiocars
{
    class Grid
    {
        public int width;
        public int height;

        public bool WithinDomain(int x, int y)
        {
            if (x >= 0 && x <= this.width && this.width >= 0)
            {
                if (y >= 0 && y <= this.height && this.height >= 0)
                    return true;
                else
                    return false;
            } 
            return false;
        }
    }
}
