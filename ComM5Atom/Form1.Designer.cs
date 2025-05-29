namespace ComM5Atom
{
	partial class Form1
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.tbSend = new System.Windows.Forms.TextBox();
			this.btnSend = new System.Windows.Forms.Button();
			this.tbRecive = new System.Windows.Forms.TextBox();
			this.BtnGetSkin = new System.Windows.Forms.Button();
			this.BtnSetSkin = new System.Windows.Forms.Button();
			this.cmbPortList = new System.Windows.Forms.ComboBox();
			this.bntPortList = new System.Windows.Forms.Button();
			this.gpSkin = new System.Windows.Forms.GroupBox();
			this.btnClear = new System.Windows.Forms.Button();
			this.gbSend = new System.Windows.Forms.GroupBox();
			this.gpRecived = new System.Windows.Forms.GroupBox();
			this.m3StackColorBar1 = new ComM5Atom.M3StackColorBar();
			this.gpWifi = new System.Windows.Forms.GroupBox();
			this.tbSSID = new System.Windows.Forms.TextBox();
			this.tbPASSWORD = new System.Windows.Forms.TextBox();
			this.btnWifi = new System.Windows.Forms.Button();
			this.gpSkin.SuspendLayout();
			this.gbSend.SuspendLayout();
			this.gpRecived.SuspendLayout();
			this.gpWifi.SuspendLayout();
			this.SuspendLayout();
			// 
			// tbSend
			// 
			this.tbSend.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbSend.Location = new System.Drawing.Point(0, 18);
			this.tbSend.Multiline = true;
			this.tbSend.Name = "tbSend";
			this.tbSend.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbSend.Size = new System.Drawing.Size(475, 155);
			this.tbSend.TabIndex = 0;
			// 
			// btnSend
			// 
			this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSend.Enabled = false;
			this.btnSend.Location = new System.Drawing.Point(377, 179);
			this.btnSend.Name = "btnSend";
			this.btnSend.Size = new System.Drawing.Size(98, 23);
			this.btnSend.TabIndex = 1;
			this.btnSend.Text = "Send";
			this.btnSend.UseVisualStyleBackColor = true;
			this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
			// 
			// tbRecive
			// 
			this.tbRecive.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbRecive.Location = new System.Drawing.Point(6, 18);
			this.tbRecive.Multiline = true;
			this.tbRecive.Name = "tbRecive";
			this.tbRecive.ReadOnly = true;
			this.tbRecive.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.tbRecive.Size = new System.Drawing.Size(474, 105);
			this.tbRecive.TabIndex = 0;
			// 
			// BtnGetSkin
			// 
			this.BtnGetSkin.Location = new System.Drawing.Point(21, 84);
			this.BtnGetSkin.Name = "BtnGetSkin";
			this.BtnGetSkin.Size = new System.Drawing.Size(130, 23);
			this.BtnGetSkin.TabIndex = 1;
			this.BtnGetSkin.Text = "getSkin";
			this.BtnGetSkin.UseVisualStyleBackColor = true;
			this.BtnGetSkin.Click += new System.EventHandler(this.BtnGetSkin_Click);
			// 
			// BtnSetSkin
			// 
			this.BtnSetSkin.Location = new System.Drawing.Point(21, 113);
			this.BtnSetSkin.Name = "BtnSetSkin";
			this.BtnSetSkin.Size = new System.Drawing.Size(130, 23);
			this.BtnSetSkin.TabIndex = 2;
			this.BtnSetSkin.Text = "setSkin";
			this.BtnSetSkin.UseVisualStyleBackColor = true;
			this.BtnSetSkin.Click += new System.EventHandler(this.BtnSetSkin_Click);
			// 
			// cmbPortList
			// 
			this.cmbPortList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbPortList.FormattingEnabled = true;
			this.cmbPortList.Location = new System.Drawing.Point(106, 15);
			this.cmbPortList.Name = "cmbPortList";
			this.cmbPortList.Size = new System.Drawing.Size(152, 20);
			this.cmbPortList.TabIndex = 1;
			// 
			// bntPortList
			// 
			this.bntPortList.Location = new System.Drawing.Point(25, 12);
			this.bntPortList.Name = "bntPortList";
			this.bntPortList.Size = new System.Drawing.Size(75, 23);
			this.bntPortList.TabIndex = 0;
			this.bntPortList.Text = "getPortList";
			this.bntPortList.UseVisualStyleBackColor = true;
			this.bntPortList.Click += new System.EventHandler(this.bntPortList_Click);
			// 
			// gpSkin
			// 
			this.gpSkin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.gpSkin.Controls.Add(this.m3StackColorBar1);
			this.gpSkin.Controls.Add(this.BtnGetSkin);
			this.gpSkin.Controls.Add(this.BtnSetSkin);
			this.gpSkin.Enabled = false;
			this.gpSkin.Location = new System.Drawing.Point(515, 12);
			this.gpSkin.Name = "gpSkin";
			this.gpSkin.Size = new System.Drawing.Size(156, 157);
			this.gpSkin.TabIndex = 4;
			this.gpSkin.TabStop = false;
			this.gpSkin.Text = "SkinColor";
			// 
			// btnClear
			// 
			this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnClear.Location = new System.Drawing.Point(5, 129);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(98, 23);
			this.btnClear.TabIndex = 1;
			this.btnClear.Text = "Clear";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// gbSend
			// 
			this.gbSend.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gbSend.Controls.Add(this.tbSend);
			this.gbSend.Controls.Add(this.btnSend);
			this.gbSend.Location = new System.Drawing.Point(12, 46);
			this.gbSend.Name = "gbSend";
			this.gbSend.Size = new System.Drawing.Size(481, 207);
			this.gbSend.TabIndex = 2;
			this.gbSend.TabStop = false;
			this.gbSend.Text = "SendText";
			// 
			// gpRecived
			// 
			this.gpRecived.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gpRecived.Controls.Add(this.tbRecive);
			this.gpRecived.Controls.Add(this.btnClear);
			this.gpRecived.Location = new System.Drawing.Point(7, 259);
			this.gpRecived.Name = "gpRecived";
			this.gpRecived.Size = new System.Drawing.Size(486, 169);
			this.gpRecived.TabIndex = 3;
			this.gpRecived.TabStop = false;
			this.gpRecived.Text = "Recived";
			// 
			// m3StackColorBar1
			// 
			this.m3StackColorBar1.ColorValue = 30;
			this.m3StackColorBar1.Location = new System.Drawing.Point(21, 18);
			this.m3StackColorBar1.MaximumSize = new System.Drawing.Size(130, 60);
			this.m3StackColorBar1.MinimumSize = new System.Drawing.Size(130, 60);
			this.m3StackColorBar1.Name = "m3StackColorBar1";
			this.m3StackColorBar1.Size = new System.Drawing.Size(130, 60);
			this.m3StackColorBar1.TabIndex = 0;
			this.m3StackColorBar1.Text = "m3StackColorBar1";
			// 
			// gpWifi
			// 
			this.gpWifi.Controls.Add(this.btnWifi);
			this.gpWifi.Controls.Add(this.tbPASSWORD);
			this.gpWifi.Controls.Add(this.tbSSID);
			this.gpWifi.Location = new System.Drawing.Point(515, 184);
			this.gpWifi.Name = "gpWifi";
			this.gpWifi.Size = new System.Drawing.Size(151, 116);
			this.gpWifi.TabIndex = 5;
			this.gpWifi.TabStop = false;
			this.gpWifi.Text = "groupBox1";
			// 
			// tbSSID
			// 
			this.tbSSID.Location = new System.Drawing.Point(21, 18);
			this.tbSSID.Name = "tbSSID";
			this.tbSSID.Size = new System.Drawing.Size(100, 19);
			this.tbSSID.TabIndex = 0;
			// 
			// tbPASSWORD
			// 
			this.tbPASSWORD.Location = new System.Drawing.Point(21, 45);
			this.tbPASSWORD.Name = "tbPASSWORD";
			this.tbPASSWORD.Size = new System.Drawing.Size(100, 19);
			this.tbPASSWORD.TabIndex = 1;
			// 
			// btnWifi
			// 
			this.btnWifi.Location = new System.Drawing.Point(46, 75);
			this.btnWifi.Name = "btnWifi";
			this.btnWifi.Size = new System.Drawing.Size(75, 23);
			this.btnWifi.TabIndex = 2;
			this.btnWifi.Text = "Wifi";
			this.btnWifi.UseVisualStyleBackColor = true;
			this.btnWifi.Click += new System.EventHandler(this.btnWifi_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(692, 443);
			this.Controls.Add(this.gpWifi);
			this.Controls.Add(this.gpRecived);
			this.Controls.Add(this.gbSend);
			this.Controls.Add(this.gpSkin);
			this.Controls.Add(this.bntPortList);
			this.Controls.Add(this.cmbPortList);
			this.Name = "Form1";
			this.Text = "M5StackAtomS3 Control";
			this.gpSkin.ResumeLayout(false);
			this.gbSend.ResumeLayout(false);
			this.gbSend.PerformLayout();
			this.gpRecived.ResumeLayout(false);
			this.gpRecived.PerformLayout();
			this.gpWifi.ResumeLayout(false);
			this.gpWifi.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.TextBox tbSend;
		private System.Windows.Forms.Button btnSend;
		private System.Windows.Forms.TextBox tbRecive;
		private M3StackColorBar m3StackColorBar1;
		private System.Windows.Forms.Button BtnGetSkin;
		private System.Windows.Forms.Button BtnSetSkin;
		private System.Windows.Forms.ComboBox cmbPortList;
		private System.Windows.Forms.Button bntPortList;
		private System.Windows.Forms.GroupBox gpSkin;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.GroupBox gbSend;
		private System.Windows.Forms.GroupBox gpRecived;
		private System.Windows.Forms.GroupBox gpWifi;
		private System.Windows.Forms.Button btnWifi;
		private System.Windows.Forms.TextBox tbPASSWORD;
		private System.Windows.Forms.TextBox tbSSID;
	}
}

