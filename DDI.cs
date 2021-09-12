using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace TouchPanelInstrument
{
    class DDI
    {
        protected String id { get; set; }
        protected float x { get; set; }
        protected float y { get; set; }
        protected float width { get; set; }
        protected float height { get; set; }

        protected SortedDictionary<int, InstrumentTouchZone> touchZones = new SortedDictionary<int, InstrumentTouchZone>();

        public class InstrumentTouchZone
        {
            public InstrumentTouchZone(int id, String label, RectangleF rect, bool touched = false)
            {
                this.id      = id;
                this.label   = label;
                this.rect    = rect;
                this.touched = touched;
            }

            public int id { get; }
            public String label { get; }
            public RectangleF rect { get; }
            public bool touched { get; set; }
        }

        public DDI(String id, float x, float y, float width, float height)
        {
            this.id = id;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public virtual void paintEvent(Graphics graphics)
        {
        }

        public virtual void touchEvent(Point touchPoint, bool boTouch)
        {
            // Test touch zoones
            foreach (InstrumentTouchZone touchZone in this.touchZones.Values)
            {
                if ((touchPoint.X >= touchZone.rect.X) && (touchPoint.X <= (touchZone.rect.X + touchZone.rect.Width)) &&
                    (touchPoint.Y >= touchZone.rect.Y) && (touchPoint.Y <= (touchZone.rect.Y + touchZone.rect.Height)))
                {
                    touchZone.touched = boTouch;
                    Console.WriteLine("TouchEvent(X={0}, Y={1}), {2}", touchPoint.X, touchPoint.Y, (boTouch) ? "1" : "0");
                }
            }
        }
    }
}