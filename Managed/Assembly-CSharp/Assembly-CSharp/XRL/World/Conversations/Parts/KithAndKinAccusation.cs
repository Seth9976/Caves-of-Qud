using System;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C05 RID: 3077
	public class KithAndKinAccusation : IKithAndKinPart
	{
		// Token: 0x06007B96 RID: 31638 RVA: 0x002E67F2 File Offset: 0x002E49F2
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == PrepareTextEvent.ID;
		}

		// Token: 0x06007B97 RID: 31639 RVA: 0x002E6808 File Offset: 0x002E4A08
		public override bool HandleEvent(PrepareTextEvent E)
		{
			string circumstanceInfluence = base.CircumstanceInfluence;
			string motiveInfluence = base.MotiveInfluence;
			string text = ((circumstanceInfluence == motiveInfluence) ? circumstanceInfluence : (circumstanceInfluence + " and " + motiveInfluence));
			E.Text.StartReplace().AddReplacer("all.influence", text).AddReplacer("motive.influence", motiveInfluence)
				.AddReplacer("thief.name", base.ThiefName)
				.Execute();
			return base.HandleEvent(E);
		}
	}
}
