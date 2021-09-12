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
            public InstrumentTouchZone(int id, String label, float x, float y, float width, float height, bool touched = false)
            {
                this.id      = id;
                this.label   = label;
                this.x       = x;
                this.y       = y;
                this.width   = width;
                this.height  = height;
                this.touched = touched;
            }

            public int id { get; }
            public String label { get; }
            public float x { get; }
            public float y { get; }
            public float width { get; }
            public float height { get; }
            public bool touched { get; set; }
            public override string ToString() => $"({x}, {y}, {width}, {height})";
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
        }


    }
}