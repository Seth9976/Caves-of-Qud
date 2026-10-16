using System;

namespace System.Web.Compilation
{
	/// <summary>Represents the method that handles the <see cref="E:System.Web.Compilation.ClientBuildManager.AppDomainUnloaded" /> event and the <see cref="E:System.Web.Compilation.ClientBuildManager.AppDomainShutdown" /> event of a <see cref="T:System.Web.Compilation.ClientBuildManager" /> object.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.Web.Compilation.BuildManagerHostUnloadEventArgs" /> that contains the event data.</param>
	// Token: 0x0200063A RID: 1594
	// (Invoke) Token: 0x06004495 RID: 17557
	public delegate void BuildManagerHostUnloadEventHandler(object sender, BuildManagerHostUnloadEventArgs e);
}
