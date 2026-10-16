using System;
using System.Globalization;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x02000341 RID: 833
	public struct Angle : IEquatable<Angle>
	{
		// Token: 0x0600181E RID: 6174 RVA: 0x0005D518 File Offset: 0x0005B718
		public static Angle Degrees(float value)
		{
			return new Angle(value, AngleUnit.Degree);
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x0005D534 File Offset: 0x0005B734
		public static Angle Gradians(float value)
		{
			return new Angle(value, AngleUnit.Gradian);
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x0005D550 File Offset: 0x0005B750
		public static Angle Radians(float value)
		{
			return new Angle(value, AngleUnit.Radian);
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x0005D56C File Offset: 0x0005B76C
		public static Angle Turns(float value)
		{
			return new Angle(value, AngleUnit.Turn);
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x0005D588 File Offset: 0x0005B788
		internal static Angle None()
		{
			return new Angle(0f, Angle.Unit.None);
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001823 RID: 6179 RVA: 0x0005D5A5 File Offset: 0x0005B7A5
		// (set) Token: 0x06001824 RID: 6180 RVA: 0x0005D5AD File Offset: 0x0005B7AD
		public float value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = value;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001825 RID: 6181 RVA: 0x0005D5B6 File Offset: 0x0005B7B6
		// (set) Token: 0x06001826 RID: 6182 RVA: 0x0005D5BE File Offset: 0x0005B7BE
		public AngleUnit unit
		{
			get
			{
				return (AngleUnit)this.m_Unit;
			}
			set
			{
				this.m_Unit = (Angle.Unit)value;
			}
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x0005D5C7 File Offset: 0x0005B7C7
		internal bool IsNone()
		{
			return this.m_Unit == Angle.Unit.None;
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x0005D5D2 File Offset: 0x0005B7D2
		public Angle(float value)
		{
			this = new Angle(value, Angle.Unit.Degree);
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x0005D5DE File Offset: 0x0005B7DE
		public Angle(float value, AngleUnit unit)
		{
			this = new Angle(value, (Angle.Unit)unit);
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x0005D5EA File Offset: 0x0005B7EA
		private Angle(float value, Angle.Unit unit)
		{
			this.m_Value = value;
			this.m_Unit = unit;
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x0005D5FC File Offset: 0x0005B7FC
		public float ToDegrees()
		{
			float num;
			switch (this.m_Unit)
			{
			case Angle.Unit.Degree:
				num = this.m_Value;
				break;
			case Angle.Unit.Gradian:
				num = this.m_Value * 360f / 400f;
				break;
			case Angle.Unit.Radian:
				num = this.m_Value * 180f / 3.1415927f;
				break;
			case Angle.Unit.Turn:
				num = this.m_Value * 360f;
				break;
			case Angle.Unit.None:
				num = 0f;
				break;
			default:
				num = 0f;
				break;
			}
			return num;
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x0005D684 File Offset: 0x0005B884
		public float ToGradians()
		{
			float num;
			switch (this.m_Unit)
			{
			case Angle.Unit.Degree:
				num = this.m_Value * 10f / 9f;
				break;
			case Angle.Unit.Gradian:
				num = this.m_Value;
				break;
			case Angle.Unit.Radian:
				num = this.m_Value * 200f / 3.1415927f;
				break;
			case Angle.Unit.Turn:
				num = this.m_Value * 400f;
				break;
			case Angle.Unit.None:
				num = 0f;
				break;
			default:
				num = 0f;
				break;
			}
			return num;
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x0005D70C File Offset: 0x0005B90C
		public float ToRadians()
		{
			float num;
			switch (this.m_Unit)
			{
			case Angle.Unit.Degree:
				num = this.m_Value * 3.1415927f / 180f;
				break;
			case Angle.Unit.Gradian:
				num = this.m_Value * 3.1415927f / 200f;
				break;
			case Angle.Unit.Radian:
				num = this.m_Value;
				break;
			case Angle.Unit.Turn:
				num = this.m_Value * 3.1415927f * 2f;
				break;
			case Angle.Unit.None:
				num = 0f;
				break;
			default:
				num = 0f;
				break;
			}
			return num;
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x0005D798 File Offset: 0x0005B998
		public float ToTurns()
		{
			float num;
			switch (this.m_Unit)
			{
			case Angle.Unit.Degree:
				num = this.m_Value / 360f;
				break;
			case Angle.Unit.Gradian:
				num = this.m_Value / 400f;
				break;
			case Angle.Unit.Radian:
				num = this.m_Value / 6.2831855f;
				break;
			case Angle.Unit.Turn:
				num = this.m_Value;
				break;
			case Angle.Unit.None:
				num = 0f;
				break;
			default:
				num = 0f;
				break;
			}
			return num;
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x0005D814 File Offset: 0x0005BA14
		internal void ConvertTo(AngleUnit newUnit)
		{
			if (!true)
			{
			}
			float num;
			switch (newUnit)
			{
			case AngleUnit.Degree:
				num = this.ToDegrees();
				break;
			case AngleUnit.Gradian:
				num = this.ToGradians();
				break;
			case AngleUnit.Radian:
				num = this.ToRadians();
				break;
			case AngleUnit.Turn:
				num = this.ToTurns();
				break;
			default:
				throw new NotImplementedException();
			}
			if (!true)
			{
			}
			this.m_Value = num;
			this.m_Unit = (Angle.Unit)newUnit;
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x0005D87C File Offset: 0x0005BA7C
		public static implicit operator Angle(float value)
		{
			return new Angle(value, AngleUnit.Degree);
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x0005D898 File Offset: 0x0005BA98
		public static bool operator ==(Angle lhs, Angle rhs)
		{
			return lhs.m_Value == rhs.m_Value && lhs.m_Unit == rhs.m_Unit;
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x0005D8CC File Offset: 0x0005BACC
		public static bool operator !=(Angle lhs, Angle rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x0005D8E8 File Offset: 0x0005BAE8
		public bool Equals(Angle other)
		{
			return other == this;
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x0005D908 File Offset: 0x0005BB08
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is Angle)
			{
				Angle angle = (Angle)obj;
				flag = this.Equals(angle);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x0005D934 File Offset: 0x0005BB34
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Unit;
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x0005D960 File Offset: 0x0005BB60
		public override string ToString()
		{
			string text = this.value.ToString(CultureInfo.InvariantCulture.NumberFormat);
			string text2 = string.Empty;
			switch (this.m_Unit)
			{
			case Angle.Unit.Degree:
			{
				bool flag = !Mathf.Approximately(0f, this.value);
				if (flag)
				{
					text2 = "deg";
				}
				break;
			}
			case Angle.Unit.Gradian:
				text2 = "grad";
				break;
			case Angle.Unit.Radian:
				text2 = "rad";
				break;
			case Angle.Unit.Turn:
				text2 = "turn";
				break;
			case Angle.Unit.None:
				text = "";
				break;
			}
			return text + text2;
		}

		// Token: 0x04000D2E RID: 3374
		private float m_Value;

		// Token: 0x04000D2F RID: 3375
		private Angle.Unit m_Unit;

		// Token: 0x02000342 RID: 834
		private enum Unit
		{
			// Token: 0x04000D31 RID: 3377
			Degree,
			// Token: 0x04000D32 RID: 3378
			Gradian,
			// Token: 0x04000D33 RID: 3379
			Radian,
			// Token: 0x04000D34 RID: 3380
			Turn,
			// Token: 0x04000D35 RID: 3381
			None
		}

		// Token: 0x02000343 RID: 835
		internal class PropertyBag : ContainerPropertyBag<Angle>
		{
			// Token: 0x06001837 RID: 6199 RVA: 0x0005DA02 File Offset: 0x0005BC02
			public PropertyBag()
			{
				base.AddProperty<float>(new Angle.PropertyBag.ValueProperty());
				base.AddProperty<AngleUnit>(new Angle.PropertyBag.UnitProperty());
			}

			// Token: 0x02000344 RID: 836
			private class ValueProperty : Property<Angle, float>
			{
				// Token: 0x17000494 RID: 1172
				// (get) Token: 0x06001838 RID: 6200 RVA: 0x0005DA24 File Offset: 0x0005BC24
				public override string Name { get; } = "value";

				// Token: 0x17000495 RID: 1173
				// (get) Token: 0x06001839 RID: 6201 RVA: 0x0005DA2C File Offset: 0x0005BC2C
				public override bool IsReadOnly { get; } = false;

				// Token: 0x0600183A RID: 6202 RVA: 0x0005DA34 File Offset: 0x0005BC34
				public override float GetValue(ref Angle container)
				{
					return container.value;
				}

				// Token: 0x0600183B RID: 6203 RVA: 0x0005DA3C File Offset: 0x0005BC3C
				public override void SetValue(ref Angle container, float value)
				{
					container.value = value;
				}
			}

			// Token: 0x02000345 RID: 837
			private class UnitProperty : Property<Angle, AngleUnit>
			{
				// Token: 0x17000496 RID: 1174
				// (get) Token: 0x0600183D RID: 6205 RVA: 0x0005DA61 File Offset: 0x0005BC61
				public override string Name { get; } = "unit";

				// Token: 0x17000497 RID: 1175
				// (get) Token: 0x0600183E RID: 6206 RVA: 0x0005DA69 File Offset: 0x0005BC69
				public override bool IsReadOnly { get; } = false;

				// Token: 0x0600183F RID: 6207 RVA: 0x0005DA71 File Offset: 0x0005BC71
				public override AngleUnit GetValue(ref Angle container)
				{
					return container.unit;
				}

				// Token: 0x06001840 RID: 6208 RVA: 0x0005DA79 File Offset: 0x0005BC79
				public override void SetValue(ref Angle container, AngleUnit value)
				{
					container.unit = value;
				}
			}
		}
	}
}
