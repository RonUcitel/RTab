using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace RTab
{
    public class Tab: Button
    {
        public Panel Body { get; set; }

        private int index = 0;
        public int Index
        {
            get { return index; }
        }
        public string name { get; set; }
        public Size size { get; set; }

        public Tab(string name, Size controllerSize, Size buttonsize, int index):base()
        {
            this.index = index;
            //body
            this.size = controllerSize;
            Body = new Panel();
            Body.Height = controllerSize.Height - buttonsize.Height;
            Body.Width = controllerSize.Width;
            Body.Name = "Body";
            Body.Location = new Point(0, buttonsize.Height);
            //Head
            Location = new Point(index * buttonsize.Width, 0);
            Name = "Head";
            Size = buttonsize;
            Text = name;
            UseVisualStyleBackColor = true;
            FlatStyle = FlatStyle.Flat;

            Label label1 = new Label();
            label1.AutoSize = true;
            label1.Location = new Point(50, 50);
            label1.Name = "label1";
            label1.Size = new Size(35, 13);
            label1.Text = Convert.ToString(index);
            Body.Controls.Add(label1);
        }
    }
}
