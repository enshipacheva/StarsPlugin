using System;
using System.Windows.Forms;
using TFlex.Model;


namespace Stars
{
	/// <summary>
	/// Класс, являющийся обёрткой вокруг UserControl-а, отображающего основные свойства выбранной звезды
	/// Логика UserControl-а.
	/// </summary>
    public partial class StarPropertiesControl : UserControl
    {
		/// <summary>
		/// храним ссылку на команду создания/редактирования, которая запущена для изменения звезды (для синхронизации)
		/// </summary>
        private StarCommand _command;

		/// <summary>
		/// храним документ, к которому относится создаваемая/редактируемая звезда
		/// </summary>
		private Document _document;
        
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="comm"></param>
		/// <param name="doc"></param>
        internal StarPropertiesControl(StarCommand comm, Document doc)
		{
			InitializeComponent();

            _command = comm;
			_document = doc;

            //подписаться на перемещение мыши
            _command.ShowCursor += new TFlex.Command.MouseEventHandler(OnShowCommandCursor);

			//Заполнить поля
			if (_command.Star.VarX.Value != null) this.textBoxCenterX.Text = _command.Star.VarX.Value.Name;
			else this.textBoxCenterX.RealValue = _command.Star.X;
			if (_command.Star.VarX.Value != null) this.textBoxCenterY.Text = _command.Star.VarY.Value.Name;
			else this.textBoxCenterY.RealValue = _command.Star.Y;
			if (_command.Star.VarR1.Value != null) this.textBoxR1.Text = _command.Star.VarR1.Value.Name;
			else this.textBoxR1.RealValue = _command.Star.R1;
			if (_command.Star.VarR2.Value != null) this.textBoxR2.Text = _command.Star.VarR2.Value.Name;
			else this.textBoxR2.RealValue = _command.Star.R2;
			if (_command.Star.VarNumber.Value != null) this.textBoxNumber.Text = _command.Star.VarNumber.Value.Name;
			else this.textBoxNumber.RealValue = _command.Star.Number;

		}

		/// <summary>
		/// Обработчик события изменения параметра звезды в диалоге.
		/// Синхронизация с объектом звезды и, следовательно, изображением.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void textBoxNumber_TextChanged(object sender, EventArgs e)
        {
            _command.Star.Number = (int)textBoxNumber.RealValue;
            _command.Star.VarNumber.Value = textBoxNumber.Value.Variable;
            _document.Redraw();
        }

		/// <summary>
		/// Обработчик события потери фокуса окошка ввода для параметра звезды в диалоге.
		/// Передача звезде переменной, если она была создана, и перерисовка.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxNumber_LostFocus(object sender, EventArgs e)
		{
            _command.Star.VarNumber.Value = textBoxNumber.Value.Variable;
            _document.Redraw();	
		}

		/// <summary>
		/// Обработчик события изменения параметра звезды в диалоге.
		/// Синхронизация с объектом звезды и, следовательно, изображением.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void textBoxR1_TextChanged(object sender, EventArgs e)
        {
            _command.Star.R1 = textBoxR1.RealValue;
            _command.Star.VarR1.Value = textBoxR1.Value.Variable;
            _document.Redraw();
        }

		/// <summary>
		/// Обработчик события потери фокуса окошка ввода для параметра звезды в диалоге.
		/// Передача звезде переменной, если она была создана, и перерисовка.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxR1_LostFocus(object sender, EventArgs e)
		{
            _command.Star.VarR1.Value = textBoxR1.Value.Variable;
            _document.Redraw();
		}

		/// <summary>
		/// Обработчик события изменения параметра звезды в диалоге.
		/// Синхронизация с объектом звезды и, следовательно, изображением.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void textBoxR2_TextChanged(object sender, EventArgs e)
        {
            _command.Star.R2 = textBoxR2.RealValue;
            _command.Star.VarR2.Value = textBoxR2.Value.Variable;
            _document.Redraw();
        }

		/// <summary>
		/// Обработчик события потери фокуса окошка ввода для параметра звезды в диалоге.
		/// Передача звезде переменной, если она была создана, и перерисовка.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxR2_LostFocus(object sender, EventArgs e)
		{
            _command.Star.VarR2.Value = textBoxR2.Value.Variable;
            _document.Redraw();
		}

		/// <summary>
		/// Обработчик события изменения параметра звезды в диалоге.
		/// Синхронизация с объектом звезды и, следовательно, изображением.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void textBoxCenterX_TextChanged(object sender, EventArgs e)
        {
            _command.Star.X = textBoxCenterX.RealValue;
            _command.Star.VarX.Value = textBoxCenterX.Value.Variable;
            _document.Redraw();
        }

		/// <summary>
		/// Обработчик события потери фокуса окошка ввода для параметра звезды в диалоге.
		/// Передача звезде переменной, если она была создана, и перерисовка.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxCenterX_LostFocus(object sender, EventArgs e)
		{
            _command.Star.VarX.Value = textBoxCenterX.Value.Variable;
            _document.Redraw();
		}

		/// <summary>
		/// Обработчик события изменения параметра звезды в диалоге.
		/// Синхронизация с объектом звезды и, следовательно, изображением.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void textBoxCenterY_TextChanged(object sender, EventArgs e)
        {
            _command.Star.Y = textBoxCenterY.RealValue;
            _command.Star.VarY.Value = textBoxCenterY.Value.Variable;
            _document.Redraw();
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
            //и перерисовываем звезду					
            _command.Star.VarY.Value = textBoxCenterY.Value.Variable;
            _document.Redraw();
		}

		/// <summary>
		/// Обработчик события отрисовки звезды в объекте звезды.
		/// Синхронизация с диалогом свойств звезды. (Синхронизация "в обратную сторону".)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void OnShowCommandCursor(object sender, TFlex.Command.MouseEventArgs e)
        {
			//Сюда мы попадаем после метода StarObject.DrawCursor(), но до метода StarObject.Draw()

            if (_command.Star != null)
            {
				if (this.textBoxR1.RealValue != _command.Star.R1)
					if (_command.Star.VarR1.Value != null) this.textBoxR1.Text = _command.Star.VarR1.Value.Name;
					else this.textBoxR1.RealValue = _command.Star.R1;

				if (this.textBoxR2.RealValue != _command.Star.R2)
					if (_command.Star.VarR2.Value != null) this.textBoxR2.Text = _command.Star.VarR2.Value.Name;
					else this.textBoxR2.RealValue = _command.Star.R2;

				if (this.textBoxCenterX.RealValue != _command.Star.X)
					if (_command.Star.VarX.Value != null) this.textBoxCenterX.Text = _command.Star.VarX.Value.Name;
					else this.textBoxCenterX.RealValue = _command.Star.X;

				if (this.textBoxCenterY.RealValue != _command.Star.Y)
					if (_command.Star.VarY.Value != null) this.textBoxCenterY.Text = _command.Star.VarY.Value.Name;
					else this.textBoxCenterY.RealValue = _command.Star.Y;

				if (this.textBoxNumber.RealValue != _command.Star.Number)
					if (_command.Star.VarNumber.Value != null) this.textBoxNumber.Text = _command.Star.VarNumber.Value.Name;
					else this.textBoxNumber.RealValue = _command.Star.Number;
            }
        }
    }
}
