//Prime Rollover - Text Based Harness
//(c) 2026 - TimeWarpToaster

//https://www.gnu.org/licenses/gpl-3.0.html

namespace PrimeRollover_Example
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnGo = new System.Windows.Forms.Button();
            this.lblPrime = new System.Windows.Forms.Label();
            this.selectPrime = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabMain = new System.Windows.Forms.TabPage();
            this.rtbMain = new System.Windows.Forms.RichTextBox();
            this.tabInstructions = new System.Windows.Forms.TabPage();
            this.rtbInstructions = new System.Windows.Forms.RichTextBox();
            this.tabDocumentation = new System.Windows.Forms.TabPage();
            this.rtbDocumentation = new System.Windows.Forms.RichTextBox();
            this.tabLogs = new System.Windows.Forms.TabPage();
            this.rtbLogsOut = new System.Windows.Forms.RichTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tabAbout = new System.Windows.Forms.TabPage();
            this.rtbAbout = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabInstructions.SuspendLayout();
            this.tabDocumentation.SuspendLayout();
            this.tabLogs.SuspendLayout();
            this.tabAbout.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1036, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(116, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 28);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.splitContainer1.Panel1.Controls.Add(this.btnGo);
            this.splitContainer1.Panel1.Controls.Add(this.lblPrime);
            this.splitContainer1.Panel1.Controls.Add(this.selectPrime);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1036, 582);
            this.splitContainer1.SplitterDistance = 199;
            this.splitContainer1.TabIndex = 2;
            // 
            // btnGo
            // 
            this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGo.Location = new System.Drawing.Point(40, 211);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(118, 29);
            this.btnGo.TabIndex = 2;
            this.btnGo.Text = "GO";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // lblPrime
            // 
            this.lblPrime.AutoSize = true;
            this.lblPrime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrime.ForeColor = System.Drawing.SystemColors.Window;
            this.lblPrime.Location = new System.Drawing.Point(36, 94);
            this.lblPrime.Name = "lblPrime";
            this.lblPrime.Size = new System.Drawing.Size(64, 20);
            this.lblPrime.TabIndex = 1;
            this.lblPrime.Text = "Prime:";
            // 
            // selectPrime
            // 
            this.selectPrime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectPrime.FormattingEnabled = true;
            this.selectPrime.Location = new System.Drawing.Point(37, 117);
            this.selectPrime.Name = "selectPrime";
            this.selectPrime.Size = new System.Drawing.Size(121, 24);
            this.selectPrime.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabMain);
            this.tabControl1.Controls.Add(this.tabInstructions);
            this.tabControl1.Controls.Add(this.tabDocumentation);
            this.tabControl1.Controls.Add(this.tabLogs);
            this.tabControl1.Controls.Add(this.tabAbout);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(833, 582);
            this.tabControl1.TabIndex = 0;
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.rtbMain);
            this.tabMain.Location = new System.Drawing.Point(4, 27);
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabMain.Size = new System.Drawing.Size(825, 551);
            this.tabMain.TabIndex = 0;
            this.tabMain.Text = "Main";
            this.tabMain.UseVisualStyleBackColor = true;
            // 
            // rtbMain
            // 
            this.rtbMain.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.rtbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbMain.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbMain.Location = new System.Drawing.Point(3, 3);
            this.rtbMain.Name = "rtbMain";
            this.rtbMain.ReadOnly = true;
            this.rtbMain.Size = new System.Drawing.Size(819, 545);
            this.rtbMain.TabIndex = 0;
            this.rtbMain.Text = "";
            // 
            // tabInstructions
            // 
            this.tabInstructions.Controls.Add(this.rtbInstructions);
            this.tabInstructions.Location = new System.Drawing.Point(4, 27);
            this.tabInstructions.Name = "tabInstructions";
            this.tabInstructions.Padding = new System.Windows.Forms.Padding(3);
            this.tabInstructions.Size = new System.Drawing.Size(825, 551);
            this.tabInstructions.TabIndex = 1;
            this.tabInstructions.Text = "Instructions";
            this.tabInstructions.UseVisualStyleBackColor = true;
            // 
            // rtbInstructions
            // 
            this.rtbInstructions.BackColor = System.Drawing.SystemColors.Window;
            this.rtbInstructions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbInstructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbInstructions.Location = new System.Drawing.Point(3, 3);
            this.rtbInstructions.Name = "rtbInstructions";
            this.rtbInstructions.ReadOnly = true;
            this.rtbInstructions.Size = new System.Drawing.Size(819, 545);
            this.rtbInstructions.TabIndex = 0;
            this.rtbInstructions.Text = "";
            // 
            // tabDocumentation
            // 
            this.tabDocumentation.Controls.Add(this.rtbDocumentation);
            this.tabDocumentation.Location = new System.Drawing.Point(4, 27);
            this.tabDocumentation.Name = "tabDocumentation";
            this.tabDocumentation.Padding = new System.Windows.Forms.Padding(3);
            this.tabDocumentation.Size = new System.Drawing.Size(825, 551);
            this.tabDocumentation.TabIndex = 2;
            this.tabDocumentation.Text = "Documentation";
            this.tabDocumentation.UseVisualStyleBackColor = true;
            // 
            // rtbDocumentation
            // 
            this.rtbDocumentation.BackColor = System.Drawing.SystemColors.Window;
            this.rtbDocumentation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbDocumentation.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbDocumentation.Location = new System.Drawing.Point(3, 3);
            this.rtbDocumentation.Name = "rtbDocumentation";
            this.rtbDocumentation.ReadOnly = true;
            this.rtbDocumentation.Size = new System.Drawing.Size(819, 545);
            this.rtbDocumentation.TabIndex = 0;
            this.rtbDocumentation.Text = "";
            // 
            // tabLogs
            // 
            this.tabLogs.Controls.Add(this.rtbLogsOut);
            this.tabLogs.Location = new System.Drawing.Point(4, 27);
            this.tabLogs.Name = "tabLogs";
            this.tabLogs.Padding = new System.Windows.Forms.Padding(3);
            this.tabLogs.Size = new System.Drawing.Size(825, 551);
            this.tabLogs.TabIndex = 3;
            this.tabLogs.Text = "Logs";
            this.tabLogs.UseVisualStyleBackColor = true;
            // 
            // rtbLogsOut
            // 
            this.rtbLogsOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLogsOut.Location = new System.Drawing.Point(3, 3);
            this.rtbLogsOut.Name = "rtbLogsOut";
            this.rtbLogsOut.Size = new System.Drawing.Size(819, 545);
            this.rtbLogsOut.TabIndex = 0;
            this.rtbLogsOut.Text = "";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(0, 610);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1036, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tabAbout
            // 
            this.tabAbout.Controls.Add(this.rtbAbout);
            this.tabAbout.Location = new System.Drawing.Point(4, 27);
            this.tabAbout.Name = "tabAbout";
            this.tabAbout.Padding = new System.Windows.Forms.Padding(3);
            this.tabAbout.Size = new System.Drawing.Size(825, 551);
            this.tabAbout.TabIndex = 4;
            this.tabAbout.Text = "About";
            this.tabAbout.UseVisualStyleBackColor = true;
            // 
            // rtbAbout
            // 
            this.rtbAbout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbAbout.Location = new System.Drawing.Point(3, 3);
            this.rtbAbout.Name = "rtbAbout";
            this.rtbAbout.Size = new System.Drawing.Size(819, 545);
            this.rtbAbout.TabIndex = 0;
            this.rtbAbout.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 632);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Prime Rollover - Text Based Harness";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabInstructions.ResumeLayout(false);
            this.tabDocumentation.ResumeLayout(false);
            this.tabLogs.ResumeLayout(false);
            this.tabAbout.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Label lblPrime;
        private System.Windows.Forms.ComboBox selectPrime;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabMain;
        private System.Windows.Forms.RichTextBox rtbMain;
        private System.Windows.Forms.TabPage tabInstructions;
        private System.Windows.Forms.TabPage tabDocumentation;
        private System.Windows.Forms.TabPage tabLogs;
        private System.Windows.Forms.RichTextBox rtbLogsOut;
        private System.Windows.Forms.RichTextBox rtbDocumentation;
        private System.Windows.Forms.RichTextBox rtbInstructions;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabPage tabAbout;
        private System.Windows.Forms.RichTextBox rtbAbout;
    }
}

