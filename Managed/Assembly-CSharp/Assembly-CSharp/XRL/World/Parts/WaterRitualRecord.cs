using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000F13 RID: 3859
	[Serializable]
	public class WaterRitualRecord : IPart
	{
		// Token: 0x060099CC RID: 39372 RVA: 0x0037DC38 File Offset: 0x0037BE38
		public override void Initialize()
		{
			base.Initialize();
			Faction faction = Factions.Get(this.ParentObject.GetPrimaryFaction(true));
			this.faction = faction.Name;
			GivesRep part = this.ParentObject.GetPart<GivesRep>();
			this.totalFactionAvailable = ((part != null) ? part.repValue : (RuleSettings.REPUTATION_BASE_UNIT * 2));
			string text = "ritualRecord";
			Zone currentZone = this.ParentObject.CurrentZone;
			this.mySeed = Stat.SeededRandom(text + ((currentZone != null) ? currentZone.ZoneID : null) + this.ParentObject.DisplayName, 0, 2147483646);
			GameObjectBlueprint blueprint = this.ParentObject.GetBlueprint(true);
			bool flag = faction.UseAltBehavior(this.ParentObject);
			string text2 = blueprint.GetxTag("WaterRitual", "numSecrets", null);
			if (!text2.IsNullOrEmpty())
			{
				this.secretsRemaining = Math.Max(text2.Roll(), 0);
			}
			if (blueprint.GetxTag("WaterRitual", "SellBlueprints", null) == "true")
			{
				string text3 = blueprint.GetxTag("WaterRitual", "numBlueprints", null);
				if (!text3.IsNullOrEmpty())
				{
					this.numBlueprints = Math.Max(text3.Roll(), 0);
					return;
				}
			}
			else if (flag)
			{
				if (!faction.WaterRitualAltBlueprints.IsNullOrEmpty())
				{
					this.numBlueprints = Math.Max(faction.WaterRitualAltBlueprints.Roll(), 0);
				}
				if (!faction.WaterRitualAltGifts.IsNullOrEmpty())
				{
					this.numGifts = Math.Max(faction.WaterRitualAltGifts.Roll(), 0);
				}
				if (!faction.WaterRitualAltItems.IsNullOrEmpty())
				{
					this.numItems = Math.Max(faction.WaterRitualAltItems.Roll(), 0);
					return;
				}
			}
			else
			{
				if (!faction.WaterRitualBlueprints.IsNullOrEmpty())
				{
					this.numBlueprints = Math.Max(faction.WaterRitualBlueprints.Roll(), 0);
				}
				if (!faction.WaterRitualGifts.IsNullOrEmpty())
				{
					this.numGifts = Math.Max(faction.WaterRitualGifts.Roll(), 0);
				}
				if (!faction.WaterRitualItems.IsNullOrEmpty())
				{
					this.numItems = Math.Max(faction.WaterRitualItems.Roll(), 0);
				}
			}
		}

		// Token: 0x060099CD RID: 39373 RVA: 0x0037DE3E File Offset: 0x0037C03E
		public bool Has(string attribute)
		{
			return this.attributes.Contains(attribute);
		}

		// Token: 0x060099CE RID: 39374 RVA: 0x0037DE4C File Offset: 0x0037C04C
		public string GetAttribute(string Prefix, string Default = null)
		{
			foreach (string text in this.attributes)
			{
				if (!text.StartsWith(Prefix))
				{
					return text.Substring(Prefix.Length);
				}
			}
			return Default;
		}

		// Token: 0x060099CF RID: 39375 RVA: 0x0037DEB4 File Offset: 0x0037C0B4
		public bool TryGetAttribute(string Prefix, out string Value)
		{
			foreach (string text in this.attributes)
			{
				if (text.StartsWith(Prefix))
				{
					Value = text.Substring(Prefix.Length);
					return true;
				}
			}
			Value = null;
			return false;
		}

		// Token: 0x060099D0 RID: 39376 RVA: 0x0037DF24 File Offset: 0x0037C124
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == BeginConversationEvent.ID && this.hermitChatPenalty > 0) || ID == ReplicaCreatedEvent.ID;
		}

		// Token: 0x060099D1 RID: 39377 RVA: 0x0037DF4C File Offset: 0x0037C14C
		public override bool HandleEvent(BeginConversationEvent E)
		{
			if (this.hermitChatPenalty > 0 && E.SpeakingWith == this.ParentObject && E.Actor.IsPlayer())
			{
				Popup.Show("You bothered " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " again.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				The.Game.PlayerReputation.Modify(this.faction, -this.hermitChatPenalty, "WaterRitualHermitOathPunishment", null, null, false, false);
				this.hermitChatPenalty = 0;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060099D2 RID: 39378 RVA: 0x0037E000 File Offset: 0x0037C200
		public override bool HandleEvent(ReplicaCreatedEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				E.WantToRemove(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004017 RID: 16407
		public int mySeed;

		// Token: 0x04004018 RID: 16408
		public int secretsRemaining = Stat.Random(2, 3);

		// Token: 0x04004019 RID: 16409
		public bool giftedItem;

		// Token: 0x0400401A RID: 16410
		public bool soldItem;

		// Token: 0x0400401B RID: 16411
		public bool canGenerateItem = true;

		// Token: 0x0400401C RID: 16412
		public string faction;

		// Token: 0x0400401D RID: 16413
		public int totalFactionAvailable = 100;

		// Token: 0x0400401E RID: 16414
		public int hermitChatPenalty;

		// Token: 0x0400401F RID: 16415
		public int numBlueprints;

		// Token: 0x04004020 RID: 16416
		public int numGifts = 1;

		// Token: 0x04004021 RID: 16417
		public int numItems;

		// Token: 0x04004022 RID: 16418
		public int numFungusLeft = 1;

		// Token: 0x04004023 RID: 16419
		public int LastSocialCoprocessorBonus;

		// Token: 0x04004024 RID: 16420
		public List<string> attributes = new List<string>();

		// Token: 0x04004025 RID: 16421
		public List<int> tinkerdata = new List<int>(2);
	}
}
