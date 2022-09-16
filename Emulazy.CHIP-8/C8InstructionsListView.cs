using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emulazy.C8
{
    public partial class C8InstructionsListView : UserControl
    {
        public C8InstructionsListView()
        {
            InitializeComponent();
            InnerFont = new Font("Consolas", 10);
        }

        public List<C8OpCodeData> Items = new List<C8OpCodeData>();

        Font InnerFont;
        const int ItemHeight = 15;
        const int ScrollbarWidth = 5;

        int HexColumnWidth { get => Math.Max(55, Width / 3); }
        int ItemsOnScreen
        {
            get
            {
                return Height / ItemHeight-1;
            }
        }
        float ScrollbarHeight
        {
            get
            {                
                if (Items.Count <= ItemsOnScreen) return 0;
                return 100f / (Items.Count - ItemsOnScreen);
            }
        }

        int firstitem = 0;
        int FirstDiaplyedItem
        {
            get
            {
                if (firstitem < 0)
                {
                    firstitem = 0;
                    return firstitem;
                }
                if (firstitem < Items.Count) return firstitem;
                firstitem = 0;
                return firstitem;
            }
            set
            {
                if (value >= Items.Count) firstitem = Items.Count - 1;
                else firstitem = value;
            }
        }
        int current = -1;
        public int CurrentItem
        {
            get => current;
            set
            {
                current = value;
                FirstDiaplyedItem = Math.Max(0, current - 2);
                Invalidate();
            }
        }
        public void DrawItem(Graphics g, Point pos, C8OpCodeData item, InstructionsListViewItemPaintStyle Style)
        {
            int x = pos.X, y = pos.Y;
            g.Clip = new Region(new Rectangle(x, y, Width - x, ItemHeight));
            g.FillRegion(Style.Background, g.Clip);
            g.Clip = new Region(new Rectangle(x, y, ItemHeight,ItemHeight));            
            x += ItemHeight;
            g.Clip = new Region(new Rectangle(x, y, HexColumnWidth,ItemHeight));            
            g.DrawString(item.ToHex, InnerFont, Style.Foreground0, x, y);
            x += HexColumnWidth;
            g.Clip = new Region(new Rectangle(x, y, Width - x, ItemHeight));
            g.DrawString(item.Description, InnerFont, Style.Foreground1, x, y);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap bmp = new Bitmap(Width, Height);
            var gr = Graphics.FromImage(bmp);
            gr.Clear(Color.Black);

            float sbh = Math.Max(ScrollbarHeight, 10);
            float sbpos = FirstDiaplyedItem * ScrollbarHeight * Height * 0.01f;
            if (Items.Count * ItemHeight < Height)
            {
                FirstDiaplyedItem = 0;
                sbh = 0;
            }
            int y = 0;            
            for (int i = FirstDiaplyedItem, sz = Items.Count; i < sz && y < Height; i++, y += ItemHeight) 
            {
                if (Items.Count > 0)
                    DrawItem(gr, new Point(0, y), Items[i],
                        (i == CurrentItem) ? InstructionsListViewItemPaintStyle.Current : InstructionsListViewItemPaintStyle.Default);
            }
            gr.Clip = new Region(new Rectangle(Width - ScrollbarWidth, 0, ScrollbarWidth, Height));
            gr.FillRegion(new SolidBrush(Color.FromArgb(128, Color.White)), gr.Clip);         
            
            if (sbpos + sbh >= Height) sbpos = Height - sbh;
            gr.FillRectangle(Brushes.White, new RectangleF(Width - ScrollbarWidth, sbpos, ScrollbarWidth, sbh));
            e.Graphics.DrawImageUnscaled(bmp, Point.Empty);
            base.OnPaint(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);            
            FirstDiaplyedItem -= Math.Sign(e.Delta);
            FirstDiaplyedItem = Math.Min(FirstDiaplyedItem,Items.Count-ItemsOnScreen);                       
            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            Invalidate();
            base.OnResize(e);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;    // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
    }

    public class InstructionsListViewItemPaintStyle
    {
        public Brush Background = Brushes.Transparent;
        public Brush Foreground0 = Brushes.White;
        public Brush Foreground1 = Brushes.Yellow;
        public InstructionsListViewItemPaintStyle(Brush b,Brush f0, Brush f1)
        {
            Background = b;
            Foreground0 = f0;
            Foreground1 = f1;
        }

        public static InstructionsListViewItemPaintStyle Default = new InstructionsListViewItemPaintStyle(Brushes.Transparent, Brushes.White, Brushes.Yellow);
        public static InstructionsListViewItemPaintStyle Current = new InstructionsListViewItemPaintStyle(Brushes.White, Brushes.Black, Brushes.Magenta);
    }
}
