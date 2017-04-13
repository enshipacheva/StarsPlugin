﻿using System;
using System.Collections.Generic;
using TFlex;
using TFlex.Model;
using TFlex.Model.Model2D;
using TFlex.Drawing;
using TFlex.Command;

namespace Stars
{
	/// <summary>
	/// Абстрактный класс, является базовым для классов <see cref="EditCommand"/> и <see cref="CreateCommand">.
	/// Реализует некоторый общий функционал команд, однако не предусматривает создания экземпляров объектов.
	/// </summary>
    internal abstract class StarCommand : PluginCommand
	{
        /// <summary>
		/// расчёт гипотенузы по катетам
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
        private static double CalculateHypotenuse(double x, double y)
        {
            return Math.Sqrt(x * x + y * y);
        }

        /// <summary>
		/// Расчёт угла по смещениям
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
        private static double CalculateAngle(double x, double y)
        {
            if (x == 0 && y == 0)
                return 0;

            double al = Math.Asin(y / CalculateHypotenuse(x, y));
            if (x < 0)
                al = Math.PI - al;
            else if (y < 0)
                al = Math.PI + al + Math.PI;
            return al;
        }

		/// <summary>
		/// Расчёт угла по смещениям в градусах
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
        private static double CalculateAngleGradus(double x, double y) { return CalculateAngle(x, y) * 180 / Math.PI; }

        /// <summary>
		/// Создаваемый объект звезды
		/// </summary>
        internal StarObject Star { get; set; }

        // 
		/// <summary>
		/// Свойство, указывающее на то, нужно ли заливать звезду
		/// </summary>
        internal static bool Fill { get; set; }

        /// <summary>
		/// Количество лучей по умолчанию
		/// </summary>
        internal static int Number { get; set; }

		/// <summary>
		/// Перечисление состояний, которые бывают в команде
		/// </summary>
        public enum InputState {
            modePoint,   //Ничего не выбрано
            modeR1,      //Выбран центр (выбирается первый радиус)
            modeR2,      //Выбран центр и радиус (выбирается второй радиус)
            modeWait,    //Выбраны центр и оба радиуса, ожидаем подтверждения кнопкой "OK" или отката по нажатию правой кнопки мыши или кнопки "Cancel"
        };

        /// <summary>
		/// Текущее состояние команды
		/// </summary>"
        public InputState State { get; set; }

		
		/// <summary>
		/// храним документ, для которого вызвана команда
		/// </summary>
		protected Document _document;

		/// <summary>
		/// Для внутреннего использования.
		/// </summary>
		/// <remarks>
		/// Храним ссылку на создаваемую форму свойств
		/// </remarks>
        private PropertiesWindowForm _propertiesControlForm;

		/// <summary>
		/// Для внутреннего использования.
		/// </summary>
		/// <remarks>
		/// Храним ссылку на создаваемую форму дополнительных свойств
		/// </remarks>
        private PropertiesWindowForm _propertiesControlExForm;

        /// <summary>
		/// Конструктор. Регистрируются обработчики событий
		/// </summary>
		/// <param name="App"></param>
		public StarCommand(Plugin App) : base(App)
		{
            //Нажата кнопка на клавиатуре или в автоменю
            this.KeyPressed += new KeyEventHandler(Command_KeyPressed);

			//Перемещение мыши
			this.ShowCursor += new MouseEventHandler(Command_ShowCursor);

			//Выход из команды - вызывается после Terminate
			this.Exit += new ExitEventHandler(Command_Exit);

			//Выбор объекта
			this.Select += new SelectEventHandler(CreateCommand_Select);
		}
	  
		/// <summary>
		/// Для внутреннего использования.
		/// </summary>
		/// <remarks>
		/// Метод заполняет автоменю кнопками
		/// </remarks>
		protected void UpdateAutomenu()
		{
			List<Button> list1 = new List<Button>();
			List<Button> list2 = new List<Button>();

            for (int i = 0; i < 8; ++i)
            {
                list2.Add(new SingleButton((int)AutomenuCommands.Number + i,
                    i != 7 ? KeyCode.key3 + i : KeyCode.key0,
                    Star.Number == i + 3 ? Button.Style.Checked : Button.Style.Default));
            }

			list1.Add(new DefaultButton(DefaultButton.Kind.OK, KeyCode.keyEND));
            list1.Add(new DefaultButton(DefaultButton.Kind.Parameters));
            list1.Add(new GroupButton("Количество лучей",
				list2.ToArray() as Button[],
                Star.Number >= 3 && Star.Number <= 10 ? Star.Number - 3 : 2));
            list1.Add(new SingleButton((int)AutomenuCommands.Fill, KeyCode.keyF,
                Star.Fill ? Button.Style.Checked : Button.Style.Default));
            list1.Add(new SeparatorButton());
            list1.Add(new DefaultButton(State == InputState.modePoint ?
                DefaultButton.Kind.Exit : DefaultButton.Kind.Cancel));

            Automenu = new Automenu(list1.ToArray() as Button[]);
		}

