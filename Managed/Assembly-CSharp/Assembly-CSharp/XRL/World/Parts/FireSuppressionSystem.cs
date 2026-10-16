using System;
using System.Collections.Generic;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000EB6 RID: 3766
	[Serializable]
	public class FireSuppressionSystem : IPoweredPart
	{
		// Token: 0x06009717 RID: 38679 RVA: 0x0036D260 File Offset: 0x0036B460
		public FireSuppressionSystem()
		{
			this.WorksOnSelf = true;
			this.ChargeUse = 0;
		}

		// Token: 0x06009718 RID: 38680 RVA: 0x0036D2A0 File Offset: 0x0036B4A0
		public override bool SameAs(IPart p)
		{
			FireSuppressionSystem fireSuppressionSystem = p as FireSuppressionSystem;
			return !(fireSuppressionSystem.Amount != this.Amount) && !(fireSuppressionSystem.Sound != this.Sound) && base.SameAs(p);
		}

		// Token: 0x06009719 RID: 38681 RVA: 0x0036D2E5 File Offset: 0x0036B4E5
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600971A RID: 38682 RVA: 0x0036D2E8 File Offset: 0x0036B4E8
		public override void TurnTick(long TimeTick, int Amount1)
		{
			this.CheckFireSuppression();
		}

		// Token: 0x0600971B RID: 38683 RVA: 0x0036D2F4 File Offset: 0x0036B4F4
		public int CheckFireSuppression()
		{
			int num = 0;
			if (this.ActivePartHasMultipleSubjects())
			{
				using (List<GameObject>.Enumerator enumerator = this.GetActivePartSubjects().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						GameObject gameObject = enumerator.Current;
						this.CheckFireSuppression(gameObject);
					}
					return num;
				}
			}
			this.CheckFireSuppression(this.GetActivePartFirstSubject());
			return num;
		}

		// Token: 0x0600971C RID: 38684 RVA: 0x0036D360 File Offset: 0x0036B560
		public bool CheckFireSuppression(GameObject obj)
		{
			if (GameObject.Validate(ref obj) && obj.IsAflame() && this.Chance.in100() && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				int num = this.Amount.RollCached();
				if (num > 0)
				{
					string name = LiquidVolume.GetLiquid(this.Liquid).GetName(null);
					if (obj.IsPlayer())
					{
						IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
						{
							num.ToString(),
							" ",
							(num == 1) ? "dram" : "drams",
							" of ",
							name,
							" discharges all over you."
						}), null, true);
					}
					else if (IComponent<GameObject>.Visible(obj))
					{
						IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
						{
							num.ToString(),
							" ",
							(num == 1) ? "dram" : "drams",
							" of ",
							name,
							" discharges all over ",
							obj.the,
							obj.ShortDisplayName,
							"."
						}), null, true);
					}
					obj.ApplyEffect(new LiquidCovered(this.Liquid, num, 1, false, null, false), null);
					obj.LiquidSplash(LiquidVolume.GetLiquid(this.Liquid));
					base.PlayWorldSound(this.Sound, 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
					return true;
				}
			}
			return false;
		}

		// Token: 0x04003EC8 RID: 16072
		public int Chance = 100;

		// Token: 0x04003EC9 RID: 16073
		public string Liquid = "gel";

		// Token: 0x04003ECA RID: 16074
		public string Amount = "2-4";

		// Token: 0x04003ECB RID: 16075
		public string Sound = "Sounds/Abilities/sfx_ability_thickLiquidSpray";
	}
}
