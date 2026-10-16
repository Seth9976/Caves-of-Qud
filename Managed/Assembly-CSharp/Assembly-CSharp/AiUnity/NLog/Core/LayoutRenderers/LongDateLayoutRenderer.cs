using System;
using System.ComponentModel;
using System.Text;
using AiUnity.NLog.Core.Config;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.LayoutRenderers
{
	// Token: 0x02000252 RID: 594
	[LayoutRenderer("longdate", false)]
	[ThreadAgnostic]
	[Preserve]
	public class LongDateLayoutRenderer : LayoutRenderer
	{
		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060015CE RID: 5582 RVA: 0x00077580 File Offset: 0x00075780
		// (set) Token: 0x060015CF RID: 5583 RVA: 0x00077588 File Offset: 0x00075788
		[DefaultValue(false)]
		public bool UniversalTime { get; set; }

		// Token: 0x060015D0 RID: 5584 RVA: 0x00077594 File Offset: 0x00075794
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			DateTime dateTime = logEvent.TimeStamp;
			if (this.UniversalTime)
			{
				dateTime = dateTime.ToUniversalTime();
			}
			builder.Append(dateTime.Year);
			builder.Append('-');
			LongDateLayoutRenderer.Append2DigitsZeroPadded(builder, dateTime.Month);
			builder.Append('-');
			LongDateLayoutRenderer.Append2DigitsZeroPadded(builder, dateTime.Day);
			builder.Append(' ');
			LongDateLayoutRenderer.Append2DigitsZeroPadded(builder, dateTime.Hour);
			builder.Append(':');
			LongDateLayoutRenderer.Append2DigitsZeroPadded(builder, dateTime.Minute);
			builder.Append(':');
			LongDateLayoutRenderer.Append2DigitsZeroPadded(builder, dateTime.Second);
			builder.Append('.');
			LongDateLayoutRenderer.Append4DigitsZeroPadded(builder, (int)(dateTime.Ticks % 10000000L) / 1000);
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x00077658 File Offset: 0x00075858
		private static void Append2DigitsZeroPadded(StringBuilder builder, int number)
		{
			builder.Append((char)(number / 10 + 48));
			builder.Append((char)(number % 10 + 48));
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x00077678 File Offset: 0x00075878
		private static void Append4DigitsZeroPadded(StringBuilder builder, int number)
		{
			builder.Append((char)(number / 1000 % 10 + 48));
			builder.Append((char)(number / 100 % 10 + 48));
			builder.Append((char)(number / 10 % 10 + 48));
			builder.Append((char)(number / 1 % 10 + 48));
		}
	}
}
