using System;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Reflection;

using TFlex;
using TFlex.Model;
using TFlex.Model.Model2D;
using TFlex.Command;
using TFlex.Drawing;

//������ ���� ��������� ���������������� ����������.
//�������������� ������� ����������, ������ ������, ������ ����, ������ � ��������,
//��������� ����, ����������� ������� �� ����������.

namespace Stars
{
    /// <summary>
	/// ��� �������� ���������� ���������� ����� �����, ���������� �� PluginFactory
	/// </summary>
	public class Factory : PluginFactory
	{
        /// <summary>
        /// ���������� ����� �������������� ������ ����� ��� �������� �������
        /// </summary>
		public override Plugin CreateInstance()
		{
			return new StarsPlugin(this);
		}

        /// <summary>
		/// ���������� GUID ����������. �� ������ ���� ����������� ������ � ������ ����������
		/// </summary>
        public override Guid ID
		{
			get
			{
				return new Guid("{32F0C0D7-F516-4b69-837F-F146265981EB}");
			}
		}

        /// <summary>
		/// ��� ����������
		/// </summary>
		public override string Name
		{
			get
			{
				return "T-FLEX �����"; 
			}
		}
	};

    /// <summary>
	/// ������� ���������� � ������ � ������� ����
	/// </summary>
	enum Commands
	{
		Create = 1, //������� �������� ������
		ShowWindow, //��������/�������� ��������� ����
	};

	/// <summary>
	/// ������� �������� � ����������� ����
	/// </summary>
	enum ObjectCommands
	{
        Fill = Commands.ShowWindow + 1,
	};

	/// <summary>
	/// ������� ��������
	/// </summary>
	enum AutomenuCommands
	{
		Number,
		Fill = Number + 8,
	};

	/// <summary>
	/// ID ������ ��������, ������������ �����������
	/// </summary>
	enum ObjectTypeIcons
	{
		StarObject
	};

	/// <summary>
	/// ����� ����������. ������������ �������, ����������� �������. 
	/// ������������ �������, ���������� �� ��������� ����.
	/// </summary>
	public class StarsPlugin : Plugin
	{
		/// <summary>
		/// ���� �������� 1�� �� ������� ��������,
		/// ����� ��� �������������� ������ BeginInvoke()
		/// </summary>
		private bool firstObjectDeleted;

		/// <summary>
		/// �����������
		/// </summary>
		/// <param name="factory"></param>
		public StarsPlugin(Factory factory) : base(factory)
		{
			firstObjectDeleted = false;
        }

       	/// <summary>
		/// ��� ��������� ����, � ������� ����� ���������� ������ ���� � ���������
		/// </summary>
        private static PluginFloatingWindow FloatingWindow;

        /// <summary>
		/// ��� ��� ���������� �����, ������������ ������ ����
		/// </summary>
        private static StarsFloatingWindow FloatingWindowClient;

		/// <summary>
		/// �������� ������
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		System.Drawing.Icon LoadIconResource(string name)
		{
			System.IO.Stream stream = GetType().Assembly.
				GetManifestResourceStream("Stars.Resource_Files." + name + ".ico");
			return new System.Drawing.Icon(stream);
		}

        
		/// <summary>
		///������ ������������� ���������� � ������ �������� ����������.
        ///� ������ ���������� ����� ������ ������ �� �����. ��� ������������� �������� � OnCreateTools
		/// </summary>
		protected override void OnInitialize()
		{
			base.OnInitialize();

            //������������ ������ ��� ������ � ��������
            int[] cmdIDs = new int[8];

            for (int i = 0; i < 8; ++i)
            {
                // ��� ��� ������ ���������� ����� ����� ��������
                String hint = String.Format(i < 2 ? "{0} ����" : "{0} �����", i + 3);
                cmdIDs[i] = (int)AutomenuCommands.Number + i;
                RegisterAutomenuCommand(cmdIDs[i], hint, LoadIconResource("Fill"));
            }

            RegisterAutomenuCommand((int)AutomenuCommands.Fill, "�������", LoadIconResource("Fill"));

		}

