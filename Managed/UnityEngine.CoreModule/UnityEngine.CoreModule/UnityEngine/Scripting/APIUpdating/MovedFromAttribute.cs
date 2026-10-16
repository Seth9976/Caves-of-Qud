using System;
using UnityEngine.Bindings;

namespace UnityEngine.Scripting.APIUpdating
{
	// Token: 0x0200037A RID: 890
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate)]
	public class MovedFromAttribute : Attribute
	{
		// Token: 0x060028E3 RID: 10467 RVA: 0x0004EE4C File Offset: 0x0004D04C
		public MovedFromAttribute(bool autoUpdateAPI, string sourceNamespace = null, string sourceAssembly = null, string sourceClassName = null)
		{
			this.data.Set(autoUpdateAPI, sourceNamespace, sourceAssembly, sourceClassName);
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x0004EE67 File Offset: 0x0004D067
		public MovedFromAttribute(string sourceNamespace)
		{
			this.data.Set(true, sourceNamespace, null, null);
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x060028E5 RID: 10469 RVA: 0x0004EE84 File Offset: 0x0004D084
		internal bool AffectsAPIUpdater
		{
			get
			{
				return !this.data.classHasChanged && !this.data.assemblyHasChanged;
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x060028E6 RID: 10470 RVA: 0x0004EEB4 File Offset: 0x0004D0B4
		public bool IsInDifferentAssembly
		{
			get
			{
				return this.data.assemblyHasChanged;
			}
		}

		// Token: 0x04000B9D RID: 2973
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal MovedFromAttributeData data;
	}
}
