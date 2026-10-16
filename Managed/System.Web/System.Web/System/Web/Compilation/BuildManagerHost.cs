using System;
using System.Web.Hosting;

namespace System.Web.Compilation
{
	// Token: 0x02000638 RID: 1592
	internal class BuildManagerHost : MarshalByRefObject, IRegisteredObject
	{
		// Token: 0x0600448F RID: 17551 RVA: 0x000BA0A4 File Offset: 0x000B82A4
		protected void RegisterAssembly(string assemblyName, string assemblyLocation)
		{
			if (string.IsNullOrEmpty(assemblyName) || string.IsNullOrEmpty(assemblyLocation))
			{
				return;
			}
			HttpRuntime.RegisteredAssemblies.InsertOrUpdate((uint)assemblyName.GetHashCode(), assemblyName, assemblyLocation, assemblyLocation);
			HttpRuntime.EnableAssemblyMapping(true);
		}

		// Token: 0x06004490 RID: 17552 RVA: 0x00003946 File Offset: 0x00001B46
		public void Stop(bool immediate)
		{
		}
	}
}
