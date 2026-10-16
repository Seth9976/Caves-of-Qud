using System;
using System.Reflection;

namespace System.Web.UI
{
	// Token: 0x0200017B RID: 379
	internal interface IScriptResourceMapping
	{
		// Token: 0x06000F4F RID: 3919
		IScriptResourceDefinition GetDefinition(string resourceName);

		// Token: 0x06000F50 RID: 3920
		IScriptResourceDefinition GetDefinition(string resourceName, Assembly resourceAssembly);
	}
}
