using System;
using XRL.Core;

namespace XRL.World.Parts
{
	// Token: 0x02001209 RID: 4617
	[Serializable]
	public class FriendlyFireAmnestyDuringQuest : IPart
	{
		// Token: 0x0600B7A8 RID: 47016 RVA: 0x0042D1F0 File Offset: 0x0042B3F0
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B7A9 RID: 47017 RVA: 0x0042D1F3 File Offset: 0x0042B3F3
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CanBeAngeredByBeingAttacked");
			Registrar.Register("CanBeAngeredByDamage");
			Registrar.Register("CanBeAngeredByFriendlyFire");
			Registrar.Register("CanBeAngeredByPropertyCrime");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B7AA RID: 47018 RVA: 0x0042D22C File Offset: 0x0042B42C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CanBeAngeredByBeingAttacked" || E.ID == "CanBeAngeredByDamage")
			{
				if (!string.IsNullOrEmpty(this.QuestName) && XRLCore.Core.Game.HasUnfinishedQuest(this.QuestName) && this.ParentObject.IsAlliedTowards(E.GetGameObjectParameter("Attacker")))
				{
					return false;
				}
			}
			else if ((E.ID == "CanBeAngeredByFriendlyFire" || E.ID == "CanBeAngeredByPropertyCrime") && !string.IsNullOrEmpty(this.QuestName) && XRLCore.Core.Game.HasUnfinishedQuest(this.QuestName))
			{
				return false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x040049DC RID: 18908
		public string QuestName;
	}
}
