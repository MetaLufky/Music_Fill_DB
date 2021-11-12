
namespace MusicFillDB
{
  partial class Form1
  {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.label1 = new System.Windows.Forms.Label();
      this.m_tbFolderPath = new System.Windows.Forms.TextBox();
      this.m_btnFolderSearch = new System.Windows.Forms.Button();
      this.m_scMain = new System.Windows.Forms.SplitContainer();
      this.m_btnFillData = new System.Windows.Forms.Button();
      this.m_tbConsole = new System.Windows.Forms.TextBox();
      this.m_lblCountText = new System.Windows.Forms.Label();
      this.m_lblCount = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.m_scMain)).BeginInit();
      this.m_scMain.Panel1.SuspendLayout();
      this.m_scMain.Panel2.SuspendLayout();
      this.m_scMain.SuspendLayout();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(13, 12);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(157, 15);
      this.label1.TabIndex = 0;
      this.label1.Text = "Chemin d\'accès des playlists";
      // 
      // m_tbFolderPath
      // 
      this.m_tbFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.m_tbFolderPath.BackColor = System.Drawing.SystemColors.ControlDark;
      this.m_tbFolderPath.Enabled = false;
      this.m_tbFolderPath.Location = new System.Drawing.Point(13, 30);
      this.m_tbFolderPath.Name = "m_tbFolderPath";
      this.m_tbFolderPath.Size = new System.Drawing.Size(727, 23);
      this.m_tbFolderPath.TabIndex = 1;
      this.m_tbFolderPath.Text = "P:\\Music\\Playlist";
      // 
      // m_btnFolderSearch
      // 
      this.m_btnFolderSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.m_btnFolderSearch.Location = new System.Drawing.Point(746, 30);
      this.m_btnFolderSearch.Name = "m_btnFolderSearch";
      this.m_btnFolderSearch.Size = new System.Drawing.Size(26, 24);
      this.m_btnFolderSearch.TabIndex = 2;
      this.m_btnFolderSearch.Text = "...";
      this.m_btnFolderSearch.UseVisualStyleBackColor = true;
      this.m_btnFolderSearch.Click += new System.EventHandler(this.m_btnFolderSearch_Click);
      // 
      // m_scMain
      // 
      this.m_scMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.m_scMain.Cursor = System.Windows.Forms.Cursors.Default;
      this.m_scMain.Location = new System.Drawing.Point(13, 12);
      this.m_scMain.Name = "m_scMain";
      this.m_scMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // m_scMain.Panel1
      // 
      this.m_scMain.Panel1.Controls.Add(this.m_lblCount);
      this.m_scMain.Panel1.Controls.Add(this.m_lblCountText);
      this.m_scMain.Panel1.Controls.Add(this.m_btnFillData);
      this.m_scMain.Panel1.Controls.Add(this.label1);
      this.m_scMain.Panel1.Controls.Add(this.m_btnFolderSearch);
      this.m_scMain.Panel1.Controls.Add(this.m_tbFolderPath);
      // 
      // m_scMain.Panel2
      // 
      this.m_scMain.Panel2.AllowDrop = true;
      this.m_scMain.Panel2.AutoScroll = true;
      this.m_scMain.Panel2.Controls.Add(this.m_tbConsole);
      this.m_scMain.Size = new System.Drawing.Size(775, 426);
      this.m_scMain.SplitterDistance = 95;
      this.m_scMain.TabIndex = 3;
      // 
      // m_btnFillData
      // 
      this.m_btnFillData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.m_btnFillData.Location = new System.Drawing.Point(13, 60);
      this.m_btnFillData.Name = "m_btnFillData";
      this.m_btnFillData.Size = new System.Drawing.Size(402, 23);
      this.m_btnFillData.TabIndex = 3;
      this.m_btnFillData.Text = "Remplir la base de données";
      this.m_btnFillData.UseVisualStyleBackColor = true;
      this.m_btnFillData.Click += new System.EventHandler(this.m_btnFillData_Click);
      // 
      // m_tbConsole
      // 
      this.m_tbConsole.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_tbConsole.Enabled = false;
      this.m_tbConsole.Location = new System.Drawing.Point(0, 0);
      this.m_tbConsole.Multiline = true;
      this.m_tbConsole.Name = "m_tbConsole";
      this.m_tbConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.m_tbConsole.Size = new System.Drawing.Size(775, 327);
      this.m_tbConsole.TabIndex = 0;
      // 
      // m_lblCountText
      // 
      this.m_lblCountText.AutoSize = true;
      this.m_lblCountText.Location = new System.Drawing.Point(421, 64);
      this.m_lblCountText.Name = "m_lblCountText";
      this.m_lblCountText.Size = new System.Drawing.Size(126, 15);
      this.m_lblCountText.TabIndex = 4;
      this.m_lblCountText.Text = "Nombre de doublons :";
      // 
      // m_lblCount
      // 
      this.m_lblCount.AutoSize = true;
      this.m_lblCount.Location = new System.Drawing.Point(553, 64);
      this.m_lblCount.Name = "m_lblCount";
      this.m_lblCount.Size = new System.Drawing.Size(0, 15);
      this.m_lblCount.TabIndex = 5;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.m_scMain);
      this.Cursor = System.Windows.Forms.Cursors.Default;
      this.Name = "Form1";
      this.Text = "Fill Music To DB";
      this.m_scMain.Panel1.ResumeLayout(false);
      this.m_scMain.Panel1.PerformLayout();
      this.m_scMain.Panel2.ResumeLayout(false);
      this.m_scMain.Panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.m_scMain)).EndInit();
      this.m_scMain.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox m_tbFolderPath;
    private System.Windows.Forms.Button m_btnFolderSearch;
    private System.Windows.Forms.SplitContainer m_scMain;
    private System.Windows.Forms.Button m_btnFillData;
    private System.Windows.Forms.TextBox m_tbConsole;
    private System.Windows.Forms.Label m_lblCount;
    private System.Windows.Forms.Label m_lblCountText;
  }
}

