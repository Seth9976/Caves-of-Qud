using System;
using Unity.Properties;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000043 RID: 67
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal readonly struct PropertyPathInfo
	{
		// Token: 0x06000248 RID: 584 RVA: 0x0000A81A File Offset: 0x00008A1A
		internal PropertyPathInfo(in PropertyPath propertyPath, Type type)
		{
			this.propertyPath = propertyPath;
			this.type = type;
		}

		// Token: 0x04000160 RID: 352
		public readonly PropertyPath propertyPath;

		// Token: 0x04000161 RID: 353
		public readonly Type type;
	}
}
