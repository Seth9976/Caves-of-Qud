using System;
using Newtonsoft.Json;

namespace XRL
{
	// Token: 0x02000528 RID: 1320
	[JsonConverter(typeof(Version.Converter))]
	[Serializable]
	public struct Version : IComparable<Version>, IEquatable<Version>, IEquatable<Version>
	{
		// Token: 0x17000608 RID: 1544
		public int this[int Index]
		{
			get
			{
				if (Index == 0)
				{
					return this.Major;
				}
				if (Index == 1)
				{
					return this.Minor;
				}
				if (Index == 2)
				{
					return this.Build;
				}
				if (Index == 3)
				{
					return this.Revision;
				}
				throw new IndexOutOfRangeException();
			}
			set
			{
				if (Index == 0)
				{
					this.Major = value;
					return;
				}
				if (Index == 1)
				{
					this.Minor = value;
					return;
				}
				if (Index == 2)
				{
					this.Build = value;
					return;
				}
				if (Index == 3)
				{
					this.Revision = value;
					return;
				}
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x0600388C RID: 14476 RVA: 0x00115C73 File Offset: 0x00113E73
		public Version(int Major, int Minor = 0, int Build = 0, int Revision = 0)
		{
			this.Major = Major;
			this.Minor = Minor;
			this.Build = Build;
			this.Revision = Revision;
		}

		/// <remarks>
		/// <see cref="T:System.Version">System.Version</see> has a strange quirk where build and revision can be undefined/-1 to where 7.0 does not equal 7.0.0.0.
		/// We do not replicate this behaviour in <see cref="T:XRL.Version">XRL.Version</see> and force a minimum zero value.
		/// </remarks>
		// Token: 0x0600388D RID: 14477 RVA: 0x00115C92 File Offset: 0x00113E92
		public Version(Version Version)
		{
			this.Major = Version.Major;
			this.Minor = Version.Minor;
			this.Build = Math.Max(Version.Build, 0);
			this.Revision = Math.Max(Version.Revision, 0);
		}

		// Token: 0x0600388E RID: 14478 RVA: 0x00115CD0 File Offset: 0x00113ED0
		public Version(string Version)
		{
			if (!Version.TryParse(Version, out this.Major, out this.Minor, out this.Build, out this.Revision))
			{
				throw new FormatException("'" + Version + "' is not a valid version string.");
			}
		}

		// Token: 0x0600388F RID: 14479 RVA: 0x00115D10 File Offset: 0x00113F10
		public int CompareTo(Version Other)
		{
			if (this.Major != Other.Major)
			{
				if (this.Major > Other.Major)
				{
					return 1;
				}
				return -1;
			}
			else if (this.Minor != Other.Minor)
			{
				if (this.Minor > Other.Minor)
				{
					return 1;
				}
				return -1;
			}
			else if (this.Build != Other.Build)
			{
				if (this.Build > Other.Build)
				{
					return 1;
				}
				return -1;
			}
			else
			{
				if (this.Revision == Other.Revision)
				{
					return 0;
				}
				if (this.Revision > Other.Revision)
				{
					return 1;
				}
				return -1;
			}
		}

		// Token: 0x06003890 RID: 14480 RVA: 0x00115D9E File Offset: 0x00113F9E
		public override int GetHashCode()
		{
			return HashCode.Combine<int, int, int, int>(this.Major, this.Minor, this.Build, this.Revision);
		}

		// Token: 0x06003891 RID: 14481 RVA: 0x00115DC0 File Offset: 0x00113FC0
		public override bool Equals(object Other)
		{
			if (Other is Version)
			{
				Version version = (Version)Other;
				return this.Equals(version);
			}
			Version version2 = Other as Version;
			return version2 != null && this.Equals(version2);
		}

		// Token: 0x06003892 RID: 14482 RVA: 0x00115DF7 File Offset: 0x00113FF7
		public bool Equals(Version Other)
		{
			return this.Major == Other.Major && this.Minor == Other.Minor && this.Build == Other.Build && this.Revision == Other.Revision;
		}

		// Token: 0x06003893 RID: 14483 RVA: 0x00115E34 File Offset: 0x00114034
		public bool Equals(Version Other)
		{
			return Other != null && this.Major == Other.Major && this.Minor == Other.Minor && this.Build == Math.Max(Other.Build, 0) && this.Revision == Math.Max(Other.Revision, 0);
		}

		// Token: 0x06003894 RID: 14484 RVA: 0x00115E8C File Offset: 0x0011408C
		public unsafe bool EqualsSemantic(ReadOnlySpan<char> Range)
		{
			if (Range.IsEmpty)
			{
				return true;
			}
			if (Range.Length == 1 && (*Range[0] == 42 || *Range[0] == 120 || *Range[0] == 88))
			{
				return true;
			}
			foreach (ReadOnlySpan<char> readOnlySpan in Range.DelimitedBy("||"))
			{
				ReadOnlySpan<char> readOnlySpan2 = readOnlySpan.Trim();
				int num = readOnlySpan2.IndexOf('-');
				if (num != -1)
				{
					Version version = Version.ParseLowSemantic(readOnlySpan2.Slice(0, num).Trim());
					Version version2 = Version.ParseHighSemantic((num >= readOnlySpan2.Length - 1) ? ReadOnlySpan<char>.Empty : readOnlySpan2.Slice(num + 1).Trim());
					if (this >= version && this <= version2)
					{
						return true;
					}
				}
				bool flag = true;
				foreach (ReadOnlySpan<char> readOnlySpan3 in readOnlySpan2.DelimitedBy(' '))
				{
					ReadOnlySpan<char> readOnlySpan4 = readOnlySpan3.Trim();
					if (!readOnlySpan4.IsEmpty)
					{
						Version zero = Version.Zero;
						Version max = Version.Max;
						char c = (char)(*readOnlySpan4[0]);
						if (c == '^')
						{
							if (!Version.TryParseSemantic(readOnlySpan4.Slice(1), ref zero))
							{
								flag = false;
								break;
							}
							for (int i = 0; i < 4; i++)
							{
								max[i] = zero[i];
								if (zero[i] != 0)
								{
									break;
								}
							}
							if (this < zero || this > max)
							{
								flag = false;
								break;
							}
						}
						else if (c == '<')
						{
							if (*readOnlySpan4[1] == 61)
							{
								if (!Version.TryParseSemantic(readOnlySpan4.Slice(2), ref max) || this > max)
								{
									flag = false;
									break;
								}
							}
							else if (!Version.TryParseSemantic(readOnlySpan4.Slice(1), ref zero) || this >= zero)
							{
								flag = false;
								break;
							}
						}
						else if (c == '>')
						{
							if (*readOnlySpan4[1] == 61)
							{
								if (!Version.TryParseSemantic(readOnlySpan4.Slice(2), ref zero) || this < zero)
								{
									flag = false;
									break;
								}
							}
							else if (!Version.TryParseSemantic(readOnlySpan4.Slice(1), ref max) || this <= max)
							{
								flag = false;
								break;
							}
						}
						else
						{
							ReadOnlySpan<char> readOnlySpan5 = ((c == '=') ? readOnlySpan4.Slice(1) : readOnlySpan4);
							if (!Version.TryParseSemantic(readOnlySpan5, ref zero) || !Version.TryParseSemantic(readOnlySpan5, ref max) || this < zero || this > max)
							{
								flag = false;
								break;
							}
						}
					}
				}
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003895 RID: 14485 RVA: 0x0011615D File Offset: 0x0011435D
		public bool IsZero()
		{
			return this == Version.Zero;
		}

		// Token: 0x06003896 RID: 14486 RVA: 0x0011616F File Offset: 0x0011436F
		public override string ToString()
		{
			return this.ToString((this.Revision == 0) ? 3 : 4);
		}

		// Token: 0x06003897 RID: 14487 RVA: 0x00116184 File Offset: 0x00114384
		public unsafe string ToString(int Fields)
		{
			if (Fields == 0)
			{
				return string.Empty;
			}
			if (Fields == 1)
			{
				return this.Major.ToString();
			}
			int num = this.Major.CountDigits() + this.Minor.CountDigits() + 1;
			int num2 = 0;
			if (Fields >= 3)
			{
				num += this.Build.CountDigits() + 1;
			}
			if (Fields >= 4)
			{
				num += this.Revision.CountDigits() + 1;
			}
			int num3 = num;
			Span<char> span;
			checked
			{
				span = new Span<char>(stackalloc byte[unchecked((UIntPtr)num3) * 2], num3);
				(ref span).Insert(ref num2, this.Major);
			}
			*span[num2++] = '.';
			(ref span).Insert(ref num2, this.Minor);
			if (Fields >= 3)
			{
				*span[num2++] = '.';
				(ref span).Insert(ref num2, this.Build);
			}
			if (Fields >= 4)
			{
				*span[num2++] = '.';
				(ref span).Insert(ref num2, this.Revision);
			}
			return new string(span);
		}

		// Token: 0x06003898 RID: 14488 RVA: 0x00116279 File Offset: 0x00114479
		public static implicit operator Version(Version Version)
		{
			return new Version(Version);
		}

		// Token: 0x06003899 RID: 14489 RVA: 0x00116281 File Offset: 0x00114481
		public static bool operator ==(Version First, Version Second)
		{
			return First.Equals(Second);
		}

		// Token: 0x0600389A RID: 14490 RVA: 0x0011628B File Offset: 0x0011448B
		public static bool operator !=(Version First, Version Second)
		{
			return !First.Equals(Second);
		}

		// Token: 0x0600389B RID: 14491 RVA: 0x00116298 File Offset: 0x00114498
		public static bool operator <(Version First, Version Second)
		{
			return First.CompareTo(Second) < 0;
		}

		// Token: 0x0600389C RID: 14492 RVA: 0x001162A5 File Offset: 0x001144A5
		public static bool operator <=(Version First, Version Second)
		{
			return First.CompareTo(Second) <= 0;
		}

		// Token: 0x0600389D RID: 14493 RVA: 0x001162B5 File Offset: 0x001144B5
		public static bool operator >(Version First, Version Second)
		{
			return First.CompareTo(Second) > 0;
		}

		// Token: 0x0600389E RID: 14494 RVA: 0x001162C2 File Offset: 0x001144C2
		public static bool operator >=(Version First, Version Second)
		{
			return First.CompareTo(Second) >= 0;
		}

		// Token: 0x0600389F RID: 14495 RVA: 0x001162D2 File Offset: 0x001144D2
		public static bool TryParse(ReadOnlySpan<char> Text, out Version Version)
		{
			Version = default(Version);
			return Version.TryParse(Text, out Version.Major, out Version.Minor, out Version.Build, out Version.Revision);
		}

		// Token: 0x060038A0 RID: 14496 RVA: 0x001162FC File Offset: 0x001144FC
		public static bool TryParse(ReadOnlySpan<char> Text, out int Major, out int Minor, out int Build, out int Revision)
		{
			Major = (Minor = (Build = (Revision = 0)));
			if (Text.Length == 0)
			{
				return false;
			}
			int num = Text.IndexOf('.');
			if (num == -1)
			{
				return int.TryParse(Text, out Major);
			}
			if (!int.TryParse(Text.Slice(0, num), out Major))
			{
				return false;
			}
			Text = Text.Slice(num + 1);
			num = Text.IndexOf('.');
			if (num == -1)
			{
				return int.TryParse(Text, out Minor);
			}
			if (!int.TryParse(Text.Slice(0, num), out Minor))
			{
				return false;
			}
			Text = Text.Slice(num + 1);
			num = Text.IndexOf('.');
			if (num == -1)
			{
				return int.TryParse(Text, out Build);
			}
			if (!int.TryParse(Text.Slice(0, num), out Build))
			{
				return false;
			}
			Text = Text.Slice(num + 1);
			num = Text.IndexOf('.');
			return int.TryParse((num == -1) ? Text : Text.Slice(0, num), out Revision);
		}

		// Token: 0x060038A1 RID: 14497 RVA: 0x001163E4 File Offset: 0x001145E4
		public unsafe static bool TryParseSemantic(ReadOnlySpan<char> Text, ref Version Version)
		{
			int num = 0;
			foreach (ReadOnlySpan<char> readOnlySpan in Text.DelimitedBy('.'))
			{
				if (readOnlySpan.IsEmpty || *readOnlySpan[0] == 42 || *readOnlySpan[0] == 120 || *readOnlySpan[0] == 88)
				{
					break;
				}
				int num2;
				if (!int.TryParse(readOnlySpan, out num2))
				{
					return false;
				}
				Version[num] = num2;
				num++;
			}
			return true;
		}

		// Token: 0x060038A2 RID: 14498 RVA: 0x00116464 File Offset: 0x00114664
		public static Version ParseLowSemantic(ReadOnlySpan<char> Text)
		{
			Version zero = Version.Zero;
			Version.TryParseSemantic(Text, ref zero);
			return zero;
		}

		// Token: 0x060038A3 RID: 14499 RVA: 0x00116484 File Offset: 0x00114684
		public static Version ParseHighSemantic(ReadOnlySpan<char> Text)
		{
			Version max = Version.Max;
			Version.TryParseSemantic(Text, ref max);
			return max;
		}

		// Token: 0x04001C84 RID: 7300
		public static readonly Version Zero = new Version(0, 0, 0, 0);

		// Token: 0x04001C85 RID: 7301
		public static readonly Version Max = new Version(int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue);

		// Token: 0x04001C86 RID: 7302
		public const int Length = 4;

		// Token: 0x04001C87 RID: 7303
		public int Major;

		// Token: 0x04001C88 RID: 7304
		public int Minor;

		// Token: 0x04001C89 RID: 7305
		public int Build;

		// Token: 0x04001C8A RID: 7306
		public int Revision;

		// Token: 0x0200194F RID: 6479
		public class Converter : JsonConverter<Version>
		{
			// Token: 0x0600E315 RID: 58133 RVA: 0x00526357 File Offset: 0x00524557
			public override void WriteJson(JsonWriter writer, Version value, JsonSerializer serializer)
			{
				writer.WriteValue(value.ToString());
			}

			// Token: 0x0600E316 RID: 58134 RVA: 0x0052636C File Offset: 0x0052456C
			public override Version ReadJson(JsonReader reader, Type objectType, Version existingValue, bool hasExistingValue, JsonSerializer serializer)
			{
				if (reader.TokenType == JsonToken.String)
				{
					return new Version((string)reader.Value);
				}
				return Version.Zero;
			}
		}
	}
}
