using System;
using System.Globalization;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020006D6 RID: 1750
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	[Serializable]
	internal struct Dimension : IEquatable<Dimension>
	{
		// Token: 0x060032A8 RID: 12968 RVA: 0x000C2ECD File Offset: 0x000C10CD
		public Dimension(float value, Dimension.Unit unit)
		{
			this.unit = unit;
			this.value = value;
		}

		// Token: 0x060032A9 RID: 12969 RVA: 0x000C2EE0 File Offset: 0x000C10E0
		public Length ToLength()
		{
			LengthUnit lengthUnit = ((this.unit == Dimension.Unit.Percent) ? LengthUnit.Percent : LengthUnit.Pixel);
			return new Length(this.value, lengthUnit);
		}

		// Token: 0x060032AA RID: 12970 RVA: 0x000C2F0C File Offset: 0x000C110C
		public TimeValue ToTime()
		{
			TimeUnit timeUnit = ((this.unit == Dimension.Unit.Millisecond) ? TimeUnit.Millisecond : TimeUnit.Second);
			return new TimeValue(this.value, timeUnit);
		}

		// Token: 0x060032AB RID: 12971 RVA: 0x000C2F38 File Offset: 0x000C1138
		public Angle ToAngle()
		{
			Angle angle;
			switch (this.unit)
			{
			case Dimension.Unit.Degree:
				angle = new Angle(this.value, AngleUnit.Degree);
				break;
			case Dimension.Unit.Gradian:
				angle = new Angle(this.value, AngleUnit.Gradian);
				break;
			case Dimension.Unit.Radian:
				angle = new Angle(this.value, AngleUnit.Radian);
				break;
			case Dimension.Unit.Turn:
				angle = new Angle(this.value, AngleUnit.Turn);
				break;
			default:
				angle = new Angle(this.value, AngleUnit.Degree);
				break;
			}
			return angle;
		}

		// Token: 0x060032AC RID: 12972 RVA: 0x000C2FB8 File Offset: 0x000C11B8
		public static bool operator ==(Dimension lhs, Dimension rhs)
		{
			return lhs.value == rhs.value && lhs.unit == rhs.unit;
		}

		// Token: 0x060032AD RID: 12973 RVA: 0x000C2FEC File Offset: 0x000C11EC
		public static bool operator !=(Dimension lhs, Dimension rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060032AE RID: 12974 RVA: 0x000C3008 File Offset: 0x000C1208
		public bool Equals(Dimension other)
		{
			return other == this;
		}

		// Token: 0x060032AF RID: 12975 RVA: 0x000C3028 File Offset: 0x000C1228
		public override bool Equals(object obj)
		{
			bool flag = !(obj is Dimension);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				Dimension dimension = (Dimension)obj;
				flag2 = dimension == this;
			}
			return flag2;
		}

		// Token: 0x060032B0 RID: 12976 RVA: 0x000C3064 File Offset: 0x000C1264
		public override int GetHashCode()
		{
			int num = -799583767;
			num = num * -1521134295 + this.unit.GetHashCode();
			return num * -1521134295 + this.value.GetHashCode();
		}

		// Token: 0x060032B1 RID: 12977 RVA: 0x000C30AC File Offset: 0x000C12AC
		public override string ToString()
		{
			string text = string.Empty;
			switch (this.unit)
			{
			case Dimension.Unit.Unitless:
				text = string.Empty;
				break;
			case Dimension.Unit.Pixel:
				text = "px";
				break;
			case Dimension.Unit.Percent:
				text = "%";
				break;
			case Dimension.Unit.Second:
				text = "s";
				break;
			case Dimension.Unit.Millisecond:
				text = "ms";
				break;
			case Dimension.Unit.Degree:
				text = "deg";
				break;
			case Dimension.Unit.Gradian:
				text = "grad";
				break;
			case Dimension.Unit.Radian:
				text = "rad";
				break;
			case Dimension.Unit.Turn:
				text = "turn";
				break;
			}
			return this.value.ToString(CultureInfo.InvariantCulture.NumberFormat) + text;
		}

		// Token: 0x040019C8 RID: 6600
		public Dimension.Unit unit;

		// Token: 0x040019C9 RID: 6601
		public float value;

		// Token: 0x020006D7 RID: 1751
		public enum Unit
		{
			// Token: 0x040019CB RID: 6603
			Unitless,
			// Token: 0x040019CC RID: 6604
			Pixel,
			// Token: 0x040019CD RID: 6605
			Percent,
			// Token: 0x040019CE RID: 6606
			Second,
			// Token: 0x040019CF RID: 6607
			Millisecond,
			// Token: 0x040019D0 RID: 6608
			Degree,
			// Token: 0x040019D1 RID: 6609
			Gradian,
			// Token: 0x040019D2 RID: 6610
			Radian,
			// Token: 0x040019D3 RID: 6611
			Turn
		}
	}
}
