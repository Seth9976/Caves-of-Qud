using System;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x02000EC3 RID: 3779
	[Serializable]
	public class GuardSpawner : IPart
	{
		// Token: 0x06009787 RID: 38791 RVA: 0x003708F3 File Offset: 0x0036EAF3
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == BeforeObjectCreatedEvent.ID;
		}

		// Token: 0x06009788 RID: 38792 RVA: 0x0037090C File Offset: 0x0036EB0C
		public override bool HandleEvent(BeforeObjectCreatedEvent E)
		{
			string text = "DynamicInheritsTable:Creature:Tier" + Tier.Constrain(ZoneManager.zoneGenerationContextTier + 1).ToString();
			GameObjectBlueprint gameObjectBlueprint = null;
			int num = 0;
			while (++num < 10)
			{
				GameObjectBlueprint gameObjectBlueprint2 = GameObjectFactory.Factory.Blueprints[PopulationManager.RollOneFrom(text, null, null).Blueprint];
				if (HasGuards.IsSuitableGuard(gameObjectBlueprint2))
				{
					gameObjectBlueprint = gameObjectBlueprint2;
					break;
				}
			}
			if (gameObjectBlueprint != null)
			{
				E.ReplacementObject = GameObjectFactory.Factory.CreateObject(gameObjectBlueprint, 0, 0, null, null, null, null, null);
				E.ReplacementObject.AddPart<HiredGuard>(new HiredGuard(null, this.Faction, null, "hired guard", "MerchantGuard", false), true, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003F00 RID: 16128
		public string Faction = "Merchants";

		// Token: 0x04003F01 RID: 16129
		public int minLevel = 20;
	}
}
