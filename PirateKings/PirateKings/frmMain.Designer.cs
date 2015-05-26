namespace PirateKings
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.label1 = new System.Windows.Forms.Label();
            this.cmbAccount = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblPlayer = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.avatar = new System.Windows.Forms.PictureBox();
            this.avatarNext = new System.Windows.Forms.PictureBox();
            this.avatarPre = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.avatar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.avatarNext)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.avatarPre)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Account:";
            // 
            // cmbAccount
            // 
            this.cmbAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAccount.FormattingEnabled = true;
            this.cmbAccount.Items.AddRange(new object[] {
            "0969023566",
            "0912901720",
            "0974260220",
            "0944220487",
            "nhothuy48cb@gmail.com",
            "lethiminhht@gmail.com"});
            this.cmbAccount.Location = new System.Drawing.Point(79, 10);
            this.cmbAccount.Name = "cmbAccount";
            this.cmbAccount.Size = new System.Drawing.Size(273, 21);
            this.cmbAccount.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(358, 10);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(44, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblPlayer
            // 
            this.lblPlayer.AutoSize = true;
            this.lblPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayer.Location = new System.Drawing.Point(12, 35);
            this.lblPlayer.Name = "lblPlayer";
            this.lblPlayer.Size = new System.Drawing.Size(193, 13);
            this.lblPlayer.TabIndex = 4;
            this.lblPlayer.Text = "Rank:0 Shields:0 Spins:0 Cash:0";
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.Location = new System.Drawing.Point(12, 53);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(131, 13);
            this.lblInfo.TabIndex = 5;
            this.lblInfo.Text = "Name: Rank:0 Cash:0";
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "1.png");
            this.imageList.Images.SetKeyName(1, "2.png");
            this.imageList.Images.SetKeyName(2, "3.png");
            this.imageList.Images.SetKeyName(3, "4.png");
            this.imageList.Images.SetKeyName(4, "5.png");
            this.imageList.Images.SetKeyName(5, "6.png");
            this.imageList.Images.SetKeyName(6, "7.png");
            this.imageList.Images.SetKeyName(7, "8.png");
            this.imageList.Images.SetKeyName(8, "9.png");
            this.imageList.Images.SetKeyName(9, "10.png");
            this.imageList.Images.SetKeyName(10, "11.png");
            this.imageList.Images.SetKeyName(11, "12.png");
            this.imageList.Images.SetKeyName(12, "13.png");
            this.imageList.Images.SetKeyName(13, "14.png");
            this.imageList.Images.SetKeyName(14, "16.png");
            this.imageList.Images.SetKeyName(15, "17.png");
            this.imageList.Images.SetKeyName(16, "19.png");
            this.imageList.Images.SetKeyName(17, "20.png");
            this.imageList.Images.SetKeyName(18, "21.png");
            this.imageList.Images.SetKeyName(19, "22.png");
            this.imageList.Images.SetKeyName(20, "23.png");
            this.imageList.Images.SetKeyName(21, "15.png");
            this.imageList.Images.SetKeyName(22, "18.png");
            // 
            // avatar
            // 
            this.avatar.Image = global::PirateKings.Properties.Resources.pk;
            this.avatar.Location = new System.Drawing.Point(142, 86);
            this.avatar.Name = "avatar";
            this.avatar.Size = new System.Drawing.Size(127, 111);
            this.avatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.avatar.TabIndex = 6;
            this.avatar.TabStop = false;
            // 
            // avatarNext
            // 
            this.avatarNext.Image = global::PirateKings.Properties.Resources.pk;
            this.avatarNext.Location = new System.Drawing.Point(275, 86);
            this.avatarNext.Name = "avatarNext";
            this.avatarNext.Size = new System.Drawing.Size(127, 111);
            this.avatarNext.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.avatarNext.TabIndex = 7;
            this.avatarNext.TabStop = false;
            // 
            // avatarPre
            // 
            this.avatarPre.Image = global::PirateKings.Properties.Resources.pk;
            this.avatarPre.Location = new System.Drawing.Point(9, 86);
            this.avatarPre.Name = "avatarPre";
            this.avatarPre.Size = new System.Drawing.Size(127, 111);
            this.avatarPre.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.avatarPre.TabIndex = 8;
            this.avatarPre.TabStop = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 209);
            this.Controls.Add(this.avatarPre);
            this.Controls.Add(this.avatarNext);
            this.Controls.Add(this.avatar);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.lblPlayer);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cmbAccount);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pirate Kings | nhothuy48cb@gmail.com";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.avatar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.avatarNext)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.avatarPre)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbAccount;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblPlayer;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.PictureBox avatar;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.PictureBox avatarNext;
        private System.Windows.Forms.PictureBox avatarPre;
    }
}