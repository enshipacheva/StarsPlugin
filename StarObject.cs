using System;
using System.IO;
using System.Drawing;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Windows.Forms;

using TFlex;
using TFlex.Model;
using TFlex.Model.Model2D;
using TFlex.Drawing;

//---------------------------------------------------------------------------------------------------------
//������ ���� ��������� ���������������� ������� "������" � ��������� "����"
//������ ���� ������� �� ������ ProxyObject2D
//��������� ������ ����� ����������� ���������, ����� �������� ���������� � ����, 
//������-������, � �.�.
//����������� ���� ���������� ��� ������������� ����������� � ��������� .NET ��������� ������������ � .xml

namespace Stars
{    
	
	//------------------------------------------------------------------------------------------------------
	
	/// <summary>
	/// ����� �������� ����������� ��� Generic ������ ������ ����
	/// </summary>
    [XmlRoot(ElementName = "StarObjectCollection", Namespace = "", IsNullable = false)]
    public class StarObjectCollection : List<StarObject>
    {
        public StarObjectCollection()
        {
        }
    }

	//------------------------------------------------------------------------------------------------------

    /// <summary>
    /// ����� "������"
    /// </summary>
    [XmlRoot(ElementName = "StarObject", Namespace = "Stars", IsNullable = false)]
	public class StarObject : ProxyObject2D
	{
		/// <summary>
		/// ��������������� ������� �������� ����������
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
        static double CalculateHypotenuse(double x, double y) { return Math.Sqrt(x * x + y * y);  }

		//��������� ������------------------------------------------------------------------------------------

        //������ �� ������ ������� ������ (��������, ���� ��� ������ �����) �� �
        //���� ������ ������ ������� ����� ��, ��� ������� ������. ��� ����� ����������
        //����������� �������� (�� ����� FixingNode)

		/// <summary>
		/// ������������ ������
		/// </summary>
		enum References
		{
			FixingNode = 0, Number, X, Y, R1, R2, Angle, Thickness
		};

		//��������� ���������--------------------------------------------------------------------------------

		/// <summary>
		/// ������ �� �������� ���� ������ ��� ������ ��������� ������ ������������� �������
		/// </summary>
		[XmlIgnore]
		public ReferenceHolder<Node> FixingNode;

		/// <summary>
		/// ������ �� ���������� �������� ������ ��� ������ ��������� ������ ������������� �������
		/// </summary>
		[XmlIgnore]
		internal ReferenceHolder<Variable> VarX;

		/// <summary>
		/// ������ �� ���������� �������� ������ ��� ������ ��������� ������ ������������� �������
		/// </summary>
		[XmlIgnore]
		internal ReferenceHolder<Variable> VarY;

		/// <summary>
		/// ������ �� ���������� 1�� ������� ������ ��� ������ ��������� ������ ������������� �������
		/// </summary>
		[XmlIgnore]
		internal ReferenceHolder<Variable> VarR1;

		/// <summary>
		/// ������ �� ���������� 2�� ������� ������ ��� ������ ��������� ������ ������������� �������
		/// </summary>
		[XmlIgnore]
		internal ReferenceHolder<Variable> VarR2;

		/// <summary>
		/// ������ �� ���������� ����� ����� ������ ��� ������ ��������� ������ ������������� �������
		/// </summary>
		[XmlIgnore]
		internal ReferenceHolder<Variable> VarNumber;

		/// <summary>
		/// ������ �� ���������� ���������� ���� �������� ������ ��� ������ ��������� ������ ������������� �������
		/// </summary>
		[XmlIgnore]
		internal ReferenceHolder<Variable> VarAngle;

		/// <summary>
		/// ������ �� ���������� ������� ����� ������ ��� ������ ��������� ������ ������������� �������
		/// </summary>
		[XmlIgnore]
		internal ReferenceHolder<Variable> VarThickness;

		//���������-��������-----------------------------------------------------------------------------------

		/// <summary>
		/// �������������� ����������
		/// </summary>
        [XmlElement]
        public double X 
		{ get; set; }
		
		/// <summary>
		/// ������������ ����������
		/// </summary>
        [XmlElement]
        public double Y { get; set; }
		
		/// <summary>
		/// ������ ������ ������
		/// </summary>
        [XmlElement]
        public double R1 { get; set; }
		
