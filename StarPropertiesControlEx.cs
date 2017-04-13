using System;
using System.Windows.Forms;
using TFlex.Model;

namespace Stars
{
	/// <summary>
	/// Класс, являющийся обёрткой вокруг UserControl-а, отображающего дополнительные свойства выбранной звезды
	/// Логика UserControl-а.
	/// </summary>
    public partial class StarPropertiesControlEx : UserControl
    {
        private StarCommand _command;

		private Document _document;

		/// <summary>
		/// Обработчик события изменения параметра звезды в диалоге.
		/// Синхронизация с объектом звезды и, следовательно, изображением.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		internal StarPropertiesControlEx(StarCommand comm, Document doc)
		{
			InitializeComponent();

            _command = comm;
			_document = doc;

            //подписаться на перемещение мыши
            _command.ShowCursor += new TFlex.Command.MouseEventHandler(OnShowCommandCursor);

			//Заполнить поля
			if (_command.Star.VarAngle.Value != null) this.textBoxAngle.Text = _command.Star.VarAngle.Value.Name;
			else this.textBoxAngle.RealValue = _command.Star.Angle;
			if (_command.Star.VarThickness.Value != null) this.textBoxThickness.Text = _command.Star.VarThickness.Value.Name;
			else this.textBoxThickness.RealValue = _command.Star.Thickness;
		}

		/// <summary>
		/// Обработчик события изменения параметра звезды в диалоге.
		/// Синхронизация с объектом звезды и, следовательно, изображением.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void buttonColor_Click(object sender, EventArgs e)
        {
            ColorDialog cDialog = new ColorDialog();
            if (cDialog.ShowDialog() == DialogResult.OK)
            {
                _command.Star.Color = TFlex.Drawing.StandardColors.IndexFromColor(cDialog.Color);
                _document.Redraw();
            }
            cDialog.Dispose();
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
            //заносим его в св-ва звезды и перерисовываем её
            _command.Star.Thickness = (int)textBoxThickness.RealValue;
            _command.Star.VarThickness.Value = textBoxThickness.Value.Variable;
            _document.Redraw();
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
            //и перерисовываем звезду					
            _command.Star.VarThickness.Value = textBoxThickness.Value.Variable;
            _document.Redraw();
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
            //заносим его в св-ва звезды и перерисовываем её
            _command.Star.Angle = textBoxAngle.RealValue;
            _command.Star.VarAngle.Value = textBoxAngle.Value.Variable;
            _document.Redraw();
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
            //и перерисовываем звезду					
            _command.Star.VarAngle.Value = textBoxAngle.Value.Variable;
            _document.Redraw();
		}

		/// <summary>
		/// Обработчик события изменения параметра звезды в диалоге.
		/// Синхронизация с объектом звезды и, следовательно, изображением.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void checkBoxFill_CheckStateChanged(object sender, EventArgs e)
        {
            _command.Star.Fill = checkBoxFill.Checked;
            _document.Redraw();
        }

		/// <summary>
		/// Обработчик события отображения звезды в комманде.
		/// Синхронизация с диалогом свойств звезды. (Синхронизация "в обратную сторону".)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void OnShowCommandCursor(object sender, TFlex.Command.MouseEventArgs e)
        {
			if (_command.Star != null)
            {
				if (this.textBoxAngle.RealValue != _command.Star.Angle)
					if (_command.Star.VarAngle.Value != null) this.textBoxAngle.Text = _command.Star.VarAngle.Value.Name;
					else this.textBoxAngle.RealValue = _command.Star.Angle;

				if (this.textBoxThickness.RealValue != _command.Star.Thickness)
					if (_command.Star.VarThickness.Value != null) this.textBoxThickness.Text = _command.Star.VarThickness.Value.Name;
					else this.textBoxThickness.RealValue = _command.Star.Thickness;

                checkBoxFill.Checked = _command.Star.Fill;
            }
        }
    }
}
