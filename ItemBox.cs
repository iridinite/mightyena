/*
 * Mightyena - A Gen-III Pokémon Save Editor
 * (C) Mika Molenkamp, 2017.
*/

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Mightyena {

    /// <summary>
    /// Represents a delegate called when an <seealso cref="ItemBox"/> is clicked.
    /// </summary>
    /// <param name="boxNo">The number of the box.</param>
    public delegate void ItemBoxClickHandler(int boxNo);

    /// <summary>
    /// Displays the contents of a <seealso cref="Gen3Item"/>.
    /// </summary>
    public partial class ItemBox : UserControl {

        public static int Selection { get; set; } = -1;

        public Gen3Item Item { get; set; }
        public int BoxNo { get; set; }

        private bool hovering = false;
        private readonly Pen borderPen = new Pen(Color.DarkSlateGray);
        private readonly Font quantityFont = new Font(FontFamily.GenericSansSerif, 8f, FontStyle.Bold);
        private readonly Brush quantityBrush = new SolidBrush(Color.White);
        private readonly Brush quantityBrush2 = new SolidBrush(Color.Black);
        private readonly Brush bgNormal
            = new LinearGradientBrush(new PointF(0f, 0f), new PointF(0f, 30f), Color.Gray, Color.DarkGray);
        private readonly Brush bgHover
            = new LinearGradientBrush(new PointF(0f, 0f), new PointF(0f, 30f), Color.White, Color.DarkGray);
        private readonly Brush bgSelect
            = new LinearGradientBrush(new PointF(0f, 0f), new PointF(0f, 30f), Color.White, Color.LightSkyBlue);

        public event ItemBoxClickHandler ItemClick;

        public ItemBox() {
            InitializeComponent();
        }

        private void ItemBox_Paint(object sender, PaintEventArgs e) {
            // pick which brush to use for the background
            Graphics gfx = e.Graphics;
            Brush brush;
            if (Selection == BoxNo)
                brush = bgSelect;
            else if (hovering)
                brush = bgHover;
            else
                brush = bgNormal;
            // draw border and background
            gfx.FillRectangle(brush, new RectangleF(0f, 0f, 29f, 29f));
            gfx.DrawRectangle(borderPen, 0f, 0f, 29f, 29f);
            // draw item details
            if (Item == null) return;
            Utils.DrawItemIcon(gfx, Item.Index, 0, 0);

            if (Item.Quantity > 1) {
                string quantityText = Item.Quantity.ToString();
                SizeF quantitySize = gfx.MeasureString(quantityText, quantityFont);

                gfx.DrawString(quantityText, quantityFont, quantityBrush2,
                    new PointF(30f - quantitySize.Width, 30f - quantitySize.Height));
                gfx.DrawString(quantityText, quantityFont, quantityBrush,
                    new PointF(29f - quantitySize.Width, 29f - quantitySize.Height));
            }
        }

        private void ItemBox_MouseEnter(object sender, System.EventArgs e) {
            hovering = true;
            Invalidate();
        }

        private void ItemBox_MouseLeave(object sender, System.EventArgs e) {
            hovering = false;
            Invalidate();
        }

        private void ItemBox_Click(object sender, System.EventArgs e) {
            ItemClick?.Invoke(BoxNo);
            Invalidate();
        }

    }

}
