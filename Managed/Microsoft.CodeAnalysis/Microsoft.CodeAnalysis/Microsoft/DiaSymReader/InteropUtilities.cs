using System;
using System.Runtime.InteropServices;

namespace Microsoft.DiaSymReader
{
	// Token: 0x020000F6 RID: 246
	internal class InteropUtilities
	{
		// Token: 0x06000872 RID: 2162 RVA: 0x00018DDC File Offset: 0x00016FDC
		internal static T[] NullToEmpty<T>(T[] items)
		{
			if (items != null)
			{
				return items;
			}
			return EmptyArray<T>.Instance;
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00018DE8 File Offset: 0x00016FE8
		internal static void ThrowExceptionForHR(int hr)
		{
			if (hr < 0 && hr != -2147467259 && hr != -2147467263)
			{
				Marshal.ThrowExceptionForHR(hr, InteropUtilities.s_ignoreIErrorInfo);
			}
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00018E0C File Offset: 0x0001700C
		internal unsafe static void CopyQualifiedTypeName(char* qualifiedName, int qualifiedNameBufferLength, int* qualifiedNameLength, string namespaceStr, string nameStr)
		{
			if (namespaceStr == null)
			{
				namespaceStr = string.Empty;
			}
			if (qualifiedNameLength != null)
			{
				int num = ((namespaceStr.Length > 0) ? (namespaceStr.Length + 1) : 0) + nameStr.Length;
				if (qualifiedName != null)
				{
					*qualifiedNameLength = Math.Min(num, Math.Max(0, qualifiedNameBufferLength - 1));
				}
				else
				{
					*qualifiedNameLength = num;
				}
			}
			if (qualifiedName != null && qualifiedNameBufferLength > 0)
			{
				char* ptr = qualifiedName;
				char* ptr2 = ptr + qualifiedNameBufferLength - 1;
				if (namespaceStr.Length > 0)
				{
					int num2 = 0;
					while (num2 < namespaceStr.Length && ptr < ptr2)
					{
						*ptr = namespaceStr.get_Chars(num2);
						ptr++;
						num2++;
					}
					if (ptr < ptr2)
					{
						*ptr = '.';
						ptr++;
					}
				}
				int num3 = 0;
				while (num3 < nameStr.Length && ptr < ptr2)
				{
					*ptr = nameStr.get_Chars(num3);
					ptr++;
					num3++;
				}
				*ptr = '\0';
			}
		}

		// Token: 0x04000266 RID: 614
		private static readonly IntPtr s_ignoreIErrorInfo = new IntPtr(-1);
	}
}
