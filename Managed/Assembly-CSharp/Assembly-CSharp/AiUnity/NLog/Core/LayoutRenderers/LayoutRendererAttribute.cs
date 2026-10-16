using System;
using System.ComponentModel;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.LayoutRenderers
{
	// Token: 0x0200024D RID: 589
	[AttributeUsage(AttributeTargets.Class)]
	[Preserve]
	public sealed class LayoutRendererAttribute : DisplayNameAttribute
	{
		// Token: 0x060015A9 RID: 5545 RVA: 0x00076E4F File Offset: 0x0007504F
		public LayoutRendererAttribute(string name, bool isWrapper = false)
			: base(name)
		{
			this.IsWrapper = isWrapper;
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060015AA RID: 5546 RVA: 0x00076E5F File Offset: 0x0007505F
		// (set) Token: 0x060015AB RID: 5547 RVA: 0x00076E67 File Offset: 0x00075067
		public bool IsWrapper { get; set; }
	}
}
