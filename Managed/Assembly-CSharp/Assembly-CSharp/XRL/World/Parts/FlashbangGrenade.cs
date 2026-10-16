using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ConsoleLib.Console;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000FD1 RID: 4049
	[Serializable]
	public class FlashbangGrenade : IGrenade
	{
		// Token: 0x0600A0F3 RID: 41203 RVA: 0x003A9048 File Offset: 0x003A7248
		public override bool SameAs(IPart p)
		{
			FlashbangGrenade flashbangGrenade = p as FlashbangGrenade;
			return flashbangGrenade.Radius == this.Radius && !(flashbangGrenade.Duration != this.Duration) && base.SameAs(p);
		}

		// Token: 0x0600A0F4 RID: 41204 RVA: 0x003A9088 File Offset: 0x003A7288
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetComponentNavigationWeightEvent.ID || ID == GetComponentAdjacentNavigationWeightEvent.ID;
		}

		// Token: 0x0600A0F5 RID: 41205 RVA: 0x003A90A6 File Offset: 0x003A72A6
		public override bool HandleEvent(GetComponentNavigationWeightEvent E)
		{
			E.MinWeight(2);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A0F6 RID: 41206 RVA: 0x003A90B6 File Offset: 0x003A72B6
		public override bool HandleEvent(GetComponentAdjacentNavigationWeightEvent E)
		{
			E.MinWeight(2);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A0F7 RID: 41207 RVA: 0x003A90C8 File Offset: 0x003A72C8
		protected override bool DoDetonate(Cell C, GameObject Actor = null, GameObject ApparentTarget = null, bool Indirect = false)
		{
			base.DidX("explode", "with a flash", "!", null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			base.PlayWorldSound(base.GetPropertyOrTag("DetonatedSound", "Sounds/Grenade/sfx_grenade_flashbang_explode"), 1f, 0f, true, null, 0f, 1f, 1f, int.MaxValue);
			FlashbangGrenade.Flashbang(C, this.Radius, this.Duration, true, this.ParentObject.GetPhase());
			this.ParentObject.Destroy(null, true, false, null);
			return true;
		}

		// Token: 0x0600A0F8 RID: 41208 RVA: 0x003A9160 File Offset: 0x003A7360
		public static void Flashbang(Cell C, int Radius, string Duration, bool IncludeBaseCell, int Phase)
		{
			TextConsole textConsole = Look._TextConsole;
			ScreenBuffer scrapBuffer = TextConsole.ScrapBuffer;
			XRLCore.Core.RenderMapToBuffer(scrapBuffer);
			List<Cell> list = new List<Cell>();
			C.GetRealAdjacentCells(Radius, list, false);
			foreach (Cell cell in list)
			{
				if (cell != C || IncludeBaseCell)
				{
					foreach (GameObject gameObject in cell.GetObjectsWithPart("Brain"))
					{
						int num = Duration.RollCached();
						if (!gameObject.PhaseMatches(Phase))
						{
							num = num * 2 / 3;
						}
						gameObject.FireEvent("FlashbangHit");
						if (gameObject.HasRegisteredEvent("ModifyFlashbang"))
						{
							FlashbangGrenade.eModifyFlashbang.SetParameter("Amount", num);
							gameObject.FireEvent(FlashbangGrenade.eModifyFlashbang);
							int intParameter = FlashbangGrenade.eModifyFlashbang.GetIntParameter("Amount", 0);
							if (intParameter < num)
							{
								if (intParameter <= 0)
								{
									continue;
								}
								num = intParameter;
							}
						}
						if (gameObject.FireEvent("ApplyAttackConfusion"))
						{
							gameObject.ApplyEffect(new Confused(num, Radius * 5, 7, "SensoryConfusion"), null);
						}
					}
				}
			}
			if (C.ParentZone.IsActive())
			{
				bool flag = false;
				StringBuilder stringBuilder = Event.NewStringBuilder(null);
				for (int i = 0; i < Radius; i++)
				{
					XRLCore.Core.RenderMapToBuffer(scrapBuffer);
					foreach (Cell cell2 in list)
					{
						if (cell2.ParentZone == C.ParentZone && cell2.IsVisible())
						{
							flag = true;
							scrapBuffer.Goto(cell2.X, cell2.Y);
							if (30.in100())
							{
								scrapBuffer.Write("&K ", true, false, false, null, -1);
							}
							else
							{
								int num2 = Stat.Random(1, 3);
								char c;
								if (num2 == 1)
								{
									c = '*';
								}
								else if (num2 == 2)
								{
									c = ' ';
								}
								else
								{
									c = '.';
								}
								scrapBuffer.Write(stringBuilder.Clear().Append('&').Append(Phase.getRandomFlashColor(Phase))
									.Append("^Y")
									.Append(c)
									.ToString(), true, false, false, null, -1);
							}
						}
					}
					if (flag)
					{
						textConsole.DrawBuffer(scrapBuffer, null, false);
						Thread.Sleep(25);
					}
				}
			}
		}

		// Token: 0x040042DC RID: 17116
		public int Radius = 4;

		// Token: 0x040042DD RID: 17117
		public string Duration = "1d2+4";

		// Token: 0x040042DE RID: 17118
		[NonSerialized]
		public static Event eModifyFlashbang = new Event("ModifyFlashbang", "Amount", 0);
	}
}
