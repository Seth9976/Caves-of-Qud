using System;
using System.ComponentModel;
using System.Text;
using AiUnity.NLog.Core.Config;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.LayoutRenderers
{
	// Token: 0x02000256 RID: 598
	[LayoutRenderer("shortdate", false)]
	[ThreadAgnostic]
	[Preserve]
	public class ShortDateLayoutRenderer : LayoutRenderer
	{
		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060015E2 RID: 5602 RVA: 0x000777F4 File Offset: 0x000759F4
		// (set) Token: 0x060015E3 RID: 5603 RVA: 0x000777FC File Offset: 0x000759FC
		[DefaultValue(false)]
		public bool UniversalTime { get; set; }

		// Token: 0x060015E4 RID: 5604 RVA: 0x00077808 File Offset: 0x00075A08
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			DateTime dateTime = logEvent.TimeStamp;
			if (this.UniversalTime)
			{
				dateTime = dateTime.ToUniversalTime();
			}
			builder.Append(dateTime.ToString("yyyy-MM-dd"));
		}
	}
}
