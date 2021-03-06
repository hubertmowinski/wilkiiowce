namespace Chess
{
    partial class Chess
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Chess));
            this.RestartButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SendMoveButton = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.Undo = new System.Windows.Forms.PictureBox();
            this.GameState = new System.Windows.Forms.Label();
            this.Timer = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.WhiteTimer = new System.Windows.Forms.Label();
            this.TimeButton = new System.Windows.Forms.Button();
            this.BlackTimer = new System.Windows.Forms.Label();
            this.State = new System.Windows.Forms.GroupBox();
            //this.Odbieranie = new System.ComponentModel.BackgroundWorker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TextPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.StopButton = new System.Windows.Forms.Button();
            this.StartButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TextAddress = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TextNick = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Undo)).BeginInit();
            this.Timer.SuspendLayout();
            this.State.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // RestartButton
            // 
            this.RestartButton.Enabled = false;
            this.RestartButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.RestartButton.Location = new System.Drawing.Point(18, 42);
            this.RestartButton.Margin = new System.Windows.Forms.Padding(2);
            this.RestartButton.Name = "RestartButton";
            this.RestartButton.Size = new System.Drawing.Size(49, 19);
            this.RestartButton.TabIndex = 0;
            this.RestartButton.Text = "Restart";
            this.RestartButton.UseVisualStyleBackColor = true;
            this.RestartButton.Click += new System.EventHandler(this.RestartButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.SendMoveButton);
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.Undo);
            this.groupBox1.Controls.Add(this.RestartButton);
            this.groupBox1.Location = new System.Drawing.Point(393, 153);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(93, 106);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controls";
            // 
            // SendMoveButton
            // 
            this.SendMoveButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.SendMoveButton.Location = new System.Drawing.Point(15, 65);
            this.SendMoveButton.Margin = new System.Windows.Forms.Padding(2);
            this.SendMoveButton.Name = "SendMoveButton";
            this.SendMoveButton.Size = new System.Drawing.Size(52, 30);
            this.SendMoveButton.TabIndex = 5;
            this.SendMoveButton.Text = "Send move";
            this.SendMoveButton.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Chess.Properties.Resources.CantRedo;
            this.pictureBox2.Location = new System.Drawing.Point(53, 17);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(24, 21);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // Undo
            // 
            this.Undo.Image = global::Chess.Properties.Resources.undoArrrow;
            this.Undo.Location = new System.Drawing.Point(10, 17);
            this.Undo.Margin = new System.Windows.Forms.Padding(2);
            this.Undo.Name = "Undo";
            this.Undo.Size = new System.Drawing.Size(24, 21);
            this.Undo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Undo.TabIndex = 3;
            this.Undo.TabStop = false;
            // 
            // GameState
            // 
            this.GameState.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.GameState.AutoSize = true;
            this.GameState.BackColor = System.Drawing.Color.Transparent;
            this.GameState.Font = new System.Drawing.Font("Stencil", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameState.ForeColor = System.Drawing.Color.OliveDrab;
            this.GameState.Location = new System.Drawing.Point(4, 14);
            this.GameState.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.GameState.Name = "GameState";
            this.GameState.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.GameState.Size = new System.Drawing.Size(67, 18);
            this.GameState.TabIndex = 2;
            this.GameState.Text = "Normal";
            this.GameState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Odbieranie
            // 
            //this.Odbieranie.WorkerSupportsCancellation = true;
            //this.Odbieranie.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Odbieranie_DoWork);
            // 
            // Timer
            // 
            this.Timer.BackColor = System.Drawing.Color.Transparent;
            this.Timer.Controls.Add(this.label5);
            this.Timer.Controls.Add(this.label4);
            this.Timer.Controls.Add(this.WhiteTimer);
            this.Timer.Controls.Add(this.TimeButton);
            this.Timer.Controls.Add(this.BlackTimer);
            this.Timer.Location = new System.Drawing.Point(396, 252);
            this.Timer.Margin = new System.Windows.Forms.Padding(2);
            this.Timer.Name = "Timer";
            this.Timer.Padding = new System.Windows.Forms.Padding(2);
            this.Timer.Size = new System.Drawing.Size(94, 126);
            this.Timer.TabIndex = 3;
            this.Timer.TabStop = false;
            this.Timer.Text = "Timer";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 65);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "White";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 30);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Black";
            // 
            // WhiteTimer
            // 
            this.WhiteTimer.AutoSize = true;
            this.WhiteTimer.BackColor = System.Drawing.Color.Goldenrod;
            this.WhiteTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WhiteTimer.Location = new System.Drawing.Point(47, 63);
            this.WhiteTimer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.WhiteTimer.Name = "WhiteTimer";
            this.WhiteTimer.Size = new System.Drawing.Size(44, 18);
            this.WhiteTimer.TabIndex = 2;
            this.WhiteTimer.Text = "00:00";
            // 
            // TimeButton
            // 
            this.TimeButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.TimeButton.Location = new System.Drawing.Point(28, 102);
            this.TimeButton.Margin = new System.Windows.Forms.Padding(2);
            this.TimeButton.Name = "TimeButton";
            this.TimeButton.Size = new System.Drawing.Size(42, 19);
            this.TimeButton.TabIndex = 1;
            this.TimeButton.Text = "Start";
            this.TimeButton.UseVisualStyleBackColor = true;
            this.TimeButton.Click += new System.EventHandler(this.TimeButton_Click);
            // 
            // BlackTimer
            // 
            this.BlackTimer.AutoSize = true;
            this.BlackTimer.BackColor = System.Drawing.Color.Goldenrod;
            this.BlackTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BlackTimer.Location = new System.Drawing.Point(47, 28);
            this.BlackTimer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.BlackTimer.Name = "BlackTimer";
            this.BlackTimer.Size = new System.Drawing.Size(44, 18);
            this.BlackTimer.TabIndex = 0;
            this.BlackTimer.Text = "00:00";
            // 
            // State
            // 
            this.State.Controls.Add(this.GameState);
            this.State.Location = new System.Drawing.Point(392, 382);
            this.State.Margin = new System.Windows.Forms.Padding(2);
            this.State.Name = "State";
            this.State.Padding = new System.Windows.Forms.Padding(2);
            this.State.Size = new System.Drawing.Size(96, 31);
            this.State.TabIndex = 4;
            this.State.TabStop = false;
            this.State.Text = "State";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.TextNick);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.TextAddress);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.TextPort);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.StopButton);
            this.groupBox2.Controls.Add(this.StartButton);
            this.groupBox2.Location = new System.Drawing.Point(392, 11);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(94, 138);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Connection";
            // 
            // TextPort
            // 
            this.TextPort.Location = new System.Drawing.Point(49, 44);
            this.TextPort.Name = "TextPort";
            this.TextPort.Size = new System.Drawing.Size(43, 20);
            this.TextPort.TabIndex = 9;
            this.TextPort.Text = "8000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 47);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Port";
            // 
            // StopButton
            // 
            this.StopButton.Enabled = false;
            this.StopButton.Location = new System.Drawing.Point(49, 19);
            this.StopButton.Margin = new System.Windows.Forms.Padding(2);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(41, 20);
            this.StopButton.TabIndex = 7;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            // 
            // StartButton
            // 
            this.StartButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.StartButton.Location = new System.Drawing.Point(10, 17);
            this.StartButton.Margin = new System.Windows.Forms.Padding(2);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(35, 22);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 67);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 26);
            this.label1.TabIndex = 10;
            this.label1.Text = "Adres IP \r\nserwera";
            // 
            // TextAddress
            // 
            this.TextAddress.Location = new System.Drawing.Point(49, 73);
            this.TextAddress.Margin = new System.Windows.Forms.Padding(2);
            this.TextAddress.Name = "TextAddress";
            this.TextAddress.Size = new System.Drawing.Size(45, 20);
            this.TextAddress.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Nick";
            // 
            // TextNick
            // 
            this.TextNick.Location = new System.Drawing.Point(45, 105);
            this.TextNick.Name = "TextNick";
            this.TextNick.Size = new System.Drawing.Size(49, 20);
            this.TextNick.TabIndex = 18;
            this.TextNick.Text = "Hubert";
            // 
            // Chess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 421);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.State);
            this.Controls.Add(this.Timer);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumSize = new System.Drawing.Size(529, 460);
            this.MinimumSize = new System.Drawing.Size(488, 460);
            this.Name = "Chess";
            this.Text = "Chess";
            this.Load += new System.EventHandler(this.Chess_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Undo)).EndInit();
            this.Timer.ResumeLayout(false);
            this.Timer.PerformLayout();
            this.State.ResumeLayout(false);
            this.State.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button RestartButton;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Label GameState;
        private System.Windows.Forms.GroupBox Timer;
        private System.Windows.Forms.Label BlackTimer;
        private System.Windows.Forms.Button TimeButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label WhiteTimer;
        private System.Windows.Forms.PictureBox Undo;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.GroupBox State;
        private System.Windows.Forms.GroupBox groupBox2;
        //private System.ComponentModel.BackgroundWorker Odbieranie;
        private System.Windows.Forms.TextBox TextPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button SendMoveButton;
        private System.Windows.Forms.TextBox TextNick;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TextAddress;
        private System.Windows.Forms.Label label1;
    }
}

