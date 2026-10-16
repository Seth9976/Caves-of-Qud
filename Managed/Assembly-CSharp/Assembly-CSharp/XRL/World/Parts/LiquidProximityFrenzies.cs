using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000ED5 RID: 3797
	[Serializable]
	public class LiquidProximityFrenzies : IPart
	{
		// Token: 0x06009808 RID: 38920 RVA: 0x00373608 File Offset: 0x00371808
		public override bool SameAs(IPart p)
		{
			LiquidProximityFrenzies liquidProximityFrenzies = p as LiquidProximityFrenzies;
			return !(liquidProximityFrenzies.LiquidID != this.LiquidID) && liquidProximityFrenzies.Range == this.Range && liquidProximityFrenzies.LiquidMustBeConnectedByLiquid == this.LiquidMustBeConnectedByLiquid && !(liquidProximityFrenzies.Duration != this.Duration) && liquidProximityFrenzies.Stack == this.Stack && liquidProximityFrenzies.QuicknessBonus == this.QuicknessBonus && liquidProximityFrenzies.MaxKillRadiusBonus == this.MaxKillRadiusBonus && liquidProximityFrenzies.BerserkDuration == this.BerserkDuration && liquidProximityFrenzies.BerserkImmediately == this.BerserkImmediately && liquidProximityFrenzies.BerserkOnDealDamage == this.BerserkOnDealDamage && liquidProximityFrenzies.PreferBleedingTarget == this.PreferBleedingTarget && base.SameAs(p);
		}

		// Token: 0x06009809 RID: 38921 RVA: 0x003736DD File Offset: 0x003718DD
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600980A RID: 38922 RVA: 0x003736E0 File Offset: 0x003718E0
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.CheckForEffectTrigger();
		}

		// Token: 0x0600980B RID: 38923 RVA: 0x003736EC File Offset: 0x003718EC
		public bool CheckForEffectTrigger()
		{
			if (!this.Stack && this.ParentObject.HasEffect<Frenzied>())
			{
				return false;
			}
			GameObject proximateLiquidPool = this.GetProximateLiquidPool();
			if (proximateLiquidPool == null)
			{
				return false;
			}
			this.TriggerEffect(proximateLiquidPool);
			return true;
		}

		// Token: 0x0600980C RID: 38924 RVA: 0x00373728 File Offset: 0x00371928
		public GameObject GetProximateLiquidPool()
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null)
			{
				return null;
			}
			GameObject gameObject = currentCell.GetOpenLiquidVolume();
			if (this.LiquidMustBeConnectedByLiquid && gameObject == null)
			{
				return null;
			}
			if (gameObject != null)
			{
				LiquidVolume liquidVolume = gameObject.LiquidVolume;
				bool? flag = ((liquidVolume != null) ? new bool?(liquidVolume.ContainsLiquid(this.LiquidID)) : null);
				bool flag2 = true;
				if ((flag.GetValueOrDefault() == flag2) & (flag != null))
				{
					return gameObject;
				}
			}
			int i = 0;
			int count = currentCell.Objects.Count;
			while (i < count)
			{
				GameObject gameObject2 = currentCell.Objects[i];
				LiquidCovered effect = gameObject2.GetEffect<LiquidCovered>();
				if (effect != null)
				{
					LiquidVolume liquid = effect.Liquid;
					if (liquid != null && liquid.ContainsLiquid(this.LiquidID))
					{
						return gameObject2;
					}
				}
				i++;
			}
			List<Cell> list = Event.NewCellList();
			List<Cell> list2 = Event.NewCellList();
			List<Cell> list3 = Event.NewCellList();
			list.Add(currentCell);
			list2.AddRange(currentCell.GetLocalAdjacentCells());
			for (int j = 1; j <= this.Range; j++)
			{
				list3.Clear();
				list3.AddRange(list2);
				list.AddRange(list3);
				list2.Clear();
				int k = 0;
				int count2 = list3.Count;
				while (k < count2)
				{
					Cell cell = list3[k];
					gameObject = cell.GetOpenLiquidVolume();
					if (!this.LiquidMustBeConnectedByLiquid || gameObject != null)
					{
						if (gameObject != null)
						{
							LiquidVolume liquidVolume2 = gameObject.LiquidVolume;
							bool? flag = ((liquidVolume2 != null) ? new bool?(liquidVolume2.ContainsLiquid(this.LiquidID)) : null);
							bool flag2 = true;
							if ((flag.GetValueOrDefault() == flag2) & (flag != null))
							{
								return gameObject;
							}
						}
						int l = 0;
						int count3 = cell.Objects.Count;
						while (l < count3)
						{
							GameObject gameObject3 = cell.Objects[l];
							LiquidCovered effect2 = gameObject3.GetEffect<LiquidCovered>();
							if (effect2 != null)
							{
								LiquidVolume liquid2 = effect2.Liquid;
								if (liquid2 != null && liquid2.ContainsLiquid(this.LiquidID))
								{
									return gameObject3;
								}
							}
							l++;
						}
						foreach (Cell cell2 in cell.GetLocalAdjacentCells())
						{
							if (!list.Contains(cell2) && !list2.Contains(cell2))
							{
								list2.Add(cell2);
							}
						}
					}
					k++;
				}
			}
			return null;
		}

		// Token: 0x0600980D RID: 38925 RVA: 0x00373994 File Offset: 0x00371B94
		public bool TriggerEffect(GameObject obj = null)
		{
			Frenzied frenzied = new Frenzied(this.Duration.RollCached(), this.QuicknessBonus, this.MaxKillRadiusBonus, this.BerserkDuration, this.BerserkImmediately, this.BerserkOnDealDamage, this.PreferBleedingTarget);
			if (!this.ParentObject.ApplyEffect(frenzied, null))
			{
				return false;
			}
			if (!this.Message.IsNullOrEmpty() && base.Visible())
			{
				string text = this.Message;
				if (text.Contains("=liquid="))
				{
					text = text.Replace("=liquid=", ColorUtility.StripFormatting(LiquidVolume.GetLiquid(this.LiquidID).Name));
				}
				text = GameText.VariableReplace(text, this.ParentObject, obj, false);
				if (!text.IsNullOrEmpty())
				{
					IComponent<GameObject>.AddPlayerMessage(text, null, true);
				}
			}
			return true;
		}

		// Token: 0x04003F44 RID: 16196
		public string LiquidID = "blood";

		// Token: 0x04003F45 RID: 16197
		public int Range = 10;

		// Token: 0x04003F46 RID: 16198
		public bool LiquidMustBeConnectedByLiquid = true;

		// Token: 0x04003F47 RID: 16199
		public string Duration = "10";

		// Token: 0x04003F48 RID: 16200
		public bool Stack;

		// Token: 0x04003F49 RID: 16201
		public int QuicknessBonus = 10;

		// Token: 0x04003F4A RID: 16202
		public int MaxKillRadiusBonus = 10;

		// Token: 0x04003F4B RID: 16203
		public int BerserkDuration = 5;

		// Token: 0x04003F4C RID: 16204
		public bool BerserkImmediately;

		// Token: 0x04003F4D RID: 16205
		public bool BerserkOnDealDamage;

		// Token: 0x04003F4E RID: 16206
		public bool PreferBleedingTarget;

		// Token: 0x04003F4F RID: 16207
		public string Message = "=subject.T==subject.directionIfAny= =verb:are= frenzied by the scent of =liquid=!";
	}
}
