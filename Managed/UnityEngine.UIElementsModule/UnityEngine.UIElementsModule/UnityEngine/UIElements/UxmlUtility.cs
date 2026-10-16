using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000590 RID: 1424
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal static class UxmlUtility
	{
		// Token: 0x06002A96 RID: 10902 RVA: 0x00098FA8 File Offset: 0x000971A8
		public static List<string> ParseStringListAttribute(string itemList)
		{
			bool flag = string.IsNullOrEmpty((itemList != null) ? itemList.Trim() : null);
			List<string> list;
			if (flag)
			{
				list = null;
			}
			else
			{
				string[] array = itemList.Split(',', StringSplitOptions.None);
				bool flag2 = array.Length != 0;
				if (flag2)
				{
					List<string> list2 = new List<string>();
					foreach (string text in array)
					{
						list2.Add(text.Trim());
					}
					list = list2;
				}
				else
				{
					list = null;
				}
			}
			return list;
		}

		// Token: 0x06002A97 RID: 10903 RVA: 0x00099024 File Offset: 0x00097224
		public static void MoveListItem(IList list, int src, int dst)
		{
			object obj = list[src];
			list.RemoveAt(src);
			list.Insert(dst, obj);
		}

		// Token: 0x06002A98 RID: 10904 RVA: 0x0009904C File Offset: 0x0009724C
		public static float ParseFloat(string value, float defaultValue = 0f)
		{
			float num;
			return float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out num) ? num : defaultValue;
		}

		// Token: 0x06002A99 RID: 10905 RVA: 0x00099078 File Offset: 0x00097278
		public static byte ParseByte(string value, byte defaultValue = 0)
		{
			byte b;
			return byte.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out b) ? b : defaultValue;
		}

		// Token: 0x06002A9A RID: 10906 RVA: 0x000990A0 File Offset: 0x000972A0
		public static sbyte ParseSByte(string value, sbyte defaultValue = 0)
		{
			sbyte b;
			return sbyte.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out b) ? b : defaultValue;
		}

		// Token: 0x06002A9B RID: 10907 RVA: 0x000990C8 File Offset: 0x000972C8
		public static short ParseShort(string value, short defaultValue = 0)
		{
			short num;
			return short.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out num) ? num : defaultValue;
		}

		// Token: 0x06002A9C RID: 10908 RVA: 0x000990F0 File Offset: 0x000972F0
		public static ushort ParseUShort(string value, ushort defaultValue = 0)
		{
			ushort num;
			return ushort.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out num) ? num : defaultValue;
		}

		// Token: 0x06002A9D RID: 10909 RVA: 0x00099118 File Offset: 0x00097318
		public static int ParseInt(string value, int defaultValue = 0)
		{
			int num;
			return int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out num) ? num : defaultValue;
		}

		// Token: 0x06002A9E RID: 10910 RVA: 0x00099140 File Offset: 0x00097340
		public static uint ParseUint(string value, uint defaultValue = 0U)
		{
			uint num;
			return uint.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out num) ? num : defaultValue;
		}

		// Token: 0x06002A9F RID: 10911 RVA: 0x00099168 File Offset: 0x00097368
		public static float TryParseFloatAttribute(string attributeName, IUxmlAttributes bag, ref int foundAttributeCounter)
		{
			string text;
			bool flag = bag.TryGetAttributeValue(attributeName, out text);
			float num;
			if (flag)
			{
				foundAttributeCounter++;
				num = UxmlUtility.ParseFloat(text, 0f);
			}
			else
			{
				num = 0f;
			}
			return num;
		}

		// Token: 0x06002AA0 RID: 10912 RVA: 0x000991A4 File Offset: 0x000973A4
		public static int TryParseIntAttribute(string attributeName, IUxmlAttributes bag, ref int foundAttributeCounter)
		{
			string text;
			bool flag = bag.TryGetAttributeValue(attributeName, out text);
			int num;
			if (flag)
			{
				foundAttributeCounter++;
				num = UxmlUtility.ParseInt(text, 0);
			}
			else
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x06002AA1 RID: 10913 RVA: 0x000991D8 File Offset: 0x000973D8
		public static Type ParseType(string value, Type defaultType = null)
		{
			try
			{
				bool flag = !string.IsNullOrEmpty(value);
				if (flag)
				{
					return Type.GetType(value, true);
				}
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
			return defaultType;
		}

		// Token: 0x06002AA2 RID: 10914 RVA: 0x00099228 File Offset: 0x00097428
		public static string ValidateUxmlName(string name)
		{
			bool flag = !char.IsLetter(name[0]) && name[0] != '_';
			string text;
			if (flag)
			{
				text = "Element names must start with a letter or underscore";
			}
			else
			{
				bool flag2 = name.StartsWith("xml", StringComparison.OrdinalIgnoreCase);
				if (flag2)
				{
					text = "Element names cannot start with the letters xml (or XML, or Xml, etc)";
				}
				else
				{
					for (int i = 1; i < name.Length; i++)
					{
						char c = name[i];
						bool flag3 = char.IsWhiteSpace(c) || (!char.IsLetterOrDigit(c) && c != '-' && c != '_' && c != '.');
						if (flag3)
						{
							return string.Format("The character '{0}' is invalid. Element names can contain letters, digits, hyphens, underscores, and periods.", c);
						}
					}
					text = null;
				}
			}
			return text;
		}

		// Token: 0x06002AA3 RID: 10915 RVA: 0x000992EC File Offset: 0x000974EC
		public static string TypeToString(Type value)
		{
			bool flag = value == null;
			string text;
			if (flag)
			{
				text = null;
			}
			else
			{
				text = value.FullName + ", " + value.Assembly.GetName().Name;
			}
			return text;
		}

		// Token: 0x06002AA4 RID: 10916 RVA: 0x00099330 File Offset: 0x00097530
		public static string ValueToString(Bounds value)
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("{0},{1},{2},{3},{4},{5}", new object[]
			{
				value.center.x,
				value.center.y,
				value.center.z,
				value.size.x,
				value.size.y,
				value.size.z
			}));
		}

		// Token: 0x06002AA5 RID: 10917 RVA: 0x000993CC File Offset: 0x000975CC
		public static string ValueToString(BoundsInt value)
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("{0},{1},{2},{3},{4},{5}", new object[]
			{
				value.position.x,
				value.position.y,
				value.position.z,
				value.size.x,
				value.size.y,
				value.size.z
			}));
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x00099478 File Offset: 0x00097678
		public static string ValueToString(Rect value)
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("{0},{1},{2},{3}", new object[] { value.x, value.y, value.width, value.height }));
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x000994D8 File Offset: 0x000976D8
		public static string ValueToString(RectInt value)
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("{0},{1},{2},{3}", new object[] { value.x, value.y, value.width, value.height }));
		}

		// Token: 0x06002AA8 RID: 10920 RVA: 0x00099536 File Offset: 0x00097736
		public static string ValueToString(Vector2 value)
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("{0},{1}", new object[] { value.x, value.y }));
		}

		// Token: 0x06002AA9 RID: 10921 RVA: 0x00099569 File Offset: 0x00097769
		public static string ValueToString(Vector2Int value)
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("{0},{1}", new object[] { value.x, value.y }));
		}

		// Token: 0x06002AAA RID: 10922 RVA: 0x000995A0 File Offset: 0x000977A0
		public static string ValueToString(Vector3 value)
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("{0},{1},{2}", new object[] { value.x, value.y, value.z }));
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x000995EC File Offset: 0x000977EC
		public static string ValueToString(Vector3Int value)
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("{0},{1},{2}", new object[] { value.x, value.y, value.z }));
		}

		// Token: 0x06002AAC RID: 10924 RVA: 0x0009963C File Offset: 0x0009783C
		public static string ValueToString(Vector4 value)
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("{0},{1},{2},{3}", new object[] { value.x, value.y, value.z, value.w }));
		}

		// Token: 0x06002AAD RID: 10925 RVA: 0x00099698 File Offset: 0x00097898
		public static object CloneObject(object value)
		{
			bool flag = value != null && !(value is string) && !(value is Type) && value.GetType().IsClass;
			object obj;
			if (flag)
			{
				obj = UxmlSerializedDataUtility.CopySerialized(value);
			}
			else
			{
				obj = value;
			}
			return obj;
		}
	}
}
