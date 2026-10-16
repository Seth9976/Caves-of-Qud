using System;
using Unity;

namespace System.Web.Hosting
{
	/// <summary>Provides a method to retrieve an <see cref="T:System.Web.Hosting.IProcessHost" /> interface.</summary>
	// Token: 0x0200076C RID: 1900
	public sealed class ProcessHostFactoryHelper : MarshalByRefObject, IProcessHostFactoryHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Hosting.ProcessHostFactoryHelper" /> class.</summary>
		// Token: 0x06004D2C RID: 19756 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ProcessHostFactoryHelper()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the process host.</summary>
		/// <returns>An object that contains the process host.</returns>
		/// <param name="functions">Functions that are declared by the <see cref="T:System.Web.Hosting.IProcessHostSupportFunctions" /> interface.</param>
		// Token: 0x06004D2D RID: 19757 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public object GetProcessHost(IProcessHostSupportFunctions functions)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