		/// <summary>
		/// ���� ����� ���������� � ��� ������, ����� ������� ���������������� �������,
		/// ������� ������, �������� ������ ����
		/// </summary>
		protected override void OnCreateTools()
		{
			base.OnCreateTools();

            RegisterCommand((int)Commands.Create, "�������� ����", LoadIconResource("CreateStars"), LoadIconResource("CreateStars")); // ������������ ������� ��������
            RegisterCommand((int)Commands.ShowWindow, "�������� ����", LoadIconResource("StarsWindow"), LoadIconResource("StarsWindow")); // ������������ ������� ������ ����

			//������������ ������� ������������ ���� ������� ������
            RegisterObjectCommand((int)ObjectCommands.Fill, "�������", LoadIconResource("Fill"), LoadIconResource("Fill")); // ������������ ������� ������� ��� ������������ ����
			
			//������������ ������ ������
            RegisterObjectTypeIcon((int)ObjectTypeIcons.StarObject, LoadIconResource("StarObject"));

            //��������� ������ � ��������� ����
            TFlex.Menu submenu = new TFlex.Menu();
            submenu.CreatePopup();
			submenu.Append((int)Commands.Create, "&�������", this);
			submenu.Append((int)Commands.ShowWindow, "&�������� ����", this);
            TFlex.Application.Window.InsertPluginSubMenu("�����",submenu, MainWindow.InsertMenuPosition.PluginSamples, this);

            //������ ������ � �������� "�����"
            int[] cmdIDs = new int[] { (int)Commands.Create,(int)Commands.ShowWindow };
			CreateToolbar("�����", cmdIDs);
			//CreateMainBarPanel("�����", cmdIDs, new Guid("C418BC60-5890-42E9-966C-AF86B6BB675D"), true);

            //������ ��������� ���� �� ������� ����
            FloatingWindow = CreateFloatingWindow(0, "�����");
            FloatingWindow.Caption = "�����"; // ��� ��������
            FloatingWindow.Icon = LoadIconResource("StarObject");//��� ������
            FloatingWindow.Visible = false; //���� �����

			//�� ������ ���� ������ ��� ���������, ����� �������� ��� ��� ������ � ������ (��� ����� ��� AttachPlugin() ���������), 
			//������ ���������� ������ � �������� ���������
			if (TFlex.Application.ActiveDocument != null)
				TFlex.Application.ActiveDocument.AttachPlugin(this);
		}


		/// <summary>
		/// ��������� ������ �� ������ � �������� ����
		/// </summary>
		/// <param name="document"></param>
		/// <param name="id"></param>
		protected override void OnCommand(Document document, int id)
		{
			switch ((Commands)id)
			{
				default:
					base.OnCommand(document, id);
					break;
				case Commands.Create://������� �������� ������
                    FloatingWindow.Visible = true; //����� ���������� ��������� ����
                    CreateCommand �ommand = new CreateCommand(this, document);
                    �ommand.Run(document.ActiveView);
					break;
				case Commands.ShowWindow: //������� ������ ���������� ����
                    FloatingWindow.Visible = !FloatingWindow.Visible;
					break;
			}
		}

		/// <summary>
		/// ����� ����� ����������� ������� � ������������� �������
		/// </summary>
		/// <param name="cmdUI"></param>
		protected override void OnUpdateCommand(CommandUI cmdUI)
		{
			if (cmdUI == null)
				return;

			if (cmdUI.Document == null)
			{
				cmdUI.Enable(false);
				return;
			}
            if (cmdUI.ID == (int)Commands.ShowWindow)
            {
                cmdUI.SetCheck(FloatingWindow.Visible);
            }

			cmdUI.Enable();
		}

        
		/// <summary>
		/// ��� ������� ������ ��������� ������� ����������, ��������������� ���� �������
        /// ���� ���������� ����� ����� �������, �� ����� �������� ����� TypeID
		/// </summary>
		/// <param name="Document"></param>
		/// <param name="OwnerHandle"></param>
		/// <param name="TypeID"></param>
		/// <returns></returns>
		protected override ProxyObject CreateObject(Document Document,IntPtr OwnerHandle, int TypeID)
		{
			switch (TypeID)
			{
				case 1:
					return new StarObject(OwnerHandle);
			}
			return null;
        }

