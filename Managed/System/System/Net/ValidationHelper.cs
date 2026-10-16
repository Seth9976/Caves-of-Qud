using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x020005E3 RID: 1507
	internal static class ValidationHelper
	{
		// Token: 0x06003057 RID: 12375 RVA: 0x000A68CE File Offset: 0x000A4ACE
		public static string[] MakeEmptyArrayNull(string[] stringArray)
		{
			if (stringArray == null || stringArray.Length == 0)
			{
				return null;
			}
			return stringArray;
		}

		// Token: 0x06003058 RID: 12376 RVA: 0x000A68DA File Offset: 0x000A4ADA
		public static string MakeStringNull(string stringValue)
		{
			if (stringValue == null || stringValue.Length == 0)
			{
				return null;
			}
			return stringValue;
		}

		// Token: 0x06003059 RID: 12377 RVA: 0x000A68EA File Offset: 0x000A4AEA
		public static string ExceptionMessage(Exception exception)
		{
			if (exception == null)
			{
				return string.Empty;
			}
			if (exception.InnerException == null)
			{
				return exception.Message;
			}
			return exception.Message + " (" + ValidationHelper.ExceptionMessage(exception.InnerException) + ")";
		}

		// Token: 0x0600305A RID: 12378 RVA: 0x000A6924 File Offset: 0x000A4B24
		public static string ToString(object objectValue)
		{
			if (objectValue == null)
			{
				return "(null)";
			}
			if (objectValue is string && ((string)objectValue).Length == 0)
			{
				return "(string.empty)";
			}
			if (objectValue is Exception)
			{
				return ValidationHelper.ExceptionMessage(objectValue as Exception);
			}
			if (objectValue is IntPtr)
			{
				return "0x" + ((IntPtr)objectValue).ToString("x");
			}
			return objectValue.ToString();
		}

		// Token: 0x0600305B RID: 12379 RVA: 0x000A6998 File Offset: 0x000A4B98
		public static string HashString(object objectValue)
		{
			if (objectValue == null)
			{
				return "(null)";
			}
			if (objectValue is string && ((string)objectValue).Length == 0)
			{
				return "(string.empty)";
			}
			return objectValue.GetHashCode().ToString(NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x0600305C RID: 12380 RVA: 0x000A69DC File Offset: 0x000A4BDC
		public static bool IsInvalidHttpString(string stringValue)
		{
			return stringValue.IndexOfAny(ValidationHelper.InvalidParamChars) != -1;
		}

		// Token: 0x0600305D RID: 12381 RVA: 0x000A69EF File Offset: 0x000A4BEF
		public static bool IsBlankString(string stringValue)
		{
			return stringValue == null || stringValue.Length == 0;
		}

		// Token: 0x0600305E RID: 12382 RVA: 0x0009A1A7 File Offset: 0x000983A7
		public static bool ValidateTcpPort(int port)
		{
			return port >= 0 && port <= 65535;
		}

		// Token: 0x0600305F RID: 12383 RVA: 0x00099E34 File Offset: 0x00098034
		public static bool ValidateRange(int actual, int fromAllowed, int toAllowed)
		{
			return actual >= fromAllowed && actual <= toAllowed;
		}

		// Token: 0x06003060 RID: 12384 RVA: 0x000A6A00 File Offset: 0x000A4C00
		internal static void ValidateSegment(ArraySegment<byte> segment)
		{
			if (segment.Array == null)
			{
				throw new ArgumentNullException("segment");
			}
			if (segment.Offset < 0 || segment.Count < 0 || segment.Count > segment.Array.Length - segment.Offset)
			{
				throw new ArgumentOutOfRangeException("segment");
			}
		}

		// Token: 0x04001B30 RID: 6960
		public static string[] EmptyArray = new string[0];

		// Token: 0x04001B31 RID: 6961
		internal static readonly char[] InvalidMethodChars = new char[] { ' ', '\r', '\n', '\t' };

		// Token: 0x04001B32 RID: 6962
		internal static readonly char[] InvalidParamChars = new char[]
		{
			'(', ')', '<', '>', '@', ',', ';', ':', '\\', '"',
			'\'', '/', '[', ']', '?', '=', '{', '}', ' ', '\t',
			'\r', '\n'
		};
	}
}
