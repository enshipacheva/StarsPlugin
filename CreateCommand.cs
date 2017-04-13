using System;
using System.Runtime.InteropServices;
using TFlex;
using TFlex.Model;
using TFlex.Model.Model2D;
using TFlex.Drawing;
using TFlex.Command;

namespace Stars
{
    /// <summary>
    /// ����� ��������� ���������������� ������� �������� ������ 
    /// </summary>
	class CreateCommand : StarCommand
	{		
		/// <summary>
        /// �����������. �������������� ����������� �������.
        /// </summary>
        /// <param name="plugin"></param>
		/// <param name="document"></param>
		public CreateCommand(Plugin plugin, Document document)
            : base(plugin)
        {
			_document = document;

            /// �������������
			this.Initialize += new InitializeEventHandler(Command_Initialize);
			
			///�����
			this.Exit += new ExitEventHandler(Command_Exit);
        }

        /// <summary>
        /// ������������� �������
        /// </summary>
		public override int ID {get{return (int)Commands.Create;}}

        /// <summary>
        /// ������������� �������:
        /// ������� ������ �� ���������� �� ���������
        /// �������� automenu � ��������� ���� �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void Command_Initialize(object sender, InitializeEventArgs e)
		{
            //������������ ����������� ������ (ProxyObject2D)
            Star = new StarObject();
            //������������� ��������� ���������
            State = InputState.modePoint;
            //������ ������ ��������
            UpdateAutomenu();
            //������ ���� ������� (�������� ����� �������� ������) � ����� ���������� ������� �� ������ ����� ����
            CreatePropertiesWindow();
            this.PropertiesWindow.HeaderButtonPressed += new PropetiesWindowHeaderButtonPressedEventHandler(propertiesWindow_HeaderButtonPressed);
		}

        /// <summary>
        /// �������� ����� ������ � ���������
        /// </summary>
        /// <param name="document"></param>
		protected override void CreateNewObject(Document document)
		{
            //����������� ������� ���� �������� �� ��������� ������
			document.BeginChanges("������� ������");
			
            //������ ����� ������� ������ �� ������ ������. �� �������� ������ 
			//(������ ����� ��������� ������ � �������� �������� ������� �� ��������)
			ExternalObject obj = new ExternalObject(Star, document, Owner);
			StarObject proxyStar = obj.VolatileObject as StarObject;
			proxyStar.ApplyReferences(Star);

            //���� ExternalObject-� ���������� - ��� ���� ��� ProxyObject2D (A.K.A. ����������� �������, VolatileObject)
            obj.Color = Star.Color;

			Star.RevokeReferences();

            //����������� ������� ���� ��������
			document.EndChanges();
            
            //� �������� ������ ���� � ����
            StarsPlugin.UpdateFloatingWindow(document);
		}

        /// <summary>
        /// ��������� ������� ������ � �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void propertiesWindow_HeaderButtonPressed(object sender, PropetiesWindowHeaderButtonPressedEventArgs e)
        {
			switch (e.Button)
			{
				case PropertiesWindowHeaderButton.Cancel:
					Terminate();
					break;
				case PropertiesWindowHeaderButton.OK:
					StarApproved(_document);
	
					break;
			}
        }


		/// <summary>
		/// ����������� ���������� ������� �������
		/// </summary>
		/// <param name="document"></param>
		protected override void StarApproved(Document document)
		{
			//������ ����� ������, ��� ����� ����� ���-�� ����������, �.�. ������ Star ����������
			CreateNewObject(document);

			//��������� � ��������� ���������
			State = InputState.modePoint;
			this.PropertiesWindow.EnableHeaderButton(PropertiesWindowHeaderButton.OK, false);
		}













	
	}
}
