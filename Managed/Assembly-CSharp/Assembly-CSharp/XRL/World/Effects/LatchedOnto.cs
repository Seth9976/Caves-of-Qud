using System;
using ConsoleLib.Console;
using XRL.Core;
using XRL.Language;

namespace XRL.World.Effects
{
	// Token: 0x02000D58 RID: 3416
	[Serializable]
	public class LatchedOnto : Effect
	{
		// Token: 0x060086FB RID: 34555 RVA: 0x0030777B File Offset: 0x0030597B
		public LatchedOnto()
		{
			this.DisplayName = "{{r|latched onto}}";
			this.Duration = 9;
		}

		// Token: 0x060086FC RID: 34556 RVA: 0x003077AC File Offset: 0x003059AC
		public LatchedOnto(GameObject LatchedOnWeapon)
			: this()
		{
			this.LatchedOnWeapon = LatchedOnWeapon;
		}

		// Token: 0x060086FD RID: 34557 RVA: 0x003077BB File Offset: 0x003059BB
		public LatchedOnto(GameObject LatchedOnWeapon, int SaveTarget)
			: this(LatchedOnWeapon)
		{
			this.SaveTarget = SaveTarget;
		}

		// Token: 0x060086FE RID: 34558 RVA: 0x003077CB File Offset: 0x003059CB
		public LatchedOnto(GameObject LatchedOnWeapon, int SaveTarget, string SaveStat)
			: this(LatchedOnWeapon, SaveTarget)
		{
			this.SaveStat = SaveStat;
		}

		// Token: 0x060086FF RID: 34559 RVA: 0x003077DC File Offset: 0x003059DC
		public LatchedOnto(GameObject LatchedOnWeapon, int SaveTarget, string SaveStat, string SaveDifficultyStat)
			: this(LatchedOnWeapon, SaveTarget, SaveStat)
		{
			this.SaveDifficultyStat = SaveDifficultyStat;
		}

		// Token: 0x06008700 RID: 34560 RVA: 0x003077EF File Offset: 0x003059EF
		public LatchedOnto(GameObject LatchedOnWeapon, int SaveTarget, string SaveStat, string SaveDifficultyStat, int Duration)
			: this(LatchedOnWeapon, SaveTarget, SaveStat, SaveDifficultyStat)
		{
			this.Duration = Duration;
		}

		// Token: 0x06008701 RID: 34561 RVA: 0x00307804 File Offset: 0x00305A04
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06008702 RID: 34562 RVA: 0x00307807 File Offset: 0x00305A07
		public override int GetEffectType()
		{
			return 33554464;
		}

		// Token: 0x06008703 RID: 34563 RVA: 0x00307810 File Offset: 0x00305A10
		public override string GetDetails()
		{
			string text = this.WhatSubjectIsHeldBy(true, true, false);
			if (text == null)
			{
				return "Has been latched onto.\nCan't move without breaking free first.";
			}
			return "Has been latched onto by " + text + ".\nCan't move without breaking free first.";
		}

		// Token: 0x06008704 RID: 34564 RVA: 0x00307840 File Offset: 0x00305A40
		public override bool Apply(GameObject Object)
		{
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_attachedTo", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			return true;
		}

		// Token: 0x06008705 RID: 34565 RVA: 0x0030787B File Offset: 0x00305A7B
		public override void Remove(GameObject Object)
		{
			base.Remove(Object);
		}

		// Token: 0x06008706 RID: 34566 RVA: 0x00307884 File Offset: 0x00305A84
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID;
		}

