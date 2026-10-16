using System;

namespace XRL.World.Parts
{
	// Token: 0x0200104F RID: 4175
	[Serializable]
	public abstract class IProgrammableRecoiler : IPoweredPart
	{
		// Token: 0x0600A63D RID: 42557 RVA: 0x003CD33D File Offset: 0x003CB53D
		public IProgrammableRecoiler()
		{
			this.ChargeUse = 10000;
			this.IsRealityDistortionBased = true;
			this.MustBeUnderstood = true;
			this.WorksOnCarrier = true;
			this.WorksOnHolder = true;
		}

		// Token: 0x0600A63E RID: 42558 RVA: 0x003CD36C File Offset: 0x003CB56C
		public override bool SameAs(IPart p)
		{
			IProgrammableRecoiler programmableRecoiler = p as IProgrammableRecoiler;
			return programmableRecoiler.Reprogrammable == this.Reprogrammable && programmableRecoiler.TimesProgrammed == this.TimesProgrammed && base.SameAs(p);
		}

		// Token: 0x0600A63F RID: 42559 RVA: 0x003CD3A7 File Offset: 0x003CB5A7
		public virtual void ProgrammedForLocation(Zone Z, Cell C)
		{
		}

		// Token: 0x0600A640 RID: 42560 RVA: 0x003CD3AC File Offset: 0x003CB5AC
		public static bool ProgramObjectForLocation(GameObject obj, Zone Z, Cell C = null, IProgrammableRecoiler pPR = null)
		{
			if (Z == null)
			{
				return false;
			}
			if (Z.IsWorldMap())
			{
				return false;
			}
			ITeleporter partDescendedFrom = obj.GetPartDescendedFrom<ITeleporter>();
			if (partDescendedFrom == null)
			{
				return false;
			}
			if (pPR == null)
			{
				pPR = obj.GetPartDescendedFrom<IProgrammableRecoiler>();
				if (pPR == null)
				{
					return false;
				}
			}
			pPR.TimesProgrammed++;
			partDescendedFrom.DestinationZone = Z.ZoneID;
			partDescendedFrom.DestinationX = ((C == null) ? (-1) : C.X);
			partDescendedFrom.DestinationY = ((C == null) ? (-1) : C.Y);
			pPR.PlayWorldSound(pPR.Sound, 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
			pPR.ProgrammedForLocation(Z, C);
			return true;
		}

		// Token: 0x0600A641 RID: 42561 RVA: 0x003CD457 File Offset: 0x003CB657
		public static bool ProgramObjectForLocation(GameObject obj, Cell C)
		{
			return IProgrammableRecoiler.ProgramObjectForLocation(obj, C.ParentZone, C, null);
		}

		// Token: 0x0600A642 RID: 42562 RVA: 0x003CD467 File Offset: 0x003CB667
		public static bool ProgramObjectForLocation(GameObject obj)
		{
			return IProgrammableRecoiler.ProgramObjectForLocation(obj, obj.CurrentCell);
		}

		// Token: 0x0600A643 RID: 42563 RVA: 0x003CD475 File Offset: 0x003CB675
		public bool ProgramForLocation(Zone Z, Cell C = null)
		{
			return IProgrammableRecoiler.ProgramObjectForLocation(this.ParentObject, Z, C, this);
		}

		// Token: 0x0600A644 RID: 42564 RVA: 0x003CD485 File Offset: 0x003CB685
		public bool ProgramForLocation(Cell C)
		{
			return C != null && this.ProgramForLocation(C.ParentZone, C);
		}

		// Token: 0x0600A645 RID: 42565 RVA: 0x003CD499 File Offset: 0x003CB699
		public bool ProgramForLocation(GameObject obj)
		{
			return obj != null && this.ProgramForLocation(obj.CurrentCell);
		}

		// Token: 0x0600A646 RID: 42566 RVA: 0x003CD4AC File Offset: 0x003CB6AC
		public bool ProgramForLocation()
		{
			return this.ProgramForLocation(this.ParentObject);
		}

		// Token: 0x0600A647 RID: 42567 RVA: 0x003CD4BC File Offset: 0x003CB6BC
		public bool ProgramRecoiler(GameObject Actor, IEvent FromEvent = null)
		{
			if (!this.IsObjectActivePartSubject(Actor))
			{
				return false;
			}
			Cell cell = Actor.CurrentCell;
			InteriorZone interiorZone = cell.ParentZone as InteriorZone;
			if (interiorZone != null)
			{
				cell = interiorZone.ResolveBasisCell();
			}
			if ((!this.Reprogrammable && this.TimesProgrammed > 0) || cell == null || cell.ParentZone == null || cell.OnWorldMap())
			{
				return Actor.Fail(this.ParentObject.Does("click", int.MaxValue, null, null, "merely", false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + ".");
			}
			ActivePartStatus activePartStatus = base.GetActivePartStatus(true, false, false, false, false, false, false, true, false, false, 1, null, false, 0L, null);
			if (activePartStatus != ActivePartStatus.Operational)
			{
				if (activePartStatus == ActivePartStatus.Unpowered)
				{
					Actor.Fail(this.ParentObject.Does("do", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " not have enough charge to be imprinted with the current location.");
				}
				else
				{
					Actor.Fail(this.ParentObject.Does("click", int.MaxValue, null, null, "merely", false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + ".");
				}
				return false;
			}
			if (this.IsRealityDistortionBased && !Actor.FireEvent(Event.New("InitiateRealityDistortionLocal", "Object", Actor, "Device", this.ParentObject), FromEvent))
			{
				return false;
			}
			SoundManager.PlayUISound("Sounds/Abilities/sfx_ability_mutation_mental_generic_activate", 1f, false, false, SoundRequest.SoundEffectType.None);
			base.DidX("vibrate", "as the current location is imprinted in " + this.ParentObject.its + " geospatial core", null, null, null, null, null, false, false, null, null, false, false, false, true, false, null);
			this.ProgramForLocation(cell);
			return true;
		}

		// Token: 0x040044A3 RID: 17571
		public string Sound;

		// Token: 0x040044A4 RID: 17572
		public bool Reprogrammable;

		// Token: 0x040044A5 RID: 17573
		public int TimesProgrammed;
	}
}
