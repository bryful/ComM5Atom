namespace c4palette
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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileManu = new System.Windows.Forms.ToolStripMenuItem();
			this.openMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.saveMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.importPngMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.exportPngMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.quitMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.paletteCanvas1 = new c4palette.PaletteCanvas();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileManu,
            this.editToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(551, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileManu
			// 
			this.fileManu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openMenu,
            this.saveMenu,
            this.toolStripMenuItem1,
            this.importPngMenu,
            this.exportPngMenu,
            this.toolStripMenuItem2,
            this.quitMenu});
			this.fileManu.Name = "fileManu";
			this.fileManu.Size = new System.Drawing.Size(37, 20);
			this.fileManu.Text = "File";
			// 
			// openMenu
			// 
			this.openMenu.Name = "openMenu";
			this.openMenu.Size = new System.Drawing.Size(180, 22);
			this.openMenu.Text = "Open";
			// 
			// saveMenu
			// 
			this.saveMenu.Name = "saveMenu";
			this.saveMenu.Size = new System.Drawing.Size(180, 22);
			this.saveMenu.Text = "Save";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
			// 
			// importPngMenu
			// 
			this.importPngMenu.Name = "importPngMenu";
			this.importPngMenu.Size = new System.Drawing.Size(180, 22);
			this.importPngMenu.Text = "ImportPng";
			// 
			// exportPngMenu
			// 
			this.exportPngMenu.Name = "exportPngMenu";
			this.exportPngMenu.Size = new System.Drawing.Size(180, 22);
			this.exportPngMenu.Text = "ExportPng";
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(177, 6);
			// 
			// quitMenu
			// 
			this.quitMenu.Name = "quitMenu";
			this.quitMenu.Size = new System.Drawing.Size(180, 22);
			this.quitMenu.Text = "Quit";
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyMenu,
            this.pasteMenu});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem.Text = "Edit";
			// 
			// copyMenu
			// 
			this.copyMenu.Name = "copyMenu";
			this.copyMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.copyMenu.Size = new System.Drawing.Size(142, 22);
			this.copyMenu.Text = "Copy";
			// 
			// pasteMenu
			// 
			this.pasteMenu.Name = "pasteMenu";
			this.pasteMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.pasteMenu.Size = new System.Drawing.Size(142, 22);
			this.pasteMenu.Text = "Paste";
			// 
			// paletteCanvas1
			// 
			this.paletteCanvas1.Location = new System.Drawing.Point(12, 27);
			this.paletteCanvas1.MaximumSize = new System.Drawing.Size(516, 56);
			this.paletteCanvas1.MinimumSize = new System.Drawing.Size(516, 56);
			this.paletteCanvas1.Name = "paletteCanvas1";
			this.paletteCanvas1.Size = new System.Drawing.Size(516, 56);
			this.paletteCanvas1.TabIndex = 0;
			this.paletteCanvas1.Text = "paletteCanvas1";
			// 
			// Form1
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(551, 88);
			this.Controls.Add(this.paletteCanvas1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form1";
			this.ShowIcon = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "c4palette";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private PaletteCanvas paletteCanvas1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileManu;
		private System.Windows.Forms.ToolStripMenuItem openMenu;
		private System.Windows.Forms.ToolStripMenuItem saveMenu;
		private System.Windows.Forms.ToolStripMenuItem quitMenu;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyMenu;
		private System.Windows.Forms.ToolStripMenuItem pasteMenu;
		private System.Windows.Forms.ToolStripMenuItem importPngMenu;
		private System.Windows.Forms.ToolStripMenuItem exportPngMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
	}
}

