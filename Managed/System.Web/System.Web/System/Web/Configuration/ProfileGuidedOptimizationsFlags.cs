using System;

namespace System.Web.Configuration
{
	/// <summary>Specifies the optimization mode for an application deployment environment.</summary>
	// Token: 0x0200056D RID: 1389
	[Flags]
	public enum ProfileGuidedOptimizationsFlags
	{
		/// <summary>No optimizations are performed based on the deployment environment of the application.</summary>
		// Token: 0x0400203F RID: 8255
		None = 0,
		/// <summary>All optimizations are performed based on the deployment environment of the application.</summary>
		// Token: 0x04002040 RID: 8256
		All = 1
	}
}