		/// <summary>
		/// ������������� �� ��������� ������� ��� �������� ������ �������
		/// </summary>
		/// <param name="args"></param>
		protected override void ObjectCreatedEventHandler(ObjectEventArgs args)
		{			
			ExternalObject obj;	
			
			if(args.Object.IsKindOf(ObjectType.External))
			{
				obj = args.Object as ExternalObject;
				if(obj.ConstObject.OwnerApp == this)
				FloatingWindowClient.DelayedUpdate(args.Document, ref firstObjectDeleted);
			}
		}


        /// <summary>
		/// ������������� �� ��������� ������� ��� �������� ������ ���������
		/// </summary>
		/// <param name="args"></param>
        protected override void NewDocumentCreatedEventHandler(DocumentEventArgs args)
        {
            //AttachPlugin ����� ������� �����������, ����� �� ������� ��������� �� ����� ��������� ����������� � ��������
            args.Document.AttachPlugin(this);
        }

		/// <summary>
		/// ������������� �� ��������� ������� ����� �������� ���������
		/// </summary>
		/// <param name="args"></param>
		protected override void DocumentOpenEventHandler(DocumentEventArgs args)
		{
			//������� ��������� ����� �������� ���������
			args.Document.AttachPlugin(this);
			UpdateFloatingWindow(args.Document);
		}

		/// <summary>
		/// ������������� �� ��������� ������� ��� �������� ���������
		/// </summary>
		/// <param name="args"></param>
        protected override void ClosingDocumentEventHandler(DocumentEventArgs args)
        {
            //������ ������� ���������� ��� �������� ���������
            //����� ��� ������� �������� ���� �� ������� ����
            UpdateFloatingWindow(null);
        }

		/// <summary>
		/// ������������� �� ��������� ������� ��� ����������� ���� ���������
		/// </summary>
		/// <param name="args"></param>
        protected override void ViewActivatedEventHandler(ViewEventArgs args) 
        { 
            //������ ������� ���������� ��� ����������� ���� ���������
            //������� ������ ����
			UpdateFloatingWindow(args.Document);
        }

		/// <summary>
		///����� �� ������ ������� ��������� ����� ���������� ���� ��� ��� ��
        ///��������� ������� ��� � ������ CreateFloatingWindow(0, "�����");
        ///���� ���� ������ ���� ���������, �� ����� id ����� ��������������� ������� ��������� � ������
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
        protected override System.Windows.Forms.Control CreateFloatingWindowControl(uint id)
        {
            FloatingWindowClient = new Stars.StarsFloatingWindow(this);
            return FloatingWindowClient;
        }


		/// <summary>
		/// ������������� �� �������� ������� � ������������ ���
		/// </summary>
		/// <param name="args"></param>
		protected override void ObjectDeletedEventHandler(ObjectEventArgs args)
		{
			if (!firstObjectDeleted) firstObjectDeleted = true;

			FloatingWindowClient.DelayedUpdate(args.Document, ref firstObjectDeleted);
		}		
		
		/// <summary>
		/// ���������� ���������� ������ ����
		/// </summary>
		/// <param name="doc"></param>
        public static void UpdateFloatingWindow(TFlex.Model.Document doc)
        {
            FloatingWindowClient.UpdateContents(doc);
        }

        //������ ��� ��������/�������
        //-------------------------------------------------------------------------------------------------------------------       

		/// <summary>
		/// ��� ������� ���������� ��� ������ ������� �������� �������� ���������
		/// </summary>
		/// <param name="args"></param>
		protected override void ShowingExportDialogEventHandler(ShowingImportExportDialogEventArgs args)
		{
			args.AddFilter("����� ���� (*.str)|*.str|");
		}

		/// <summary>
		/// ��� ������� ���������� ���� ������������ ������ ��� ��������, ����������� � ���������� �������
		/// </summary>
		/// <param name="args"></param>
		protected override void ExportDialogShownEventHandler(ImportExportDialogShownEventArgs args)
		{
				this.ExportToXml(args.FilePath, args);
		}

