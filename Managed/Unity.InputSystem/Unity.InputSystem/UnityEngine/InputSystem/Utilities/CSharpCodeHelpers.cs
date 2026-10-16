using System;
using System.Linq;
using System.Text;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200012D RID: 301
	internal static class CSharpCodeHelpers
	{
		// Token: 0x0600110D RID: 4365 RVA: 0x00051B40 File Offset: 0x0004FD40
		public static bool IsProperIdentifier(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return false;
			}
			if (char.IsDigit(name[0]))
			{
				return false;
			}
			foreach (char c in name)
			{
				if (!char.IsLetterOrDigit(c) && c != '_')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x00051B90 File Offset: 0x0004FD90
		public static bool IsEmptyOrProperIdentifier(string name)
		{
			return string.IsNullOrEmpty(name) || CSharpCodeHelpers.IsProperIdentifier(name);
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x00051BA2 File Offset: 0x0004FDA2
		public static bool IsEmptyOrProperNamespaceName(string name)
		{
			return string.IsNullOrEmpty(name) || name.Split('.', StringSplitOptions.None).All<string>(new Func<string, bool>(CSharpCodeHelpers.IsProperIdentifier));
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x00051BC8 File Offset: 0x0004FDC8
		public static string MakeIdentifier(string name, string suffix = "")
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			if (char.IsDigit(name[0]))
			{
				name = "_" + name;
			}
			bool flag = false;
			foreach (char c in name)
			{
				if (!char.IsLetterOrDigit(c) && c != '_')
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (char c2 in name)
				{
					if (char.IsLetterOrDigit(c2) || c2 == '_')
					{
						stringBuilder.Append(c2);
					}
				}
				name = stringBuilder.ToString();
			}
			return name + suffix;
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x00051C80 File Offset: 0x0004FE80
		public static string MakeTypeName(string name, string suffix = "")
		{
			string text = CSharpCodeHelpers.MakeIdentifier(name, suffix);
			if (char.IsLower(text[0]))
			{
				text = char.ToUpperInvariant(text[0]).ToString() + text.Substring(1);
			}
			return text;
		}
	}
}
