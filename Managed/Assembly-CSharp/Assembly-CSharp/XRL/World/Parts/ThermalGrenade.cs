using System;
using System.Collections.Generic;
using System.Threading;
using ConsoleLib.Console;
using XRL.Core;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000FD8 RID: 4056
	[Serializable]
	public class ThermalGrenade : IGrenade
	{
		// Token: 0x0600A126 RID: 41254 RVA: 0x003AA549 File Offset: 0x003A8749
		public override bool SameAs(IPart p)
		{
			return (p as ThermalGrenade).TemperatureDelta == this.TemperatureDelta && base.SameAs(p);
		}

		// Token: 0x0600A127 RID: 41255 RVA: 0x003AA567 File Offset: 0x003A8767
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetComponentNavigationWeightEvent.ID || ID == GetComponentAdjacentNavigationWeightEvent.ID;
		}

		// Token: 0x0600A128 RID: 41256 RVA: 0x003AA585 File Offset: 0x003A8785
		public override bool HandleEvent(GetComponentNavigationWeightEvent E)
		{
			E.MinWeight(8);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A129 RID: 41257 RVA: 0x003AA595 File Offset: 0x003A8795
		public override bool HandleEvent(GetComponentAdjacentNavigationWeightEvent E)
		{
			E.MinWeight(8);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A12A RID: 41258 RVA: 0x003AA5A8 File Offset: 0x003A87A8
		protected override bool DoDetonate(Cell C, GameObject Actor = null, GameObject ApparentTarget = null, bool Indirect = false)
		{
			base.DidX("explode", null, "!", null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			this.ParentObject.FireEvent(Event.New("Detonated", "Owner", Actor));
			base.PlayWorldSound(base.GetPropertyOrTag("DetonatedSound", "Sounds/Grenade/sfx_grenade_highExplosive_explode"), 1f, 0f, true, null, 0f, 1f, 1f, int.MaxValue);
			List<Cell> list = new List<Cell>((this.Radius + 2) * (this.Radius + 2));
			C.GetAdjacentCells(this.Radius, list, false, true);
			TextConsole textConsole = Look._TextConsole;
			ScreenBuffer scrapBuffer = TextConsole.ScrapBuffer;
			XRLCore.Core.RenderMapToBuffer(scrapBuffer);
			bool flag = false;
			int phase = this.ParentObject.GetPhase();
			foreach (Cell cell in list)
			{
				cell.TemperatureChange(this.TemperatureDelta, Actor, false, false, false, false, phase);
				if (cell.IsVisible())
				{
					if (this.TemperatureDelta > 0)
					{
						scrapBuffer.Goto(cell);
						int num = Stat.Random(1, 3);
						if (num == 1)
						{
							scrapBuffer.Write("&R*", true, false, false, null, -1);
						}
						else if (num == 2)
						{
							scrapBuffer.Write("&W*", true, false, false, null, -1);
						}
						else
						{
							scrapBuffer.Write("&r*", true, false, false, null, -1);
						}
					}
					else
					{
						scrapBuffer.Goto(cell);
						int num2 = Stat.Random(1, 3);
						if (num2 == 1)
						{
							scrapBuffer.Write("&C*", true, false, false, null, -1);
						}
						else if (num2 == 2)
						{
							scrapBuffer.Write("&Y*", true, false, false, null, -1);
						}
						else
						{
							scrapBuffer.Write("&c*", true, false, false, null, -1);
						}
					}
					GameObject firstObject = cell.GetFirstObject();
					if (firstObject != null)
					{
						if (this.TemperatureDelta > 0)
						{
							for (int i = 0; i < 5; i++)
							{
								firstObject.ParticleText("&r" + ((char)(219 + Stat.Random(0, 4))).ToString(), 4.9f, 5);
							}
							for (int j = 0; j < 5; j++)
							{
								firstObject.ParticleText("&R" + ((char)(219 + Stat.Random(0, 4))).ToString(), 4.9f, 5);
							}
							for (int k = 0; k < 5; k++)
							{
								firstObject.ParticleText("&W" + ((char)(219 + Stat.Random(0, 4))).ToString(), 4.9f, 5);
							}
						}
						else
						{
							for (int l = 0; l < 5; l++)
							{
								firstObject.ParticleText("&Y" + ((char)(219 + Stat.Random(0, 4))).ToString(), 4.9f, 5);
							}
							for (int m = 0; m < 5; m++)
							{
								firstObject.ParticleText("&C" + ((char)(219 + Stat.Random(0, 4))).ToString(), 4.9f, 5);
							}
							for (int n = 0; n < 5; n++)
							{
								firstObject.ParticleText("&c" + ((char)(219 + Stat.Random(0, 4))).ToString(), 4.9f, 5);
							}
						}
					}
					flag = true;
				}
			}
			if (flag)
			{
				textConsole.DrawBuffer(scrapBuffer, null, false);
				Thread.Sleep(20);
			}
			this.ParentObject.Destroy(null, true, false, null);
			return true;
		}

		// Token: 0x040042ED RID: 17133
		public int Radius = 1;

		// Token: 0x040042EE RID: 17134
		public int TemperatureDelta = 1000;
	}
}