		/// <summary>
		/// ��� ������� ���������� ��� ������ ������� ������� �������� ���������
		/// </summary>
		/// <param name="args"></param>
		protected override void ShowingImportDialogEventHandler(ShowingImportExportDialogEventArgs args)
		{
			args.AddFilter("����� ���� (*.str)|*.str|");
		}

		/// <summary>
		/// ��� ������� ���������� ���� ������������ ������ ��� �������, ����������� � ���������� �������
		/// </summary>
		/// <param name="args"></param>
		protected override void ImportDialogShownEventHandler(ImportExportDialogShownEventArgs args)
		{
				this.ImportFromXml(args.FilePath, args);
		}

		/// <summary>
		/// ��������������� ���������� ������ <see cref="StarObjectCollection"> �� .xml-�����
		/// </summary>
		/// <param name="_fileName"></param>
		/// <param name="args"></param>
        public void ImportFromXml(string _fileName, ImportExportDialogShownEventArgs args)
        {
              string FileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), _fileName);

              XmlSerializer[] serializers = XmlSerializer.FromTypes(new Type[] {
                typeof(StarObjectCollection),
              });

              XmlSerializer ser = serializers[0];

              if (!File.Exists(FileName))
                  return;

            StarObjectCollection result = null;
            using (FileStream fs = new FileStream(FileName, System.IO.FileMode.Open, FileAccess.Read))
            {
                result = ser.Deserialize(fs) as StarObjectCollection;
                fs.Close();
            }

            if (result == null || result.Count == 0)
                return;

            Document doc = args.Document;

            //����������� ������� ���� �������� �� ��������� ������
            doc.BeginChanges("������� ������");

            //�������� ������� TFlex (ExternalObject) ��� ������ �� ��������������� ����
            foreach (StarObject star in result)
            {
                //������ ����� ������� ������ �� ������ ������. �� �������� ������
                ExternalObject obj = new ExternalObject(star, doc, this);

                //��� ��� �������� �� ��� ������ ���������� (������-) ������ ExternalObject-�, � �� �������� ������ Star
                StarObject newStar = obj.VolatileObject as StarObject;
                //���� �������� (����� ���� � �������)
                newStar.FixingNode.Value = null;
                //���� ExternalObject-� ���������� - ��� ���� ��� ProxyObject2D (A.K.A. ����������� �������, VolatileObject)
                obj.Color = newStar.Color;
            }

            //����������� ������� ���� ��������
            doc.EndChanges();
            //� �������� ������ ���� � ����
            StarsPlugin.UpdateFloatingWindow(doc);

        }

		/// <summary>
		/// ������������� ���������� ������ <see cref="StarObject"> � .xml-����
		/// </summary>
		/// <param name="_fileName"></param>
		/// <param name="args"></param>
        public void ExportToXml(string _fileName, ImportExportDialogShownEventArgs args)
        {
            //preparing XML serializer
            string FileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), _fileName);

            bool starHasBeenFound = false;
            StarObjectCollection collection = new StarObjectCollection();
            StarObject star = null;
            ExternalObject wrap;
            foreach(object obj in args.Document.Objects2D)
            {
                wrap = obj as ExternalObject;
                if (wrap != null)
                    star = wrap.ConstObject as StarObject;
                if (star != null)
                {
                    starHasBeenFound = true;
                    collection.Add(star);
                }
            }

            XmlSerializer[] serializers = XmlSerializer.FromTypes(new Type[] {
                typeof(StarObjectCollection),
            });

            XmlSerializerNamespaces xmlnsEmpty = new XmlSerializerNamespaces();
            xmlnsEmpty.Add(string.Empty, string.Empty);

            XmlSerializer ser = serializers[0];

            if (starHasBeenFound)
            {
                using (FileStream fs = new FileStream(FileName, System.IO.FileMode.Create))
                {

                    ser.Serialize(fs, collection, xmlnsEmpty);
                    fs.Close();
                }
            }
            else MessageBox.Show("� ������� ��������� ���� �� �������.", "���������", MessageBoxButtons.OK);

        }


    }
}