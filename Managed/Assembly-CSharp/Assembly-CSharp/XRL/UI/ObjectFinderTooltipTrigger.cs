using System;
using ModelShark;

namespace XRL.UI
{
	// Token: 0x0200147D RID: 5245
	public class ObjectFinderTooltipTrigger : TooltipTrigger
	{
		// Token: 0x0600CE8D RID: 52877 RVA: 0x004B2229 File Offset: 0x004B0429
		public override void StartHover()
		{
			this.SetupAndGo();
		}

		// Token: 0x0600CE8E RID: 52878 RVA: 0x004B2234 File Offset: 0x004B0434
		private async void SetupAndGo()
		{
			await Look.SetupItemTooltipAsync(this.line.lastData.go, this);
			base.StartHover();
		}

		// Token: 0x04005026 RID: 20518
		public ObjectFinderLine line;
	}
}
