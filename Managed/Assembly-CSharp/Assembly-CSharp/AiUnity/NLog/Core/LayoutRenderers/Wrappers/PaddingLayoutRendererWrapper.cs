using System;
using System.ComponentModel;
using AiUnity.NLog.Core.Config;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.LayoutRenderers.Wrappers
{
	// Token: 0x0200025B RID: 603
	[LayoutRenderer("pad", true)]
	[AmbientProperty("Padding")]
	[AmbientProperty("PadCharacter")]
	[AmbientProperty("FixedLength")]
	[ThreadAgnostic]
	[Preserve]
	public sealed class PaddingLayoutRendererWrapper : WrapperLayoutRendererBase
	{
		// Token: 0x060015FB RID: 5627 RVA: 0x00077B77 File Offset: 0x00075D77
		public PaddingLayoutRendererWrapper()
		{
			this.PadCharacter = ' ';
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060015FC RID: 5628 RVA: 0x00077B87 File Offset: 0x00075D87
		// (set) Token: 0x060015FD RID: 5629 RVA: 0x00077B8F File Offset: 0x00075D8F
		public int Padding { get; set; }

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060015FE RID: 5630 RVA: 0x00077B98 File Offset: 0x00075D98
		// (set) Token: 0x060015FF RID: 5631 RVA: 0x00077BA0 File Offset: 0x00075DA0
		[DefaultValue(' ')]
		public char PadCharacter { get; set; }

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06001600 RID: 5632 RVA: 0x00077BA9 File Offset: 0x00075DA9
		// (set) Token: 0x06001601 RID: 5633 RVA: 0x00077BB1 File Offset: 0x00075DB1
		[DefaultValue(false)]
		public bool FixedLength { get; set; }

		// Token: 0x06001602 RID: 5634 RVA: 0x00077BBC File Offset: 0x00075DBC
		protected override string Transform(string text)
		{
			string text2 = text ?? string.Empty;
			if (this.Padding != 0)
			{
				if (this.Padding > 0)
				{
					text2 = text2.PadLeft(this.Padding, this.PadCharacter);
				}
				else
				{
					text2 = text2.PadRight(-this.Padding, this.PadCharacter);
				}
				int num = this.Padding;
				if (num < 0)
				{
					num = -num;
				}
				if (this.FixedLength && text2.Length > num)
				{
					text2 = text2.Substring(0, num);
				}
			}
			return text2;
		}
	}
}
