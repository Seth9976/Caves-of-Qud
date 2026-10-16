using System;

namespace NUnit.Framework
{
	// Token: 0x02000052 RID: 82
	internal static class Guard
	{
		// Token: 0x0600028B RID: 651 RVA: 0x00006851 File Offset: 0x00004A51
		public static void ArgumentNotNull(object value, string name)
		{
			if (value == null)
			{
				throw new ArgumentNullException("Argument " + name + " must not be null", name);
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000686D File Offset: 0x00004A6D
		public static void ArgumentNotNullOrEmpty(string value, string name)
		{
			Guard.ArgumentNotNull(value, name);
			if (value == string.Empty)
			{
				throw new ArgumentException("Argument " + name + " must not be the empty string", name);
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000689A File Offset: 0x00004A9A
		public static void ArgumentInRange(bool condition, string message, string paramName)
		{
			if (!condition)
			{
				throw new ArgumentOutOfRangeException(paramName, message);
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x000068A7 File Offset: 0x00004AA7
		public static void ArgumentValid(bool condition, string message, string paramName)
		{
			if (!condition)
			{
				throw new ArgumentException(message, paramName);
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x000068B4 File Offset: 0x00004AB4
		public static void OperationValid(bool condition, string message)
		{
			if (!condition)
			{
				throw new InvalidOperationException(message);
			}
		}
	}
}
