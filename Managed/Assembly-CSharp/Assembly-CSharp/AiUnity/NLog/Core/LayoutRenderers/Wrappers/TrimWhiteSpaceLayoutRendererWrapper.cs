using System;
using System.ComponentModel;
using AiUnity.NLog.Core.Config;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.LayoutRenderers.Wrappers
{
	// Token: 0x0200025D RID: 605
	[LayoutRenderer("trim-whitespace", true)]
	[AmbientProperty("TrimWhiteSpace")]
	[ThreadAgnostic]
	[Preserve]
	public sealed class TrimWhiteSpaceLayoutRendererWrapper : WrapperLayoutRendererBase
	{
		// Token: 0x06001613 RID: 5651 RVA: 0x00077E54 File Offset: 0x00076054
		public TrimWhiteSpaceLayoutRendererWrapper()
		{
			this.TrimWhiteSpace = true;
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06001614 RID: 5652 RVA: 0x00077E63 File Offset: 0x00076063
		// (set) Token: 0x06001615 RID: 5653 RVA: 0x00077E6B File Offset: 0x0007606B
		[DefaultValue(true)]
		public bool TrimWhiteSpace { get; set; }

		// Token: 0x06001616 RID: 5654 RVA: 0x00077E74 File Offset: 0x00076074
		protected override string Transform(string text)
		{
			if (!this.TrimWhiteSpace)
			{
				return text;
			}
			return text.Trim();
		}
	}
}
