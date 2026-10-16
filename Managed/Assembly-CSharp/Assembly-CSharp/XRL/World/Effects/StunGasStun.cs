using System;
using XRL.Core;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000D97 RID: 3479
	[Serializable]
	public class StunGasStun : Effect
	{
		// Token: 0x06008AAC RID: 35500 RVA: 0x00316E7F File Offset: 0x0031507F
		public StunGasStun()
		{
			this.Duration = 1;
			this.DisplayName = this.GetDescription();
		}

		// Token: 0x06008AAD RID: 35501 RVA: 0x00316EA2 File Offset: 0x003150A2
		public StunGasStun(int _Density)
			: this()
		{
			this.Density = _Density;
		}

		// Token: 0x06008AAE RID: 35502 RVA: 0x00316EB1 File Offset: 0x003150B1
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008AAF RID: 35503 RVA: 0x00316EB4 File Offset: 0x003150B4
		public override int GetEffectType()
		{
			return 117440520;
		}

		// Token: 0x06008AB0 RID: 35504 RVA: 0x00316EBB File Offset: 0x003150BB
		public override string GetDescription()
		{
			return "{{C|stunned by gas}}";
		}

		// Token: 0x06008AB1 RID: 35505 RVA: 0x00316EC2 File Offset: 0x003150C2
		public override string GetDetails()
		{
			if (this.Density > 60)
			{
				return "(very dense) Can't take actions.";
			}
			if (this.Density > 40)
			{
				return "(dense) -60% Quickness";
			}
			return "(light) -30% Quickness";
		}

		// Token: 0x06008AB2 RID: 35506 RVA: 0x00316EE9 File Offset: 0x003150E9
		public override bool Apply(GameObject Object)
		{
			return Object.HasPart<Brain>() && Object.FireEvent("CanApplyStunGasStun") && ApplyEffectEvent.Check(Object, "StunGasStun", this, null) && Object.FireEvent("ApplyStunGasStun");
		}

		// Token: 0x06008AB3 RID: 35507 RVA: 0x00316F20 File Offset: 0x00315120
		public override void Remove(GameObject Object)
		{
			this.UnapplyStats();
		}

		// Token: 0x06008AB4 RID: 35508 RVA: 0x00316F28 File Offset: 0x00315128
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			Registrar.Register("CommandTakeAction");
			Registrar.Register("IsMobile");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008AB5 RID: 35509 RVA: 0x00316F5E File Offset: 0x0031515E
		private void ApplyStats()
		{
			base.StatShifter.SetStatShift("Speed", -this.SpeedPenalty, false);
		}

		// Token: 0x06008AB6 RID: 35510 RVA: 0x00316F79 File Offset: 0x00315179
		private void UnapplyStats()
		{
			base.StatShifter.RemoveStatShifts();
		}

		// Token: 0x06008AB7 RID: 35511 RVA: 0x00316F88 File Offset: 0x00315188
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 10 && num < 25)
				{
					if (this.Density > 60)
					{
						E.Tile = null;
						E.RenderString = "!";
						E.ColorString = "&C^c";
					}
					else if (this.SpeedPenalty > 0)
					{
						E.Tile = null;
						E.RenderString = "\u0019";
						E.ColorString = "&C^c";
					}
				}
			}
			return true;
		}

		// Token: 0x06008AB8 RID: 35512 RVA: 0x00317004 File Offset: 0x00315204
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandTakeAction")
			{
				if (this.Duration > 0)
				{
					this.Duration = 0;
					if (base.Object.FireEvent("ApplyStun"))
					{
						if (this.Density > 60)
						{
							base.DidX("are", "stunned", "!", null, null, null, base.Object, false, false, null, null, false, false, false, false, false, null);
							base.Object.ForfeitTurn(false);
							base.Object.ParticleText("*stunned*", IComponent<GameObject>.ConsequentialColorChar(null, base.Object), false, 1.5f, -8f);
							return false;
						}
						if (this.Density > 40)
						{
							if (this.SpeedPenalty != 0)
							{
								this.UnapplyStats();
							}
							this.SpeedPenalty = base.Object.Stat("Speed", 0) * 6 / 10;
							this.ApplyStats();
						}
						else
						{
							if (this.SpeedPenalty != 0)
							{
								this.UnapplyStats();
							}
							this.SpeedPenalty = base.Object.Stat("Speed", 0) * 3 / 10;
							this.ApplyStats();
						}
					}
				}
			}
			else if (E.ID == "IsMobile")
			{
				if (this.Duration > 0 && this.Density > 60)
				{
					return false;
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

		// Token: 0x04003AB8 RID: 15032
		public int Density = 20;

		// Token: 0x04003AB9 RID: 15033
		public int SpeedPenalty;
	}
}
