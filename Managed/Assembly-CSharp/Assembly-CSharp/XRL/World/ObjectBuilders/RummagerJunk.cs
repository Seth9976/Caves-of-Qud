using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World.ObjectBuilders
{
	// Token: 0x02000A05 RID: 2565
	public class RummagerJunk : IObjectBuilder
	{
		// Token: 0x06006D02 RID: 27906 RVA: 0x002475DC File Offset: 0x002457DC
		public override void Apply(GameObject GO, string Context)
		{
			for (int i = 0; i < 3; i++)
			{
				if (25.in100())
				{
					GO.ReceiveObjectFromPopulation("Junk 2", null, false, 0, 0, null, null, null, Context, null);
				}
			}
			for (int j = 0; j < 3; j++)
			{
				if (25.in100())
				{
					GO.ReceiveObjectFromPopulation("Junk 3", null, false, 0, 0, null, null, null, Context, null);
				}
			}
			if (5.in100())
			{
				GO.ReceiveObjectFromPopulation("Junk 4", null, false, 0, 0, null, null, null, Context, null);
			}
			string text = "Desert Rifle";
			string text2 = "Lead Slugs";
			int num = 40;
			int num2 = Stat.Random(1, 100);
			if (num2 <= 25)
			{
				text = "Desert Rifle";
				text2 = "Lead Slug";
				num = Stat.Random(6, 12);
			}
			else if (num2 <= 60)
			{
				text = "Borderlands Revolver";
				text2 = "Lead Slug";
				num = Stat.Random(12, 16);
			}
			else if (num2 <= 70)
			{
				text = "Semi-Automatic Pistol";
				text2 = "Lead Slug";
				num = Stat.Random(12, 26);
			}
			else if (num2 <= 80)
			{
				text = "Carbine";
				text2 = "Lead Slug";
				num = Stat.Random(16, 26);
			}
			else if (num2 <= 81)
			{
				text = "Grenade Launcher";
				text2 = "HEGrenade1";
				num = Stat.Random(3, 6);
			}
			else if (num2 <= 83)
			{
				text = "Missile Launcher";
				text2 = "HE Missile";
				num = Stat.Random(3, 6);
			}
			else if (num2 <= 85)
			{
				text = "Chaingun";
				text2 = "Lead Slug";
				num = Stat.Random(176, 196);
			}
			else if (num2 <= 88)
			{
				text = "Flamethrower";
				text2 = "Oilskin";
				num = Stat.Random(1, 2);
			}
			else if (num2 <= 91)
			{
				text = "Sniper Rifle";
				text2 = "Lead Slug";
				num = Stat.Random(14, 16);
			}
			else if (num2 <= 96)
			{
				text = "Chain Pistol";
				text2 = "Lead Slug";
				num = Stat.Random(114, 116);
			}
			else if (num2 <= 100)
			{
				text = "Combat Shotgun";
				text2 = "Shotgun Shell";
				num = Stat.Random(8, 16);
			}
			if (text2 == "Chem Cell")
			{
				using (List<GameObject>.Enumerator enumerator = new List<GameObject> { GameObject.Create(text, 0, 0, null, null, null, Context, null) }.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						GameObject gameObject = enumerator.Current;
						EnergyCellSocket energyCellSocket;
						if (gameObject.TryGetPart<EnergyCellSocket>(out energyCellSocket))
						{
							energyCellSocket.Cell = GameObject.Create("Chem Cell", 0, 0, null, null, null, Context, null);
						}
						GO.ReceiveObject(gameObject, false, null);
					}
					return;
				}
			}
			GameObject gameObject2 = GameObject.Create(text, 0, 0, null, null, null, Context, null);
			LiquidAmmoLoader part = gameObject2.GetPart<LiquidAmmoLoader>();
			for (int k = 0; k < num; k++)
			{
				GameObject gameObject3 = GameObject.Create(text2, 0, 0, null, null, null, Context, null);
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
			GO.ReceiveObject(gameObject2, false, null);
		}
	}
}
