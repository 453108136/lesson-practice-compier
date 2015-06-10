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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.lexicalView = new System.Windows.Forms.ListView();
            this.lexical = new System.Windows.Forms.RadioButton();
            this.syntax = new System.Windows.Forms.RadioButton();
            this.threeAddr = new System.Windows.Forms.RadioButton();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.syntaxTreeView = new System.Windows.Forms.TreeView();
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
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(43, 81);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(366, 399);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(43, 512);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "打开";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(188, 512);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "保存";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(334, 512);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "编译";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // lexicalView
            // 
            this.lexicalView.Location = new System.Drawing.Point(446, 81);
            this.lexicalView.Name = "lexicalView";
            this.lexicalView.Size = new System.Drawing.Size(309, 399);
            this.lexicalView.TabIndex = 7;
            this.lexicalView.UseCompatibleStateImageBehavior = false;
            this.lexicalView.Visible = false;
            // 
            // lexical
            // 
            this.lexical.AutoSize = true;
            this.lexical.Location = new System.Drawing.Point(446, 41);
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
            this.syntax.Location = new System.Drawing.Point(581, 41);
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
            this.threeAddr.Location = new System.Drawing.Point(708, 41);
            this.threeAddr.Name = "threeAddr";
            this.threeAddr.Size = new System.Drawing.Size(59, 16);
            this.threeAddr.TabIndex = 10;
            this.threeAddr.TabStop = true;
            this.threeAddr.Text = "三地址";
            this.threeAddr.UseVisualStyleBackColor = true;
            this.threeAddr.Click += new System.EventHandler(this.threeAddr_Click);
            // 
            // richTextBox3
            // 
            this.richTextBox3.Location = new System.Drawing.Point(446, 81);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.Size = new System.Drawing.Size(309, 399);
            this.richTextBox3.TabIndex = 13;
            this.richTextBox3.Text = "";
            this.richTextBox3.Visible = false;
            // 
            // syntaxTreeView
            // 
            this.syntaxTreeView.Location = new System.Drawing.Point(446, 81);
            this.syntaxTreeView.Name = "syntaxTreeView";
            this.syntaxTreeView.Size = new System.Drawing.Size(309, 399);
            this.syntaxTreeView.TabIndex = 14;
            this.syntaxTreeView.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 562);
            this.Controls.Add(this.syntaxTreeView);
            this.Controls.Add(this.richTextBox3);
            this.Controls.Add(this.threeAddr);
            this.Controls.Add(this.syntax);
            this.Controls.Add(this.lexical);
            this.Controls.Add(this.lexicalView);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.richTextBox1);
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
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ListView lexicalView;
        private System.Windows.Forms.RadioButton lexical;
        private System.Windows.Forms.RadioButton syntax;
        private System.Windows.Forms.RadioButton threeAddr;
        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.TreeView syntaxTreeView;
    }
}

