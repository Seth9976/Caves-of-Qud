using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x02000EF8 RID: 3832
	public class RobotSpawner : IPart
	{
		// Token: 0x06009906 RID: 39174 RVA: 0x0037897C File Offset: 0x00376B7C
		public override bool WantEvent(int ID, int cascade)
		{
			return ID == BeforeObjectCreatedEvent.ID;
		}

		// Token: 0x06009907 RID: 39175 RVA: 0x00378988 File Offset: 0x00376B88
		public override bool HandleEvent(BeforeObjectCreatedEvent E)
		{
			string text = "Robots";
			Predicate<GameObjectBlueprint> predicate;
			if (!this.Village)
			{
				predicate = null;
			}
			else
			{
				predicate = (GameObjectBlueprint x) => !x.HasTag("ExcludeFromVillagePopulations", false);
			}
			List<GameObjectBlueprint> members = Faction.GetMembers(text, predicate, true, true);
			E.ReplacementObject = GameObject.Create(members.GetRandomElement<GameObjectBlueprint>(null), 0, 0, null, null, null, null, null);
			return base.HandleEvent(E);
		}

		// Token: 0x04003FBF RID: 16319
		public bool Village;
	}
}
