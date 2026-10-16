using System;
using System.Text;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Common;
using AiUnity.NLog.Core.Config;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.LayoutRenderers
{
	// Token: 0x0200024B RID: 587
	[LayoutRenderer("instancename", false)]
	[AppDomainFixedOutput]
	[ThreadAgnostic]
	[Preserve]
	public class InstanceNameLayoutRenderer : LayoutRenderer
	{
		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06001593 RID: 5523 RVA: 0x00076C78 File Offset: 0x00074E78
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<NLogInternalLogger>.Instance;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06001594 RID: 5524 RVA: 0x00076C7F File Offset: 0x00074E7F
		// (set) Token: 0x06001595 RID: 5525 RVA: 0x00076C86 File Offset: 0x00074E86
		private static string InstanceName { get; set; }

		// Token: 0x06001596 RID: 5526 RVA: 0x00076C8E File Offset: 0x00074E8E
		protected override void InitializeLayoutRenderer()
		{
			base.InitializeLayoutRenderer();
			if (InstanceNameLayoutRenderer.InstanceName == null)
			{
				InstanceNameLayoutRenderer.InstanceName = "unset";
			}
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x00076CA7 File Offset: 0x00074EA7
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			builder.Append(InstanceNameLayoutRenderer.InstanceName);
		}
	}
}
