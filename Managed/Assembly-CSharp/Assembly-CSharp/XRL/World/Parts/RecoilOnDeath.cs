using System;
using System.Collections.Generic;
using UnityEngine;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x0200108A RID: 4234
	[Serializable]
	public class RecoilOnDeath : IPoweredPart
	{
		// Token: 0x0600A888 RID: 43144 RVA: 0x003D9E20 File Offset: 0x003D8020
		public RecoilOnDeath()
		{
			this.ChargeUse = 0;
			this.WorksOnEquipper = true;
			this.IsEMPSensitive = false;
			this.NameForStatus = "EmergencyMedEvac";
		}

		// Token: 0x0600A889 RID: 43145 RVA: 0x003D9E70 File Offset: 0x003D8070
		public override bool SameAs(IPart p)
		{
			RecoilOnDeath recoilOnDeath = p as RecoilOnDeath;
			return !(recoilOnDeath.DestinationZone != this.DestinationZone) && recoilOnDeath.DestinationX == this.DestinationX && recoilOnDeath.DestinationY == this.DestinationY && base.SameAs(p);
		}

		// Token: 0x0600A88A RID: 43146 RVA: 0x003D9EC0 File Offset: 0x003D80C0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<BeforeDismemberEvent>.ID || ID == EquippedEvent.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600A88B RID: 43147 RVA: 0x003D9EE6 File Offset: 0x003D80E6
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			if (this.ParentObject.Equipped.IsValid())
			{
				Registrar.Register(this.ParentObject.Equipped, BeforeDieEvent.ID, 0, false);
			}
		}

		// Token: 0x0600A88C RID: 43148 RVA: 0x003D9F12 File Offset: 0x003D8112
		public override bool HandleEvent(BeforeDismemberEvent E)
		{
			return (!E.IsObject || !E.Part.ObjectEquippedOnThisOrAnyParent(this.ParentObject) || !this.ParentObject.IsWorn(null)) && base.HandleEvent(E);
		}

		// Token: 0x0600A88D RID: 43149 RVA: 0x003D9F46 File Offset: 0x003D8146
		public override bool HandleEvent(EquippedEvent E)
		{
			E.Actor.RegisterEvent(this, BeforeDieEvent.ID, 0, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A88E RID: 43150 RVA: 0x003D9F62 File Offset: 0x003D8162
		public override bool HandleEvent(UnequippedEvent E)
		{
			E.Actor.UnregisterEvent(this, BeforeDieEvent.ID);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A88F RID: 43151 RVA: 0x003D9F7C File Offset: 0x003D817C
		public override bool HandleEvent(BeforeDieEvent E)
		{
			GameObject dying = E.Dying;
			if (!dying.IsValid() || this.ParentObject.Equipped != dying || !this.ParentObject.IsWorn(null) || this.DestinationZone.IsNullOrEmpty())
			{
				return true;
			}
			if (base.IsDisabled(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				return true;
			}
			Zone zone = The.ZoneManager.GetZone(this.DestinationZone);
			Zone anyBasisZone = this.GetAnyBasisZone();
			WorldBlueprint worldBlueprint = ((anyBasisZone != null) ? anyBasisZone.ResolveWorldBlueprint() : null);
			WorldBlueprint worldBlueprint2 = ((zone != null) ? zone.ResolveWorldBlueprint() : null);
			if (zone == null || worldBlueprint != worldBlueprint2)
			{
				return true;
			}
			Cell cell = zone.GetCell(40, 20);
			dying.RestorePristineHealth(false, false);
			if (this.DestinationX == -1 || this.DestinationY == -1)
			{
				try
				{
					List<Cell> emptyReachableCells = zone.GetEmptyReachableCells();
					if (emptyReachableCells.Count > 0)
					{
						cell = emptyReachableCells.GetRandomElement<Cell>(null);
					}
					goto IL_0100;
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
					goto IL_0100;
				}
			}
			cell = zone.GetCell(this.DestinationX, this.DestinationY);
			IL_0100:
			if (dying.IsPlayer())
			{
				Popup.Show("Just before your demise, you are transported to safety! " + this.ParentObject.Does("disintegrate", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			IComponent<GameObject>.XDidY(dying, "dematerialize", "out of the local region of spacetime", null, null, null, dying, null, false, false, null, null, false, false, false, false, false, null);
			dying.TeleportTo(cell, new int?(1000), true, false, false, false, null, "appear");
			E.RequestInterfaceExit();
			if (dying.IsPlayer())
			{
				this.ParentObject.Destroy(null, false, false, null);
			}
			return false;
		}

		// Token: 0x040045A5 RID: 17829
		public string DestinationZone = "";

		// Token: 0x040045A6 RID: 17830
		public int DestinationX = 40;

		// Token: 0x040045A7 RID: 17831
		public int DestinationY = 13;
	}
}
