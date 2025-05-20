using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Management;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Timers;

namespace ComM5Atom
{

	public class RecevedEventArgs : EventArgs
	{
		public string Tag;
		public int Size;
		public byte[] Data;
		public string DataString
		{
			get
			{
				if (Data == null) return "";
				return System.Text.Encoding.UTF8.GetString(Data);
			}
		}
		public RecevedEventArgs(string tag,int size, byte[] d) 
		{
			Tag = tag;
			Size = size;
			Data = d; 
		}
	}
	public class ComM5AtomS3
	{
		private bool _chkMode = false;
		public delegate void RecevedEventHandler(object sender, RecevedEventArgs e);
		public event RecevedEventHandler Receved;


		public void OnReceived(RecevedEventArgs e)
		{
			if (Receved != null)
			{
				Receved(this, e);
			}
		}
		private string[] _portList = new string[0];
		public string[] PortList
		{
			get { return _portList; }
		}
		public SerialPort serialPort = new SerialPort();
		private int _PortIndex = -1;
		public int PortIndex
		{
			get { return _PortIndex; }
			set { _PortIndex = value; }
		}
		public ComM5AtomS3()
		{

			//ListupPort();
			serialPort.DataReceived += SerialPort_DataReceived;
		}

		

		private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			if(_chkMode) return;
			RecevedEventArgs rc = new RecevedEventArgs("", 0, new byte[0]);

			if(GetSerialData(out rc) == true)
			{
				OnReceived(rc);
			}

		}
		
		private bool GetSerialData(out RecevedEventArgs rc)
		{
			bool ret = false;
			rc = new RecevedEventArgs("", 0, new byte[0]);

			if (serialPort.IsOpen == false)
			{
				return ret;
			}
			int retry = 100;
			//待つ
			while (serialPort.BytesToRead < 8) 
			{
				System.Threading.Thread.Sleep(100);
				retry--;
				if (retry<=0)
				{
					return ret;
				}
			}
			//int bytesToRead = serialPort.BytesToRead;
			byte[] tt = new byte[4];
			byte[] ll = new byte[4];

			if (serialPort.Read(tt, 0, 4) != 4) return ret;
			var encoding = Encoding.GetEncoding("UTF-8");
			rc.Tag = encoding.GetString(tt);
			if (serialPort.Read(ll, 0, 4) != 4) return ret;
			int size = BitConverter.ToInt32(ll, 0);
			rc.Size = size;
			if(size <= 0)
			{
				ret = true;
				return ret;
			}
			byte[] buffer = new byte[size];
			int cnt = 0;
			int err = 0;
			while (cnt < size)
			{
				int rd = serialPort.BytesToRead;
				if (rd <= 0)
				{
					err++;
					if (err > 20)
					{
						err = -1;
						break;
					}
					System.Threading.Thread.Sleep(10);
					continue;
				}
				if (rd > size - cnt) rd = size - cnt;
				cnt += serialPort.Read(buffer, cnt, rd);
			}
			if (cnt < size)
			{
				Array.Resize(ref buffer, cnt);
			}
			rc.Data = buffer;
			ret = (cnt==size);
			return ret;
		}

