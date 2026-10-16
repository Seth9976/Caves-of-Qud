using System;
using Unity.Properties;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000041 RID: 65
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal readonly struct BindingTypeResult
	{
		// Token: 0x06000240 RID: 576 RVA: 0x0000A743 File Offset: 0x00008943
		internal BindingTypeResult(Type type, in PropertyPath resolvedPath)
		{
			this.type = type;
			this.resolvedPath = resolvedPath;
			this.returnCode = VisitReturnCode.Ok;
			this.errorIndex = -1;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000A767 File Offset: 0x00008967
		internal BindingTypeResult(VisitReturnCode returnCode, int errorIndex, in PropertyPath resolvedPath)
		{
			this.type = null;
			this.resolvedPath = resolvedPath;
			this.returnCode = returnCode;
			this.errorIndex = errorIndex;
		}

		// Token: 0x04000159 RID: 345
		public readonly Type type;

		// Token: 0x0400015A RID: 346
		public readonly VisitReturnCode returnCode;

		// Token: 0x0400015B RID: 347
		public readonly int errorIndex;

		// Token: 0x0400015C RID: 348
		public readonly PropertyPath resolvedPath;
	}
}
