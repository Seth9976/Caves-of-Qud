using System;
using XRL.UI.Framework;
using XRL.World;

namespace Qud.UI
{
	// Token: 0x0200045D RID: 1117
	public class CharacterEffectLineData : PooledFrameworkDataElement<CharacterEffectLineData>
	{
		// Token: 0x06002A4F RID: 10831 RVA: 0x000DE33D File Offset: 0x000DC53D
		public CharacterEffectLineData set(Effect effect)
		{
			this.effect = effect;
			return this;
		}

		// Token: 0x06002A50 RID: 10832 RVA: 0x000DE347 File Offset: 0x000DC547
		public override void free()
		{
			this.effect = null;
			base.free();
		}

		// Token: 0x0400162F RID: 5679
		public Effect effect;
	}
}
