using System;
using System.Windows.Forms;
using System.Drawing;
using gma.System.Windows;
using System.Collections.Generic;


namespace TouchPanelEventHandler
{
    class MainForm : System.Windows.Forms.Form
    {
        private Panel panel1;

        //private const int WM_NCHITTEST = 0x84;

        private SortedDictionary<int, TouchPanelInstrument.DDI> indicator = new SortedDictionary<int, TouchPanelInstrument.DDI>();

        public MainForm()
        {
            InitializeComponent();
        }
    
        // THIS METHOD IS MAINTAINED BY THE FORM DESIGNER
        // DO NOT EDIT IT MANUALLY! YOUR CHANGES ARE LIKELY TO BE LOST
        void InitializeComponent() {
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.ForeColor = System.Drawing.Color.Coral;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1920, 1080);
            this.panel1.TabIndex = 4;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_Clicked);
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1924, 1084);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "Touch Panel Event Handler";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Black;
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MouseDown);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.MouseDown);
            this.ResumeLayout(false);

        }
            
        [STAThread]
        public static void Main(string[] args)
        {
            Application.Run(new MainForm());
        }

        //[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        //protected override void WndProc(ref Message m)
        //{
        //    // Listen for operating system messages.
        //    switch (m.Msg)
        //    {
        //        case 0x84: //WM_NCHITTEST:
        //            base.WndProc(ref m);
        //            //if ((/*m.LParam.ToInt32() >> 16 and m.LParam.ToInt32() & 0xffff fit in your transparen region*/)
        //            //  && m.Result.ToInt32() == 1)
        //            if (m.Result.ToInt32() == 1)
        //            {
        //                m.Result = new IntPtr(2);   // HTCAPTION
        //            }
        //            Console.WriteLine("Message={0}", 0x84);
        //            break;

        //        default:
        //            base.WndProc(ref m);
        //            break;
        //    }
        //}

        void ButtonStartClick(object sender, System.EventArgs e)
        {
            actHook.Start();
        }
        
        void ButtonStopClick(object sender, System.EventArgs e)
        {
            actHook.Stop();
        }
        
        
        UserActivityHook actHook;

        public int WM_NCHITTEST { get; private set; }

        void MainFormLoad(object sender, System.EventArgs e)
        {
            actHook = new UserActivityHook(); // crate an instance with global hooks
            // hang on events
            actHook.OnMouseActivity+=new MouseEventHandler(MouseEvent);
            actHook.KeyDown+=new KeyEventHandler(MyKeyDown);
            actHook.KeyPress+=new KeyPressEventHandler(MyKeyPress);
            actHook.KeyUp+=new KeyEventHandler(MyKeyUp);


            //Create intruments
            indicator.Add(0, new TouchPanelInstrument.MCD("Left DDI", 0, 100, 640, 640));
            indicator.Add(1, new TouchPanelInstrument.MCD("Right DDI", 1280, 100, 640, 640));
            indicator.Add(2, new TouchPanelInstrument.UFC("UFC", 640, 0, 640, 440));
            indicator.Add(3, new TouchPanelInstrument.MCD("MPCD", 640, 440, 640, 640));

        }

        public void MouseEvent(object sender, MouseEventArgs e)
        {
            //labelMousePosition.Text=String.Format("x={0}  y={1} wheel={2}", e.X, e.Y, e.Delta);
            //if (e.Clicks>0) LogWrite("MouseButton 	- " + e.Button.ToString());

            if (e.Button == MouseButtons.Left)
            {
                Console.WriteLine("1:SRC(X={0}, Y={1}), WND(X={2}, Y={3}), PNL(X={4}, Y={5}) RLT(X={6}, Y={7})",
                    e.Location.X, e.Location.Y,
                    this.Left, this.Top,
                    this.panel1.Left, this.panel1.Top,
                    e.Location.X - this.Left - this.panel1.Left, e.Location.Y - this.Top - this.panel1.Top);
                Console.WriteLine("2:SRC(X={0}, Y={1}), WND(X={2}, Y={3}), PNL(X={4}, Y={5}) RLT(X={6}, Y={7})",
                    e.Location.X, e.Location.Y,
                    this.DesktopLocation.X, this.DesktopLocation.Y,
                    this.panel1.Location.X, this.panel1.Location.Y,
                    e.Location.X - this.DesktopLocation.X - this.panel1.Location.X, e.Location.Y - this.DesktopLocation.Y - this.panel1.Location.Y);

                Point p = this.PointToClient(e.Location);
                Console.WriteLine("3:CLIENT_PNT(X={0}, Y={1})", p.X, p.Y);

            }
        }

        public void MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Console.WriteLine("MouseDown on MainForm");
        }

        public void MyKeyDown(object sender, KeyEventArgs e)
        {
            LogWrite("KeyDown 	- " + e.KeyData.ToString());
        }
        
        public void MyKeyPress(object sender, KeyPressEventArgs e)
        {
            LogWrite("KeyPress 	- " + e.KeyChar);
        }
        
        public void MyKeyUp(object sender, KeyEventArgs e)
        {
            LogWrite("KeyUp 		- " + e.KeyData.ToString());
        }
        
        private void LogWrite(string txt)
        {
            //textBox.AppendText(txt + Environment.NewLine);
            //textBox.SelectionStart = textBox.Text.Length;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = panel1.CreateGraphics();

            for (int i = 0; i < indicator.Count; ++i)
            {
                indicator[i].paint(graphics);
            }

            //indicator[2].paint(graphics);

        }

        private void panel1_Clicked(object sender, MouseEventArgs e)
        {
            Console.WriteLine("PanelClick(X={0}, Y={1})", e.X, e.Y);
        }
    }
}
