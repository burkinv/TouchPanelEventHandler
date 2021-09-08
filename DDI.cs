using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace TouchPanelInstrument
{
    class DDI
    {
        private const float scaleFactor = 0.9f;
        public String id { get; set; }
        private float x { get; set; }
        private float y { get; set; }
        private float width { get; set; }
        private float height { get; set; }

        private float interiorWidth;
        private float interiorHeight;
        private float interiorMarginX;
        private float interiorMarginY;
        private float touchMenuWidth;
        private float touchMenuHeight;

        private SortedDictionary<int, InstrumentTouchZone> touchZone = new SortedDictionary<int, InstrumentTouchZone>();

        public struct InstrumentTouchZone
        {
            public InstrumentTouchZone(int id, float x, float y, float width, float height)
            {
                this.id = id;
                this.x = x;
                this.y = y;
                this.width = width;
                this.height = height;
            }

            public int id { get; }
            public float x { get; }
            public float y { get; }
            public float width { get; }
            public float height { get; }


            public override string ToString() => $"({x}, {y}, {width}, {height})";
        }

        public DDI(String id, float x, float y, float width, float height)
        {
            this.id = id;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;

            interiorWidth = scaleFactor * width;
            interiorHeight = scaleFactor * height;
            interiorMarginX = (width - interiorWidth) / 2;
            interiorMarginY = (height - interiorHeight) / 2;
            touchMenuWidth = interiorWidth / 5;
            touchMenuHeight = interiorMarginX;

            for (int i = 0; i < 20; ++i)
            {
                touchZone.Add(i, new InstrumentTouchZone(i, 0, 0, 0, 0));
            }
        }
        
        public void paint(Graphics graphics)
        {
            Pen p = new Pen(Color.Green);

            SolidBrush sb1 = new SolidBrush(Color.Green);

            // Draw instrument boundary
            graphics.DrawRectangle(p, x, y, width, height);

            // Draw instrument interior boundary
            graphics.DrawRectangle(p, x + interiorMarginX, y + interiorMarginY, interiorWidth, interiorHeight);


            graphics.FillRectangle(sb1, 1280, 100, 100, 100);


        }

    }
}