namespace MegaPaint
{
    partial class DrawArea
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DrawArea));
            this.ctxtMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetRotationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveToFrontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveToBackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxtMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctxtMenu
            // 
            this.ctxtMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.resetRotationToolStripMenuItem,
            this.moveToFrontToolStripMenuItem,
            this.moveToBackToolStripMenuItem});
            this.ctxtMenu.Name = "contextMenu";
            this.ctxtMenu.Size = new System.Drawing.Size(148, 92);
            this.ctxtMenu.Opened += new System.EventHandler(this.ctxtMenu_Opened);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::MegaPaint.Properties.Resources.Delete;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // resetRotationToolStripMenuItem
            // 
            this.resetRotationToolStripMenuItem.Image = global::MegaPaint.Properties.Resources.ResetRotate;
            this.resetRotationToolStripMenuItem.Name = "resetRotationToolStripMenuItem";
            this.resetRotationToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.resetRotationToolStripMenuItem.Text = "Reset rotation";
            this.resetRotationToolStripMenuItem.Click += new System.EventHandler(this.resetRotationToolStripMenuItem_Click);
            // 
            // moveToFrontToolStripMenuItem
            // 
            this.moveToFrontToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("moveToFrontToolStripMenuItem.Image")));
            this.moveToFrontToolStripMenuItem.Name = "moveToFrontToolStripMenuItem";
            this.moveToFrontToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.moveToFrontToolStripMenuItem.Text = "Move to front";
            this.moveToFrontToolStripMenuItem.Click += new System.EventHandler(this.moveToFrontToolStripMenuItem_Click);
            // 
            // moveToBackToolStripMenuItem
            // 
            this.moveToBackToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("moveToBackToolStripMenuItem.Image")));
            this.moveToBackToolStripMenuItem.Name = "moveToBackToolStripMenuItem";
            this.moveToBackToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.moveToBackToolStripMenuItem.Text = "Move to back";
            this.moveToBackToolStripMenuItem.Click += new System.EventHandler(this.moveToBackToolStripMenuItem_Click);
            // 
            // DrawArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Name = "DrawArea";
            this.Size = new System.Drawing.Size(1200, 1200);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawArea_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DrawArea_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DrawArea_KeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DrawArea_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DrawArea_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DrawArea_MouseUp);
            this.ctxtMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip ctxtMenu;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveToFrontToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveToBackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetRotationToolStripMenuItem;
    }
}
