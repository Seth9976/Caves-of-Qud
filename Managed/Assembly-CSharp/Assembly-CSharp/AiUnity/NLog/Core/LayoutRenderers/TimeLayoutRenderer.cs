using System;
using System.ComponentModel;
using System.Text;
using AiUnity.NLog.Core.Config;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.LayoutRenderers
{
	// Token: 0x02000259 RID: 601
	[LayoutRenderer("time", false)]
	[ThreadAgnostic]
	[Preserve]
	public class TimeLayoutRenderer : LayoutRenderer
	{
		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060015EF RID: 5615 RVA: 0x00077A0C File Offset: 0x00075C0C
		// (set) Token: 0x060015F0 RID: 5616 RVA: 0x00077A14 File Offset: 0x00075C14
		[DefaultValue(false)]
		public bool UniversalTime { get; set; }

		// Token: 0x060015F1 RID: 5617 RVA: 0x00077A20 File Offset: 0x00075C20
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			DateTime dateTime = logEvent.TimeStamp;
			if (this.UniversalTime)
			{
				dateTime = dateTime.ToUniversalTime();
			}
			TimeLayoutRenderer.Append2DigitsZeroPadded(builder, dateTime.Hour);
			builder.Append(':');
			TimeLayoutRenderer.Append2DigitsZeroPadded(builder, dateTime.Minute);
			builder.Append(':');
			TimeLayoutRenderer.Append2DigitsZeroPadded(builder, dateTime.Second);
			builder.Append('.');
			TimeLayoutRenderer.Append4DigitsZeroPadded(builder, (int)(dateTime.Ticks % 10000000L) / 1000);
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x00077AA1 File Offset: 0x00075CA1
		private static void Append2DigitsZeroPadded(StringBuilder builder, int number)
		{
			builder.Append((char)(number / 10 + 48));
			builder.Append((char)(number % 10 + 48));
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x00077AC4 File Offset: 0x00075CC4
		private static void Append4DigitsZeroPadded(StringBuilder builder, int number)
		{
			builder.Append((char)(number / 1000 % 10 + 48));
			builder.Append((char)(number / 100 % 10 + 48));
			builder.Append((char)(number / 10 % 10 + 48));
			builder.Append((char)(number / 1 % 10 + 48));
		}
	}
}
