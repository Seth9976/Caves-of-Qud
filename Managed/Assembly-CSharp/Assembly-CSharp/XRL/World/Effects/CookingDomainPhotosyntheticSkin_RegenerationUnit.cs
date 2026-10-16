using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000CD7 RID: 3287
	[Serializable]
	public class CookingDomainPhotosyntheticSkin_RegenerationUnit : ProceduralCookingEffectUnit
	{
		// Token: 0x060082A5 RID: 33445 RVA: 0x002F80A8 File Offset: 0x002F62A8
		public override void Init(GameObject target)
		{
			PhotosyntheticSkin part = target.GetPart<PhotosyntheticSkin>();
			if (part != null)
			{
				this.Tier = part.Level;
			}
		}

		// Token: 0x060082A6 RID: 33446 RVA: 0x002F80CB File Offset: 0x002F62CB
		public override string GetDescription()
		{
			return (20 + this.Tier * 10).Signed(false) + "% to natural healing rate";
		}

		// Token: 0x060082A7 RID: 33447 RVA: 0x002F80E9 File Offset: 0x002F62E9
		public override string GetTemplatedDescription()
		{
			return "+20% + (Photosynthetic Skin level * 10)% to natural healing rate";
		}

		// Token: 0x060082A8 RID: 33448 RVA: 0x002F80F0 File Offset: 0x002F62F0
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.RegisterEffectEvent(parent, "Regenerating2");
		}

		// Token: 0x060082A9 RID: 33449 RVA: 0x002F80FE File Offset: 0x002F62FE
		public override void Remove(GameObject Object, Effect parent)
		{
			Object.UnregisterEffectEvent(parent, "Regenerating2");
		}

		// Token: 0x060082AA RID: 33450 RVA: 0x002F810C File Offset: 0x002F630C
		public override void FireEvent(Event E)
		{
			if (E.ID == "Regenerating2")
			{
				float num = 1f + (float)(20 + this.Tier * 10) * 0.01f;
				int num2 = (int)Math.Ceiling((double)((float)E.GetIntParameter("Amount", 0) * num));
				E.SetParameter("Amount", num2);
				return;
			}
		}

		// Token: 0x0400393D RID: 14653
		public int Tier = 1;
	}
}
