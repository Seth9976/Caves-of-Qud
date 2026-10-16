using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.LayoutRenderers
{
	// Token: 0x02000245 RID: 581
	[LayoutRenderer("counter", false)]
	[Preserve]
	public class CounterLayoutRenderer : LayoutRenderer
	{
		// Token: 0x06001558 RID: 5464 RVA: 0x00076247 File Offset: 0x00074447
		public CounterLayoutRenderer()
		{
			this.Increment = 1;
			this.Value = 1;
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06001559 RID: 5465 RVA: 0x0007625D File Offset: 0x0007445D
		// (set) Token: 0x0600155A RID: 5466 RVA: 0x00076265 File Offset: 0x00074465
		[DefaultValue(1)]
		public int Value { get; set; }

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x0600155B RID: 5467 RVA: 0x0007626E File Offset: 0x0007446E
		// (set) Token: 0x0600155C RID: 5468 RVA: 0x00076276 File Offset: 0x00074476
		[DefaultValue(1)]
		public int Increment { get; set; }

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x0600155D RID: 5469 RVA: 0x0007627F File Offset: 0x0007447F
		// (set) Token: 0x0600155E RID: 5470 RVA: 0x00076287 File Offset: 0x00074487
		public string Sequence { get; set; }

		// Token: 0x0600155F RID: 5471 RVA: 0x00076290 File Offset: 0x00074490
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			int num;
			if (this.Sequence != null)
			{
				num = CounterLayoutRenderer.GetNextSequenceValue(this.Sequence, this.Value, this.Increment);
			}
			else
			{
				num = this.Value;
				this.Value += this.Increment;
			}
			builder.Append(num.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x000762EC File Offset: 0x000744EC
		private static int GetNextSequenceValue(string sequenceName, int defaultValue, int increment)
		{
			Dictionary<string, int> dictionary = CounterLayoutRenderer.sequences;
			int num3;
			lock (dictionary)
			{
				int num;
				if (!CounterLayoutRenderer.sequences.TryGetValue(sequenceName, out num))
				{
					num = defaultValue;
				}
				int num2 = num;
				num += increment;
				CounterLayoutRenderer.sequences[sequenceName] = num;
				num3 = num2;
			}
			return num3;
		}

		// Token: 0x04000D8B RID: 3467
		private static Dictionary<string, int> sequences = new Dictionary<string, int>();
	}
}
