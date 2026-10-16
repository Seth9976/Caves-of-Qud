using System;
using XRL.World.Parts;

namespace XRL.World.Quests
{
	// Token: 0x02000B22 RID: 2850
	[Serializable]
	public class WeirdwireConduitSystem : IQuestSystem
	{
		// Token: 0x06007294 RID: 29332 RVA: 0x002B642B File Offset: 0x002B462B
		public override void RegisterPlayer(GameObject Player, IEventRegistrar Registrar)
		{
			Registrar.Register(TookEvent.ID, 0, false);
		}

		// Token: 0x06007295 RID: 29333 RVA: 0x002B643C File Offset: 0x002B463C
		public override bool HandleEvent(TookEvent E)
		{
			int totalLength = this.TotalLength;
			this.TotalLength = The.Player.Inventory.GetObjectStackCount((GameObject x) => x.HasPart(typeof(Wire)), true);
			if (this.TotalLength >= 200)
			{
				The.Game.FinishQuestStep("Weirdwire Conduit... Eureka!", "Find 200 Feet of Wire", -1, true, null);
			}
			else if (this.TotalLength != totalLength)
			{
				base.AddPlayerMessage("You now have " + this.TotalLength.ToString() + " feet of copper wire.", 'c', true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007296 RID: 29334 RVA: 0x002B64E0 File Offset: 0x002B46E0
		public override void Start()
		{
			int num = 0;
			foreach (GameObject gameObject in The.Player.Inventory.Objects)
			{
				if (gameObject.HasPart(typeof(Wire)))
				{
					num += gameObject.Count;
				}
			}
			if (num >= 200)
			{
				The.Game.FinishQuestStep("Weirdwire Conduit... Eureka!", "Find 200 Feet of Wire", -1, true, null);
			}
		}

		// Token: 0x06007297 RID: 29335 RVA: 0x002B6574 File Offset: 0x002B4774
		public override void Finish()
		{
			GameObject influencer = this.GetInfluencer();
			if (influencer != null)
			{
				AIWiring aiwiring = influencer.RequirePart<AIWiring>(false);
				influencer.Brain.Goals.Clear();
				aiwiring.QueueAction();
			}
		}

		// Token: 0x06007298 RID: 29336 RVA: 0x002B65AA File Offset: 0x002B47AA
		public override GameObject GetInfluencer()
		{
			return GameObject.FindByBlueprint("Argyve");
		}

		// Token: 0x04003528 RID: 13608
		public int TotalLength;
	}
}
