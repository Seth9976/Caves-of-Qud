using System;
using System.IO;

namespace System.ComponentModel
{
	/// <summary>Provides methods to verify the machine name and path conform to a specific syntax. This class cannot be inherited.</summary>
	// Token: 0x020003F1 RID: 1009
	public static class SyntaxCheck
	{
		/// <summary>Checks the syntax of the machine name to confirm that it does not contain "\".</summary>
		/// <returns>true if <paramref name="value" /> matches the proper machine name format; otherwise, false.</returns>
		/// <param name="value">A string containing the machine name to check. </param>
		// Token: 0x060020F2 RID: 8434 RVA: 0x000718F8 File Offset: 0x0006FAF8
		public static bool CheckMachineName(string value)
		{
			if (value == null)
			{
				return false;
			}
			value = value.Trim();
			return !value.Equals(string.Empty) && value.IndexOf('\\') == -1;
		}

		/// <summary>Checks the syntax of the path to see whether it starts with "\\".</summary>
		/// <returns>true if <paramref name="value" /> matches the proper path format; otherwise, false.</returns>
		/// <param name="value">A string containing the path to check. </param>
		// Token: 0x060020F3 RID: 8435 RVA: 0x00071921 File Offset: 0x0006FB21
		public static bool CheckPath(string value)
		{
			if (value == null)
			{
				return false;
			}
			value = value.Trim();
			return !value.Equals(string.Empty) && value.StartsWith("\\\\");
		}

		/// <summary>Checks the syntax of the path to see if it starts with "\" or drive letter "C:".</summary>
		/// <returns>true if <paramref name="value" /> matches the proper path format; otherwise, false.</returns>
		/// <param name="value">A string containing the path to check. </param>
		// Token: 0x060020F4 RID: 8436 RVA: 0x0007194A File Offset: 0x0006FB4A
		public static bool CheckRootedPath(string value)
		{
			if (value == null)
			{
				return false;
			}
			value = value.Trim();
			return !value.Equals(string.Empty) && Path.IsPathRooted(value);
		}
	}
}
