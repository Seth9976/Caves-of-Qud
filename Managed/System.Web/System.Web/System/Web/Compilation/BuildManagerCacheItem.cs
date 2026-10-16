using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Text;

namespace System.Web.Compilation
{
	// Token: 0x02000635 RID: 1589
	internal sealed class BuildManagerCacheItem
	{
		// Token: 0x06004479 RID: 17529 RVA: 0x000B9723 File Offset: 0x000B7923
		public BuildManagerCacheItem(Assembly assembly, BuildProvider bp, CompilerResults results)
		{
			this.BuiltAssembly = assembly;
			this.CompiledCustomString = bp.GetCustomString(results);
			this.VirtualPath = bp.VirtualPath;
			this.Type = bp.GetGeneratedType(results);
		}

		// Token: 0x0600447A RID: 17530 RVA: 0x000B9758 File Offset: 0x000B7958
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("BuildCacheItem [");
			bool flag = true;
			if (!string.IsNullOrEmpty(this.CompiledCustomString))
			{
				stringBuilder.Append("compiledCustomString: " + this.CompiledCustomString);
				flag = false;
			}
			if (this.BuiltAssembly != null)
			{
				stringBuilder.Append((flag ? string.Empty : "; ") + "assembly: " + this.BuiltAssembly.ToString());
				flag = false;
			}
			if (!string.IsNullOrEmpty(this.VirtualPath))
			{
				stringBuilder.Append((flag ? string.Empty : "; ") + "virtualPath: " + this.VirtualPath);
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x040024A3 RID: 9379
		public readonly string CompiledCustomString;

		// Token: 0x040024A4 RID: 9380
		public readonly Assembly BuiltAssembly;

		// Token: 0x040024A5 RID: 9381
		public readonly string VirtualPath;

		// Token: 0x040024A6 RID: 9382
		public readonly Type Type;
	}
}
