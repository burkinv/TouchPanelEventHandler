using System;
using System.Windows.Forms;
using System.Drawing;

namespace TouchPanelInstrument
{
    internal class UFC : TouchPanelInstrument.DDI
    {
        private float interiorWidth;
        private float interiorHeight;
        private float interiorMarginX;
        private float interiorMarginY;
        private float touchMenuWidth;
        private float touchMenuHeight;

        public UFC(String id, float x, float y, float width, float height) : base(id, x, y, width, height)
        {

        }

        public override void paint(Graphics graphics)
        {
            Pen p = new Pen(Color.Green);

            SolidBrush sb1 = new SolidBrush(Color.Green);

            // Draw instrument boundary
            graphics.DrawRectangle(p, x, y, width, height);

            // Draw instrument interior boundary
            graphics.DrawRectangle(p, x + interiorMarginX, y + interiorMarginY, interiorWidth, interiorHeight);

            // Draw touch zoones
            for (int i = 0; i < touchZones.Count; ++i)
            {
                InstrumentTouchZone touchZone = touchZones[i];
                graphics.DrawRectangle(p, touchZone.x, touchZone.y, touchZone.width, touchZone.height);
            }


            graphics.FillRectangle(sb1, 1280, 100, 100, 100);
        }


    }
}