using System;
using ConsoleLib.Console;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x0200130D RID: 4877
	[Serializable]
	public class TrembleEarthquakes : IPart
	{
		// Token: 0x0600BED5 RID: 48853 RVA: 0x0045DE13 File Offset: 0x0045C013
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID;
		}

		// Token: 0x0600BED6 RID: 48854 RVA: 0x0045DE29 File Offset: 0x0045C029
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.CheckQuake();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BED7 RID: 48855 RVA: 0x0045DE38 File Offset: 0x0045C038
		public void CheckQuake()
		{
			if (Stat.Random(1, 200) == 1)
			{
				this.Quake();
			}
		}

		// Token: 0x0600BED8 RID: 48856 RVA: 0x0045DE50 File Offset: 0x0045C050
		public void Quake()
		{
			Zone currentZone = this.ParentObject.CurrentZone;
			if (currentZone.IsActive())
			{
				ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(true);
				The.Core.RenderBaseToBuffer(scrapBuffer);
				scrapBuffer.Shake(500, 25, Popup._TextConsole);
				if (currentZone.IsOutside())
				{
					IComponent<GameObject>.AddPlayerMessage("The ground shakes violently!", null, true);
					return;
				}
				IComponent<GameObject>.AddPlayerMessage("The ground shakes violently and loose rock falls from the ceiling!", null, true);
				this.RocksFall(currentZone);
			}
		}

		// Token: 0x0600BED9 RID: 48857 RVA: 0x0045DEC0 File Offset: 0x0045C0C0
		public void RocksFall(Zone Zone)
		{
			for (int i = 0; i < Zone.Height; i++)
			{
				for (int j = 0; j < Zone.Width; j++)
				{
					Cell cell = Zone.GetCell(j, i);
					if (cell != null)
					{
						int k = 0;
						int count = cell.Objects.Count;
						while (k < count)
						{
							GameObject gameObject = cell.Objects[k];
							if (gameObject.IsCreature && gameObject.IsPlayerControlled() && gameObject.PhaseMatches(1))
							{
								int num = Stat.RollDamagePenetrations(Stats.GetCombatAV(gameObject), 0, 0);
								if (num > 0)
								{
									string resultColor = Stat.GetResultColor(num);
									int num2 = 0;
									for (int l = 0; l < num; l++)
									{
										num2 += Stat.Random(1, 3);
									}
									gameObject.TakeDamage(num2, string.Concat(new string[]
									{
										"from falling rocks! {{",
										resultColor,
										"|(x",
										num.ToString(),
										")}}"
									}), "Crushing Cudgel", "You were crushed by falling rocks.", gameObject.Does("were", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, true, true, null, true, null, false) + " @@crushed by falling rocks.", null, null, null, null, null, false, false, false, false, false, false, false, false, false, 5, null);
								}
							}
							k++;
						}
					}
				}
			}
		}
	}
}
