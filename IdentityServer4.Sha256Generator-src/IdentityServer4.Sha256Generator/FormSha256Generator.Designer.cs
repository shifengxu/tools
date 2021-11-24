namespace secureapiwrapper.sha256generator
{
    partial class FormSha256Generator
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.lblInput = new System.Windows.Forms.Label();
            this.lblSha256 = new System.Windows.Forms.Label();
            this.txtSha256 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.Location = new System.Drawing.Point(297, 58);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(308, 28);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "IdentityServer4 Sha256 Generator";
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(221, 115);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(520, 27);
            this.txtInput.TabIndex = 1;
            this.txtInput.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
            // 
            // lblInput
            // 
            this.lblInput.AutoSize = true;
            this.lblInput.Location = new System.Drawing.Point(151, 115);
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size(43, 20);
            this.lblInput.TabIndex = 2;
            this.lblInput.Text = "Input";
            // 
            // lblSha256
            // 
            this.lblSha256.AutoSize = true;
            this.lblSha256.Location = new System.Drawing.Point(151, 179);
            this.lblSha256.Name = "lblSha256";
            this.lblSha256.Size = new System.Drawing.Size(62, 20);
            this.lblSha256.TabIndex = 3;
            this.lblSha256.Text = "SHA256";
            // 
            // txtSha256
            // 
            this.txtSha256.Location = new System.Drawing.Point(221, 172);
            this.txtSha256.Name = "txtSha256";
            this.txtSha256.Size = new System.Drawing.Size(520, 27);
            this.txtSha256.TabIndex = 2;
            // 
            // FormSha256Generator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 509);
            this.Controls.Add(this.txtSha256);
            this.Controls.Add(this.lblSha256);
            this.Controls.Add(this.lblInput);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.lblTitle);
            this.Name = "FormSha256Generator";
            this.Text = "IdentityServer4 Sha256 Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Label lblInput;
        private System.Windows.Forms.Label lblSha256;
        private System.Windows.Forms.TextBox txtSha256;
    }
}