		/// <summary>
		/// ������ ������ ������
		/// </summary>
		[XmlElement]
        public double R2 { get; set; }
		
		/// <summary>
		/// ��������� ����
		/// </summary>
        [XmlElement]
        public double Angle { get; set; }

		/// <summary>
		/// ������� �����
		/// </summary>
        [XmlElement]
        public double Thickness { get; set; }

		/// <summary>
		/// ���������� �����
		/// </summary>
        [XmlElement]
        public int Number { get; set; }

		/// <summary>
		/// ����� �� ��������
		/// </summary>
        [XmlElement]
        public bool Fill { get; set; }

		/// <summary>
		/// ���� ������� � �������
		/// </summary>
        [XmlElement]
        public int Color { get; set; }

		//------------------------------------------------------------------------------------------------------

		/// <summary>
		/// �����������
		/// </summary>
		public StarObject()
		{
			X = 0;
			Y = 0;
			R1 = 10;
			R2 = 20;
			Number = 5;
			Angle = 0;
			Thickness = 1;
			Fill = false;
            Color = (int)TFlex.Drawing.Color.Black;

			FixingNode =	new ReferenceHolder<Node>((int)References.FixingNode, this);
			VarX =			new ReferenceHolder<Variable>((int)References.X, this);
			VarY =			new ReferenceHolder<Variable>((int)References.Y, this);
			VarR1 =			new ReferenceHolder<Variable>((int)References.R1, this);
			VarR2 =			new ReferenceHolder<Variable>((int)References.R2, this);
			VarNumber =		new ReferenceHolder<Variable>((int)References.Number, this);
			VarAngle =		new ReferenceHolder<Variable>((int)References.Angle, this);
			VarThickness =	new ReferenceHolder<Variable>((int)References.Thickness, this);
		}

		/// <summary>
		/// �����������
		/// </summary>
		/// <param name="OwnerHandle"></param>
        public StarObject(IntPtr OwnerHandle)
            : base(OwnerHandle)
		{
			X = 0;
			Y = 0;
			R1 = 10;
			R2 = 20;
			Number = 5;
			Angle = 0;
			Thickness = 1;
			Fill = false;
            Color = (int)TFlex.Drawing.Color.Black;

			FixingNode = new ReferenceHolder<Node>((int)References.FixingNode, this);
			VarX = new ReferenceHolder<Variable>((int)References.X, this);
			VarY = new ReferenceHolder<Variable>((int)References.Y, this);
			VarR1 = new ReferenceHolder<Variable>((int)References.R1, this);
			VarR2 = new ReferenceHolder<Variable>((int)References.R2, this);
			VarNumber = new ReferenceHolder<Variable>((int)References.Number, this);
			VarAngle = new ReferenceHolder<Variable>((int)References.Angle, this);
			VarThickness = new ReferenceHolder<Variable>((int)References.Thickness, this);
		}

		/// <summary>
		/// ��������� ��������� ������� � ��������. ����� ��������� �����������
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		protected override bool Equals(ProxyObject obj)
		{
			StarObject other = obj as StarObject;

			if (other == null)
				return false;

            return X == other.X &&
                Y == other.Y &&
                R1 == other.R1 &&
                R2 == other.R2 &&
                Number == other.Number &&
                Angle == other.Angle &&
                Thickness == other.Thickness &&
                Fill == other.Fill &&
                Color == other.Color;
		}

		/// <summary>
		/// ID ���� �������. �� ���� ���������� ������ ������ �������� ��� ������ ��� �� �����
		/// </summary>
		protected override int TypeID {get{return 1;}}

		/// <summary>
		/// ��� ���� �������. ������������ � ������ ������
		/// </summary>
		protected override string TypeName {get{return "������";}}
        
		/// <summary>
		/// ID ������ �������
		/// </summary>
		protected override int IconID {get{return (int)ObjectTypeIcons.StarObject;}}
        
		/// <summary>
		/// ����� ���� ����� ��� ������ ������ ������, ����� �������� ������ �����
		/// </summary>
        protected override int Version {get{return 0;}}

