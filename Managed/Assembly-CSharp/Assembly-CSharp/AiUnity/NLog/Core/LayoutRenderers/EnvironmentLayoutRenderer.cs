using System;
using System.Text;
using AiUnity.Common.Attributes;
using AiUnity.NLog.Core.Config;
using AiUnity.NLog.Core.Layouts;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.LayoutRenderers
{
	// Token: 0x02000247 RID: 583
	[LayoutRenderer("environment", false)]
	[Preserve]
	public class EnvironmentLayoutRenderer : LayoutRenderer
	{
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x0600156A RID: 5482 RVA: 0x000763EA File Offset: 0x000745EA
		// (set) Token: 0x0600156B RID: 5483 RVA: 0x000763F2 File Offset: 0x000745F2
		[RequiredParameter]
		[DefaultParameter]
		public string Variable { get; set; }

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x0600156C RID: 5484 RVA: 0x000763FB File Offset: 0x000745FB
		// (set) Token: 0x0600156D RID: 5485 RVA: 0x00076403 File Offset: 0x00074603
		public string Default { get; set; }

		// Token: 0x0600156E RID: 5486 RVA: 0x0007640C File Offset: 0x0007460C
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			if (this.Variable != null)
			{
				string environmentVariable = Environment.GetEnvironmentVariable(this.Variable);
				if (!string.IsNullOrEmpty(environmentVariable))
				{
					SimpleLayout simpleLayout = new SimpleLayout(environmentVariable);
					builder.Append(simpleLayout.Render(logEvent));
					return;
				}
				if (this.Default != null)
				{
					SimpleLayout simpleLayout2 = new SimpleLayout(this.Default);
					builder.Append(simpleLayout2.Render(logEvent));
				}
			}
		}
	}
}
