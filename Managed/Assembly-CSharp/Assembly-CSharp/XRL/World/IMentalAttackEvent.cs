using System;

namespace XRL.World
{
	// Token: 0x020007D2 RID: 2002
	[GameEvent(Base = true, Cascade = 1)]
	public abstract class IMentalAttackEvent : MinEvent
	{
		// Token: 0x060050A9 RID: 20649 RVA: 0x001876FA File Offset: 0x001858FA
		public override int GetCascadeLevel()
		{
			return IMentalAttackEvent.CascadeLevel;
		}

		// Token: 0x060050AA RID: 20650 RVA: 0x00187701 File Offset: 0x00185901
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060050AB RID: 20651 RVA: 0x0018770C File Offset: 0x0018590C
		public override void Reset()
		{
			base.Reset();
			this.Attacker = null;
			this.Defender = null;
			this.Source = null;
			this.Command = null;
			this.Dice = null;
			this.Type = 0;
			this.Magnitude = 0;
			this.Penetrations = 0;
			this.Difficulty = 0;
			this.BaseDifficulty = 0;
			this.Modifier = 0;
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x060050AC RID: 20652 RVA: 0x0018776C File Offset: 0x0018596C
		// (set) Token: 0x060050AD RID: 20653 RVA: 0x0018777E File Offset: 0x0018597E
		public bool Reflected
		{
			get
			{
				return this.Type.HasBit(16777216);
			}
			set
			{
				(ref this.Type).SetBit(16777216, value);
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x060050AE RID: 20654 RVA: 0x00187791 File Offset: 0x00185991
		// (set) Token: 0x060050AF RID: 20655 RVA: 0x001877A3 File Offset: 0x001859A3
		public bool Reflectable
		{
			get
			{
				return this.Type.HasBit(8388608);
			}
			set
			{
				(ref this.Type).SetBit(8388608, value);
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x060050B0 RID: 20656 RVA: 0x001877B6 File Offset: 0x001859B6
		// (set) Token: 0x060050B1 RID: 20657 RVA: 0x001877C4 File Offset: 0x001859C4
		public bool Psionic
		{
			get
			{
				return this.Type.HasBit(1);
			}
			set
			{
				(ref this.Type).SetBit(1, value);
			}
		}

		// Token: 0x060050B2 RID: 20658 RVA: 0x001877D3 File Offset: 0x001859D3
		public bool IsPlayerInvolved()
		{
			return this.Attacker.IsPlayer() || this.Defender.IsPlayer();
		}

		// Token: 0x060050B3 RID: 20659 RVA: 0x001877F0 File Offset: 0x001859F0
		public void SetFrom(IMentalAttackEvent E)
		{
			this.Attacker = E.Attacker;
			this.Defender = E.Defender;
			this.Source = E.Source;
			this.Command = E.Command;
			this.Dice = E.Dice;
			this.Type = E.Type;
			this.Magnitude = E.Magnitude;
			this.Penetrations = E.Penetrations;
			this.Difficulty = E.Difficulty;
			this.BaseDifficulty = E.BaseDifficulty;
			this.Modifier = E.Modifier;
		}

		// Token: 0x060050B4 RID: 20660 RVA: 0x00187881 File Offset: 0x00185A81
		public void ApplyTo(IMentalAttackEvent E)
		{
			E.SetFrom(this);
		}

		// Token: 0x060050B5 RID: 20661 RVA: 0x0018788C File Offset: 0x00185A8C
		public void SetFrom(Event E)
		{
			this.Attacker = E.GetGameObjectParameter("Attacker");
			this.Defender = E.GetGameObjectParameter("Defender");
			this.Source = E.GetGameObjectParameter("Source");
			this.Command = E.GetStringParameter("Command", null);
			this.Dice = E.GetStringParameter("Dice", null);
			this.Type = E.GetIntParameter("Type", 0);
			this.Magnitude = E.GetIntParameter("Magnitude", 0);
			this.Penetrations = E.GetIntParameter("Penetrations", 0);
			this.BaseDifficulty = E.GetIntParameter("BaseDifficulty", 0);
			this.Difficulty = E.GetIntParameter("Difficulty", 0);
			this.Modifier = E.GetIntParameter("Modifier", 0);
		}

		// Token: 0x060050B6 RID: 20662 RVA: 0x0018795C File Offset: 0x00185B5C
		public void ApplyTo(Event E)
		{
			E.SetParameter("Attacker", this.Attacker);
			E.SetParameter("Defender", this.Defender);
			E.SetParameter("Source", this.Source);
			E.SetParameter("Command", this.Command);
			E.SetParameter("Dice", this.Dice);
			E.SetParameter("Type", this.Type);
			E.SetParameter("Magnitude", this.Magnitude);
			E.SetParameter("Penetrations", this.Penetrations);
			E.SetParameter("BaseDifficulty", this.BaseDifficulty);
			E.SetParameter("Difficulty", this.Difficulty);
			E.SetParameter("Modifier", this.Modifier);
		}

		// Token: 0x0400283B RID: 10299
		public new static readonly int CascadeLevel = 1;

		// Token: 0x0400283C RID: 10300
		public GameObject Attacker;

		// Token: 0x0400283D RID: 10301
		public GameObject Defender;

		// Token: 0x0400283E RID: 10302
		public GameObject Source;

		// Token: 0x0400283F RID: 10303
		public string Command;

		// Token: 0x04002840 RID: 10304
		public string Dice;

		// Token: 0x04002841 RID: 10305
		public int Type;

		// Token: 0x04002842 RID: 10306
		public int Magnitude;

		// Token: 0x04002843 RID: 10307
		public int Penetrations;

		// Token: 0x04002844 RID: 10308
		public int Difficulty;

		// Token: 0x04002845 RID: 10309
		public int BaseDifficulty;

		// Token: 0x04002846 RID: 10310
		public int Modifier;
	}
}
