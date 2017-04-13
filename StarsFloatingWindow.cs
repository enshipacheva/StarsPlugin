using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using TFlex.Model;
using TFlex.Drawing;
using TFlex.Model.Model2D;

namespace Stars
{
	/// <summary>
	/// �����, ���������� ������� ������ UserControl-�, ������������� ������ ���� � ������� ���������
	/// ������ UserControl-�.
	/// </summary>
    public partial class StarsFloatingWindow : UserControl
    {
		public delegate void DelegatedUpdate(TFlex.Model.Document doc);
		private delegate void DelegatedShow(object sender, EventArgs e);

		private Document currentDocument;
		private StarsPlugin _plugin;

		/// <summary>
		/// ���������� ��������
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		private static double max(double a, double b)
		{
			return (a > b ? a : b);
		}


		/// <summary>
		/// �����������
		/// </summary>
        public StarsFloatingWindow(StarsPlugin plugin)
        {
            InitializeComponent();
            SizeChanged += new EventHandler(Sized);
			_plugin = plugin;
        }

		/// <summary>
		/// ����� ��������� �������� UserControl-�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
        protected void Sized(Object sender, EventArgs args)
        {
            System.Drawing.Rectangle rect = ClientRectangle;
			List1.Size = new System.Drawing.Size(rect.Size.Width - 4, rect.Size.Height - 4);
        }
		
		/// <summary>
		/// ����� ���������� ������ ����
		/// </summary>
		/// <param name="doc"></param>
        public void UpdateContents(TFlex.Model.Document doc)
        {
			ListViewItem currentItem;
			
			List1.Items.Clear();

            if (doc != null)
            {
                foreach (TFlex.Model.Model2D.ExternalObject extObject in doc.Externals)
                {
					if (extObject.ConstObject != null)
						if (extObject.ConstObject.OwnerApp.Equals(_plugin))
						{
							currentItem = new ListViewItem();
							currentItem.Text = extObject.DisplayName;
							currentItem.Tag = extObject.ID;
							List1.Items.Add(currentItem);
						}
                }
            }

			currentDocument = doc;
        }

		/// <summary>
		/// ���������� ���������� ������ ����
		/// </summary>
		/// <param name="doc"></param>
		/// <param name="firstObjectDeleted"></param>
		public void DelayedUpdate(TFlex.Model.Document doc, ref bool firstObjectDeleted)
		{
			DelegatedUpdate updateMethod = new DelegatedUpdate( UpdateContents );
			BeginInvoke(updateMethod, doc);
			firstObjectDeleted = false;
		}

		/// <summary>
		/// ���������� ������� ������� � ���������� ���� �� ����� "��������"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ShowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			StarObject star = null;
			TFlex.Model.Model2D.ExternalObject extObject = null;
			ModelObject obj = null;

			if ((List1.SelectedItems.Count > 0) && ((uint)List1.SelectedItems[0].Tag >= 0))
				obj = currentDocument.GetObjectByID((uint)List1.SelectedItems[0].Tag);

			//� ��������, ���������� ��� �������� �������, �.�. ������ ������ ���� ������� ��������, 
			//� ������ � ���� ������� ������ ������
			if (obj != null)
				extObject = obj as TFlex.Model.Model2D.ExternalObject;
			if (extObject != null)
				star = extObject.ConstObject as StarObject;
			if (star != null)
			{
				//���� ������ ��������� � ������� ��������������, ��������� ����� "��������" � ��������� �������, 
				//�.�. �� � ������ ������ ��� - ������ � ����� (���������������)
				if (extObject.Attributes.GetIntAttribute("Hidden") != 1)
				{
					//��� ���������� � ������ ������ ���� ������� ������������ ��������������
					TFlex.Drawing.Point topLeft = new TFlex.Drawing.Point(star.X - max(star.R1, star.R2), star.Y - max(star.R1, star.R2));
					TFlex.Drawing.Point bottomRight = new TFlex.Drawing.Point(star.X + max(star.R1, star.R2), star.Y + max(star.R1, star.R2));

					//���� �� ����� ���������
					foreach (TFlex.Model.View view in currentDocument.Views)
					{
						//��� ������� ���� �������� ��������, ���������� ��� ������ � ������ ����������� ������
						View2D v2d = view as View2D;
						view.Page = extObject.Page;
						if (v2d != null) v2d.ZoomRectangle = new TFlex.Drawing.Rectangle(topLeft, bottomRight);
					}
				}
			}

		}

