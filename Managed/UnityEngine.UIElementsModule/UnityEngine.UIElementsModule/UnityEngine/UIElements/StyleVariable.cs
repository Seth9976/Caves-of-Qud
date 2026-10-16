using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020004DA RID: 1242
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal struct StyleVariable
	{
		// Token: 0x060025EF RID: 9711 RVA: 0x000898A9 File Offset: 0x00087AA9
		public StyleVariable(string name, StyleSheet sheet, StyleValueHandle[] handles)
		{
			this.name = name;
			this.sheet = sheet;
			this.handles = handles;
		}

		// Token: 0x060025F0 RID: 9712 RVA: 0x000898C4 File Offset: 0x00087AC4
		public override int GetHashCode()
		{
			int num = this.name.GetHashCode();
			num = (num * 397) ^ this.sheet.GetHashCode();
			return (num * 397) ^ this.handles.GetHashCode();
		}

		// Token: 0x04001116 RID: 4374
		public readonly string name;

		// Token: 0x04001117 RID: 4375
		public readonly StyleSheet sheet;

		// Token: 0x04001118 RID: 4376
		public readonly StyleValueHandle[] handles;
	}
}
