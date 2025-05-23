using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace c4palette
{
	public class PreviewPanel :Control
	{
		private Bitmap bitmap = new Bitmap(300,300);
		public void Clear()
		{
			if (bitmap != null)
			{
				bitmap.Dispose();
				bitmap = new Bitmap(300, 300);
			}
			this.Size = new Size(300, 300);
			Graphics g = Graphics.FromImage(bitmap);
			g.Clear(Color.Black);

			this.Invalidate();
		}
		public PreviewPanel()
		{
			this.DoubleBuffered = true;
			this.SetStyle(ControlStyles.ResizeRedraw, true);
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, false);
			this.UpdateStyles();
			Clear();
		}
		public bool LoadPngFile(string s)
		{
			if (s == null) return false;
			if (!File.Exists(s)) return false;
			try
			{
				bitmap = new Bitmap(s);
				bitmap.SetResolution(96, 96);

				int w = bitmap.Width;
				int h = bitmap.Height;
				if (w > 300) w = 300;
				if (h > 300) h = 300;
				this.Size = new Size(w, h);

				this.Invalidate();
				return true;
			}
			catch 
			{
				bitmap = new Bitmap(300,300);
				this.Size = new Size(300, 300);
				return false;
			}
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.Clear(Color.Black);
			e.Graphics.DrawImage(bitmap,0,0);
		}
		private Color _color = Color.Empty;
		public Color Color
		{
			get { return _color; }
		}
		public Color GetColor(int x, int y)
		{
			if (bitmap == null) return Color.Empty;
			if (x < 0 || x >= bitmap.Width) return Color.Empty;
			if (y < 0 || y >= bitmap.Height) return Color.Empty;
			Color c = bitmap.GetPixel(x, y);
			return c;
		}
		protected override void OnMouseClick(MouseEventArgs e)
		{
			if (bitmap == null) return;
			if (e.Button != MouseButtons.Left) return;
			if (e.X < 0 || e.X >= bitmap.Width) return;
			if (e.Y < 0 || e.Y >= bitmap.Height) return;
			_color = bitmap.GetPixel(e.X, e.Y);
			base.OnMouseClick(e);
		}
	}
}
