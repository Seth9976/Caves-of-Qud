using System;
using System.Collections.Generic;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x02000FD3 RID: 4051
	[Serializable]
	public class GravityGrenade : IGrenade
	{
		// Token: 0x0600A101 RID: 41217 RVA: 0x003A9694 File Offset: 0x003A7894
		public override bool SameAs(IPart p)
		{
			GravityGrenade gravityGrenade = p as GravityGrenade;
			return gravityGrenade.Force == this.Force && gravityGrenade.Radius == this.Radius && gravityGrenade.ForceDropoff == this.ForceDropoff && gravityGrenade.RealityDistortionBased == this.RealityDistortionBased && base.SameAs(p);
		}

		// Token: 0x0600A102 RID: 41218 RVA: 0x003A96EF File Offset: 0x003A78EF
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetComponentNavigationWeightEvent.ID || ID == GetComponentAdjacentNavigationWeightEvent.ID;
		}

		// Token: 0x0600A103 RID: 41219 RVA: 0x003A970D File Offset: 0x003A790D
		public override bool HandleEvent(GetComponentNavigationWeightEvent E)
		{
			E.MinWeight(5);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A104 RID: 41220 RVA: 0x003A971D File Offset: 0x003A791D
		public override bool HandleEvent(GetComponentAdjacentNavigationWeightEvent E)
		{
			E.MinWeight(5);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A105 RID: 41221 RVA: 0x003A9730 File Offset: 0x003A7930
		protected override bool DoDetonate(Cell Cell, GameObject Actor = null, GameObject ApparentTarget = null, bool Indirect = false)
		{
			if (this.RealityDistortionBased && !IComponent<GameObject>.CheckRealityDistortionUsability(this.ParentObject, null, null, this.ParentObject, null, null, 0))
			{
				return false;
			}
			int phase = this.ParentObject.GetPhase();
			base.PlayWorldSound(base.GetPropertyOrTag("DetonatedSound", null), 1f, 0f, true, null, 0f, 1f, 1f, int.MaxValue);
			base.DidX("implode", null, "!", null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			this.ParentObject.Destroy(null, true, false, null);
			Cell.ImplosionSplat(this.Radius);
			List<Cell> list = new List<Cell>();
			list.Add(Cell);
			List<GameObject> list2 = Event.NewGameObjectList();
			int num = this.Force;
			int num2 = 1;
			while (num2 <= this.Radius && num > 0)
			{
				List<Cell> list3 = Cell.GetRealAdjacentCells(num2, true).ShuffleInPlace<Cell>(null);
				int i = 0;
				int count = list3.Count;
				while (i < count)
				{
					Cell cell = list3[i];
					if (cell.Objects.Count > 0 && !list.Contains(cell))
					{
						list2.AddRange(cell.Objects);
					}
					i++;
				}
				list.AddRange(list3);
				list2.ShuffleInPlace<GameObject>(null);
				int j = 0;
				int count2 = list2.Count;
				while (j < count2)
				{
					GameObject gameObject = list2[j];
					if (gameObject.CurrentCell != null && GameObject.Validate(gameObject) && list3.Contains(gameObject.CurrentCell) && gameObject.PhaseMatches(phase) && (!this.RealityDistortionBased || IComponent<GameObject>.CheckRealityDistortionAccessibility(gameObject, null, null, this.ParentObject, null, 0)))
					{
						if (gameObject.IsFlying)
						{
							Flight.Fall(gameObject);
						}
						gameObject.Accelerate(num, null, Cell, null, "Artificial Gravitation", Actor, false, ApparentTarget, null, 0.1, true, false, false, true, false, false);
					}
					j++;
				}
				list2.Clear();
				num -= this.ForceDropoff;
				num2++;
			}
			return true;
		}

		// Token: 0x040042E2 RID: 17122
		public int Force = 1000;

		// Token: 0x040042E3 RID: 17123
		public int Radius = 2;

		// Token: 0x040042E4 RID: 17124
		public int ForceDropoff = 500;

		// Token: 0x040042E5 RID: 17125
		public bool RealityDistortionBased = true;
	}
}
