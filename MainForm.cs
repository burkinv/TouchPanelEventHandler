using System;
using System.Windows.Forms;
using System.Drawing;
using gma.System.Windows;
using System.Collections.Generic;


namespace TouchPanelEventHandler
{
    class MainForm : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Label labelMousePosition;
        private Panel panel1;
        private System.Windows.Forms.TextBox textBox;

        private SortedDictionary<int, TouchPanelInstrument.DDI> indicator = new SortedDictionary<int, TouchPanelInstrument.DDI>();

        public MainForm()
        {
            InitializeComponent();
        }
    
        // THIS METHOD IS MAINTAINED BY THE FORM DESIGNER
        // DO NOT EDIT IT MANUALLY! YOUR CHANGES ARE LIKELY TO BE LOST
        void InitializeComponent() {
            this.textBox = new System.Windows.Forms.TextBox();
            this.labelMousePosition = new System.Windows.Forms.Label();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox.Font = new System.Drawing.Font("Courier New", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.textBox.ForeColor = System.Drawing.Color.White;
            this.textBox.Location = new System.Drawing.Point(4, 55);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox.Size = new System.Drawing.Size(276, 1030);
            this.textBox.TabIndex = 3;
            // 
            // labelMousePosition
            // 
            this.labelMousePosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMousePosition.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelMousePosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelMousePosition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelMousePosition.ForeColor = System.Drawing.Color.White;
            this.labelMousePosition.Location = new System.Drawing.Point(4, 29);
            this.labelMousePosition.Name = "labelMousePosition";
            this.labelMousePosition.Size = new System.Drawing.Size(276, 23);
            this.labelMousePosition.TabIndex = 2;
            this.labelMousePosition.Text = "labelMousePosition";
            this.labelMousePosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonStop
            // 
            this.buttonStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStop.ForeColor = System.Drawing.Color.White;
            this.buttonStop.Location = new System.Drawing.Point(85, 3);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 1;
            this.buttonStop.Text = "Stop";
            this.buttonStop.Click += new System.EventHandler(this.ButtonStopClick);
            // 
            // buttonStart
            // 
            this.buttonStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStart.ForeColor = System.Drawing.Color.White;
            this.buttonStart.Location = new System.Drawing.Point(4, 3);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Start";
            this.buttonStart.Click += new System.EventHandler(this.ButtonStartClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.ForeColor = System.Drawing.Color.Coral;
            this.panel1.Location = new System.Drawing.Point(286, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1920, 1080);
            this.panel1.TabIndex = 4;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(2208, 1087);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.labelMousePosition);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.Name = "MainForm";
            this.Text = "Touch Panel Event Handler";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Black;
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
            
        [STAThread]
        public static void Main(string[] args)
        {
            Application.Run(new MainForm());
        }
        
        void ButtonStartClick(object sender, System.EventArgs e)
        {
            actHook.Start();
        }
        
        void ButtonStopClick(object sender, System.EventArgs e)
        {
            actHook.Stop();
        }
        
        
        UserActivityHook actHook;
        void MainFormLoad(object sender, System.EventArgs e)
        {
            actHook = new UserActivityHook(); // crate an instance with global hooks
            // hang on events
            actHook.OnMouseActivity+=new MouseEventHandler(MouseMoved);
            actHook.KeyDown+=new KeyEventHandler(MyKeyDown);
            actHook.KeyPress+=new KeyPressEventHandler(MyKeyPress);
            actHook.KeyUp+=new KeyEventHandler(MyKeyUp);


            //Create intruments
            indicator.Add(0, new TouchPanelInstrument.MCD("Left DDI", 0, 100, 640, 640));
            indicator.Add(1, new TouchPanelInstrument.MCD("Right DDI", 1280, 100, 640, 640));
            indicator.Add(2, new TouchPanelInstrument.UFC("UFC", 640, 0, 640, 440));
            indicator.Add(3, new TouchPanelInstrument.MCD("MPCD", 640, 440, 640, 640));

        }

        public void MouseMoved(object sender, MouseEventArgs e)
        {
            labelMousePosition.Text=String.Format("x={0}  y={1} wheel={2}", e.X, e.Y, e.Delta);
            if (e.Clicks>0) LogWrite("MouseButton 	- " + e.Button.ToString());
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
            textBox.AppendText(txt + Environment.NewLine);
            textBox.SelectionStart = textBox.Text.Length;
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
    }
}
