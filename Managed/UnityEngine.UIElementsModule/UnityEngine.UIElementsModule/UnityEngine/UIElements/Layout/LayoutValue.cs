using System;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x020006C4 RID: 1732
	internal struct LayoutValue
	{
		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x0600320F RID: 12815 RVA: 0x000BD14C File Offset: 0x000BB34C
		public LayoutUnit Unit
		{
			get
			{
				return this.unit;
			}
		}

		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x06003210 RID: 12816 RVA: 0x000BD154 File Offset: 0x000BB354
		public float Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06003211 RID: 12817 RVA: 0x000BD15C File Offset: 0x000BB35C
		public static LayoutValue Point(float value)
		{
			return new LayoutValue
			{
				value = value,
				unit = (float.IsNaN(value) ? LayoutUnit.Undefined : LayoutUnit.Point)
			};
		}

		// Token: 0x06003212 RID: 12818 RVA: 0x000BD194 File Offset: 0x000BB394
		public bool Equals(LayoutValue other)
		{
			return this.Unit == other.Unit && (this.Value.Equals(other.Value) || this.Unit == LayoutUnit.Undefined);
		}

		// Token: 0x06003213 RID: 12819 RVA: 0x000BD1DC File Offset: 0x000BB3DC
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3;
				if (obj is LayoutValue)
				{
					LayoutValue layoutValue = (LayoutValue)obj;
					flag3 = this.Equals(layoutValue);
				}
				else
				{
					flag3 = false;
				}
				flag2 = flag3;
			}
			return flag2;
		}

		// Token: 0x06003214 RID: 12820 RVA: 0x000BD214 File Offset: 0x000BB414
		public override int GetHashCode()
		{
			return (this.Value.GetHashCode() * 397) ^ (int)this.Unit;
		}

		// Token: 0x06003215 RID: 12821 RVA: 0x000BD244 File Offset: 0x000BB444
		public static LayoutValue Undefined()
		{
			return new LayoutValue
			{
				value = float.NaN,
				unit = LayoutUnit.Undefined
			};
		}

		// Token: 0x06003216 RID: 12822 RVA: 0x000BD274 File Offset: 0x000BB474
		public static LayoutValue Auto()
		{
			return new LayoutValue
			{
				value = float.NaN,
				unit = LayoutUnit.Auto
			};
		}

		// Token: 0x06003217 RID: 12823 RVA: 0x000BD2A4 File Offset: 0x000BB4A4
		public static LayoutValue Percent(float value)
		{
			return new LayoutValue
			{
				value = value,
				unit = (float.IsNaN(value) ? LayoutUnit.Undefined : LayoutUnit.Percent)
			};
		}

		// Token: 0x06003218 RID: 12824 RVA: 0x000BD2DC File Offset: 0x000BB4DC
		public static implicit operator LayoutValue(float value)
		{
			return LayoutValue.Point(value);
		}

		// Token: 0x04001902 RID: 6402
		private float value;

		// Token: 0x04001903 RID: 6403
		private LayoutUnit unit;
	}
}
