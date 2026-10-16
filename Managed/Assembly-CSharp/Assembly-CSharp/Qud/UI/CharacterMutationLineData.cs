using System;
using XRL.UI.Framework;
using XRL.World.Parts.Mutation;

namespace Qud.UI
{
	// Token: 0x0200045F RID: 1119
	public class CharacterMutationLineData : PooledFrameworkDataElement<CharacterMutationLineData>
	{
		// Token: 0x06002A59 RID: 10841 RVA: 0x000DE565 File Offset: 0x000DC765
		public CharacterMutationLineData set(BaseMutation mutation)
		{
			this.mutation = mutation;
			return this;
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x000DE56F File Offset: 0x000DC76F
		public override void free()
		{
			this.mutation = null;
			base.free();
		}

		// Token: 0x04001635 RID: 5685
		public BaseMutation mutation;
	}
}
