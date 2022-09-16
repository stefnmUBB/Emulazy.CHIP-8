using Emulazy.Base;

namespace Emulazy.C8
{
    partial class C8EmuControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Frame = new Emulazy.Base.EmuFrame();
            ((System.ComponentModel.ISupportInitialize)(this.Frame)).BeginInit();
            this.SuspendLayout();
            // 
            // Frame
            // 
            this.Frame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Frame.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.Frame.Location = new System.Drawing.Point(0, 0);
            this.Frame.Name = "Frame";
            this.Frame.Size = new System.Drawing.Size(256, 128);
            this.Frame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Frame.TabIndex = 0;
            this.Frame.TabStop = false;
            this.Frame.Click += new System.EventHandler(this.Frame_Click);
            // 
            // C8EmuControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.Frame);
            this.Name = "C8EmuControl";
            this.Size = new System.Drawing.Size(256, 128);
            ((System.ComponentModel.ISupportInitialize)(this.Frame)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private EmuFrame Frame;
    }
}
