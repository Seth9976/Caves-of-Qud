using System;

namespace XRL.World
{
	// Token: 0x020007D3 RID: 2003
	[GameEvent(Base = true)]
	public abstract class INavigationWeightEvent : MinEvent
	{
		// Token: 0x060050B9 RID: 20665 RVA: 0x00187A3F File Offset: 0x00185C3F
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060050BA RID: 20666 RVA: 0x00187A48 File Offset: 0x00185C48
		public override void Reset()
		{
			base.Reset();
			this.Cell = null;
			this.Actor = null;
			this.Object = null;
			this.Uncacheable = false;
			this.Weight = 0;
			this.PriorWeight = 0;
			this.Nav = 0;
			this._SocialWeight = 0;
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x060050BB RID: 20667 RVA: 0x00187A88 File Offset: 0x00185C88
		// (set) Token: 0x060050BC RID: 20668 RVA: 0x00187A95 File Offset: 0x00185C95
		public bool Smart
		{
			get
			{
				return (this.Nav & 1) != 0;
			}
			set
			{
				if (value)
				{
					this.Nav |= 1;
					return;
				}
				this.Nav &= -2;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x060050BD RID: 20669 RVA: 0x00187AB8 File Offset: 0x00185CB8
		// (set) Token: 0x060050BE RID: 20670 RVA: 0x00187AC5 File Offset: 0x00185CC5
		public bool Burrower
		{
			get
			{
				return (this.Nav & 2) != 0;
			}
			set
			{
				if (value)
				{
					this.Nav |= 2;
					return;
				}
				this.Nav &= -3;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x060050BF RID: 20671 RVA: 0x00187AE8 File Offset: 0x00185CE8
		// (set) Token: 0x060050C0 RID: 20672 RVA: 0x00187AF5 File Offset: 0x00185CF5
		public bool Autoexploring
		{
			get
			{
				return (this.Nav & 4) != 0;
			}
			set
			{
				if (value)
				{
					this.Nav |= 4;
					return;
				}
				this.Nav &= -5;
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x060050C1 RID: 20673 RVA: 0x00187B18 File Offset: 0x00185D18
		// (set) Token: 0x060050C2 RID: 20674 RVA: 0x00187B25 File Offset: 0x00185D25
		public bool Flying
		{
			get
			{
				return (this.Nav & 8) != 0;
			}
			set
			{
				if (value)
				{
					this.Nav |= 8;
					return;
				}
				this.Nav &= -9;
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x060050C3 RID: 20675 RVA: 0x00187B48 File Offset: 0x00185D48
		// (set) Token: 0x060050C4 RID: 20676 RVA: 0x00187B56 File Offset: 0x00185D56
		public bool WallWalker
		{
			get
			{
				return (this.Nav & 16) != 0;
			}
			set
			{
				if (value)
				{
					this.Nav |= 16;
					return;
				}
				this.Nav &= -17;
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x060050C5 RID: 20677 RVA: 0x00187B7A File Offset: 0x00185D7A
		// (set) Token: 0x060050C6 RID: 20678 RVA: 0x00187B88 File Offset: 0x00185D88
		public bool IgnoresWalls
		{
			get
			{
				return (this.Nav & 32) != 0;
			}
			set
			{
				if (value)
				{
					this.Nav |= 32;
					return;
				}
				this.Nav &= -33;
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x060050C7 RID: 20679 RVA: 0x00187BAC File Offset: 0x00185DAC
		// (set) Token: 0x060050C8 RID: 20680 RVA: 0x00187BBA File Offset: 0x00185DBA
		public bool Swimming
		{
			get
			{
				return (this.Nav & 64) != 0;
			}
			set
			{
				if (value)
				{
					this.Nav |= 64;
					return;
				}
				this.Nav &= -65;
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x060050C9 RID: 20681 RVA: 0x00187BDE File Offset: 0x00185DDE
		// (set) Token: 0x060050CA RID: 20682 RVA: 0x00187BEF File Offset: 0x00185DEF
		public bool Slimewalking
		{
			get
			{
				return (this.Nav & 128) != 0;
			}
			set
			{
				if (value)
				{
					this.Nav |= 128;
					return;
				}
				this.Nav &= -129;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x060050CB RID: 20683 RVA: 0x00187C19 File Offset: 0x00185E19
		// (set) Token: 0x060050CC RID: 20684 RVA: 0x00187C2A File Offset: 0x00185E2A
		public bool Aquatic
		{
			get
			{
				return (this.Nav & 256) != 0;
			}
			set
			{
				if (value)
				{
					this.Nav |= 256;
					return;
				}
				this.Nav &= -257;
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x060050CD RID: 20685 RVA: 0x00187C54 File Offset: 0x00185E54
		// (set) Token: 0x060050CE RID: 20686 RVA: 0x00187C65 File Offset: 0x00185E65
		public bool Polypwalking
		{
			get
			{
				return (this.Nav & 512) != 0;
			}
			set
			{
				if (value)
				{
					this.Nav |= 512;
					return;
				}
				this.Nav &= -513;
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x060050CF RID: 20687 RVA: 0x00187C8F File Offset: 0x00185E8F
		// (set) Token: 0x060050D0 RID: 20688 RVA: 0x00187CA0 File Offset: 0x00185EA0
		public bool Strutwalking
		{
			get
			{
				return (this.Nav & 1024) != 0;
			}
			set
			{
				if (value)
				{
					this.Nav |= 1024;
					return;
				}
				this.Nav &= -1025;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x060050D1 RID: 20689 RVA: 0x00187CCA File Offset: 0x00185ECA
		// (set) Token: 0x060050D2 RID: 20690 RVA: 0x00187CDB File Offset: 0x00185EDB
		public bool Juggernaut
		{
			get
			{
				return (this.Nav & 2048) != 0;
			}
			set
			{
				if (value)
				{
					this.Nav |= 2048;
					return;
				}
				this.Nav &= -2049;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x060050D3 RID: 20691 RVA: 0x00187D05 File Offset: 0x00185F05
		// (set) Token: 0x060050D4 RID: 20692 RVA: 0x00187D16 File Offset: 0x00185F16
		public bool Reefer
		{
			get
			{
				return (this.Nav & 4096) != 0;
			}
			set
			{
				if (value)
				{
					this.Nav |= 4096;
					return;
				}
				this.Nav &= -4097;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x060050D5 RID: 20693 RVA: 0x00187D40 File Offset: 0x00185F40
		// (set) Token: 0x060050D6 RID: 20694 RVA: 0x00187D51 File Offset: 0x00185F51
		public bool IgnoreCreatures
		{
			get
			{
				return (this.Nav & 8192) != 0;
			}
			set
			{
				if (value)
				{
					this.Nav |= 8192;
					return;
				}
				this.Nav &= -8193;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x060050D7 RID: 20695 RVA: 0x00187D7B File Offset: 0x00185F7B
		// (set) Token: 0x060050D8 RID: 20696 RVA: 0x00187D8C File Offset: 0x00185F8C
		public bool IgnoreGases
		{
			get
			{
				return (this.Nav & 16384) != 0;
			}
			set
			{
				if (value)
				{
					this.Nav |= 16384;
					return;
				}
				this.Nav &= -16385;
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x060050D9 RID: 20697 RVA: 0x00187DB6 File Offset: 0x00185FB6
		// (set) Token: 0x060050DA RID: 20698 RVA: 0x00187DC7 File Offset: 0x00185FC7
		public bool Unbreathing
		{
			get
			{
				return (this.Nav & 32768) != 0;
			}
			set
			{
				if (value)
				{
					this.Nav |= 32768;
					return;
				}
				this.Nav &= -32769;
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x060050DB RID: 20699 RVA: 0x00187DF1 File Offset: 0x00185FF1
		// (set) Token: 0x060050DC RID: 20700 RVA: 0x00187E02 File Offset: 0x00186002
		public bool FilthAffinity
		{
			get
			{
				return (this.Nav & 65536) != 0;
			}
			set
			{
				if (value)
				{
					this.Nav |= 65536;
					return;
				}
				this.Nav &= -65537;
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x060050DD RID: 20701 RVA: 0x00187E2C File Offset: 0x0018602C
		// (set) Token: 0x060050DE RID: 20702 RVA: 0x00187E3D File Offset: 0x0018603D
		public bool OutOfPhase
		{
			get
			{
				return (this.Nav & 131072) != 0;
			}
			set
			{
				if (value)
				{
					this.Nav |= 131072;
					return;
				}
				this.Nav &= -131073;
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x060050DF RID: 20703 RVA: 0x00187E67 File Offset: 0x00186067
		// (set) Token: 0x060050E0 RID: 20704 RVA: 0x00187E78 File Offset: 0x00186078
		public bool Omniphase
		{
			get
			{
				return (this.Nav & 262144) != 0;
			}
			set
			{
				if (value)
				{
					this.Nav |= 262144;
					return;
				}
				this.Nav &= -262145;
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x060050E1 RID: 20705 RVA: 0x00187EA2 File Offset: 0x001860A2
		// (set) Token: 0x060050E2 RID: 20706 RVA: 0x00187EB3 File Offset: 0x001860B3
		public bool Nullphase
		{
			get
			{
				return (this.Nav & 524288) != 0;
			}
			set
			{
				if (value)
				{
					this.Nav |= 524288;
					return;
				}
				this.Nav &= -524289;
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x060050E3 RID: 20707 RVA: 0x00187EDD File Offset: 0x001860DD
		// (set) Token: 0x060050E4 RID: 20708 RVA: 0x00187EEE File Offset: 0x001860EE
		public bool FlexPhase
		{
			get
			{
				return (this.Nav & 1048576) != 0;
			}
			set
			{
				if (value)
				{
					this.Nav |= 1048576;
					return;
				}
				this.Nav &= -1048577;
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x060050E5 RID: 20709 RVA: 0x00187F18 File Offset: 0x00186118
		// (set) Token: 0x060050E6 RID: 20710 RVA: 0x00187F35 File Offset: 0x00186135
		public int SocialWeight
		{
			get
			{
				if (this.Weight <= this._SocialWeight)
				{
					return this._SocialWeight;
				}
				return this.Weight;
			}
			set
			{
				this._SocialWeight = value;
			}
		}

		// Token: 0x060050E7 RID: 20711 RVA: 0x00187F3E File Offset: 0x0018613E
		public void MinWeight(int Value)
		{
			if (this.Weight < Value)
			{
				this.Weight = Value;
			}
		}

		// Token: 0x060050E8 RID: 20712 RVA: 0x00187F50 File Offset: 0x00186150
		public void MinWeight(int Value, int Max)
		{
			if (Value > Max)
			{
				Value = Max;
			}
			if (this.Weight < Value)
			{
				this.Weight = Value;
			}
		}

		// Token: 0x060050E9 RID: 20713 RVA: 0x00187F69 File Offset: 0x00186169
		public void MinSocialWeight(int Value)
		{
			if (this._SocialWeight < Value)
			{
				this._SocialWeight = Value;
			}
		}

		// Token: 0x060050EA RID: 20714 RVA: 0x00187F7B File Offset: 0x0018617B
		public void MinSocialWeight(int Value, int Max)
		{
			if (Value > Max)
			{
				Value = Max;
			}
			if (this._SocialWeight < Value)
			{
				this._SocialWeight = Value;
			}
		}

		// Token: 0x060050EB RID: 20715 RVA: 0x00187F94 File Offset: 0x00186194
		public virtual void ApplyTo(INavigationWeightEvent E)
		{
			E.Cell = this.Cell;
			E.Actor = this.Actor;
			E.Object = this.Object;
			E.Nav = this.Nav;
			E.Uncacheable = this.Uncacheable;
			E.Weight = this.Weight;
			E._SocialWeight = this._SocialWeight;
			E.PriorWeight = this.PriorWeight;
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x060050EC RID: 20716 RVA: 0x00188001 File Offset: 0x00186201
		// (set) Token: 0x060050ED RID: 20717 RVA: 0x00188022 File Offset: 0x00186222
		public int Phase
		{
			get
			{
				if (this.Nullphase)
				{
					return 4;
				}
				if (this.Omniphase)
				{
					return 3;
				}
				if (this.OutOfPhase)
				{
					return 2;
				}
				return 1;
			}
			set
			{
			}
		}

		// Token: 0x060050EE RID: 20718 RVA: 0x00188024 File Offset: 0x00186224
		public bool PhaseMatches(GameObject Object)
		{
			if (!Object.PhaseMatches(this.Phase))
			{
				return false;
			}
			if (this.FlexPhase)
			{
				int phase = Object.GetPhase();
				if (phase == 1 || phase == 2)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04002847 RID: 10311
		public Cell Cell;

		// Token: 0x04002848 RID: 10312
		public GameObject Actor;

		// Token: 0x04002849 RID: 10313
		public GameObject Object;

		// Token: 0x0400284A RID: 10314
		public bool Uncacheable;

		// Token: 0x0400284B RID: 10315
		public int Weight;

		// Token: 0x0400284C RID: 10316
		public int PriorWeight;

		// Token: 0x0400284D RID: 10317
		public int Nav;

		// Token: 0x0400284E RID: 10318
		private int _SocialWeight;
	}
}