        /// <summary>
		/// Для внутреннего использования.
		/// </summary>
		/// <remarks>
		/// Метод создаёт окно свойств и в нём две формы ввода
		/// </remarks>
        protected void CreatePropertiesWindow()
        {
            //Инициализируем диалог свойств
            //В нём 2 закладки
            PropertiesWindow propertiesWindow = new PropertiesWindow();
            propertiesWindow.Caption = "Свойства звезды";
            StarPropertiesControl control1 = new StarPropertiesControl(this, _document);
            PropertiesWindowForm form1 = new PropertiesWindowForm(control1);
            form1.Caption = "Основные свойства";
            this._propertiesControlForm = form1;
            propertiesWindow.AppendForm(form1);

            //Вторая закладка
            StarPropertiesControlEx control2 = new StarPropertiesControlEx(this, _document);
            PropertiesWindowForm form2 = new PropertiesWindowForm(control2);
            form2.Caption = "Дополнительные свойства";
            this._propertiesControlExForm = form2;
            form2.Visible = false;
            propertiesWindow.AppendForm(form2);

            //Тип заголовка в окне свойств
            propertiesWindow.PropertiesHeaderType = PropertiesWindow.HeaderType.OkPreviewCancel;

            propertiesWindow.EnableHeaderButton(PropertiesWindowHeaderButton.OK, false);
            propertiesWindow.EnableHeaderButton(PropertiesWindowHeaderButton.Preview, false);
            
            this.PropertiesWindow = propertiesWindow;

            //обработчик нажатий на клавиши проставляется в классах-наследниках
        
        }

		/// <summary>
		/// Для внутреннего использования.
		/// </summary>
		/// <remarks>
		/// Обработчик события выхода из команды, вызывается после Terminate.
		/// </remarks>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void Command_Exit(object sender, ExitEventArgs e) {}

