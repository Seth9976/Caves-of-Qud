using System;
using UnityEngine.Bindings;

namespace UnityEngine.Scripting.APIUpdating
{
	// Token: 0x02000379 RID: 889
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal struct MovedFromAttributeData
	{
		// Token: 0x060028E2 RID: 10466 RVA: 0x0004EDF4 File Offset: 0x0004CFF4
		public void Set(bool autoUpdateAPI, string sourceNamespace = null, string sourceAssembly = null, string sourceClassName = null)
		{
			this.className = sourceClassName;
			this.classHasChanged = this.className != null;
			this.nameSpace = sourceNamespace;
			this.nameSpaceHasChanged = this.nameSpace != null;
			this.assembly = sourceAssembly;
			this.assemblyHasChanged = this.assembly != null;
			this.autoUdpateAPI = autoUpdateAPI;
		}

		// Token: 0x04000B96 RID: 2966
		public string className;

		// Token: 0x04000B97 RID: 2967
		public string nameSpace;

		// Token: 0x04000B98 RID: 2968
		public string assembly;

		// Token: 0x04000B99 RID: 2969
		public bool classHasChanged;

		// Token: 0x04000B9A RID: 2970
		public bool nameSpaceHasChanged;

		// Token: 0x04000B9B RID: 2971
		public bool assemblyHasChanged;

		// Token: 0x04000B9C RID: 2972
		public bool autoUdpateAPI;
	}
}
