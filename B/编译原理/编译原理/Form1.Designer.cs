namespace compiler
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.fileBox = new System.Windows.Forms.RichTextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.lexicalView = new System.Windows.Forms.ListView();
            this.lexical = new System.Windows.Forms.RadioButton();
            this.syntax = new System.Windows.Forms.RadioButton();
            this.threeAddr = new System.Windows.Forms.RadioButton();
            this.sytaxBox = new System.Windows.Forms.RichTextBox();
            this.syntaxTreeView = new System.Windows.Forms.TreeView();
            this.autoButton = new System.Windows.Forms.Button();
            this.pauseButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.delayLab = new System.Windows.Forms.Label();
            this.delayBox = new System.Windows.Forms.TextBox();
            this.errorView = new System.Windows.Forms.ListView();
            this.LLerrorView = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(43, 34);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(273, 21);
            this.textBox1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(334, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "浏览";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // fileBox
            // 
            this.fileBox.Location = new System.Drawing.Point(43, 81);
            this.fileBox.Name = "fileBox";
            this.fileBox.Size = new System.Drawing.Size(397, 594);
            this.fileBox.TabIndex = 2;
            this.fileBox.Text = "";
            this.fileBox.WordWrap = false;
            this.fileBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.fileBox_MouseClick);
            this.fileBox.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            this.fileBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.fileBox_KeyUp);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(42, 692);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "保存";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(241, 692);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "编译";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // lexicalView
            // 
            this.lexicalView.Location = new System.Drawing.Point(485, 81);
            this.lexicalView.MultiSelect = false;
            this.lexicalView.Name = "lexicalView";
            this.lexicalView.Size = new System.Drawing.Size(373, 404);
            this.lexicalView.TabIndex = 7;
            this.lexicalView.UseCompatibleStateImageBehavior = false;
            this.lexicalView.Visible = false;
            // 
            // lexical
            // 
            this.lexical.AutoSize = true;
            this.lexical.Location = new System.Drawing.Point(485, 41);
            this.lexical.Name = "lexical";
            this.lexical.Size = new System.Drawing.Size(47, 16);
            this.lexical.TabIndex = 8;
            this.lexical.TabStop = true;
            this.lexical.Text = "词法";
            this.lexical.UseVisualStyleBackColor = true;
            this.lexical.Click += new System.EventHandler(this.lexical_Click);
            // 
            // syntax
            // 
            this.syntax.AutoSize = true;
            this.syntax.Location = new System.Drawing.Point(654, 41);
            this.syntax.Name = "syntax";
            this.syntax.Size = new System.Drawing.Size(47, 16);
            this.syntax.TabIndex = 9;
            this.syntax.TabStop = true;
            this.syntax.Text = "语法";
            this.syntax.UseVisualStyleBackColor = true;
            this.syntax.Click += new System.EventHandler(this.syntax_Click);
            // 
            // threeAddr
            // 
            this.threeAddr.AutoSize = true;
            this.threeAddr.Location = new System.Drawing.Point(803, 41);
            this.threeAddr.Name = "threeAddr";
            this.threeAddr.Size = new System.Drawing.Size(59, 16);
            this.threeAddr.TabIndex = 10;
            this.threeAddr.TabStop = true;
            this.threeAddr.Text = "三地址";
            this.threeAddr.UseVisualStyleBackColor = true;
            this.threeAddr.Click += new System.EventHandler(this.threeAddr_Click);
            // 
            // sytaxBox
            // 
            this.sytaxBox.Location = new System.Drawing.Point(485, 81);
            this.sytaxBox.Name = "sytaxBox";
            this.sytaxBox.Size = new System.Drawing.Size(373, 594);
            this.sytaxBox.TabIndex = 13;
            this.sytaxBox.Text = "";
            this.sytaxBox.Visible = false;
            // 
            // syntaxTreeView
            // 
            this.syntaxTreeView.Location = new System.Drawing.Point(485, 81);
            this.syntaxTreeView.Name = "syntaxTreeView";
            this.syntaxTreeView.Size = new System.Drawing.Size(373, 404);
            this.syntaxTreeView.TabIndex = 14;
            this.syntaxTreeView.Visible = false;
            // 
            // autoButton
            // 
            this.autoButton.Location = new System.Drawing.Point(558, 692);
            this.autoButton.Name = "autoButton";
            this.autoButton.Size = new System.Drawing.Size(75, 23);
            this.autoButton.TabIndex = 16;
            this.autoButton.Text = "自动";
            this.autoButton.UseVisualStyleBackColor = true;
            this.autoButton.Click += new System.EventHandler(this.autoButton_Click);
            // 
            // pauseButton
            // 
            this.pauseButton.Location = new System.Drawing.Point(783, 692);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(75, 23);
            this.pauseButton.TabIndex = 17;
            this.pauseButton.Text = "暂停";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "第：0行 0列";
            // 
            // delayLab
            // 
            this.delayLab.AutoSize = true;
            this.delayLab.Location = new System.Drawing.Point(388, 697);
            this.delayLab.Name = "delayLab";
            this.delayLab.Size = new System.Drawing.Size(77, 12);
            this.delayLab.TabIndex = 19;
            this.delayLab.Text = "延迟（毫秒）";
            // 
            // delayBox
            // 
            this.delayBox.Location = new System.Drawing.Point(456, 694);
            this.delayBox.Name = "delayBox";
            this.delayBox.Size = new System.Drawing.Size(58, 21);
            this.delayBox.TabIndex = 20;
            this.delayBox.Text = "1000";
            // 
            // errorView
            // 
            this.errorView.HideSelection = false;
            this.errorView.Location = new System.Drawing.Point(485, 502);
            this.errorView.Name = "errorView";
            this.errorView.Size = new System.Drawing.Size(373, 173);
            this.errorView.TabIndex = 21;
            this.errorView.UseCompatibleStateImageBehavior = false;
            this.errorView.Visible = false;
            this.errorView.SelectedIndexChanged += new System.EventHandler(this.errorView_SelectedIndexChanged);
            // 
            // LLerrorView
            // 
            this.LLerrorView.Location = new System.Drawing.Point(485, 502);
            this.LLerrorView.Name = "LLerrorView";
            this.LLerrorView.Size = new System.Drawing.Size(373, 173);
            this.LLerrorView.TabIndex = 22;
            this.LLerrorView.UseCompatibleStateImageBehavior = false;
            this.LLerrorView.Visible = false;
            this.LLerrorView.SelectedIndexChanged += new System.EventHandler(this.LLerrorView_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 731);
            this.Controls.Add(this.LLerrorView);
            this.Controls.Add(this.errorView);
            this.Controls.Add(this.delayBox);
            this.Controls.Add(this.delayLab);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.autoButton);
            this.Controls.Add(this.syntaxTreeView);
            this.Controls.Add(this.sytaxBox);
            this.Controls.Add(this.threeAddr);
            this.Controls.Add(this.syntax);
            this.Controls.Add(this.lexical);
            this.Controls.Add(this.lexicalView);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.fileBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "玩具编译器";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox fileBox;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ListView lexicalView;
        private System.Windows.Forms.RadioButton lexical;
        private System.Windows.Forms.RadioButton syntax;
        private System.Windows.Forms.RadioButton threeAddr;
        private System.Windows.Forms.RichTextBox sytaxBox;
        private System.Windows.Forms.TreeView syntaxTreeView;
        private System.Windows.Forms.Button autoButton;
        private System.Windows.Forms.Button pauseButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label delayLab;
        private System.Windows.Forms.TextBox delayBox;
        private System.Windows.Forms.ListView errorView;
        private System.Windows.Forms.ListView LLerrorView;
    }
}