		public string[] ListupPort()
		{
			chkPortM5Stack();
			return _portList;
		}
		public string[] GetDeviceNames()
		{
			var deviceNameList = new System.Collections.ArrayList();
			var check = new System.Text.RegularExpressions.Regex("(COM[1-9][0-9]?[0-9]?)");

			ManagementClass mcPnPEntity = new ManagementClass("Win32_PnPEntity");
			ManagementObjectCollection manageObjCol = mcPnPEntity.GetInstances();

			//全てのPnPデバイスを探索しシリアル通信が行われるデバイスを随時追加する
			foreach (ManagementObject manageObj in manageObjCol)
			{
				//Nameプロパティを取得
				var namePropertyValue = manageObj.GetPropertyValue("Name");
				if (namePropertyValue == null)
				{
					continue;
				}

				//Nameプロパティ文字列の一部が"(COM1)～(COM999)"と一致するときリストに追加"
				string name = namePropertyValue.ToString();
				if (check.IsMatch(name))
				{
					deviceNameList.Add(name);
				}
			}

			//戻り値作成
			if (deviceNameList.Count > 0)
			{
				string[] deviceNames = new string[deviceNameList.Count];

				int index = 0;
				foreach (var name in deviceNameList)
				{
					deviceNames[index] = name.ToString();
				}
				return deviceNames;
			}
			else
			{
				return null;
			}
		}
		private bool OpenPortOn(string nm)
		{
			if (serialPort.IsOpen)
			{
				serialPort.Close();
			}
			try
			{
				serialPort.PortName = nm;
				serialPort.BaudRate = 115200;
				serialPort.DataBits = 8;
				serialPort.Parity = Parity.None;
				serialPort.StopBits = StopBits.One;
				serialPort.Handshake = Handshake.None;
				serialPort.DtrEnable = false;
				serialPort.RtsEnable = false;
				serialPort.WriteBufferSize = 51200;
				serialPort.Open();
			}
			catch
			{
				return false;
			}
			return serialPort.IsOpen;
		}
		private bool chkPortM5Stack()
		{
			_portList = new string[0];
			if (serialPort.IsOpen)
			{
				serialPort.Close();
			}
			string[] lst = SerialPort.GetPortNames();
			if(lst.Length == 0) return false;
			List<string> lst2 = new List<string>();
			_chkMode = true;
			foreach (string nm in lst)
			{
				if (OpenPortOn(nm))
				{
					if (SendBinData("m5sk", "cnk"))
					{
						RecevedEventArgs rc = new RecevedEventArgs("", 0, new byte[0]);
						if(GetSerialData(out rc) == true)
						{
							if (rc.Tag == "m5sk")
							{
								lst2.Add(nm);
							}
						}
					}
				}
			}
			_chkMode = false;
			_portList = lst2.ToArray();
			_PortIndex = _portList.Length-1;
			return (_portList.Length > 0);
		}
		public bool OpenPort(int idx = 0)
		{
			if (serialPort.IsOpen)
			{
				return true;
			}
			if (idx < 0 || idx >= _portList.Length)
			{
				return false;
			}
			try
			{
				serialPort.PortName = _portList[idx];
				serialPort.BaudRate = 115200;
				serialPort.DataBits = 8;
				serialPort.Parity = Parity.None;
				serialPort.StopBits = StopBits.One;
				serialPort.Handshake = Handshake.None;
				serialPort.DtrEnable = false;
				serialPort.RtsEnable = false;
				serialPort.WriteBufferSize = 51200;
				_PortIndex = idx;
				serialPort.Open();
			}
			catch
			{
				_PortIndex = -1;
				return false;
			}
			return serialPort.IsOpen;
		}
		public void ClosePort()
		{
			if (serialPort.IsOpen)
			{
				serialPort.Close();
				_PortIndex = -1;
			}
		}
		public bool SendBinData(string tag, string data)
		{
			bool ret = false;
			if (serialPort.IsOpen == false)
			{
				return ret;
			}
			byte[] bdata = System.Text.Encoding.UTF8.GetBytes(data + '\0');
			byte[] head = CreateHeader(tag, bdata.Length);
			serialPort.Write(head, 0, head.Length);
			if (bdata.Length > 0)
			{
				serialPort.Write(bdata, 0, bdata.Length);
			}
			ret = true;
			return ret;
		}
		public bool SendBinData(string tag, byte[] data)
		{
			bool ret = false;
			if (serialPort.IsOpen == false)
			{
				return ret;
			}
			try
			{
				byte[] head = CreateHeader(tag, data.Length);
				serialPort.Write(head, 0, head.Length);
				if(data.Length > 0)
				{
					serialPort.Write(data, 0, data.Length);
				}
				ret = true;
			}
			catch
			{
				ret = false;
			}
			return ret;
		}
		public byte[] CompressBitmapToJpeg(Bitmap bitmap, long quality = 90L)
		{
			using (var ms = new MemoryStream())
			{
				// JPEGエンコーダを取得
				ImageCodecInfo jpegCodec = ImageCodecInfo.GetImageEncoders()
					.FirstOrDefault(codec => codec.FormatID == ImageFormat.Jpeg.Guid);

				// 品質パラメータを設定
				var encoderParams = new EncoderParameters(1);
				encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);

				// メモリストリームにJPEG形式で保存
				bitmap.Save(ms, jpegCodec, encoderParams);

				return ms.ToArray();
			}
		}
		private byte[] CreateHeader(string head, long sz)
		{
			byte[] header = new byte[8];
			header[0] = (byte)head[0];
			header[1] = (byte)head[1];
			header[2] = (byte)head[2];
			header[3] = (byte)head[3];

			header[4] = (byte)(sz & 0xFF);
			header[5] = (byte)(sz >> 8 & 0xFF);
			header[6] = (byte)(sz >> 16 & 0xFF);
			header[7] = (byte)(sz >> 24 & 0xFF);

			return header;
		}
		public bool SendJpegData(Bitmap bmp, long quality = 90L)
		{
			bool ret = false;
			if (serialPort.IsOpen)
			{
				byte[] jpegData = CompressBitmapToJpeg(bmp, quality);
				if (jpegData.Length > 150 * 1024)
				{
					quality -= 25;
					if (quality < 20) quality = 20;
					jpegData = CompressBitmapToJpeg(bmp, quality);
				}
				Debug.WriteLine("jpegData.Length = " + jpegData.Length.ToString());
				ret = SendBinData("jpeg", jpegData);
			}
			return ret;
		}
		public bool SendTextData(string s)
		{
			bool ret = false;
			if (serialPort.IsOpen)
			{
				byte[] bdata = System.Text.Encoding.UTF8.GetBytes(s+'\0');
				ret = SendBinData("text", bdata);
			}
			return ret;
		}
		public bool SendInt32(int a)
		{
			bool ret = false;
			if (serialPort.IsOpen)
			{
				byte[] bdata = BitConverter.GetBytes(a);
				ret = SendBinData("inte", bdata);
			}
			return ret;

		}

		public int GetSkin()
		{
			int ret = -1;
			if (serialPort.IsOpen)
			{
				SendBinData("gskn", new byte[0]);
			}
			return ret;
		}
		public void SetSkin(int v)
		{
			if (serialPort.IsOpen)
			{
				SendBinData("sskn", BitConverter.GetBytes(v));
			}
		}
	}
}
