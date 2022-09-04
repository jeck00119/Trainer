using System.Drawing;

namespace Trainer
{
    partial class Trainer_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Trainer_Form));
            this.Walk_Speed = new System.Windows.Forms.CheckBox();
            this.Zoom = new System.Windows.Forms.CheckBox();
            this.Jump = new System.Windows.Forms.CheckBox();
            this.BGWorker = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.ProcOpenLabel = new System.Windows.Forms.Label();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.Fly_NoClip = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Teleport = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.wpTP_Button = new MetroFramework.Controls.MetroButton();
            this.wpTP_label = new System.Windows.Forms.Label();
            this.trackBar_Speed = new MetroFramework.Controls.MetroTrackBar();
            this.trackBar_Jump = new MetroFramework.Controls.MetroTrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Num_1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.metroPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Walk_Speed
            // 
            this.Walk_Speed.AutoSize = true;
            this.Walk_Speed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.Walk_Speed.Location = new System.Drawing.Point(100, 47);
            this.Walk_Speed.Margin = new System.Windows.Forms.Padding(4);
            this.Walk_Speed.Name = "Walk_Speed";
            this.Walk_Speed.Size = new System.Drawing.Size(111, 20);
            this.Walk_Speed.TabIndex = 0;
            this.Walk_Speed.Text = "Walk Speed";
            this.Walk_Speed.UseVisualStyleBackColor = false;
            this.Walk_Speed.CheckedChanged += new System.EventHandler(this.Walk_Speed_CheckedChanged);
            // 
            // Zoom
            // 
            this.Zoom.AutoSize = true;
            this.Zoom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.Zoom.Location = new System.Drawing.Point(100, 19);
            this.Zoom.Margin = new System.Windows.Forms.Padding(4);
            this.Zoom.Name = "Zoom";
            this.Zoom.Size = new System.Drawing.Size(89, 20);
            this.Zoom.TabIndex = 1;
            this.Zoom.Text = "Inf. Zoom";
            this.Zoom.UseVisualStyleBackColor = false;
            this.Zoom.CheckedChanged += new System.EventHandler(this.Zoom_CheckedChanged);
            // 
            // Jump
            // 
            this.Jump.AutoSize = true;
            this.Jump.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.Jump.Location = new System.Drawing.Point(100, 75);
            this.Jump.Margin = new System.Windows.Forms.Padding(4);
            this.Jump.Name = "Jump";
            this.Jump.Size = new System.Drawing.Size(63, 20);
            this.Jump.TabIndex = 2;
            this.Jump.Text = "Jump";
            this.Jump.UseVisualStyleBackColor = false;
            this.Jump.CheckedChanged += new System.EventHandler(this.Jump_CheckedChanged);
            // 
            // BGWorker
            // 
            this.BGWorker.WorkerReportsProgress = true;
            this.BGWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BGWorker_DoWork);
            this.BGWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BGWorker_ProgressChanged);
            this.BGWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BGWorker_RunWorkerCompleted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 43);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Process:";
            // 
            // ProcOpenLabel
            // 
            this.ProcOpenLabel.AutoSize = true;
            this.ProcOpenLabel.Location = new System.Drawing.Point(73, 43);
            this.ProcOpenLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ProcOpenLabel.Name = "ProcOpenLabel";
            this.ProcOpenLabel.Size = new System.Drawing.Size(33, 16);
            this.ProcOpenLabel.TabIndex = 6;
            this.ProcOpenLabel.Text = "N/A";
            // 
            // metroPanel1
            // 
            this.metroPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.metroPanel1.Controls.Add(this.Fly_NoClip);
            this.metroPanel1.Controls.Add(this.label6);
            this.metroPanel1.Controls.Add(this.Teleport);
            this.metroPanel1.Controls.Add(this.label3);
            this.metroPanel1.Controls.Add(this.wpTP_Button);
            this.metroPanel1.Controls.Add(this.wpTP_label);
            this.metroPanel1.Controls.Add(this.trackBar_Speed);
            this.metroPanel1.Controls.Add(this.trackBar_Jump);
            this.metroPanel1.Controls.Add(this.label5);
            this.metroPanel1.Controls.Add(this.label4);
            this.metroPanel1.Controls.Add(this.label2);
            this.metroPanel1.Controls.Add(this.Num_1);
            this.metroPanel1.Controls.Add(this.Jump);
            this.metroPanel1.Controls.Add(this.Zoom);
            this.metroPanel1.Controls.Add(this.Walk_Speed);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(10, 62);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(416, 197);
            this.metroPanel1.TabIndex = 7;
            this.metroPanel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // Fly_NoClip
            // 
            this.Fly_NoClip.AutoSize = true;
            this.Fly_NoClip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.Fly_NoClip.Location = new System.Drawing.Point(100, 159);
            this.Fly_NoClip.Margin = new System.Windows.Forms.Padding(4);
            this.Fly_NoClip.Name = "Fly_NoClip";
            this.Fly_NoClip.Size = new System.Drawing.Size(110, 20);
            this.Fly_NoClip.TabIndex = 19;
            this.Fly_NoClip.Text = "Fly + NoClip";
            this.Fly_NoClip.UseVisualStyleBackColor = false;
            this.Fly_NoClip.CheckedChanged += new System.EventHandler(this.Fly_NoClip_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.label6.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label6.Location = new System.Drawing.Point(32, 159);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 16);
            this.label6.TabIndex = 18;
            this.label6.Text = "Num 6";
            // 
            // Teleport
            // 
            this.Teleport.AutoSize = true;
            this.Teleport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.Teleport.Location = new System.Drawing.Point(100, 131);
            this.Teleport.Margin = new System.Windows.Forms.Padding(4);
            this.Teleport.Name = "Teleport";
            this.Teleport.Size = new System.Drawing.Size(173, 20);
            this.Teleport.TabIndex = 17;
            this.Teleport.Text = "Teleport to Locations";
            this.Teleport.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.label3.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label3.Location = new System.Drawing.Point(32, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 16);
            this.label3.TabIndex = 15;
            this.label3.Text = "Num 5";
            // 
            // wpTP_Button
            // 
            this.wpTP_Button.Location = new System.Drawing.Point(238, 105);
            this.wpTP_Button.Name = "wpTP_Button";
            this.wpTP_Button.Size = new System.Drawing.Size(52, 19);
            this.wpTP_Button.TabIndex = 14;
            this.wpTP_Button.Text = "Activate";
            this.wpTP_Button.Click += new System.EventHandler(this.wpTP_Button_Click);
            // 
            // wpTP_label
            // 
            this.wpTP_label.AutoSize = true;
            this.wpTP_label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.wpTP_label.Location = new System.Drawing.Point(97, 105);
            this.wpTP_label.Name = "wpTP_label";
            this.wpTP_label.Size = new System.Drawing.Size(135, 16);
            this.wpTP_label.TabIndex = 13;
            this.wpTP_label.Text = "Waypoint Teleport";
            // 
            // trackBar_Speed
            // 
            this.trackBar_Speed.BackColor = System.Drawing.Color.Transparent;
            this.trackBar_Speed.Location = new System.Drawing.Point(218, 47);
            this.trackBar_Speed.Name = "trackBar_Speed";
            this.trackBar_Speed.Size = new System.Drawing.Size(75, 23);
            this.trackBar_Speed.TabIndex = 12;
            this.trackBar_Speed.Text = "trackBar_Speed";
            this.trackBar_Speed.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.trackBar_Speed.ValueChanged += new System.EventHandler(this.trackBar_Speed_ValueChanged);
            // 
            // trackBar_Jump
            // 
            this.trackBar_Jump.BackColor = System.Drawing.Color.Transparent;
            this.trackBar_Jump.Location = new System.Drawing.Point(170, 76);
            this.trackBar_Jump.Name = "trackBar_Jump";
            this.trackBar_Jump.Size = new System.Drawing.Size(75, 23);
            this.trackBar_Jump.TabIndex = 11;
            this.trackBar_Jump.Text = "trackBar_Jump";
            this.trackBar_Jump.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.trackBar_Jump.ValueChanged += new System.EventHandler(this.trackBar_Jump_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.label5.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label5.Location = new System.Drawing.Point(32, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Num 4";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.label4.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label4.Location = new System.Drawing.Point(32, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Num 3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.label2.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label2.Location = new System.Drawing.Point(32, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Num 2";
            // 
            // Num_1
            // 
            this.Num_1.AutoSize = true;
            this.Num_1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.Num_1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.Num_1.Location = new System.Drawing.Point(32, 19);
            this.Num_1.Name = "Num_1";
            this.Num_1.Size = new System.Drawing.Size(50, 16);
            this.Num_1.TabIndex = 6;
            this.Num_1.Text = "Num 1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Sienna;
            this.label7.Location = new System.Drawing.Point(155, 9);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(117, 20);
            this.label7.TabIndex = 8;
            this.label7.Text = "FFXIV Online";
            // 
            // Trainer_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 269);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ProcOpenLabel);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.DarkCyan;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Trainer_Form";
            this.Padding = new System.Windows.Forms.Padding(30, 74, 30, 25);
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Trainer_Form_Shown);
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox Walk_Speed;
        private System.Windows.Forms.CheckBox Zoom;
        private System.Windows.Forms.CheckBox Jump;
        private System.ComponentModel.BackgroundWorker BGWorker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ProcOpenLabel;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Num_1;
        private MetroFramework.Controls.MetroTrackBar trackBar_Jump;
        private MetroFramework.Controls.MetroTrackBar trackBar_Speed;
        private System.Windows.Forms.Label wpTP_label;
        private MetroFramework.Controls.MetroButton wpTP_Button;
        private System.Windows.Forms.CheckBox Teleport;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox Fly_NoClip;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}

