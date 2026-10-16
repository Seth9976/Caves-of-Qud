using System;
using XRL.Core;
using XRL.Language;
using XRL.World.Parts.Skill;

namespace XRL.World.Effects
{
	// Token: 0x02000D49 RID: 3401
	[Serializable]
	public class Hooked : Effect
	{
		// Token: 0x06008661 RID: 34401 RVA: 0x00305744 File Offset: 0x00303944
		public Hooked()
		{
			this.DisplayName = "hooked";
			this.Duration = 9;
		}

		// Token: 0x06008662 RID: 34402 RVA: 0x0030576A File Offset: 0x0030396A
		public Hooked(GameObject HookingWeapon)
			: this()
		{
			this.HookingWeapon = HookingWeapon;
		}

		// Token: 0x06008663 RID: 34403 RVA: 0x00305779 File Offset: 0x00303979
		public Hooked(GameObject HookingWeapon, int SaveTarget)
			: this(HookingWeapon)
		{
			this.SaveTarget = SaveTarget;
		}

		// Token: 0x06008664 RID: 34404 RVA: 0x00305789 File Offset: 0x00303989
		public Hooked(GameObject HookingWeapon, int SaveTarget, int Duration)
			: this(HookingWeapon, SaveTarget)
		{
			this.Duration = Duration;
		}

		// Token: 0x06008665 RID: 34405 RVA: 0x0030579A File Offset: 0x0030399A
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008666 RID: 34406 RVA: 0x0030579D File Offset: 0x0030399D
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06008667 RID: 34407 RVA: 0x003057A0 File Offset: 0x003039A0
		public override int GetEffectType()
		{
			return 33554464;
		}

		// Token: 0x06008668 RID: 34408 RVA: 0x003057A7 File Offset: 0x003039A7
		public override string GetDetails()
		{
			return "Is being dragged.\nCan't move without breaking free first.";
		}

		// Token: 0x06008669 RID: 34409 RVA: 0x003057B0 File Offset: 0x003039B0
		public override bool Apply(GameObject Object)
		{
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_attachedTo", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			return true;
		}

		// Token: 0x0600866A RID: 34410 RVA: 0x003057EB File Offset: 0x003039EB
		public override void Remove(GameObject Object)
		{
			base.Remove(Object);
		}

		// Token: 0x0600866B RID: 34411 RVA: 0x003057F4 File Offset: 0x003039F4
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == SingletonEvent<CommandTakeActionEvent>.ID;
		}

