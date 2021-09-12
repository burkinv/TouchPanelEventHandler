using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace TouchPanelInstrument
{
    internal class UFC : TouchPanelInstrument.DDI
    {
        private const float scaleFactorX = 0.40f;
        private const float scaleFactorY = 0.80f;

        private float interiorMarginX;
        private float interiorMarginY;
        private float keyboardWidth;
        private float keyboardHeight;
        private float touchKeyWidth;
        private float touchKeyHeight;

        public UFC(String id, float x, float y, float width, float height) : base(id, x, y, width, height)
        {
            keyboardWidth  = scaleFactorX * width;
            keyboardHeight = scaleFactorY * height;
            interiorMarginX = 0.02f * width;
            interiorMarginY = 0.02f * height;
            touchKeyWidth = (keyboardWidth - 2*interiorMarginX) / 3;
            touchKeyHeight = (keyboardHeight - 5*interiorMarginY) / 5;

            // Define touch key zones
            int keyIndex = 0;
            for (int i = 1; i <= 4; ++i)
            {
                for (int k = 1; k <= 3; ++k)
                {
                    String label;
                    switch (keyIndex)
                    {
                        case 10:
                            label = "CLR";
                            break;

                        case 12:
                            label = "ENT";
                            break;
                        default:
                            label = keyIndex.ToString();
                            break;
                    }

                    touchZones.Add(keyIndex, new InstrumentTouchZone(keyIndex, label, x + k*interiorMarginX + (k-1)*touchKeyWidth, 
                                                                                      y + (i+1)*interiorMarginY + i*touchKeyHeight,
                                                                               touchKeyWidth, touchKeyHeight));
                    ++keyIndex;
                }
            }

            touchZones.Add(keyIndex, new InstrumentTouchZone(keyIndex, "A/P", x + interiorMarginX,
                                                                              6 * interiorMarginY + 5 * touchKeyHeight,
                                                                              touchKeyWidth, touchKeyHeight));
            ++keyIndex;
            touchZones.Add(keyIndex, new InstrumentTouchZone(13, "IFF", x + 2 * interiorMarginX + touchKeyWidth,
                                                                              6 * interiorMarginY + 5 * touchKeyHeight,
                                                                              touchKeyWidth, touchKeyHeight));
            ++keyIndex;
            touchZones.Add(keyIndex, new InstrumentTouchZone(13, "TCN", x + 3 * interiorMarginX + 2 * touchKeyWidth,
                                                                              6 * interiorMarginY + 5 * touchKeyHeight,
                                                                              touchKeyWidth, touchKeyHeight));
            ++keyIndex;
            touchZones.Add(keyIndex, new InstrumentTouchZone(13, "ILS", x + 4 * interiorMarginX + 3 * touchKeyWidth,
                                                                              6 * interiorMarginY + 5 * touchKeyHeight,
                                                                              touchKeyWidth, touchKeyHeight));
            ++keyIndex;
            touchZones.Add(keyIndex, new InstrumentTouchZone(13, "D/L", x + 5 * interiorMarginX + 4 * touchKeyWidth,
                                                                              6 * interiorMarginY + 5 * touchKeyHeight,
                                                                              touchKeyWidth, touchKeyHeight));
            ++keyIndex;
            touchZones.Add(keyIndex, new InstrumentTouchZone(13, "BCN", x + 6 * interiorMarginX + 5 * touchKeyWidth,
                                                                              6 * interiorMarginY + 5 * touchKeyHeight,
                                                                              touchKeyWidth, touchKeyHeight));
            ++keyIndex;
            touchZones.Add(keyIndex, new InstrumentTouchZone(13, "ON/OFF", x + 7 * interiorMarginX + 6 * touchKeyWidth,
                                                                              6 * interiorMarginY + 5 * touchKeyHeight,
                                                                              touchKeyWidth, touchKeyHeight));
            ++keyIndex;

        }

        public override void paintEvent(Graphics graphics)
        {
            Pen p = new Pen(Color.Green);

            SolidBrush sb1 = new SolidBrush(Color.Green);

            // Draw instrument boundary
            graphics.DrawRectangle(p, x, y, width, height);

            // Draw input value indicator
            graphics.DrawRectangle(p, x + interiorMarginX, y + interiorMarginY, keyboardWidth, touchKeyHeight);

            // Draw keyboard
            SortedDictionary<int, InstrumentTouchZone>.Enumerator touchZoneEnumerator = touchZones.GetEnumerator();
            while (touchZoneEnumerator.MoveNext())
            {
                InstrumentTouchZone touchZone = touchZoneEnumerator.Current.Value;
                graphics.FillRectangle(sb1, touchZone.x, touchZone.y, touchZone.width, touchZone.height);
            }
        }

        public override void touchEvent(Point touchPoint, bool boTouch)
        {
        }

    }
}