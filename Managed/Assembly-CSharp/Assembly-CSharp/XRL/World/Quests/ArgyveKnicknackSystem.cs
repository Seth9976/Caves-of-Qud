using System;
using XRL.World.Parts;

namespace XRL.World.Quests
{
	// Token: 0x02000B20 RID: 2848
	[Serializable]
	public class ArgyveKnicknackSystem : IQuestSystem
	{
		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x0600728A RID: 29322 RVA: 0x002B6273 File Offset: 0x002B4473
		public virtual string StepID
		{
			get
			{
				return "Find a Knickknack";
			}
		}

		// Token: 0x0600728B RID: 29323 RVA: 0x002B627A File Offset: 0x002B447A
		public override void RegisterPlayer(GameObject Player, IEventRegistrar Registrar)
		{
			Registrar.Register(TookEvent.ID, 0, false);
		}

		// Token: 0x0600728C RID: 29324 RVA: 0x002B6289 File Offset: 0x002B4489
		public override bool HandleEvent(TookEvent E)
		{
			if (this.IsValidItem(E.Item))
			{
				base.Game.FinishQuestStep(this.QuestID, this.StepID, -1, true, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600728D RID: 29325 RVA: 0x002B62BC File Offset: 0x002B44BC
		public bool IsValidItem(GameObject Object)
		{
			Examiner examiner;
			return Object.HasPart(typeof(TinkerItem)) && Object.TryGetPart<Examiner>(out examiner) && examiner.Complexity > 0;
		}

		// Token: 0x0600728E RID: 29326 RVA: 0x002B62F0 File Offset: 0x002B44F0
		public override void Start()
		{
			foreach (GameObject gameObject in The.Player.Inventory.Objects)
			{
				if (this.IsValidItem(gameObject))
				{
					base.Game.FinishQuestStep(this.QuestID, this.StepID, -1, true, null);
					break;
				}
			}
		}

		// Token: 0x0600728F RID: 29327 RVA: 0x002B636C File Offset: 0x002B456C
		public override GameObject GetInfluencer()
		{
			return GameObject.FindByBlueprint("Argyve");
		}
	}
}
