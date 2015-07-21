namespace DelegateDemo
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.txName = new System.Windows.Forms.TextBox();
            this.btProcess = new System.Windows.Forms.Button();
            this.opCap = new System.Windows.Forms.RadioButton();
            this.opLower = new System.Windows.Forms.RadioButton();
            this.lsBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // txName
            // 
            this.txName.Location = new System.Drawing.Point(164, 23);
            this.txName.Name = "txName";
            this.txName.Size = new System.Drawing.Size(230, 21);
            this.txName.TabIndex = 0;
            // 
            // btProcess
            // 
            this.btProcess.Location = new System.Drawing.Point(242, 65);
            this.btProcess.Name = "btProcess";
            this.btProcess.Size = new System.Drawing.Size(75, 23);
            this.btProcess.TabIndex = 1;
            this.btProcess.Text = "Process";
            this.btProcess.UseVisualStyleBackColor = true;
            this.btProcess.Enabled = false;
            this.btProcess.Click += new System.EventHandler(this.btProcess_Click);
            // 
            // opCap
            // 
            this.opCap.AutoSize = true;
            this.opCap.Location = new System.Drawing.Point(434, 68);
            this.opCap.Name = "opCap";
            this.opCap.Size = new System.Drawing.Size(62, 16);
            this.opCap.TabIndex = 2;
            this.opCap.Text = "Capital";
            this.opCap.UseVisualStyleBackColor = true;
            this.opCap.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // opLower
            // 
            this.opLower.AutoSize = true;
            this.opLower.Location = new System.Drawing.Point(434, 111);
            this.opLower.Name = "opLower";
            this.opLower.Size = new System.Drawing.Size(58, 16);
            this.opLower.TabIndex = 3;
            this.opLower.Text = "Lower";
            this.opLower.UseVisualStyleBackColor = true;
            this.opLower.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // lsBox
            // 
            this.lsBox.FormattingEnabled = true;
            this.lsBox.ItemHeight = 12;
            this.lsBox.Location = new System.Drawing.Point(164, 128);
            this.lsBox.Name = "lsBox";
            this.lsBox.Size = new System.Drawing.Size(230, 172);
            this.lsBox.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 326);
            this.Controls.Add(this.lsBox);
            this.Controls.Add(this.opLower);
            this.Controls.Add(this.opCap);
            this.Controls.Add(this.btProcess);
            this.Controls.Add(this.txName);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txName;
        private System.Windows.Forms.Button btProcess;
        private System.Windows.Forms.RadioButton opCap;
        private System.Windows.Forms.RadioButton opLower;
        private System.Windows.Forms.ListBox lsBox;
    }
}

