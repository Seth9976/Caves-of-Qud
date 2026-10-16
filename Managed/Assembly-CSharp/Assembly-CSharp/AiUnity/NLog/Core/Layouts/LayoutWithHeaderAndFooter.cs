using System;
using AiUnity.NLog.Core.Config;

namespace AiUnity.NLog.Core.Layouts
{
	// Token: 0x02000240 RID: 576
	[Layout("LayoutWithHeaderAndFooter")]
	[ThreadAgnostic]
	public class LayoutWithHeaderAndFooter : Layout
	{
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x0600152C RID: 5420 RVA: 0x00075D0A File Offset: 0x00073F0A
		// (set) Token: 0x0600152D RID: 5421 RVA: 0x00075D12 File Offset: 0x00073F12
		public Layout Layout { get; set; }

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x00075D1B File Offset: 0x00073F1B
		// (set) Token: 0x0600152F RID: 5423 RVA: 0x00075D23 File Offset: 0x00073F23
		public Layout Header { get; set; }

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x00075D2C File Offset: 0x00073F2C
		// (set) Token: 0x06001531 RID: 5425 RVA: 0x00075D34 File Offset: 0x00073F34
		public Layout Footer { get; set; }

		// Token: 0x06001532 RID: 5426 RVA: 0x00075D3D File Offset: 0x00073F3D
		protected override string GetFormattedMessage(LogEventInfo logEvent)
		{
			return this.Layout.Render(logEvent);
		}
	}
}
