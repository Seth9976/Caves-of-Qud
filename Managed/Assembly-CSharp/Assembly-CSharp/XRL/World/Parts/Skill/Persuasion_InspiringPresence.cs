using System;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200135A RID: 4954
	[Serializable]
	public class Persuasion_InspiringPresence : BaseSkill
	{
		// Token: 0x0600C0E4 RID: 49380 RVA: 0x004692C7 File Offset: 0x004674C7
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C0E5 RID: 49381 RVA: 0x004692DD File Offset: 0x004674DD
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("MinionTakingAction");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C0E6 RID: 49382 RVA: 0x004692F2 File Offset: 0x004674F2
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("jewels", 3);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C0E7 RID: 49383 RVA: 0x00469318 File Offset: 0x00467518
		public override bool FireEvent(Event E)
		{
			if (E.ID == "MinionTakingAction")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Object");
				int num = this.ParentObject.StatMod("Ego", 0) * 4;
				gameObjectParameter.ApplyEffect(new Emboldened(5, "Hitpoints", num), null);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C0E8 RID: 49384 RVA: 0x00469370 File Offset: 0x00467570
		public override bool AddSkill(GameObject GO)
		{
			if (GO.IsPlayer())
			{
				SocialSifrah.AwardInsight();
			}
			return base.AddSkill(GO);
		}
	}
}
