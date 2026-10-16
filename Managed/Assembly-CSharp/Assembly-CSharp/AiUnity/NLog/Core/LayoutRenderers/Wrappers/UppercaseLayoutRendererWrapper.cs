using System;
using System.ComponentModel;
using System.Globalization;
using AiUnity.NLog.Core.Config;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.LayoutRenderers.Wrappers
{
	// Token: 0x0200025E RID: 606
	[LayoutRenderer("uppercase", true)]
	[AmbientProperty("Uppercase")]
	[ThreadAgnostic]
	[Preserve]
	public sealed class UppercaseLayoutRendererWrapper : WrapperLayoutRendererBase
	{
		// Token: 0x06001617 RID: 5655 RVA: 0x00077E86 File Offset: 0x00076086
		public UppercaseLayoutRendererWrapper()
		{
			this.Culture = CultureInfo.InvariantCulture;
			this.Uppercase = true;
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06001618 RID: 5656 RVA: 0x00077EA0 File Offset: 0x000760A0
		// (set) Token: 0x06001619 RID: 5657 RVA: 0x00077EA8 File Offset: 0x000760A8
		[DefaultValue(true)]
		public bool Uppercase { get; set; }

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x0600161A RID: 5658 RVA: 0x00077EB1 File Offset: 0x000760B1
		// (set) Token: 0x0600161B RID: 5659 RVA: 0x00077EB9 File Offset: 0x000760B9
		public CultureInfo Culture { get; set; }

		// Token: 0x0600161C RID: 5660 RVA: 0x00077EC2 File Offset: 0x000760C2
		protected override string Transform(string text)
		{
			if (!this.Uppercase)
			{
				return text;
			}
			return text.ToUpper(this.Culture);
		}
	}
}
