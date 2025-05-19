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
			ListupPort();
			serialPort.DataReceived += SerialPort_DataReceived;
		}

		private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			while (serialPort.BytesToRead < 8) {; }
			int bytesToRead = serialPort.BytesToRead;
			byte[] tt = new byte[4];
			byte[] ll = new byte[4];

			serialPort.Read(tt, 0, 4);
			serialPort.Read(ll, 0, 4);
			int size = BitConverter.ToInt32(ll, 0);
			byte[] buffer = new byte[size];
			int cnt = 0;
			int err = 0;
			while (cnt<size) 
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
			if (cnt<size)
			{
				Array.Resize(ref buffer, cnt);
			}
			var encoding = Encoding.GetEncoding("UTF-8");
			OnReceived(new RecevedEventArgs(
				encoding.GetString(tt),
				size,
				buffer
				));
		}
		public string[] ListupPort()
		{
			_portList = SerialPort.GetPortNames();
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
			serialPort.Write(bdata, 0, bdata.Length);
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
		/*
		public string BitmapEncode(Bitmap bitmap, long quality = 90L)
		{
			byte[] jpegData = CompressBitmapToJpeg(bitmap);
			return Convert.ToBase64String(jpegData);

		}
		*/
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
			int ret = -1;
			if (serialPort.IsOpen)
			{
				SendBinData("sskn", BitConverter.GetBytes(v));
			}
		}
	}
}
