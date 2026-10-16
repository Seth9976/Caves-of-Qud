using System;
using System.Reflection;
using System.Threading;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000168 RID: 360
	internal class DefaultAnalyzerAssemblyLoader : AnalyzerAssemblyLoader
	{
		// Token: 0x06000E82 RID: 3714 RVA: 0x0002C25F File Offset: 0x0002A45F
		protected override Assembly LoadFromPathImpl(string fullPath)
		{
			if (Interlocked.CompareExchange(ref this._hookedAssemblyResolve, 0, 1) == 0)
			{
				AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(this.CurrentDomain_AssemblyResolve);
			}
			return this.LoadImpl(fullPath);
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x0002C28D File Offset: 0x0002A48D
		protected virtual Assembly LoadImpl(string fullPath)
		{
			return Assembly.LoadFrom(fullPath);
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x0002C298 File Offset: 0x0002A498
		private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			Assembly assembly;
			try
			{
				assembly = base.Load(AppDomain.CurrentDomain.ApplyPolicy(args.Name));
			}
			catch
			{
				assembly = null;
			}
			return assembly;
		}

		// Token: 0x0400041E RID: 1054
		private int _hookedAssemblyResolve;
	}
}
