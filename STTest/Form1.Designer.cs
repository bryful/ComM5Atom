namespace STTest
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
			this.btnListup = new System.Windows.Forms.Button();
			this.cmbPortList = new System.Windows.Forms.ComboBox();
			this.btnSend1 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnListup
			// 
			this.btnListup.Location = new System.Drawing.Point(33, 23);
			this.btnListup.Name = "btnListup";
			this.btnListup.Size = new System.Drawing.Size(75, 23);
			this.btnListup.TabIndex = 0;
			this.btnListup.Text = "Listup";
			this.btnListup.UseVisualStyleBackColor = true;
			// 
			// cmbPortList
			// 
			this.cmbPortList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbPortList.Enabled = false;
			this.cmbPortList.FormattingEnabled = true;
			this.cmbPortList.Location = new System.Drawing.Point(123, 26);
			this.cmbPortList.Name = "cmbPortList";
			this.cmbPortList.Size = new System.Drawing.Size(121, 20);
			this.cmbPortList.TabIndex = 1;
			// 
			// btnSend1
			// 
			this.btnSend1.Location = new System.Drawing.Point(114, 62);
			this.btnSend1.Name = "btnSend1";
			this.btnSend1.Size = new System.Drawing.Size(99, 69);
			this.btnSend1.TabIndex = 2;
			this.btnSend1.Text = "Send1";
			this.btnSend1.UseVisualStyleBackColor = true;
			this.btnSend1.Click += new System.EventHandler(this.btnSend1_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(219, 62);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(99, 69);
			this.button1.TabIndex = 3;
			this.button1.Text = "Send2";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(391, 166);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.btnSend1);
			this.Controls.Add(this.cmbPortList);
			this.Controls.Add(this.btnListup);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnListup;
		private System.Windows.Forms.ComboBox cmbPortList;
		private System.Windows.Forms.Button btnSend1;
		private System.Windows.Forms.Button button1;
	}
}

