using System;
using XRL.Core;
using XRL.UI;

namespace XRL.World.Effects
{
	// Token: 0x02000D53 RID: 3411
	[Serializable]
	public class Immobilized : Effect, ITierInitialized
	{
		// Token: 0x060086C1 RID: 34497 RVA: 0x00306A68 File Offset: 0x00304C68
		public Immobilized()
		{
			this.DisplayName = "immobilized";
			this.Duration = 1;
		}

		// Token: 0x060086C2 RID: 34498 RVA: 0x00306AC4 File Offset: 0x00304CC4
		public Immobilized(int SaveTarget)
			: this()
		{
			this.SaveTarget = SaveTarget;
		}

		// Token: 0x060086C3 RID: 34499 RVA: 0x00306AD3 File Offset: 0x00304CD3
		public Immobilized(int SaveTarget, string SaveStat)
			: this(SaveTarget)
		{
			this.SaveStat = SaveStat;
		}

		// Token: 0x060086C4 RID: 34500 RVA: 0x00306AE3 File Offset: 0x00304CE3
		public Immobilized(int SaveTarget, string SaveStat, string SaveVs)
			: this(SaveTarget, SaveStat)
		{
			this.SaveVs = SaveVs;
		}

		// Token: 0x060086C5 RID: 34501 RVA: 0x00306AF4 File Offset: 0x00304CF4
		public Immobilized(int SaveTarget, string SaveStat, string SaveVs, string Text)
			: this(SaveTarget, SaveStat, SaveVs)
		{
			this.Text = Text;
		}

		// Token: 0x060086C6 RID: 34502 RVA: 0x00306B07 File Offset: 0x00304D07
		public Immobilized(int SaveTarget, string SaveStat, string SaveVs, string Text, bool LinkedToProne)
			: this(SaveTarget, SaveStat, SaveVs, Text)
		{
			this.LinkedToProne = LinkedToProne;
		}

		// Token: 0x060086C7 RID: 34503 RVA: 0x00306B1C File Offset: 0x00304D1C
		public void Initialize(int Tier)
		{
			this.Duration = Tier + 1;
		}

		// Token: 0x060086C8 RID: 34504 RVA: 0x00306B27 File Offset: 0x00304D27
		public override int GetEffectType()
		{
			return 100663424;
		}

		// Token: 0x060086C9 RID: 34505 RVA: 0x00306B2E File Offset: 0x00304D2E
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x060086CA RID: 34506 RVA: 0x00306B31 File Offset: 0x00304D31
		public override string GetDetails()
		{
			return "Can't move or stand up.";
		}

		// Token: 0x060086CB RID: 34507 RVA: 0x00306B38 File Offset: 0x00304D38
		public override bool Apply(GameObject Object)
		{
			if (!Object.Statistics.ContainsKey("Energy"))
			{
				return false;
			}
			if (!Object.CanChangeBodyPosition("Immobilized", false, true, false, false))
			{
				return false;
			}
			if (Object.FireEvent("ApplyImmobilized"))
			{
				base.DidX("are", this.Text, "!", null, null, null, Object, false, false, null, null, false, false, false, false, false, null);
				if (!Object.IsPlayer() && base.Visible())
				{
					Object.ParticleText("*" + this.Text + "*", IComponent<GameObject>.ConsequentialColorChar(null, Object), false, 1.5f, -8f);
				}
				Object.Statistics["Energy"].BaseValue = 0;
				Object.BodyPositionChanged(null, true);
				return true;
			}
			return false;
		}

		// Token: 0x060086CC RID: 34508 RVA: 0x00306BFE File Offset: 0x00304DFE
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CanChangeMovementModeEvent>.ID || ID == PooledEvent<GetCompanionStatusEvent>.ID;
		}

