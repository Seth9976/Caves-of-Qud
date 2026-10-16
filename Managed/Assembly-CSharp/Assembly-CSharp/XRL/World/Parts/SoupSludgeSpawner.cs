using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F01 RID: 3841
	[Serializable]
	public class SoupSludgeSpawner : IPart
	{
		// Token: 0x0600994C RID: 39244 RVA: 0x0037A5BE File Offset: 0x003787BE
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600994D RID: 39245 RVA: 0x0037A5C1 File Offset: 0x003787C1
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x0600994E RID: 39246 RVA: 0x0037A5D8 File Offset: 0x003787D8
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			int num = Math.Min(this.Rank.RollCached(), LiquidVolume.Liquids.Count);
			GameObject gameObject = GameObjectFactory.Factory.CreateObject("SoupSludge");
			gameObject.SetStringProperty("SpawnedFrom", this.ParentObject.Blueprint, false);
			int num2 = 0;
			SoupSludge soupSludge = gameObject.AddPart<SoupSludge>(new SoupSludge(), true, false);
			while (soupSludge.ComponentLiquids.Count < num && num2++ < 100)
			{
				string blueprint = PopulationManager.GenerateOne("RandomLiquidWithRaresNoLava", null, null, null, null, null).Blueprint;
				if (!soupSludge.ComponentLiquids.Contains(blueprint))
				{
					soupSludge.CatalyzeLiquid(blueprint);
					num2 = 0;
				}
			}
			soupSludge.CatalyzeName();
			E.ReplacementObject = gameObject;
			return base.HandleEvent(E);
		}

		// Token: 0x04003FDE RID: 16350
		public string Rank = "1d20";
	}
}
