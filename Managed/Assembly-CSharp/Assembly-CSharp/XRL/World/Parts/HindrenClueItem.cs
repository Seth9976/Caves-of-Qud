using System;
using Qud.API;

namespace XRL.World.Parts
{
	// Token: 0x02000FDD RID: 4061
	[Serializable]
	public class HindrenClueItem : IPart
	{
		// Token: 0x0600A145 RID: 41285 RVA: 0x003AB10F File Offset: 0x003A930F
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600A146 RID: 41286 RVA: 0x003AB112 File Offset: 0x003A9312
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquippedEvent.ID || ID == TakenEvent.ID;
		}

		// Token: 0x0600A147 RID: 41287 RVA: 0x003AB130 File Offset: 0x003A9330
		public override bool HandleEvent(EquippedEvent E)
		{
			this.RevealHindrenClue(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A148 RID: 41288 RVA: 0x003AB145 File Offset: 0x003A9345
		public override bool HandleEvent(TakenEvent E)
		{
			this.RevealHindrenClue(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A149 RID: 41289 RVA: 0x003AB15A File Offset: 0x003A935A
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterLookedAt");
			Registrar.Register("LookedAt");
			Registrar.Register("Open");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A14A RID: 41290 RVA: 0x003AB188 File Offset: 0x003A9388
		public override bool FireEvent(Event E)
		{
			if (E.ID == "LookedAt" || E.ID == "AfterLookedAt")
			{
				this.RevealHindrenClue(null);
			}
			else if (E.ID == "Open")
			{
				this.RevealHindrenClue(E.GetGameObjectParameter("Opener"));
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600A14B RID: 41291 RVA: 0x003AB1EC File Offset: 0x003A93EC
		private void RevealHindrenClue(GameObject who = null)
		{
			if (The.Game != null && The.Game.HasQuest("Kith and Kin") && !The.Game.FinishedQuest("Kith and Kin") && (who == null || who.IsPlayer()))
			{
				JournalAPI.RevealObservation(this.ParentObject.Blueprint, true);
				KithAndKinGameState.Instance.foundClue();
			}
		}
	}
}