		// Token: 0x060086CD RID: 34509 RVA: 0x00306C1C File Offset: 0x00304E1C
		public override bool HandleEvent(CanChangeMovementModeEvent E)
		{
			if (this.Duration > 0 && !E.Involuntary && E.Object == base.Object)
			{
				if (E.ShowMessage)
				{
					E.Object.Fail("You are " + this.Text + "!");
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060086CE RID: 34510 RVA: 0x00306C7A File Offset: 0x00304E7A
		public override bool HandleEvent(GetCompanionStatusEvent E)
		{
			if (E.Object == base.Object)
			{
				E.AddStatus("immobilized", 70);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060086CF RID: 34511 RVA: 0x00306CA0 File Offset: 0x00304EA0
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginTakeAction");
			Registrar.Register("BodyPositionChanged");
			Registrar.Register("CanChangeBodyPosition");
			Registrar.Register("CanStandUp");
			Registrar.Register("EndTurn");
			Registrar.Register("IsMobile");
			Registrar.Register("LeaveCell");
			Registrar.Register("MovementModeChanged");
			base.Register(Object, Registrar);
		}

		// Token: 0x060086D0 RID: 34512 RVA: 0x00306D10 File Offset: 0x00304F10
		public int GetEffectiveSaveTarget()
		{
			int num = this.SaveTarget;
			if (this.SaveTargetTurnDivisor != 0)
			{
				num -= this.Turns / this.SaveTargetTurnDivisor;
			}
			return num;
		}

		// Token: 0x060086D1 RID: 34513 RVA: 0x00306D40 File Offset: 0x00304F40
		public void EndImmobilization()
		{
			base.DidX("are", "no longer " + this.Text, null, null, null, base.Object, null, false, false, null, null, false, false, false, false, false, null);
			base.Object.RemoveEffect(this, true);
		}

		// Token: 0x060086D2 RID: 34514 RVA: 0x00306D8C File Offset: 0x00304F8C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "IsMobile" || E.ID == "CanStandUp")
			{
				if (this.Duration > 0)
				{
					return false;
				}
			}
			else if (E.ID == "BeginTakeAction")
			{
				if (this.Turns > 0 && base.Object.MakeSave(this.SaveStat, this.GetEffectiveSaveTarget(), null, null, this.SaveVs, false, false, false, false, null))
				{
					this.EndImmobilization();
				}
				else
				{
					this.Turns++;
				}
			}
			else if (E.ID == "LeaveCell")
			{
				if (this.Duration > 0 && !E.HasParameter("Teleporting") && !E.HasParameter("Dragging") && !E.HasParameter("Forced"))
				{
					if (base.Object.IsPlayer())
					{
						IComponent<GameObject>.AddPlayerMessage("You are " + this.Text + "&y!", null, true);
					}
					base.Object.UseEnergy(1000);
					return false;
				}
			}
			else if (E.ID == "CanChangeBodyPosition")
			{
				if (this.Duration > 0 && !E.HasFlag("Involuntary"))
				{
					if (E.HasFlag("ShowMessage") && base.Object.IsPlayer())
					{
						Popup.ShowFail("You are {{|" + this.Text + "}}!", true, true, true);
					}
					return false;
				}
			}
			else if (E.ID == "MovementModeChanged" || E.ID == "BodyPositionChanged")
			{
				base.Object.RemoveEffect(this, true);
			}
			return base.FireEvent(E);
		}

		// Token: 0x060086D3 RID: 34515 RVA: 0x00306F50 File Offset: 0x00305150
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 40 && num < 60)
				{
					E.RenderString = "\u001c";
					E.ColorString = "&K";
				}
			}
			return true;
		}

		// Token: 0x040039F5 RID: 14837
		public string Text = "immobilized";

		// Token: 0x040039F6 RID: 14838
		public int SaveTarget = 20;

		// Token: 0x040039F7 RID: 14839
		public string SaveStat = "Agility";

		// Token: 0x040039F8 RID: 14840
		public string SaveVs = "Immobilization";

		// Token: 0x040039F9 RID: 14841
		public int SaveTargetTurnDivisor = 5;

		// Token: 0x040039FA RID: 14842
		public int Turns;

		// Token: 0x040039FB RID: 14843
		public bool LinkedToProne = true;
	}
}
