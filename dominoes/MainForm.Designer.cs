/*
 * Created by SharpDevelop.
 * User: Freddie
 * Date: 26/07/2014
 * Time: 13:24
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace dominoes
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.topPanel = new System.Windows.Forms.Panel();
			this.clearBtn = new System.Windows.Forms.Button();
			this.selF = new System.Windows.Forms.Label();
			this.writeBtn = new System.Windows.Forms.Button();
			this.iterBtn = new System.Windows.Forms.Button();
			this.readBtn = new System.Windows.Forms.Button();
			this.txt = new System.Windows.Forms.TextBox();
			this.viewF = new dominoes.PictureView();
			this.trimBtn = new System.Windows.Forms.Button();
			this.topPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.viewF)).BeginInit();
			this.SuspendLayout();
			// 
			// topPanel
			// 
			this.topPanel.Controls.Add(this.trimBtn);
			this.topPanel.Controls.Add(this.clearBtn);
			this.topPanel.Controls.Add(this.selF);
			this.topPanel.Controls.Add(this.writeBtn);
			this.topPanel.Controls.Add(this.iterBtn);
			this.topPanel.Controls.Add(this.readBtn);
			this.topPanel.Controls.Add(this.txt);
			this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.topPanel.Location = new System.Drawing.Point(0, 0);
			this.topPanel.Name = "topPanel";
			this.topPanel.Size = new System.Drawing.Size(566, 96);
			this.topPanel.TabIndex = 0;
			// 
			// clearBtn
			// 
			this.clearBtn.Location = new System.Drawing.Point(126, 64);
			this.clearBtn.Name = "clearBtn";
			this.clearBtn.Size = new System.Drawing.Size(75, 23);
			this.clearBtn.TabIndex = 5;
			this.clearBtn.Text = "Clear";
			this.clearBtn.UseVisualStyleBackColor = true;
			this.clearBtn.Click += new System.EventHandler(this.ClearBtnClick);
			// 
			// selF
			// 
			this.selF.Location = new System.Drawing.Point(103, 17);
			this.selF.Name = "selF";
			this.selF.Size = new System.Drawing.Size(133, 23);
			this.selF.TabIndex = 4;
			// 
			// writeBtn
			// 
			this.writeBtn.Location = new System.Drawing.Point(12, 38);
			this.writeBtn.Name = "writeBtn";
			this.writeBtn.Size = new System.Drawing.Size(75, 23);
			this.writeBtn.TabIndex = 3;
			this.writeBtn.Text = "Write";
			this.writeBtn.UseVisualStyleBackColor = true;
			this.writeBtn.Click += new System.EventHandler(this.WriteBtnClick);
			// 
			// iterBtn
			// 
			this.iterBtn.Location = new System.Drawing.Point(12, 64);
			this.iterBtn.Name = "iterBtn";
			this.iterBtn.Size = new System.Drawing.Size(75, 23);
			this.iterBtn.TabIndex = 2;
			this.iterBtn.Text = "Iter";
			this.iterBtn.UseVisualStyleBackColor = true;
			this.iterBtn.Click += new System.EventHandler(this.IterBtnClick);
			// 
			// readBtn
			// 
			this.readBtn.Location = new System.Drawing.Point(12, 12);
			this.readBtn.Name = "readBtn";
			this.readBtn.Size = new System.Drawing.Size(75, 23);
			this.readBtn.TabIndex = 1;
			this.readBtn.Text = "Read";
			this.readBtn.UseVisualStyleBackColor = true;
			this.readBtn.Click += new System.EventHandler(this.ReadBtnClick);
			// 
			// txt
			// 
			this.txt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txt.Location = new System.Drawing.Point(254, 3);
			this.txt.Multiline = true;
			this.txt.Name = "txt";
			this.txt.Size = new System.Drawing.Size(309, 90);
			this.txt.TabIndex = 0;
			// 
			// viewF
			// 
			this.viewF.Dock = System.Windows.Forms.DockStyle.Fill;
			this.viewF.Location = new System.Drawing.Point(0, 96);
			this.viewF.Name = "viewF";
			this.viewF.Size = new System.Drawing.Size(566, 177);
			this.viewF.TabIndex = 1;
			this.viewF.TabStop = false;
			this.viewF.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ViewFMouseMove);
			this.viewF.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ViewFMouseDown);
			this.viewF.Paint += new System.Windows.Forms.PaintEventHandler(this.ViewFPaint);
			this.viewF.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ViewFMouseUp);
			this.viewF.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewFKeyDown);
			// 
			// trimBtn
			// 
			this.trimBtn.Location = new System.Drawing.Point(126, 38);
			this.trimBtn.Name = "trimBtn";
			this.trimBtn.Size = new System.Drawing.Size(75, 23);
			this.trimBtn.TabIndex = 6;
			this.trimBtn.Text = "Trim";
			this.trimBtn.UseVisualStyleBackColor = true;
			this.trimBtn.Click += new System.EventHandler(this.TrimBtnClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(566, 273);
			this.Controls.Add(this.viewF);
			this.Controls.Add(this.topPanel);
			this.Name = "MainForm";
			this.Text = "dominoes";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.topPanel.ResumeLayout(false);
			this.topPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.viewF)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button clearBtn;
		private System.Windows.Forms.Button trimBtn;
		private System.Windows.Forms.Label selF;
		private System.Windows.Forms.Button writeBtn;
		private System.Windows.Forms.Button iterBtn;
		private System.Windows.Forms.TextBox txt;
		private System.Windows.Forms.Button readBtn;
		private dominoes.PictureView viewF;
		private System.Windows.Forms.Panel topPanel;
	}
}
