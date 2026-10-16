using System;
using System.Collections.Generic;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000073 RID: 115
	public static class TMPro_ExtensionMethods
	{
		// Token: 0x060005AB RID: 1451 RVA: 0x0002DCD1 File Offset: 0x0002BED1
		internal static int TagToInt(this string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return 0;
			}
			return (int)(((int)s[0] << 24) | ((int)s[1] << 16) | ((int)s[2] << 8) | s[3]);
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0002DD04 File Offset: 0x0002BF04
		public static int[] ToIntArray(this string text)
		{
			int[] array = new int[text.Length];
			for (int i = 0; i < text.Length; i++)
			{
				array[i] = (int)text[i];
			}
			return array;
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0002DD3C File Offset: 0x0002BF3C
		public static string ArrayToString(this char[] chars)
		{
			string text = string.Empty;
			int num = 0;
			while (num < chars.Length && chars[num] != '\0')
			{
				text += chars[num].ToString();
				num++;
			}
			return text;
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0002DD78 File Offset: 0x0002BF78
		public static string IntToString(this int[] unicodes)
		{
			char[] array = new char[unicodes.Length];
			for (int i = 0; i < unicodes.Length; i++)
			{
				array[i] = (char)unicodes[i];
			}
			return new string(array);
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0002DDAC File Offset: 0x0002BFAC
		internal static string UintToString(this List<uint> unicodes)
		{
			char[] array = new char[unicodes.Count];
			for (int i = 0; i < unicodes.Count; i++)
			{
				array[i] = (char)unicodes[i];
			}
			return new string(array);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0002DDE8 File Offset: 0x0002BFE8
		public static string IntToString(this int[] unicodes, int start, int length)
		{
			if (start > unicodes.Length)
			{
				return string.Empty;
			}
			int num = Mathf.Min(start + length, unicodes.Length);
			char[] array = new char[num - start];
			int num2 = 0;
			for (int i = start; i < num; i++)
			{
				array[num2++] = (char)unicodes[i];
			}
			return new string(array);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0002DE38 File Offset: 0x0002C038
		public static int FindInstanceID<T>(this List<T> list, T target) where T : global::UnityEngine.Object
		{
			int instanceID = target.GetInstanceID();
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].GetInstanceID() == instanceID)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0002DE79 File Offset: 0x0002C079
		public static bool Compare(this Color32 a, Color32 b)
		{
			return a.r == b.r && a.g == b.g && a.b == b.b && a.a == b.a;
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0002DEB5 File Offset: 0x0002C0B5
		public static bool CompareRGB(this Color32 a, Color32 b)
		{
			return a.r == b.r && a.g == b.g && a.b == b.b;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0002DEE3 File Offset: 0x0002C0E3
		public static bool Compare(this Color a, Color b)
		{
			return a.r == b.r && a.g == b.g && a.b == b.b && a.a == b.a;
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0002DF1F File Offset: 0x0002C11F
		public static bool CompareRGB(this Color a, Color b)
		{
			return a.r == b.r && a.g == b.g && a.b == b.b;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0002DF50 File Offset: 0x0002C150
		public static Color32 Multiply(this Color32 c1, Color32 c2)
		{
			byte b = (byte)((float)c1.r / 255f * ((float)c2.r / 255f) * 255f);
			byte b2 = (byte)((float)c1.g / 255f * ((float)c2.g / 255f) * 255f);
			byte b3 = (byte)((float)c1.b / 255f * ((float)c2.b / 255f) * 255f);
			byte b4 = (byte)((float)c1.a / 255f * ((float)c2.a / 255f) * 255f);
			return new Color32(b, b2, b3, b4);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0002DFF0 File Offset: 0x0002C1F0
		public static Color32 Tint(this Color32 c1, Color32 c2)
		{
			byte b = (byte)((float)c1.r / 255f * ((float)c2.r / 255f) * 255f);
			byte b2 = (byte)((float)c1.g / 255f * ((float)c2.g / 255f) * 255f);
			byte b3 = (byte)((float)c1.b / 255f * ((float)c2.b / 255f) * 255f);
			byte b4 = (byte)((float)c1.a / 255f * ((float)c2.a / 255f) * 255f);
			return new Color32(b, b2, b3, b4);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0002E090 File Offset: 0x0002C290
		public static Color32 Tint(this Color32 c1, float tint)
		{
			byte b = (byte)Mathf.Clamp((float)c1.r / 255f * tint * 255f, 0f, 255f);
			byte b2 = (byte)Mathf.Clamp((float)c1.g / 255f * tint * 255f, 0f, 255f);
			byte b3 = (byte)Mathf.Clamp((float)c1.b / 255f * tint * 255f, 0f, 255f);
			byte b4 = (byte)Mathf.Clamp((float)c1.a / 255f * tint * 255f, 0f, 255f);
			return new Color32(b, b2, b3, b4);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0002E13C File Offset: 0x0002C33C
		internal static Color32 GammaToLinear(this Color32 c)
		{
			return new Color32(TMPro_ExtensionMethods.GammaToLinear(c.r), TMPro_ExtensionMethods.GammaToLinear(c.g), TMPro_ExtensionMethods.GammaToLinear(c.b), c.a);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0002E16C File Offset: 0x0002C36C
		private static byte GammaToLinear(byte value)
		{
			float num = (float)value / 255f;
			if (num <= 0.04045f)
			{
				return (byte)(num / 12.92f * 255f);
			}
			if (num < 1f)
			{
				return (byte)(Mathf.Pow((num + 0.055f) / 1.055f, 2.4f) * 255f);
			}
			if (num == 1f)
			{
				return byte.MaxValue;
			}
			return (byte)(Mathf.Pow(num, 2.2f) * 255f);
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0002E1E0 File Offset: 0x0002C3E0
		public static Color MinAlpha(this Color c1, Color c2)
		{
			float num = ((c1.a < c2.a) ? c1.a : c2.a);
			return new Color(c1.r, c1.g, c1.b, num);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0002E224 File Offset: 0x0002C424
		public static bool Compare(this Vector3 v1, Vector3 v2, int accuracy)
		{
			bool flag = (int)(v1.x * (float)accuracy) == (int)(v2.x * (float)accuracy);
			bool flag2 = (int)(v1.y * (float)accuracy) == (int)(v2.y * (float)accuracy);
			bool flag3 = (int)(v1.z * (float)accuracy) == (int)(v2.z * (float)accuracy);
			return flag && flag2 && flag3;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0002E27C File Offset: 0x0002C47C
		public static bool Compare(this Quaternion q1, Quaternion q2, int accuracy)
		{
			bool flag = (int)(q1.x * (float)accuracy) == (int)(q2.x * (float)accuracy);
			bool flag2 = (int)(q1.y * (float)accuracy) == (int)(q2.y * (float)accuracy);
			bool flag3 = (int)(q1.z * (float)accuracy) == (int)(q2.z * (float)accuracy);
			bool flag4 = (int)(q1.w * (float)accuracy) == (int)(q2.w * (float)accuracy);
			return flag && flag2 && flag3 && flag4;
		}
	}
}
