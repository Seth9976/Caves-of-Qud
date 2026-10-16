using System;
using System.Collections.Generic;
using HistoryKit;
using Qud.API;
using XRL.Core;

namespace XRL.World.Parts
{
	// Token: 0x02000E62 RID: 3682
	[Serializable]
	public class CultistSpawner : IPart
	{
		// Token: 0x060094E9 RID: 38121 RVA: 0x0035DB02 File Offset: 0x0035BD02
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x060094EA RID: 38122 RVA: 0x0035DB05 File Offset: 0x0035BD05
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x060094EB RID: 38123 RVA: 0x0035DB1C File Offset: 0x0035BD1C
		private GameObject GetLikedFactionMember(HistoricEntitySnapshot SultanSnapshot, Predicate<GameObjectBlueprint> filter)
		{
			List<string> list = SultanSnapshot.GetList("likedFactions");
			GameObject gameObject = null;
			list.Shuffle<string>(null);
			foreach (string text in list)
			{
				gameObject = EncountersAPI.GetACreatureFromFaction(text, filter);
				if (gameObject != null)
				{
					break;
				}
			}
			return gameObject;
		}

		// Token: 0x060094EC RID: 38124 RVA: 0x0035DB84 File Offset: 0x0035BD84
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell" && this.CultistType == "Tomb Cultist")
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				HistoricEntitySnapshot currentSnapshot = XRLCore.Core.Game.sultanHistory.GetEntitiesByDelegate(delegate(HistoricEntity e)
				{
					HistoricEntitySnapshot currentSnapshot2 = e.GetCurrentSnapshot();
					return currentSnapshot2.GetProperty("type", "unknown") == "sultan" && int.Parse(currentSnapshot2.GetProperty("period", "unknown")) == this.Period;
				}).GetRandomElement(null).GetCurrentSnapshot();
				string text = ((this.Period == 6) ? "Resheph" : ("SultanCult" + this.Period.ToString()));
				if (!this.bTierExact && If.Chance(this.TierBumpChance, null, null))
				{
					this.Tier = (If.CoinFlip(null, null) ? (this.Tier - 1) : (this.Tier + 1));
				}
				GameObject gameObject = this.GetLikedFactionMember(currentSnapshot, (GameObjectBlueprint c) => c.Tier == this.Tier && !c.HasTag("Merchant", false));
				if (gameObject == null)
				{
					gameObject = EncountersAPI.GetACreature((GameObjectBlueprint c) => c.Tier == this.Tier && !c.HasTag("Merchant", false));
				}
				if (gameObject == null)
				{
					this.ParentObject.Destroy(null, false, false, null);
					return false;
				}
				CultistTemplate.Apply(gameObject, text);
				TombCultistTemplate.Apply(gameObject, currentSnapshot);
				gameObject.MakeActive(false);
				currentCell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				this.ParentObject.Destroy(null, false, false, null);
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003DAF RID: 15791
		public int Tier = 1;

		// Token: 0x04003DB0 RID: 15792
		public int Period = 5;

		// Token: 0x04003DB1 RID: 15793
		public string CultistType = "Tomb Cultist";

		// Token: 0x04003DB2 RID: 15794
		public bool bTierExact;

		// Token: 0x04003DB3 RID: 15795
		public int TierBumpChance = 15;
	}
}
