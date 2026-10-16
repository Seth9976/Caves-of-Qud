using System;
using XRL.Liquids;
using XRL.UI;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x02001070 RID: 4208
	[Serializable]
	public class NeutronFluxContainment : IPoweredPart
	{
		// Token: 0x0600A79F RID: 42911 RVA: 0x003D4281 File Offset: 0x003D2481
		public NeutronFluxContainment()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600A7A0 RID: 42912 RVA: 0x003D4290 File Offset: 0x003D2490
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AfterObjectCreatedEvent.ID || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == BootSequenceInitializedEvent.ID || ID == CellChangedEvent.ID || ID == EffectAppliedEvent.ID || ID == EffectRemovedEvent.ID || ID == PooledEvent<NeutronFluxPourExplodesEvent>.ID || ID == PowerSwitchFlippedEvent.ID;
		}

		// Token: 0x0600A7A1 RID: 42913 RVA: 0x003D42EC File Offset: 0x003D24EC
		public override bool HandleEvent(NeutronFluxPourExplodesEvent E)
		{
			if (E.PouredTo == this.ParentObject)
			{
				if (E.Prospective)
				{
					if (!base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && E.PouredBy != null)
					{
						E.PouredBy.Fail(this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " " + base.GetStatusPhrase() + " and cannot be poured into.");
						E.Interrupt = true;
					}
					return false;
				}
				if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					return false;
				}
			}
			else if (E.PouredFrom == this.ParentObject)
			{
				if (E.Prospective)
				{
					if (!base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
					{
						if (E.PouredBy != null)
						{
							E.PouredBy.Fail(this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " " + base.GetStatusPhrase() + " and cannot be poured from.");
							E.Interrupt = true;
						}
						return false;
					}
					if (!NeutronFluxContainment.IsSuitableToReceiveNeutronFlux(E.PouredTo, E.PouredBy))
					{
						GameObject pouredBy = E.PouredBy;
						if (pouredBy == null || !pouredBy.IsPlayer())
						{
							return false;
						}
						if (Popup.ShowYesNo("There's no magnetic containment inside " + E.PouredTo.t(2147483647, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ". Pour anyway?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) != DialogResult.Yes)
						{
							E.Interrupt = true;
							return false;
						}
					}
				}
				else if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && NeutronFluxContainment.IsSuitableToReceiveNeutronFlux(E.PouredTo, E.PouredBy))
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A7A2 RID: 42914 RVA: 0x003D4538 File Offset: 0x003D2738
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			GameObject @object = E.Object;
			if (@object != null && @object.IsPlayer() && this.ParentObject.GetObjectContext() == E.Object)
			{
				LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
				if (liquidVolume != null && liquidVolume.ContainsLiquid("neutronflux") && base.WasReady() && !base.IsReady(false, false, false, false, false, false, false, false, false, false, E.Traveling ? 102 : 2, null, false, 0L, null))
				{
					if (E.Traveling)
					{
						if (!E.TravelMessagesSuppressed)
						{
							E.TravelMessagesSuppressed = true;
							if (Popup.ShowYesNo(this.GetWarningMessage() + " Do you want to stop travelling?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes)
							{
								return false;
							}
						}
					}
					else
					{
						Popup.Show(this.GetWarningMessage(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
						AutoAct.Interrupt(null, null, null, false);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A7A3 RID: 42915 RVA: 0x003D4630 File Offset: 0x003D2830
		public override bool HandleEvent(CellChangedEvent E)
		{
			this.CheckExplosion();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A7A4 RID: 42916 RVA: 0x003D463F File Offset: 0x003D283F
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			this.CheckExplosion();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A7A5 RID: 42917 RVA: 0x003D464E File Offset: 0x003D284E
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			this.CheckExplosion();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A7A6 RID: 42918 RVA: 0x003D465D File Offset: 0x003D285D
		public override bool HandleEvent(BootSequenceInitializedEvent E)
		{
			this.CheckExplosion();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A7A7 RID: 42919 RVA: 0x003D466C File Offset: 0x003D286C
		public override bool HandleEvent(PowerSwitchFlippedEvent E)
		{
			this.CheckExplosion();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A7A8 RID: 42920 RVA: 0x003D467C File Offset: 0x003D287C
		public override bool HandleEvent(AfterObjectCreatedEvent E)
		{
			if (E.Context == "Wares" || E.Context == "Stock" || E.Context == "Restock")
			{
				LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
				if (liquidVolume != null)
				{
					liquidVolume.Empty(false, false);
				}
			}
			else
			{
				LiquidVolume liquidVolume2 = this.ParentObject.LiquidVolume;
				if (liquidVolume2 != null && liquidVolume2.ContainsLiquid("neutronflux"))
				{
					if (base.IsReady(false, false, false, true, false, false, false, false, false, false, 1, null, false, 0L, null))
					{
						BootSequence part = this.ParentObject.GetPart<BootSequence>();
						if (part != null)
						{
							part.ForceBoot();
						}
					}
					else
					{
						this.ParentObject.LiquidVolume.Empty(false, false);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A7A9 RID: 42921 RVA: 0x003D4750 File Offset: 0x003D2950
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A7AA RID: 42922 RVA: 0x003D4754 File Offset: 0x003D2954
		public override void TurnTick(long TimeTick, int Amount)
		{
			base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, Amount, null, false, 0, false, null);
			this.CheckExplosion();
		}

		// Token: 0x0600A7AB RID: 42923 RVA: 0x003D4790 File Offset: 0x003D2990
		public static bool IsSuitableToReceiveNeutronFlux(GameObject Object, GameObject Actor = null)
		{
			NeutronFluxContainment neutronFluxContainment;
			return GameObject.Validate(ref Object) && Object.LiquidVolume != null && (GetPreferredLiquidEvent.GetFor(Object, Actor) == "neutronflux" || (Object.HasPart<NoDamage>() || Object.HasPart<NoDamageExcept>()) || (Object.TryGetPart<NeutronFluxContainment>(out neutronFluxContainment) && neutronFluxContainment.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null)));
		}

		// Token: 0x0600A7AC RID: 42924 RVA: 0x003D4810 File Offset: 0x003D2A10
		public void CheckExplosion()
		{
			LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
			if (liquidVolume != null && liquidVolume.ContainsLiquid("neutronflux") && !base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				GameObject holder = this.ParentObject.Holder;
				base.DidX("explode", null, "!", null, null, null, holder, false, false, null, null, false, false, false, false, true, null);
				LiquidNeutronFlux.Explode(this.ParentObject, holder, 0);
			}
		}

		// Token: 0x0600A7AD RID: 42925 RVA: 0x003D489C File Offset: 0x003D2A9C
		private string GetWarningMessage()
		{
			return this.ParentObject.Does("beep", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " loudly and " + this.ParentObject.GetVerb("flash", true, false, false, true, false) + " a warning glyph.";
		}
	}
}
