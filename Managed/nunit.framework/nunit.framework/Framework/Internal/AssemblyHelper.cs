using System;
using System.IO;
using System.Reflection;

namespace NUnit.Framework.Internal
{
	// Token: 0x02000061 RID: 97
	public static class AssemblyHelper
	{
		// Token: 0x0600034B RID: 843 RVA: 0x00007544 File Offset: 0x00005744
		public static string GetAssemblyPath(Assembly assembly)
		{
			string codeBase = assembly.CodeBase;
			if (AssemblyHelper.IsFileUri(codeBase))
			{
				return AssemblyHelper.GetAssemblyPathFromCodeBase(codeBase);
			}
			return assembly.Location;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000756D File Offset: 0x0000576D
		public static string GetDirectoryName(Assembly assembly)
		{
			return Path.GetDirectoryName(AssemblyHelper.GetAssemblyPath(assembly));
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000757A File Offset: 0x0000577A
		public static AssemblyName GetAssemblyName(Assembly assembly)
		{
			return assembly.GetName();
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00007584 File Offset: 0x00005784
		public static Assembly Load(string nameOrPath)
		{
			string text = Path.GetExtension(nameOrPath).ToLower();
			if (text == ".dll" || text == ".exe")
			{
				return Assembly.Load(AssemblyName.GetAssemblyName(nameOrPath));
			}
			return Assembly.Load(nameOrPath);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x000075C9 File Offset: 0x000057C9
		private static bool IsFileUri(string uri)
		{
			return uri.ToLower().StartsWith(Uri.UriSchemeFile);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x000075DC File Offset: 0x000057DC
		public static string GetAssemblyPathFromCodeBase(string codeBase)
		{
			int num = Uri.UriSchemeFile.Length + Uri.SchemeDelimiter.Length;
			if (codeBase[num] == '/')
			{
				if (codeBase[num + 2] == ':')
				{
					num++;
				}
			}
			else if (codeBase[num + 1] != ':')
			{
				num -= 2;
			}
			return codeBase.Substring(num);
		}
	}
}
