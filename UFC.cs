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

        private Font font = new Font(FontFamily.GenericSansSerif, 18, FontStyle.Bold);


        public UFC(String id, float x, float y, float width, float height) : base(id, x, y, width, height)
        {
            keyboardWidth  = scaleFactorX * width;
            keyboardHeight = scaleFactorY * height;
            interiorMarginX = 0.02f * width;
            interiorMarginY = 0.02f * height;
            touchKeyWidth = (keyboardWidth - 2*interiorMarginX) / 3;
            touchKeyHeight = (keyboardHeight - 5*interiorMarginY) / 5;

            // Define touch key zones
            int keyIndex = 1;
            for (int i = 1; i <= 4; ++i)
            {
                for (int k = 1; k <= 3; ++k)
                {
                    String label;
                    switch (keyIndex)
                    {
                        case 2:
                            label = "N\n2";
                            break;
                        case 4:
                            label = "W\n4";
                            break;
                        case 6:
                            label = "E\n6";
                            break;
                        case 8:
                            label = "W\n8";
                            break;
                        case 10:
                            label = "CLR";
                            break;
                        case 11:
                            label = "-\n0";
                            break;
                        case 12:
                            label = "ENT";
                            break;
                        default:
                            label = keyIndex.ToString();
                            break;
                    }

                    touchZones.Add(keyIndex, new InstrumentTouchZone(keyIndex, label, new RectangleF(x + k*interiorMarginX + (k-1)*touchKeyWidth, 
                                                                                      y + (i+1)*interiorMarginY + i*touchKeyHeight,
                                                                               touchKeyWidth, touchKeyHeight)));
                    ++keyIndex;
                }
            }

            touchZones.Add(keyIndex, new InstrumentTouchZone(keyIndex, "A/P", new RectangleF(x + interiorMarginX,
                                                                              6 * interiorMarginY + 5 * touchKeyHeight,
                                                                              touchKeyWidth, touchKeyHeight)));
            ++keyIndex;
            touchZones.Add(keyIndex, new InstrumentTouchZone(keyIndex, "IFF", new RectangleF(x + 2 * interiorMarginX + touchKeyWidth,
                                                                              6 * interiorMarginY + 5 * touchKeyHeight,
                                                                              touchKeyWidth, touchKeyHeight)));
            ++keyIndex;
            touchZones.Add(keyIndex, new InstrumentTouchZone(keyIndex, "TCN", new RectangleF(x + 3 * interiorMarginX + 2 * touchKeyWidth,
                                                                              6 * interiorMarginY + 5 * touchKeyHeight,
                                                                              touchKeyWidth, touchKeyHeight)));
            ++keyIndex;
            touchZones.Add(keyIndex, new InstrumentTouchZone(keyIndex, "ILS", new RectangleF(x + 4 * interiorMarginX + 3 * touchKeyWidth,
                                                                              6 * interiorMarginY + 5 * touchKeyHeight,
                                                                              touchKeyWidth, touchKeyHeight)));
            ++keyIndex;
            touchZones.Add(keyIndex, new InstrumentTouchZone(keyIndex, "D/L", new RectangleF(x + 5 * interiorMarginX + 4 * touchKeyWidth,
                                                                              6 * interiorMarginY + 5 * touchKeyHeight,
                                                                              touchKeyWidth, touchKeyHeight)));
            ++keyIndex;
            touchZones.Add(keyIndex, new InstrumentTouchZone(keyIndex, "BCN", new RectangleF(x + 6 * interiorMarginX + 5 * touchKeyWidth,
                                                                              6 * interiorMarginY + 5 * touchKeyHeight,
                                                                              touchKeyWidth, touchKeyHeight)));
            ++keyIndex;
            touchZones.Add(keyIndex, new InstrumentTouchZone(keyIndex, "ON\nOFF", new RectangleF(x + 7 * interiorMarginX + 6 * touchKeyWidth,
                                                                              6 * interiorMarginY + 5 * touchKeyHeight,
                                                                              touchKeyWidth, touchKeyHeight)));
            ++keyIndex;

        }

        public override void paintEvent(Graphics graphics)
        {
            Pen p = new Pen(Color.Green);

            SolidBrush sbGreen = new SolidBrush(Color.Green);
            SolidBrush sbYellow = new SolidBrush(Color.Yellow);

            // Draw instrument boundary
            graphics.DrawRectangle(p, x, y, width, height);

            // Draw input value indicator
            graphics.DrawRectangle(p, x + interiorMarginX, y + interiorMarginY, keyboardWidth, touchKeyHeight);

            // Draw touch zoones
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            foreach (KeyValuePair<int, InstrumentTouchZone> touchZonePair in this.touchZones)
            {
                InstrumentTouchZone touchZone = touchZonePair.Value;
                // Paint backgound
                if (touchZone.touched)
                {
                    graphics.DrawRectangle(p, touchZone.rect.X, touchZone.rect.Y, touchZone.rect.Width, touchZone.rect.Height);
                    //graphics.FillRectangle(sbYellow, touchZone.x, touchZone.y, touchZone.width, touchZone.height);
                }
                else
                    graphics.FillRectangle(sbGreen, touchZone.rect.X, touchZone.rect.Y, touchZone.rect.Width, touchZone.rect.Height);

                // Paint label

                // Draw the text and the surrounding rectangle.
                //RectangleF touchZoneRect = new RectangleF(touchZone.x, touchZone.y, touchZone.width, touchZone.height);
                graphics.DrawString(touchZone.label, this.font, Brushes.White, touchZone.rect, stringFormat);
            }
        }
    }
}