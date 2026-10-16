using System;
using XRL.UI;
using XRL.World.Parts;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C29 RID: 3113
	public class WaterRitualNephilimPacify : IWaterRitualPart
	{
		// Token: 0x06007C81 RID: 31873 RVA: 0x002EC570 File Offset: 0x002EA770
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == GetChoiceTagEvent.ID || ID == EnteredElementEvent.ID;
		}

		// Token: 0x06007C82 RID: 31874 RVA: 0x002EC590 File Offset: 0x002EA790
		public override void Awake()
		{
			NephalProperties nephalProperties;
			if (The.Speaker.TryGetPart<NephalProperties>(out nephalProperties) && nephalProperties.Phase < 3 && !NephalProperties.IsFoiled(The.Speaker.Blueprint))
			{
				this.Reputation = GetWaterRitualCostEvent.GetFor(The.Player, The.Speaker, "Pacify", 200);
				this.Visible = true;
			}
		}

		// Token: 0x06007C83 RID: 31875 RVA: 0x002EC5EC File Offset: 0x002EA7EC
		public override bool HandleEvent(EnteredElementEvent E)
		{
			NephalProperties nephalProperties;
			if (The.Speaker.TryGetPart<NephalProperties>(out nephalProperties) && base.UseReputation("WaterRitualUse") && nephalProperties.TryPacify())
			{
				this.TryFinishNephalStep();
				this.TryGiveCircle();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007C84 RID: 31876 RVA: 0x002EC634 File Offset: 0x002EA834
		public bool TryFinishNephalStep()
		{
			Quest quest;
			QuestStep questStep;
			if (The.Game.Quests.TryGetValue("Reclamation", out quest) && quest.StepsByID.TryGetValue("Nephal", out questStep) && !questStep.Finished && questStep.Value == The.Speaker.Blueprint)
			{
				The.Game.FinishQuestStep(quest, questStep.ID, -1, true, null);
				return true;
			}
			return false;
		}

		// Token: 0x06007C85 RID: 31877 RVA: 0x002EC6A4 File Offset: 0x002EA8A4
		public bool TryGiveCircle()
		{
			bool flag = false;
			DropOnDeath dropOnDeath;
			if (The.Speaker.TryGetPart<DropOnDeath>(out dropOnDeath) && !dropOnDeath.Blueprints.IsNullOrEmpty())
			{
				GameObject gameObject = GameObject.Create(dropOnDeath.Blueprints);
				flag = The.Player.ReceiveObject(gameObject, false, null);
				Popup.Show("You receive " + gameObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + "!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				The.Speaker.RemovePart(dropOnDeath);
			}
			return flag;
		}

		// Token: 0x06007C86 RID: 31878 RVA: 0x002EC738 File Offset: 0x002EA938
		public override bool HandleEvent(GetChoiceTagEvent E)
		{
			E.Tag = string.Concat(new string[]
			{
				"{{",
				this.Lowlight,
				"|[{{",
				this.Numeric,
				"|",
				base.GetReputationCost("WaterRitualUse").ToString(),
				"}} reputation]}}"
			});
			return false;
		}
	}
}
