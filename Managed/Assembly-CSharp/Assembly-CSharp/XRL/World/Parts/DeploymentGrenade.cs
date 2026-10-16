using System;
using System.Collections.Generic;
using XRL.World.AI;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000FCF RID: 4047
	[Serializable]
	public class DeploymentGrenade : IGrenade
	{
		// Token: 0x0600A0E4 RID: 41188 RVA: 0x003A8830 File Offset: 0x003A6A30
		public override bool SameAs(IPart p)
		{
			DeploymentGrenade deploymentGrenade = p as DeploymentGrenade;
			return deploymentGrenade.Radius == this.Radius && deploymentGrenade.Chance == this.Chance && deploymentGrenade.AtLeast == this.AtLeast && !(deploymentGrenade.Count != this.Count) && !(deploymentGrenade.Duration != this.Duration) && !(deploymentGrenade.Blueprint != this.Blueprint) && !(deploymentGrenade.UsabilityEvent != this.UsabilityEvent) && !(deploymentGrenade.AccessibilityEvent != this.AccessibilityEvent) && !(deploymentGrenade.ActivationVerb != this.ActivationVerb) && !(deploymentGrenade.PhaseDuration != this.PhaseDuration) && !(deploymentGrenade.OmniphaseDuration != this.OmniphaseDuration) && deploymentGrenade.RealRadius == this.RealRadius && deploymentGrenade.BlockedBySolid == this.BlockedBySolid && deploymentGrenade.BlockedByNonEmpty == this.BlockedByNonEmpty && deploymentGrenade.Seeping == this.Seeping && deploymentGrenade.DustPuff == this.DustPuff && deploymentGrenade.DustPuffEach == this.DustPuffEach && deploymentGrenade.NoXPValue == this.NoXPValue && deploymentGrenade.LoyalToThrower == this.LoyalToThrower && deploymentGrenade.TriflingCompanion == this.TriflingCompanion && deploymentGrenade.IsRealityDistortionBased == this.IsRealityDistortionBased && deploymentGrenade.RealityStabilizationPenetration == this.RealityStabilizationPenetration && base.SameAs(p);
		}

		// Token: 0x0600A0E5 RID: 41189 RVA: 0x003A89D3 File Offset: 0x003A6BD3
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetComponentNavigationWeightEvent.ID;
		}

		// Token: 0x0600A0E6 RID: 41190 RVA: 0x003A89E9 File Offset: 0x003A6BE9
		public override bool HandleEvent(GetComponentNavigationWeightEvent E)
		{
			E.MinWeight(2);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A0E7 RID: 41191 RVA: 0x003A89FC File Offset: 0x003A6BFC
		private bool CanDeploy(Cell Cell, Cell BaseCell, Event Check, Dictionary<int, bool> Track, GameObject Actor, int Phase)
		{
			if (!this.Chance.in100())
			{
				return false;
			}
			if (this.RealRadius && Cell != BaseCell && Cell.RealDistanceTo(BaseCell, true) > (double)this.Radius)
			{
				return false;
			}
			if (this.BlockedBySolid && Cell.IsSolid(this.Seeping, Phase))
			{
				return false;
			}
			if (this.BlockedByNonEmpty && !Cell.IsEmptyForPopulation())
			{
				return false;
			}
			if (Track != null && Track.ContainsKey(Cell.LocalCoordKey))
			{
				return false;
			}
			if (Check != null && !Cell.FireEvent(Check))
			{
				return false;
			}
			if (this.IsRealityDistortionBased)
			{
				IComponent<GameObject>.CheckRealityDistortionAccessibility(this.ParentObject, Cell, Actor, this.ParentObject, null, this.RealityStabilizationPenetration);
			}
			return true;
		}

		// Token: 0x0600A0E8 RID: 41192 RVA: 0x003A8AAC File Offset: 0x003A6CAC
		private void Deploy(Cell Cell, GameObject Object, Dictionary<int, bool> Track, GameObject Actor, int Phase)
		{
			if (!this.Duration.IsNullOrEmpty())
			{
				Object.AddPart<Temporary>(new Temporary(this.Duration.RollCached()), true, false);
			}
			if (Phase == 2)
			{
				Object.ForceApplyEffect(new Phased(this.PhaseDuration.IsNullOrEmpty() ? 9999 : this.PhaseDuration.RollCached()), null);
			}
			else if (Phase == 3)
			{
				Object.ForceApplyEffect(new Omniphase(this.OmniphaseDuration.IsNullOrEmpty() ? 9999 : this.OmniphaseDuration.RollCached()), null);
			}
			Cell.AddObject(Object, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			Object.MakeActive(false);
			if (this.NoXPValue && Object.HasStat("XPValue"))
			{
				Object.GetStat("XPValue").BaseValue = 0;
			}
			if (this.LoyalToThrower && Actor != null)
			{
				Object.SetAlliedLeader<AllyConstructed>(Actor, 0, false);
				Object.IsTrifling = this.TriflingCompanion;
			}
			if (this.DustPuffEach)
			{
				Object.DustPuff("&y", 15);
			}
			if (Track != null)
			{
				Track[Cell.LocalCoordKey] = true;
			}
		}

		// Token: 0x0600A0E9 RID: 41193 RVA: 0x003A8BD0 File Offset: 0x003A6DD0
		private void Deploy(Cell Cell, Dictionary<int, bool> Track, GameObject Actor, int Phase)
		{
			GameObjectFactory.ProcessSpecification(this.Blueprint, delegate(GameObject obj)
			{
				this.Deploy(Cell, obj, Track, Actor, Phase);
			}, null, 1, 0, 0, null, "DeploymentGrenade", null, null, null, null);
		}

		// Token: 0x0600A0EA RID: 41194 RVA: 0x003A8C2C File Offset: 0x003A6E2C
		private void Deploy(List<Cell> Cells, Cell BaseCell, Event Check, Dictionary<int, bool> Track, GameObject Actor, int Phase)
		{
			GameObjectFactory.ProcessSpecification(this.Blueprint, delegate(GameObject obj)
			{
				int num = 0;
				Cell cell;
				do
				{
					cell = Cells.GetRandomElement<Cell>(null);
					if (cell == null)
					{
						break;
					}
					if (!this.CanDeploy(cell, BaseCell, Check, Track, Actor, Phase))
					{
						cell = null;
					}
				}
				while (cell == null && ++num < 10);
				if (cell != null)
				{
					this.Deploy(cell, obj, Track, Actor, Phase);
					Cells.Remove(cell);
					return;
				}
				obj.Obliterate(null, false, null);
			}, null, 1, 0, 0, null, "DeploymentGrenade", null, null, null, null);
		}

		// Token: 0x0600A0EB RID: 41195 RVA: 0x003A8C98 File Offset: 0x003A6E98
		protected override bool DoDetonate(Cell BaseCell, GameObject Actor = null, GameObject ApparentTarget = null, bool Indirect = false)
		{
			if (!this.UsabilityEvent.IsNullOrEmpty() && !this.ParentObject.FireEvent(this.UsabilityEvent))
			{
				return false;
			}
			if (this.IsRealityDistortionBased)
			{
				GameObject parentObject = this.ParentObject;
				Cell cell = null;
				GameObject parentObject2 = this.ParentObject;
				IPart part = null;
				int realityStabilizationPenetration = this.RealityStabilizationPenetration;
				if (!IComponent<GameObject>.CheckRealityDistortionUsability(parentObject, cell, Actor, parentObject2, part, null, realityStabilizationPenetration))
				{
					return false;
				}
			}
			base.PlayWorldSound(base.GetPropertyOrTag("DetonatedSound", null), 1f, 0f, true, null, 0f, 1f, 1f, int.MaxValue);
			base.DidX(this.ActivationVerb, null, "!", null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			int phase = this.ParentObject.GetPhase();
			Event @event = ((!this.AccessibilityEvent.IsNullOrEmpty()) ? Event.New(this.AccessibilityEvent, 0, 0, 0) : null);
			Dictionary<int, bool> dictionary = ((this.AtLeast > 0 || !this.Count.IsNullOrEmpty()) ? new Dictionary<int, bool>() : null);
			List<Cell> localAdjacentCells = BaseCell.GetLocalAdjacentCells(this.Radius, false);
			if (this.Count.IsNullOrEmpty())
			{
				int num = 0;
				do
				{
					if (num == 1)
					{
						localAdjacentCells.ShuffleInPlace<Cell>(null);
					}
					if (this.CanDeploy(BaseCell, BaseCell, @event, dictionary, Actor, phase))
					{
						this.Deploy(BaseCell, dictionary, Actor, phase);
					}
					foreach (Cell cell2 in localAdjacentCells)
					{
						if (num > 0 && dictionary.Count >= this.AtLeast)
						{
							break;
						}
						if (this.CanDeploy(cell2, BaseCell, @event, dictionary, Actor, phase))
						{
							this.Deploy(cell2, dictionary, Actor, phase);
						}
					}
					if (dictionary == null || dictionary.Count >= this.AtLeast)
					{
						break;
					}
				}
				while (++num < 10);
			}
			else
			{
				int num2 = this.Count.RollCached();
				for (int i = 0; i < num2; i++)
				{
					this.Deploy(localAdjacentCells, BaseCell, @event, dictionary, Actor, phase);
				}
			}
			if (this.DustPuff)
			{
				this.ParentObject.DustPuff("&y", 15);
			}
			this.ParentObject.Destroy(null, true, false, null);
			return true;
		}

		// Token: 0x040042C4 RID: 17092
		public int Radius = 1;

		// Token: 0x040042C5 RID: 17093
		public int Chance = 100;

		// Token: 0x040042C6 RID: 17094
		public int AtLeast;

		// Token: 0x040042C7 RID: 17095
		public string Count;

		// Token: 0x040042C8 RID: 17096
		public string Duration;

		// Token: 0x040042C9 RID: 17097
		public string Blueprint = "Forcefield";

		// Token: 0x040042CA RID: 17098
		public string UsabilityEvent;

		// Token: 0x040042CB RID: 17099
		public string AccessibilityEvent;

		// Token: 0x040042CC RID: 17100
		public string ActivationVerb = "detonate";

		// Token: 0x040042CD RID: 17101
		public string PhaseDuration;

		// Token: 0x040042CE RID: 17102
		public string OmniphaseDuration;

		// Token: 0x040042CF RID: 17103
		public bool RealRadius;

		// Token: 0x040042D0 RID: 17104
		public bool BlockedBySolid = true;

		// Token: 0x040042D1 RID: 17105
		public bool BlockedByNonEmpty = true;

		// Token: 0x040042D2 RID: 17106
		public bool Seeping;

		// Token: 0x040042D3 RID: 17107
		public bool DustPuff = true;

		// Token: 0x040042D4 RID: 17108
		public bool DustPuffEach;

		// Token: 0x040042D5 RID: 17109
		public bool NoXPValue = true;

		// Token: 0x040042D6 RID: 17110
		public bool LoyalToThrower;

		// Token: 0x040042D7 RID: 17111
		public bool TriflingCompanion = true;

		// Token: 0x040042D8 RID: 17112
		public bool IsRealityDistortionBased;

		// Token: 0x040042D9 RID: 17113
		public int RealityStabilizationPenetration;
	}
}
