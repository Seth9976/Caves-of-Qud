using System;
using XRL.World.Effects;
using XRL.World.Parts;

namespace XRL.World.Biomes
{
	// Token: 0x02000A23 RID: 2595
	public static class RustedTemplate
	{
		// Token: 0x06006D68 RID: 28008 RVA: 0x0024B127 File Offset: 0x00249327
		public static void Apply(GameObject GO)
		{
			if (GO.HasPart<Metal>())
			{
				GO.ApplyEffect(new Rusted(), null);
			}
		}
	}
}