		/// <summary>
		/// ������ ����� ������ ������� ������ �� ������, ������� ������� ������ � �����
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="Version"></param>
        protected override void Read(Stream stream, int Version)
		{
			BinaryReader reader = new BinaryReader(stream);

			X = reader.ReadDouble();
			Y = reader.ReadDouble();
			R1 = reader.ReadDouble();
			R2 = reader.ReadDouble();
			Number = reader.ReadInt32();
			Angle = reader.ReadDouble();
			Thickness = reader.ReadDouble();
			Fill = reader.ReadBoolean();
            Color = reader.ReadInt32();
		}

		/// <summary>
		/// ���� ����� ������ �������� ������ �������.
        ///������ �� ������ ������ ����������� ����, ��� ������ ��������� ������
		/// </summary>
		/// <param name="stream"></param>
		protected override void Write(Stream stream)
		{
			BinaryWriter writer = new BinaryWriter(stream);

			writer.Write(X);
			writer.Write(Y);
			writer.Write(R1);
			writer.Write(R2);
			writer.Write(Number);
			writer.Write(Angle);
			writer.Write(Thickness);
			writer.Write(Fill);
            writer.Write(Color);
		}

		/// <summary>
		/// ��������������� �������, ������������ ������� ������
		/// </summary>
		/// <returns></returns>
		Polyline CreatePolyline()
		{
			Node node = FixingNode.Value;
			if (node != null)
			{
				X = node.AbsX;
				Y = node.AbsY;
			}

			Polyline polyline = new Polyline();

			double x = X;
			double y = Y;
			double r1 = R1;
			double r2 = R2;
			double step = Math.PI * 2 / Number;
			for(int i = 0; i < Number; i++)
			{
				double angle = Angle / 180 * Math.PI + step * i;
				double s1 = Math.Sin(angle);
				double c1 = Math.Cos(angle);
				polyline.Add(x + r1 * c1,y + r1 * s1);

				double s2 = Math.Sin(angle + step / 2);
				double c2 = Math.Cos(angle + step / 2);
				polyline.Add(x + r2 * c2,y + r2 * s2);
			}

            if (Number != 0)
			    polyline.Add(polyline.get_X(0), polyline.get_Y(0));

			return polyline;
		}

		/// <summary>
		/// �������������� ������� ���������� ��� ��������� ������
		/// </summary>
		/// <param name="graphics"></param>
		public void DrawCursor(TFlex.Drawing.Graphics graphics)
		{
            Draw(graphics);
        }

		/// <summary>
		/// ����� ����������� ���������, ����� ���-�� ���������� �� ������
		/// </summary>
		/// <param name="graphics"></param>
        protected override void Draw(TFlex.Drawing.Graphics graphics)
        {
			//���� ��������� ������ ������ �����������, ��� ������ ���������� ������ ���
			//���������������� � ����
			SyncPropertiesWithVariables();

            //����� ��������� ON
            graphics.BeginDraw();
            
            //�������� ���������
            Polyline pl = CreatePolyline();

            //������������� ��������� ���������:
            //���� ������ ������������, ���� ��� ��������� ���� ExternalObject � �������������� � ������ StarObject
            if (this.Owner2D != null)
                Color = graphics.Color = this.Owner2D.Color.IntValue;

            //���� �� ExternalObject �� �����, ���������� ���� StarObject
            else
                graphics.SetColor(Color);

            //���� ������� ���������
            if (Fill)
                graphics.Fill(pl);                
            //���� ������ � ����������
            else
            {
                graphics.SetLineWidth(Thickness);
                graphics.Polyline(pl);
            }

            graphics.SetColor((int)TFlex.Drawing.Color.Black);
            graphics.DrawMarker(MarkerType.FreeNode, new TFlex.Drawing.Point(X, Y));

            //����� ��������� OFF
            graphics.EndDraw();
        }

		/// <summary>
		/// ����� �� ������ ���� ���������� ���������������? ��.
		/// </summary>
		/// <param name="Context"></param>
		protected override void CanTransform(TransformContext Context)
        {
            Context.ReturnOK();
        }
		
		/// <summary>
		/// ���������� ���������� � ����� ���������...
		/// </summary>
		/// <param name="Context"></param>
        protected override bool Transform(TransformContext Context)
		{
            if (FixingNode.Value == null)
            {
                double refX, refY;
                refX = X; refY = Y;
                
                Context.Map.ToWCS(ref refX, ref refY);
                X = refX; Y = refY;

                R1 *= Context.Map.Scale;
                R2 *= Context.Map.Scale;
                double angle = Angle / 180 * Math.PI;
                Context.Map.ApplyToAngle(ref angle);
                Angle = angle / Math.PI * 180;
                return true;
            }

			return false;
		}

