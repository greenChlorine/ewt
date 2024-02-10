namespace ewt360
{
    partial class courseitem
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.ratiobar = new System.Windows.Forms.ProgressBar();
            this.typeimg = new System.Windows.Forms.PictureBox();
            this.paperbtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.typeimg)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(101, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.DeepPink;
            this.label2.Location = new System.Drawing.Point(187, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(645, 18);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 31);
            this.button1.TabIndex = 2;
            this.button1.Text = "点击观看";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(541, 18);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(99, 31);
            this.button2.TabIndex = 2;
            this.button2.Text = "查看学案";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ratiobar
            // 
            this.ratiobar.Location = new System.Drawing.Point(107, 46);
            this.ratiobar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ratiobar.Name = "ratiobar";
            this.ratiobar.Size = new System.Drawing.Size(417, 12);
            this.ratiobar.TabIndex = 4;
            // 
            // typeimg
            // 
            this.typeimg.Location = new System.Drawing.Point(12, 8);
            this.typeimg.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.typeimg.Name = "typeimg";
            this.typeimg.Size = new System.Drawing.Size(81, 50);
            this.typeimg.TabIndex = 5;
            this.typeimg.TabStop = false;
            // 
            // paperbtn
            // 
            this.paperbtn.Location = new System.Drawing.Point(602, 20);
            this.paperbtn.Name = "paperbtn";
            this.paperbtn.Size = new System.Drawing.Size(86, 28);
            this.paperbtn.TabIndex = 6;
            this.paperbtn.Text = "完成试卷";
            this.paperbtn.UseVisualStyleBackColor = true;
            this.paperbtn.Visible = false;
            this.paperbtn.Click += new System.EventHandler(this.paperbtn_Click);
            // 
            // courseitem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.paperbtn);
            this.Controls.Add(this.typeimg);
            this.Controls.Add(this.ratiobar);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "courseitem";
            this.Size = new System.Drawing.Size(789, 70);
            this.Load += new System.EventHandler(this.courseitem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.typeimg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ProgressBar ratiobar;
        private System.Windows.Forms.PictureBox typeimg;
        private System.Windows.Forms.Button paperbtn;
    }
}
