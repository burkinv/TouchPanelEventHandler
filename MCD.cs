using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace TouchPanelInstrument
{
    class MCD : TouchPanelInstrument.DDI
    {
        private const float scaleFactor = 0.8f;
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
                touchZones.Add(i, new InstrumentTouchZone(i, i.ToString(), new RectangleF(x, y + interiorMarginY + (4 - i)*touchMenuWidth, touchMenuHeight, touchMenuWidth)));
            }
            // Top row
            for (int i = 5; i < 10; ++i)
            {
                touchZones.Add(i, new InstrumentTouchZone(i, i.ToString(), new RectangleF(x + interiorMarginX + (i - 5)*touchMenuWidth, y, touchMenuWidth, touchMenuHeight)));
            }
            // Right column
            for (int i = 10; i < 15; ++i)
            {
                touchZones.Add(i, new InstrumentTouchZone(i, i.ToString(), new RectangleF(x + interiorMarginX + interiorWidth, y + interiorMarginY + (i - 10)*touchMenuWidth, touchMenuHeight, touchMenuWidth)));
            }
            // Bottom row
            for (int i = 15; i < 20; ++i)
            {
                touchZones.Add(i, new InstrumentTouchZone(i, i.ToString(), new RectangleF(x + interiorMarginX + (4 - (i - 15))*touchMenuWidth, y + interiorMarginY + interiorHeight, touchMenuWidth, touchMenuHeight)));
            }
        }

        public override void paintEvent(Graphics graphics)
        {
            Pen p = new Pen(Color.Green);
            SolidBrush sbGreen = new SolidBrush(Color.Green);

            // Draw instrument boundary
            graphics.DrawRectangle(p, x, y, width, height);

            // Draw instrument interior boundary
            //graphics.DrawRectangle(p, x + interiorMarginX, y + interiorMarginY, interiorWidth, interiorHeight);

            // Draw touch zoones
            foreach (KeyValuePair<int, InstrumentTouchZone> touchZonePair in this.touchZones)
            {
                InstrumentTouchZone touchZone = touchZonePair.Value;
                if (touchZone.touched)
                {
                    //graphics.FillRectangle(sbGreen, touchZone.x, touchZone.y, touchZone.width, touchZone.height);
                    graphics.DrawRectangle(p, touchZone.rect.X, touchZone.rect.Y, touchZone.rect.Width, touchZone.rect.Height);
                }
                //else
                //    graphics.DrawRectangle(p, touchZone.x, touchZone.y, touchZone.width, touchZone.height);
            }
        }

        //public override void touchEvent(Point touchPoint, bool boTouch)
        //{
        //    // Test touch zoones
        //    foreach (InstrumentTouchZone touchZone in this.touchZones.Values)
        //    {
        //        if ((touchPoint.X >= touchZone.x) && (touchPoint.X <= (touchZone.x + touchZone.width)) &&
        //            (touchPoint.Y >= touchZone.y) && (touchPoint.Y <= (touchZone.y + touchZone.height)))
        //        {
        //            touchZone.touched = boTouch;
        //            Console.WriteLine("TouchEvent(X={0}, Y={1}), {2}", touchPoint.X, touchPoint.Y, (boTouch) ? "1" : "0");
        //        }
        //    }
        //}
    }
}