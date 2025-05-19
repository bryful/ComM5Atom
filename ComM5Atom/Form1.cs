using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComM5Atom
{
	public partial class Form1 : Form
	{
		public ComM5AtomS3 comM5Atom = new ComM5AtomS3();
		delegate void SetTextCallback(string text);

		public Form1()
		{
			InitializeComponent();
			comM5Atom.Receved += (sender, e) =>
			{
				if (e.Tag == "text")
				{
					if (tbRecive.InvokeRequired)
					{
						tbRecive.Invoke(new Action(() => tbRecive.AppendText(e.DataString + "\r\n")));
					}
					else
					{
						tbRecive.AppendText(e.DataString + "\r\n");
					}
				}
				else if(e.Tag == "gskn")
				{
					int col = BitConverter.ToInt32( e.Data, 0);
					if (m3StackColorBar1.InvokeRequired)
					{
						m3StackColorBar1.Invoke(new Action(() => m3StackColorBar1.ColorValue = col));
					}
					else
					{
						m3StackColorBar1.ColorValue = col;
					}
				}
				/*
				if((parts.Length >= 2) && (parts[0]=="skin"))
				{
					if (m3StackColorBar1.InvokeRequired)
					{
						m3StackColorBar1.Invoke(new Action(() => m3StackColorBar1.ColorValue = int.Parse(parts[1])));
					}
					else
					{
						m3StackColorBar1.ColorValue = int.Parse(parts[1]);
					}
				}*/
			};
			ListupPort();
			cmbPortList.SelectedIndexChanged += (sender, e) =>
			{
				btnSend.Enabled = (cmbPortList.SelectedIndex >= 0);
			};
		}
		private void ListupPort()
		{
			cmbPortList.Items.Clear();
			cmbPortList.Items.AddRange(comM5Atom.ListupPort());
			if(cmbPortList.Items.Count > 0)
			{
				cmbPortList.SelectedIndex = 0;
			}
			btnSend.Enabled = (cmbPortList.SelectedIndex >= 0);
		}
		private bool chekPort()
		{
			bool ret = false;
			if (cmbPortList.SelectedIndex < 0) return ret;
			if (comM5Atom.PortIndex < 0)
			{
				ret =  comM5Atom.OpenPort(cmbPortList.SelectedIndex);
			}
			else
			{
				if (cmbPortList.SelectedIndex != comM5Atom.PortIndex)
				{
					comM5Atom.ClosePort();
					ret = comM5Atom.OpenPort(cmbPortList.SelectedIndex);
				}
				else
				{
					ret = true;
				}
			}
			return ret;
		}
		private void btnSend_Click(object sender, EventArgs e)
		{
			if(chekPort() == false) return;
			comM5Atom.SendTextData(tbSend.Text);
		}

		private void BtnGetSkin_Click(object sender, EventArgs e)
		{
			if (chekPort() == false) return;
			comM5Atom.GetSkin();

		}


		private void bntPortList_Click(object sender, EventArgs e)
		{
			ListupPort();
		}


		private void btnClear_Click(object sender, EventArgs e)
		{
			tbRecive.Clear();
		}

		private void BtnSetSkin_Click(object sender, EventArgs e)
		{
			if (chekPort() == false) return;
			comM5Atom.SetSkin(m3StackColorBar1.ColorValue);

		}

		private void button1_Click(object sender, EventArgs e)
		{
			Int32 value = 0x0500FF01;
			byte[] data = BitConverter.GetBytes(value);

			string str = string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", data[0], data[1], data[2], data[3]);
			tbRecive.AppendText(str + "\r\n");
		}

	}
}
