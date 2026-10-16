using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000CD8 RID: 3288
	[Serializable]
	public class CookingDomainPhotosyntheticSkin_UnitQuickness : ProceduralCookingEffectUnit
	{
		// Token: 0x060082AC RID: 33452 RVA: 0x002F817C File Offset: 0x002F637C
		public override void Init(GameObject target)
		{
			PhotosyntheticSkin part = target.GetPart<PhotosyntheticSkin>();
			if (part != null)
			{
				this.Tier = part.Level;
			}
		}

		// Token: 0x060082AD RID: 33453 RVA: 0x002F819F File Offset: 0x002F639F
		public override string GetDescription()
		{
			return (13 + this.Tier * 2).Signed(false) + " Quickness";
		}

		// Token: 0x060082AE RID: 33454 RVA: 0x002F81BC File Offset: 0x002F63BC
		public override string GetTemplatedDescription()
		{
			return "+13 + (Photosynthetic Skin level * 2) Quickness";
		}

		// Token: 0x060082AF RID: 33455 RVA: 0x002F81C3 File Offset: 0x002F63C3
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.Statistics["Speed"].Bonus += 13 + this.Tier * 2;
		}

		// Token: 0x060082B0 RID: 33456 RVA: 0x002F81EC File Offset: 0x002F63EC
		public override void Remove(GameObject Object, Effect parent)
		{
			Object.Statistics["Speed"].Bonus -= 13 + this.Tier * 2;
		}

		// Token: 0x0400393E RID: 14654
		public int Tier = 1;
	}
}
