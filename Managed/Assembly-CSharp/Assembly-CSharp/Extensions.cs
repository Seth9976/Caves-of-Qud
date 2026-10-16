using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Xml;
using ConsoleLib.Console;
using Cysharp.Text;
using Genkit;
using Newtonsoft.Json;
using Platform.IO;
using Qud.UI;
using UnityEngine;
using Wintellect.PowerCollections;
using XRL;
using XRL.Collections;
using XRL.Language;
using XRL.Rules;
using XRL.UI;
using XRL.World;
using XRL.World.Text;
using XRL.World.Text.Delegates;

// Token: 0x0200002F RID: 47
public static class Extensions
{
	// Token: 0x060000B0 RID: 176 RVA: 0x000058A8 File Offset: 0x00003AA8
	public static bool CompareTo(this StringBuilder s1, string s2)
	{
		if (s1 == null && s2 == null)
		{
			return true;
		}
		if (s1 == null || s2 == null)
		{
			return false;
		}
		if (s1.Length != s2.Length)
		{
			return false;
		}
		for (int i = 0; i < s1.Length; i++)
		{
			if (s1[i] != s2[i])
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x000058F9 File Offset: 0x00003AF9
	public static string Strip(this string s)
	{
		return ConsoleLib.Console.ColorUtility.StripFormatting(s);
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x00005901 File Offset: 0x00003B01
	public static string Color(this string String, string Color)
	{
		return ConsoleLib.Console.ColorUtility.ApplyColor(String, Color);
	}

	// Token: 0x060000B3 RID: 179 RVA: 0x0000590A File Offset: 0x00003B0A
	public static string Color(this string String, char Color)
	{
		return ConsoleLib.Console.ColorUtility.ApplyColor(String, Color);
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x00005913 File Offset: 0x00003B13
	public static string Capitalize(this string s)
	{
		return ConsoleLib.Console.ColorUtility.CapitalizeExceptFormatting(s);
	}

	// Token: 0x060000B5 RID: 181 RVA: 0x0000591B File Offset: 0x00003B1B
	public static string Pluralize(this string s)
	{
		return Grammar.Pluralize(s);
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x00005923 File Offset: 0x00003B23
	public static int StrippedLength(this string s)
	{
		return ConsoleLib.Console.ColorUtility.LengthExceptFormatting(s);
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x0000592B File Offset: 0x00003B2B
	public static int CompareExceptFormatting(this string Text, string Vs)
	{
		return ConsoleLib.Console.ColorUtility.CompareExceptFormatting(Text, Vs);
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x00005934 File Offset: 0x00003B34
	public static int CompareExceptFormattingAndCase(this string Text, string Vs)
	{
		return ConsoleLib.Console.ColorUtility.CompareExceptFormattingAndCase(Text, Vs);
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x0000593D File Offset: 0x00003B3D
	public static bool EqualsExceptFormatting(this string Text, string Vs)
	{
		return ConsoleLib.Console.ColorUtility.EqualsExceptFormatting(Text, Vs);
	}

	// Token: 0x060000BA RID: 186 RVA: 0x00005946 File Offset: 0x00003B46
	public static bool EqualsExceptFormattingAndCase(this string Text, string Vs)
	{
		return ConsoleLib.Console.ColorUtility.EqualsExceptFormattingAndCase(Text, Vs);
	}

	// Token: 0x060000BB RID: 187 RVA: 0x0000594F File Offset: 0x00003B4F
	public static bool IsNullOrEmpty<T>([NotNullWhen(false)] this ICollection<T> list)
	{
		return list == null || list.Count < 1;
	}

	// Token: 0x060000BC RID: 188 RVA: 0x0000595F File Offset: 0x00003B5F
	public static bool IsReadOnlyNullOrEmpty<T>([NotNullWhen(false)] this IReadOnlyCollection<T> list)
	{
		return list == null || list.Count < 1;
	}

	// Token: 0x060000BD RID: 189 RVA: 0x0000596F File Offset: 0x00003B6F
	public static bool IsNullOrEmpty<T>([NotNullWhen(false)] this IEnumerable<T> list)
	{
		return list == null || !list.Any<T>();
	}

	// Token: 0x060000BE RID: 190 RVA: 0x0000597F File Offset: 0x00003B7F
	public static bool IsNullOrEmpty([NotNullWhen(false)] this string value)
	{
		return value == null || value.Length == 0;
	}

	// Token: 0x060000BF RID: 191 RVA: 0x0000598F File Offset: 0x00003B8F
	public static bool IsNullOrEmpty([NotNullWhen(false)] this StringBuilder value)
	{
		return value == null || value.Length == 0;
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x0000599F File Offset: 0x00003B9F
	public static bool IsNullOrEmpty([NotNullWhen(false)] this GlobalLocation value)
	{
		return ((value != null) ? value.World : null) == null;
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x000059B0 File Offset: 0x00003BB0
	public static T ElementAtOrDefault<T>(this IReadOnlyList<T> List, int Index, T Default = default(T))
	{
		if (List == null || Index >= List.Count)
		{
			return Default;
		}
		return List[Index];
	}

	// Token: 0x060000C2 RID: 194 RVA: 0x000059C7 File Offset: 0x00003BC7
	public static string Coalesce(this string First, string Second)
	{
		if (!First.IsNullOrEmpty())
		{
			return First;
		}
		if (!Second.IsNullOrEmpty())
		{
			return Second;
		}
		return "";
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x000059E2 File Offset: 0x00003BE2
	public static string Coalesce(this string First, string Second, string Third)
	{
		if (!First.IsNullOrEmpty())
		{
			return First;
		}
		if (!Second.IsNullOrEmpty())
		{
			return Second;
		}
		if (!Third.IsNullOrEmpty())
		{
			return Third;
		}
		return "";
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x00005A07 File Offset: 0x00003C07
	public static string Coalesce(this string First, string Second, string Third, string Fourth)
	{
		if (!First.IsNullOrEmpty())
		{
			return First;
		}
		if (!Second.IsNullOrEmpty())
		{
			return Second;
		}
		if (!Third.IsNullOrEmpty())
		{
			return Third;
		}
		if (!Fourth.IsNullOrEmpty())
		{
			return Fourth;
		}
		return "";
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x00005A36 File Offset: 0x00003C36
	public static string Coalesce(this string First, string Second, string Third, string Fourth, string Fifth)
	{
		if (!First.IsNullOrEmpty())
		{
			return First;
		}
		if (!Second.IsNullOrEmpty())
		{
			return Second;
		}
		if (!Third.IsNullOrEmpty())
		{
			return Third;
		}
		if (!Fourth.IsNullOrEmpty())
		{
			return Fourth;
		}
		if (!Fifth.IsNullOrEmpty())
		{
			return Fifth;
		}
		return "";
	}

	// Token: 0x060000C6 RID: 198 RVA: 0x00005A74 File Offset: 0x00003C74
	public static string Coalesce(this string First, params string[] Rest)
	{
		if (!First.IsNullOrEmpty())
		{
			return First;
		}
		for (int i = 0; i < Rest.Length; i++)
		{
			if (!Rest[i].IsNullOrEmpty())
			{
				return Rest[i];
			}
		}
		return "";
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x00005AAC File Offset: 0x00003CAC
	public static string Replace(this string Text, string Old, string New, StringComparison Comparison, bool RespectTitleCase)
	{
		int num = Text.IndexOf(Old, Comparison);
		int num2 = 0;
		if (num == -1)
		{
			return Text;
		}
		StringBuilder stringBuilder = The.StringBuilder;
		while (num != -1)
		{
			stringBuilder.Append(Text, num2, num - num2);
			int length = stringBuilder.Length;
			stringBuilder.Append(New);
			if (RespectTitleCase)
			{
				char c = stringBuilder[length];
				(ref c).ToCaseOf(Text[num]);
				stringBuilder[length] = c;
			}
			num += Old.Length;
			num2 = num;
			num = Text.IndexOf(Old, num, Comparison);
		}
		stringBuilder.Append(Text, num2, Text.Length - num2);
		return stringBuilder.ToString();
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x00005B43 File Offset: 0x00003D43
	public static void ToCaseOf(this char Character, char Of)
	{
		if (char.IsUpper(Character))
		{
			if (char.IsUpper(Of))
			{
				return;
			}
			Character = char.ToLowerInvariant(Character);
		}
		if (char.IsUpper(Of))
		{
			Character = char.ToUpperInvariant(Character);
		}
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x00005B71 File Offset: 0x00003D71
	public static bool IsDirectorySeparator(this char Value)
	{
		return Value == Platform.IO.Path.DirectorySeparatorChar || Value == Platform.IO.Path.AltDirectorySeparatorChar;
	}

	// Token: 0x060000CA RID: 202 RVA: 0x00005B85 File Offset: 0x00003D85
	public static bool IsDirectorySeparatorInvariant(this char Value)
	{
		return Value == '/' || Value == '\\';
	}

	// Token: 0x060000CB RID: 203 RVA: 0x00005B94 File Offset: 0x00003D94
	public static string WithTrailingDirectorySeparator(this string Value)
	{
		if (Value.IsNullOrEmpty())
		{
			return Platform.IO.Path.DirectorySeparatorChar.GetString();
		}
		if (!Value[Value.Length - 1].IsDirectorySeparator())
		{
			return Value + Platform.IO.Path.DirectorySeparatorChar.ToString();
		}
		return Value;
	}

	// Token: 0x060000CC RID: 204 RVA: 0x00005BE0 File Offset: 0x00003DE0
	public static string WithLeadingDirectorySeparator(this string Value)
	{
		if (Value.IsNullOrEmpty())
		{
			return Platform.IO.Path.DirectorySeparatorChar.GetString();
		}
		if (!Value[0].IsDirectorySeparator())
		{
			return Platform.IO.Path.DirectorySeparatorChar.ToString() + Value;
		}
		return Value;
	}

	// Token: 0x060000CD RID: 205 RVA: 0x00005C23 File Offset: 0x00003E23
	public static string GetString(this char Value)
	{
		if ((int)Value < CharCache.Length)
		{
			return CharCache.Values[(int)Value];
		}
		return Value.ToString();
	}

	// Token: 0x060000CE RID: 206 RVA: 0x00005C3C File Offset: 0x00003E3C
	public static bool Contains(this string text, string find, CompareOptions comp)
	{
		return CultureInfo.InvariantCulture.CompareInfo.IndexOf(text, find, comp) >= 0;
	}

	// Token: 0x060000CF RID: 207 RVA: 0x00005C58 File Offset: 0x00003E58
	public static bool TryParseRange(this string Range, out int Low, out int High)
	{
		High = 0;
		int num = Range.IndexOf('-');
		if (num < 0)
		{
			bool flag = int.TryParse(Range, out Low);
			High = Low;
			return flag;
		}
		return int.TryParse(Range.Substring(0, num), out Low) && int.TryParse(Range.Substring(num + 1), out High);
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x00005CA2 File Offset: 0x00003EA2
	public static void SetBit(this int field, int mask, bool value)
	{
		if (value)
		{
			field |= mask;
			return;
		}
		field &= ~mask;
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x00005CB5 File Offset: 0x00003EB5
	public static void SetBit(this uint field, uint mask, bool value)
	{
		if (value)
		{
			field |= mask;
			return;
		}
		field &= ~mask;
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x00005CC8 File Offset: 0x00003EC8
	public static bool HasBit(this int field, int mask)
	{
		return (field & mask) != 0;
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x00005CD0 File Offset: 0x00003ED0
	public static bool HasBit(this uint field, uint mask)
	{
		return (field & mask) > 0U;
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x00005CD8 File Offset: 0x00003ED8
	public static bool HasAllBits(this int field, int mask)
	{
		return (field & mask) == mask;
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x00005CE0 File Offset: 0x00003EE0
	public static bool HasAllBits(this uint field, uint mask)
	{
		return (field & mask) == mask;
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x00005CE8 File Offset: 0x00003EE8
	public static int CountBits(this int field)
	{
		field -= (field >> 1) & 1431655765;
		field = (field & 858993459) + ((field >> 2) & 858993459);
		field = (field + (field >> 4)) & 252645135;
		return field * 16843009 >> 24;
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x00005D21 File Offset: 0x00003F21
	public static int CountDigits(this int Number)
	{
		if (Number == 0)
		{
			return 1;
		}
		return (((long)Number > 0L) ? 1 : 2) + (int)Math.Log10((double)Math.Abs(Number));
	}

	// Token: 0x060000D8 RID: 216 RVA: 0x00005D40 File Offset: 0x00003F40
	public static void Insert(this Span<char> Text, int Index, int Value)
	{
		(ref Text).Insert(ref Index, Value);
	}

	// Token: 0x060000D9 RID: 217 RVA: 0x00005D4C File Offset: 0x00003F4C
	public unsafe static void Insert(this Span<char> Text, ref int Index, int Value)
	{
		if (Value < 0)
		{
			int num = Index;
			Index = num + 1;
			*Text[num] = '-';
			Value *= -1;
		}
		int i = Index;
		do
		{
			int num2 = Value % 10;
			Value /= 10;
			int num = Index;
			Index = num + 1;
			*Text[num] = (char)(48 + num2);
		}
		while (Value > 0);
		int num3 = Index - 1;
		while (i < num3)
		{
			char c = *Text[i];
			*Text[i++] = *Text[num3];
			*Text[num3--] = c;
		}
	}

	// Token: 0x060000DA RID: 218 RVA: 0x00005DD4 File Offset: 0x00003FD4
	public static Dictionary<T, int> Increment<T>(this Dictionary<T, int> dict, T key)
	{
		int num;
		dict.TryGetValue(key, out num);
		dict[key] = num + 1;
		return dict;
	}

	// Token: 0x060000DB RID: 219 RVA: 0x00005DF8 File Offset: 0x00003FF8
	public static V GetValue<K, V>(this Dictionary<K, V> Self, K Key, V Default = default(V))
	{
		V v;
		if (Key == null || !Self.TryGetValue(Key, out v))
		{
			return Default;
		}
		return v;
	}

	// Token: 0x060000DC RID: 220 RVA: 0x00005E1C File Offset: 0x0000401C
	public static V PullValue<K, V>(this Dictionary<K, V> Self, K Key, V Default = default(V))
	{
		V v;
		if (Key != null && Self.TryGetValue(Key, out v))
		{
			Self.Remove(Key);
			return v;
		}
		return Default;
	}

	// Token: 0x060000DD RID: 221 RVA: 0x00005E48 File Offset: 0x00004048
	public static int RemoveAll<K, V>(this Dictionary<K, V> Self, Predicate<KeyValuePair<K, V>> Predicate)
	{
		List<K> keys = global::Extensions.RemoveKeyCache<K>.Keys;
		int num = 0;
		try
		{
			foreach (KeyValuePair<K, V> keyValuePair in Self)
			{
				if (Predicate(keyValuePair))
				{
					keys.Add(keyValuePair.Key);
				}
			}
			foreach (K k in keys)
			{
				if (Self.Remove(k))
				{
					num++;
				}
			}
		}
		finally
		{
			keys.Clear();
		}
		return num;
	}

	// Token: 0x060000DE RID: 222 RVA: 0x00005F08 File Offset: 0x00004108
	public static int RemoveAll<K, V>(this Dictionary<K, V> Self, IList<K> Keys)
	{
		int num = 0;
		int i = 0;
		int count = Keys.Count;
		while (i < count)
		{
			if (Self.Remove(Keys[i]))
			{
				num++;
			}
			i++;
		}
		return num;
	}

	// Token: 0x060000DF RID: 223 RVA: 0x00005F3E File Offset: 0x0000413E
	public static string ToRTFCached(this string s, int blockWrap = -1, bool stripFormatting = false)
	{
		if (s == null)
		{
			return RTF.FormatToRTF(s, "FF", blockWrap, stripFormatting);
		}
		return global::Extensions.CachedRTFExpansions.Get(new ValueTuple<string, int, bool>(s, blockWrap, stripFormatting));
	}

	// Token: 0x060000E0 RID: 224 RVA: 0x00005F63 File Offset: 0x00004163
	public static string ToStringCached(this int i)
	{
		return global::Extensions.CachedIntStringifications.Get(i);
	}

	// Token: 0x060000E1 RID: 225 RVA: 0x00005F70 File Offset: 0x00004170
	public static List<string> CachedCommaExpansion(this string text)
	{
		if (text == null)
		{
			return null;
		}
		return global::Extensions.CachedCommaExpansions.Get(text);
	}

	// Token: 0x060000E2 RID: 226 RVA: 0x00005F84 File Offset: 0x00004184
	public static List<string> CachedDoubleSemicolonExpansion(this string Text)
	{
		if (Text == null)
		{
			return null;
		}
		if (Text == global::Extensions.LastDoubleSemicolonExpansionRequest)
		{
			return global::Extensions.LastDoubleSemicolonExpansionResult;
		}
		List<string> list;
		if (!global::Extensions.CachedDoubleSemicolonExpansions.TryGetValue(Text, out list))
		{
			list = new List<string>(Text.Split(";;", StringSplitOptions.None));
			global::Extensions.CachedDoubleSemicolonExpansions.Add(Text, list);
		}
		global::Extensions.LastDoubleSemicolonExpansionRequest = Text;
		global::Extensions.LastDoubleSemicolonExpansionResult = list;
		return list;
	}

	// Token: 0x060000E3 RID: 227 RVA: 0x00005FE4 File Offset: 0x000041E4
	public static Dictionary<string, string> CachedDictionaryExpansion(this string text)
	{
		if (text == global::Extensions.LastDictionaryExpansionRequest && text != null)
		{
			return global::Extensions.LastDictionaryExpansionResult;
		}
		Dictionary<string, string> dictionary;
		if (!global::Extensions.CachedDictionaryExpansions.TryGetValue(text, out dictionary))
		{
			string[] array = text.Split(";;", StringSplitOptions.RemoveEmptyEntries);
			dictionary = new Dictionary<string, string>(array.Length);
			foreach (string text2 in array)
			{
				string[] array3 = text2.Split("::", 2, StringSplitOptions.None);
				if (array3.Length < 2)
				{
					Debug.LogError("Bad dictionary expansion part '" + text2 + "'");
				}
				else if (dictionary.ContainsKey(array3[0]))
				{
					Debug.LogError("Duplicate dictionary expansion entry '" + array3[0] + "'");
				}
				else
				{
					dictionary.Add(array3[0], array3[1]);
				}
			}
			global::Extensions.CachedDictionaryExpansions.Add(text, dictionary);
		}
		global::Extensions.LastDictionaryExpansionRequest = text;
		global::Extensions.LastDictionaryExpansionResult = dictionary;
		return dictionary;
	}

	// Token: 0x060000E4 RID: 228 RVA: 0x000060BC File Offset: 0x000042BC
	public static string ToStringForCachedDictionaryExpansion(this Dictionary<string, string> Dict)
	{
		StringBuilder stringBuilder = The.StringBuilder;
		foreach (KeyValuePair<string, string> keyValuePair in Dict)
		{
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Append(";;");
			}
			stringBuilder.Append(keyValuePair.Key).Append("::").Append(keyValuePair.Value);
		}
		return stringBuilder.ToString();
	}

	// Token: 0x060000E5 RID: 229 RVA: 0x00006148 File Offset: 0x00004348
	public static string ToStringForCachedNumericDictionaryExpansion(this Dictionary<string, int> Dict)
	{
		StringBuilder stringBuilder = The.StringBuilder;
		foreach (KeyValuePair<string, int> keyValuePair in Dict)
		{
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Append(";;");
			}
			stringBuilder.Append(keyValuePair.Key).Append("::").Append(keyValuePair.Value);
		}
		return stringBuilder.ToString();
	}

	// Token: 0x060000E6 RID: 230 RVA: 0x000061D4 File Offset: 0x000043D4
	public static Dictionary<string, int> CachedNumericDictionaryExpansion(this string text)
	{
		if (text == global::Extensions.LastNumericDictionaryExpansionRequest && text != null)
		{
			return global::Extensions.LastNumericDictionaryExpansionResult;
		}
		Dictionary<string, int> dictionary;
		if (!global::Extensions.CachedNumericDictionaryExpansions.TryGetValue(text, out dictionary))
		{
			string[] array = text.Split(";;", StringSplitOptions.RemoveEmptyEntries);
			dictionary = new Dictionary<string, int>(array.Length);
			foreach (string text2 in array)
			{
				string[] array3 = text2.Split("::", 2, StringSplitOptions.None);
				if (array3.Length < 2)
				{
					Debug.LogError("Bad dictionary expansion part '" + text2 + "'");
				}
				else if (dictionary.ContainsKey(array3[0]))
				{
					Debug.LogError("Duplicate dictionary expansion entry '" + array3[0] + "'");
				}
				else
				{
					dictionary.Add(array3[0], Convert.ToInt32(array3[1]));
				}
			}
			global::Extensions.CachedNumericDictionaryExpansions.Add(text, dictionary);
		}
		global::Extensions.LastNumericDictionaryExpansionRequest = text;
		global::Extensions.LastNumericDictionaryExpansionResult = dictionary;
		return dictionary;
	}

	// Token: 0x060000E7 RID: 231 RVA: 0x000062B0 File Offset: 0x000044B0
	public static int ReverseIndexOf<T>(this List<T> list, T item)
	{
		for (int i = list.Count - 1; i >= 0; i--)
		{
			T t = list[i];
			if (t.Equals(item))
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x060000E8 RID: 232 RVA: 0x000062F0 File Offset: 0x000044F0
	public static string Snippet(this string text, int howManySpaces = 4, int ifNoSpaces = 10)
	{
		int num = text.UpToNthIndex(' ', howManySpaces, -1);
		if (num <= 0)
		{
			return text.Substring(0, ifNoSpaces);
		}
		return text.Substring(0, num);
	}

	// Token: 0x060000E9 RID: 233 RVA: 0x00006320 File Offset: 0x00004520
	public static int UpToNthIndex(this string Text, char Char, int Number, int Default = -1)
	{
		int i = 0;
		int num = 0;
		int length = Text.Length;
		while (i < length)
		{
			if (Text[i] == Char)
			{
				Default = i;
				if (++num == Number)
				{
					break;
				}
			}
			i++;
		}
		return Default;
	}

	// Token: 0x060000EA RID: 234 RVA: 0x00006358 File Offset: 0x00004558
	public static int UpToNthIndex(this string Text, Func<char, bool> Predicate, int Number, int Default = -1)
	{
		int i = 0;
		int num = 0;
		int length = Text.Length;
		while (i < length)
		{
			if (Predicate(Text[i]))
			{
				Default = i;
				if (++num == Number)
				{
					break;
				}
			}
			i++;
		}
		return Default;
	}

	// Token: 0x060000EB RID: 235 RVA: 0x00006398 File Offset: 0x00004598
	public static int UpToNthIndex<T>(this List<T> List, T Value, int Number, int Default = -1)
	{
		int i = 0;
		int num = 0;
		int count = List.Count;
		while (i < count)
		{
			T t = List[i];
			if (t.Equals(Value))
			{
				Default = i;
				if (++num == Number)
				{
					break;
				}
			}
			i++;
		}
		return Default;
	}

	// Token: 0x060000EC RID: 236 RVA: 0x000063E4 File Offset: 0x000045E4
	public static int FindCount<T>(this List<T> list, Predicate<T> filter)
	{
		int num = 0;
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i] != null && filter(list[i]))
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x060000ED RID: 237 RVA: 0x00006426 File Offset: 0x00004626
	public static void EnsureCapacity<T>(this List<T> List, int Capacity)
	{
		if (List.Capacity >= Capacity)
		{
			return;
		}
		List.Capacity = Capacity;
	}

	// Token: 0x060000EE RID: 238 RVA: 0x0000643C File Offset: 0x0000463C
	public static void DisposeElements<T>(this IList<T> List, int Capacity) where T : IDisposable
	{
		for (int i = List.Count - 1; i >= 0; i--)
		{
			T t = List[i];
			t.Dispose();
		}
		List.Clear();
	}

	// Token: 0x060000EF RID: 239 RVA: 0x00006478 File Offset: 0x00004678
	public static T RemoveRandomElement<T>(this List<T> list, global::System.Random R = null)
	{
		int count = list.Count;
		if (count == 0)
		{
			return default(T);
		}
		if (count != 1)
		{
			if (R == null)
			{
				R = Stat.Rand;
			}
			int num = R.Next(0, list.Count);
			T t = list[num];
			list.RemoveAt(num);
			return t;
		}
		return list[0];
	}

	// Token: 0x060000F0 RID: 240 RVA: 0x000064D0 File Offset: 0x000046D0
	public static T GetRandomElement<T>(this List<T> list, global::System.Random R = null)
	{
		int count = list.Count;
		if (count == 0)
		{
			return default(T);
		}
		if (count != 1)
		{
			if (R == null)
			{
				R = Stat.Rand;
			}
			return list[R.Next(0, list.Count)];
		}
		return list[0];
	}

	// Token: 0x060000F1 RID: 241 RVA: 0x0000651C File Offset: 0x0000471C
	public static T GetRandomElement<T>(this Dictionary<T, int> list, global::System.Random R = null)
	{
		int count = list.Count;
		if (count != 0)
		{
			if (count != 1)
			{
				if (R == null)
				{
					R = Stat.Rand;
				}
				int num = 0;
				foreach (int num2 in list.Values)
				{
					num += num2;
				}
				int num3 = R.Next(0, num);
				int num4 = 0;
				foreach (KeyValuePair<T, int> keyValuePair in list)
				{
					num4 += keyValuePair.Value;
					if (num4 >= num3)
					{
						return keyValuePair.Key;
					}
				}
				throw new Exception("should be unreachable");
			}
			foreach (T t in list.Keys)
			{
				if (list[t] > 0)
				{
					return t;
				}
			}
			return default(T);
		}
		T t2 = default(T);
		return t2;
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x0000665C File Offset: 0x0000485C
	public static T GetRandomElement<T>(this Dictionary<T, int> list, ref int total, global::System.Random R = null)
	{
		int count = list.Count;
		if (count != 0)
		{
			if (count != 1)
			{
				if (R == null)
				{
					R = Stat.Rand;
				}
				if (total == 0)
				{
					foreach (int num in list.Values)
					{
						total += num;
					}
				}
				int num2 = R.Next(0, total);
				int num3 = 0;
				foreach (KeyValuePair<T, int> keyValuePair in list)
				{
					num3 += keyValuePair.Value;
					if (num3 >= num2)
					{
						return keyValuePair.Key;
					}
				}
				throw new Exception("should be unreachable");
			}
			foreach (T t in list.Keys)
			{
				if (list[t] > 0)
				{
					return t;
				}
			}
			return default(T);
		}
		T t2 = default(T);
		return t2;
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x0000679C File Offset: 0x0000499C
	public static T GetRandomElement<T>(this T[] list, global::System.Random R = null)
	{
		int num = list.Length;
		if (num == 0)
		{
			return default(T);
		}
		if (num != 1)
		{
			if (R == null)
			{
				R = Stat.Rand;
			}
			return list[R.Next(0, list.Length)];
		}
		return list[0];
	}

	// Token: 0x060000F4 RID: 244 RVA: 0x000067E2 File Offset: 0x000049E2
	public static T GetRandomElementCosmetic<T>(this T[] list)
	{
		return list.GetRandomElement<T>(Stat.Rnd2);
	}

	// Token: 0x060000F5 RID: 245 RVA: 0x000067F0 File Offset: 0x000049F0
	public static T GetRandomElement<T>(this IEnumerable<T> enumerable, global::System.Random R = null)
	{
		int num = enumerable.Count<T>();
		if (num == 0)
		{
			return default(T);
		}
		if (num != 1)
		{
			if (R == null)
			{
				R = Stat.Rand;
			}
			int num2 = R.Next(0, num);
			return enumerable.ElementAt<T>(num2);
		}
		return enumerable.ElementAt<T>(0);
	}

	// Token: 0x060000F6 RID: 246 RVA: 0x0000683C File Offset: 0x00004A3C
	public static T GetRandomElement<T>(this ICollection<T> collection, global::System.Random R = null)
	{
		int count = collection.Count;
		if (count == 0)
		{
			return default(T);
		}
		if (count != 1)
		{
			if (R == null)
			{
				R = Stat.Rand;
			}
			int num = R.Next(0, count);
			return collection.ElementAt<T>(num);
		}
		return collection.ElementAt<T>(0);
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x00006885 File Offset: 0x00004A85
	public static T GetRandomElementCosmetic<T>(this IEnumerable<T> enumerable)
	{
		return enumerable.GetRandomElement<T>(Stat.Rnd2);
	}

	// Token: 0x060000F8 RID: 248 RVA: 0x00006894 File Offset: 0x00004A94
	public static char GetRandomElement(this string list, global::System.Random R = null)
	{
		int length = list.Length;
		if (length == 0)
		{
			return '\0';
		}
		if (length != 1)
		{
			if (R == null)
			{
				R = Stat.Rand;
			}
			return list[R.Next(0, list.Length)];
		}
		return list[0];
	}

	// Token: 0x060000F9 RID: 249 RVA: 0x000068D8 File Offset: 0x00004AD8
	public static char GetRandomElementCosmetic(this string list)
	{
		return list.GetRandomElement(Stat.Rnd2);
	}

	// Token: 0x060000FA RID: 250 RVA: 0x000068E8 File Offset: 0x00004AE8
	public static T GetRandomElement<T>(this List<T> list, Predicate<T> filter, global::System.Random R = null)
	{
		if (list.Count == 0)
		{
			return default(T);
		}
		int num = 0;
		T t = default(T);
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i] != null && filter(list[i]))
			{
				num++;
				t = list[i];
			}
		}
		if (num == 0)
		{
			return default(T);
		}
		if (num == 1)
		{
			return t;
		}
		if (R == null)
		{
			R = Stat.Rand;
		}
		if (num == list.Count)
		{
			return list[R.Next(0, list.Count)];
		}
		int num2 = R.Next(0, num);
		if (num2 == num - 1)
		{
			return t;
		}
		int num3 = 0;
		for (int j = 0; j < list.Count; j++)
		{
			if (list[j] != null && filter(list[j]) && num3++ == num2)
			{
				return list[j];
			}
		}
		return default(T);
	}

	// Token: 0x060000FB RID: 251 RVA: 0x000069F0 File Offset: 0x00004BF0
	public static T GetRandomElementCosmetic<T>(this List<T> list, Predicate<T> filter)
	{
		return list.GetRandomElement<T>(filter, Stat.Rnd2);
	}

	// Token: 0x060000FC RID: 252 RVA: 0x00006A00 File Offset: 0x00004C00
	public static T GetRandomElement<T>(this T[] list, Predicate<T> filter, global::System.Random R = null)
	{
		if (list.Length == 0)
		{
			return default(T);
		}
		int num = 0;
		T t = default(T);
		for (int i = 0; i < list.Length; i++)
		{
			if (list[i] != null && filter(list[i]))
			{
				num++;
				t = list[i];
			}
		}
		if (num == 0)
		{
			return default(T);
		}
		if (num == 1)
		{
			return t;
		}
		if (R == null)
		{
			R = Stat.Rand;
		}
		if (num == list.Length)
		{
			return list[R.Next(0, list.Length)];
		}
		int num2 = R.Next(0, num);
		if (num2 == num - 1)
		{
			return t;
		}
		int num3 = 0;
		for (int j = 0; j < list.Length; j++)
		{
			if (list[j] != null && filter(list[j]) && num3++ == num2)
			{
				return list[j];
			}
		}
		return default(T);
	}

	// Token: 0x060000FD RID: 253 RVA: 0x00006AF8 File Offset: 0x00004CF8
	public static T GetRandomElementCosmetic<T>(this T[] list, Predicate<T> filter)
	{
		return list.GetRandomElement<T>(filter, Stat.Rnd2);
	}

	// Token: 0x060000FE RID: 254 RVA: 0x00006B08 File Offset: 0x00004D08
	public static string GetRandomSubstring(this string Text, char Separator, bool Trim = false, global::System.Random R = null)
	{
		if (Text.IsNullOrEmpty())
		{
			return "";
		}
		int i = 0;
		int num = 1;
		int num2 = Text.Length - 1;
		while (i < num2)
		{
			if (Text[i] == Separator)
			{
				num++;
			}
			i++;
		}
		if (num == 1)
		{
			return Text;
		}
		int num3 = 0;
		int num4 = (R ?? Stat.Rand).Next(0, num);
		i = 0;
		num = 0;
		while (i < num2)
		{
			if (Text[i] == Separator)
			{
				if (num++ == num4)
				{
					i--;
					break;
				}
				num3 = i + 1;
			}
			i++;
		}
		if (Trim)
		{
			while (char.IsWhiteSpace(Text[num3]))
			{
				num3++;
			}
			while (char.IsWhiteSpace(Text[i]))
			{
				i--;
			}
		}
		return Text.Substring(num3, i - num3 + 1);
	}

	// Token: 0x060000FF RID: 255 RVA: 0x00006BC8 File Offset: 0x00004DC8
	public static string GetDelimitedSubstring(this string Text, char Separator, int Index, bool Trim = false)
	{
		if (Text.IsNullOrEmpty())
		{
			if (Index <= 0)
			{
				return Text;
			}
			return null;
		}
		else
		{
			int num = 0;
			int i = 0;
			int num2 = 0;
			int num3 = Text.Length - 1;
			while (i < num3)
			{
				if (Text[i] == Separator)
				{
					if (num2++ == Index)
					{
						i--;
						break;
					}
					num = i + 1;
				}
				i++;
			}
			if (num2 == 0 && Index > 0)
			{
				return null;
			}
			if (Trim)
			{
				while (char.IsWhiteSpace(Text[num]))
				{
					num++;
				}
				while (char.IsWhiteSpace(Text[i]))
				{
					i--;
				}
			}
			return Text.Substring(num, i - num + 1);
		}
	}

	// Token: 0x06000100 RID: 256 RVA: 0x00006C5C File Offset: 0x00004E5C
	public static int FindDelimitedSubstring(this string Text, char Separator, string Substring, StringComparison Comparison = StringComparison.Ordinal)
	{
		if (Text == null || Substring == null)
		{
			return -1;
		}
		int num = Text.Length - 1;
		int i = -1;
		int num2 = -1;
		while (i < num)
		{
			i = Text.IndexOf(Substring, i + 1, Comparison);
			if (i == -1)
			{
				break;
			}
			num2++;
			if (i <= 0 || Text[i - 1] == Separator)
			{
				int num3 = i + Substring.Length;
				if (num3 - 1 >= num || Text[num3] == Separator)
				{
					return num2;
				}
			}
		}
		return -1;
	}

	// Token: 0x06000101 RID: 257 RVA: 0x00006CC4 File Offset: 0x00004EC4
	public static bool HasDelimitedSubstring([NotNullWhen(true)] this string Text, char Separator, string Substring, StringComparison Comparison = StringComparison.Ordinal)
	{
		if (Text == null || Substring == null)
		{
			return false;
		}
		int num = Text.Length - 1;
		int i = -1;
		while (i < num)
		{
			i = Text.IndexOf(Substring, i + 1, Comparison);
			if (i == -1)
			{
				break;
			}
			if (i <= 0 || Text[i - 1] == Separator)
			{
				int num2 = i + Substring.Length;
				if (num2 - 1 >= num || Text[num2] == Separator)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000102 RID: 258 RVA: 0x00006D28 File Offset: 0x00004F28
	public static bool HasDelimitedSubstring([NotNullWhen(true)] this string Text, string Separator, string Substring, StringComparison Comparison = StringComparison.Ordinal)
	{
		if (Text == null || Substring == null || Separator.IsNullOrEmpty())
		{
			return false;
		}
		int num = Text.Length - 1;
		int i = -1;
		while (i < num)
		{
			i = Text.IndexOf(Substring, i + 1, Comparison);
			if (i != -1)
			{
				if (i > 0)
				{
					if (i < Separator.Length)
					{
						continue;
					}
					bool flag = true;
					int j = 0;
					int length = Separator.Length;
					while (j < length)
					{
						if (Text[i - Separator.Length + j] != Separator[j])
						{
							flag = false;
							break;
						}
						j++;
					}
					if (!flag)
					{
						continue;
					}
				}
				int num2 = i + Substring.Length;
				if (num2 - 1 < num)
				{
					if (num2 + Separator.Length >= num)
					{
						continue;
					}
					bool flag2 = true;
					int k = 0;
					int length2 = Separator.Length;
					while (k < length2)
					{
						if (Text[num2 + k] != Separator[k])
						{
							flag2 = false;
							break;
						}
						k++;
					}
					if (!flag2)
					{
						continue;
					}
				}
				return true;
			}
			break;
		}
		return false;
	}

	// Token: 0x06000103 RID: 259 RVA: 0x00006E12 File Offset: 0x00005012
	public static string AddDelimitedSubstring(this string Text, char Separator, string Substring, StringComparison Comparison = StringComparison.Ordinal)
	{
		if (Text == null)
		{
			return Substring;
		}
		if (Text.HasDelimitedSubstring(Separator, Substring, Comparison))
		{
			return Text;
		}
		return Text + Separator + Substring;
	}

	// Token: 0x06000104 RID: 260 RVA: 0x00006E33 File Offset: 0x00005033
	public static string AddDelimitedSubstring(this string Text, string Separator, string Substring, StringComparison Comparison = StringComparison.Ordinal)
	{
		if (Text == null)
		{
			return Substring;
		}
		if (Text.HasDelimitedSubstring(Separator, Substring, Comparison))
		{
			return Text;
		}
		return Text + Separator + Substring;
	}

	// Token: 0x06000105 RID: 261 RVA: 0x00006E50 File Offset: 0x00005050
	public static IReadOnlyListEnumerator<T> Iterate<T>(this IReadOnlyList<T> List)
	{
		if (List == null)
		{
			return default(IReadOnlyListEnumerator<T>);
		}
		return new IReadOnlyListEnumerator<T>(List, 0, List.Count);
	}

	// Token: 0x06000106 RID: 262 RVA: 0x00006E78 File Offset: 0x00005078
	public static IReadOnlyListEnumerator<T> Iterate<T>(this IReadOnlyList<T> List, int Start)
	{
		if (List == null)
		{
			return default(IReadOnlyListEnumerator<T>);
		}
		return new IReadOnlyListEnumerator<T>(List, Start, List.Count - Start);
	}

	// Token: 0x06000107 RID: 263 RVA: 0x00006EA4 File Offset: 0x000050A4
	public static IReadOnlyListEnumerator<T> Iterate<T>(this IReadOnlyList<T> List, int Start, int Length)
	{
		if (List == null)
		{
			return default(IReadOnlyListEnumerator<T>);
		}
		int count = List.Count;
		if (Start + Length > count)
		{
			Length = count - Start;
		}
		return new IReadOnlyListEnumerator<T>(List, Start, Length);
	}

	// Token: 0x06000108 RID: 264 RVA: 0x00006ED8 File Offset: 0x000050D8
	public static IReadOnlyListEnumerator<T> IterateFirst<T>(this IReadOnlyList<T> List, int Amount)
	{
		if (List == null)
		{
			return default(IReadOnlyListEnumerator<T>);
		}
		int count = List.Count;
		if (Amount > count)
		{
			Amount = count;
		}
		return new IReadOnlyListEnumerator<T>(List, 0, Amount);
	}

	// Token: 0x06000109 RID: 265 RVA: 0x00006F08 File Offset: 0x00005108
	public static IReadOnlyListEnumerator<T> IterateLast<T>(this IReadOnlyList<T> List, int Amount)
	{
		if (List == null)
		{
			return default(IReadOnlyListEnumerator<T>);
		}
		int count = List.Count;
		if (Amount > count)
		{
			Amount = count;
		}
		return new IReadOnlyListEnumerator<T>(List, count - Amount, Amount);
	}

	// Token: 0x0600010A RID: 266 RVA: 0x00006F3C File Offset: 0x0000513C
	public static RingEnumerator<T> IterateRingAt<T>(this IReadOnlyList<T> List, int Start, bool IncludeSelf = false, bool Invert = false)
	{
		if (List == null)
		{
			return default(RingEnumerator<T>);
		}
		return new RingEnumerator<T>(List, Start, IncludeSelf, Invert);
	}

	// Token: 0x0600010B RID: 267 RVA: 0x00006F60 File Offset: 0x00005160
	public static RingEnumerator<T> IterateRing<T>(this IReadOnlyList<T> List, T Start, bool IncludeSelf = false, bool Invert = false) where T : IEquatable<T>
	{
		if (List == null)
		{
			return default(RingEnumerator<T>);
		}
		int i = 0;
		int count = List.Count;
		while (i < count)
		{
			T t = List[i];
			if (t.Equals(Start))
			{
				return new RingEnumerator<T>(List, i, IncludeSelf, Invert);
			}
			i++;
		}
		return default(RingEnumerator<T>);
	}

	// Token: 0x0600010C RID: 268 RVA: 0x00006FB8 File Offset: 0x000051B8
	public static RingSpanEnumerator<T> IterateRingAt<T>(this ReadOnlySpan<T> Span, int Start, bool IncludeSelf = false, bool Invert = false)
	{
		return new RingSpanEnumerator<T>(Span, Start, IncludeSelf, Invert);
	}

	// Token: 0x0600010D RID: 269 RVA: 0x00006FC4 File Offset: 0x000051C4
	public unsafe static RingSpanEnumerator<T> IterateRing<T>(this ReadOnlySpan<T> Span, T Start, bool IncludeSelf = false, bool Invert = false) where T : IEquatable<T>
	{
		int i = 0;
		int length = Span.Length;
		while (i < length)
		{
			T t = *Span[i];
			if (t.Equals(Start))
			{
				return new RingSpanEnumerator<T>(Span, i, IncludeSelf, Invert);
			}
			i++;
		}
		return default(RingSpanEnumerator<T>);
	}

	// Token: 0x0600010E RID: 270 RVA: 0x00007016 File Offset: 0x00005216
	public static RingSpanEnumerator<T> IterateRingAt<T>(this Span<T> Span, int Start, bool IncludeSelf = false, bool Invert = false)
	{
		return Span.IterateRingAt<T>(Start, IncludeSelf, Invert);
	}

	// Token: 0x0600010F RID: 271 RVA: 0x00007026 File Offset: 0x00005226
	public static RingSpanEnumerator<T> IterateRing<T>(this Span<T> Span, T Start, bool IncludeSelf = false, bool Invert = false) where T : IEquatable<T>
	{
		return Span.IterateRing(Start, IncludeSelf, Invert);
	}

	// Token: 0x06000110 RID: 272 RVA: 0x00007036 File Offset: 0x00005236
	public static RingSpanEnumerator<T> IterateRingAt<T>(this T[] Array, int Start, bool IncludeSelf = false, bool Invert = false)
	{
		return Array.IterateRingAt<T>(Start, IncludeSelf, Invert);
	}

	// Token: 0x06000111 RID: 273 RVA: 0x00007046 File Offset: 0x00005246
	public static RingSpanEnumerator<T> IterateRing<T>(this T[] Array, T Start, bool IncludeSelf = false, bool Invert = false) where T : IEquatable<T>
	{
		return Array.IterateRing(Start, IncludeSelf, Invert);
	}

	// Token: 0x06000112 RID: 274 RVA: 0x00007056 File Offset: 0x00005256
	public static Int32Enumerator IterateInt32(this string Text)
	{
		return new Int32Enumerator(Text);
	}

	// Token: 0x06000113 RID: 275 RVA: 0x00007063 File Offset: 0x00005263
	public static Int32Enumerator IterateInt32(this ReadOnlySpan<char> Text)
	{
		return new Int32Enumerator(Text);
	}

	// Token: 0x06000114 RID: 276 RVA: 0x0000706C File Offset: 0x0000526C
	public static void ExtractInt32(this ReadOnlySpan<char> Text, out int? First)
	{
		Int32Enumerator int32Enumerator = Text.IterateInt32();
		First = (int32Enumerator.MoveNext() ? new int?(int32Enumerator.Current) : null);
	}

	// Token: 0x06000115 RID: 277 RVA: 0x000070A8 File Offset: 0x000052A8
	public static void ExtractInt32(this ReadOnlySpan<char> Text, out int? First, out int? Second)
	{
		First = null;
		Second = null;
		Int32Enumerator int32Enumerator = Text.IterateInt32();
		if (!int32Enumerator.MoveNext())
		{
			return;
		}
		First = new int?(int32Enumerator.Current);
		if (!int32Enumerator.MoveNext())
		{
			return;
		}
		Second = new int?(int32Enumerator.Current);
	}

	// Token: 0x06000116 RID: 278 RVA: 0x00007104 File Offset: 0x00005304
	public static void ExtractInt32(this ReadOnlySpan<char> Text, out int? First, out int? Second, out int? Third)
	{
		First = null;
		Second = null;
		Third = null;
		Int32Enumerator int32Enumerator = Text.IterateInt32();
		if (!int32Enumerator.MoveNext())
		{
			return;
		}
		First = new int?(int32Enumerator.Current);
		if (!int32Enumerator.MoveNext())
		{
			return;
		}
		Second = new int?(int32Enumerator.Current);
		if (!int32Enumerator.MoveNext())
		{
			return;
		}
		Third = new int?(int32Enumerator.Current);
	}

	// Token: 0x06000117 RID: 279 RVA: 0x00007184 File Offset: 0x00005384
	public static void ExtractInt32(this ReadOnlySpan<char> Text, out int? First, out int? Second, out int? Third, out int? Fourth)
	{
		First = null;
		Second = null;
		Third = null;
		Fourth = null;
		Int32Enumerator int32Enumerator = Text.IterateInt32();
		if (!int32Enumerator.MoveNext())
		{
			return;
		}
		First = new int?(int32Enumerator.Current);
		if (!int32Enumerator.MoveNext())
		{
			return;
		}
		Second = new int?(int32Enumerator.Current);
		if (!int32Enumerator.MoveNext())
		{
			return;
		}
		Third = new int?(int32Enumerator.Current);
		if (!int32Enumerator.MoveNext())
		{
			return;
		}
		Fourth = new int?(int32Enumerator.Current);
	}

	// Token: 0x06000118 RID: 280 RVA: 0x00007226 File Offset: 0x00005426
	public static void ExtractInt32(this string Text, out int? First)
	{
		Text.AsSpan().ExtractInt32(out First);
	}

	// Token: 0x06000119 RID: 281 RVA: 0x00007234 File Offset: 0x00005434
	public static void ExtractInt32(this string Text, out int? First, out int? Second)
	{
		Text.AsSpan().ExtractInt32(out First, out Second);
	}

	// Token: 0x0600011A RID: 282 RVA: 0x00007243 File Offset: 0x00005443
	public static void ExtractInt32(this string Text, out int? First, out int? Second, out int? Third)
	{
		Text.AsSpan().ExtractInt32(out First, out Second, out Third);
	}

	// Token: 0x0600011B RID: 283 RVA: 0x00007253 File Offset: 0x00005453
	public static void ExtractInt32(this string Text, out int? First, out int? Second, out int? Third, out int? Fourth)
	{
		Text.AsSpan().ExtractInt32(out First, out Second, out Third, out Fourth);
	}

	// Token: 0x0600011C RID: 284 RVA: 0x00007265 File Offset: 0x00005465
	public static DelimitedEnumeratorChar DelimitedBy(this string Value, char Separator)
	{
		return new DelimitedEnumeratorChar(Value, Separator);
	}

	// Token: 0x0600011D RID: 285 RVA: 0x00007273 File Offset: 0x00005473
	public static DelimitedEnumeratorChar DelimitedBy(this ReadOnlySpan<char> Value, char Separator)
	{
		return new DelimitedEnumeratorChar(Value, Separator);
	}

	// Token: 0x0600011E RID: 286 RVA: 0x0000727C File Offset: 0x0000547C
	public static DelimitedEnumeratorString DelimitedBy(this string Value, string Separator)
	{
		return new DelimitedEnumeratorString(Value, Separator);
	}

	// Token: 0x0600011F RID: 287 RVA: 0x0000728A File Offset: 0x0000548A
	public static DelimitedEnumeratorString DelimitedBy(this ReadOnlySpan<char> Value, string Separator)
	{
		return new DelimitedEnumeratorString(Value, Separator);
	}

	// Token: 0x06000120 RID: 288 RVA: 0x00007294 File Offset: 0x00005494
	public static void Split(this string Text, char Separator, out string First, out string Second)
	{
		First = Text;
		Second = null;
		if (Text != null)
		{
			int num = Text.IndexOf(Separator);
			if (num >= 0)
			{
				First = Text.Substring(0, num);
				Second = Text.Substring(num + 1);
			}
		}
	}

	// Token: 0x06000121 RID: 289 RVA: 0x000072CC File Offset: 0x000054CC
	public static void Split(this string Text, char Separator, out string First, out string Second, out string Third)
	{
		First = Text;
		Second = null;
		Third = null;
		if (Text != null)
		{
			int num = Text.IndexOf(Separator);
			if (num >= 0)
			{
				First = Text.Substring(0, num);
				int num2 = Text.IndexOf(Separator, num + 1);
				if (num2 >= 0)
				{
					Second = Text.Substring(num + 1, num2 - num - 1);
					Third = Text.Substring(num2 + 1);
					return;
				}
				Second = Text.Substring(num + 1);
				Third = null;
			}
		}
	}

	// Token: 0x06000122 RID: 290 RVA: 0x00007338 File Offset: 0x00005538
	public static void AsDelimitedSpans(this string Text, char Separator, out ReadOnlySpan<char> First, out ReadOnlySpan<char> Second)
	{
		First = null;
		Second = null;
		int num = 0;
		foreach (ReadOnlySpan<char> readOnlySpan in Text.DelimitedBy(Separator))
		{
			if (++num == 1)
			{
				First = readOnlySpan;
			}
			else
			{
				if (num != 2)
				{
					break;
				}
				Second = readOnlySpan;
			}
		}
	}

	// Token: 0x06000123 RID: 291 RVA: 0x000073A0 File Offset: 0x000055A0
	public static void AsDelimitedSpans(this string Text, char Separator, out ReadOnlySpan<char> First, out ReadOnlySpan<char> Second, out ReadOnlySpan<char> Third)
	{
		First = null;
		Second = null;
		Third = null;
		int num = 0;
		foreach (ReadOnlySpan<char> readOnlySpan in Text.DelimitedBy(Separator))
		{
			if (++num == 1)
			{
				First = readOnlySpan;
			}
			else if (num == 2)
			{
				Second = readOnlySpan;
			}
			else
			{
				if (num != 3)
				{
					break;
				}
				Third = readOnlySpan;
			}
		}
	}

	// Token: 0x06000124 RID: 292 RVA: 0x00007424 File Offset: 0x00005624
	public static void AsDelimitedSpans(this string Text, char Separator, out ReadOnlySpan<char> First, out ReadOnlySpan<char> Second, out ReadOnlySpan<char> Third, out ReadOnlySpan<char> Fourth)
	{
		First = null;
		Second = null;
		Third = null;
		Fourth = null;
		int num = 0;
		foreach (ReadOnlySpan<char> readOnlySpan in Text.DelimitedBy(Separator))
		{
			if (++num == 1)
			{
				First = readOnlySpan;
			}
			else if (num == 2)
			{
				Second = readOnlySpan;
			}
			else if (num == 3)
			{
				Third = readOnlySpan;
			}
			else
			{
				if (num != 4)
				{
					break;
				}
				Fourth = readOnlySpan;
			}
		}
	}

	// Token: 0x06000125 RID: 293 RVA: 0x000074C0 File Offset: 0x000056C0
	public static void AsDelimitedSpans(this string Text, char Separator, out ReadOnlySpan<char> First, out ReadOnlySpan<char> Second, out ReadOnlySpan<char> Third, out ReadOnlySpan<char> Fourth, out ReadOnlySpan<char> Fifth)
	{
		First = null;
		Second = null;
		Third = null;
		Fourth = null;
		Fifth = null;
		int num = 0;
		foreach (ReadOnlySpan<char> readOnlySpan in Text.DelimitedBy(Separator))
		{
			if (++num == 1)
			{
				First = readOnlySpan;
			}
			else if (num == 2)
			{
				Second = readOnlySpan;
			}
			else if (num == 3)
			{
				Third = readOnlySpan;
			}
			else if (num == 4)
			{
				Fourth = readOnlySpan;
			}
			else
			{
				if (num != 5)
				{
					break;
				}
				Fifth = readOnlySpan;
			}
		}
	}

	// Token: 0x06000126 RID: 294 RVA: 0x00007578 File Offset: 0x00005778
	public static void AsDelimitedSpans(this string Text, char Separator, out ReadOnlySpan<char> First, out ReadOnlySpan<char> Second, out ReadOnlySpan<char> Third, out ReadOnlySpan<char> Fourth, out ReadOnlySpan<char> Fifth, out ReadOnlySpan<char> Sixth)
	{
		First = null;
		Second = null;
		Third = null;
		Fourth = null;
		Fifth = null;
		Sixth = null;
		int num = 0;
		foreach (ReadOnlySpan<char> readOnlySpan in Text.DelimitedBy(Separator))
		{
			if (++num == 1)
			{
				First = readOnlySpan;
			}
			else if (num == 2)
			{
				Second = readOnlySpan;
			}
			else if (num == 3)
			{
				Third = readOnlySpan;
			}
			else if (num == 4)
			{
				Fourth = readOnlySpan;
			}
			else if (num == 5)
			{
				Fifth = readOnlySpan;
			}
			else
			{
				if (num != 6)
				{
					break;
				}
				Sixth = readOnlySpan;
			}
		}
	}

	// Token: 0x06000127 RID: 295 RVA: 0x0000764C File Offset: 0x0000584C
	public static void Split(this ReadOnlySpan<char> Text, char Separator, out ReadOnlySpan<char> First, out ReadOnlySpan<char> Second)
	{
		First = null;
		Second = null;
		int num = 0;
		foreach (ReadOnlySpan<char> readOnlySpan in Text.DelimitedBy(Separator))
		{
			if (++num == 1)
			{
				First = readOnlySpan;
			}
			else
			{
				if (num != 2)
				{
					break;
				}
				Second = readOnlySpan;
			}
		}
	}

	// Token: 0x06000128 RID: 296 RVA: 0x000076B4 File Offset: 0x000058B4
	public static void Split(this ReadOnlySpan<char> Text, char Separator, out ReadOnlySpan<char> First, out ReadOnlySpan<char> Second, out ReadOnlySpan<char> Third)
	{
		First = null;
		Second = null;
		Third = null;
		int num = 0;
		foreach (ReadOnlySpan<char> readOnlySpan in Text.DelimitedBy(Separator))
		{
			if (++num == 1)
			{
				First = readOnlySpan;
			}
			else if (num == 2)
			{
				Second = readOnlySpan;
			}
			else
			{
				if (num != 3)
				{
					break;
				}
				Third = readOnlySpan;
			}
		}
	}

	// Token: 0x06000129 RID: 297 RVA: 0x00007738 File Offset: 0x00005938
	public static int LastSubdirectoryIndex(this string Path, string Directory, StringComparison Comparison = StringComparison.Ordinal)
	{
		int length = Path.Length;
		int length2 = Directory.Length;
		int num = length - 1;
		while (num != -1)
		{
			num = Path.LastIndexOf(Directory, num, Comparison);
			if (num == -1)
			{
				break;
			}
			if (num <= 0 || Path[num - 1].IsDirectorySeparatorInvariant())
			{
				int num2 = num + length2;
				if (num2 >= length || Path[num2].IsDirectorySeparatorInvariant())
				{
					return num;
				}
			}
		}
		return -1;
	}

	// Token: 0x0600012A RID: 298 RVA: 0x00007798 File Offset: 0x00005998
	public unsafe static void CopyTo(this string Source, int SourceIndex, Span<char> Destination, int DestinationIndex, int Length)
	{
		for (int i = 0; i < Length; i++)
		{
			*Destination[DestinationIndex + i] = Source[SourceIndex + i];
		}
	}

	// Token: 0x0600012B RID: 299 RVA: 0x000077C6 File Offset: 0x000059C6
	public unsafe static string WithColor(this string Text, string Color)
	{
		return string.Create<ValueTuple<string, string>>(Text.Length + Color.Length + 5, new ValueTuple<string, string>(Text, Color), delegate(Span<char> Span, ValueTuple<string, string> Tuple)
		{
			int num = 0;
			int length = Tuple.Item1.Length;
			int length2 = Tuple.Item2.Length;
			*Span[num++] = '{';
			*Span[num++] = '{';
			Tuple.Item2.CopyTo(0, Span, num, length2);
			num += length2;
			*Span[num++] = '|';
			Tuple.Item1.CopyTo(0, Span, num, length);
			num += length;
			*Span[num++] = '}';
			*Span[num] = '}';
		});
	}

	// Token: 0x0600012C RID: 300 RVA: 0x00007802 File Offset: 0x00005A02
	public static Color WithAlpha(this Color Color, float Alpha)
	{
		Color.a = Alpha;
		return Color;
	}

	// Token: 0x0600012D RID: 301 RVA: 0x00007810 File Offset: 0x00005A10
	public static T GetCyclicElement<T>(this IList<T> list, int Index)
	{
		int count = list.Count;
		if (count == 0)
		{
			return default(T);
		}
		if (count != 1)
		{
			return list[Index % list.Count];
		}
		return list[0];
	}

	// Token: 0x0600012E RID: 302 RVA: 0x00007850 File Offset: 0x00005A50
	public static T GetCyclicElement<T>(this T[] list, int Index)
	{
		int num = list.Length;
		if (num == 0)
		{
			return default(T);
		}
		if (num != 1)
		{
			return list[Index % list.Length];
		}
		return list[0];
	}

	// Token: 0x0600012F RID: 303 RVA: 0x00007888 File Offset: 0x00005A88
	public static bool Exists<T>(this IList<T> List, Func<int, T, bool> Predicate)
	{
		int i = 0;
		int count = List.Count;
		while (i < count)
		{
			if (Predicate(i, List[i]))
			{
				return true;
			}
			i++;
		}
		return false;
	}

	// Token: 0x06000130 RID: 304 RVA: 0x000078BC File Offset: 0x00005ABC
	public static void Fill<T>(this IList<T> List, T Value)
	{
		int i = 0;
		int count = List.Count;
		while (i < count)
		{
			List[i] = Value;
			i++;
		}
	}

	// Token: 0x06000131 RID: 305 RVA: 0x000078E4 File Offset: 0x00005AE4
	public static void Fill<T>(this IList<T> List, IEnumerable<T> Values)
	{
		using (IEnumerator<T> enumerator = Values.GetEnumerator())
		{
			int i = 0;
			int count = List.Count;
			while (i < count)
			{
				if (!enumerator.MoveNext())
				{
					break;
				}
				List[i] = enumerator.Current;
				i++;
			}
		}
	}

	// Token: 0x06000132 RID: 306 RVA: 0x00007940 File Offset: 0x00005B40
	public static void Rebase<T>(this IList<T> List, IReadOnlyList<T> Other)
	{
		List.Clear();
		if (!Other.IsNullOrEmpty<T>())
		{
			int i = 0;
			int count = Other.Count;
			while (i < count)
			{
				List.Add(Other[i]);
				i++;
			}
		}
	}

	// Token: 0x06000133 RID: 307 RVA: 0x0000797B File Offset: 0x00005B7B
	public static StringBuilder Clear(this StringBuilder SB)
	{
		SB.Length = 0;
		return SB;
	}

	// Token: 0x06000134 RID: 308 RVA: 0x00007985 File Offset: 0x00005B85
	public static string ClipExceptFormatting(this StringBuilder SB, int maxLength, string suffixWhenTruncated = "")
	{
		if (ConsoleLib.Console.ColorUtility.LengthExceptFormatting(SB) > maxLength)
		{
			return ConsoleLib.Console.ColorUtility.ClipExceptFormatting(SB, maxLength - suffixWhenTruncated.Length) + suffixWhenTruncated;
		}
		return SB.ToString();
	}

	// Token: 0x06000135 RID: 309 RVA: 0x000079AB File Offset: 0x00005BAB
	public static int LengthExceptFormatting(this string @string)
	{
		if (@string.IsNullOrEmpty())
		{
			return 0;
		}
		return ConsoleLib.Console.ColorUtility.LengthExceptFormatting(@string);
	}

	// Token: 0x06000136 RID: 310 RVA: 0x000079BD File Offset: 0x00005BBD
	public static string ClipExceptFormatting(this string @string, int maxLength, string suffixWhenTruncated = "")
	{
		if (ConsoleLib.Console.ColorUtility.LengthExceptFormatting(@string) > maxLength)
		{
			return ConsoleLib.Console.ColorUtility.ClipExceptFormatting(@string, maxLength - suffixWhenTruncated.Length) + suffixWhenTruncated;
		}
		return @string.ToString();
	}

	// Token: 0x06000137 RID: 311 RVA: 0x000079E4 File Offset: 0x00005BE4
	public static bool ValueEquals(this StringBuilder SB, string Value)
	{
		if (SB.Length != Value.Length)
		{
			return false;
		}
		int i = 0;
		int length = Value.Length;
		while (i < length)
		{
			if (SB[i] != Value[i])
			{
				return false;
			}
			i++;
		}
		return true;
	}

	// Token: 0x06000138 RID: 312 RVA: 0x00007A27 File Offset: 0x00005C27
	public static string Signed(this int Value, bool NotIfZero = false)
	{
		if (!(NotIfZero ? (Value > 0) : (Value >= 0)))
		{
			return Value.ToString();
		}
		return "+" + Value.ToString();
	}

	// Token: 0x06000139 RID: 313 RVA: 0x00007A54 File Offset: 0x00005C54
	public static StringBuilder CompoundSigned(this StringBuilder SB, int Value)
	{
		if (SB.Length > 0 && Value >= 0)
		{
			SB.Append('+');
		}
		SB.Append(Value);
		return SB;
	}

	// Token: 0x0600013A RID: 314 RVA: 0x00007A75 File Offset: 0x00005C75
	public static StringBuilder AppendSigned(this StringBuilder SB, int Value)
	{
		if (Value >= 0)
		{
			SB.Append('+');
		}
		SB.Append(Value);
		return SB;
	}

	// Token: 0x0600013B RID: 315 RVA: 0x00007A8D File Offset: 0x00005C8D
	public static StringBuilder AppendSigned(this StringBuilder SB, int Value, string Color)
	{
		return SB.Append((Value >= 0) ? '+' : '-').Append("{{").Append(Color)
			.Append('|')
			.Append(Math.Abs(Value))
			.Append("}}");
	}

	// Token: 0x0600013C RID: 316 RVA: 0x00007ACC File Offset: 0x00005CCC
	public static StringBuilder AppendRange(this StringBuilder SB, IEnumerable<string> Range, string Separator)
	{
		bool flag = true;
		foreach (string text in Range)
		{
			if (flag)
			{
				flag = false;
			}
			else
			{
				SB.Append(Separator);
			}
			SB.Append(text);
		}
		return SB;
	}

	// Token: 0x0600013D RID: 317 RVA: 0x00007B28 File Offset: 0x00005D28
	public static StringBuilder AppendThings(this StringBuilder SB, int Number, string Singular, string Plural)
	{
		return SB.Append(Number).Append(' ').Append((Number == 1) ? Singular : Plural);
	}

	// Token: 0x0600013E RID: 318 RVA: 0x00007B45 File Offset: 0x00005D45
	public static StringBuilder Compound(this StringBuilder SB, string Text)
	{
		if (!Text.IsNullOrEmpty())
		{
			if (SB.Length > 0)
			{
				SB.Append(' ');
			}
			SB.Append(Text);
		}
		return SB;
	}

	// Token: 0x0600013F RID: 319 RVA: 0x00007B6A File Offset: 0x00005D6A
	public static StringBuilder Compound(this StringBuilder SB, string Text, string With)
	{
		if (!Text.IsNullOrEmpty())
		{
			if (SB.Length > 0)
			{
				SB.Append(With);
			}
			SB.Append(Text);
		}
		return SB;
	}

	// Token: 0x06000140 RID: 320 RVA: 0x00007B8E File Offset: 0x00005D8E
	public static StringBuilder Compound(this StringBuilder SB, string Text, char With)
	{
		if (!Text.IsNullOrEmpty())
		{
			if (SB.Length > 0)
			{
				SB.Append(With);
			}
			SB.Append(Text);
		}
		return SB;
	}

	// Token: 0x06000141 RID: 321 RVA: 0x00007BB2 File Offset: 0x00005DB2
	public static StringBuilder Compound(this StringBuilder SB, char Ch)
	{
		if (SB.Length > 0)
		{
			SB.Append(' ');
		}
		SB.Append(Ch);
		return SB;
	}

	// Token: 0x06000142 RID: 322 RVA: 0x00007BCF File Offset: 0x00005DCF
	public static StringBuilder Compound(this StringBuilder SB, char Ch, string With)
	{
		if (SB.Length > 0)
		{
			SB.Append(With);
		}
		SB.Append(Ch);
		return SB;
	}

	// Token: 0x06000143 RID: 323 RVA: 0x00007BEB File Offset: 0x00005DEB
	public static StringBuilder Compound(this StringBuilder SB, char Ch, char With)
	{
		if (SB.Length > 0)
		{
			SB.Append(With);
		}
		SB.Append(Ch);
		return SB;
	}

	// Token: 0x06000144 RID: 324 RVA: 0x00007C07 File Offset: 0x00005E07
	public static StringBuilder Compound(this StringBuilder SB, int Value)
	{
		if (SB.Length > 0)
		{
			SB.Append(' ');
		}
		SB.Append(Value);
		return SB;
	}

	// Token: 0x06000145 RID: 325 RVA: 0x00007C24 File Offset: 0x00005E24
	public static StringBuilder Compound(this StringBuilder SB, int Value, string With)
	{
		if (SB.Length > 0)
		{
			SB.Append(With);
		}
		SB.Append(Value);
		return SB;
	}

	// Token: 0x06000146 RID: 326 RVA: 0x00007C40 File Offset: 0x00005E40
	public static StringBuilder Compound(this StringBuilder SB, int Value, char With)
	{
		if (SB.Length > 0)
		{
			SB.Append(With);
		}
		SB.Append(Value);
		return SB;
	}

	// Token: 0x06000147 RID: 327 RVA: 0x00007C5C File Offset: 0x00005E5C
	public static void Compound(this Utf16ValueStringBuilder SB, string Value, string With)
	{
		if (SB.Length > 0)
		{
			SB.Append(With);
		}
		SB.Append(Value);
	}

	// Token: 0x06000148 RID: 328 RVA: 0x00007C75 File Offset: 0x00005E75
	public static void Compound(this Utf16ValueStringBuilder SB, string Value, char With)
	{
		if (SB.Length > 0)
		{
			SB.Append(With);
		}
		SB.Append(Value);
	}

	// Token: 0x06000149 RID: 329 RVA: 0x00007C8E File Offset: 0x00005E8E
	public static void Compound(this Utf16ValueStringBuilder SB, string Value)
	{
		if (SB.Length > 0)
		{
			SB.Append(' ');
		}
		SB.Append(Value);
	}

	// Token: 0x0600014A RID: 330 RVA: 0x00007CA8 File Offset: 0x00005EA8
	public static void AppendMultiple(this Utf16ValueStringBuilder SB, string Value1, string Value2)
	{
		SB.Append(Value1);
		SB.Append(Value2);
	}

	// Token: 0x0600014B RID: 331 RVA: 0x00007CB8 File Offset: 0x00005EB8
	public static void AppendMultiple(this Utf16ValueStringBuilder SB, string Value1, string Value2, string Value3)
	{
		SB.Append(Value1);
		SB.Append(Value2);
		SB.Append(Value3);
	}

	// Token: 0x0600014C RID: 332 RVA: 0x00007CCF File Offset: 0x00005ECF
	public static void AppendMultiple(this Utf16ValueStringBuilder SB, string Value1, string Value2, string Value3, string Value4)
	{
		SB.Append(Value1);
		SB.Append(Value2);
		SB.Append(Value3);
		SB.Append(Value4);
	}

	// Token: 0x0600014D RID: 333 RVA: 0x00007CEE File Offset: 0x00005EEE
	public static void AppendMultiple(this Utf16ValueStringBuilder SB, string Value1, string Value2, string Value3, string Value4, string Value5)
	{
		SB.Append(Value1);
		SB.Append(Value2);
		SB.Append(Value3);
		SB.Append(Value4);
		SB.Append(Value5);
	}

	// Token: 0x0600014E RID: 334 RVA: 0x00007D18 File Offset: 0x00005F18
	public static StringBuilder Unindent(this StringBuilder SB)
	{
		int i = 0;
		int num = SB.Length;
		while (i < num)
		{
			if (!char.IsWhiteSpace(SB[i]))
			{
				SB.Remove(0, i);
				break;
			}
			i++;
		}
		for (i = SB.Length - 1; i >= 0; i--)
		{
			if (!char.IsWhiteSpace(SB[i]))
			{
				SB.Remove(i + 1, SB.Length - i - 1);
				break;
			}
		}
		i = 0;
		num = SB.Length;
		int num2 = num;
		while (i < num)
		{
			char c = SB[i];
			if (c == '\n')
			{
				if (i - num2 > 1)
				{
					goto IL_0093;
				}
				num2 = i;
			}
			else if (num2 < num && !char.IsWhiteSpace(c))
			{
				goto IL_0093;
			}
			IL_00AE:
			i++;
			continue;
			IL_0093:
			SB.Remove(num2 + 1, i - num2 - 1);
			num -= i - num2 - 1;
			i = num2;
			num2 = num;
			goto IL_00AE;
		}
		return SB;
	}

	// Token: 0x0600014F RID: 335 RVA: 0x00007DDC File Offset: 0x00005FDC
	public static StringBuilder Compound(this StringBuilder SB, IEnumerable<string> Values, string With = " ")
	{
		foreach (string text in Values)
		{
			if (SB.Length > 0)
			{
				SB.Append(With);
			}
			SB.Append(text);
		}
		return SB;
	}

	// Token: 0x06000150 RID: 336 RVA: 0x00007E38 File Offset: 0x00006038
	public unsafe static string ToMaskString(this int Mask)
	{
		Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)64], 32);
		for (int i = span.Length - 1; i >= 0; i--)
		{
			*span[i] = (((Mask & (1 << i)) != 0) ? '1' : '0');
		}
		return span.ToString();
	}

	// Token: 0x06000151 RID: 337 RVA: 0x00007E8C File Offset: 0x0000608C
	public unsafe static string ToMaskString(this short Mask)
	{
		Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)32], 16);
		for (int i = span.Length - 1; i >= 0; i--)
		{
			*span[i] = ((((int)Mask & (1 << i)) != 0) ? '1' : '0');
		}
		return span.ToString();
	}

	// Token: 0x06000152 RID: 338 RVA: 0x00007EE0 File Offset: 0x000060E0
	public static StringBuilder AppendMask(this StringBuilder Builder, int Mask, int Length = 32)
	{
		for (int i = Length - 1; i >= 0; i--)
		{
			Builder.Append(((Mask & (1 << i)) != 0) ? '1' : '0');
		}
		return Builder;
	}

	// Token: 0x06000153 RID: 339 RVA: 0x00007F14 File Offset: 0x00006114
	public static StringBuilder AppendMask(this StringBuilder Builder, int Mask, int Start, int Length)
	{
		for (int i = Start + Length - 1; i >= Start; i--)
		{
			Builder.Append(((Mask & (1 << i)) != 0) ? '1' : '0');
		}
		return Builder;
	}

	// Token: 0x06000154 RID: 340 RVA: 0x00007F4C File Offset: 0x0000614C
	public static StringBuilder AppendJoin(this StringBuilder SB, string Separator, IEnumerable<string> Values)
	{
		StringBuilder stringBuilder;
		using (IEnumerator<string> enumerator = Values.GetEnumerator())
		{
			if (!enumerator.MoveNext())
			{
				stringBuilder = SB;
			}
			else
			{
				string text = enumerator.Current;
				if (text != null)
				{
					SB.Append(text);
				}
				while (enumerator.MoveNext())
				{
					SB.Append(Separator);
					text = enumerator.Current;
					if (text != null)
					{
						SB.Append(text);
					}
				}
				stringBuilder = SB;
			}
		}
		return stringBuilder;
	}

	// Token: 0x06000155 RID: 341 RVA: 0x00007FC4 File Offset: 0x000061C4
	public static StringBuilder AppendItVerb(this StringBuilder SB, global::XRL.World.GameObject Object, string Verb, bool Capitalized = false)
	{
		return SB.Append(Capitalized ? Object.It : Object.it).Append(' ').Append(Object.GetVerb(Verb, false, true, false, true, false));
	}

	// Token: 0x06000156 RID: 342 RVA: 0x00007FF5 File Offset: 0x000061F5
	public static StringBuilder CompoundItVerb(this StringBuilder SB, global::XRL.World.GameObject Object, string Verb, char With = ' ', bool Capitalized = true)
	{
		return SB.Compound(Capitalized ? Object.It : Object.it, With).Append(' ').Append(Object.GetVerb(Verb, false, true, false, true, false));
	}

	// Token: 0x06000157 RID: 343 RVA: 0x00008028 File Offset: 0x00006228
	public static StringBuilder AppendCase(this StringBuilder SB, string Value)
	{
		if (Value.IsNullOrEmpty())
		{
			return SB;
		}
		SB.AppendCase(Value, SB.NextIsCapital());
		return SB;
	}

	// Token: 0x06000158 RID: 344 RVA: 0x00008044 File Offset: 0x00006244
	public static StringBuilder AppendCase(this StringBuilder SB, string Value, bool Capitalize)
	{
		if (Value.IsNullOrEmpty())
		{
			return SB;
		}
		SB.Append(Value);
		char c = Value[0];
		if (char.IsUpper(c) != Capitalize)
		{
			int length = Value.Length;
			SB[SB.Length - length] = (Capitalize ? char.ToUpper(c) : char.ToLower(c));
		}
		return SB;
	}

	// Token: 0x06000159 RID: 345 RVA: 0x0000809C File Offset: 0x0000629C
	public static StringBuilder AppendUpper(this StringBuilder SB, string Value)
	{
		if (Value.IsNullOrEmpty())
		{
			return SB;
		}
		int length = Value.Length;
		SB.EnsureCapacity(SB.Length + length);
		for (int i = 0; i < length; i++)
		{
			SB.Append(char.ToUpperInvariant(Value[i]));
		}
		return SB;
	}

	// Token: 0x0600015A RID: 346 RVA: 0x000080EC File Offset: 0x000062EC
	public static StringBuilder AppendLower(this StringBuilder SB, string Value)
	{
		if (Value.IsNullOrEmpty())
		{
			return SB;
		}
		int length = Value.Length;
		SB.EnsureCapacity(SB.Length + length);
		for (int i = 0; i < length; i++)
		{
			SB.Append(char.ToLowerInvariant(Value[i]));
		}
		return SB;
	}

	// Token: 0x0600015B RID: 347 RVA: 0x0000813C File Offset: 0x0000633C
	public static StringBuilder TrimEnd(this StringBuilder SB, char Character = ' ')
	{
		int num = SB.Length;
		while (SB[num - 1] == Character)
		{
			num = (SB.Length = num - 1);
		}
		return SB;
	}

	// Token: 0x0600015C RID: 348 RVA: 0x0000816A File Offset: 0x0000636A
	public static StringBuilder VariableReplace(this StringBuilder Text, StringMap<ReplacerEntry> Replacers = null, IList<TextArgument> Arguments = null, int DefaultArgument = -1)
	{
		GameText.Process(Text, Replacers, null, Arguments, DefaultArgument, false);
		return Text;
	}

	// Token: 0x0600015D RID: 349 RVA: 0x00008178 File Offset: 0x00006378
	public static string VariableReplace(this string Text, StringMap<ReplacerEntry> Replacers = null, IList<TextArgument> Arguments = null, int DefaultArgument = -1)
	{
		GameText.Process(ref Text, Replacers, null, Arguments, DefaultArgument, false);
		return Text;
	}

	// Token: 0x0600015E RID: 350 RVA: 0x00008187 File Offset: 0x00006387
	public static StringBuilder StripColorFormatting(this StringBuilder SB)
	{
		ConsoleLib.Console.ColorUtility.StripFormatting(SB);
		return SB;
	}

	// Token: 0x0600015F RID: 351 RVA: 0x00008191 File Offset: 0x00006391
	public static bool EndsWith(this StringBuilder SB, char Value)
	{
		return SB[SB.Length - 1] == Value;
	}

	// Token: 0x06000160 RID: 352 RVA: 0x000081A4 File Offset: 0x000063A4
	public static bool IsWordCharacter(this char ch)
	{
		return ch == '-' || (ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || (ch >= '0' && ch <= '9');
	}

	// Token: 0x06000161 RID: 353 RVA: 0x000081D0 File Offset: 0x000063D0
	public static char ToLowerASCII(this char Char)
	{
		if (Char >= 'A' && Char <= 'Z')
		{
			Char |= ' ';
		}
		return Char;
	}

	// Token: 0x06000162 RID: 354 RVA: 0x000081E4 File Offset: 0x000063E4
	public static char ToUpperASCII(this char Char)
	{
		if (Char >= 'a' && Char <= 'z')
		{
			Char &= '\uffdf';
		}
		return Char;
	}

	// Token: 0x06000163 RID: 355 RVA: 0x000081FC File Offset: 0x000063FC
	public static bool NextIsCapital(this StringBuilder SB)
	{
		if (SB.Length == 0)
		{
			return true;
		}
		char c = SB[SB.Length - 1];
		return c == '\n' || (c == ' ' && SB.Length >= 2 && SB[SB.Length - 2] == '.');
	}

	// Token: 0x06000164 RID: 356 RVA: 0x0000824C File Offset: 0x0000644C
	public static void Rebase<K, V>(this IDictionary<K, V> Dictionary, IReadOnlyDictionary<K, V> Other)
	{
		Dictionary.Clear();
		if (!Other.IsNullOrEmpty<KeyValuePair<K, V>>())
		{
			foreach (KeyValuePair<K, V> keyValuePair in Other)
			{
				Dictionary.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}
	}

	// Token: 0x06000165 RID: 357 RVA: 0x000082B0 File Offset: 0x000064B0
	public static void SetRange<K, V>(this IDictionary<K, V> Dictionary, IReadOnlyDictionary<K, V> Other)
	{
		if (!Other.IsNullOrEmpty<KeyValuePair<K, V>>())
		{
			foreach (KeyValuePair<K, V> keyValuePair in Other)
			{
				Dictionary[keyValuePair.Key] = keyValuePair.Value;
			}
		}
	}

	// Token: 0x06000166 RID: 358 RVA: 0x00008310 File Offset: 0x00006510
	public static void AddRange<K, V>(this IDictionary<K, V> Dictionary, IReadOnlyDictionary<K, V> Other)
	{
		if (!Other.IsNullOrEmpty<KeyValuePair<K, V>>())
		{
			foreach (KeyValuePair<K, V> keyValuePair in Other)
			{
				Dictionary.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}
	}

	// Token: 0x06000167 RID: 359 RVA: 0x00008370 File Offset: 0x00006570
	public static int TryAddRange<K, V>(this IDictionary<K, V> Dictionary, IReadOnlyDictionary<K, V> Other)
	{
		int num = 0;
		if (!Other.IsNullOrEmpty<KeyValuePair<K, V>>())
		{
			foreach (KeyValuePair<K, V> keyValuePair in Other)
			{
				if (Dictionary.TryAdd(keyValuePair.Key, keyValuePair.Value))
				{
					num++;
				}
			}
		}
		return num;
	}

	// Token: 0x06000168 RID: 360 RVA: 0x000083D8 File Offset: 0x000065D8
	public static void Set<K, V>(this Dictionary<K, V> dictionary, K key, V value)
	{
		dictionary[key] = value;
	}

	// Token: 0x06000169 RID: 361 RVA: 0x000083E4 File Offset: 0x000065E4
	public static bool PairEquals<K, V>(this Dictionary<K, V> Dictionary, Dictionary<K, V> Other, bool NullOrEmpty = true) where V : IComparable<V>
	{
		if (Dictionary == Other)
		{
			return true;
		}
		if (NullOrEmpty && Dictionary.IsNullOrEmpty<KeyValuePair<K, V>>() && Other.IsNullOrEmpty<KeyValuePair<K, V>>())
		{
			return true;
		}
		if (Dictionary == null || Other == null)
		{
			return false;
		}
		if (Dictionary.Count != Other.Count)
		{
			return false;
		}
		foreach (KeyValuePair<K, V> keyValuePair in Dictionary)
		{
			V v;
			if (!Other.TryGetValue(keyValuePair.Key, out v))
			{
				return false;
			}
			V value = keyValuePair.Value;
			if (!value.Equals(v))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x0600016A RID: 362 RVA: 0x00008498 File Offset: 0x00006698
	public static bool ChanceIn(this int Chance, int Odds)
	{
		return Chance > 0 && (Chance >= Odds || Stat.Random(1, Odds) <= Chance);
	}

	// Token: 0x0600016B RID: 363 RVA: 0x000084B3 File Offset: 0x000066B3
	public static bool in10(this int chance)
	{
		return chance > 0 && (chance >= 10 || Stat.Random(1, 10) <= chance);
	}

	// Token: 0x0600016C RID: 364 RVA: 0x000084D0 File Offset: 0x000066D0
	public static bool in10(this int? chance)
	{
		if (chance != null)
		{
			int? num = chance;
			int num2 = 0;
			if ((num.GetValueOrDefault() > num2) & (num != null))
			{
				num = chance;
				num2 = 10;
				if (!((num.GetValueOrDefault() >= num2) & (num != null)))
				{
					int num3 = Stat.Random(1, 10);
					num = chance;
					return (num3 <= num.GetValueOrDefault()) & (num != null);
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600016D RID: 365 RVA: 0x0000853C File Offset: 0x0000673C
	public static bool in100(this int chance)
	{
		return chance > 0 && (chance >= 100 || Stat.Random(1, 100) <= chance);
	}

	// Token: 0x0600016E RID: 366 RVA: 0x0000855C File Offset: 0x0000675C
	public static bool in100(this int? chance)
	{
		if (chance != null)
		{
			int? num = chance;
			int num2 = 0;
			if ((num.GetValueOrDefault() > num2) & (num != null))
			{
				num = chance;
				num2 = 100;
				if (!((num.GetValueOrDefault() >= num2) & (num != null)))
				{
					int num3 = Stat.Random(1, 100);
					num = chance;
					return (num3 <= num.GetValueOrDefault()) & (num != null);
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600016F RID: 367 RVA: 0x000085C8 File Offset: 0x000067C8
	public static bool in1000(this int chance)
	{
		return chance > 0 && (chance >= 1000 || Stat.Random(1, 1000) <= chance);
	}

	// Token: 0x06000170 RID: 368 RVA: 0x000085EC File Offset: 0x000067EC
	public static bool in1000(this int? chance)
	{
		if (chance != null)
		{
			int? num = chance;
			int num2 = 0;
			if ((num.GetValueOrDefault() > num2) & (num != null))
			{
				num = chance;
				num2 = 1000;
				if (!((num.GetValueOrDefault() >= num2) & (num != null)))
				{
					int num3 = Stat.Random(1, 1000);
					num = chance;
					return (num3 <= num.GetValueOrDefault()) & (num != null);
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000171 RID: 369 RVA: 0x0000865E File Offset: 0x0000685E
	public static bool in10000(this int chance)
	{
		return chance > 0 && (chance >= 10000 || Stat.Random(1, 10000) <= chance);
	}

	// Token: 0x06000172 RID: 370 RVA: 0x00008684 File Offset: 0x00006884
	public static bool in10000(this int? chance)
	{
		if (chance != null)
		{
			int? num = chance;
			int num2 = 0;
			if ((num.GetValueOrDefault() > num2) & (num != null))
			{
				num = chance;
				num2 = 10000;
				if (!((num.GetValueOrDefault() >= num2) & (num != null)))
				{
					int num3 = Stat.Random(1, 10000);
					num = chance;
					return (num3 <= num.GetValueOrDefault()) & (num != null);
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000173 RID: 371 RVA: 0x000086F6 File Offset: 0x000068F6
	public static bool in10(this int chance, global::System.Random rnd)
	{
		return chance > 0 && (chance >= 10 || rnd.Next(1, 11) <= chance);
	}

	// Token: 0x06000174 RID: 372 RVA: 0x00008714 File Offset: 0x00006914
	public static bool in10(this int? chance, global::System.Random rnd)
	{
		if (chance != null)
		{
			int? num = chance;
			int num2 = 0;
			if ((num.GetValueOrDefault() > num2) & (num != null))
			{
				num = chance;
				num2 = 10;
				if (!((num.GetValueOrDefault() >= num2) & (num != null)))
				{
					int num3 = rnd.Next(1, 11);
					num = chance;
					return (num3 <= num.GetValueOrDefault()) & (num != null);
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000175 RID: 373 RVA: 0x00008781 File Offset: 0x00006981
	public static bool in100(this int chance, global::System.Random rnd)
	{
		return chance > 0 && (chance >= 100 || rnd.Next(1, 101) <= chance);
	}

	// Token: 0x06000176 RID: 374 RVA: 0x000087A0 File Offset: 0x000069A0
	public static bool in100(this int? chance, global::System.Random rnd)
	{
		if (chance != null)
		{
			int? num = chance;
			int num2 = 0;
			if ((num.GetValueOrDefault() > num2) & (num != null))
			{
				num = chance;
				num2 = 100;
				if (!((num.GetValueOrDefault() >= num2) & (num != null)))
				{
					int num3 = rnd.Next(1, 101);
					num = chance;
					return (num3 <= num.GetValueOrDefault()) & (num != null);
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000177 RID: 375 RVA: 0x0000880D File Offset: 0x00006A0D
	public static bool in1000(this int chance, global::System.Random rnd)
	{
		return chance > 0 && (chance >= 1000 || rnd.Next(1, 1001) <= chance);
	}

	// Token: 0x06000178 RID: 376 RVA: 0x00008834 File Offset: 0x00006A34
	public static bool in1000(this int? chance, global::System.Random rnd)
	{
		if (chance != null)
		{
			int? num = chance;
			int num2 = 0;
			if ((num.GetValueOrDefault() > num2) & (num != null))
			{
				num = chance;
				num2 = 1000;
				if (!((num.GetValueOrDefault() >= num2) & (num != null)))
				{
					int num3 = rnd.Next(1, 1001);
					num = chance;
					return (num3 <= num.GetValueOrDefault()) & (num != null);
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000179 RID: 377 RVA: 0x000088A7 File Offset: 0x00006AA7
	public static bool in10000(this int chance, global::System.Random rnd)
	{
		return chance > 0 && (chance >= 10000 || rnd.Next(1, 10001) <= chance);
	}

	// Token: 0x0600017A RID: 378 RVA: 0x000088CC File Offset: 0x00006ACC
	public static bool in10000(this int? chance, global::System.Random rnd)
	{
		if (chance != null)
		{
			int? num = chance;
			int num2 = 0;
			if ((num.GetValueOrDefault() > num2) & (num != null))
			{
				num = chance;
				num2 = 10000;
				if (!((num.GetValueOrDefault() >= num2) & (num != null)))
				{
					int num3 = rnd.Next(1, 10001);
					num = chance;
					return (num3 <= num.GetValueOrDefault()) & (num != null);
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600017B RID: 379 RVA: 0x0000893F File Offset: 0x00006B3F
	public static bool in10(this long chance)
	{
		return chance > 0L && (chance >= 10L || (long)Stat.Random(1, 10) <= chance);
	}

	// Token: 0x0600017C RID: 380 RVA: 0x00008960 File Offset: 0x00006B60
	public static bool in10(this long? chance)
	{
		if (chance != null)
		{
			long? num = chance;
			long num2 = 0L;
			if ((num.GetValueOrDefault() > num2) & (num != null))
			{
				num = chance;
				num2 = 10L;
				if (!((num.GetValueOrDefault() >= num2) & (num != null)))
				{
					long num3 = (long)Stat.Random(1, 10);
					num = chance;
					return (num3 <= num.GetValueOrDefault()) & (num != null);
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600017D RID: 381 RVA: 0x000089CF File Offset: 0x00006BCF
	public static bool in100(this long chance)
	{
		return chance > 0L && (chance >= 100L || (long)Stat.Random(1, 100) <= chance);
	}

	// Token: 0x0600017E RID: 382 RVA: 0x000089F0 File Offset: 0x00006BF0
	public static bool in100(this long? chance)
	{
		if (chance != null)
		{
			long? num = chance;
			long num2 = 0L;
			if ((num.GetValueOrDefault() > num2) & (num != null))
			{
				num = chance;
				num2 = 100L;
				if (!((num.GetValueOrDefault() >= num2) & (num != null)))
				{
					long num3 = (long)Stat.Random(1, 100);
					num = chance;
					return (num3 <= num.GetValueOrDefault()) & (num != null);
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600017F RID: 383 RVA: 0x00008A5F File Offset: 0x00006C5F
	public static bool in1000(this long chance)
	{
		return chance > 0L && (chance >= 1000L || (long)Stat.Random(1, 1000) <= chance);
	}

	// Token: 0x06000180 RID: 384 RVA: 0x00008A88 File Offset: 0x00006C88
	public static bool in1000(this long? chance)
	{
		if (chance != null)
		{
			long? num = chance;
			long num2 = 0L;
			if ((num.GetValueOrDefault() > num2) & (num != null))
			{
				num = chance;
				num2 = 1000L;
				if (!((num.GetValueOrDefault() >= num2) & (num != null)))
				{
					long num3 = (long)Stat.Random(1, 1000);
					num = chance;
					return (num3 <= num.GetValueOrDefault()) & (num != null);
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000181 RID: 385 RVA: 0x00008AFD File Offset: 0x00006CFD
	public static bool in10000(this long chance)
	{
		return chance > 0L && (chance >= 10000L || (long)Stat.Random(1, 10000) <= chance);
	}

	// Token: 0x06000182 RID: 386 RVA: 0x00008B24 File Offset: 0x00006D24
	public static bool in10000(this long? chance)
	{
		if (chance != null)
		{
			long? num = chance;
			long num2 = 0L;
			if ((num.GetValueOrDefault() > num2) & (num != null))
			{
				num = chance;
				num2 = 10000L;
				if (!((num.GetValueOrDefault() >= num2) & (num != null)))
				{
					long num3 = (long)Stat.Random(1, 10000);
					num = chance;
					return (num3 <= num.GetValueOrDefault()) & (num != null);
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000183 RID: 387 RVA: 0x00008B99 File Offset: 0x00006D99
	public static bool in10(this long chance, global::System.Random rnd)
	{
		return chance > 0L && (chance >= 10L || (long)rnd.Next(1, 11) <= chance);
	}

	// Token: 0x06000184 RID: 388 RVA: 0x00008BBC File Offset: 0x00006DBC
	public static bool in10(this long? chance, global::System.Random rnd)
	{
		if (chance != null)
		{
			long? num = chance;
			long num2 = 0L;
			if ((num.GetValueOrDefault() > num2) & (num != null))
			{
				num = chance;
				num2 = 10L;
				if (!((num.GetValueOrDefault() >= num2) & (num != null)))
				{
					long num3 = (long)rnd.Next(1, 11);
					num = chance;
					return (num3 <= num.GetValueOrDefault()) & (num != null);
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000185 RID: 389 RVA: 0x00008C2C File Offset: 0x00006E2C
	public static bool in100(this long chance, global::System.Random rnd)
	{
		return chance > 0L && (chance >= 100L || (long)rnd.Next(1, 101) <= chance);
	}

	// Token: 0x06000186 RID: 390 RVA: 0x00008C50 File Offset: 0x00006E50
	public static bool in100(this long? chance, global::System.Random rnd)
	{
		if (chance != null)
		{
			long? num = chance;
			long num2 = 0L;
			if ((num.GetValueOrDefault() > num2) & (num != null))
			{
				num = chance;
				num2 = 100L;
				if (!((num.GetValueOrDefault() >= num2) & (num != null)))
				{
					long num3 = (long)rnd.Next(1, 101);
					num = chance;
					return (num3 <= num.GetValueOrDefault()) & (num != null);
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000187 RID: 391 RVA: 0x00008CC0 File Offset: 0x00006EC0
	public static bool in1000(this long chance, global::System.Random rnd)
	{
		return chance > 0L && (chance >= 1000L || (long)rnd.Next(1, 1001) <= chance);
	}

	// Token: 0x06000188 RID: 392 RVA: 0x00008CE8 File Offset: 0x00006EE8
	public static bool in1000(this long? chance, global::System.Random rnd)
	{
		if (chance != null)
		{
			long? num = chance;
			long num2 = 0L;
			if ((num.GetValueOrDefault() > num2) & (num != null))
			{
				num = chance;
				num2 = 1000L;
				if (!((num.GetValueOrDefault() >= num2) & (num != null)))
				{
					long num3 = (long)rnd.Next(1, 1001);
					num = chance;
					return (num3 <= num.GetValueOrDefault()) & (num != null);
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000189 RID: 393 RVA: 0x00008D5E File Offset: 0x00006F5E
	public static bool in10000(this long chance, global::System.Random rnd)
	{
		return chance > 0L && (chance >= 10000L || (long)rnd.Next(1, 10001) <= chance);
	}

	// Token: 0x0600018A RID: 394 RVA: 0x00008D88 File Offset: 0x00006F88
	public static bool in10000(this long? chance, global::System.Random rnd)
	{
		if (chance != null)
		{
			long? num = chance;
			long num2 = 0L;
			if ((num.GetValueOrDefault() > num2) & (num != null))
			{
				num = chance;
				num2 = 10000L;
				if (!((num.GetValueOrDefault() >= num2) & (num != null)))
				{
					long num3 = (long)rnd.Next(1, 10001);
					num = chance;
					return (num3 <= num.GetValueOrDefault()) & (num != null);
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600018B RID: 395 RVA: 0x00008DFE File Offset: 0x00006FFE
	public static bool in10(this float chance)
	{
		return chance > 0f && (chance >= 10f || (float)Stat.Random(1, 10) <= chance);
	}

	// Token: 0x0600018C RID: 396 RVA: 0x00008E24 File Offset: 0x00007024
	public static bool in10(this float? chance)
	{
		if (chance != null)
		{
			float? num = chance;
			float num2 = 0f;
			if ((num.GetValueOrDefault() > num2) & (num != null))
			{
				num = chance;
				num2 = (float)10;
				if (!((num.GetValueOrDefault() >= num2) & (num != null)))
				{
					float num3 = (float)Stat.Random(1, 10);
					num = chance;
					return (num3 <= num.GetValueOrDefault()) & (num != null);
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600018D RID: 397 RVA: 0x00008E96 File Offset: 0x00007096
	public static bool in100(this float chance)
	{
		return chance > 0f && (chance >= 100f || (float)Stat.Random(1, 100) <= chance);
	}

	// Token: 0x0600018E RID: 398 RVA: 0x00008EBC File Offset: 0x000070BC
	public static bool in100(this float? chance)
	{
		if (chance != null)
		{
			float? num = chance;
			float num2 = 0f;
			if ((num.GetValueOrDefault() > num2) & (num != null))
			{
				num = chance;
				num2 = (float)100;
				if (!((num.GetValueOrDefault() >= num2) & (num != null)))
				{
					float num3 = (float)Stat.Random(1, 100);
					num = chance;
					return (num3 <= num.GetValueOrDefault()) & (num != null);
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600018F RID: 399 RVA: 0x00008F2E File Offset: 0x0000712E
	public static bool in1000(this float chance)
	{
		return chance > 0f && (chance >= 1000f || (float)Stat.Random(1, 1000) <= chance);
	}

	// Token: 0x06000190 RID: 400 RVA: 0x00008F58 File Offset: 0x00007158
	public static bool in1000(this float? chance)
	{
		if (chance != null)
		{
			float? num = chance;
			float num2 = 0f;
			if ((num.GetValueOrDefault() > num2) & (num != null))
			{
				num = chance;
				num2 = (float)1000;
				if (!((num.GetValueOrDefault() >= num2) & (num != null)))
				{
					float num3 = (float)Stat.Random(1, 1000);
					num = chance;
					return (num3 <= num.GetValueOrDefault()) & (num != null);
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000191 RID: 401 RVA: 0x00008FD0 File Offset: 0x000071D0
	public static bool in10000(this float chance)
	{
		return chance > 0f && (chance >= 10000f || (float)Stat.Random(1, 10000) <= chance);
	}

	// Token: 0x06000192 RID: 402 RVA: 0x00008FF8 File Offset: 0x000071F8
	public static bool in10000(this float? chance)
	{
		if (chance != null)
		{
			float? num = chance;
			float num2 = 0f;
			if ((num.GetValueOrDefault() > num2) & (num != null))
			{
				num = chance;
				num2 = (float)10000;
				if (!((num.GetValueOrDefault() >= num2) & (num != null)))
				{
					float num3 = (float)Stat.Random(1, 10000);
					num = chance;
					return (num3 <= num.GetValueOrDefault()) & (num != null);
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000193 RID: 403 RVA: 0x00009070 File Offset: 0x00007270
	public static bool in10(this float chance, global::System.Random rnd)
	{
		return chance > 0f && (chance >= 10f || (float)rnd.Next(1, 11) <= chance);
	}

	// Token: 0x06000194 RID: 404 RVA: 0x00009098 File Offset: 0x00007298
	public static bool in10(this float? chance, global::System.Random rnd)
	{
		if (chance != null)
		{
			float? num = chance;
			float num2 = 0f;
			if ((num.GetValueOrDefault() > num2) & (num != null))
			{
				num = chance;
				num2 = (float)10;
				if (!((num.GetValueOrDefault() >= num2) & (num != null)))
				{
					float num3 = (float)rnd.Next(1, 11);
					num = chance;
					return (num3 <= num.GetValueOrDefault()) & (num != null);
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000195 RID: 405 RVA: 0x0000910B File Offset: 0x0000730B
	public static bool in100(this float chance, global::System.Random rnd)
	{
		return chance > 0f && (chance >= 100f || (float)rnd.Next(1, 101) <= chance);
	}

	// Token: 0x06000196 RID: 406 RVA: 0x00009134 File Offset: 0x00007334
	public static bool in100(this float? chance, global::System.Random rnd)
	{
		if (chance != null)
		{
			float? num = chance;
			float num2 = 0f;
			if ((num.GetValueOrDefault() > num2) & (num != null))
			{
				num = chance;
				num2 = (float)100;
				if (!((num.GetValueOrDefault() >= num2) & (num != null)))
				{
					float num3 = (float)rnd.Next(1, 101);
					num = chance;
					return (num3 <= num.GetValueOrDefault()) & (num != null);
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000197 RID: 407 RVA: 0x000091A7 File Offset: 0x000073A7
	public static bool in1000(this float chance, global::System.Random rnd)
	{
		return chance > 0f && (chance >= 1000f || (float)rnd.Next(1, 1001) <= chance);
	}

	// Token: 0x06000198 RID: 408 RVA: 0x000091D0 File Offset: 0x000073D0
	public static bool in1000(this float? chance, global::System.Random rnd)
	{
		if (chance != null)
		{
			float? num = chance;
			float num2 = 0f;
			if ((num.GetValueOrDefault() > num2) & (num != null))
			{
				num = chance;
				num2 = (float)1000;
				if (!((num.GetValueOrDefault() >= num2) & (num != null)))
				{
					float num3 = (float)rnd.Next(1, 1001);
					num = chance;
					return (num3 <= num.GetValueOrDefault()) & (num != null);
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000199 RID: 409 RVA: 0x00009249 File Offset: 0x00007449
	public static bool in10000(this float chance, global::System.Random rnd)
	{
		return chance > 0f && (chance >= 10000f || (float)rnd.Next(1, 10001) <= chance);
	}

	// Token: 0x0600019A RID: 410 RVA: 0x00009274 File Offset: 0x00007474
	public static bool in10000(this float? chance, global::System.Random rnd)
	{
		if (chance != null)
		{
			float? num = chance;
			float num2 = 0f;
			if ((num.GetValueOrDefault() > num2) & (num != null))
			{
				num = chance;
				num2 = (float)10000;
				if (!((num.GetValueOrDefault() >= num2) & (num != null)))
				{
					float num3 = (float)rnd.Next(1, 10001);
					num = chance;
					return (num3 <= num.GetValueOrDefault()) & (num != null);
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600019B RID: 411 RVA: 0x000092ED File Offset: 0x000074ED
	public static void AddIf<T>(this List<T> list, T element, Func<T, bool> conditional)
	{
		if (!conditional(element))
		{
			return;
		}
		list.Add(element);
	}

	// Token: 0x0600019C RID: 412 RVA: 0x00009300 File Offset: 0x00007500
	public static void AddIfNot<T>(this List<T> list, T element, Func<T, bool> conditional)
	{
		if (conditional(element))
		{
			return;
		}
		list.Add(element);
	}

	// Token: 0x0600019D RID: 413 RVA: 0x00009313 File Offset: 0x00007513
	public static void AddIfNotNull<T>(this List<T> list, T element)
	{
		if (element == null)
		{
			return;
		}
		list.Add(element);
	}

	// Token: 0x0600019E RID: 414 RVA: 0x00009325 File Offset: 0x00007525
	public static bool ShowSuccess(this global::XRL.World.GameObject Object, string Message, bool Log = true)
	{
		if (Object != null && Object.IsPlayer())
		{
			Popup.Show(Message, null, "Sounds/UI/ui_notification", true, true, true, Log, null);
		}
		return true;
	}

	// Token: 0x0600019F RID: 415 RVA: 0x00009344 File Offset: 0x00007544
	public static bool ShowFailure(this global::XRL.World.GameObject Object, string Message)
	{
		if (Object != null && Object.IsPlayer())
		{
			Popup.ShowFail(Message, true, true, true);
		}
		return false;
	}

	// Token: 0x060001A0 RID: 416 RVA: 0x0000935B File Offset: 0x0000755B
	public static void Return<T>(this ArrayPool<T> Pool, ref T[] Array, bool Clear = false)
	{
		Pool.Return(Array, Clear);
		Array = Array.Empty<T>();
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x0000936D File Offset: 0x0000756D
	public static List<T> ShuffleInPlace<T>(this List<T> list, global::System.Random R = null)
	{
		if (R == null)
		{
			Algorithms.RandomShuffleInPlace<T>(list);
		}
		else
		{
			Algorithms.RandomShuffleInPlace<T>(list, R);
		}
		return list;
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x00009382 File Offset: 0x00007582
	public static IList<T> ShuffleInPlace<T>(this IList<T> list, global::System.Random R = null)
	{
		if (R == null)
		{
			Algorithms.RandomShuffleInPlace<T>(list);
		}
		else
		{
			Algorithms.RandomShuffleInPlace<T>(list, R);
		}
		return list;
	}

	// Token: 0x060001A3 RID: 419 RVA: 0x00009397 File Offset: 0x00007597
	public static T[] ShuffleInPlace<T>(this T[] list, global::System.Random R = null)
	{
		if (R == null)
		{
			Algorithms.RandomShuffleInPlace<T>(list);
		}
		else
		{
			Algorithms.RandomShuffleInPlace<T>(list, R);
		}
		return list;
	}

	// Token: 0x060001A4 RID: 420 RVA: 0x000093AC File Offset: 0x000075AC
	public static IEnumerable<int> ShuffledRange(int low, int high, global::System.Random rng = null)
	{
		if (rng == null)
		{
			rng = Stat.Rnd;
		}
		int count = high - low;
		int num = count + 3;
		if (num % 2 == 0)
		{
			num++;
		}
		bool flag;
		do
		{
			num += 2;
			flag = true;
			int num2 = (int)Math.Sqrt((double)num) + 1;
			int num3 = 3;
			while (flag && num3 <= num2)
			{
				flag &= num % num3 != 0;
				num3 += 2;
			}
		}
		while (!flag);
		long a = (long)rng.Next(2, num);
		long pl = (long)num;
		int i = 0;
		int found = 0;
		while (found < count)
		{
			int num4 = i;
			i = num4 + 1;
			int num5 = (int)((long)i * a % pl);
			if (num5 < count)
			{
				num4 = found;
				found = num4 + 1;
				yield return num5 + low;
			}
		}
		yield break;
	}

	// Token: 0x060001A5 RID: 421 RVA: 0x000093CA File Offset: 0x000075CA
	public static IEnumerable<T> InRandomOrderNoAlloc<T>(this List<T> list, global::System.Random rng = null)
	{
		if (rng == null)
		{
			rng = Stat.Rnd;
		}
		foreach (int num in global::Extensions.ShuffledRange(0, list.Count - 1, rng))
		{
			yield return list[num];
		}
		IEnumerator<int> enumerator = null;
		yield break;
		yield break;
	}

	// Token: 0x060001A6 RID: 422 RVA: 0x000093E1 File Offset: 0x000075E1
	public static IEnumerable<T> InRandomOrder<T>(this List<T> list, global::System.Random R = null)
	{
		if (R == null)
		{
			R = Stat.Rnd;
		}
		foreach (int num in Enumerable.Range(0, list.Count).ToList<int>().Shuffle<int>(R))
		{
			yield return list[num];
		}
		List<int>.Enumerator enumerator = default(List<int>.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x000093F8 File Offset: 0x000075F8
	public static List<T> Shuffle<T>(this List<T> list, global::System.Random R = null)
	{
		if (R == null)
		{
			return new List<T>(Algorithms.RandomShuffle<T>(list));
		}
		return new List<T>(Algorithms.RandomShuffle<T>(list, R));
	}

	// Token: 0x060001A8 RID: 424 RVA: 0x00009415 File Offset: 0x00007615
	public static double toRadians(this double angle)
	{
		return 3.141592653589793 * angle / 180.0;
	}

	// Token: 0x060001A9 RID: 425 RVA: 0x0000942C File Offset: 0x0000762C
	public static float toRadians(this float angle)
	{
		return (float)((double)angle).toRadians();
	}

	// Token: 0x060001AA RID: 426 RVA: 0x00009436 File Offset: 0x00007636
	public static float toRadians(this int angle)
	{
		return (float)((double)angle).toRadians();
	}

	// Token: 0x060001AB RID: 427 RVA: 0x00009440 File Offset: 0x00007640
	public static double toDegrees(this double angle)
	{
		return angle * 57.29577951308232;
	}

	// Token: 0x060001AC RID: 428 RVA: 0x0000944D File Offset: 0x0000764D
	public static float toDegrees(this float angle)
	{
		return (float)((double)angle).toDegrees();
	}

	// Token: 0x060001AD RID: 429 RVA: 0x00009457 File Offset: 0x00007657
	public static double normalizeRadians(this double angle)
	{
		return angle - 6.283185307179586 * Math.Floor((angle + 3.141592653589793) / 6.283185307179586);
	}

	// Token: 0x060001AE RID: 430 RVA: 0x0000947F File Offset: 0x0000767F
	public static float normalizeRadians(this float angle)
	{
		return (float)((double)angle).normalizeRadians();
	}

	// Token: 0x060001AF RID: 431 RVA: 0x00009489 File Offset: 0x00007689
	public static int normalizeDegrees(this int angle)
	{
		return angle % 360;
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x00009494 File Offset: 0x00007694
	public static string DebugNode(this XmlTextReader Reader)
	{
		if (Reader.NodeType == XmlNodeType.Element || Reader.NodeType == XmlNodeType.EndElement)
		{
			string text = string.Concat(new string[]
			{
				Reader.LineNumber.ToString(),
				":",
				Reader.LinePosition.ToString(),
				(Reader.NodeType == XmlNodeType.EndElement && !Reader.IsEmptyElement) ? "</" : "<",
				Reader.Name
			});
			bool isEmptyElement = Reader.IsEmptyElement;
			if (Reader.HasAttributes)
			{
				while (Reader.MoveToNextAttribute())
				{
					text = string.Concat(new string[] { text, " ", Reader.Name, "=\"", Reader.Value, "\"" });
				}
			}
			return text + (isEmptyElement ? " />" : ">");
		}
		return "[" + Reader.NodeType.ToString() + "]";
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x000095A8 File Offset: 0x000077A8
	public static bool SkipToEnd(this XmlTextReader Reader)
	{
		if (Reader.NodeType == XmlNodeType.EndElement || Reader.IsEmptyElement)
		{
			return true;
		}
		int depth = Reader.Depth;
		while (Reader.Read())
		{
			if (Reader.NodeType == XmlNodeType.EndElement && depth == Reader.Depth)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060001B2 RID: 434 RVA: 0x000095F0 File Offset: 0x000077F0
	public static double DiminishingReturns(this double val, double scale)
	{
		if (val < 0.0)
		{
			return -(-val).DiminishingReturns(scale);
		}
		double num = val / scale;
		return (Math.Sqrt(8.0 * num + 1.0) - 1.0) / 2.0 * scale;
	}

	// Token: 0x060001B3 RID: 435 RVA: 0x00009648 File Offset: 0x00007848
	public static float DiminishingReturns(this float val, double scale)
	{
		if (val < 0f)
		{
			return -(-val).DiminishingReturns(scale);
		}
		double num = (double)val / scale;
		return (float)((Math.Sqrt(8.0 * num + 1.0) - 1.0) / 2.0 * scale);
	}

	// Token: 0x060001B4 RID: 436 RVA: 0x000096A0 File Offset: 0x000078A0
	public static int DiminishingReturns(this int val, double scale)
	{
		if (val < 0)
		{
			return -(-val).DiminishingReturns(scale);
		}
		double num = (double)val / scale;
		return (int)((Math.Sqrt(8.0 * num + 1.0) - 1.0) / 2.0 * scale);
	}

	// Token: 0x060001B5 RID: 437 RVA: 0x000096F1 File Offset: 0x000078F1
	public static bool EqualsNoCase(this string val, string cmp)
	{
		return string.Equals(val, cmp, StringComparison.OrdinalIgnoreCase);
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x000096FB File Offset: 0x000078FB
	public static bool EqualsNoCase(this string val, ReadOnlySpan<char> cmp)
	{
		return cmp.Equals(val, StringComparison.OrdinalIgnoreCase);
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x0000970A File Offset: 0x0000790A
	public static bool EqualsNoCase(this ReadOnlySpan<char> val, ReadOnlySpan<char> cmp)
	{
		return val.Equals(cmp, StringComparison.OrdinalIgnoreCase);
	}

	// Token: 0x060001B8 RID: 440 RVA: 0x00009714 File Offset: 0x00007914
	public static uint GetStableHashCode32(this string Value)
	{
		return Hash.FNV1A32(Value, 2166136261U);
	}

	// Token: 0x060001B9 RID: 441 RVA: 0x00009721 File Offset: 0x00007921
	public static uint GetStableHashCode32(this string Value, uint Hash)
	{
		return Hash.FNV1A32(Value, Hash);
	}

	// Token: 0x060001BA RID: 442 RVA: 0x0000972A File Offset: 0x0000792A
	public static void GetStableHashCode32(this string Value, ref uint Hash)
	{
		Hash = Hash.FNV1A32(Value, Hash);
	}

	// Token: 0x060001BB RID: 443 RVA: 0x00009736 File Offset: 0x00007936
	public static ulong GetStableHashCode64(this string Value)
	{
		return Hash.FNV1A64(Value, 14695981039346656037UL);
	}

	// Token: 0x060001BC RID: 444 RVA: 0x00009747 File Offset: 0x00007947
	public static ulong GetStableHashCode64(this string Value, ulong Hash)
	{
		return Hash.FNV1A64(Value, Hash);
	}

	// Token: 0x060001BD RID: 445 RVA: 0x00009750 File Offset: 0x00007950
	public static void GetStableHashCode64(this string Value, ref ulong Hash)
	{
		Hash = Hash.FNV1A64(Value, Hash);
	}

	// Token: 0x060001BE RID: 446 RVA: 0x0000975C File Offset: 0x0000795C
	public static ulong GetStableHashCode64(this Type Type, BindingFlags Flags = BindingFlags.Instance | BindingFlags.Public, FieldAttributes Mask = FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized)
	{
		FieldInfo[] fields = Type.GetFields(Flags);
		ulong stableHashCode = Type.GetName(true).GetStableHashCode64();
		int i = 0;
		int num = fields.Length;
		while (i < num)
		{
			if ((fields[i].Attributes & Mask) == FieldAttributes.PrivateScope)
			{
				fields[i].FieldType.GetName(true).GetStableHashCode64(ref stableHashCode);
			}
			i++;
		}
		return stableHashCode;
	}

	// Token: 0x060001BF RID: 447 RVA: 0x000097B0 File Offset: 0x000079B0
	public static bool EqualsEmptyEqualsNull(this string val, string cmp)
	{
		return val == cmp || (string.IsNullOrEmpty(val) && string.IsNullOrEmpty(cmp));
	}

	// Token: 0x060001C0 RID: 448 RVA: 0x000097D0 File Offset: 0x000079D0
	public static T Deserialize<T>(this JsonSerializer serializer, string filePath)
	{
		T t;
		using (Stream stream = Platform.IO.File.OpenRead(filePath))
		{
			using (StreamReader streamReader = new StreamReader(stream))
			{
				using (JsonTextReader jsonTextReader = new JsonTextReader(streamReader))
				{
					t = serializer.Deserialize<T>(jsonTextReader);
				}
			}
		}
		return t;
	}

	// Token: 0x060001C1 RID: 449 RVA: 0x00009844 File Offset: 0x00007A44
	public static T Decompress<T>(this JsonSerializer serializer, string file)
	{
		T t;
		using (Stream stream = Platform.IO.File.OpenRead(file))
		{
			using (GZipStream gzipStream = new GZipStream(stream, global::System.IO.Compression.CompressionLevel.Fastest))
			{
				using (StreamReader streamReader = new StreamReader(gzipStream))
				{
					using (JsonTextReader jsonTextReader = new JsonTextReader(streamReader))
					{
						t = serializer.Deserialize<T>(jsonTextReader);
					}
				}
			}
		}
		return t;
	}

	// Token: 0x060001C2 RID: 450 RVA: 0x000098D8 File Offset: 0x00007AD8
	public static void Serialize(this JsonSerializer serializer, string file, object value)
	{
		using (Stream stream = Platform.IO.File.OpenWrite(file))
		{
			using (StreamWriter streamWriter = new StreamWriter(stream))
			{
				using (JsonTextWriter jsonTextWriter = new JsonTextWriter(streamWriter))
				{
					serializer.Serialize(jsonTextWriter, value);
				}
			}
		}
	}

	// Token: 0x060001C3 RID: 451 RVA: 0x0000994C File Offset: 0x00007B4C
	public static void Compress(this JsonSerializer serializer, string file, object value)
	{
		using (Stream stream = Platform.IO.File.OpenWrite(file))
		{
			using (GZipStream gzipStream = new GZipStream(stream, global::System.IO.Compression.CompressionLevel.Fastest))
			{
				using (StreamWriter streamWriter = new StreamWriter(gzipStream))
				{
					using (JsonTextWriter jsonTextWriter = new JsonTextWriter(streamWriter))
					{
						serializer.Serialize(jsonTextWriter, value);
					}
				}
			}
		}
	}

	// Token: 0x060001C4 RID: 452 RVA: 0x000099DC File Offset: 0x00007BDC
	public static int IndexOf<T>(this T[] Self, T Value)
	{
		return Array.IndexOf<T>(Self, Value);
	}

	// Token: 0x060001C5 RID: 453 RVA: 0x000099E5 File Offset: 0x00007BE5
	public static bool Contains<T>(this T[] Self, T Value)
	{
		return Array.IndexOf<T>(Self, Value) != -1;
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x000099F4 File Offset: 0x00007BF4
	public static T[][] With<T>(this T[][] Array, int Size)
	{
		int i = 0;
		int num = Array.Length;
		while (i < num)
		{
			Array[i] = new T[Size];
			i++;
		}
		return Array;
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x00009A1C File Offset: 0x00007C1C
	public static int IndexOf<T>(this IReadOnlyList<T> Self, T Value)
	{
		EqualityComparer<T> @default = EqualityComparer<T>.Default;
		for (int i = 0; i < Self.Count; i++)
		{
			if (@default.Equals(Self[i], Value))
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x060001C8 RID: 456 RVA: 0x00009A54 File Offset: 0x00007C54
	public static bool Contains<T>(this IReadOnlyList<T> Self, T Value)
	{
		EqualityComparer<T> @default = EqualityComparer<T>.Default;
		for (int i = 0; i < Self.Count; i++)
		{
			if (@default.Equals(Self[i], Value))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060001C9 RID: 457 RVA: 0x00009A8C File Offset: 0x00007C8C
	public static void Populate(this JsonSerializer serializer, string file, object target)
	{
		using (Stream stream = Platform.IO.File.OpenRead(file))
		{
			using (StreamReader streamReader = new StreamReader(stream))
			{
				using (JsonTextReader jsonTextReader = new JsonTextReader(streamReader))
				{
					serializer.Populate(jsonTextReader, target);
				}
			}
		}
	}

	// Token: 0x060001CA RID: 458 RVA: 0x00009B00 File Offset: 0x00007D00
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ReadLockSlim TakeReadLock(this ReaderWriterLockSlim Lock)
	{
		Lock.EnterReadLock();
		return new ReadLockSlim(Lock);
	}

	// Token: 0x060001CB RID: 459 RVA: 0x00009B0E File Offset: 0x00007D0E
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static UpgradeableReadLockSlim TakeUpgradeableReadLock(this ReaderWriterLockSlim Lock)
	{
		Lock.EnterUpgradeableReadLock();
		return new UpgradeableReadLockSlim(Lock);
	}

	// Token: 0x060001CC RID: 460 RVA: 0x00009B1C File Offset: 0x00007D1C
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static WriteLockSlim TakeWriteLock(this ReaderWriterLockSlim Lock)
	{
		Lock.EnterWriteLock();
		return new WriteLockSlim(Lock);
	}

	// Token: 0x060001CD RID: 461 RVA: 0x00009B2A File Offset: 0x00007D2A
	public static ConstructorInfo GetDefaultConstructor(this Type Value, BindingFlags Flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
	{
		return Value.GetConstructor(Flags, null, Type.EmptyTypes, null);
	}

	// Token: 0x060001CE RID: 462 RVA: 0x00009B3A File Offset: 0x00007D3A
	public static IEnumerable<Type> YieldInheritedTypes(this Type Value)
	{
		yield return Value.BaseType;
		foreach (Type type in Value.GetInterfaces())
		{
			yield return type;
		}
		Type[] array = null;
		yield break;
	}

	// Token: 0x060001CF RID: 463 RVA: 0x00009B4C File Offset: 0x00007D4C
	public static int GetSubclassDepth(this Type Type)
	{
		Type type = Type;
		int num = 0;
		for (;;)
		{
			type = type.BaseType;
			if (type == null)
			{
				break;
			}
			num++;
		}
		return num;
	}

	// Token: 0x060001D0 RID: 464 RVA: 0x00009B70 File Offset: 0x00007D70
	public static int GetSubclassDepth(this Type Type, Type BaseType)
	{
		Type type = Type;
		int num = 0;
		while (type != BaseType)
		{
			type = type.BaseType;
			if (type == null)
			{
				throw new ArgumentException(Type.Name + " is not a subclass of " + BaseType.Name + ".");
			}
			num++;
		}
		return num;
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x00009BB7 File Offset: 0x00007DB7
	public static bool IsSubclassOfGeneric(this Type Current, Type Base)
	{
		while (!Current.IsGenericType || Current.GetGenericTypeDefinition() != Base)
		{
			Current = Current.BaseType;
			if (!(Current != null))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x00009BE0 File Offset: 0x00007DE0
	public static string GetName(this MethodInfo Method, bool Signature = false, bool Full = false)
	{
		if (!Full && !Signature)
		{
			return Method.Name;
		}
		string text;
		using (TextBuilder textBuilder = TextBuilder.Get(64))
		{
			Method.AppendName(textBuilder, Signature, Full);
			text = textBuilder.ToString();
		}
		return text;
	}

	// Token: 0x060001D3 RID: 467 RVA: 0x00009C30 File Offset: 0x00007E30
	public static void AppendName(this MethodInfo Method, TextBuilder Builder, bool Signature = false, bool Full = false)
	{
		if (Full)
		{
			Method.DeclaringType.AppendName(Builder, Full);
			Builder.Append('.');
		}
		Builder.Append(Method.Name);
		if (Signature)
		{
			ParameterInfo[] parameters = Method.GetParameters();
			Builder.Append('(');
			for (int i = 0; i < parameters.Length; i++)
			{
				if (i != 0)
				{
					Builder.Append(", ");
				}
				parameters[i].ParameterType.AppendName(Builder, Full);
			}
			Builder.Append(')');
		}
	}

	// Token: 0x060001D4 RID: 468 RVA: 0x00009CAC File Offset: 0x00007EAC
	public static string GetName(this Type Type, bool Full = false)
	{
		bool flag = true;
		Type type = Nullable.GetUnderlyingType(Type);
		if (type == null)
		{
			flag = false;
			type = Type;
		}
		bool isGenericType = type.IsGenericType;
		if (flag || isGenericType)
		{
			string text;
			using (TextBuilder textBuilder = TextBuilder.Get(64))
			{
				global::Extensions.AppendNameInternal(type, textBuilder, flag, isGenericType, Full);
				text = textBuilder.ToString();
			}
			return text;
		}
		if (!Full)
		{
			return type.Name;
		}
		return type.FullName;
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x00009D20 File Offset: 0x00007F20
	public static void AppendName(this Type Type, TextBuilder Builder, bool Full = false)
	{
		bool flag = true;
		Type type = Nullable.GetUnderlyingType(Type);
		if (type == null)
		{
			flag = false;
			type = Type;
		}
		global::Extensions.AppendNameInternal(type, Builder, flag, type.IsGenericType, Full);
	}

	// Token: 0x060001D6 RID: 470 RVA: 0x00009D4C File Offset: 0x00007F4C
	private static void AppendNameInternal(Type Type, TextBuilder Builder, bool Nullable, bool Generic, bool Full)
	{
		string text = (Full ? Type.FullName : Type.Name);
		if (Generic)
		{
			int num = text.IndexOf('`');
			if (num >= 0)
			{
				Builder.Append(text, 0, num);
			}
			else
			{
				Builder.Append(text);
			}
			Builder.Append('<');
			Type[] genericArguments = Type.GetGenericArguments();
			for (int i = 0; i < genericArguments.Length; i++)
			{
				if (i != 0)
				{
					Builder.Append(',');
				}
				genericArguments[i].AppendName(Builder, Full);
			}
			Builder.Append('>');
		}
		else
		{
			Builder.Append(text);
		}
		if (Nullable)
		{
			Builder.Append('?');
		}
	}

	// Token: 0x060001D7 RID: 471 RVA: 0x00009DE4 File Offset: 0x00007FE4
	public static FieldInfo[] GetCachedFields(this Type Type)
	{
		FieldInfo[] array;
		if (!global::Extensions.TypeFields.TryGetValue(Type, out array))
		{
			array = (global::Extensions.TypeFields[Type] = Type.GetFields(BindingFlags.Instance | BindingFlags.Public));
		}
		return array;
	}

	// Token: 0x040000DC RID: 220
	[TupleElementNames(new string[] { "s", "blockWrap", "stripFormatting" })]
	private static LRUCache<ValueTuple<string, int, bool>, string> CachedRTFExpansions = new LRUCache<ValueTuple<string, int, bool>, string>(256, new global::Extensions.RTFCacheGenerator(), null);

	// Token: 0x040000DD RID: 221
	private static LRUCache<int, string> CachedIntStringifications = new LRUCache<int, string>(256, new global::Extensions.IntStringGenerator(), null);

	// Token: 0x040000DE RID: 222
	private static LRUCache<string, List<string>> CachedCommaExpansions = new LRUCache<string, List<string>>(128, new global::Extensions.CommaExpressionGenerator(), null);

	// Token: 0x040000DF RID: 223
	private static Dictionary<string, List<string>> CachedDoubleSemicolonExpansions = new Dictionary<string, List<string>>(16);

	// Token: 0x040000E0 RID: 224
	private static string LastDoubleSemicolonExpansionRequest;

	// Token: 0x040000E1 RID: 225
	private static List<string> LastDoubleSemicolonExpansionResult;

	// Token: 0x040000E2 RID: 226
	private static Dictionary<string, Dictionary<string, string>> CachedDictionaryExpansions = new Dictionary<string, Dictionary<string, string>>(32);

	// Token: 0x040000E3 RID: 227
	private static string LastDictionaryExpansionRequest;

	// Token: 0x040000E4 RID: 228
	private static Dictionary<string, string> LastDictionaryExpansionResult;

	// Token: 0x040000E5 RID: 229
	private static Dictionary<string, Dictionary<string, int>> CachedNumericDictionaryExpansions = new Dictionary<string, Dictionary<string, int>>(8);

	// Token: 0x040000E6 RID: 230
	private static string LastNumericDictionaryExpansionRequest;

	// Token: 0x040000E7 RID: 231
	private static Dictionary<string, int> LastNumericDictionaryExpansionResult;

	// Token: 0x040000E8 RID: 232
	private static Dictionary<Type, FieldInfo[]> TypeFields = new Dictionary<Type, FieldInfo[]>(2560);

	// Token: 0x02001556 RID: 5462
	private static class RemoveKeyCache<T>
	{
		// Token: 0x040055D5 RID: 21973
		public static List<T> Keys = new List<T>(16);
	}

	// Token: 0x02001557 RID: 5463
	private class RTFCacheGenerator : LRUCache<ValueTuple<string, int, bool>, string>.IGenerator
	{
		// Token: 0x0600D4D9 RID: 54489 RVA: 0x004F1831 File Offset: 0x004EFA31
		public string Generate([TupleElementNames(new string[] { "s", "blockWrap", "stripFormatting" })] ValueTuple<string, int, bool> Key)
		{
			return RTF.FormatToRTF(Key.Item1, "FF", Key.Item2, Key.Item3);
		}
	}

	// Token: 0x02001558 RID: 5464
	private class IntStringGenerator : LRUCache<int, string>.IGenerator
	{
		// Token: 0x0600D4DB RID: 54491 RVA: 0x004F1857 File Offset: 0x004EFA57
		public string Generate(int Key)
		{
			return Key.ToString();
		}
	}

	// Token: 0x02001559 RID: 5465
	private class CommaExpressionGenerator : LRUCache<string, List<string>>.IGenerator
	{
		// Token: 0x0600D4DD RID: 54493 RVA: 0x004F1868 File Offset: 0x004EFA68
		public List<string> Generate(string Key)
		{
			return Key.Split(',', StringSplitOptions.None).ToList<string>();
		}
	}
}
