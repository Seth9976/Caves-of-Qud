using System;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000CD9 RID: 3289
	[Serializable]
	public class CookingDomainPhotosyntheticSkin_SatedUnit : ProceduralCookingEffectUnit
	{
		// Token: 0x060082B2 RID: 33458 RVA: 0x002F8224 File Offset: 0x002F6424
		public override void Init(GameObject target)
		{
			PhotosyntheticSkin part = target.GetPart<PhotosyntheticSkin>();
			if (part != null)
			{
				this.Tier = part.Level;
			}
		}

		// Token: 0x060082B3 RID: 33459 RVA: 0x002F8247 File Offset: 0x002F6447
		public override string GetDescription()
		{
			return "";
		}

		// Token: 0x060082B4 RID: 33460 RVA: 0x002F824E File Offset: 0x002F644E
		public override string GetTemplatedDescription()
		{
			return "";
		}

		// Token: 0x060082B5 RID: 33461 RVA: 0x002F8255 File Offset: 0x002F6455
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.RegisterEffectEvent(parent, "BeginTakeAction");
		}

		// Token: 0x060082B6 RID: 33462 RVA: 0x002F8263 File Offset: 0x002F6463
		public override void Remove(GameObject Object, Effect parent)
		{
			Stomach part = Object.GetPart<Stomach>();
			if (part != null)
			{
				part.GetHungry();
			}
			Object.UnregisterEffectEvent(parent, "BeginTakeAction");
		}

		// Token: 0x060082B7 RID: 33463 RVA: 0x002F8284 File Offset: 0x002F6484
		public override void FireEvent(Event E)
		{
			if (E.ID == "BeginTakeAction")
			{
				Stomach part = this.parent.Object.GetPart<Stomach>();
				if (part != null)
				{
					part.ResetCookingCounter();
				}
				this.parent.Duration--;
				return;
			}
		}

		// Token: 0x0400393F RID: 14655
		public int Tier = 1;
	}
}
