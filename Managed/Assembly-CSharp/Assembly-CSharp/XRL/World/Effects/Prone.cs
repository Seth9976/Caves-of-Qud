using System;
using XRL.Core;
using XRL.World.Anatomy;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000D73 RID: 3443
	[Serializable]
	public class Prone : Effect, ITierInitialized
	{
		// Token: 0x06008868 RID: 34920 RVA: 0x0030D870 File Offset: 0x0030BA70
		public Prone()
		{
			this.Duration = 1;
			this.DisplayName = "{{C|prone}}";
		}

		// Token: 0x06008869 RID: 34921 RVA: 0x0030D88C File Offset: 0x0030BA8C
		public Prone(bool Voluntary = false, bool StartMessageUsePopup = false, bool StopMessageUsePopup = false, GameObject LyingOn = null)
			: this()
		{
			this.Voluntary = Voluntary;
			this.StartMessageUsePopup = StartMessageUsePopup;
			this.StopMessageUsePopup = StopMessageUsePopup;
			this.LyingOn = LyingOn;
			if (LyingOn != null)
			{
				this.DisplayName = "{{C|lying on " + LyingOn.an(int.MaxValue, null, null, false, true, false, false, true, false, true, false, false, true, false, null, false, null, false) + "}}";
			}
		}

		// Token: 0x0600886A RID: 34922 RVA: 0x0030D8FC File Offset: 0x0030BAFC
		public override int GetEffectType()
		{
			int num = 117440640;
			if (this.Voluntary)
			{
				num |= 134217728;
			}
			return num;
		}

		// Token: 0x0600886B RID: 34923 RVA: 0x0030D920 File Offset: 0x0030BB20
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x0600886C RID: 34924 RVA: 0x0030D924 File Offset: 0x0030BB24
		public override string GetDescription()
		{
			this.CheckLyingOn();
			if (this.LyingOn != null)
			{
				return "{{C|lying on " + this.LyingOn.an(int.MaxValue, null, null, false, true, false, false, true, false, true, false, false, true, false, null, false, null, false) + "}}";
			}
			return "{{C|prone}}";
		}

		// Token: 0x0600886D RID: 34925 RVA: 0x0030D97E File Offset: 0x0030BB7E
		private string EffectSummary()
		{
			return "-6 Agility.\n-5 DV.\n-80 move speed.\nMust spend a turn to stand up.";
		}

		// Token: 0x0600886E RID: 34926 RVA: 0x0030D985 File Offset: 0x0030BB85
		public override string GetDetails()
		{
			if (this.LyingOn != null)
			{
				return this.LyingOn.DisplayName + ":\n" + this.EffectSummary();
			}
			return this.EffectSummary();
		}

		// Token: 0x0600886F RID: 34927 RVA: 0x0030D9B4 File Offset: 0x0030BBB4
		public bool IsLyingOnValid()
		{
			return GameObject.Validate(ref this.LyingOn) && this.LyingOn.CurrentCell != null && GameObject.Validate(base.Object) && base.Object.CurrentCell != null && this.LyingOn.CurrentCell == base.Object.CurrentCell;
		}

		// Token: 0x06008870 RID: 34928 RVA: 0x0030DA18 File Offset: 0x0030BC18
		public bool CheckLyingOn()
		{
			bool flag = this.IsLyingOnValid();
			if (!flag || this.LyingOn == null)
			{
				if (this.LyingOn != null)
				{
					this.LyingOn = null;
				}
				this.DisplayName = "{{C|prone}}";
				return flag;
			}
			this.DisplayName = "{{C|lying on " + this.LyingOn.an(int.MaxValue, null, null, false, true, false, false, true, false, true, false, false, true, false, null, false, null, false) + "}}";
			return flag;
		}

		// Token: 0x06008871 RID: 34929 RVA: 0x0030DA8F File Offset: 0x0030BC8F
		public static bool LimbSupportsProneness(BodyPart Part)
		{
			return (!(Part.Type != "Feet") || !(Part.Type != "Roots")) && Part.Mobility > 0;
		}

		// Token: 0x06008872 RID: 34930 RVA: 0x0030DAC4 File Offset: 0x0030BCC4
		public override bool Apply(GameObject Object)
		{
			if (Object.HasEffect<Prone>())
			{
				return false;
			}
			if (!Object.HasBodyPart(new Predicate<BodyPart>(Prone.LimbSupportsProneness)))
			{
				return false;
			}
			if (IsRootedInPlaceEvent.Check(Object))
			{
				return false;
			}
			if (!Object.CanChangeBodyPosition("Prone", false, !this.Voluntary, false, false))
			{
				return false;
			}
			if (!Object.FireEvent("ApplyProne"))
			{
				return false;
			}
			Object.RemoveEffect<Sitting>();
			if (this.Voluntary)
			{
				if (this.LyingOn != null && Object.IsPlayerControlled())
				{
					base.DidXToY("lie", "down on", this.LyingOn, null, null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, false, this.StartMessageUsePopup, null);
				}
				else
				{
					base.DidX("lie", "down", null, null, null, null, null, false, false, null, null, false, false, false, false, this.StartMessageUsePopup, null);
				}
			}
			else
			{
				base.DidX("are", "knocked prone", "!", null, null, null, Object, false, false, null, null, false, false, false, false, this.StartMessageUsePopup, null);
				Object.ParticleText("*knocked prone*", IComponent<GameObject>.ConsequentialColorChar(null, Object), false, 1.5f, -8f);
			}
			Object.BodyPositionChanged(null, !this.Voluntary);
			Cell currentCell = Object.CurrentCell;
			if (currentCell != null)
			{
				ObjectGoingProneEvent.Send(Object, currentCell, this.Voluntary, this.StartMessageUsePopup);
			}
			this.ApplyStats();
			Object.ForfeitTurn(true);
			return true;
		}

		// Token: 0x06008873 RID: 34931 RVA: 0x0030DC1C File Offset: 0x0030BE1C
		public override void Remove(GameObject Object)
		{
			this.UnapplyStats();
			foreach (Immobilized immobilized in Object.YieldEffects<Immobilized>())
			{
				if (immobilized != null && immobilized.LinkedToProne)
				{
					immobilized.EndImmobilization();
				}
			}
			base.Remove(Object);
		}

		// Token: 0x06008874 RID: 34932 RVA: 0x0030DC80 File Offset: 0x0030BE80
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == SingletonEvent<CommandTakeActionEvent>.ID && !this.Voluntary) || (ID == LeaveCellEvent.ID && this.Voluntary) || ID == PooledEvent<GetDisplayNameEvent>.ID;
		}

		// Token: 0x06008875 RID: 34933 RVA: 0x0030DCB8 File Offset: 0x0030BEB8
		public override bool HandleEvent(CommandTakeActionEvent E)
		{
			if (this.Duration > 0 && !this.Voluntary && !base.Object.HasEffect<Asleep>() && base.Object.FireEvent("CanStandUp") && base.Object.CanChangeBodyPosition("Standing", false, false, false, false))
			{
				this.Duration--;
				if (this.Duration > 0)
				{
					base.Object.UseEnergy(1000);
					return false;
				}
				this.StandUp(false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008876 RID: 34934 RVA: 0x0030DD44 File Offset: 0x0030BF44
		public override bool HandleEvent(LeaveCellEvent E)
		{
			if (this.Voluntary && this.Duration > 0)
			{
				this.StandUp(false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008877 RID: 34935 RVA: 0x0030DD68 File Offset: 0x0030BF68
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!E.Reference)
			{
				this.CheckLyingOn();
				if (this.LyingOn != null)
				{
					E.AddTag("[{{B|lying on " + this.LyingOn.an(int.MaxValue, null, null, false, true, false, false, true, false, true, false, false, true, false, null, false, null, false) + "}}]", 0);
				}
				else
				{
					E.AddTag("[{{B|prone}}]", 0);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008878 RID: 34936 RVA: 0x0030DDE0 File Offset: 0x0030BFE0
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			Registrar.Register("BodyPositionChanged");
			Registrar.Register("CanChangeBodyPosition");
			Registrar.Register("MovementModeChanged");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008879 RID: 34937 RVA: 0x0030DE2C File Offset: 0x0030C02C
		private void ApplyStats()
		{
			base.StatShifter.SetStatShift(base.Object, "Agility", -6, false);
			base.StatShifter.SetStatShift(base.Object, "DV", -5, false);
			base.StatShifter.SetStatShift(base.Object, "MoveSpeed", 80, false);
		}

		// Token: 0x0600887A RID: 34938 RVA: 0x0030DE87 File Offset: 0x0030C087
		private void UnapplyStats()
		{
			base.StatShifter.RemoveStatShifts(base.Object);
		}

		// Token: 0x0600887B RID: 34939 RVA: 0x0030DE9C File Offset: 0x0030C09C
		private void StandUp(bool UsePopup = false)
		{
			this.CheckLyingOn();
			if (this.LyingOn != null && base.Object.IsPlayerControlled())
			{
				if (this.LyingOn.HasPart(typeof(Bed)))
				{
					base.DidXToY("rise", "from", this.LyingOn, null, null, null, null, base.Object, null, false, false, false, false, false, null, null, null, false, false, false, false, UsePopup || this.StopMessageUsePopup, null);
				}
				else
				{
					base.DidXToY("stand", "up from", this.LyingOn, null, null, null, null, base.Object, null, false, false, false, false, false, null, null, null, false, false, false, false, UsePopup || this.StopMessageUsePopup, null);
				}
			}
			else
			{
				base.DidX("stand", "up", null, null, null, base.Object, null, false, false, null, null, false, false, false, false, UsePopup || this.StopMessageUsePopup, null);
			}
			base.Object.UseEnergy(1000, "Position", null, null, false);
			base.Object.RemoveEffect(this, true);
			GameObject @object = base.Object;
			if (@object == null)
			{
				return;
			}
			@object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_standUp", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
		}

		// Token: 0x0600887C RID: 34940 RVA: 0x0030DFEC File Offset: 0x0030C1EC
		public override bool Render(RenderEvent E)
		{
			int num = XRLCore.CurrentFrame % 60;
			if (num > 35 && num < 45)
			{
				E.Tile = null;
				E.RenderString = "_";
				E.ColorString = (this.Voluntary ? "&c" : "&R");
			}
			return true;
		}

		// Token: 0x0600887D RID: 34941 RVA: 0x0030E03C File Offset: 0x0030C23C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CanChangeBodyPosition")
			{
				if (!E.HasFlag("Involuntary") && E.GetStringParameter("To", null) != "Asleep" && E.GetStringParameter("To", null) != "Standing" && (!base.Object.FireEvent("CanStandUp") || !base.Object.FireEvent("CanStandUpFromProne")))
				{
					return false;
				}
			}
			else if (E.ID == "MovementModeChanged" || E.ID == "BodyPositionChanged")
			{
				if (this.Duration > 0 && E.GetStringParameter("To", null) != "Asleep" && E.GetStringParameter("To", null) != "Standing")
				{
					this.StandUp(false);
				}
			}
			else if (E.ID == "BeforeDeepCopyWithoutEffects")
			{
				this.UnapplyStats();
			}
			else if (E.ID == "AfterDeepCopyWithoutEffects")
			{
				this.ApplyStats();
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003A50 RID: 14928
		public bool Voluntary;

		// Token: 0x04003A51 RID: 14929
		public bool StartMessageUsePopup;

		// Token: 0x04003A52 RID: 14930
		public bool StopMessageUsePopup;

		// Token: 0x04003A53 RID: 14931
		public GameObject LyingOn;
	}
}
