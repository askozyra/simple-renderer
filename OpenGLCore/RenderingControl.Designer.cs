﻿
using System;

namespace OpenGLCore
{
    partial class RenderingControl
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
            this.SuspendLayout();
            // 
            // RenderingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Name = "RenderingControl";
            this.Load += new System.EventHandler(this.OpenGLRenderingControl_Load);
            this.HandleDestroyed += new System.EventHandler(this.OpenGLRenderingControl_Destroyed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RenderingControl_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RenderingControl_KeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RenderingControl_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.RenderingControl_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RenderingControl_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
