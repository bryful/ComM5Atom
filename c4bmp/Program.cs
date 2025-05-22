using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace c4bmp
{



	internal class Program
	{
		static void Usage()
		{
			Console.WriteLine("[c4bmp.exe]");
			Console.WriteLine("Usage: c4bmp <input.png> [paletteFile]");
			Console.WriteLine("Convert 24BitPNG to 4BitBMP.");
			Console.WriteLine("paletteFile: 16色のパレットファイル(テキスト)を指定。");
		}
		static void Main(string[] args)
		{
			BitmapConverter aaa = new BitmapConverter();
			if (args.Length < 1)
			{
				Usage();
				return;
			}
			string op = args[0].ToLower();
			if (op=="-palette")
			{
				aaa.SavePalFile("palette.txt");
				return;
			}
			string fileName = "";
			string paletteFile = "";
			if (args.Length >0) fileName = args[0];
			if (args.Length > 1) paletteFile = args[1];

			if (File.Exists(paletteFile))
			{
				aaa.LoadPalFile(paletteFile);
			}
			string d = "";
			try
			{
				d = Path.GetDirectoryName(fileName);
			}
			catch
			{
			}
			if (d=="")
			{
				d = Directory.GetCurrentDirectory();
			}
			string f = Path.GetFileName(fileName);
			if(f.IndexOf("*") >= 0)
			{
				string[] files = Directory.GetFiles(d, f);
				foreach(string s in files)
				{
					aaa.Exec(s);
				}
			}
			else if (Directory.Exists(fileName))
			{
				string[] files = Directory.GetFiles(fileName, "*.png");
				foreach (string s in files)
				{
					aaa.Exec(s);
				}

			}
			else if (File.Exists(fileName))
			{
				aaa.Exec(fileName);
			}
			else
			{
				Usage();
			}
		}
	}
}
