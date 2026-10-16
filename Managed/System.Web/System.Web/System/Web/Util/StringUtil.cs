using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Web.Util
{
	// Token: 0x02000131 RID: 305
	internal static class StringUtil
	{
		// Token: 0x06000E34 RID: 3636 RVA: 0x00026228 File Offset: 0x00024428
		internal static string CheckAndTrimString(string paramValue, string paramName)
		{
			return StringUtil.CheckAndTrimString(paramValue, paramName, true);
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x00026232 File Offset: 0x00024432
		internal static string CheckAndTrimString(string paramValue, string paramName, bool throwIfNull)
		{
			return StringUtil.CheckAndTrimString(paramValue, paramName, throwIfNull, -1);
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x00026240 File Offset: 0x00024440
		internal static string CheckAndTrimString(string paramValue, string paramName, bool throwIfNull, int lengthToCheck)
		{
			if (paramValue == null)
			{
				if (throwIfNull)
				{
					throw new ArgumentNullException(paramName);
				}
				return null;
			}
			else
			{
				string text = paramValue.Trim();
				if (text.Length == 0)
				{
					throw new ArgumentException(global::SR.GetString("Input parameter '{0}' cannot be an empty string.", new object[] { paramName }));
				}
				if (lengthToCheck > -1 && text.Length > lengthToCheck)
				{
					throw new ArgumentException(global::SR.GetString("Trimmed string value '{0}' of input parameter '{1}' cannot exceed character length {2}.", new object[]
					{
						paramValue,
						paramName,
						lengthToCheck.ToString(CultureInfo.InvariantCulture)
					}));
				}
				return text;
			}
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x000262C0 File Offset: 0x000244C0
		internal static bool Equals(string s1, string s2)
		{
			return s1 == s2 || (string.IsNullOrEmpty(s1) && string.IsNullOrEmpty(s2));
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x000262E0 File Offset: 0x000244E0
		internal unsafe static bool Equals(string s1, int offset1, string s2, int offset2, int length)
		{
			if (offset1 < 0)
			{
				throw new ArgumentOutOfRangeException("offset1");
			}
			if (offset2 < 0)
			{
				throw new ArgumentOutOfRangeException("offset2");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			if (((s1 == null) ? 0 : s1.Length) - offset1 < length)
			{
				throw new ArgumentOutOfRangeException(global::SR.GetString("The sum of {0} and {1} is greater than the length of the buffer.", new object[] { "offset1", "length" }));
			}
			if (((s2 == null) ? 0 : s2.Length) - offset2 < length)
			{
				throw new ArgumentOutOfRangeException(global::SR.GetString("The sum of {0} and {1} is greater than the length of the buffer.", new object[] { "offset2", "length" }));
			}
			if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
			{
				return true;
			}
			fixed (string text = s1)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				fixed (string text2 = s2)
				{
					char* ptr2 = text2;
					if (ptr2 != null)
					{
						ptr2 += RuntimeHelpers.OffsetToStringData / 2;
					}
					char* ptr3 = ptr + offset1;
					char* ptr4 = ptr2 + offset2;
					int num = length;
					while (num-- > 0)
					{
						if (*(ptr3++) != *(ptr4++))
						{
							return false;
						}
					}
					text = null;
				}
				return true;
			}
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x000263F8 File Offset: 0x000245F8
		internal static bool EqualsIgnoreCase(string s1, string s2)
		{
			return (string.IsNullOrEmpty(s1) && string.IsNullOrEmpty(s2)) || (!string.IsNullOrEmpty(s1) && !string.IsNullOrEmpty(s2) && s2.Length == s1.Length && string.Compare(s1, 0, s2, 0, s2.Length, StringComparison.OrdinalIgnoreCase) == 0);
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x0002644C File Offset: 0x0002464C
		internal static bool EqualsIgnoreCase(string s1, int index1, string s2, int index2, int length)
		{
			return string.Compare(s1, index1, s2, index2, length, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x0002645D File Offset: 0x0002465D
		internal unsafe static string StringFromWCharPtr(IntPtr ip, int length)
		{
			return new string((char*)(void*)ip, 0, length);
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x0002646C File Offset: 0x0002466C
		internal static string StringFromCharPtr(IntPtr ip, int length)
		{
			return Marshal.PtrToStringAnsi(ip, length);
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x00026478 File Offset: 0x00024678
		internal static bool StringEndsWith(string s, char c)
		{
			int length = s.Length;
			return length != 0 && s[length - 1] == c;
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x000264A0 File Offset: 0x000246A0
		internal unsafe static bool StringEndsWith(string s1, string s2)
		{
			int num = s1.Length - s2.Length;
			if (num < 0)
			{
				return false;
			}
			fixed (string text = s1)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				fixed (string text2 = s2)
				{
					char* ptr2 = text2;
					if (ptr2 != null)
					{
						ptr2 += RuntimeHelpers.OffsetToStringData / 2;
					}
					char* ptr3 = ptr + num;
					char* ptr4 = ptr2;
					int length = s2.Length;
					while (length-- > 0)
					{
						if (*(ptr3++) != *(ptr4++))
						{
							return false;
						}
					}
					text = null;
				}
				return true;
			}
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x0002651C File Offset: 0x0002471C
		internal static bool StringEndsWithIgnoreCase(string s1, string s2)
		{
			int num = s1.Length - s2.Length;
			return num >= 0 && string.Compare(s1, num, s2, 0, s2.Length, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x00026550 File Offset: 0x00024750
		internal static bool StringStartsWith(string s, char c)
		{
			return s.Length != 0 && s[0] == c;
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x00026568 File Offset: 0x00024768
		internal unsafe static bool StringStartsWith(string s1, string s2)
		{
			if (s2.Length > s1.Length)
			{
				return false;
			}
			fixed (string text = s1)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				fixed (string text2 = s2)
				{
					char* ptr2 = text2;
					if (ptr2 != null)
					{
						ptr2 += RuntimeHelpers.OffsetToStringData / 2;
					}
					char* ptr3 = ptr;
					char* ptr4 = ptr2;
					int length = s2.Length;
					while (length-- > 0)
					{
						if (*(ptr3++) != *(ptr4++))
						{
							return false;
						}
					}
					text = null;
				}
				return true;
			}
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x000265D8 File Offset: 0x000247D8
		internal static bool StringStartsWithIgnoreCase(string s1, string s2)
		{
			return !string.IsNullOrEmpty(s1) && !string.IsNullOrEmpty(s2) && s2.Length <= s1.Length && string.Compare(s1, 0, s2, 0, s2.Length, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x00026610 File Offset: 0x00024810
		internal static bool StringArrayEquals(string[] a, string[] b)
		{
			if (a == null != (b == null))
			{
				return false;
			}
			if (a == null)
			{
				return true;
			}
			int num = a.Length;
			if (num != b.Length)
			{
				return false;
			}
			for (int i = 0; i < num; i++)
			{
				if (a[i] != b[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x00026658 File Offset: 0x00024858
		internal unsafe static int GetStringHashCode(string s)
		{
			char* ptr = s;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			int num = 352654597;
			int num2 = num;
			int* ptr2 = (int*)ptr;
			for (int i = s.Length; i > 0; i -= 4)
			{
				num = ((num << 5) + num + (num >> 27)) ^ *ptr2;
				if (i <= 2)
				{
					break;
				}
				num2 = ((num2 << 5) + num2 + (num2 >> 27)) ^ ptr2[1];
				ptr2 += 2;
			}
			return num + num2 * 1566083941;
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x000266CC File Offset: 0x000248CC
		internal static int GetNullTerminatedByteArray(Encoding enc, string s, out byte[] bytes)
		{
			bytes = null;
			if (s == null)
			{
				return 0;
			}
			bytes = new byte[enc.GetMaxByteCount(s.Length) + 1];
			return enc.GetBytes(s, 0, s.Length, bytes, 0);
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x000266FC File Offset: 0x000248FC
		internal unsafe static void memcpyimpl(byte* src, byte* dest, int len)
		{
			if (len >= 16)
			{
				do
				{
					*(int*)dest = *(int*)src;
					*(int*)(dest + 4) = *(int*)(src + 4);
					*(int*)(dest + (IntPtr)2 * 4) = *(int*)(src + (IntPtr)2 * 4);
					*(int*)(dest + (IntPtr)3 * 4) = *(int*)(src + (IntPtr)3 * 4);
					dest += 16;
					src += 16;
				}
				while ((len -= 16) >= 16);
			}
			if (len > 0)
			{
				if ((len & 8) != 0)
				{
					*(int*)dest = *(int*)src;
					*(int*)(dest + 4) = *(int*)(src + 4);
					dest += 8;
					src += 8;
				}
				if ((len & 4) != 0)
				{
					*(int*)dest = *(int*)src;
					dest += 4;
					src += 4;
				}
				if ((len & 2) != 0)
				{
					*(short*)dest = *(short*)src;
					dest += 2;
					src += 2;
				}
				if ((len & 1) != 0)
				{
					*(dest++) = *(src++);
				}
			}
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x000267A8 File Offset: 0x000249A8
		internal static string[] ObjectArrayToStringArray(object[] objectArray)
		{
			string[] array = new string[objectArray.Length];
			objectArray.CopyTo(array, 0);
			return array;
		}
	}
}
