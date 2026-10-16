using System;
using System.Runtime.CompilerServices;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World.ObjectBuilders
{
	// Token: 0x02000A09 RID: 2569
	public class GunslingerEquipment1 : IObjectBuilder
	{
		// Token: 0x06006D0A RID: 27914 RVA: 0x00247A70 File Offset: 0x00245C70
		public override void Apply(GameObject GO, string Context)
		{
			GunslingerEquipment1.<>c__DisplayClass0_0 CS$<>8__locals1;
			CS$<>8__locals1.Context = Context;
			string text = "Laser Pistol";
			string text2 = "Chem Cell";
			int num = 2;
			int num2 = Stat.Random(1, 100);
			if (num2 <= 25)
			{
				text = "Laser Pistol";
			}
			else if (num2 <= 55)
			{
				text = "Borderlands Revolver";
				text2 = "Lead Slug";
				num = Stat.Random(36, 46);
			}
			else if (num2 <= 75)
			{
				text = "Semi-Automatic Pistol";
				text2 = "Lead Slug";
				num = Stat.Random(76, 96);
			}
			else if (num2 <= 100)
			{
				text = "Chain Pistol";
				text2 = "Lead Slug";
				num = Stat.Random(136, 246);
			}
			if (text2 == "Chem Cell")
			{
				GO.ReceiveObject(GunslingerEquipment1.<Apply>g__SpawnWithChemCell|0_0(GameObject.Create(text, 45, 0, null, null, null, CS$<>8__locals1.Context, null), ref CS$<>8__locals1), false, null);
				GO.ReceiveObject(GunslingerEquipment1.<Apply>g__SpawnWithChemCell|0_0(GameObject.Create(text, 0, 0, null, null, null, CS$<>8__locals1.Context, null), ref CS$<>8__locals1), false, null);
				return;
			}
			GameObject gameObject = GameObject.Create(text, 45, 0, null, null, null, CS$<>8__locals1.Context, null);
			GameObject gameObject2 = GameObject.Create(text, 0, 0, null, null, null, CS$<>8__locals1.Context, null);
			LiquidAmmoLoader part = gameObject.GetPart<LiquidAmmoLoader>();
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject3 = GameObject.Create(text2, 0, 0, null, null, null, CS$<>8__locals1.Context, null);
				if (part != null)
				{
					LiquidVolume liquidVolume = gameObject3.LiquidVolume;
					if (liquidVolume != null)
					{
						liquidVolume.Empty(false, false);
						liquidVolume.ComponentLiquids.Add(part.Liquid, 1000);
						liquidVolume.Volume = Stat.Random(liquidVolume.MaxVolume / 3 + 1, liquidVolume.MaxVolume);
						liquidVolume.Update();
					}
				}
				GO.ReceiveObject(gameObject3, false, null);
			}
			GO.ReceiveObject(gameObject, false, null);
			GO.ReceiveObject(gameObject2, false, null);
		}

		// Token: 0x06006D0C RID: 27916 RVA: 0x00247C38 File Offset: 0x00245E38
		[CompilerGenerated]
		internal static GameObject <Apply>g__SpawnWithChemCell|0_0(GameObject gun, ref GunslingerEquipment1.<>c__DisplayClass0_0 A_1)
		{
			EnergyCellSocket energyCellSocket;
			if (gun.TryGetPart<EnergyCellSocket>(out energyCellSocket))
			{
				energyCellSocket.Cell = GameObject.Create("Chem Cell", 0, 0, null, null, null, A_1.Context, null);
			}
			return gun;
		}
	}
}
