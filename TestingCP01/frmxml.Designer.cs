namespace TestingCP01
{
    partial class frmxml
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
            this.Read = new System.Windows.Forms.Button();
            this.richtb = new System.Windows.Forms.RichTextBox();
            this.Create = new System.Windows.Forms.Button();
            this.OpenFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Read
            // 
            this.Read.Location = new System.Drawing.Point(134, 12);
            this.Read.Name = "Read";
            this.Read.Size = new System.Drawing.Size(130, 32);
            this.Read.TabIndex = 0;
            this.Read.Text = "WriteData";
            this.Read.UseVisualStyleBackColor = true;
            this.Read.Click += new System.EventHandler(this.Read_Click);
            // 
            // richtb
            // 
            this.richtb.Location = new System.Drawing.Point(12, 50);
            this.richtb.Name = "richtb";
            this.richtb.Size = new System.Drawing.Size(391, 325);
            this.richtb.TabIndex = 2;
            this.richtb.Text = "";
            // 
            // Create
            // 
            this.Create.Location = new System.Drawing.Point(12, 12);
            this.Create.Name = "Create";
            this.Create.Size = new System.Drawing.Size(116, 32);
            this.Create.TabIndex = 3;
            this.Create.Text = "Create Empty File";
            this.Create.UseVisualStyleBackColor = true;
            this.Create.Click += new System.EventHandler(this.Create_Click);
            // 
            // OpenFile
            // 
            this.OpenFile.Location = new System.Drawing.Point(270, 12);
            this.OpenFile.Name = "OpenFile";
            this.OpenFile.Size = new System.Drawing.Size(137, 32);
            this.OpenFile.TabIndex = 4;
            this.OpenFile.Text = "Open File";
            this.OpenFile.UseVisualStyleBackColor = true;
            this.OpenFile.Click += new System.EventHandler(this.OpenFile_Click);
            // 
            // frmxml
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 387);
            this.Controls.Add(this.OpenFile);
            this.Controls.Add(this.Create);
            this.Controls.Add(this.richtb);
            this.Controls.Add(this.Read);
            this.Name = "frmxml";
            this.Text = "frmxml";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Read;
        private System.Windows.Forms.RichTextBox richtb;
        private System.Windows.Forms.Button Create;
        private System.Windows.Forms.Button OpenFile;
    }
}