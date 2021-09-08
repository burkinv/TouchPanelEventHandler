using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace TouchPanelInstrument
{
    class MCD : TouchPanelInstrument.DDI
    {
        private const float scaleFactor = 0.9f;
        private float interiorWidth;
        private float interiorHeight;
        private float interiorMarginX;
        private float interiorMarginY;
        private float touchMenuWidth;
        private float touchMenuHeight;

        public MCD(String id, float x, float y, float width, float height) : base(id, x, y, width, height)
        {
            interiorWidth = scaleFactor * width;
            interiorHeight = scaleFactor * height;
            interiorMarginX = (width - interiorWidth) / 2;
            interiorMarginY = (height - interiorHeight) / 2;
            touchMenuWidth = interiorWidth / 5;
            touchMenuHeight = interiorMarginX;

            // Left column
            for (int i = 0; i < 5; ++i)
            {
                touchZones.Add(i, new InstrumentTouchZone(i, i.ToString(), x, y + interiorMarginY + (4 - i)*touchMenuWidth, touchMenuHeight, touchMenuWidth));
            }
            // Top row
            for (int i = 5; i < 10; ++i)
            {
                touchZones.Add(i, new InstrumentTouchZone(i, i.ToString(), x + interiorMarginX + (i - 5)*touchMenuWidth, y, touchMenuWidth, touchMenuHeight));
            }
            // Right column
            for (int i = 10; i < 15; ++i)
            {
                touchZones.Add(i, new InstrumentTouchZone(i, i.ToString(), x + interiorMarginX + interiorWidth, y + interiorMarginY + (i - 10)*touchMenuWidth, touchMenuHeight, touchMenuWidth));
            }
            // Bottom row
            for (int i = 15; i < 20; ++i)
            {
                touchZones.Add(i, new InstrumentTouchZone(i, i.ToString(), x + interiorMarginX + (4 - (i - 15))*touchMenuWidth, y + interiorMarginY + interiorHeight, touchMenuWidth, touchMenuHeight));
            }
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
            SortedDictionary<int, InstrumentTouchZone>.Enumerator touchZoneEnumerator = touchZones.GetEnumerator();
            while(touchZoneEnumerator.MoveNext())
            {
                InstrumentTouchZone touchZone = touchZoneEnumerator.Current.Value;
                graphics.DrawRectangle(p, touchZone.x, touchZone.y, touchZone.width, touchZone.height);
            }
        }

    }
}