using System;

namespace XRL.World.Quests
{
	// Token: 0x02000B25 RID: 2853
	[Serializable]
	public class WillingSpiritSystem : IQuestSystem
	{
		// Token: 0x060072AD RID: 29357 RVA: 0x002B69A0 File Offset: 0x002B4BA0
		public override void RegisterPlayer(GameObject Player, IEventRegistrar Registrar)
		{
			Registrar.Register(TookEvent.ID, 0, false);
		}

		// Token: 0x060072AE RID: 29358 RVA: 0x002B69AF File Offset: 0x002B4BAF
		public override bool HandleEvent(TookEvent E)
		{
			this.CheckObject(E.Item);
			return base.HandleEvent(E);
		}

		// Token: 0x060072AF RID: 29359 RVA: 0x002B69C4 File Offset: 0x002B4BC4
		public override void Start()
		{
			foreach (GameObject gameObject in The.Player.Inventory.GetObjects())
			{
				this.CheckObject(gameObject);
			}
		}

		// Token: 0x060072B0 RID: 29360 RVA: 0x002B6A20 File Offset: 0x002B4C20
		public void CheckObject(GameObject Object)
		{
			if (Object.Blueprint == "Scrapped Waydroid")
			{
				base.Game.FinishQuestStep("More Than a Willing Spirit", "Travel to Golgotha", -1, true, null);
				base.Game.FinishQuestStep("More Than a Willing Spirit", "Find a Dysfunctional Waydroid", -1, true, null);
				return;
			}
			if (Object.Blueprint == "Dormant Waydroid")
			{
				base.Game.FinishQuestStep("More Than a Willing Spirit", "Travel to Golgotha", -1, true, null);
				base.Game.FinishQuestStep("More Than a Willing Spirit", "Find a Dysfunctional Waydroid", -1, true, null);
				base.Game.FinishQuestStep("More Than a Willing Spirit", "Repair the Waydroid", -1, true, null);
			}
		}

		// Token: 0x060072B1 RID: 29361 RVA: 0x002B6ACF File Offset: 0x002B4CCF
		public override GameObject GetInfluencer()
		{
			return GameObject.FindByBlueprint("Mafeo");
		}
	}
}
