using System;
using System.Reflection;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000287 RID: 647
	internal static class ClassLibraryInitializer
	{
		// Token: 0x06002039 RID: 8249 RVA: 0x0003E0F6 File Offset: 0x0003C2F6
		[RequiredByNativeCode]
		private static void Init()
		{
			UnityLogWriter.Init();
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x0003E0FF File Offset: 0x0003C2FF
		[RequiredByNativeCode(Optional = true)]
		private static void InitAssemblyRedirections()
		{
			AppDomain.CurrentDomain.AssemblyResolve += delegate(object _, ResolveEventArgs args)
			{
				AssemblyName assemblyName = new AssemblyName(args.Name);
				Assembly assembly2;
				try
				{
					Assembly assembly = AppDomain.CurrentDomain.Load(assemblyName.Name);
					assembly2 = assembly;
				}
				catch
				{
					assembly2 = null;
				}
				return assembly2;
			};
		}
	}
}