		// Token: 0x0600866C RID: 34412 RVA: 0x00305814 File Offset: 0x00303A14
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (this.Duration > 0)
			{
				GameObject hookingWeapon = this.HookingWeapon;
				Axe_HookAndDrag axe_HookAndDrag;
				if (hookingWeapon == null)
				{
					axe_HookAndDrag = null;
				}
				else
				{
					GameObject equipped = hookingWeapon.Equipped;
					axe_HookAndDrag = ((equipped != null) ? equipped.GetPart<Axe_HookAndDrag>() : null);
				}
				Axe_HookAndDrag axe_HookAndDrag2 = axe_HookAndDrag;
				if (axe_HookAndDrag2 != null)
				{
					axe_HookAndDrag2.Validate(null);
				}
				else
				{
					base.Object.RemoveEffect(this, true);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600866D RID: 34413 RVA: 0x0030586C File Offset: 0x00303A6C
		public override bool HandleEvent(CommandTakeActionEvent E)
		{
			if (this.Duration > 0 && (!base.Object.FireEvent("BeforeGrabbed") || base.Object.MakeSave("Strength", this.SaveTarget, this.HookingWeapon.Equipped, null, "HookAndDrag Continue Grab Restraint Escape", false, false, false, false, this.HookingWeapon)))
			{
				base.DidX("break", "free from " + this.WhatSubjectIsHeldBy(), "!", null, null, base.Object, null, false, false, null, null, false, false, false, false, false, null);
				base.Object.ParticleText("*broke free*", IComponent<GameObject>.ConsequentialColorChar(base.Object, null), false, 1.5f, -8f);
				base.Object.RemoveEffect(this, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600866E RID: 34414 RVA: 0x00305939 File Offset: 0x00303B39
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginMove");
			Registrar.Register("Juked");
			Registrar.Register("IsMobile");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600866F RID: 34415 RVA: 0x00305964 File Offset: 0x00303B64
		public override bool Render(RenderEvent E)
		{
			if (this.Duration <= 0)
			{
				return true;
			}
			int num = XRLCore.CurrentFrame % 60;
			if (num > 35 && num < 45)
			{
				E.Tile = null;
				E.RenderString = "X";
				E.ColorString = "&R";
			}
			return true;
		}

		// Token: 0x06008670 RID: 34416 RVA: 0x003059B0 File Offset: 0x00303BB0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "IsMobile")
			{
				return false;
			}
			if (E.ID == "Juked")
			{
				base.Object.RemoveEffect(this, true);
			}
			else if (E.ID == "BeginMove")
			{
				GameObject hookingWeapon = this.HookingWeapon;
				Axe_HookAndDrag axe_HookAndDrag;
				if (hookingWeapon == null)
				{
					axe_HookAndDrag = null;
				}
				else
				{
					GameObject equipped = hookingWeapon.Equipped;
					axe_HookAndDrag = ((equipped != null) ? equipped.GetPart<Axe_HookAndDrag>() : null);
				}
				Axe_HookAndDrag axe_HookAndDrag2 = axe_HookAndDrag;
				if (axe_HookAndDrag2 != null && E.GetGameObjectParameter("Dragging") != this.HookingWeapon && axe_HookAndDrag2.Validate(null))
				{
					Cell cell = E.GetParameter("DestinationCell") as Cell;
					if (cell != null)
					{
						foreach (GameObject gameObject in cell.LoopObjectsWithPart("Brain"))
						{
							if (gameObject.Brain.IsHostileTowards(base.Object) && gameObject.IsCombat)
							{
								goto IL_0199;
							}
						}
						if (E.HasParameter("Teleporting"))
						{
							base.Object.ParticleText("*broke free*", IComponent<GameObject>.ConsequentialColorChar(base.Object, null), false, 1.5f, -8f);
							this.Duration = 0;
						}
						else if (this.Duration > 0 && !E.HasParameter("Dragging") && !E.HasParameter("Teleporting"))
						{
							if (base.Object.IsPlayer())
							{
								IComponent<GameObject>.AddPlayerMessage(this.HookedMessage, 'R', true);
							}
							base.Object.UseEnergy(1000, "Movement Failure", null, null, false);
							return false;
						}
					}
				}
			}
			IL_0199:
			return base.FireEvent(E);
		}

		// Token: 0x06008671 RID: 34417 RVA: 0x00305B70 File Offset: 0x00303D70
		public string WhatSubjectIsHeldBy()
		{
			GameObject.Validate(ref this.HookingWeapon);
			if (this.HookingWeapon == null)
			{
				return "the hook maneuver";
			}
			if (this.HookingWeapon.HasProperName || this.HookingWeapon.Equipped == null)
			{
				return this.HookingWeapon.the + this.HookingWeapon.ShortDisplayName;
			}
			if (this.HookingWeapon.Equipped.IsPlayer())
			{
				return "your " + this.HookingWeapon.ShortDisplayName;
			}
			return Grammar.MakePossessive(this.HookingWeapon.Equipped.the + this.HookingWeapon.Equipped.ShortDisplayName) + " " + this.HookingWeapon.ShortDisplayName;
		}

		// Token: 0x040039DC RID: 14812
		public int SaveTarget;

		// Token: 0x040039DD RID: 14813
		public string HookedMessage = "You are hooked!";

		// Token: 0x040039DE RID: 14814
		public GameObject HookingWeapon;
	}
}