		/// <summary>
		/// ���������� ������� ������� � ���������� ���� �� ����� "�������������"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EditToolStripMenuItem_Click(object sender, EventArgs e)
		{
			StarObject star = null;
			TFlex.Model.Model2D.ExternalObject extObject = null;
			ModelObject obj = null;

			if ((List1.SelectedItems.Count > 0) && ((uint)List1.SelectedItems[0].Tag >= 0))
				obj = currentDocument.GetObjectByID((uint)List1.SelectedItems[0].Tag);

			if (obj != null)
				extObject = obj as TFlex.Model.Model2D.ExternalObject;
			if (extObject != null)
				star = extObject.ConstObject as StarObject;
			if (star != null) //�������� ������
			{
				ExternalObject selectedOwner = star.Owner2D;
				EditCommand command = new EditCommand(star.OwnerApp, selectedOwner, currentDocument);
				currentDocument.Selection.DeselectAll();
				command.Run(currentDocument.ActiveView);
			}
		}

		/// <summary>
		/// ���������� ������� ������� � ���������� ���� �� ����� "�������"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//����� ������ �� ID, ����������� �� ������ ����������� ��-�� ������
			TFlex.Model.Model2D.ExternalObject extObject = null;
			ModelObject obj = null;

			if ((List1.SelectedItems.Count > 0) && ((uint)List1.SelectedItems[0].Tag >= 0))
				obj = currentDocument.GetObjectByID((uint)List1.SelectedItems[0].Tag);

			//� ��������, ���������� ��� �������� �������, �.�. ������ ������ ���� ������� ��������, 
			//� ������ � ���� ������� ������ ������
			if (obj != null)
				extObject = obj as TFlex.Model.Model2D.ExternalObject;
			if (extObject != null)
				currentDocument.DeleteObjects(new ObjectArray(extObject), new DeleteOptions());
		}

		/// <summary>
		/// ���������� ������� ������ ���� (�������� ������� ����):
		/// ���� ������ ������ ������� ��� ����� �� ��-��� ListView, �������� ����������� ����,
		/// ���� �� ��� ������ ���-�� ��� - ��������� ���
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void List1_MouseUp(object sender, MouseEventArgs e)
		{
			//�������� ����, ������ ���� ��� ���� ���
			if (e.Button == MouseButtons.Right)
			{
				//�������� ����, �� ������� ��� ����
				ListViewItem currentItem = List1.GetItemAt(e.X, e.Y);
				if (currentItem != null)
					this.List1.ContextMenuStrip = contextMenuStrip1;
				else
					this.List1.ContextMenuStrip = null;
			}
		}

		/// <summary>
		/// ���������� ������� ������� ������� ����: 
		/// ���������� ��������� ������������ ���� � false,
		/// ��� ����������������� ��� ��� � ����������� �� ����� ������
		/// ��� ��� �������� ������� ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void List1_MouseDown(object sender, MouseEventArgs e)
		{
			this.List1.ContextMenuStrip = null;
		}


		/// <summary>
		/// ���������� ������� ����� ����������� ����� � ������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void List1_SelectedIndexChanged(object sender, EventArgs e)
		{
			StarObject star = null;
			TFlex.Model.Model2D.ExternalObject extObject = null;
			ModelObject obj = null;

			if ((List1.SelectedItems.Count > 0) && ((uint)List1.SelectedItems[0].Tag >= 0))
				obj = currentDocument.GetObjectByID((uint)List1.SelectedItems[0].Tag);
			
			if (obj != null)
				extObject = obj as TFlex.Model.Model2D.ExternalObject;
			if (extObject != null)
				star = extObject.ConstObject as StarObject;
			//���� ������ ��������� � ������� ��������������, ��������� ��������� �������, 
			//�.�. �� � ������ ������ ��� - ������ � ����� (���������������)
			if (   (star != null) && (extObject.Attributes.GetIntAttribute("Hidden") != 1)   ) 
				//�������� ������
				currentDocument.Selection.Select(extObject);
		}

		private void List1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			if (List1.SelectedItems.Count == 0)
				currentDocument.Selection.DeselectAll();				
		}
		


    }
}
