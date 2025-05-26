using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
namespace ComBoard
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			String strHostName = string.Empty;
			IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
			IPAddress[] addr = ipEntry.AddressList;

			string str = "";
			for (int i = 0; i < addr.Length; i++)
			{
				str += $"IP Address {i}: {addr[i]}: [{addr[i].ToString()}] \r\n";
			}
			textBox1.Text = str;
		}
	}
}
