using System;
using System.Reflection;

namespace System.Web.Util
{
	// Token: 0x0200010B RID: 267
	internal static class AssemblyUtil
	{
		// Token: 0x06000DA4 RID: 3492 RVA: 0x000251C4 File Offset: 0x000233C4
		public static string GetAssemblyFileVersion(Assembly assembly)
		{
			AssemblyFileVersionAttribute[] array = (AssemblyFileVersionAttribute[])assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
			string text;
			if (array.Length != 0)
			{
				text = array[0].Version;
				if (string.IsNullOrEmpty(text))
				{
					text = "0.0.0.0";
				}
			}
			else
			{
				text = "0.0.0.0";
			}
			return text;
		}

		// Token: 0x0400118D RID: 4493
		private const string _emptyFileVersion = "0.0.0.0";
	}
}
