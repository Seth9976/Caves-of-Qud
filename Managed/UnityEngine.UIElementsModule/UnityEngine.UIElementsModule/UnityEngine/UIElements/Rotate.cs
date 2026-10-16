using System;
using Unity.Properties;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x0200045A RID: 1114
	public struct Rotate : IEquatable<Rotate>
	{
		// Token: 0x06002295 RID: 8853 RVA: 0x000817A2 File Offset: 0x0007F9A2
		internal Rotate(Angle angle, Vector3 axis)
		{
			this.m_Angle = angle;
			this.m_Axis = axis;
			this.m_IsNone = false;
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x000817BA File Offset: 0x0007F9BA
		public Rotate(Angle angle)
		{
			this.m_Angle = angle;
			this.m_Axis = Vector3.forward;
			this.m_IsNone = false;
		}

		// Token: 0x06002297 RID: 8855 RVA: 0x000817D8 File Offset: 0x0007F9D8
		internal Rotate(Quaternion quaternion)
		{
			float num;
			Vector3 vector;
			quaternion.ToAngleAxis(out num, out vector);
			this.m_Angle = num;
			this.m_Axis = vector;
			this.m_IsNone = false;
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x0008180C File Offset: 0x0007FA0C
		internal static Rotate Initial()
		{
			return new Rotate(0f);
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x00081830 File Offset: 0x0007FA30
		public static Rotate None()
		{
			Rotate rotate = Rotate.Initial();
			rotate.m_IsNone = true;
			return rotate;
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x0600229A RID: 8858 RVA: 0x00081851 File Offset: 0x0007FA51
		// (set) Token: 0x0600229B RID: 8859 RVA: 0x00081859 File Offset: 0x0007FA59
		public Angle angle
		{
			get
			{
				return this.m_Angle;
			}
			set
			{
				this.m_Angle = value;
			}
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x0600229C RID: 8860 RVA: 0x00081862 File Offset: 0x0007FA62
		// (set) Token: 0x0600229D RID: 8861 RVA: 0x0008186A File Offset: 0x0007FA6A
		internal Vector3 axis
		{
			get
			{
				return this.m_Axis;
			}
			set
			{
				this.m_Axis = value;
			}
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x00081873 File Offset: 0x0007FA73
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal bool IsNone()
		{
			return this.m_IsNone;
		}

		// Token: 0x0600229F RID: 8863 RVA: 0x0008187C File Offset: 0x0007FA7C
		public static bool operator ==(Rotate lhs, Rotate rhs)
		{
			return lhs.m_Angle == rhs.m_Angle && lhs.m_Axis == rhs.m_Axis && lhs.m_IsNone == rhs.m_IsNone;
		}

		// Token: 0x060022A0 RID: 8864 RVA: 0x000818C8 File Offset: 0x0007FAC8
		public static bool operator !=(Rotate lhs, Rotate rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060022A1 RID: 8865 RVA: 0x000818E4 File Offset: 0x0007FAE4
		public bool Equals(Rotate other)
		{
			return other == this;
		}

		// Token: 0x060022A2 RID: 8866 RVA: 0x00081904 File Offset: 0x0007FB04
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is Rotate)
			{
				Rotate rotate = (Rotate)obj;
				flag = this.Equals(rotate);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x00081930 File Offset: 0x0007FB30
		public override int GetHashCode()
		{
			return (this.m_Angle.GetHashCode() * 793) ^ (this.m_Axis.GetHashCode() * 791) ^ (this.m_IsNone.GetHashCode() * 197);
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x00081984 File Offset: 0x0007FB84
		public override string ToString()
		{
			return this.m_Angle.ToString() + " " + this.m_Axis.ToString();
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x000819C4 File Offset: 0x0007FBC4
		internal Quaternion ToQuaternion()
		{
			return Quaternion.AngleAxis(this.m_Angle.ToDegrees(), this.m_Axis);
		}

		// Token: 0x04000FA3 RID: 4003
		private Angle m_Angle;

		// Token: 0x04000FA4 RID: 4004
		private Vector3 m_Axis;

		// Token: 0x04000FA5 RID: 4005
		private bool m_IsNone;

		// Token: 0x0200045B RID: 1115
		internal class PropertyBag : ContainerPropertyBag<Rotate>
		{
			// Token: 0x060022A6 RID: 8870 RVA: 0x000819EC File Offset: 0x0007FBEC
			public PropertyBag()
			{
				base.AddProperty<Angle>(new Rotate.PropertyBag.AngleProperty());
				base.AddProperty<Vector3>(new Rotate.PropertyBag.AxisProperty());
			}

			// Token: 0x0200045C RID: 1116
			private class AngleProperty : Property<Rotate, Angle>
			{
				// Token: 0x17000921 RID: 2337
				// (get) Token: 0x060022A7 RID: 8871 RVA: 0x00081A0E File Offset: 0x0007FC0E
				public override string Name { get; } = "angle";

				// Token: 0x17000922 RID: 2338
				// (get) Token: 0x060022A8 RID: 8872 RVA: 0x00081A16 File Offset: 0x0007FC16
				public override bool IsReadOnly { get; } = false;

				// Token: 0x060022A9 RID: 8873 RVA: 0x00081A1E File Offset: 0x0007FC1E
				public override Angle GetValue(ref Rotate container)
				{
					return container.angle;
				}

				// Token: 0x060022AA RID: 8874 RVA: 0x00081A26 File Offset: 0x0007FC26
				public override void SetValue(ref Rotate container, Angle value)
				{
					container.angle = value;
				}
			}

			// Token: 0x0200045D RID: 1117
			private class AxisProperty : Property<Rotate, Vector3>
			{
				// Token: 0x17000923 RID: 2339
				// (get) Token: 0x060022AC RID: 8876 RVA: 0x00081A4B File Offset: 0x0007FC4B
				public override string Name { get; } = "axis";

				// Token: 0x17000924 RID: 2340
				// (get) Token: 0x060022AD RID: 8877 RVA: 0x00081A53 File Offset: 0x0007FC53
				public override bool IsReadOnly { get; } = false;

				// Token: 0x060022AE RID: 8878 RVA: 0x00081A5B File Offset: 0x0007FC5B
				public override Vector3 GetValue(ref Rotate container)
				{
					return container.axis;
				}

				// Token: 0x060022AF RID: 8879 RVA: 0x00081A63 File Offset: 0x0007FC63
				public override void SetValue(ref Rotate container, Vector3 value)
				{
					container.axis = value;
				}
			}
		}
	}
}