        //���� ��������� ��� 3 ������, �� �� ������ ����� ������ ����� ����������� ���� ������---------------------------

		/// <summary>
		/// ���������� ����� �����
		/// </summary>
		protected override int GetNodeCount() {return Number * 2;}

		/// <summary>
		/// ���������� ID ��������� ����
		/// </summary>
		protected override int GetNodeID(int Number) {return Number;}

		/// <summary>
		/// ���������� ����
		/// </summary>
		protected override bool GetNode(int ID, ref double rx, ref double ry)
		{
			double x1 = X;
			double y1 = Y;
			double r1 = R1;

			if (ID % 2 != 0)
				r1 = R2;

			double step = Math.PI * 2 / Number;

			double angle = (ID / 2) * step;
			if (ID % 2 != 0)
				angle += step / 2;

			angle += Angle / 180 * Math.PI;

			double s1 = Math.Sin(angle);
			double c1 = Math.Cos(angle);

			rx = x1 + r1 * c1;
			ry = y1 + r1 * s1;
    
			return true;
		}


		/// <summary>
		/// ���� ��������� ���� �����, �� ����� �������� ���� ������� � ����������� ���� �������
        ///�������, ��� ������� ������ �������
		/// </summary>
		/// <param name="Menu"></param>
		/// <returns></returns>
		protected override bool GetContextMenu(TFlex.Menu Menu)
		{
			Menu.Append((int)ObjectCommands.Fill, "&�������", true, Fill, OwnerApp);

			return true;
		}

		/// <summary>
		/// ����� ����� ���������� ������� ������������ ����
		/// </summary>
		/// <param name="CommandID"></param>
		/// <param name="View"></param>
		/// <returns></returns>
		protected override bool OnCommand(int CommandID, TFlex.Model.View View)
		{
            switch ((ObjectCommands)CommandID)
            {
                //������� ������������ ���� "�������" ������ ��������� (����� �����):
                case ObjectCommands.Fill:
                    //��� ��� �� ������ ������������ ������ ������
                    Document doc = this.Owner2D.Document;
                    doc.BeginChanges("�������� ������� ������");
                    StarObject newStar = this.Owner2D.VolatileObject as StarObject;
                    newStar.Fill = !Fill;
                    doc.EndChanges();
                    return true;
            }

			return false;
		}


		//���������� ������� ������������ ���� "��������" (����� �����)
		protected override bool Edit(TFlex.Model.View v)
		{
			TFlex.Plugin app = this.OwnerApp;
			Document doc = this.Owner2D.Document;
			ExternalObject selectedOwner = this.Owner2D;

			EditCommand command = new EditCommand(app, selectedOwner, doc);
			doc.Selection.DeselectAll();
			command.Run(v);

			return true;		
		}

        //���� ��������� ��������� 2 ������, �� ������ ����� �������� � ������� "��������"-------------------------------

		/// <summary>
		/// ���������� ������ �������
		/// </summary>
		/// <param name="List"></param>
		protected override void GetPropList(PropertyArray List)
		{
			switch (List.Reason)
			{
                case PropertyArray.ReasonType.Measure:
                    List.Add("PERIMETER", "��������", PropertyArray.Type.Real);
                    break;
                case PropertyArray.ReasonType.Properties:
                    List.Add("RAYCOUNT", "���������� �����", PropertyArray.Type.Real);
                    break;
            }
            List.Add("R1", "������ 1", PropertyArray.Type.Real);
            List.Add("R2", "������ 2", PropertyArray.Type.Real);
        }
	
		/// <summary>
		/// ������� �������� �������� ������
		/// </summary>
		/// <param name="Name"></param>
		/// <returns></returns>
		protected override double GetRealProp(string Name)
		{
			if (string.Compare(Name, "PERIMETER", true) == 0)
			{
				Polyline pl = CreatePolyline();

				double result = 0;
				for (int i = 1, sz = pl.get_PointCount(0); i < sz; ++i)
					result += CalculateHypotenuse(pl.get_X(i - 1) - pl.get_X(i), pl.get_Y(i - 1) - pl.get_Y(i));
				return result;
			}

            if (string.Compare(Name, "RAYCOUNT", true) == 0)
            {
                return Number;
            }
		
			if (string.Compare(Name, "R1", true) == 0)
			{
				return R1;
			}
		
			if (string.Compare(Name, "R2", true) == 0)
			{
				return R2;
			}

			return 0;
		}
        
