using System.Globalization;
using System;
using System.Windows.Forms;
using System.Drawing;

namespace RTab
{
    [ProvideToolboxControl("RTab.RTabController", false)]
    [Docking(DockingBehavior.Ask)]
    public partial class RTabController : UserControl
    {
        private Point mouseDownLocation;
        public Button add;
        public int ButtonHeight { get; set; }
        public Tab[] Collection { get; set; }
        private const int buttonWidth = 75;
        private int index = 0;
        public int Index
        {
            get { return index; }
        }
        public RTabController()
        {
            InitializeComponent();

            this.Dock = DockStyle.Fill;
            ButtonHeight = 25;

            Collection = new Tab[1];
            Collection[0] = new Tab("New tab", Size, new Size(Math.Min(buttonWidth, Width), ButtonHeight), 0);
            Controls.Add(Collection[0]);
            Collection[0].Visible = true;
            Collection[0].Show();
            //add button
            add = new Button();
            add.Location = new Point(Math.Min(buttonWidth, Width), 0);
            add.Name = "add";
            add.Size = new Size(ButtonHeight, ButtonHeight);
            add.Text = "+";
            add.UseVisualStyleBackColor = true;
            add.FlatStyle = FlatStyle.Flat;
            add.Click += AddTab;
            Controls.Add(add);
            add.Visible = true;
            add.Show();
        }

        private void AddTab(object sender, EventArgs e)
        {
            Size button = new Size(Math.Min(buttonWidth, Width / (Collection.Length + 1)), ButtonHeight);
            add.Left += button.Width;
            Tab[] p = new Tab[Collection.Length+1];

            for (int i = 0; i < Collection.Length; i++)
            {
                p[i] = Collection[i];
            }
            p[Collection.Length] = new Tab("New Tab " + Collection.Length, Size, button, Collection.Length);
            p[Collection.Length].Click += Head_Click;
            Collection = p;
            Controls.Add(Collection[Collection.Length - 1]);
        }

        private void Head_Click(object sender, EventArgs e)
        {
            Tab s = sender as Tab;
            index = s.Index;
            Reflash(s);
        }

        public void Reflash()
        {
            Size button = new Size(Math.Min(buttonWidth, Width / (Collection.Length)), ButtonHeight);
            Controls.Clear();
            for (int i = 0; i < Collection.Length; i++)
            {
                Collection[i].size = button;
                Collection[i].Location = new Point(i * Math.Min(buttonWidth, Width / (Collection.Length)));
                Controls.Add(Collection[i]);
            }
            add.Left = Collection.Length * Math.Min(buttonWidth, Width / (Collection.Length));
            Controls.Add(add);
        }

        public void Reflash(Tab s)
        {
            Size button = new Size(Math.Min(buttonWidth, Width / Collection.Length), ButtonHeight);
            Controls.Clear();
            for (int i = 0; i < Collection.Length; i++)
            {
                Collection[i].size = button;
                Collection[i].Location = new Point(i * Math.Min(buttonWidth, Width / Collection.Length));
                Controls.Add(Collection[i]);
            }
            add.Left = Collection.Length * Math.Min(buttonWidth, Width / (Collection.Length));
            Controls.Add(add);
            Controls.Add(s.Body);
        }
        private void RTabController_Load(object sender, EventArgs e)
        {
            
        }

        private void Tab_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownLocation = e.Location;
            }
        }

        private void Tab_MouseMove(object sender, MouseEventArgs e)
        {
            Button b = sender as Button;
            if (e.Button == MouseButtons.Left)
            {
                b.Left = e.X + b.Left - mouseDownLocation.X;
                //b.Top = e.Y + b.Top - MouseDownLocation.Y;
            }
        }

        private void RTabController_SizeChanged(object sender, EventArgs e)
        {
            Reflash(Collection[index]);
        }
    }
}
