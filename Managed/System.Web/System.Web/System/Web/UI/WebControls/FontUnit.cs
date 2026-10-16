using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the size of a font.</summary>
	// Token: 0x02000393 RID: 915
	[TypeConverter(typeof(FontUnitConverter))]
	[Serializable]
	public struct FontUnit
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FontUnit" /> class with the specified <see cref="T:System.Web.UI.WebControls.FontSize" />.</summary>
		/// <param name="type">One of the <see cref="T:System.Web.UI.WebControls.FontSize" /> values. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified font size is not one of the <see cref="T:System.Web.UI.WebControls.FontSize" /> values. </exception>
		// Token: 0x060023F1 RID: 9201 RVA: 0x0005B514 File Offset: 0x00059714
		public FontUnit(FontSize type)
		{
			if (type < FontSize.NotSet || type > FontSize.XXLarge)
			{
				throw new ArgumentOutOfRangeException("type");
			}
			this.type = type;
			if (type == FontSize.AsUnit)
			{
				this.unit = new Unit(10.0, UnitType.Point);
				return;
			}
			this.unit = Unit.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FontUnit" /> class with the specified font size.</summary>
		/// <param name="value">The size of the font. </param>
		// Token: 0x060023F2 RID: 9202 RVA: 0x0005B563 File Offset: 0x00059763
		public FontUnit(int value)
		{
			this = new FontUnit(new Unit((double)value, UnitType.Point));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FontUnit" /> class with the specified font size in points.</summary>
		/// <param name="value">A <see cref="T:System.Double" /> that specifies the font size. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is outside the valid range.</exception>
		// Token: 0x060023F3 RID: 9203 RVA: 0x0005B573 File Offset: 0x00059773
		public FontUnit(double value)
		{
			this = new FontUnit(new Unit(value, UnitType.Point));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FontUnit" /> class with the specified font size and <see cref="T:System.Web.UI.WebControls.UnitType" /> value.</summary>
		/// <param name="value">A <see cref="T:System.Double" /> that specifies the font size. </param>
		/// <param name="type">A <see cref="T:System.Web.UI.WebControls.UnitType" /> to specify the units of the size.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is outside the valid range.</exception>
		// Token: 0x060023F4 RID: 9204 RVA: 0x0005B582 File Offset: 0x00059782
		public FontUnit(double value, UnitType type)
		{
			this = new FontUnit(new Unit(value, type));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FontUnit" /> class with the specified <see cref="T:System.Web.UI.WebControls.Unit" />.</summary>
		/// <param name="value">A <see cref="T:System.Web.UI.WebControls.Unit" /> that specifies the font size. </param>
		// Token: 0x060023F5 RID: 9205 RVA: 0x0005B591 File Offset: 0x00059791
		public FontUnit(Unit value)
		{
			this.type = FontSize.AsUnit;
			this.unit = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FontUnit" /> class with the specified string.</summary>
		/// <param name="value">A string to specify the font size.</param>
		// Token: 0x060023F6 RID: 9206 RVA: 0x0005B5A1 File Offset: 0x000597A1
		public FontUnit(string value)
		{
			this = new FontUnit(value, Thread.CurrentThread.CurrentCulture);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FontUnit" /> class with the specified string using the specified <see cref="T:System.Globalization.CultureInfo" /> object.</summary>
		/// <param name="value">A string to specify the font size.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> used to make string comparisons.</param>
		// Token: 0x060023F7 RID: 9207 RVA: 0x0005B5B4 File Offset: 0x000597B4
		public FontUnit(string value, CultureInfo culture)
		{
			if (string.IsNullOrEmpty(value))
			{
				this.type = FontSize.NotSet;
				this.unit = Unit.Empty;
				return;
			}
			string text = value.ToLower(Helpers.InvariantCulture);
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 731393469U)
			{
				if (num <= 395807292U)
				{
					if (num != 223437115U)
					{
						if (num != 284158167U)
						{
							if (num == 395807292U)
							{
								if (text == "xxlarge")
								{
									this.type = FontSize.XXLarge;
									goto IL_028C;
								}
							}
						}
						else if (text == "xx-small")
						{
							this.type = FontSize.XXSmall;
							goto IL_028C;
						}
					}
					else if (text == "smaller")
					{
						this.type = FontSize.Smaller;
						goto IL_028C;
					}
				}
				else if (num != 515378866U)
				{
					if (num != 681251161U)
					{
						if (num == 731393469U)
						{
							if (text == "x-small")
							{
								this.type = FontSize.XSmall;
								goto IL_028C;
							}
						}
					}
					else if (text == "x-large")
					{
						this.type = FontSize.XLarge;
						goto IL_028C;
					}
				}
				else if (text == "larger")
				{
					this.type = FontSize.Larger;
					goto IL_028C;
				}
			}
			else if (num <= 1738597334U)
			{
				if (num != 900716406U)
				{
					if (num != 1271934388U)
					{
						if (num == 1738597334U)
						{
							if (text == "xsmall")
							{
								this.type = FontSize.XSmall;
								goto IL_028C;
							}
						}
					}
					else if (text == "large")
					{
						this.type = FontSize.Large;
						goto IL_028C;
					}
				}
				else if (text == "medium")
				{
					this.type = FontSize.Medium;
					goto IL_028C;
				}
			}
			else if (num <= 2159706708U)
			{
				if (num != 1865116687U)
				{
					if (num == 2159706708U)
					{
						if (text == "xxsmall")
						{
							this.type = FontSize.XXSmall;
							goto IL_028C;
						}
					}
				}
				else if (text == "xx-large")
				{
					this.type = FontSize.XXLarge;
					goto IL_028C;
				}
			}
			else if (num != 2730816652U)
			{
				if (num == 2778388034U)
				{
					if (text == "xlarge")
					{
						this.type = FontSize.XLarge;
						goto IL_028C;
					}
				}
			}
			else if (text == "small")
			{
				this.type = FontSize.Small;
				goto IL_028C;
			}
			this.type = FontSize.AsUnit;
			this.unit = new Unit(value, culture);
			return;
			IL_028C:
			this.unit = Unit.Empty;
		}

		/// <summary>Gets a value that indicates whether the font size has been set.</summary>
		/// <returns>true if the font size has not been set; otherwise, false.</returns>
		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x060023F8 RID: 9208 RVA: 0x0005B858 File Offset: 0x00059A58
		public bool IsEmpty
		{
			get
			{
				return this.type == FontSize.NotSet;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.FontSize" /> enumeration value that represents the font size.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.FontSize" /> values.</returns>
		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x060023F9 RID: 9209 RVA: 0x0005B863 File Offset: 0x00059A63
		public FontSize Type
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.Unit" /> that represents the font size.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> object that specifies the font size.</returns>
		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x060023FA RID: 9210 RVA: 0x0005B86B File Offset: 0x00059A6B
		public Unit Unit
		{
			get
			{
				return this.unit;
			}
		}

		/// <summary>Converts the specified string to its <see cref="T:System.Web.UI.WebControls.FontUnit" /> equivalent.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.FontUnit" /> that represents the font size specified by the <paramref name="s" /> parameter.</returns>
		/// <param name="s">A string representation of one of the <see cref="T:System.Web.UI.WebControls.FontSize" /> values. </param>
		// Token: 0x060023FB RID: 9211 RVA: 0x0005B873 File Offset: 0x00059A73
		public static FontUnit Parse(string s)
		{
			return new FontUnit(s);
		}

		/// <summary>Converts the specified string to its <see cref="T:System.Web.UI.WebControls.FontUnit" /> equivalent in the specified culture.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.FontUnit" /> that represents the font size specified by the <paramref name="s" /> parameter in the culture specified by the <paramref name="culture" /> parameter.</returns>
		/// <param name="s">A string representation of one of the <see cref="T:System.Web.UI.WebControls.FontSize" /> values. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that represents the culture of the <see cref="T:System.Web.UI.WebControls.FontUnit" /> object. </param>
		// Token: 0x060023FC RID: 9212 RVA: 0x0005B87B File Offset: 0x00059A7B
		public static FontUnit Parse(string s, CultureInfo culture)
		{
			return new FontUnit(s, culture);
		}

		/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.FontUnit" /> of type <see cref="T:System.Drawing.Point" /> from an integer value.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.FontUnit" /> that represents the font size specified by the <paramref name="n" /> parameter.</returns>
		/// <param name="n">An integer representing the <see cref="T:System.Drawing.Point" /> value to convert to a <see cref="T:System.Web.UI.WebControls.FontUnit" />. </param>
		// Token: 0x060023FD RID: 9213 RVA: 0x0005B884 File Offset: 0x00059A84
		public static FontUnit Point(int n)
		{
			return new FontUnit(n);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equivalent to the instance of the <see cref="T:System.Web.UI.WebControls.FontUnit" /> class that this method is called from.</summary>
		/// <returns>true if the specified <see cref="T:System.Object" /> is equivalent to the instance of the <see cref="T:System.Web.UI.WebControls.FontUnit" /> class that this method is called from; otherwise, false.</returns>
		/// <param name="obj">A <see cref="T:System.Object" /> that contains the object to compare to this instance. </param>
		// Token: 0x060023FE RID: 9214 RVA: 0x0005B88C File Offset: 0x00059A8C
		public override bool Equals(object obj)
		{
			if (obj is FontUnit)
			{
				FontUnit fontUnit = (FontUnit)obj;
				return fontUnit.type == this.type && fontUnit.unit == this.unit;
			}
			return false;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer that represents the hash code.</returns>
		// Token: 0x060023FF RID: 9215 RVA: 0x0005B8CB File Offset: 0x00059ACB
		public override int GetHashCode()
		{
			return this.type.GetHashCode() ^ this.unit.GetHashCode();
		}

		/// <summary>Compares two <see cref="T:System.Web.UI.WebControls.FontUnit" /> objects for equality.</summary>
		/// <returns>true if both <see cref="T:System.Web.UI.WebControls.FontUnit" /> objects are equal; otherwise, false.</returns>
		/// <param name="left">A <see cref="T:System.Web.UI.WebControls.FontUnit" /> on the left of the operator that contains font properties. </param>
		/// <param name="right">A <see cref="T:System.Web.UI.WebControls.FontUnit" /> on the right of the operator that contains font properties. </param>
		// Token: 0x06002400 RID: 9216 RVA: 0x0005B8F0 File Offset: 0x00059AF0
		public static bool operator ==(FontUnit left, FontUnit right)
		{
			return left.type == right.type && left.unit == right.unit;
		}

		/// <summary>Compares two <see cref="T:System.Web.UI.WebControls.FontUnit" /> objects for inequality.</summary>
		/// <returns>true if both <see cref="T:System.Web.UI.WebControls.FontUnit" /> objects are not equal; otherwise, false.</returns>
		/// <param name="left">A <see cref="T:System.Web.UI.WebControls.FontUnit" /> that contains font properties on the left of the operator. </param>
		/// <param name="right">A <see cref="T:System.Web.UI.WebControls.FontUnit" /> that contains font properties on the right of the operator. </param>
		// Token: 0x06002401 RID: 9217 RVA: 0x0005B913 File Offset: 0x00059B13
		public static bool operator !=(FontUnit left, FontUnit right)
		{
			return left.type != right.type || left.unit != right.unit;
		}

		/// <summary>Implicitly creates a <see cref="T:System.Web.UI.WebControls.FontUnit" /> of type <see cref="T:System.Drawing.Point" /> from an integer value.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.FontUnit" /> of type <see cref="T:System.Drawing.Point" /> that represents the font size specified the <paramref name="n" /> parameter.</returns>
		/// <param name="n">An integer representing the <see cref="T:System.Drawing.Point" /> value to convert into a <see cref="T:System.Web.UI.WebControls.FontUnit" />. </param>
		// Token: 0x06002402 RID: 9218 RVA: 0x0005B884 File Offset: 0x00059A84
		public static implicit operator FontUnit(int n)
		{
			return new FontUnit(n);
		}

		/// <summary>Converts a <see cref="T:System.Web.UI.WebControls.FontUnit" /> object to a string equivalent using the specified format provider.</summary>
		/// <returns>A string that represents this <see cref="T:System.Web.UI.WebControls.FontUnit" />, with any numeric unit value in the format specified by <paramref name="formatProvider" />.</returns>
		/// <param name="formatProvider">An <see cref="T:System.IFormatProvider" /> that supplies culture-specific formatting information, which is used if the <see cref="P:System.Web.UI.WebControls.FontUnit.Type" /> property is set to the <see cref="F:System.Web.UI.WebControls.FontSize.AsUnit" /> value; otherwise, it is ignored.</param>
		// Token: 0x06002403 RID: 9219 RVA: 0x0005B936 File Offset: 0x00059B36
		public string ToString(IFormatProvider formatProvider)
		{
			if (this.type == FontSize.NotSet)
			{
				return string.Empty;
			}
			if (this.type == FontSize.AsUnit)
			{
				return this.unit.ToString(formatProvider);
			}
			return FontUnit.font_size_names[(int)this.type];
		}

		/// <summary>Converts the <see cref="T:System.Web.UI.WebControls.FontUnit" /> object to a string representation, using the specified <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <returns>The string representation of the <see cref="T:System.Web.UI.WebControls.FontUnit" /> object in the specified culture.</returns>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that contains the culture to represent the <see cref="T:System.Web.UI.WebControls.FontUnit" />. </param>
		// Token: 0x06002404 RID: 9220 RVA: 0x0005B968 File Offset: 0x00059B68
		public string ToString(CultureInfo culture)
		{
			if (this.type == FontSize.NotSet)
			{
				return string.Empty;
			}
			if (this.type == FontSize.AsUnit)
			{
				return this.unit.ToString(culture);
			}
			return FontUnit.font_size_names[(int)this.type];
		}

		/// <summary>Converts the <see cref="T:System.Web.UI.WebControls.FontUnit" /> object to the default string representation.</summary>
		/// <returns>The string representation of the <see cref="T:System.Web.UI.WebControls.FontUnit" /> object.</returns>
		// Token: 0x06002405 RID: 9221 RVA: 0x0005B99A File Offset: 0x00059B9A
		public override string ToString()
		{
			return this.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x04001992 RID: 6546
		private FontSize type;

		// Token: 0x04001993 RID: 6547
		private Unit unit;

		/// <summary>Represents an empty <see cref="T:System.Web.UI.WebControls.FontUnit" /> object.</summary>
		// Token: 0x04001994 RID: 6548
		public static readonly FontUnit Empty;

		/// <summary>Represents a <see cref="T:System.Web.UI.WebControls.FontUnit" /> object with the <see cref="P:System.Web.UI.WebControls.FontUnit.Type" /> property set to FontSize.Smaller.</summary>
		// Token: 0x04001995 RID: 6549
		public static readonly FontUnit Smaller = new FontUnit(FontSize.Smaller);

		/// <summary>Represents a <see cref="T:System.Web.UI.WebControls.FontUnit" /> object with the <see cref="P:System.Web.UI.WebControls.FontUnit.Type" /> property set to FontSize.Larger.</summary>
		// Token: 0x04001996 RID: 6550
		public static readonly FontUnit Larger = new FontUnit(FontSize.Larger);

		/// <summary>Represents a <see cref="T:System.Web.UI.WebControls.FontUnit" /> object with the <see cref="P:System.Web.UI.WebControls.FontUnit.Type" /> property set to FontSize.XXSmall.</summary>
		// Token: 0x04001997 RID: 6551
		public static readonly FontUnit XXSmall = new FontUnit(FontSize.XXSmall);

		/// <summary>Represents a <see cref="T:System.Web.UI.WebControls.FontUnit" /> object with the <see cref="P:System.Web.UI.WebControls.FontUnit.Type" /> property set to FontSize.XSmall.</summary>
		// Token: 0x04001998 RID: 6552
		public static readonly FontUnit XSmall = new FontUnit(FontSize.XSmall);

		/// <summary>Represents a <see cref="T:System.Web.UI.WebControls.FontUnit" /> object with the <see cref="P:System.Web.UI.WebControls.FontUnit.Type" /> property set to FontSize.Small.</summary>
		// Token: 0x04001999 RID: 6553
		public static readonly FontUnit Small = new FontUnit(FontSize.Small);

		/// <summary>Represents a <see cref="T:System.Web.UI.WebControls.FontUnit" /> object with the <see cref="P:System.Web.UI.WebControls.FontUnit.Type" /> property set to FontSize.Medium.</summary>
		// Token: 0x0400199A RID: 6554
		public static readonly FontUnit Medium = new FontUnit(FontSize.Medium);

		/// <summary>Represents a <see cref="T:System.Web.UI.WebControls.FontUnit" /> object with the <see cref="P:System.Web.UI.WebControls.FontUnit.Type" /> property set to FontSize.Large.</summary>
		// Token: 0x0400199B RID: 6555
		public static readonly FontUnit Large = new FontUnit(FontSize.Large);

		/// <summary>Represents a <see cref="T:System.Web.UI.WebControls.FontUnit" /> object with the <see cref="P:System.Web.UI.WebControls.FontUnit.Type" /> property set to FontSize.XLarge.</summary>
		// Token: 0x0400199C RID: 6556
		public static readonly FontUnit XLarge = new FontUnit(FontSize.XLarge);

		/// <summary>Represents a <see cref="T:System.Web.UI.WebControls.FontUnit" /> object with the <see cref="P:System.Web.UI.WebControls.FontUnit.Type" /> property set to FontSize.XXLarge.</summary>
		// Token: 0x0400199D RID: 6557
		public static readonly FontUnit XXLarge = new FontUnit(FontSize.XXLarge);

		// Token: 0x0400199E RID: 6558
		private static string[] font_size_names = new string[]
		{
			null, null, "Smaller", "Larger", "XX-Small", "X-Small", "Small", "Medium", "Large", "X-Large",
			"XX-Large"
		};
	}
}
