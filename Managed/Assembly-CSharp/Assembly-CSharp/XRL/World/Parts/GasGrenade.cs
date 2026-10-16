using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000FD2 RID: 4050
	[Serializable]
	public class GasGrenade : IGrenade
	{
		// Token: 0x0600A0FB RID: 41211 RVA: 0x003A9460 File Offset: 0x003A7660
		public override bool SameAs(IPart p)
		{
			GasGrenade gasGrenade = p as GasGrenade;
			return !(gasGrenade.GasObject != this.GasObject) && gasGrenade.Density == this.Density && gasGrenade.Level == this.Level && base.SameAs(p);
		}

		// Token: 0x0600A0FC RID: 41212 RVA: 0x003A94B0 File Offset: 0x003A76B0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetComponentNavigationWeightEvent.ID || ID == GetComponentAdjacentNavigationWeightEvent.ID;
		}

		// Token: 0x0600A0FD RID: 41213 RVA: 0x003A94CE File Offset: 0x003A76CE
		public override bool HandleEvent(GetComponentNavigationWeightEvent E)
		{
			E.MinWeight(5);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A0FE RID: 41214 RVA: 0x003A94DE File Offset: 0x003A76DE
		public override bool HandleEvent(GetComponentAdjacentNavigationWeightEvent E)
		{
			E.MinWeight(5);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A0FF RID: 41215 RVA: 0x003A94F0 File Offset: 0x003A76F0
		protected override bool DoDetonate(Cell C, GameObject Actor = null, GameObject ApparentTarget = null, bool Indirect = false)
		{
			base.PlayWorldSound(base.GetPropertyOrTag("DetonatedSound", null), 1f, 0f, true, null, 0f, 1f, 1f, int.MaxValue);
			List<Cell> adjacentCells = C.GetAdjacentCells(true);
			adjacentCells.Add(C);
			bool flag = this.ParentObject.HasEffect<Phased>();
			bool flag2 = this.ParentObject.HasEffect<Omniphase>();
			Event @event = Event.New("CreatorModifyGas", "Gas", null);
			foreach (Cell cell in adjacentCells)
			{
				GameObject gameObject = GameObject.Create(this.GasObject);
				Gas part = gameObject.GetPart<Gas>();
				part.Creator = Actor;
				part.Density = this.Density;
				part.Level = this.Level;
				if (flag)
				{
					gameObject.ForceApplyEffect(new Phased(Stat.Random(23, 32)), null);
				}
				if (flag2)
				{
					gameObject.ForceApplyEffect(new Omniphase(Stat.Random(46, 64)), null);
				}
				@event.SetParameter("Gas", part);
				if (Actor != null)
				{
					Actor.FireEvent(@event);
				}
				cell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			}
			base.DidX("explode", null, "!", null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			this.ParentObject.Destroy(null, true, false, null);
			return true;
		}

		// Token: 0x040042DF RID: 17119
		public string GasObject = "PoisonGas";

		// Token: 0x040042E0 RID: 17120
		public int Density = 20;

		// Token: 0x040042E1 RID: 17121
		public int Level = 1;
	}
}
