using System;
using System.Collections.Generic;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000ED6 RID: 3798
	[Serializable]
	public class LiquidProximityTriggersEffect : IPart
	{
		// Token: 0x0600980F RID: 38927 RVA: 0x00373AB4 File Offset: 0x00371CB4
		public override bool SameAs(IPart p)
		{
			LiquidProximityTriggersEffect liquidProximityTriggersEffect = p as LiquidProximityTriggersEffect;
			return !(liquidProximityTriggersEffect.LiquidID != this.LiquidID) && liquidProximityTriggersEffect.Range == this.Range && liquidProximityTriggersEffect.LiquidMustBeConnectedByLiquid == this.LiquidMustBeConnectedByLiquid && !(liquidProximityTriggersEffect.EffectName != this.EffectName) && !(liquidProximityTriggersEffect.Duration != this.Duration) && liquidProximityTriggersEffect.Stack == this.Stack && base.SameAs(p);
		}

		// Token: 0x06009810 RID: 38928 RVA: 0x00373B3E File Offset: 0x00371D3E
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x06009811 RID: 38929 RVA: 0x00373B41 File Offset: 0x00371D41
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.CheckForEffectTrigger();
		}

		// Token: 0x06009812 RID: 38930 RVA: 0x00373B4A File Offset: 0x00371D4A
		public bool CheckForEffectTrigger()
		{
			if (!this.Stack && this.ParentObject.HasEffect(this.EffectName))
			{
				return false;
			}
			if (!this.IsLiquidProximate())
			{
				return false;
			}
			this.TriggerEffect();
			return true;
		}

		// Token: 0x06009813 RID: 38931 RVA: 0x00373B7C File Offset: 0x00371D7C
		public bool IsLiquidProximate()
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null)
			{
				return false;
			}
			GameObject gameObject = currentCell.GetOpenLiquidVolume();
			if (this.LiquidMustBeConnectedByLiquid && gameObject == null)
			{
				return false;
			}
			if (gameObject != null)
			{
				LiquidVolume liquidVolume = gameObject.LiquidVolume;
				bool? flag = ((liquidVolume != null) ? new bool?(liquidVolume.ContainsLiquid(this.LiquidID)) : null);
				bool flag2 = true;
				if ((flag.GetValueOrDefault() == flag2) & (flag != null))
				{
					return true;
				}
			}
			int i = 0;
			int count = currentCell.Objects.Count;
			while (i < count)
			{
				LiquidCovered effect = currentCell.Objects[i].GetEffect<LiquidCovered>();
				if (effect != null)
				{
					LiquidVolume liquid = effect.Liquid;
					if (liquid != null && liquid.ContainsLiquid(this.LiquidID))
					{
						return true;
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
								return true;
							}
						}
						int l = 0;
						int count3 = cell.Objects.Count;
						while (l < count3)
						{
							LiquidCovered effect2 = cell.Objects[l].GetEffect<LiquidCovered>();
							if (effect2 != null)
							{
								LiquidVolume liquid2 = effect2.Liquid;
								if (liquid2 != null && liquid2.ContainsLiquid(this.LiquidID))
								{
									return true;
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
			return false;
		}

		// Token: 0x06009814 RID: 38932 RVA: 0x00373DE0 File Offset: 0x00371FE0
		public bool TriggerEffect()
		{
			Type type = ModManager.ResolveType("XRL.World.Effects." + this.EffectName, false, false, true);
			if (type == null)
			{
				MetricsManager.LogError("tried to trigger nonexistent effect " + this.EffectName);
				return false;
			}
			Effect effect = Activator.CreateInstance(type) as Effect;
			if (effect == null)
			{
				MetricsManager.LogError("failed to create instance of effect " + this.EffectName);
				return false;
			}
			effect.Duration = this.Duration.RollCached();
			return this.ParentObject.ApplyEffect(effect, null);
		}

		// Token: 0x04003F50 RID: 16208
		public string LiquidID = "blood";

		// Token: 0x04003F51 RID: 16209
		public int Range = 10;

		// Token: 0x04003F52 RID: 16210
		public bool LiquidMustBeConnectedByLiquid = true;

		// Token: 0x04003F53 RID: 16211
		public string EffectName = "Frenzied";

		// Token: 0x04003F54 RID: 16212
		public string Duration = "20";

		// Token: 0x04003F55 RID: 16213
		public bool Stack;
	}
}
