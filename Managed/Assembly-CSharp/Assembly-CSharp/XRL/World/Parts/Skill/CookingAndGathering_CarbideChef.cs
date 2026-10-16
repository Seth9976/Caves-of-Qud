using System;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001325 RID: 4901
	[Serializable]
	public class CookingAndGathering_CarbideChef : BaseSkill
	{
		// Token: 0x0600BFB3 RID: 49075 RVA: 0x004636B2 File Offset: 0x004618B2
		public void Inspire()
		{
			if (!this.ParentObject.HasEffect<Inspired>())
			{
				this.ParentObject.ApplyEffect(new Inspired(2400), null);
			}
		}

		// Token: 0x0600BFB4 RID: 49076 RVA: 0x004636D8 File Offset: 0x004618D8
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == PooledEvent<AfterLevelGainedEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600BFB5 RID: 49077 RVA: 0x004636F6 File Offset: 0x004618F6
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("salt", 3);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BFB6 RID: 49078 RVA: 0x00463719 File Offset: 0x00461919
		public override bool HandleEvent(AfterLevelGainedEvent E)
		{
			if (E.Actor.IsPlayer() && E.Actor == this.ParentObject)
			{
				this.Inspire();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BFB7 RID: 49079 RVA: 0x00463743 File Offset: 0x00461943
		public override bool FireEvent(Event E)
		{
			if (E.ID == "VisitingNewZone" && this.ParentObject.IsPlayer() && 5.in100())
			{
				this.Inspire();
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600BFB8 RID: 49080 RVA: 0x00463779 File Offset: 0x00461979
		public override bool AddSkill(GameObject GO)
		{
			GO.RegisterPartEvent(this, "VisitingNewZone");
			return base.AddSkill(GO);
		}

		// Token: 0x0600BFB9 RID: 49081 RVA: 0x0046378E File Offset: 0x0046198E
		public override bool RemoveSkill(GameObject GO)
		{
			GO.UnregisterPartEvent(this, "VisitingNewZone");
			return base.AddSkill(GO);
		}
	}
}
