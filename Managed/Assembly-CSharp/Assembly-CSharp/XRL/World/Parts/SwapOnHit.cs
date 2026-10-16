using System;
using System.Collections.Generic;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x020010A8 RID: 4264
	[Serializable]
	public class SwapOnHit : IActivePart
	{
		// Token: 0x0600A9AF RID: 43439 RVA: 0x003E055C File Offset: 0x003DE75C
		public SwapOnHit()
		{
			this.IsRealityDistortionBased = true;
			this.WorksOnEquipper = true;
		}

		// Token: 0x0600A9B0 RID: 43440 RVA: 0x003E0584 File Offset: 0x003DE784
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<ShotCompleteEvent>.ID;
		}

		// Token: 0x0600A9B1 RID: 43441 RVA: 0x003E059A File Offset: 0x003DE79A
		public override bool HandleEvent(ShotCompleteEvent E)
		{
			this.OnShotComplete();
			this.Targets.Clear();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A9B2 RID: 43442 RVA: 0x003E05B4 File Offset: 0x003DE7B4
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600A9B3 RID: 43443 RVA: 0x003E05B7 File Offset: 0x003DE7B7
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponMissleWeaponFiring");
			Registrar.Register("WeaponMissileWeaponHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A9B4 RID: 43444 RVA: 0x003E05D8 File Offset: 0x003DE7D8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponMissileWeaponHit")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Defender");
				this.Targets[gameObjectParameter] = gameObjectParameter.CurrentCell;
			}
			else if (E.ID == "WeaponMissleWeaponFiring")
			{
				this.Targets.Clear();
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600A9B5 RID: 43445 RVA: 0x003E063C File Offset: 0x003DE83C
		public void OnShotComplete()
		{
			if (this.Targets.Count <= 0 || !base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				return;
			}
			GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
			if (activePartFirstSubject == null || !activePartFirstSubject.IsValid() || activePartFirstSubject.IsNowhere())
			{
				return;
			}
			Cell currentCell = activePartFirstSubject.CurrentCell;
			GameObject randomElement = this.Targets.Keys.GetRandomElement<GameObject>(null);
			Event @event = Event.New("InitiateRealityDistortionTransit", "Object", null, "Device", this.ParentObject, "Cell", null);
			foreach (KeyValuePair<GameObject, Cell> keyValuePair in this.Targets)
			{
				this.SwapPositions(activePartFirstSubject, currentCell, keyValuePair.Key, keyValuePair.Value, @event, keyValuePair.Key != randomElement);
			}
			if (!Achievement.BEAMSPLIT_SPACE_INVERTER.Achieved && activePartFirstSubject.IsPlayer() && this.Targets.Count > 1)
			{
				Achievement.BEAMSPLIT_SPACE_INVERTER.Unlock();
			}
		}

		// Token: 0x0600A9B6 RID: 43446 RVA: 0x003E076C File Offset: 0x003DE96C
		public Cell GetPassableCellFor(GameObject Object, Cell Target)
		{
			if (!Target.IsPassable(Object, true))
			{
				Cell closestPassableCellFor = Target.getClosestPassableCellFor(Object);
				if (closestPassableCellFor != null)
				{
					return closestPassableCellFor;
				}
			}
			return Target;
		}

		// Token: 0x0600A9B7 RID: 43447 RVA: 0x003E0794 File Offset: 0x003DE994
		public void SwapPositions(GameObject Object, Cell ObjectCell, GameObject Target, Cell TargetCell, Event E, bool Clone = false)
		{
			if (Target == null || TargetCell == null)
			{
				return;
			}
			bool flag = Target.IsValid() && !Target.IsNowhere() && Physics.IsMoveable(Target, true);
			E.SetParameter("Object", Target);
			E.SetParameter("Cell", ObjectCell);
			if (flag && (!Target.FireEvent(E) || !TargetCell.RemoveObject(Target, false, false, false, false, false, true, true, null, null, null, null, null, null, null)))
			{
				return;
			}
			if (Clone)
			{
				Cell passableCellFor = this.GetPassableCellFor(Object, TargetCell);
				TemporalFugue.CreateFugueCopyOf(Object, Object, passableCellFor, this.ParentObject, true, this.CopyDuration, this.HostileCopyChance, "space inverter", this.FriendlyCopyColorString, this.HostileCopyColorString, this.FriendlyCopyPrefix, this.HostileCopyPrefix, null);
			}
			else
			{
				E.SetParameter("Object", Object);
				E.SetParameter("Cell", TargetCell);
				if (!Object.FireEvent(E) || !ObjectCell.RemoveObject(Object, false, false, false, false, false, true, true, null, null, null, null, null, null, null))
				{
					if (flag)
					{
						TargetCell.AddObject(Target, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
					return;
				}
				this.GetPassableCellFor(Object, TargetCell).AddObject(Object, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				Object.SmallTeleportSwirl(null, "&C", false, null, false);
				if (Object.IsPlayer() || Target.IsPlayer())
				{
					IComponent<GameObject>.XDidYToZ(Object, "swap", "positions with", Target, null, "!", null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
				}
			}
			if (flag)
			{
				this.GetPassableCellFor(Target, ObjectCell).AddObject(Target, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			}
		}

		// Token: 0x040045F5 RID: 17909
		public int CopyDuration = 5;

		// Token: 0x040045F6 RID: 17910
		public int HostileCopyChance;

		// Token: 0x040045F7 RID: 17911
		public string HostileCopyColorString;

		// Token: 0x040045F8 RID: 17912
		public string HostileCopyPrefix;

		// Token: 0x040045F9 RID: 17913
		public string FriendlyCopyColorString;

		// Token: 0x040045FA RID: 17914
		public string FriendlyCopyPrefix;

		// Token: 0x040045FB RID: 17915
		[NonSerialized]
		public Dictionary<GameObject, Cell> Targets = new Dictionary<GameObject, Cell>();
	}
}
