using System;
using XRL.World.Effects;
using XRL.World.Parts;

namespace XRL.World.Biomes
{
	// Token: 0x02000A25 RID: 2597
	public static class RustedInventoryTemplate
	{
		// Token: 0x06006D6B RID: 28011 RVA: 0x0024B233 File Offset: 0x00249433
		private static void ApplyMetalRust(GameObject Object)
		{
			if (Object.HasPart<Metal>() && 50.in100())
			{
				Object.ApplyEffect(new Rusted(), null);
			}
		}

		// Token: 0x06006D6C RID: 28012 RVA: 0x0024B254 File Offset: 0x00249454
		public static void Apply(GameObject GO)
		{
			if (((GO != null) ? GO.Render : null) != null)
			{
				Body body = GO.Body;
				if (body != null)
				{
					body.ForeachEquippedObject(new Action<GameObject>(RustedInventoryTemplate.ApplyMetalRust));
				}
				Inventory inventory = GO.Inventory;
				if (inventory == null)
				{
					return;
				}
				inventory.ReverseForeachObject(new Action<GameObject>(RustedInventoryTemplate.ApplyMetalRust));
			}
		}
	}
}
