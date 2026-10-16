using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000EB8 RID: 3768
	[Serializable]
	public class Frenzy : IPart
	{
		// Token: 0x06009721 RID: 38689 RVA: 0x0036D654 File Offset: 0x0036B854
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x06009722 RID: 38690 RVA: 0x0036D657 File Offset: 0x0036B857
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009723 RID: 38691 RVA: 0x0036D65A File Offset: 0x0036B85A
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginTakeAction");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009724 RID: 38692 RVA: 0x0036D670 File Offset: 0x0036B870
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginTakeAction")
			{
				GameObject partyLeader = this.ParentObject.PartyLeader;
				if ((partyLeader == null || partyLeader.IsInvalid() || partyLeader.CurrentCell == null) && !this.ParentObject.HasEffect<Frenzied>())
				{
					this.ParentObject.ApplyEffect(new Frenzied(Stat.Random(100, 200), 10, 10, 5, false, false, false), null);
				}
			}
			return base.FireEvent(E);
		}
	}
}
