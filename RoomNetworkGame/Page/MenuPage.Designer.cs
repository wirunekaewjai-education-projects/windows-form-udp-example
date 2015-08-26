namespace RoomNetworkGame.Page
{
    partial class MenuPage
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
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.hostNameTextBox = new System.Windows.Forms.TextBox();
            this.joinNameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.hostIPTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(240, 290);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(180, 48);
            this.button2.TabIndex = 3;
            this.button2.Text = "Join";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.OnJoinClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(240, 131);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(180, 48);
            this.button1.TabIndex = 2;
            this.button1.Text = "Host";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OnHostClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(218, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Name :";
            // 
            // hostNameTextBox
            // 
            this.hostNameTextBox.Location = new System.Drawing.Point(265, 99);
            this.hostNameTextBox.Name = "hostNameTextBox";
            this.hostNameTextBox.Size = new System.Drawing.Size(173, 20);
            this.hostNameTextBox.TabIndex = 5;
            // 
            // joinNameTextBox
            // 
            this.joinNameTextBox.Location = new System.Drawing.Point(265, 230);
            this.joinNameTextBox.Name = "joinNameTextBox";
            this.joinNameTextBox.Size = new System.Drawing.Size(173, 20);
            this.joinNameTextBox.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(218, 233);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Name :";
            // 
            // hostIPTextBox
            // 
            this.hostIPTextBox.Location = new System.Drawing.Point(265, 256);
            this.hostIPTextBox.Name = "hostIPTextBox";
            this.hostIPTextBox.Size = new System.Drawing.Size(173, 20);
            this.hostIPTextBox.TabIndex = 9;
            this.hostIPTextBox.Text = "127.0.0.1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(211, 259);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Host IP :";
            // 
            // StartMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.hostIPTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.joinNameTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.hostNameTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "StartMenu";
            this.Size = new System.Drawing.Size(640, 480);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox hostNameTextBox;
        private System.Windows.Forms.TextBox joinNameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox hostIPTextBox;
        private System.Windows.Forms.Label label3;

    }
}
