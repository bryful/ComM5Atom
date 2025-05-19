using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComM5Atom
{
	public class M3StackColorBar : Control
	{
		private bool refFlag=false;
		private NumericUpDown numR = new NumericUpDown();
		private NumericUpDown numG = new NumericUpDown();
		private NumericUpDown numB = new NumericUpDown();
		private int _colWidth = 60;

		private int red255 = 0;
		private int green255 = 0;
		private int blue255 = 0;

		public int ColorValue
		{
			get
			{
				return M3Color();
			}
			set
			{
				SetM3Color(value);
			}
		}
		public M3StackColorBar()
		{
			this.Size = new Size(130, 60);
			this.MinimumSize = new Size(130, 60);
			this.MaximumSize = new Size(130, 60);

			numR.Location = new Point(_colWidth, 0);
			numR.Size = new Size(70, 19);
			numG.Location = new Point(_colWidth, 20);
			numG.Size = new Size(70, 19);
			numB.Location = new Point(_colWidth, 40);
			numB.Size = new Size(70, 19);

			numR.Minimum = 0;
			numR.Maximum = 31;
			numG.Minimum = 0;
			numG.Maximum = 63;
			numB.Minimum = 0;
			numB.Maximum = 31;
			this.Controls.Add(numR);
			this.Controls.Add(numG);
			this.Controls.Add(numB);
			numR.ValueChanged += (sender,e) =>
			{
				if (refFlag == true) return;
				this.Invalidate();
			};
			numG.ValueChanged += (sender, e) =>
			{
				if (refFlag == true) return;
				this.Invalidate();
			};
			numB.ValueChanged += (sender, e) =>
			{
				if (refFlag == true) return;
				this.Invalidate();
			};
		}
		private void Calc()
		{
			red255 = (int)numR.Value * 255 / 31;
			green255 = (int)numG.Value * 255 / 63;
			blue255 = (int)numB.Value * 255 / 31;
		}
		private int M3Color()
		{
			int r = (int)numR.Value;
			int g = (int)numG.Value;
			int b = (int)numB.Value;
			return (r << 11) | (g << 5) | b;
		}
		private void SetM3Color(int v)
		{
			int r = (v >> 11) & 0x1F;
			int g = (v >> 5) & 0x3F;
			int b = v & 0x1F;
			refFlag = true;
			numR.Value = (decimal)r;
			numG.Value = (decimal)g;
			numB.Value = (decimal)b;
			refFlag = false;
			this.Invalidate();
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Calc();
			Graphics g = e.Graphics;
			Rectangle rect = new Rectangle(0, 0, _colWidth, this.Height);
			using (Brush b = new SolidBrush(Color.FromArgb(red255, green255, blue255)))
			{
				g.FillRectangle(b, rect);
			}
		}
		private void ShowColorDialog()
		{

			using (ColorDialog colorDialog = new ColorDialog())
			{
				colorDialog.AllowFullOpen = true;
				colorDialog.AnyColor = true;
				colorDialog.SolidColorOnly = false;
				colorDialog.FullOpen = true;
				colorDialog.ShowHelp = false;
				Calc();
				colorDialog.Color = Color.FromArgb(red255, green255, blue255);
				if (colorDialog.ShowDialog() == DialogResult.OK)
				{
					refFlag = true;
					numR.Value = (decimal)(colorDialog.Color.R * 31 / 255);
					numG.Value = (decimal)(colorDialog.Color.G * 63 / 255);
					numB.Value = (decimal)(colorDialog.Color.B * 31 / 255);
					Calc();
					refFlag = false;
					this.Invalidate();
				}

			}

		}
		protected override void OnMouseClick(MouseEventArgs e)
		{
			base.OnMouseClick(e);
			if (e.Button == MouseButtons.Left)
			{
				if(e.X < _colWidth)
				{
					ShowColorDialog();
				}
			}
		}
	}
}
