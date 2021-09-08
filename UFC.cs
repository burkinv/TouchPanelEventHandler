using System;
using System.Windows.Forms;
using System.Drawing;

namespace TouchPanelInstrument
{
    internal class UFC : TouchPanelInstrument.DDI
    {
        public UFC(String id, float x, float y, float width, float height) : base(id, x, y, width, height)
        {
        }

        public static void paintInstrument(Graphics graphics, int x, int y, int width, int height)
        {
            Pen p = new Pen(Color.Green);

            SolidBrush sb1 = new SolidBrush(Color.Green);

            graphics.DrawRectangle(p, x, y, width, height);

        }

    }
}