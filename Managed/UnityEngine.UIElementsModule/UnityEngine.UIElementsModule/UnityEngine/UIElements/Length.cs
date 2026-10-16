using System;
using System.Globalization;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x02000455 RID: 1109
	[Serializable]
	public struct Length : IEquatable<Length>
	{
		// Token: 0x06002276 RID: 8822 RVA: 0x0008131C File Offset: 0x0007F51C
		public static Length Percent(float value)
		{
			return new Length(value, LengthUnit.Percent);
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x00081338 File Offset: 0x0007F538
		public static Length Auto()
		{
			return new Length(0f, Length.Unit.Auto);
		}

		// Token: 0x06002278 RID: 8824 RVA: 0x00081358 File Offset: 0x0007F558
		public static Length None()
		{
			return new Length(0f, Length.Unit.None);
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x06002279 RID: 8825 RVA: 0x00081375 File Offset: 0x0007F575
		// (set) Token: 0x0600227A RID: 8826 RVA: 0x0008137D File Offset: 0x0007F57D
		public float value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = Mathf.Clamp(value, -8388608f, 8388608f);
			}
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x0600227B RID: 8827 RVA: 0x00081395 File Offset: 0x0007F595
		// (set) Token: 0x0600227C RID: 8828 RVA: 0x0008139D File Offset: 0x0007F59D
		public LengthUnit unit
		{
			get
			{
				return (LengthUnit)this.m_Unit;
			}
			set
			{
				this.m_Unit = (Length.Unit)value;
			}
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x000813A6 File Offset: 0x0007F5A6
		public bool IsAuto()
		{
			return this.m_Unit == Length.Unit.Auto;
		}

		// Token: 0x0600227E RID: 8830 RVA: 0x000813B1 File Offset: 0x0007F5B1
		public bool IsNone()
		{
			return this.m_Unit == Length.Unit.None;
		}

		// Token: 0x0600227F RID: 8831 RVA: 0x000813BC File Offset: 0x0007F5BC
		public Length(float value)
		{
			this = new Length(value, Length.Unit.Pixel);
		}

		// Token: 0x06002280 RID: 8832 RVA: 0x000813C8 File Offset: 0x0007F5C8
		public Length(float value, LengthUnit unit)
		{
			this = new Length(value, (Length.Unit)unit);
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x000813D4 File Offset: 0x0007F5D4
		private Length(float value, Length.Unit unit)
		{
			this = default(Length);
			this.value = value;
			this.m_Unit = unit;
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x000813F0 File Offset: 0x0007F5F0
		public static implicit operator Length(float value)
		{
			return new Length(value, LengthUnit.Pixel);
		}

		// Token: 0x06002283 RID: 8835 RVA: 0x0008140C File Offset: 0x0007F60C
		public static bool operator ==(Length lhs, Length rhs)
		{
			return lhs.m_Value == rhs.m_Value && lhs.m_Unit == rhs.m_Unit;
		}

		// Token: 0x06002284 RID: 8836 RVA: 0x00081440 File Offset: 0x0007F640
		public static bool operator !=(Length lhs, Length rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06002285 RID: 8837 RVA: 0x0008145C File Offset: 0x0007F65C
		public bool Equals(Length other)
		{
			return other == this;
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x0008147C File Offset: 0x0007F67C
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is Length)
			{
				Length length = (Length)obj;
				flag = this.Equals(length);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06002287 RID: 8839 RVA: 0x000814A8 File Offset: 0x0007F6A8
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Unit;
		}

		// Token: 0x06002288 RID: 8840 RVA: 0x000814D4 File Offset: 0x0007F6D4
		public override string ToString()
		{
			string text = this.value.ToString(CultureInfo.InvariantCulture.NumberFormat);
			string text2 = string.Empty;
			switch (this.m_Unit)
			{
			case Length.Unit.Pixel:
			{
				bool flag = !Mathf.Approximately(0f, this.value);
				if (flag)
				{
					text2 = "px";
				}
				break;
			}
			case Length.Unit.Percent:
				text2 = "%";
				break;
			case Length.Unit.Auto:
				text = "auto";
				break;
			case Length.Unit.None:
				text = "none";
				break;
			}
			return text + text2;
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x0008156C File Offset: 0x0007F76C
		internal static Length ParseString(string str, Length defaultValue = default(Length))
		{
			bool flag = string.IsNullOrEmpty(str);
			Length length;
			if (flag)
			{
				length = defaultValue;
			}
			else
			{
				str = str.ToLowerInvariant().Trim();
				Length length2 = defaultValue;
				bool flag2 = char.IsLetter(str[0]);
				if (flag2)
				{
					bool flag3 = str == "auto";
					if (flag3)
					{
						length2 = Length.Auto();
					}
					else
					{
						bool flag4 = str == "none";
						if (flag4)
						{
							length2 = Length.None();
						}
					}
				}
				else
				{
					int num = 0;
					int num2 = -1;
					int i = 0;
					while (i < str.Length)
					{
						char c = str[i];
						bool flag5 = char.IsNumber(c) || c == '.';
						if (flag5)
						{
							num++;
							i++;
						}
						else
						{
							bool flag6 = char.IsLetter(c) || c == '%';
							if (flag6)
							{
								num2 = i;
								break;
							}
							return defaultValue;
						}
					}
					string text = str.Substring(0, num);
					string text2 = string.Empty;
					bool flag7 = num2 > 0;
					if (flag7)
					{
						text2 = str.Substring(num2, str.Length - num2);
					}
					else
					{
						text2 = "px";
					}
					float num3 = defaultValue.value;
					LengthUnit lengthUnit = defaultValue.unit;
					float num4;
					bool flag8 = float.TryParse(text, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, CultureInfo.InvariantCulture.NumberFormat, out num4);
					if (flag8)
					{
						num3 = num4;
					}
					string text3 = text2;
					string text4 = text3;
					if (!(text4 == "px"))
					{
						if (text4 == "%")
						{
							lengthUnit = LengthUnit.Percent;
						}
					}
					else
					{
						lengthUnit = LengthUnit.Pixel;
					}
					length2 = new Length(num3, lengthUnit);
				}
				length = length2;
			}
			return length;
		}

		// Token: 0x04000F97 RID: 3991
		internal const float k_MaxValue = 8388608f;

		// Token: 0x04000F98 RID: 3992
		[SerializeField]
		private float m_Value;

		// Token: 0x04000F99 RID: 3993
		[SerializeField]
		private Length.Unit m_Unit;

		// Token: 0x02000456 RID: 1110
		private enum Unit
		{
			// Token: 0x04000F9B RID: 3995
			Pixel,
			// Token: 0x04000F9C RID: 3996
			Percent,
			// Token: 0x04000F9D RID: 3997
			Auto,
			// Token: 0x04000F9E RID: 3998
			None
		}

		// Token: 0x02000457 RID: 1111
		internal class PropertyBag : ContainerPropertyBag<Length>
		{
			// Token: 0x0600228A RID: 8842 RVA: 0x00081706 File Offset: 0x0007F906
			public PropertyBag()
			{
				base.AddProperty<float>(new Length.PropertyBag.ValueProperty());
				base.AddProperty<LengthUnit>(new Length.PropertyBag.UnitProperty());
			}

			// Token: 0x02000458 RID: 1112
			private class ValueProperty : Property<Length, float>
			{
				// Token: 0x1700091B RID: 2331
				// (get) Token: 0x0600228B RID: 8843 RVA: 0x00081728 File Offset: 0x0007F928
				public override string Name { get; } = "value";

				// Token: 0x1700091C RID: 2332
				// (get) Token: 0x0600228C RID: 8844 RVA: 0x00081730 File Offset: 0x0007F930
				public override bool IsReadOnly { get; } = false;

				// Token: 0x0600228D RID: 8845 RVA: 0x00081738 File Offset: 0x0007F938
				public override float GetValue(ref Length container)
				{
					return container.value;
				}

				// Token: 0x0600228E RID: 8846 RVA: 0x00081740 File Offset: 0x0007F940
				public override void SetValue(ref Length container, float value)
				{
					container.value = value;
				}
			}

			// Token: 0x02000459 RID: 1113
			private class UnitProperty : Property<Length, LengthUnit>
			{
				// Token: 0x1700091D RID: 2333
				// (get) Token: 0x06002290 RID: 8848 RVA: 0x00081765 File Offset: 0x0007F965
				public override string Name { get; } = "unit";

				// Token: 0x1700091E RID: 2334
				// (get) Token: 0x06002291 RID: 8849 RVA: 0x0008176D File Offset: 0x0007F96D
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06002292 RID: 8850 RVA: 0x00081775 File Offset: 0x0007F975
				public override LengthUnit GetValue(ref Length container)
				{
					return container.unit;
				}

				// Token: 0x06002293 RID: 8851 RVA: 0x0008177D File Offset: 0x0007F97D
				public override void SetValue(ref Length container, LengthUnit value)
				{
					container.unit = value;
				}
			}
		}
	}
}
