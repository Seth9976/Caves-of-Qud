using System;

namespace XRL.World.Parts.Skill
{
	/// This part is not used in the base game.
	// Token: 0x020013AE RID: 5038
	[Serializable]
	public class TenfoldPath_Hod : BaseInitiatorySkill
	{
		// Token: 0x0600C29E RID: 49822 RVA: 0x00471A24 File Offset: 0x0046FC24
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetRandomBuyChimericBodyPartRollsEvent>.ID || ID == PooledEvent<GetRandomBuyMutationCountEvent>.ID;
		}

		// Token: 0x0600C29F RID: 49823 RVA: 0x00471A42 File Offset: 0x0046FC42
		public override bool HandleEvent(GetRandomBuyMutationCountEvent E)
		{
			E.Amount++;
			return base.HandleEvent(E);
		}

		// Token: 0x0600C2A0 RID: 49824 RVA: 0x00471A59 File Offset: 0x0046FC59
		public override bool HandleEvent(GetRandomBuyChimericBodyPartRollsEvent E)
		{
			E.Amount++;
			return base.HandleEvent(E);
		}

		// Token: 0x0600C2A1 RID: 49825 RVA: 0x00471A70 File Offset: 0x0046FC70
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C2A2 RID: 49826 RVA: 0x00471A73 File Offset: 0x0046FC73
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Regenerating");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C2A3 RID: 49827 RVA: 0x00471A88 File Offset: 0x0046FC88
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Regenerating")
			{
				E.SetParameter("Amount", E.GetIntParameter("Amount", 0) * 21 / 20);
			}
			return base.FireEvent(E);
		}
	}
}