		/// <summary>
		/// Для внутреннего использования.
		/// </summary>
		/// <remarks>
		/// Обработчик нажатия кнопки клавиатуры или мыши.
		/// </remarks>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        protected void Command_KeyPressed(object sender, KeyEventArgs e)
		{
            //Обработка кнопок, нажатых в команде
			switch (e.Code)
			{
                //Enter на клавиатуре обычно делает то же самое, что и левая кнопка мыши
				case KeyCode.keyENTER:
				case KeyCode.LMB:
				{
                    switch (State)
					{
						case InputState.modePoint:
						{
                            //Если выбран узел, то запоминаем его
                            Star.FixingNode.Value = e.SelectedObject as Node;
							if (Star.FixingNode.Value != null)
							{
								Star.X = Star.FixingNode.Value.AbsX;
								Star.Y = Star.FixingNode.Value.AbsY;
							}
                                //Если нет, то запоминаем координаты
							else
							{
								Star.X = e.GX;
								Star.Y = e.GY;
							}
                            //И переходим в другое состояние
							State = InputState.modeR1;
							
							break;
						}
                        //Выбираем первый радиус
						case InputState.modeR1:
						{
                            double X = e.DX, Y = e.DY;
                            //Вычисляем радиус и угол
							Star.R2 = CalculateHypotenuse(X - Star.X, Y - Star.Y);
							Star.Angle = CalculateAngleGradus(Y - Star.X, X - Star.Y);
							State = InputState.modeR2;

							break;
						}
                        //Выбираем второй радиус
                        case InputState.modeR2:
						{
                            double X = e.DX, Y = e.DY;

                            Star.R1 = CalculateHypotenuse(X - Star.X, Y - Star.Y);
							Star.Angle = CalculateAngleGradus(X - Star.X, Y - Star.Y);
							e.Document.Selection.DeselectAll();

                            //делаем доступной кнопку подтверждения (OK, в GUI - зел. галочка)
                            //если она будет нажата, произойдёт создание новой звезды и переход в начальное состояние
                            this.PropertiesWindow.EnableHeaderButton(PropertiesWindowHeaderButton.OK, true);
                            State = InputState.modeWait;

							break;
						}
                        case InputState.modeWait:
                        {
                            break;
                        }
					}

					UpdateAutomenu();

					break;
				}

				case KeyCode.keyESCAPE:
				case KeyCode.RMB:
					switch (State)
					{
						case InputState.modePoint:
							Terminate();
							break;

						case InputState.modeR1:
							State = InputState.modePoint;
							break;

						case InputState.modeR2:
							State = InputState.modeR1;
							break;

                        case InputState.modeWait:
                            State = InputState.modeR2;
                            this.PropertiesWindow.EnableHeaderButton(PropertiesWindowHeaderButton.OK, false);
                            break;
					}
					UpdateAutomenu();
					break;
                //Вызываем диалог свойств звезды
				case KeyCode.keyP:
				{
					StarProperties dialog = new StarProperties(Star);

					if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
						Star.Assign(dialog.Star);

					_document.Redraw();
					dialog.Dispose();
					UpdateAutomenu();

					break;
				}
                //Переключаем режим заливки
				case KeyCode.keyF:
					Star.Fill = !Star.Fill;
					UpdateAutomenu();
					break;
                //По цифровым клавишам устанавливаем соответствующее количество лучей
				case KeyCode.key3:
				case KeyCode.key4:
				case KeyCode.key5:
				case KeyCode.key6:
				case KeyCode.key7:
				case KeyCode.key8:
				case KeyCode.key9:
				case KeyCode.key0:
				{
					int number = e.Code != KeyCode.key0 ? e.Code - KeyCode.key3 + 3 : 10;
					if (Star.Number == number)
					{
						if (++Star.Number > 10)
							Star.Number = 3;
					}
					else
						Star.Number = number;
					UpdateAutomenu();
					break;
				}
				case KeyCode.keyEND:
				{
					if (State == InputState.modeWait)
						//обрабатываем подтверждение звезды
						StarApproved(_document);

					break;
				}




			}
		}
        

		/// <summary>
		/// Для внутреннего использования.
		/// </summary>
		/// <remarks>
		/// Метод рисует динамический курсор звезды - рассчитывает её параметры и делегирует отрисовку в StarObject.Draw(graphics)
		/// </remarks>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        protected void Command_ShowCursor(object sender, MouseEventArgs e)
        {
			Graphics graphics = e.Graphics;
           
			switch (State)
			{
				case InputState.modePoint:
				{
                    Star.X = e.GX;
					Star.Y = e.GY;
                    break;
				}
				case InputState.modeR1:
				{
                    double X = e.DX, Y = e.DY;
                    Star.R2 = CalculateHypotenuse(X - Star.X, Y - Star.Y);
					Star.Angle = CalculateAngleGradus(X - Star.X, Y - Star.Y);
                    break;
				}
				case InputState.modeR2:
				{
                    double X = e.DX, Y = e.DY;
                    Star.R1 = CalculateHypotenuse(X - Star.X, Y - Star.Y);
                    Star.Angle = CalculateAngleGradus(X - Star.X, Y - Star.Y);
                    break;
				}
                case InputState.modeWait:
                {
                    break;
                }
			}

            //делегируем отрисовку объекту звезды - режим рисования УЖЕ ВКЛЮЧЕН
            Star.DrawCursor(graphics);
        }

		//Обработчик события выделения объекта - нужен для привязки звезды к узлу
		private void CreateCommand_Select(object sender, SelectEventArgs e)
		{
			switch (e.GroupType)
			{
				case ObjectType.Undefined:
					e.Result = true;
					break;
				//Можем выбирать узлы
				case ObjectType.Node:
					e.Result = true;
					break;
			}
		}



		/// <summary>
        /// Создание новой звезды в документе, чисто виртуальный метод
        /// </summary>
        /// <param name="document"></param>
		protected virtual void CreateNewObject(Document document) { }


		/// <summary>
		/// Виртуальный метод для того, чтобы команды могли по-разному обрабатывать нажатие галочки
		/// </summary>
		/// <param name="document"></param>
		protected virtual void StarApproved(Document document) { }





    }
}
