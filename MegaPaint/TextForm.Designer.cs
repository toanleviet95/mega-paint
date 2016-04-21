namespace MegaPaint
{
    partial class frmTextForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTextForm));
            this.lblTextInput = new System.Windows.Forms.Label();
            this.txtTextInput = new System.Windows.Forms.TextBox();
            this.dlgFont = new System.Windows.Forms.FontDialog();
            this.btnBackground = new System.Windows.Forms.Button();
            this.lblFontColor = new System.Windows.Forms.Label();
            this.lblBackGround = new System.Windows.Forms.Label();
            this.btnFontColor = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnEditFont = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTextInput
            // 
            this.lblTextInput.AutoSize = true;
            this.lblTextInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblTextInput.Location = new System.Drawing.Point(9, 13);
            this.lblTextInput.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTextInput.Name = "lblTextInput";
            this.lblTextInput.Size = new System.Drawing.Size(84, 20);
            this.lblTextInput.TabIndex = 3;
            this.lblTextInput.Text = "Text Input:";
            // 
            // txtTextInput
            // 
            this.txtTextInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTextInput.Location = new System.Drawing.Point(96, 6);
            this.txtTextInput.Margin = new System.Windows.Forms.Padding(4);
            this.txtTextInput.Multiline = true;
            this.txtTextInput.Name = "txtTextInput";
            this.txtTextInput.Size = new System.Drawing.Size(446, 32);
            this.txtTextInput.TabIndex = 4;
            // 
            // btnBackground
            // 
            this.btnBackground.BackColor = System.Drawing.Color.White;
            this.btnBackground.ForeColor = System.Drawing.Color.Black;
            this.btnBackground.Location = new System.Drawing.Point(312, 56);
            this.btnBackground.Name = "btnBackground";
            this.btnBackground.Size = new System.Drawing.Size(81, 39);
            this.btnBackground.TabIndex = 6;
            this.btnBackground.UseVisualStyleBackColor = false;
            this.btnBackground.Click += new System.EventHandler(this.btnBackground_Click);
            // 
            // lblFontColor
            // 
            this.lblFontColor.AutoSize = true;
            this.lblFontColor.Location = new System.Drawing.Point(205, 98);
            this.lblFontColor.Name = "lblFontColor";
            this.lblFontColor.Size = new System.Drawing.Size(73, 17);
            this.lblFontColor.TabIndex = 7;
            this.lblFontColor.Text = "Font Color";
            // 
            // lblBackGround
            // 
            this.lblBackGround.AutoSize = true;
            this.lblBackGround.Location = new System.Drawing.Point(309, 98);
            this.lblBackGround.Name = "lblBackGround";
            this.lblBackGround.Size = new System.Drawing.Size(84, 17);
            this.lblBackGround.TabIndex = 8;
            this.lblBackGround.Text = "Background";
            // 
            // btnFontColor
            // 
            this.btnFontColor.BackColor = System.Drawing.Color.Black;
            this.btnFontColor.ForeColor = System.Drawing.Color.Black;
            this.btnFontColor.Location = new System.Drawing.Point(197, 56);
            this.btnFontColor.Name = "btnFontColor";
            this.btnFontColor.Size = new System.Drawing.Size(81, 39);
            this.btnFontColor.TabIndex = 9;
            this.btnFontColor.UseVisualStyleBackColor = false;
            this.btnFontColor.Click += new System.EventHandler(this.btnFontColor_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MegaPaint.Properties.Resources.Delete;
            this.btnCancel.Location = new System.Drawing.Point(442, 119);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 38);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MegaPaint.Properties.Resources.Check;
            this.btnOK.Location = new System.Drawing.Point(240, 119);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 38);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnEditFont
            // 
            this.btnEditFont.Image = global::MegaPaint.Properties.Resources.FontDialog;
            this.btnEditFont.Location = new System.Drawing.Point(13, 119);
            this.btnEditFont.Margin = new System.Windows.Forms.Padding(4);
            this.btnEditFont.Name = "btnEditFont";
            this.btnEditFont.Size = new System.Drawing.Size(100, 38);
            this.btnEditFont.TabIndex = 0;
            this.btnEditFont.Text = "Edit Font";
            this.btnEditFont.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEditFont.UseVisualStyleBackColor = true;
            this.btnEditFont.Click += new System.EventHandler(this.btnEditFont_Click);
            // 
            // frmTextForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 170);
            this.Controls.Add(this.btnFontColor);
            this.Controls.Add(this.lblBackGround);
            this.Controls.Add(this.lblFontColor);
            this.Controls.Add(this.btnBackground);
            this.Controls.Add(this.txtTextInput);
            this.Controls.Add(this.lblTextInput);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnEditFont);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frmTextForm";
            this.Text = "Text Dialog";
            this.Load += new System.EventHandler(this.frmTextForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEditFont;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblTextInput;
        private System.Windows.Forms.TextBox txtTextInput;
        private System.Windows.Forms.FontDialog dlgFont;
        private System.Windows.Forms.Button btnBackground;
        private System.Windows.Forms.Label lblFontColor;
        private System.Windows.Forms.Label lblBackGround;
        private System.Windows.Forms.Button btnFontColor;
    }
}