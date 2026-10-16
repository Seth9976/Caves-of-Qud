using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a length measurement.</summary>
	// Token: 0x02000431 RID: 1073
	[TypeConverter(typeof(UnitConverter))]
	[Serializable]
	public struct Unit
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Unit" /> structure with the specified double precision floating point number and <see cref="T:System.Web.UI.WebControls.UnitType" />.</summary>
		/// <param name="value">A double precision floating point number that represents the length of the <see cref="T:System.Web.UI.WebControls.Unit" />. </param>
		/// <param name="type">One of the <see cref="T:System.Web.UI.WebControls.UnitType" /> enumeration values. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is not between -32768 and 32767. </exception>
		// Token: 0x06003176 RID: 12662 RVA: 0x000820D4 File Offset: 0x000802D4
		public Unit(double value, UnitType type)
		{
			if (value < -32768.0 || value > 32767.0)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			this.type = type;
			if (type == UnitType.Pixel)
			{
				this.value = (double)((int)value);
			}
			else
			{
				this.value = value;
			}
			this.valueSet = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Unit" /> structure with the specified double precision floating point number.</summary>
		/// <param name="value">A double precision floating point number that represents the length of the <see cref="T:System.Web.UI.WebControls.Unit" /> in pixels. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is not between -32768 and 32767. </exception>
		// Token: 0x06003177 RID: 12663 RVA: 0x00082128 File Offset: 0x00080328
		public Unit(double value)
		{
			this = new Unit(value, UnitType.Pixel);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Unit" /> structure with the specified 32-bit signed integer.</summary>
		/// <param name="value">A 32-bit signed integer that represents the length of the <see cref="T:System.Web.UI.WebControls.Unit" /> in pixels. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is not between -32768 and 32767. </exception>
		// Token: 0x06003178 RID: 12664 RVA: 0x00082132 File Offset: 0x00080332
		public Unit(int value)
		{
			this = new Unit((double)value, UnitType.Pixel);
		}

		// Token: 0x06003179 RID: 12665 RVA: 0x00082140 File Offset: 0x00080340
		internal Unit(string input, char sep)
		{
			if (input == null || input == string.Empty)
			{
				this.type = (UnitType)0;
				this.value = 0.0;
				this.valueSet = false;
				return;
			}
			this.value = 0.0;
			double num = 0.0;
			double num2 = 0.1;
			int num3 = 0;
			int length = input.Length;
			int num4 = 1;
			int num5 = -1;
			int num6 = 0;
			int num7 = 0;
			Unit.ParsingStage parsingStage = Unit.ParsingStage.Trim;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			while (!flag && num3 < length)
			{
				char c = input[num3];
				switch (parsingStage)
				{
				case Unit.ParsingStage.Trim:
					if (char.IsWhiteSpace(c))
					{
						num3++;
					}
					else
					{
						parsingStage = Unit.ParsingStage.SignOrSep;
					}
					break;
				case Unit.ParsingStage.SignOrSep:
					num7 = 0;
					if (c == '-')
					{
						num4 = -1;
						num3++;
						parsingStage = Unit.ParsingStage.DigitOrSep;
					}
					else if (c == sep)
					{
						num3++;
						flag2 = true;
						parsingStage = Unit.ParsingStage.DigitOrUnit;
						num = 0.0;
					}
					else
					{
						if (!char.IsDigit(c))
						{
							throw new FormatException();
						}
						parsingStage = Unit.ParsingStage.DigitOrSep;
					}
					break;
				case Unit.ParsingStage.DigitOrSep:
					if (char.IsDigit(c))
					{
						num = num * 10.0 + (double)c - 48.0;
						num3++;
						flag3 = true;
					}
					else if (c == sep)
					{
						if (num7 > 0)
						{
							throw new ArgumentOutOfRangeException("input");
						}
						num3++;
						flag2 = true;
						this.value = num * (double)num4;
						num = 0.0;
						parsingStage = Unit.ParsingStage.DigitOrUnit;
					}
					else
					{
						bool flag4 = char.IsWhiteSpace(c);
						if (!flag4 && c != '%' && !char.IsLetter(c))
						{
							throw new FormatException();
						}
						if (flag4)
						{
							if (!flag3)
							{
								throw new ArgumentOutOfRangeException("input");
							}
							num7++;
							num3++;
						}
						else
						{
							this.value = num * (double)num4;
							num = 0.0;
							num5 = num3;
							if (flag2)
							{
								flag3 = false;
								parsingStage = Unit.ParsingStage.DigitOrUnit;
							}
							else
							{
								parsingStage = Unit.ParsingStage.Unit;
							}
							num7 = 0;
						}
					}
					break;
				case Unit.ParsingStage.DigitOrUnit:
					if (c == '%')
					{
						num5 = num3;
						num6 = 1;
						flag = true;
					}
					else
					{
						bool flag4 = char.IsWhiteSpace(c);
						if (flag4 || char.IsLetter(c))
						{
							if (flag4)
							{
								num7++;
								num3++;
							}
							else
							{
								parsingStage = Unit.ParsingStage.Unit;
								num5 = num3;
							}
						}
						else
						{
							if (!char.IsDigit(c))
							{
								throw new FormatException();
							}
							if (num7 > 0)
							{
								throw new ArgumentOutOfRangeException();
							}
							num += (double)(c - '0') * num2;
							num2 *= 0.1;
							num3++;
						}
					}
					break;
				case Unit.ParsingStage.Unit:
					if (c == '%' || char.IsLetter(c))
					{
						num3++;
						num6++;
					}
					else if (num6 == 0 && char.IsWhiteSpace(c))
					{
						num3++;
						num5++;
					}
					else
					{
						flag = true;
					}
					break;
				}
			}
			this.value += num * (double)num4;
			if (num5 >= 0)
			{
				int num8 = num5 + num6;
				if (num8 < length)
				{
					for (int i = num8; i < length; i++)
					{
						if (!char.IsWhiteSpace(input[i]))
						{
							throw new ArgumentOutOfRangeException("input");
						}
					}
				}
				if (num6 != 1 || input[num5] != '%')
				{
					string text = input.Substring(num5, num6).ToLower(Helpers.InvariantCulture);
					uint num9 = global::<PrivateImplementationDetails>.ComputeStringHash(text);
					if (num9 <= 1313756516U)
					{
						if (num9 <= 1094220446U)
						{
							if (num9 != 1075471351U)
							{
								if (num9 == 1094220446U)
								{
									if (text == "in")
									{
										this.type = UnitType.Inch;
										goto IL_0494;
									}
								}
							}
							else if (text == "em")
							{
								this.type = UnitType.Em;
								goto IL_0494;
							}
						}
						else if (num9 != 1260025160U)
						{
							if (num9 == 1313756516U)
							{
								if (text == "pc")
								{
									this.type = UnitType.Pica;
									goto IL_0494;
								}
							}
						}
						else if (text == "ex")
						{
							this.type = UnitType.Ex;
							goto IL_0494;
						}
					}
					else if (num9 <= 1565420801U)
					{
						if (num9 != 1498310325U)
						{
							if (num9 == 1565420801U)
							{
								if (text == "pt")
								{
									this.type = UnitType.Point;
									goto IL_0494;
								}
							}
						}
						else if (text == "px")
						{
							this.type = UnitType.Pixel;
							goto IL_0494;
						}
					}
					else if (num9 != 1613635087U)
					{
						if (num9 == 1680451373U)
						{
							if (text == "cm")
							{
								this.type = UnitType.Cm;
								goto IL_0494;
							}
						}
					}
					else if (text == "mm")
					{
						this.type = UnitType.Mm;
						goto IL_0494;
					}
					throw new ArgumentOutOfRangeException("value");
				}
				this.type = UnitType.Percentage;
			}
			else
			{
				this.type = UnitType.Pixel;
			}
			IL_0494:
			if (flag2 && this.type == UnitType.Pixel)
			{
				throw new FormatException("Pixel units do not allow floating point values");
			}
			this.valueSet = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Unit" /> structure with the specified length.</summary>
		/// <param name="value">A string that represents the length of the <see cref="T:System.Web.UI.WebControls.Unit" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified length is not between -32768 and 32767. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not a valid CSS-compliant unit expression. </exception>
		// Token: 0x0600317A RID: 12666 RVA: 0x00082600 File Offset: 0x00080800
		public Unit(string value)
		{
			this = new Unit(value, '.');
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Unit" /> structure with the specified length and <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <param name="value">A string that represents the length of the <see cref="T:System.Web.UI.WebControls.Unit" />. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that represents the culture. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified length is not between -32768 and 32767. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not a valid CSS-compliant unit expression. </exception>
		// Token: 0x0600317B RID: 12667 RVA: 0x0008260B File Offset: 0x0008080B
		public Unit(string value, CultureInfo culture)
		{
			this = new Unit(value, culture.NumberFormat.NumberDecimalSeparator[0]);
		}

		// Token: 0x0600317C RID: 12668 RVA: 0x00082600 File Offset: 0x00080800
		internal Unit(string value, CultureInfo culture, UnitType t)
		{
			this = new Unit(value, '.');
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.Unit" /> is empty.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.Unit" /> is empty; otherwise, false.</returns>
		// Token: 0x17000FBA RID: 4026
		// (get) Token: 0x0600317D RID: 12669 RVA: 0x00082625 File Offset: 0x00080825
		public bool IsEmpty
		{
			get
			{
				return this.type == (UnitType)0;
			}
		}

		/// <summary>Gets the unit type of the <see cref="T:System.Web.UI.WebControls.Unit" />.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.UnitType" /> enumeration values. The default is <see cref="F:System.Web.UI.WebControls.UnitType.Pixel" />.</returns>
		// Token: 0x17000FBB RID: 4027
		// (get) Token: 0x0600317E RID: 12670 RVA: 0x00082630 File Offset: 0x00080830
		public UnitType Type
		{
			get
			{
				if (this.type == (UnitType)0)
				{
					return UnitType.Pixel;
				}
				return this.type;
			}
		}

		/// <summary>Gets the length of the <see cref="T:System.Web.UI.WebControls.Unit" />.</summary>
		/// <returns>A double-precision floating point number that represents the length of the <see cref="T:System.Web.UI.WebControls.Unit" />.</returns>
		// Token: 0x17000FBC RID: 4028
		// (get) Token: 0x0600317F RID: 12671 RVA: 0x00082642 File Offset: 0x00080842
		public double Value
		{
			get
			{
				return this.value;
			}
		}

		/// <summary>Converts the specified string to a <see cref="T:System.Web.UI.WebControls.Unit" />.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> that represents the specified string.</returns>
		/// <param name="s">The string to convert. </param>
		// Token: 0x06003180 RID: 12672 RVA: 0x0008264A File Offset: 0x0008084A
		public static Unit Parse(string s)
		{
			return new Unit(s);
		}

		/// <summary>Converts the specified string and <see cref="T:System.Globalization.CultureInfo" /> to a <see cref="T:System.Web.UI.WebControls.Unit" />.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> that represents the specified string.</returns>
		/// <param name="s">The string to convert. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object that represents the culture. </param>
		// Token: 0x06003181 RID: 12673 RVA: 0x00082652 File Offset: 0x00080852
		public static Unit Parse(string s, CultureInfo culture)
		{
			return new Unit(s, culture);
		}

		/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.Unit" /> of type <see cref="F:System.Web.UI.WebControls.UnitType.Percentage" /> from the specified double-precision floating-point number.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> of type <see cref="F:System.Web.UI.WebControls.UnitType.Percentage" /> that represents the length specified by the double-precision floating-point number.</returns>
		/// <param name="n">A double-precision floating-point number that represents the length of the <see cref="T:System.Web.UI.WebControls.Unit" />.</param>
		// Token: 0x06003182 RID: 12674 RVA: 0x0008265B File Offset: 0x0008085B
		public static Unit Percentage(double n)
		{
			return new Unit(n, UnitType.Percentage);
		}

		/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.Unit" /> of type <see cref="F:System.Web.UI.WebControls.UnitType.Pixel" /> from the specified 32-bit signed integer.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> of type <see cref="F:System.Web.UI.WebControls.UnitType.Pixel" /> that represents the length specified by the <paramref name="n" /> parameter.</returns>
		/// <param name="n">A 32-bit signed integer that represents the length of the <see cref="T:System.Web.UI.WebControls.Unit" />. </param>
		// Token: 0x06003183 RID: 12675 RVA: 0x00082664 File Offset: 0x00080864
		public static Unit Pixel(int n)
		{
			return new Unit(n);
		}

		/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.Unit" /> of type <see cref="F:System.Web.UI.WebControls.UnitType.Point" /> from the specified 32-bit signed integer.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> of type <see cref="F:System.Web.UI.WebControls.UnitType.Point" /> that represents the length specified by the 32-bit signed integer.</returns>
		/// <param name="n">A 32-bit signed integer that represents the length of the <see cref="T:System.Web.UI.WebControls.Unit" />. </param>
		// Token: 0x06003184 RID: 12676 RVA: 0x0008266C File Offset: 0x0008086C
		public static Unit Point(int n)
		{
			return new Unit((double)n, UnitType.Point);
		}

		/// <summary>Compares this <see cref="T:System.Web.UI.WebControls.Unit" /> with the specified object.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.Unit" /> that this method is called from is equal to the specified object; otherwise, false.</returns>
		/// <param name="obj">The object for comparison. </param>
		// Token: 0x06003185 RID: 12677 RVA: 0x00082678 File Offset: 0x00080878
		public override bool Equals(object obj)
		{
			if (obj is Unit)
			{
				Unit unit = (Unit)obj;
				return unit.type == this.type && unit.value == this.value && this.valueSet == unit.valueSet;
			}
			return false;
		}

		/// <summary>Returns a hash code for this <see cref="T:System.Web.UI.WebControls.Unit" />.</summary>
		/// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
		// Token: 0x06003186 RID: 12678 RVA: 0x000826C4 File Offset: 0x000808C4
		public override int GetHashCode()
		{
			return this.Type.GetHashCode() ^ this.Value.GetHashCode();
		}

		/// <summary>Compares two <see cref="T:System.Web.UI.WebControls.Unit" /> objects to determine whether they are equal.</summary>
		/// <returns>true if both <see cref="T:System.Web.UI.WebControls.Unit" /> objects are equal; otherwise, false.</returns>
		/// <param name="left">The <see cref="T:System.Web.UI.WebControls.Unit" /> on the left side of the operator. </param>
		/// <param name="right">The <see cref="T:System.Web.UI.WebControls.Unit" /> on the right side of the operator. </param>
		// Token: 0x06003187 RID: 12679 RVA: 0x000826F4 File Offset: 0x000808F4
		public static bool operator ==(Unit left, Unit right)
		{
			return left.Type == right.Type && left.Value == right.Value && left.valueSet == right.valueSet;
		}

		/// <summary>Compares two <see cref="T:System.Web.UI.WebControls.Unit" /> objects to determine whether they are not equal.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.Unit" /> objects are not equal; otherwise, false.</returns>
		/// <param name="left">The <see cref="T:System.Web.UI.WebControls.Unit" /> on the left side of the operator. </param>
		/// <param name="right">The <see cref="T:System.Web.UI.WebControls.Unit" /> on the right side of the operator. </param>
		// Token: 0x06003188 RID: 12680 RVA: 0x00082726 File Offset: 0x00080926
		public static bool operator !=(Unit left, Unit right)
		{
			return left.Type != right.Type || left.Value != right.Value || left.valueSet != right.valueSet;
		}

		/// <summary>Implicitly creates a <see cref="T:System.Web.UI.WebControls.Unit" /> of type <see cref="F:System.Web.UI.WebControls.UnitType.Pixel" /> from the specified 32-bit unsigned integer.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> of type <see cref="F:System.Web.UI.WebControls.UnitType.Pixel" /> that represents the 32-bit unsigned integer specified by the <paramref name="n" /> parameter.</returns>
		/// <param name="n">A 32-bit signed integer that represents the length of the <see cref="T:System.Web.UI.WebControls.Unit" />. </param>
		// Token: 0x06003189 RID: 12681 RVA: 0x00082664 File Offset: 0x00080864
		public static implicit operator Unit(int n)
		{
			return new Unit(n);
		}

		// Token: 0x0600318A RID: 12682 RVA: 0x0008275C File Offset: 0x0008095C
		internal static string GetExtension(UnitType type)
		{
			switch (type)
			{
			case UnitType.Pixel:
				return "px";
			case UnitType.Point:
				return "pt";
			case UnitType.Pica:
				return "pc";
			case UnitType.Inch:
				return "in";
			case UnitType.Mm:
				return "mm";
			case UnitType.Cm:
				return "cm";
			case UnitType.Percentage:
				return "%";
			case UnitType.Em:
				return "em";
			case UnitType.Ex:
				return "ex";
			default:
				return string.Empty;
			}
		}

		/// <summary>Converts a <see cref="T:System.Web.UI.WebControls.Unit" /> to a string equivalent in the specified culture.</summary>
		/// <returns>A <see cref="T:System.String" /> represents this <see cref="T:System.Web.UI.WebControls.Unit" /> in the culture specified by <paramref name="culture" />.</returns>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that represents the culture. </param>
		// Token: 0x0600318B RID: 12683 RVA: 0x000827D4 File Offset: 0x000809D4
		public string ToString(CultureInfo culture)
		{
			if (this.type == (UnitType)0)
			{
				return string.Empty;
			}
			string extension = Unit.GetExtension(this.type);
			return this.value.ToString(culture) + extension;
		}

		/// <summary>Converts a <see cref="T:System.Web.UI.WebControls.Unit" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents this <see cref="T:System.Web.UI.WebControls.Unit" />.</returns>
		// Token: 0x0600318C RID: 12684 RVA: 0x0008280D File Offset: 0x00080A0D
		public override string ToString()
		{
			return this.ToString(Helpers.InvariantCulture);
		}

		/// <summary>Converts a <see cref="T:System.Web.UI.WebControls.Unit" /> to a string equivalent using the specified format provider.</summary>
		/// <returns>A <see cref="T:System.String" /> representing this <see cref="T:System.Web.UI.WebControls.Unit" /> in the format specified by <paramref name="formatProvider" />.</returns>
		/// <param name="formatProvider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information.</param>
		// Token: 0x0600318D RID: 12685 RVA: 0x0008281C File Offset: 0x00080A1C
		public string ToString(IFormatProvider formatProvider)
		{
			if (this.type == (UnitType)0)
			{
				return string.Empty;
			}
			string extension = Unit.GetExtension(this.type);
			return this.value.ToString(formatProvider) + extension;
		}

		// Token: 0x04001C4E RID: 7246
		private UnitType type;

		// Token: 0x04001C4F RID: 7247
		private double value;

		// Token: 0x04001C50 RID: 7248
		private bool valueSet;

		/// <summary>Represents an empty <see cref="T:System.Web.UI.WebControls.Unit" />. This field is read-only.</summary>
		// Token: 0x04001C51 RID: 7249
		public static readonly Unit Empty;

		// Token: 0x02000432 RID: 1074
		private enum ParsingStage
		{
			// Token: 0x04001C53 RID: 7251
			Trim,
			// Token: 0x04001C54 RID: 7252
			SignOrSep,
			// Token: 0x04001C55 RID: 7253
			DigitOrSep,
			// Token: 0x04001C56 RID: 7254
			DigitOrUnit,
			// Token: 0x04001C57 RID: 7255
			Unit
		}
	}
}
