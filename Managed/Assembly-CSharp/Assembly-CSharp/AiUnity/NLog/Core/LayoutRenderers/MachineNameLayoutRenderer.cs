using System;
using System.Text;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Common;
using AiUnity.NLog.Core.Config;
using AiUnity.NLog.Core.Internal;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.LayoutRenderers
{
	// Token: 0x02000253 RID: 595
	[LayoutRenderer("machinename", false)]
	[AppDomainFixedOutput]
	[ThreadAgnostic]
	[Preserve]
	public class MachineNameLayoutRenderer : LayoutRenderer
	{
		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060015D4 RID: 5588 RVA: 0x000776D7 File Offset: 0x000758D7
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<NLogInternalLogger>.Instance;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060015D5 RID: 5589 RVA: 0x000776DE File Offset: 0x000758DE
		// (set) Token: 0x060015D6 RID: 5590 RVA: 0x000776E6 File Offset: 0x000758E6
		internal string MachineName { get; private set; }

		// Token: 0x060015D7 RID: 5591 RVA: 0x000776F0 File Offset: 0x000758F0
		protected override void InitializeLayoutRenderer()
		{
			base.InitializeLayoutRenderer();
			try
			{
				this.MachineName = Environment.MachineName;
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrown())
				{
					throw;
				}
				MachineNameLayoutRenderer.Logger.Error("Error getting machine name {0}", new object[] { ex });
				this.MachineName = string.Empty;
			}
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x00077754 File Offset: 0x00075954
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			builder.Append(this.MachineName);
		}
	}
}
