using System;
using XRL.World.AI;

namespace XRL.World.Parts.Skill
{
	/// This part is not used in the base game.
	// Token: 0x020013B3 RID: 5043
	[Serializable]
	public class TenfoldPath_Sed : BaseInitiatorySkill
	{
		// Token: 0x0600C2BA RID: 49850 RVA: 0x00471F9C File Offset: 0x0047019C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetSocialSifrahSetupEvent>.ID || ID == PooledEvent<ReputationChangeEvent>.ID;
		}

		// Token: 0x0600C2BB RID: 49851 RVA: 0x00471FBA File Offset: 0x004701BA
		public override bool HandleEvent(GetSocialSifrahSetupEvent E)
		{
			E.Rating += 10;
			E.Turns++;
			return base.HandleEvent(E);
		}

		// Token: 0x0600C2BC RID: 49852 RVA: 0x00471FE0 File Offset: 0x004701E0
		public override bool HandleEvent(ReputationChangeEvent E)
		{
			if (E.BaseAmount < 0 && !E.Transient && this.ParentObject.IsPlayer())
			{
				E.Amount /= 2;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C2BD RID: 49853 RVA: 0x00472015 File Offset: 0x00470215
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C2BE RID: 49854 RVA: 0x00472018 File Offset: 0x00470218
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EvilTwinAttitudeSetup");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C2BF RID: 49855 RVA: 0x00472030 File Offset: 0x00470230
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EvilTwinAttitudeSetup")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Twin");
				if (((gameObjectParameter != null) ? gameObjectParameter.Brain : null) != null)
				{
					gameObjectParameter.Brain.Factions = "Entropic-100";
					gameObjectParameter.Brain.AddOpinion<OpinionMollify>(this.ParentObject, 100f);
				}
				return false;
			}
			return base.FireEvent(E);
		}
	}
}
