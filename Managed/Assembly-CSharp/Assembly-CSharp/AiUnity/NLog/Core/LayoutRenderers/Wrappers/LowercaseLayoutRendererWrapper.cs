using System;
using System.ComponentModel;
using System.Globalization;
using AiUnity.NLog.Core.Config;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.LayoutRenderers.Wrappers
{
	// Token: 0x0200025A RID: 602
	[LayoutRenderer("lowercase", true)]
	[AmbientProperty("Lowercase")]
	[ThreadAgnostic]
	[Preserve]
	public sealed class LowercaseLayoutRendererWrapper : WrapperLayoutRendererBase
	{
		// Token: 0x060015F5 RID: 5621 RVA: 0x00077B23 File Offset: 0x00075D23
		public LowercaseLayoutRendererWrapper()
		{
			this.Culture = CultureInfo.InvariantCulture;
			this.Lowercase = true;
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060015F6 RID: 5622 RVA: 0x00077B3D File Offset: 0x00075D3D
		// (set) Token: 0x060015F7 RID: 5623 RVA: 0x00077B45 File Offset: 0x00075D45
		[DefaultValue(true)]
		public bool Lowercase { get; set; }

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060015F8 RID: 5624 RVA: 0x00077B4E File Offset: 0x00075D4E
		// (set) Token: 0x060015F9 RID: 5625 RVA: 0x00077B56 File Offset: 0x00075D56
		public CultureInfo Culture { get; set; }

		// Token: 0x060015FA RID: 5626 RVA: 0x00077B5F File Offset: 0x00075D5F
		protected override string Transform(string text)
		{
			if (!this.Lowercase)
			{
				return text;
			}
			return text.ToLower(this.Culture);
		}
	}
}
