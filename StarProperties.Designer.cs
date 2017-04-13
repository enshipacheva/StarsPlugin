namespace Stars
{
	/// <summary>
	/// Класс, являющийся обёрткой вокруг модального диалога свойств
	/// Внешние характеристики форма
	/// </summary>
    partial class StarProperties
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxNumber = new TFlex.VariableTextBox();
            this.textBoxR1 = new TFlex.VariableTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxR2 = new TFlex.VariableTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxAngle = new TFlex.VariableTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxThickness = new TFlex.VariableTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxFill = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonChoose = new System.Windows.Forms.Button();

			this.textBoxCenterX = new TFlex.VariableTextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textBoxCenterY = new TFlex.VariableTextBox();
			this.label8 = new System.Windows.Forms.Label();

            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Количество лучей:";
            // 
			// buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(87, 265);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "ОК";
            // 
			// buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(170, 265);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Отмена";
            // 
            // textBoxNumber
            // 
            this.textBoxNumber.Location = new System.Drawing.Point(144, 8);
            this.textBoxNumber.Name = "textBoxNumber";
            this.textBoxNumber.Size = new System.Drawing.Size(100, 20);
            this.textBoxNumber.TabIndex = 3;
			this.textBoxNumber.TextChanged += new System.EventHandler(textBoxNumber_TextChanged);
			this.textBoxNumber.LostFocus += new System.EventHandler(textBoxNumber_LostFocus);
            // 
            // textBoxR1
            // 
            this.textBoxR1.Location = new System.Drawing.Point(144, 40);
            this.textBoxR1.Name = "textBoxR1";
            this.textBoxR1.Size = new System.Drawing.Size(100, 20);
            this.textBoxR1.TabIndex = 5;
			this.textBoxR1.TextChanged += new System.EventHandler(textBoxR1_TextChanged);
			this.textBoxR1.LostFocus += new System.EventHandler(textBoxR1_LostFocus);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Радиус 1:";
            // 
            // textBoxR2
            // 
            this.textBoxR2.Location = new System.Drawing.Point(144, 64);
            this.textBoxR2.Name = "textBoxR2";
            this.textBoxR2.Size = new System.Drawing.Size(100, 20);
            this.textBoxR2.TabIndex = 7;
			this.textBoxR2.TextChanged += new System.EventHandler(textBoxR2_TextChanged);
			this.textBoxR2.LostFocus += new System.EventHandler(textBoxR2_LostFocus);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Радиус 2:";
            // 
            // textBoxAngle
            // 
            this.textBoxAngle.Location = new System.Drawing.Point(144, 120);
            this.textBoxAngle.Name = "textBoxAngle";
            this.textBoxAngle.Size = new System.Drawing.Size(100, 20);
            this.textBoxAngle.TabIndex = 11;
			this.textBoxAngle.TextChanged += new System.EventHandler(textBoxAngle_TextChanged);
			this.textBoxAngle.LostFocus += new System.EventHandler(textBoxAngle_LostFocus);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Начальный угол:";
            // 
            // textBoxThickness
            // 
            this.textBoxThickness.Location = new System.Drawing.Point(144, 96);
            this.textBoxThickness.Name = "textBoxThickness";
            this.textBoxThickness.Size = new System.Drawing.Size(100, 20);
            this.textBoxThickness.TabIndex = 9;
			this.textBoxThickness.TextChanged += new System.EventHandler(textBoxThickness_TextChanged);
			this.textBoxThickness.LostFocus += new System.EventHandler(textBoxThickness_LostFocus);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(8, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "Толщина линии:";
			// 
			// textBoxCenterX
			// 
			this.textBoxCenterX.Location = new System.Drawing.Point(144, 152);
			this.textBoxCenterX.Name = "textBoxCenterX";
			this.textBoxCenterX.Size = new System.Drawing.Size(100, 20);
			this.textBoxCenterX.TabIndex = 15;
			this.textBoxCenterX.TextChanged += new System.EventHandler(textBoxCenterX_TextChanged);
			this.textBoxCenterX.LostFocus += new System.EventHandler(textBoxCenterX_LostFocus);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8, 152);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(120, 16);
			this.label7.TabIndex = 16;
			this.label7.Text = "Координата X:";
			// 
			// textBoxCenterY
			// 
			this.textBoxCenterY.Location = new System.Drawing.Point(144, 176);
			this.textBoxCenterY.Name = "textBoxCenterY";
			this.textBoxCenterY.Size = new System.Drawing.Size(100, 20);
			this.textBoxCenterY.TabIndex = 17;
			this.textBoxCenterY.TextChanged += new System.EventHandler(textBoxCenterY_TextChanged);
			this.textBoxCenterY.LostFocus += new System.EventHandler(textBoxCenterY_LostFocus);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(8, 176);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(120, 16);
			this.label8.TabIndex = 18;
			this.label8.Text = "Координата Y:";
			// 
            // checkBoxFill
            // 
            this.checkBoxFill.Location = new System.Drawing.Point(12, 228);
            this.checkBoxFill.Name = "checkBoxFill";
            this.checkBoxFill.Size = new System.Drawing.Size(73, 19);
            this.checkBoxFill.TabIndex = 12;
            this.checkBoxFill.Text = "Заливка";
			checkBoxFill.CheckStateChanged += new System.EventHandler(checkBoxFill_CheckStateChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 206);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Цвет звезды";
            // 
            // buttonChoose
            // 
            this.buttonChoose.Location = new System.Drawing.Point(144, 201);
            this.buttonChoose.Name = "buttonChoose";
            this.buttonChoose.Size = new System.Drawing.Size(75, 23);
            this.buttonChoose.TabIndex = 14;
            this.buttonChoose.Text = "Выбрать...";
            this.buttonChoose.UseVisualStyleBackColor = true;
            this.buttonChoose.Click += new System.EventHandler(this.buttonChoose_Click);
            // 
            // StarProperties
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(257, 300);
            this.Controls.Add(this.buttonChoose);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.checkBoxFill);
            this.Controls.Add(this.textBoxAngle);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxThickness);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxR2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxR1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxNumber);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.textBoxCenterX);
			this.Controls.Add(this.textBoxCenterY);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label8);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StarProperties";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Свойства звезды";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private TFlex.VariableTextBox textBoxNumber;
        private TFlex.VariableTextBox textBoxR1;
        private System.Windows.Forms.Label label2;
        private TFlex.VariableTextBox textBoxR2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private TFlex.VariableTextBox textBoxAngle;
        private TFlex.VariableTextBox textBoxThickness;
        private System.Windows.Forms.CheckBox checkBoxFill;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonChoose;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private TFlex.VariableTextBox textBoxCenterX;
		private TFlex.VariableTextBox textBoxCenterY;
    }
}