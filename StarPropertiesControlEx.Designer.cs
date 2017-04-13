namespace Stars
{
	/// <summary>
	/// Класс, являющийся обёрткой вокруг UserControl-а, отображающего дополнительные свойства выбранной звезды
	/// Внешние характеристики UserControl-а.
	/// </summary>
    partial class StarPropertiesControlEx
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
            this.textBoxAngle = new TFlex.VariableTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxThickness = new TFlex.VariableTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonColor = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxFill = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();

            // 
            // textBoxAngle
            // 
            this.textBoxAngle.Location = new System.Drawing.Point(99, 26);
            this.textBoxAngle.Name = "textBoxAngle";
            this.textBoxAngle.Size = new System.Drawing.Size(83, 20);
            this.textBoxAngle.TabIndex = 16;
            this.textBoxAngle.TextChanged += new System.EventHandler(this.textBoxAngle_TextChanged);
			this.textBoxAngle.LostFocus += new System.EventHandler(textBoxAngle_LostFocus);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(1, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 16);
            this.label1.TabIndex = 15;
            this.label1.Text = "Начальный угол:";
            // 
            // textBoxThickness
            // 
            this.textBoxThickness.Location = new System.Drawing.Point(100, 3);
            this.textBoxThickness.Name = "textBoxThickness";
            this.textBoxThickness.Size = new System.Drawing.Size(83, 20);
            this.textBoxThickness.TabIndex = 14;
            this.textBoxThickness.TextChanged += new System.EventHandler(this.textBoxThickness_TextChanged);
			this.textBoxThickness.LostFocus += new System.EventHandler(textBoxThickness_LostFocus);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(1, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 16);
            this.label2.TabIndex = 13;
            this.label2.Text = "Толщина линии:";
            // 
            // buttonColor
            // 
            this.buttonColor.Location = new System.Drawing.Point(99, 52);
            this.buttonColor.Name = "buttonColor";
            this.buttonColor.Size = new System.Drawing.Size(82, 23);
            this.buttonColor.TabIndex = 19;
            this.buttonColor.Text = "Выбрать...";
            this.buttonColor.UseVisualStyleBackColor = true;
            this.buttonColor.Click += new System.EventHandler(this.buttonColor_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Цвет звезды";
            // 
            // checkBoxFill
            // 
            this.checkBoxFill.Location = new System.Drawing.Point(4, 74);
            this.checkBoxFill.Name = "checkBoxFill";
            this.checkBoxFill.Size = new System.Drawing.Size(73, 19);
            this.checkBoxFill.TabIndex = 17;
            this.checkBoxFill.Text = "Заливка";
            this.checkBoxFill.CheckStateChanged += new System.EventHandler(this.checkBoxFill_CheckStateChanged);
            // 
            // StarPropertiesControlEx
            // 
            this.Controls.Add(this.buttonColor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBoxFill);
            this.Controls.Add(this.textBoxAngle);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxThickness);
            this.Controls.Add(this.label2);
            this.Name = "StarPropertiesControlEx";
            this.Size = new System.Drawing.Size(184, 96);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TFlex.VariableTextBox textBoxAngle;
        private System.Windows.Forms.Label label1;
        private TFlex.VariableTextBox textBoxThickness;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonColor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxFill;
    }
}
