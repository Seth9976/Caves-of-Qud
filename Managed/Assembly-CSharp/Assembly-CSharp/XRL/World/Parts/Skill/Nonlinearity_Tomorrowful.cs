using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001358 RID: 4952
	[Serializable]
	public class Nonlinearity_Tomorrowful : BaseSkill
	{
		// Token: 0x0600C0D3 RID: 49363 RVA: 0x00468B22 File Offset: 0x00466D22
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C0D4 RID: 49364 RVA: 0x00468B25 File Offset: 0x00466D25
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("InitiatePrecognition");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C0D5 RID: 49365 RVA: 0x00468B3C File Offset: 0x00466D3C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "InitiatePrecognition")
			{
				int intParameter = E.GetIntParameter("Duration", 0);
				E.SetParameter("Duration", intParameter * 2);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C0D6 RID: 49366 RVA: 0x00468B7E File Offset: 0x00466D7E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C0D7 RID: 49367 RVA: 0x00468B94 File Offset: 0x00466D94
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("time", 3);
			}
			return base.HandleEvent(E);
		}
	}
}
