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
            this.Infinite_Ammo = new System.Windows.Forms.CheckBox();
            this.Infinite_Health = new System.Windows.Forms.CheckBox();
            this.Jump = new System.Windows.Forms.CheckBox();
            this.No_Damage = new System.Windows.Forms.CheckBox();
            this.trackBar_Jump = new System.Windows.Forms.TrackBar();
            this.BGWorker = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.ProcOpenLabel = new System.Windows.Forms.Label();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Jump)).BeginInit();
            this.metroPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Infinite_Ammo
            // 
            this.Infinite_Ammo.AutoSize = true;
            this.Infinite_Ammo.Location = new System.Drawing.Point(20, 47);
            this.Infinite_Ammo.Margin = new System.Windows.Forms.Padding(4);
            this.Infinite_Ammo.Name = "Infinite_Ammo";
            this.Infinite_Ammo.Size = new System.Drawing.Size(118, 20);
            this.Infinite_Ammo.TabIndex = 0;
            this.Infinite_Ammo.Text = "Infinite Ammo";
            this.Infinite_Ammo.UseVisualStyleBackColor = true;
            // 
            // Infinite_Health
            // 
            this.Infinite_Health.AutoSize = true;
            this.Infinite_Health.BackColor = System.Drawing.Color.DimGray;
            this.Infinite_Health.Location = new System.Drawing.Point(20, 19);
            this.Infinite_Health.Margin = new System.Windows.Forms.Padding(4);
            this.Infinite_Health.Name = "Infinite_Health";
            this.Infinite_Health.Size = new System.Drawing.Size(120, 20);
            this.Infinite_Health.TabIndex = 1;
            this.Infinite_Health.Text = "Infinite Health";
            this.Infinite_Health.UseVisualStyleBackColor = false;
            // 
            // Jump
            // 
            this.Jump.AutoSize = true;
            this.Jump.Location = new System.Drawing.Point(20, 115);
            this.Jump.Margin = new System.Windows.Forms.Padding(4);
            this.Jump.Name = "Jump";
            this.Jump.Size = new System.Drawing.Size(63, 20);
            this.Jump.TabIndex = 2;
            this.Jump.Text = "Jump";
            this.Jump.UseVisualStyleBackColor = true;
            this.Jump.CheckedChanged += new System.EventHandler(this.Jump_CheckedChanged);
            // 
            // No_Damage
            // 
            this.No_Damage.AutoSize = true;
            this.No_Damage.Location = new System.Drawing.Point(20, 76);
            this.No_Damage.Margin = new System.Windows.Forms.Padding(4);
            this.No_Damage.Name = "No_Damage";
            this.No_Damage.Size = new System.Drawing.Size(109, 20);
            this.No_Damage.TabIndex = 3;
            this.No_Damage.Text = "No Damage";
            this.No_Damage.UseVisualStyleBackColor = true;
            this.No_Damage.CheckedChanged += new System.EventHandler(this.No_Damage_CheckedChanged);
            // 
            // trackBar_Jump
            // 
            this.trackBar_Jump.Location = new System.Drawing.Point(104, 111);
            this.trackBar_Jump.Margin = new System.Windows.Forms.Padding(4);
            this.trackBar_Jump.Maximum = 5;
            this.trackBar_Jump.Minimum = 1;
            this.trackBar_Jump.Name = "trackBar_Jump";
            this.trackBar_Jump.Size = new System.Drawing.Size(156, 45);
            this.trackBar_Jump.TabIndex = 4;
            this.trackBar_Jump.Value = 1;
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
            this.label1.Location = new System.Drawing.Point(7, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Proccess:";
            // 
            // ProcOpenLabel
            // 
            this.ProcOpenLabel.AutoSize = true;
            this.ProcOpenLabel.Location = new System.Drawing.Point(82, 26);
            this.ProcOpenLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ProcOpenLabel.Name = "ProcOpenLabel";
            this.ProcOpenLabel.Size = new System.Drawing.Size(33, 16);
            this.ProcOpenLabel.TabIndex = 6;
            this.ProcOpenLabel.Text = "N/A";
            // 
            // metroPanel1
            // 
            this.metroPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.metroPanel1.Controls.Add(this.No_Damage);
            this.metroPanel1.Controls.Add(this.Jump);
            this.metroPanel1.Controls.Add(this.trackBar_Jump);
            this.metroPanel1.Controls.Add(this.Infinite_Health);
            this.metroPanel1.Controls.Add(this.Infinite_Ammo);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(1, 45);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(330, 199);
            this.metroPanel1.TabIndex = 7;
            this.metroPanel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // Trainer_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 258);
            this.Controls.Add(this.ProcOpenLabel);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.DarkCyan;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Trainer_Form";
            this.Padding = new System.Windows.Forms.Padding(30, 74, 30, 25);
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Trainer_Form_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Jump)).EndInit();
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox Infinite_Ammo;
        private System.Windows.Forms.CheckBox Infinite_Health;
        private System.Windows.Forms.CheckBox Jump;
        private System.Windows.Forms.CheckBox No_Damage;
        private System.Windows.Forms.TrackBar trackBar_Jump;
        private System.ComponentModel.BackgroundWorker BGWorker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ProcOpenLabel;
        private MetroFramework.Controls.MetroPanel metroPanel1;
    }
}

