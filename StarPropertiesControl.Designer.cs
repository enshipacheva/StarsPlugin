using TFlex;

namespace Stars
{
	/// <summary>
	/// Класс, являющийся обёрткой вокруг UserControl-а, отображающего основные свойства выбранной звезды
	/// Внешние характеристики UserControl-а.
	/// </summary>
    partial class StarPropertiesControl
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
            this.textBoxR2 = new TFlex.VariableTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxR1 = new TFlex.VariableTextBox();
            this.label2 = new System.Windows.Forms.Label();
			this.textBoxNumber = new TFlex.VariableTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelCenterX = new System.Windows.Forms.Label();
            this.labelCenterY = new System.Windows.Forms.Label();
            this.textBoxCenterX = new TFlex.VariableTextBox();
            this.textBoxCenterY = new TFlex.VariableTextBox();
            this.SuspendLayout();
            // 
            // textBoxR2
            // 
            this.textBoxR2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxR2.Location = new System.Drawing.Point(120, 48);
            this.textBoxR2.Name = "textBoxR2";
            this.textBoxR2.Size = new System.Drawing.Size(63, 20);
            this.textBoxR2.TabIndex = 18;
            this.textBoxR2.TextChanged += new System.EventHandler(textBoxR2_TextChanged);
			this.textBoxR2.LostFocus += new System.EventHandler(textBoxR2_LostFocus);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(0, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 16);
            this.label3.TabIndex = 17;
            this.label3.Text = "Радиус 2:";
            // 
            // textBoxR1
            // 
            this.textBoxR1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxR1.Location = new System.Drawing.Point(120, 24);
            this.textBoxR1.Name = "textBoxR1";
            this.textBoxR1.Size = new System.Drawing.Size(63, 20);
            this.textBoxR1.TabIndex = 16;
            this.textBoxR1.TextChanged += new System.EventHandler(textBoxR1_TextChanged);
			this.textBoxR1.LostFocus += new System.EventHandler(textBoxR1_LostFocus);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 16);
            this.label2.TabIndex = 15;
            this.label2.Text = "Радиус 1:";
            // 
            // textBoxNumber
            // 
            this.textBoxNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNumber.Location = new System.Drawing.Point(120, 0);
            this.textBoxNumber.Name = "textBoxNumber";
            this.textBoxNumber.Size = new System.Drawing.Size(63, 20);
            this.textBoxNumber.TabIndex = 14;
            this.textBoxNumber.TextChanged += new System.EventHandler(textBoxNumber_TextChanged);
			this.textBoxNumber.LostFocus += new System.EventHandler(textBoxNumber_LostFocus);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 16);
            this.label1.TabIndex = 13;
            this.label1.Text = "Количество лучей:";
            // 
            // labelCenterX
            // 
            this.labelCenterX.AutoSize = true;
            this.labelCenterX.Location = new System.Drawing.Point(0, 81);
            this.labelCenterX.Name = "labelCenterX";
            this.labelCenterX.Size = new System.Drawing.Size(63, 13);
            this.labelCenterX.TabIndex = 19;
            this.labelCenterX.Text = "Центр - Ox:";
            // 
            // labelCenterY
            // 
            this.labelCenterY.AutoSize = true;
            this.labelCenterY.Location = new System.Drawing.Point(0, 107);
            this.labelCenterY.Name = "labelCenterY";
            this.labelCenterY.Size = new System.Drawing.Size(63, 13);
            this.labelCenterY.TabIndex = 20;
            this.labelCenterY.Text = "Центр - Oy:";
            // 
            // textBoxCenterX
            // 
            this.textBoxCenterX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCenterX.Location = new System.Drawing.Point(120, 78);
            this.textBoxCenterX.Name = "textBoxCenterX";
            this.textBoxCenterX.Size = new System.Drawing.Size(63, 20);
            this.textBoxCenterX.TabIndex = 21;
            this.textBoxCenterX.TextChanged += new System.EventHandler(textBoxCenterX_TextChanged);
			this.textBoxCenterX.LostFocus += new System.EventHandler(textBoxCenterX_LostFocus);
            // 
            // textBoxCenterY
            // 
            this.textBoxCenterY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCenterY.Location = new System.Drawing.Point(120, 104);
            this.textBoxCenterY.Name = "textBoxCenterY";
            this.textBoxCenterY.Size = new System.Drawing.Size(63, 20);
            this.textBoxCenterY.TabIndex = 22;
            this.textBoxCenterY.TextChanged += new System.EventHandler(textBoxCenterY_TextChanged);
			this.textBoxCenterY.LostFocus += new System.EventHandler(textBoxCenterY_LostFocus);
            // 
            // StarPropertiesControl
            // 
            this.Controls.Add(this.textBoxCenterY);
            this.Controls.Add(this.textBoxCenterX);
            this.Controls.Add(this.labelCenterY);
            this.Controls.Add(this.labelCenterX);
            this.Controls.Add(this.textBoxR2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxR1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxNumber);
            this.Controls.Add(this.label1);
            this.Name = "StarPropertiesControl";
            this.Size = new System.Drawing.Size(187, 131);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TFlex.VariableTextBox textBoxR2;
        private System.Windows.Forms.Label label3;
        private TFlex.VariableTextBox textBoxR1;
        private System.Windows.Forms.Label label2;
		private TFlex.VariableTextBox textBoxNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelCenterX;
        private System.Windows.Forms.Label labelCenterY;
        private TFlex.VariableTextBox textBoxCenterX;
        private TFlex.VariableTextBox textBoxCenterY;
    }
}
