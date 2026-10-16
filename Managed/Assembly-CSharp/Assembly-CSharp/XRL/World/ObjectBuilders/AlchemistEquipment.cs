using System;
using XRL.Liquids;
using XRL.World.Parts;

namespace XRL.World.ObjectBuilders
{
	// Token: 0x02000A08 RID: 2568
	public class AlchemistEquipment : IObjectBuilder
	{
		// Token: 0x06006D08 RID: 27912 RVA: 0x002479D4 File Offset: 0x00245BD4
		public override void Apply(GameObject GO, string Context)
		{
			foreach (BaseLiquid baseLiquid in LiquidVolume.getAllLiquids())
			{
				GameObject gameObject = GameObject.Create("Phial", 0, 0, null, null, null, Context, null);
				LiquidVolume liquidVolume = gameObject.LiquidVolume;
				liquidVolume.ComponentLiquids.Clear();
				liquidVolume.ComponentLiquids.Add(baseLiquid.ID, 1000);
				liquidVolume.Volume = 1;
				liquidVolume.Update();
				GO.ReceiveObject(gameObject, false, null);
			}
		}
	}
}
