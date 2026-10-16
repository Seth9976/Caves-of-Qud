using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using AiUnity.NLog.Core.Config;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.LayoutRenderers
{
	// Token: 0x02000246 RID: 582
	[LayoutRenderer("date", false)]
	[ThreadAgnostic]
	[Preserve]
	public class DateLayoutRenderer : LayoutRenderer
	{
		// Token: 0x06001562 RID: 5474 RVA: 0x00076358 File Offset: 0x00074558
		public DateLayoutRenderer()
		{
			this.Format = "yyyy/MM/dd HH:mm:ss.fff";
			this.Culture = CultureInfo.InvariantCulture;
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06001563 RID: 5475 RVA: 0x00076376 File Offset: 0x00074576
		// (set) Token: 0x06001564 RID: 5476 RVA: 0x0007637E File Offset: 0x0007457E
		public CultureInfo Culture { get; set; }

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06001565 RID: 5477 RVA: 0x00076387 File Offset: 0x00074587
		// (set) Token: 0x06001566 RID: 5478 RVA: 0x0007638F File Offset: 0x0007458F
		[DefaultParameter]
		public string Format { get; set; }

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06001567 RID: 5479 RVA: 0x00076398 File Offset: 0x00074598
		// (set) Token: 0x06001568 RID: 5480 RVA: 0x000763A0 File Offset: 0x000745A0
		[DefaultValue(false)]
		public bool UniversalTime { get; set; }

		// Token: 0x06001569 RID: 5481 RVA: 0x000763AC File Offset: 0x000745AC
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			DateTime dateTime = logEvent.TimeStamp;
			if (this.UniversalTime)
			{
				dateTime = dateTime.ToUniversalTime();
			}
			builder.Append(dateTime.ToString(this.Format, this.Culture));
		}
	}
}
