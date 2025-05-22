using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Timers;
using System.Windows.Forms;
using System.Threading;
using RJCP.IO.Ports;

namespace STTest
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

			btnListup.Click += (s, e) =>
			{
				ListPorts();
			};
		}
		
		public void ListPorts()
		{
			cmbPortList.Items.Clear();
			string[] portList = System.IO.Ports.SerialPort.GetPortNames();
			cmbPortList.Items.AddRange(portList);
			cmbPortList.Enabled = (cmbPortList.Items.Count > 0);
			if (cmbPortList.Items.Count > 0)
			{
				cmbPortList.SelectedIndex = 0;
			}
		}

		public void SendJpeg()
		{
			if(cmbPortList.SelectedIndex < 0)
			{
				MessageBox.Show("Port is not selected.");
				return;
			}
			string portName = cmbPortList.Items[cmbPortList.SelectedIndex].ToString();	
			MessageBox.Show(portName);
			if (portName == "")
			{
				MessageBox.Show("Port is not selected.");
				return;
			}
			string identifier = "JPEG"; // 識別子（4バイト）


			Image img = Properties.Resources.girl;
			byte[] fileData;
			using (var ms = new MemoryStream())
			{
				img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
				fileData = ms.ToArray();
			}
			int sz = fileData.Length;
			byte[] sizeBytes = BitConverter.GetBytes(sz);
			byte[] idBytes = Encoding.ASCII.GetBytes(identifier.PadRight(4).Substring(0, 4));

			if (!BitConverter.IsLittleEndian)
				Array.Reverse(sizeBytes);  // ESP32 はリトルエンディアン
			var serial = new SerialPortStream(portName, 115200, 8, Parity.None, StopBits.One);
			serial.Open();
			// 先頭8バイト（識別子＋ファイルサイズ）を送信
			serial.Write(idBytes, 0, 4);
			serial.Write(sizeBytes, 0, 4);
			serial.Flush();
			Thread.Sleep(100);  // Arduino側の準備待ち

			// 本体データを送信（64バイトずつ）
			const int packetSize = 64;
			for (int i = 0; i < fileData.Length; i += packetSize)
			{
				int len = Math.Min(packetSize, fileData.Length - i);
				serial.Write(fileData, i, len);
				serial.Flush();
				Thread.Sleep(10);  // 少し待ってから次のパケット送信
			}

			// 終了コード（0x00）送信
			serial.WriteByte(0x00);
			serial.Flush();

			serial.Close();
			Console.WriteLine("送信完了！");
		}

		private void btnSend1_Click(object sender, EventArgs e)
		{
			SendJpeg();
		}
	}
}