		/// <summary>
		/// ���� ����� ����� ����������� ����� �������� ������� "��������"
		/// </summary>
		/// <returns></returns>
		protected override PropertyChange EditProperties()
		{
			//"this" - ConstObject �����, ������� VolatileObject ������ ��� this.Owner2D.VolatileObject as StarObject
			
			StarProperties dialog = new StarProperties(this);
			Document doc = this.Owner2D.Document;
		
			PropertyChange result = PropertyChange.Nothing;
			

			if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				doc.BeginChanges("�������� ��������� ������");

				StarObject newStar = Owner2D.VolatileObject as StarObject;
				newStar.Assign(dialog.Star);
				newStar.ApplyReferences(dialog.Star);

				doc.EndChanges();

				result = PropertyChange.Regenerate;
			}
						
			dialog.Dispose();

			return result;
		}


		/// <summary>
		/// ��� ������� �������� ������ ������ �� ��������,
		/// ����� ��������� � ��� �� ����������, ��� � ��������
		/// </summary>
		/// <param name="source"></param>
		public void Assign(StarObject source)
		{
			X = source.X;
			Y = source.Y;
			R1 = source.R1;
			R2 = source.R2;
			Number = source.Number;
			Angle = source.Angle;
			Thickness = source.Thickness;
			Fill = source.Fill;
			Color = source.Color;

			VarX.Value = source.VarX.Value;
			VarY.Value = source.VarY.Value;
			VarR1.Value = source.VarR1.Value;
			VarR2.Value = source.VarR2.Value;
			VarNumber.Value = source.VarNumber.Value;
			VarAngle.Value = source.VarAngle.Value;
			VarThickness.Value = source.VarThickness.Value;
		}

		
		/// <summary>
		/// ���� ����� �������������� ��������� ������, �������� �����������,
		/// � �������� ���������� ���� ����������
		/// </summary>
		private void SyncPropertiesWithVariables()
		{
			if (VarX.Value != null) X = VarX.Value.RealValue;
			if (VarY.Value != null) Y = VarY.Value.RealValue;
			if (VarR1.Value != null) R1 = VarR1.Value.RealValue;
			if (VarR2.Value != null) R2 = VarR2.Value.RealValue;
			if (VarNumber.Value != null) Number = (int)VarNumber.Value.RealValue;
			if (VarAngle.Value != null) Angle = VarAngle.Value.RealValue;
			if (VarThickness.Value != null) Thickness = VarThickness.Value.RealValue;
		}


		/// <summary>
		/// ���� ����� ���������� ����� ������� ������ � �����������
		/// </summary>
		internal void RevokeReferences()
		{
			VarX.ReleaseReference();
			VarY.ReleaseReference();
			VarR1.ReleaseReference();
			VarR2.ReleaseReference();
			VarNumber.ReleaseReference();
			VarAngle.ReleaseReference();
			VarThickness.ReleaseReference();
			FixingNode.ReleaseReference();
		}

		/// <summary>
		/// ���� ����� ������������� ����� ������� ������ �� ���������� �����������
		/// </summary>
		internal void ApplyReferences(StarObject star)
		{
			VarX.SetReference(star.VarX.Value);
			VarY.SetReference(star.VarY.Value);
			VarR1.SetReference(star.VarR1.Value);
			VarR2.SetReference(star.VarR2.Value);
			VarNumber.SetReference(star.VarNumber.Value);
			VarAngle.SetReference(star.VarAngle.Value);
			VarThickness.SetReference(star.VarThickness.Value);
			FixingNode.SetReference(star.FixingNode.Value);
		}
		

		/// <summary>
		/// ���������� ���������� ICloneable
		/// </summary>
		/// <returns></returns>
		protected override ProxyObject Clone(IntPtr OwnerHandle)
		{
			StarObject newStar = base.Clone(OwnerHandle) as StarObject;
			newStar.Assign(this);

			return newStar;	
		}


	}
}