		// Token: 0x06008707 RID: 34567 RVA: 0x0030789C File Offset: 0x00305A9C
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (this.Duration > 0)
			{
				if (base.Object.FireEvent("BeforeGrabbed"))
				{
					GameObject @object = base.Object;
					string saveStat = this.SaveStat;
					int saveTarget = this.SaveTarget;
					GameObject latchedOnWeapon = this.LatchedOnWeapon;
					if (!@object.MakeSave(saveStat, saveTarget, (latchedOnWeapon != null) ? latchedOnWeapon.Equipped : null, this.SaveDifficultyStat, "LatchOn Continue Grab Restraint Escape", false, false, false, false, this.LatchedOnWeapon))
					{
						goto IL_00E2;
					}
				}
				base.Object.ParticleText("*broke free*", IComponent<GameObject>.ConsequentialColorChar(base.Object, null), false, 1.5f, -8f);
				string text = this.WhatSubjectIsHeldBy(false, false, this.LatchedOnWeapon.Physics.Equipped.IsPlayer());
				base.DidX("break", "free from " + text, "!", null, null, base.Object, null, false, false, null, null, false, false, false, false, false, null);
				base.Object.RemoveEffect(this, true);
			}
			IL_00E2:
			return base.HandleEvent(E);
		}

		// Token: 0x06008708 RID: 34568 RVA: 0x00307992 File Offset: 0x00305B92
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginMove");
			Registrar.Register("Juked");
			Registrar.Register("IsMobile");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008709 RID: 34569 RVA: 0x003079BD File Offset: 0x00305BBD
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0 && XRLCore.CurrentFrame > 35 && XRLCore.CurrentFrame < 45)
			{
				E.Tile = null;
				E.RenderString = "X";
				E.ColorString = "&R";
			}
			return true;
		}

		// Token: 0x0600870A RID: 34570 RVA: 0x003079F8 File Offset: 0x00305BF8
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
			else if (E.ID == "BeginMove" && E.GetGameObjectParameter("Dragging") != this.LatchedOnWeapon)
			{
				Cell cell = E.GetParameter("DestinationCell") as Cell;
				if (cell != null)
				{
					foreach (GameObject gameObject in cell.LoopObjectsWithPart("Brain"))
					{
						if (gameObject.Brain.IsHostileTowards(base.Object) && gameObject.IsCombat)
						{
							return true;
						}
					}
					if (E.HasParameter("Teleporting"))
					{
						base.Object.ParticleText("*broke free*", IComponent<GameObject>.ConsequentialColorChar(base.Object, null), false, 1.5f, -8f);
						this.Duration = 0;
						goto IL_0183;
					}
					if (this.Duration > 0)
					{
						string text = this.WhatSubjectIsHeldBy(false, false, this.LatchedOnWeapon.Physics.Equipped.IsPlayer());
						base.DidX("are", "held in place by " + text, "!", null, null, null, base.Object, false, false, null, null, false, false, false, false, false, null);
						base.Object.UseEnergy(1000, "Movement Failure", null, null, false);
						return false;
					}
				}
			}
			IL_0183:
			return base.FireEvent(E);
		}

		// Token: 0x0600870B RID: 34571 RVA: 0x00307BA4 File Offset: 0x00305DA4
		public string WhatSubjectIsHeldBy(bool NullOkay = false, bool Indefinite = false, bool SecondPerson = false)
		{
			if (this.LatchedOnWeapon != null && this.LatchedOnWeapon.IsNowhere())
			{
				this.LatchedOnWeapon = null;
			}
			if (this.LatchedOnWeapon == null)
			{
				if (!NullOkay)
				{
					return "being latched onto";
				}
				return null;
			}
			else
			{
				if (this.LatchedOnWeapon.HasProperName || this.LatchedOnWeapon.Equipped == null)
				{
					return (Indefinite ? this.LatchedOnWeapon.a : this.LatchedOnWeapon.the) + this.LatchedOnWeapon.ShortDisplayName;
				}
				return (SecondPerson ? "your" : Grammar.MakePossessive((Indefinite ? this.LatchedOnWeapon.Equipped.a : this.LatchedOnWeapon.Equipped.the) + this.LatchedOnWeapon.Equipped.ShortDisplayName)) + " " + this.LatchedOnWeapon.ShortDisplayName;
			}
		}

		// Token: 0x0600870C RID: 34572 RVA: 0x00307C84 File Offset: 0x00305E84
		public override void Expired()
		{
			if (this.LatchedOnWeapon != null)
			{
				string text = this.WhatSubjectIsHeldBy(true, false, false);
				if (text != null)
				{
					string text2 = IComponent<GameObject>.ConsequentialColor(base.Object, null);
					if (base.Object.IsPlayer())
					{
						IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
						{
							text2,
							ColorUtility.CapitalizeExceptFormatting(text),
							text2,
							this.LatchedOnWeapon.GetVerb("release", true, false, false, true, false),
							" you."
						}), null, true);
						return;
					}
					if (base.Object.IsVisible())
					{
						IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
						{
							text2,
							ColorUtility.CapitalizeExceptFormatting(text),
							text2,
							this.LatchedOnWeapon.GetVerb("release", true, false, false, true, false),
							" ",
							base.Object.the,
							base.Object.ShortDisplayName,
							text2,
							"."
						}), null, true);
					}
				}
			}
		}

		// Token: 0x04003A09 RID: 14857
		public int SaveTarget;

		// Token: 0x04003A0A RID: 14858
		public string SaveStat = "Strength";

		// Token: 0x04003A0B RID: 14859
		public string SaveDifficultyStat = "Strength";

		// Token: 0x04003A0C RID: 14860
		public GameObject LatchedOnWeapon;
	}
}
