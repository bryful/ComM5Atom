using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace c4bmp
{

	public class BitmapConverter
	{
		private Color[] paletteColors = new Color[16] 
		{
			Color.FromArgb(0xff,0x00,0x00,0xff),
			Color.FromArgb(0xff,0xe1,0xa9,0x90),

			Color.FromArgb(0xff,0x00,0x00,0x00),
			Color.FromArgb(0xff,0x30,0x30,0x30),
			Color.FromArgb(0xff,0x4d,0x4d,0x4d),
			Color.FromArgb(0xff,0x5d,0x03,0x03),
			Color.FromArgb(0xff,0x68,0x0a,0x0a),
			Color.FromArgb(0xff,0xa1,0x29,0x29),
			Color.FromArgb(0xff,0xcd,0x42,0x42),
			Color.FromArgb(0xff,0xe4,0x4e,0x4e),
			Color.FromArgb(0xff,0xeb,0x7c,0x7c),
			Color.FromArgb(0xff,0xef,0x54,0x54),
			Color.FromArgb(0xff,0xc8,0xc8,0xc8),
			Color.FromArgb(0xff,0xf1,0xb2,0xfe),
			Color.FromArgb(0xff,0xfe,0xc4,0xc4),
			Color.FromArgb(0xff,0xFF,0xFF,0xFF),
		};
		public BitmapConverter()
		{
		}
		// 16色のパレットを指定して24bpp -> 4bpp Indexedに変換
		public Bitmap Convert24bppTo4bpp(Bitmap original)
		{
			if (original.PixelFormat != PixelFormat.Format24bppRgb)
				return null;


			int width = original.Width;
			int height = original.Height;

			// 4bpp Indexed ビットマップ作成
			Bitmap newBmp = new Bitmap(width, height, PixelFormat.Format4bppIndexed);

			// パレット設定
			ColorPalette palette = newBmp.Palette;
			for (int i = 0; i < 16; i++)
			{
				palette.Entries[i] = paletteColors[i];
			}
			newBmp.Palette = palette;

			// オリジナルビットマップからピクセル取得
			BitmapData srcData = original.LockBits(
				new Rectangle(0, 0, width, height),
				ImageLockMode.ReadOnly,
				PixelFormat.Format24bppRgb);

			BitmapData dstData = newBmp.LockBits(
				new Rectangle(0, 0, width, height),
				ImageLockMode.WriteOnly,
				PixelFormat.Format4bppIndexed);

			int srcStride = srcData.Stride;
			int dstStride = dstData.Stride;

			byte[] srcBuffer = new byte[srcStride * height];
			byte[] dstBuffer = new byte[dstStride * height];

			Marshal.Copy(srcData.Scan0, srcBuffer, 0, srcBuffer.Length);

			for (int y = 0; y < height; y++)
			{
				int srcRow = y * srcStride;
				int dstRow = y * dstStride;

				for (int x = 0; x < width; x += 2)
				{
					// ピクセル1
					int i1 = (x + 0) * 3;
					Color c1 = Color.FromArgb(
						srcBuffer[srcRow + i1 + 2],
						srcBuffer[srcRow + i1 + 1],
						srcBuffer[srcRow + i1 + 0]);

					byte index1 = FindClosestColorIndex(c1);

					// ピクセル2（画面右端ならブラック）
					byte index2 = 0;
					if (x + 1 < width)
					{
						int i2 = (x + 1) * 3;
						Color c2 = Color.FromArgb(
							srcBuffer[srcRow + i2 + 2],
							srcBuffer[srcRow + i2 + 1],
							srcBuffer[srcRow + i2 + 0]);

						index2 = FindClosestColorIndex(c2);
					}

					dstBuffer[dstRow + x / 2] = (byte)((index1 << 4) | index2);
				}
			}

			Marshal.Copy(dstBuffer, 0, dstData.Scan0, dstBuffer.Length);
			original.UnlockBits(srcData);
			newBmp.UnlockBits(dstData);

			return newBmp;
		}


		private byte FindClosestColorIndex(Color c)
		{
			int minDistance = int.MaxValue;
			byte closestIndex = 0; // ← 2からスタート

			for (byte i = 0; i < paletteColors.Length; i++) // ← インデックス2〜15のみ
			{
				if(i == 1) continue; // インデックス1はスキップ
				int dr = c.R - paletteColors[i].R;
				int dg = c.G - paletteColors[i].G;
				int db = c.B - paletteColors[i].B;
				int dist = dr * dr + dg * dg + db * db;

				if (dist < minDistance)
				{
					minDistance = dist;
					closestIndex = i;
				}
			}
			return closestIndex;
		}

		public bool LoadPalFile(string s)
		{
			bool ret = false;

			if ( File.Exists(s) )
			{
				string str = File.ReadAllText(s);
				string[] lines = str.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
				if(lines.Length >= 16)
				{
					Color[] pals = new Color[16];
					for (int i = 0; i < 16; i++)
					{
						string line = lines[i].Trim();	
						int cc = Convert.ToInt32(line, 16);
						pals[i] = Color.FromArgb(0xFF, (cc >> 16) & 0xFF, (cc >> 8) & 0xFF, cc & 0xFF);
					}
					paletteColors = pals;
					ret = true;
				}
				else
				{
					ret = false;
				}
			}
			return ret;
		}
		public bool SavePalFile(string s)
		{
			bool ret = false;
			string str = string.Empty;

			foreach(Color c in paletteColors)
			{
				if(str!="") 				{
					str += ",\r\n";
				}
				str += string.Format("{0:X2}{1:X2}{2:X2}", c.R,c.G,c.B);
			}
			if (File.Exists(s))
			{
				File.Delete(s);
			}
			File.WriteAllText(s, str.TrimEnd(','));

			return ret;
		}
		public bool Exec(string src)
		{
			bool ret = false;
			if (File.Exists(src))
			{
				string dst = Path.ChangeExtension(src, ".bmp");
				Bitmap bmp = new Bitmap(src);
				Bitmap newBmp = Convert24bppTo4bpp(bmp);
				if(newBmp != null)
				{
					newBmp.Save(dst, ImageFormat.Bmp);
					newBmp.Dispose();
				}
				bmp.Dispose();
				ret = true;
			}
			return ret;
		}
	}
}
