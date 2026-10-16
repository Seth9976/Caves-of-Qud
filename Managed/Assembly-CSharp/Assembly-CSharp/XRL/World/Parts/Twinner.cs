using System;
using System.Collections.Generic;
using Genkit;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000F10 RID: 3856
	[Serializable]
	public class Twinner : IPart
	{
		// Token: 0x060099B5 RID: 39349 RVA: 0x0037D622 File Offset: 0x0037B822
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x060099B6 RID: 39350 RVA: 0x0037D625 File Offset: 0x0037B825
		public override IPart DeepCopy(GameObject Parent)
		{
			return new Twinner
			{
				ParentObject = Parent
			};
		}

		// Token: 0x060099B7 RID: 39351 RVA: 0x0037D633 File Offset: 0x0037B833
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID || ID == PooledEvent<IsSensableAsPsychicEvent>.ID || ID == OnDestroyObjectEvent.ID;
		}

		// Token: 0x060099B8 RID: 39352 RVA: 0x0037D659 File Offset: 0x0037B859
		public override bool HandleEvent(IsSensableAsPsychicEvent E)
		{
			E.Sensable = true;
			return base.HandleEvent(E);
		}

		// Token: 0x060099B9 RID: 39353 RVA: 0x0037D669 File Offset: 0x0037B869
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.EnteredCell();
			return base.HandleEvent(E);
		}

		// Token: 0x060099BA RID: 39354 RVA: 0x0037D678 File Offset: 0x0037B878
		public override bool HandleEvent(OnDestroyObjectEvent E)
		{
			this.OnDestroyObject();
			return base.HandleEvent(E);
		}

		// Token: 0x060099BB RID: 39355 RVA: 0x0037D687 File Offset: 0x0037B887
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AITakingAction");
			base.Register(Object, Registrar);
		}

		// Token: 0x060099BC RID: 39356 RVA: 0x0037D69C File Offset: 0x0037B89C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AITakingAction" && GameObject.Validate(this.ParentObject))
			{
				this.Act();
			}
			return base.FireEvent(E);
		}

		// Token: 0x060099BD RID: 39357 RVA: 0x0037D6CC File Offset: 0x0037B8CC
		public List<Cell> GetFollowCells()
		{
			List<Cell> list = new List<Cell>();
			for (int i = 0; i < 8; i++)
			{
				Cell cellFromDirectionGlobal = this.ParentObject.CurrentCell.GetCellFromDirectionGlobal(Cell.DirectionList[i], true, true);
				if (cellFromDirectionGlobal != null && cellFromDirectionGlobal.IsPassable(null, true))
				{
					list.Add(cellFromDirectionGlobal);
				}
			}
			return list;
		}

		// Token: 0x060099BE RID: 39358 RVA: 0x0037D71C File Offset: 0x0037B91C
		public virtual void Act()
		{
			GameObject twin = this.Twin;
			if (((twin != null) ? twin.CurrentCell : null) != null)
			{
				return;
			}
			if (!base.CheckMyRealityDistortionAdvisability(null, null, null, null, null, null, 0))
			{
				return;
			}
			Cell randomElement = this.GetSpawnCells().GetRandomElement<Cell>(null);
			if (randomElement == null)
			{
				return;
			}
			Event @event = Event.New("InitiateRealityDistortionTransit", 0, 0, 0);
			@event.SetParameter("Object", this.ParentObject);
			@event.SetParameter("Mutation", this);
			@event.SetParameter("Cell", randomElement);
			if (!this.ParentObject.FireEvent(@event) || !randomElement.FireEvent(@event))
			{
				return;
			}
			this.Twin = this.ParentObject.DeepCopy(false, false, null);
			this.Twin.RemovePart<GivesRep>();
			this.Twin.GetPart<Twinner>().Twin = this.ParentObject;
			this.Spawn(this.Twin, randomElement);
		}

		// Token: 0x060099BF RID: 39359 RVA: 0x0037D7FC File Offset: 0x0037B9FC
		public List<Cell> GetSpawnCells()
		{
			List<Cell> list = Event.NewCellList();
			foreach (Cell cell in this.ParentObject.CurrentCell.GetLocalAdjacentCells())
			{
				if (cell.IsEmptyForPopulation() && !this.IsRealityStabilized(cell))
				{
					list.Add(cell);
				}
			}
			return list;
		}

		// Token: 0x060099C0 RID: 39360 RVA: 0x0037D874 File Offset: 0x0037BA74
		public virtual bool IsRealityStabilized(Cell Cell)
		{
			GameObject gameObject = null;
			GameObject parentObject = this.ParentObject;
			return !IComponent<GameObject>.CheckRealityDistortionAdvisability(gameObject, Cell, parentObject, null, this, null, 0);
		}

		// Token: 0x060099C1 RID: 39361 RVA: 0x0037D8A0 File Offset: 0x0037BAA0
		public void Spawn(GameObject obj, Cell cell)
		{
			obj.StripContents(true, true);
			obj.RestorePristineHealth(false, true);
			obj.StopFighting(this.ParentObject, false);
			obj.GetStat("XPValue").BaseValue = 0;
			this.ParentObject.StopFighting(obj, false);
			cell.AddObject(obj, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			obj.MakeActive(false);
			WasReplicatedEvent.Send(this.ParentObject, this.ParentObject, obj, "Twin", false);
			ReplicaCreatedEvent.Send(obj, this.ParentObject, this.ParentObject, "Twin", false);
			if (IComponent<GameObject>.Visible(obj))
			{
				for (int i = 0; i < 10; i++)
				{
					XRLCore.ParticleManager.AddRadial("&Mù", (float)cell.X, (float)cell.Y, (float)Stat.Random(0, 5), (float)Stat.Random(5, 10), 0.01f * (float)Stat.Random(4, 6), -0.05f * (float)Stat.Random(3, 7));
				}
			}
		}

		// Token: 0x060099C2 RID: 39362 RVA: 0x0037D998 File Offset: 0x0037BB98
		public virtual void EnteredCell()
		{
			GameObject twin = this.Twin;
			if (((twin != null) ? twin.CurrentCell : null) != null && this.ParentObject.CurrentCell.ParentZone != this.Twin.CurrentCell.ParentZone)
			{
				List<Cell> followCells = this.GetFollowCells();
				Cell cell = this.ParentObject.CurrentCell;
				if (followCells.Count > 0)
				{
					cell = ((followCells.Count > 1) ? followCells[Calc.R.Next(followCells.Count)] : followCells[0]);
				}
				this.Twin.Brain.Goals.Clear();
				this.Twin.SystemLongDistanceMoveTo(cell, null, false, true);
				this.Twin.UseEnergy(1000, "Move Join Leader", null, null, false);
			}
		}

		// Token: 0x060099C3 RID: 39363 RVA: 0x0037DA74 File Offset: 0x0037BC74
		public virtual void OnDestroyObject()
		{
			if (this.Twin != null && this.Twin.CurrentCell == null)
			{
				Twinner part = this.Twin.GetPart<Twinner>();
				if (part != null)
				{
					part.Twin = null;
				}
				this.Twin.Obliterate(null, false, null);
			}
		}

		// Token: 0x04004012 RID: 16402
		public GameObject Twin;
	}
}
