using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Stars
{
	/// <summary>
	/// Класс, являющийся обёрткой вокруг модального диалога свойств
	/// Логика формы
	/// </summary>
    public partial class StarProperties : Form
    {
		public StarObject Star { set; get; }		
		
		/// <summary>
		/// Констуктор
		/// </summary>
        public StarProperties(StarObject star)
        {
            InitializeComponent();
			Star = new StarObject();
			Star.Assign(star);

			if (star.VarX.Value != null) this.textBoxCenterX.Text = star.VarX.Value.Name;
			else this.textBoxCenterX.RealValue = Star.X;
			if (star.VarX.Value != null) this.textBoxCenterY.Text = star.VarY.Value.Name;
			else this.textBoxCenterY.RealValue = Star.Y;
			if (star.VarR1.Value != null) this.textBoxR1.Text = star.VarR1.Value.Name;
			else this.textBoxR1.RealValue = Star.R1;
			if (star.VarR2.Value != null) this.textBoxR2.Text = star.VarR2.Value.Name;
			else this.textBoxR2.RealValue = Star.R2;
			if (star.VarNumber.Value != null) this.textBoxNumber.Text = star.VarNumber.Value.Name;
			else this.textBoxNumber.RealValue = Star.Number;
			if (star.VarAngle.Value != null) this.textBoxAngle.Text = star.VarAngle.Value.Name;
			else this.textBoxAngle.RealValue = Star.Angle;
			if (star.VarThickness.Value != null) this.textBoxThickness.Text = star.VarThickness.Value.Name;
			else this.textBoxThickness.RealValue = Star.Thickness;

            checkBoxFill.Checked = Star.Fill;
        }

		//-----------------------------------------------------------------------------------------

		/// <summary>
		/// Обработчик вызова диалога выбора цвета
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void buttonChoose_Click(object sender, EventArgs e)
        {
            ColorDialog cDialog = new ColorDialog();
            cDialog.Color = TFlex.Drawing.StandardColors.ColorFromIndex(Star.Color);

            if (cDialog.ShowDialog() == DialogResult.OK)
                Star.Color = TFlex.Drawing.StandardColors.IndexFromColor(cDialog.Color);

            cDialog.Dispose();
        }

		/// <summary>
		/// Обработчик события изменения параметра звезды в диалоге.
		/// Синхронизация с объектом звезды и, следовательно, изображением.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxNumber_TextChanged(object sender, EventArgs e)
		{
			//если изменение текста надо передать в звезду (оно мб косвенным - при подстановке в TextBox значения переменной, о котором звезда уже знает), 
			//заносим его в св-ва звезды
			if (Star.Number != textBoxNumber.RealValue)
				Star.Number = (int)textBoxNumber.RealValue;
		}

		/// <summary>
		/// Обработчик события потери фокуса окошка ввода для параметра звезды в диалоге.
		/// Передача звезде переменной, если она была создана, и перерисовка.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxNumber_LostFocus(object sender, EventArgs e)
		{
			//передаём вновь созданную переменную (textBoxNumber.Value.Variable) Holder-ам звезды (_command.Star.Var*****),
			Star.VarNumber.Value = textBoxNumber.Value.Variable;
		}

		/// <summary>
		/// Обработчик события изменения параметра звезды в диалоге.
		/// Синхронизация с объектом звезды и, следовательно, изображением.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxR1_TextChanged(object sender, EventArgs e)
		{
			//если изменение текста надо передать в звезду (оно мб косвенным - при подстановке в TextBox значения переменной, о котором звезда уже знает),
			//заносим его в св-ва звезды
			if (Star.R1 != textBoxR1.RealValue)
				Star.R1 = textBoxR1.RealValue;
		}

		/// <summary>
		/// Обработчик события потери фокуса окошка ввода для параметра звезды в диалоге.
		/// Передача звезде переменной, если она была создана, и перерисовка.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxR1_LostFocus(object sender, EventArgs e)
		{
			//передаём вновь созданную переменную (textBoxNumber.Value.Variable) Holder-ам звезды (_command.Star.Var*****)
			Star.VarR1.Value = textBoxR1.Value.Variable;				
		}

		/// <summary>
		/// Обработчик события изменения параметра звезды в диалоге.
		/// Синхронизация с объектом звезды и, следовательно, изображением.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxR2_TextChanged(object sender, EventArgs e)
		{
			//если изменение текста надо передать в звезду (оно мб косвенным - при подстановке в TextBox значения переменной, о котором звезда уже знает),
			//заносим его в св-ва звезды
			if (Star.R2 != textBoxR2.RealValue)
				Star.R2 = textBoxR2.RealValue;
		}

		/// <summary>
		/// Обработчик события потери фокуса окошка ввода для параметра звезды в диалоге.
		/// Передача звезде переменной, если она была создана, и перерисовка.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxR2_LostFocus(object sender, EventArgs e)
		{
			//передаём вновь созданную переменную (textBoxNumber.Value.Variable) Holder-ам звезды (_command.Star.Var*****)				
			Star.VarR2.Value = textBoxR2.Value.Variable;
		}

		
		/// <summary>
		/// Обработчик события изменения параметра звезды в диалоге.
		/// Синхронизация с объектом звезды и, следовательно, изображением.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxCenterX_TextChanged(object sender, EventArgs e)
		{
			//если изменение текста надо передать в звезду (оно мб косвенным - при подстановке в TextBox значения переменной, о котором звезда уже знает),
			//заносим его в св-ва звезды
			if (Star.X != textBoxCenterX.RealValue)
				Star.X = textBoxCenterX.RealValue;
		}

		/// <summary>
		/// Обработчик события потери фокуса окошка ввода для параметра звезды в диалоге.
		/// Передача звезде переменной, если она была создана, и перерисовка.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxCenterX_LostFocus(object sender, EventArgs e)
		{
			//передаём вновь созданную переменную (textBoxNumber.Value.Variable) Holder-ам звезды (_command.Star.Var*****)
			Star.VarX.Value = textBoxCenterX.Value.Variable;
		}

		/// <summary>
		/// Обработчик события изменения параметра звезды в диалоге.
		/// Синхронизация с объектом звезды и, следовательно, изображением.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxCenterY_TextChanged(object sender, EventArgs e)
		{
			//если изменение текста надо передать в звезду (оно мб косвенным - при подстановке в TextBox значения переменной, о котором звезда уже знает),
			//заносим его в св-ва звезды
			if (Star.Y != textBoxCenterY.RealValue)
				Star.Y = textBoxCenterY.RealValue;
		}

		/// <summary>
		/// Обработчик события потери фокуса окошка ввода для параметра звезды в диалоге.
		/// Передача звезде переменной, если она была создана, и перерисовка.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxCenterY_LostFocus(object sender, EventArgs e)
		{
			//передаём вновь созданную переменную (textBoxNumber.Value.Variable) Holder-ам звезды (_command.Star.Var*****)
			Star.VarY.Value = textBoxCenterY.Value.Variable;
		}
 

		/// <summary>
		/// Обработчик события изменения параметра звезды в диалоге.
		/// Синхронизация с объектом звезды и, следовательно, изображением.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxThickness_TextChanged(object sender, EventArgs e)
		{
			//если изменение текста надо передать в звезду (оно мб косвенным - при подстановке в TextBox значения переменной, о котором звезда уже знает),
			//заносим его в св-ва звезды
			if (Star.Thickness != textBoxThickness.RealValue)
				Star.Thickness = (int)textBoxThickness.RealValue;
		}

		/// <summary>
		/// Обработчик события потери фокуса окошка ввода для параметра звезды в диалоге.
		/// Передача звезде переменной, если она была создана, и перерисовка.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxThickness_LostFocus(object sender, EventArgs e)
		{
			//передаём вновь созданную переменную (textBoxNumber.Value.Variable) Holder-ам звезды (_command.Star.Var*****)
			Star.VarThickness.Value = textBoxThickness.Value.Variable;
		}

		/// <summary>
		/// Обработчик события изменения параметра звезды в диалоге.
		/// Синхронизация с объектом звезды и, следовательно, изображением.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxAngle_TextChanged(object sender, EventArgs e)
		{
			//если изменение текста надо передать в звезду (оно мб косвенным - при подстановке в TextBox значения переменной, о котором звезда уже знает),
			//заносим его в св-ва звезды
			if (Star.Angle != textBoxAngle.RealValue)
				Star.Angle = textBoxAngle.RealValue;
		}

		/// <summary>
		/// Обработчик события потери фокуса окошка ввода для параметра звезды в диалоге.
		/// Передача звезде переменной, если она была создана, и перерисовка.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxAngle_LostFocus(object sender, EventArgs e)
		{
			//передаём вновь созданную переменную (textBoxNumber.Value.Variable) Holder-ам звезды (_command.Star.Var*****)
			Star.VarAngle.Value = textBoxAngle.Value.Variable;
		}

		/// <summary>
		/// Обработчик события изменения параметра звезды в диалоге.
		/// Синхронизация с объектом звезды и, следовательно, изображением.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void checkBoxFill_CheckStateChanged(object sender, EventArgs e)
		{
			Star.Fill = checkBoxFill.Checked;
		}


    }
}
