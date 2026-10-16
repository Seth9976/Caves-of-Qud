using System;
using AiUnity.NLog.Core.Common;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.Targets
{
	// Token: 0x02000226 RID: 550
	[Target("Null")]
	[Preserve]
	public sealed class NullTarget : Target
	{
		// Token: 0x0600146A RID: 5226 RVA: 0x00073453 File Offset: 0x00071653
		protected override void Write(LogEventInfo logEvent)
		{
		}
	}
}
