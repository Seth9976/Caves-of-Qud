using System;
using System.Collections.Generic;
using XRL.Core;
using XRL.UI;

namespace XRL.World.Effects
{
	// Token: 0x02000D60 RID: 3424
	[Serializable]
	public class Lost : Effect, ITierInitialized
	{
		// Token: 0x0600876C RID: 34668 RVA: 0x00309985 File Offset: 0x00307B85
		public Lost()
		{
			this.DisplayName = "lost";
			this.Duration = 9999;
		}

		// Token: 0x0600876D RID: 34669 RVA: 0x003099B8 File Offset: 0x00307BB8
		public Lost(int Duration = 9999, string InitialZone = null, string World = null, int MaxOrientZ = 10, bool DisableUnlost = false)
			: this()
		{
			this.Duration = Duration;
			this.World = World;
			if (!InitialZone.IsNullOrEmpty())
			{
				this.Visited.Add(InitialZone);
				if (this.World == null)
				{
					this.World = ZoneID.GetWorldID(InitialZone);
				}
			}
			this.MaxOrientZ = MaxOrientZ;
			this.DisableUnlost = DisableUnlost;
		}

		// Token: 0x0600876E RID: 34670 RVA: 0x00309A11 File Offset: 0x00307C11
		public void Initialize(int Tier)
		{
		}

		// Token: 0x0600876F RID: 34671 RVA: 0x00309A13 File Offset: 0x00307C13
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06008770 RID: 34672 RVA: 0x00309A16 File Offset: 0x00307C16
		public override string GetDetails()
		{
			return "Can't fast travel.";
		}

		// Token: 0x06008771 RID: 34673 RVA: 0x00309A1D File Offset: 0x00307C1D
		public override int GetEffectType()
		{
			return 117440640;
		}

		// Token: 0x06008772 RID: 34674 RVA: 0x00309A24 File Offset: 0x00307C24
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008773 RID: 34675 RVA: 0x00309A28 File Offset: 0x00307C28
		public override bool Apply(GameObject Object)
		{
			if (this.LostOn == 0L)
			{
				Zone zone = ((Object != null) ? Object.CurrentZone : null);
				if (zone != null)
				{
					if (this.World == null)
					{
						this.World = zone.ZoneWorld;
					}
					if (!this.Visited.Contains(zone.ZoneID))
					{
						this.Visited.Add(zone.ZoneID);
					}
				}
				this.LostOn = XRLCore.CurrentTurn - 1L;
			}
			if (Object != null)
			{
				int i = 0;
				int count = Object.Effects.Count;
				while (i < count)
				{
					Lost lost = Object.Effects[i] as Lost;
					if (lost != null && lost.World == this.World)
					{
						bool flag = false;
						if (this.Duration > lost.Duration)
						{
							lost.Duration = this.Duration;
							flag = true;
						}
						if (this.DisableUnlost && !lost.DisableUnlost)
						{
							lost.DisableUnlost = true;
							flag = true;
						}
						if (flag && Object != null)
						{
							Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_bewilderment", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
						}
						return false;
					}
					i++;
				}
			}
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_bewilderment", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			return true;
		}

		// Token: 0x06008774 RID: 34676 RVA: 0x00309B80 File Offset: 0x00307D80
		public override void Remove(GameObject Object)
		{
			if (Object.IsPlayer())
			{
				if (this.Duration == -1)
				{
					Popup.ShowSpace("You recognize the area and stop being lost!", null, "Sounds/UI/ui_notification", null, true, true, null);
				}
				else if (this.Duration != -2147483648)
				{
					Popup.ShowSpace("You regain your bearings.", null, "Sounds/UI/ui_notification", null, true, true, null);
				}
			}
			base.Remove(Object);
		}

		// Token: 0x06008775 RID: 34677 RVA: 0x00309BDC File Offset: 0x00307DDC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<AfterPlayerBodyChangeEvent>.ID || ID == PooledEvent<CanTravelEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == ZoneActivatedEvent.ID;
		}

		// Token: 0x06008776 RID: 34678 RVA: 0x00309C0A File Offset: 0x00307E0A
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Object == base.Object && !base.Object.IsPlayer() && !E.Reference)
			{
				E.AddAdjective("lost", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008777 RID: 34679 RVA: 0x00309C44 File Offset: 0x00307E44
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			if (base.IsPlayer() && this.Duration > 0 && !this.DisableUnlost && E.Zone != null)
			{
				if (this.World == null)
				{
					this.World = E.Zone.ZoneWorld;
				}
				if (E.Zone.ZoneWorld == this.World)
				{
					if (E.Zone.LastPlayerPresence != -1L && E.Zone.LastPlayerPresence < this.LostOn)
					{
						this.Duration = -1;
						base.Object.RemoveEffect(this, true);
					}
					else if (!this.Visited.Contains(E.Zone.ZoneID) && E.Zone.Z <= this.MaxOrientZ)
					{
						this.Visited.Add(E.Zone.ZoneID);
						if ((base.Object.HasSkill("Survival") ? 40 : 20).in100())
						{
							base.Object.RemoveEffect(this, true);
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008778 RID: 34680 RVA: 0x00309D60 File Offset: 0x00307F60
		public override bool HandleEvent(CanTravelEvent E)
		{
			if (E.Object == base.Object)
			{
				Zone currentZone = base.Object.CurrentZone;
				if (((currentZone != null) ? currentZone.ZoneWorld : null) == this.World)
				{
					return E.Object.ShowFailure("You are lost!");
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008779 RID: 34681 RVA: 0x00309DB7 File Offset: 0x00307FB7
		public override bool HandleEvent(AfterPlayerBodyChangeEvent E)
		{
			if (E.OldBody == base.Object && E.NewBody != null)
			{
				E.OldBody.RemoveEffect(this, false);
				E.NewBody.ApplyEffect(this, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003A1A RID: 14874
		public bool DisableUnlost;

		// Token: 0x04003A1B RID: 14875
		public long LostOn;

		// Token: 0x04003A1C RID: 14876
		public string World;

		// Token: 0x04003A1D RID: 14877
		public int MaxOrientZ = 10;

		// Token: 0x04003A1E RID: 14878
		public List<string> Visited = new List<string>();
	}
}
