namespace TestingCP01
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
            this.rtbResult = new System.Windows.Forms.RichTextBox();
            this.lblServer = new System.Windows.Forms.Label();
            this.tbURL = new System.Windows.Forms.TextBox();
            this.btnOnHeat1 = new System.Windows.Forms.Button();
            this.btnOffHeat1 = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnOnHeat2 = new System.Windows.Forms.Button();
            this.btnWriteXML = new System.Windows.Forms.Button();
            this.btnOffHeat2 = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnOffAC2 = new System.Windows.Forms.Button();
            this.btnOffAC1 = new System.Windows.Forms.Button();
            this.btnAC2On = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.chbCool = new System.Windows.Forms.CheckBox();
            this.chbHeat = new System.Windows.Forms.CheckBox();
            this.chbHumidity = new System.Windows.Forms.CheckBox();
            this.chbDiff = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.tbACstart = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbACstop = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbHeaterOff = new System.Windows.Forms.TextBox();
            this.tbHeaterstart = new System.Windows.Forms.TextBox();
            this.tbDamperOff = new System.Windows.Forms.TextBox();
            this.tbDamperOn = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbResult
            // 
            this.rtbResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbResult.Location = new System.Drawing.Point(0, 251);
            this.rtbResult.Name = "rtbResult";
            this.rtbResult.Size = new System.Drawing.Size(795, 323);
            this.rtbResult.TabIndex = 5;
            this.rtbResult.Text = "";
            // 
            // lblServer
            // 
            this.lblServer.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(20, 19);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(29, 13);
            this.lblServer.TabIndex = 29;
            this.lblServer.Text = "URL";
            // 
            // tbURL
            // 
            this.tbURL.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbURL.Location = new System.Drawing.Point(23, 34);
            this.tbURL.Name = "tbURL";
            this.tbURL.Size = new System.Drawing.Size(220, 20);
            this.tbURL.TabIndex = 27;
            // 
            // btnOnHeat1
            // 
            this.btnOnHeat1.Location = new System.Drawing.Point(977, 73);
            this.btnOnHeat1.Name = "btnOnHeat1";
            this.btnOnHeat1.Size = new System.Drawing.Size(75, 23);
            this.btnOnHeat1.TabIndex = 21;
            this.btnOnHeat1.Text = "On Heater1";
            this.btnOnHeat1.UseVisualStyleBackColor = true;
            this.btnOnHeat1.Visible = false;
            // 
            // btnOffHeat1
            // 
            this.btnOffHeat1.Location = new System.Drawing.Point(977, 102);
            this.btnOffHeat1.Name = "btnOffHeat1";
            this.btnOffHeat1.Size = new System.Drawing.Size(75, 23);
            this.btnOffHeat1.TabIndex = 23;
            this.btnOffHeat1.Text = "Off Heater1";
            this.btnOffHeat1.UseVisualStyleBackColor = true;
            this.btnOffHeat1.Visible = false;
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(977, 195);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 23);
            this.btnRead.TabIndex = 26;
            this.btnRead.Text = "Read";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Visible = false;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(935, 224);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(84, 24);
            this.btnStop.TabIndex = 17;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Visible = false;
            // 
            // btnOnHeat2
            // 
            this.btnOnHeat2.Location = new System.Drawing.Point(977, 131);
            this.btnOnHeat2.Name = "btnOnHeat2";
            this.btnOnHeat2.Size = new System.Drawing.Size(75, 23);
            this.btnOnHeat2.TabIndex = 22;
            this.btnOnHeat2.Text = "On Heater2";
            this.btnOnHeat2.UseVisualStyleBackColor = true;
            this.btnOnHeat2.Visible = false;
            // 
            // btnWriteXML
            // 
            this.btnWriteXML.Location = new System.Drawing.Point(896, 195);
            this.btnWriteXML.Name = "btnWriteXML";
            this.btnWriteXML.Size = new System.Drawing.Size(75, 23);
            this.btnWriteXML.TabIndex = 25;
            this.btnWriteXML.Text = "Write";
            this.btnWriteXML.UseVisualStyleBackColor = true;
            this.btnWriteXML.Visible = false;
            // 
            // btnOffHeat2
            // 
            this.btnOffHeat2.Location = new System.Drawing.Point(977, 160);
            this.btnOffHeat2.Name = "btnOffHeat2";
            this.btnOffHeat2.Size = new System.Drawing.Size(75, 23);
            this.btnOffHeat2.TabIndex = 24;
            this.btnOffHeat2.Text = "Off Heater2";
            this.btnOffHeat2.UseVisualStyleBackColor = true;
            this.btnOffHeat2.Visible = false;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(887, 73);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(84, 24);
            this.btnStart.TabIndex = 16;
            this.btnStart.Text = "ON AC1";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Visible = false;
            // 
            // btnOffAC2
            // 
            this.btnOffAC2.Location = new System.Drawing.Point(887, 163);
            this.btnOffAC2.Name = "btnOffAC2";
            this.btnOffAC2.Size = new System.Drawing.Size(84, 24);
            this.btnOffAC2.TabIndex = 20;
            this.btnOffAC2.Text = "OffAC2";
            this.btnOffAC2.UseVisualStyleBackColor = true;
            this.btnOffAC2.Visible = false;
            // 
            // btnOffAC1
            // 
            this.btnOffAC1.Location = new System.Drawing.Point(887, 103);
            this.btnOffAC1.Name = "btnOffAC1";
            this.btnOffAC1.Size = new System.Drawing.Size(84, 24);
            this.btnOffAC1.TabIndex = 19;
            this.btnOffAC1.Text = "OffAC1";
            this.btnOffAC1.UseVisualStyleBackColor = true;
            this.btnOffAC1.Visible = false;
            // 
            // btnAC2On
            // 
            this.btnAC2On.Location = new System.Drawing.Point(887, 133);
            this.btnAC2On.Name = "btnAC2On";
            this.btnAC2On.Size = new System.Drawing.Size(84, 24);
            this.btnAC2On.TabIndex = 18;
            this.btnAC2On.Text = "ON AC2";
            this.btnAC2On.UseVisualStyleBackColor = true;
            this.btnAC2On.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "In Progress";
            this.label1.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox1.Location = new System.Drawing.Point(47, 156);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(295, 20);
            this.textBox1.TabIndex = 28;
            this.textBox1.Visible = false;
            // 
            // chbCool
            // 
            this.chbCool.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chbCool.AutoSize = true;
            this.chbCool.Location = new System.Drawing.Point(279, 18);
            this.chbCool.Name = "chbCool";
            this.chbCool.Size = new System.Drawing.Size(61, 17);
            this.chbCool.TabIndex = 32;
            this.chbCool.Text = "Cooling";
            this.chbCool.UseVisualStyleBackColor = true;
            // 
            // chbHeat
            // 
            this.chbHeat.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chbHeat.AutoSize = true;
            this.chbHeat.Location = new System.Drawing.Point(279, 39);
            this.chbHeat.Name = "chbHeat";
            this.chbHeat.Size = new System.Drawing.Size(63, 17);
            this.chbHeat.TabIndex = 33;
            this.chbHeat.Text = "Heating";
            this.chbHeat.UseVisualStyleBackColor = true;
            // 
            // chbHumidity
            // 
            this.chbHumidity.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chbHumidity.AutoSize = true;
            this.chbHumidity.Location = new System.Drawing.Point(362, 16);
            this.chbHumidity.Name = "chbHumidity";
            this.chbHumidity.Size = new System.Drawing.Size(66, 17);
            this.chbHumidity.TabIndex = 34;
            this.chbHumidity.Text = "Humidity";
            this.chbHumidity.UseVisualStyleBackColor = true;
            // 
            // chbDiff
            // 
            this.chbDiff.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chbDiff.AutoSize = true;
            this.chbDiff.Location = new System.Drawing.Point(362, 39);
            this.chbDiff.Name = "chbDiff";
            this.chbDiff.Size = new System.Drawing.Size(86, 17);
            this.chbDiff.TabIndex = 35;
            this.chbDiff.Text = "Diff Pressure";
            this.chbDiff.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.MidnightBlue;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1244, 60);
            this.panel2.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Bell MT", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.GreenYellow;
            this.label2.Location = new System.Drawing.Point(428, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(338, 40);
            this.label2.TabIndex = 37;
            this.label2.Text = "XSite Auto Test App";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::TestingCP01.Properties.Resources.plclogo;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(66, 60);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 36;
            this.pictureBox1.TabStop = false;
            // 
            // btnPlay
            // 
            this.btnPlay.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPlay.BackgroundImage = global::TestingCP01.Properties.Resources.play21;
            this.btnPlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPlay.FlatAppearance.BorderSize = 0;
            this.btnPlay.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnPlay.Location = new System.Drawing.Point(196, 72);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(66, 37);
            this.btnPlay.TabIndex = 36;
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnConnect.BackColor = System.Drawing.Color.Transparent;
            this.btnConnect.BackgroundImage = global::TestingCP01.Properties.Resources.Connect1;
            this.btnConnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnConnect.FlatAppearance.BorderSize = 0;
            this.btnConnect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnConnect.Location = new System.Drawing.Point(108, 72);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(67, 37);
            this.btnConnect.TabIndex = 31;
            this.btnConnect.UseVisualStyleBackColor = false;
            // 
            // tbACstart
            // 
            this.tbACstart.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbACstart.Location = new System.Drawing.Point(85, 36);
            this.tbACstart.Name = "tbACstart";
            this.tbACstart.Size = new System.Drawing.Size(59, 20);
            this.tbACstart.TabIndex = 37;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(605, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 38;
            this.label3.Text = "Start";
            // 
            // tbACstop
            // 
            this.tbACstop.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbACstop.Location = new System.Drawing.Point(160, 37);
            this.tbACstop.Name = "tbACstop";
            this.tbACstop.Size = new System.Drawing.Size(59, 20);
            this.tbACstop.TabIndex = 39;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(681, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 40;
            this.label4.Text = "Stop";
            // 
            // tbHeaterOff
            // 
            this.tbHeaterOff.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbHeaterOff.Location = new System.Drawing.Point(160, 63);
            this.tbHeaterOff.Name = "tbHeaterOff";
            this.tbHeaterOff.Size = new System.Drawing.Size(59, 20);
            this.tbHeaterOff.TabIndex = 42;
            // 
            // tbHeaterstart
            // 
            this.tbHeaterstart.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbHeaterstart.Location = new System.Drawing.Point(85, 64);
            this.tbHeaterstart.Name = "tbHeaterstart";
            this.tbHeaterstart.Size = new System.Drawing.Size(59, 20);
            this.tbHeaterstart.TabIndex = 41;
            // 
            // tbDamperOff
            // 
            this.tbDamperOff.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbDamperOff.Location = new System.Drawing.Point(161, 89);
            this.tbDamperOff.Name = "tbDamperOff";
            this.tbDamperOff.Size = new System.Drawing.Size(59, 20);
            this.tbDamperOff.TabIndex = 44;
            // 
            // tbDamperOn
            // 
            this.tbDamperOn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbDamperOn.Location = new System.Drawing.Point(85, 89);
            this.tbDamperOn.Name = "tbDamperOn";
            this.tbDamperOn.Size = new System.Drawing.Size(59, 20);
            this.tbDamperOn.TabIndex = 43;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 45;
            this.label5.Text = "AC ";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 46;
            this.label6.Text = "Heater";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 47;
            this.label7.Text = "Damper";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Controls.Add(this.tbURL);
            this.groupBox1.Controls.Add(this.lblServer);
            this.groupBox1.Controls.Add(this.btnPlay);
            this.groupBox1.Controls.Add(this.chbCool);
            this.groupBox1.Controls.Add(this.chbDiff);
            this.groupBox1.Controls.Add(this.chbHeat);
            this.groupBox1.Controls.Add(this.chbHumidity);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(502, 182);
            this.groupBox1.TabIndex = 48;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tests";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnLoad);
            this.groupBox2.Controls.Add(this.tbDamperOff);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.tbDamperOn);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.tbHeaterOff);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.tbHeaterstart);
            this.groupBox2.Controls.Add(this.tbACstart);
            this.groupBox2.Controls.Add(this.tbACstop);
            this.groupBox2.Location = new System.Drawing.Point(521, 66);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(274, 179);
            this.groupBox2.TabIndex = 49;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SetPoints";
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(105, 123);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(84, 24);
            this.btnLoad.TabIndex = 48;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1244, 563);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnOnHeat1);
            this.Controls.Add(this.btnOffHeat1);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnOnHeat2);
            this.Controls.Add(this.btnWriteXML);
            this.Controls.Add(this.btnOffHeat2);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnOffAC2);
            this.Controls.Add(this.btnOffAC1);
            this.Controls.Add(this.btnAC2On);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.rtbResult);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "Form1";
            this.Text = "TestDashboard";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox rtbResult;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.TextBox tbURL;
        private System.Windows.Forms.Button btnOnHeat1;
        private System.Windows.Forms.Button btnOffHeat1;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnOnHeat2;
        private System.Windows.Forms.Button btnWriteXML;
        private System.Windows.Forms.Button btnOffHeat2;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnOffAC2;
        private System.Windows.Forms.Button btnOffAC1;
        private System.Windows.Forms.Button btnAC2On;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox chbCool;
        private System.Windows.Forms.CheckBox chbHeat;
        private System.Windows.Forms.CheckBox chbHumidity;
        private System.Windows.Forms.CheckBox chbDiff;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.TextBox tbACstart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbACstop;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbHeaterOff;
        private System.Windows.Forms.TextBox tbHeaterstart;
        private System.Windows.Forms.TextBox tbDamperOff;
        private System.Windows.Forms.TextBox tbDamperOn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnLoad;
    }
}

