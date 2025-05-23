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
using System.Security.Cryptography.X509Certificates;
namespace c4palette
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			openMenu.Click += (s, e) => OpenDlg();
			saveMenu.Click += (s, e) => SaveDlg();
			copyMenu.Click += (s, e) => paletteCanvas1.Copy();
			pasteMenu.Click += (s, e) => paletteCanvas1.Paste();
			importPngMenu.Click += (s, e) => ImportPng();
			exportPngMenu.Click += (s, e) => ExportPng();
			btnOpen.Click += (s, e) => OpenPreview();

			previewPanel1.MouseClick += PreviewPanel1_MouseClick;
		}

		private void PreviewPanel1_MouseClick(object sender, MouseEventArgs e)
		{
			if(cbSpoit.Checked==false) return;
			if (paletteCanvas1.SelectedIndex>=0)
			{
				Color c = previewPanel1.Color;
				if (c != Color.Empty)
				{
					paletteCanvas1.SetColor(paletteCanvas1.SelectedIndex, c);
					paletteCanvas1.Invalidate();
				}

			}
		}

		private string _PalFileName = "";
		private string _PngFileName = "";
		private string _PreviewFileName = "";
		private string _BaseDir = "";
		public void OpenDlg()
		{
			using (var dlg = new OpenFileDialog())
			{
				{
					dlg.Filter = "Palette File (*.pal)|*.pal|All files (*.*)|*.*";

					if(_BaseDir != "")
					{
						dlg.InitialDirectory = _BaseDir;
					}
					if (_PalFileName != "")
					{
						dlg.FileName = _PalFileName;
					}
					else
					{
						dlg.FileName = "palette.pal";
					}
					dlg.DefaultExt = ".pal";
					if (dlg.ShowDialog() == DialogResult.OK)
					{
						paletteCanvas1.LoadPalFile(dlg.FileName);
						_PalFileName = Path.GetFileName(dlg.FileName);
						_BaseDir = Path.GetDirectoryName(dlg.FileName);
					}
				}
			}
		}
		public void SaveDlg()
		{
			using (var dlg = new SaveFileDialog())
			{

				dlg.Filter = "Palette File (*.pal)|*.pal|All files (*.*)|*.*";
				if (_BaseDir != "")
				{
					dlg.InitialDirectory = _BaseDir;
				}
				if (_PalFileName != "")
				{
					dlg.FileName = _PalFileName;
				}
				else
				{
					dlg.FileName = "palette.pal";
				}
				dlg.DefaultExt = ".pal";
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					paletteCanvas1.SavePalFile(dlg.FileName);
					_PalFileName = Path.GetFileName(dlg.FileName);
					_BaseDir = Path.GetDirectoryName(dlg.FileName);
				}
			}

		}
		public void ImportPng()
		{
					using (var dlg = new OpenFileDialog())
			{
				dlg.Filter = "PNG File (*.png)|*.png|All files (*.*)|*.*";
				if (_BaseDir != "")
				{
					dlg.InitialDirectory = _BaseDir;
				}
				if (_PngFileName != "")
				{
					dlg.FileName = _PngFileName;
			}else
			{
				dlg.FileName = "palette.png";
			}
			dlg.DefaultExt = ".png";
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					paletteCanvas1.LoadPict(dlg.FileName);
					_PalFileName = Path.GetFileName(dlg.FileName);
					_BaseDir = Path.GetDirectoryName(dlg.FileName);
				}
			}
		}
		public void ExportPng()
		{
			using (var dlg = new SaveFileDialog())
			{
				dlg.Filter = "PNG File (*.png)|*.png|All files (*.*)|*.*";
				if (_BaseDir != "")
				{
					dlg.InitialDirectory = _BaseDir;
				}
				if (_PngFileName != "")
				{
					dlg.FileName = _PngFileName;
				}else
				{
					dlg.FileName = "palette.png";
				}
				dlg.DefaultExt = ".png";
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					paletteCanvas1.SavePict(dlg.FileName);
					_PngFileName = Path.GetFileName(dlg.FileName);
					_BaseDir = Path.GetDirectoryName(dlg.FileName);
				}
			}
		}
		public void OpenPreview()
		{
			using (var dlg = new OpenFileDialog())
			{
				dlg.Filter = "PNG File (*.png)|*.png|All files (*.*)|*.*";
				if (_BaseDir != "")
				{
					dlg.InitialDirectory = _BaseDir;
				}
				if (_PreviewFileName != "")
				{
					dlg.FileName = _PreviewFileName;
				}else
				{
					dlg.FileName = "preview.png";
				}
				dlg.DefaultExt = ".png";
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					previewPanel1.LoadPngFile(dlg.FileName);
					_PreviewFileName = Path.GetFileName(dlg.FileName);
					_BaseDir = Path.GetDirectoryName(dlg.FileName);
				}
			}
		}
	}

}
