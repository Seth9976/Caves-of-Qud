using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200052A RID: 1322
	public struct BlendState : IEquatable<BlendState>
	{
		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x060032A7 RID: 12967 RVA: 0x00062420 File Offset: 0x00060620
		public static BlendState defaultValue
		{
			get
			{
				return new BlendState(false, false);
			}
		}

		// Token: 0x060032A8 RID: 12968 RVA: 0x0006243C File Offset: 0x0006063C
		public BlendState(bool separateMRTBlend = false, bool alphaToMask = false)
		{
			this.m_BlendState0 = RenderTargetBlendState.defaultValue;
			this.m_BlendState1 = RenderTargetBlendState.defaultValue;
			this.m_BlendState2 = RenderTargetBlendState.defaultValue;
			this.m_BlendState3 = RenderTargetBlendState.defaultValue;
			this.m_BlendState4 = RenderTargetBlendState.defaultValue;
			this.m_BlendState5 = RenderTargetBlendState.defaultValue;
			this.m_BlendState6 = RenderTargetBlendState.defaultValue;
			this.m_BlendState7 = RenderTargetBlendState.defaultValue;
			this.m_SeparateMRTBlendStates = Convert.ToByte(separateMRTBlend);
			this.m_AlphaToMask = Convert.ToByte(alphaToMask);
			this.m_Padding = 0;
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x060032A9 RID: 12969 RVA: 0x000624C4 File Offset: 0x000606C4
		// (set) Token: 0x060032AA RID: 12970 RVA: 0x000624E1 File Offset: 0x000606E1
		public bool separateMRTBlendStates
		{
			get
			{
				return Convert.ToBoolean(this.m_SeparateMRTBlendStates);
			}
			set
			{
				this.m_SeparateMRTBlendStates = Convert.ToByte(value);
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x060032AB RID: 12971 RVA: 0x000624F0 File Offset: 0x000606F0
		// (set) Token: 0x060032AC RID: 12972 RVA: 0x0006250D File Offset: 0x0006070D
		public bool alphaToMask
		{
			get
			{
				return Convert.ToBoolean(this.m_AlphaToMask);
			}
			set
			{
				this.m_AlphaToMask = Convert.ToByte(value);
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x060032AD RID: 12973 RVA: 0x0006251C File Offset: 0x0006071C
		// (set) Token: 0x060032AE RID: 12974 RVA: 0x00062534 File Offset: 0x00060734
		public RenderTargetBlendState blendState0
		{
			get
			{
				return this.m_BlendState0;
			}
			set
			{
				this.m_BlendState0 = value;
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x060032AF RID: 12975 RVA: 0x00062540 File Offset: 0x00060740
		// (set) Token: 0x060032B0 RID: 12976 RVA: 0x00062558 File Offset: 0x00060758
		public RenderTargetBlendState blendState1
		{
			get
			{
				return this.m_BlendState1;
			}
			set
			{
				this.m_BlendState1 = value;
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x060032B1 RID: 12977 RVA: 0x00062564 File Offset: 0x00060764
		// (set) Token: 0x060032B2 RID: 12978 RVA: 0x0006257C File Offset: 0x0006077C
		public RenderTargetBlendState blendState2
		{
			get
			{
				return this.m_BlendState2;
			}
			set
			{
				this.m_BlendState2 = value;
			}
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x060032B3 RID: 12979 RVA: 0x00062588 File Offset: 0x00060788
		// (set) Token: 0x060032B4 RID: 12980 RVA: 0x000625A0 File Offset: 0x000607A0
		public RenderTargetBlendState blendState3
		{
			get
			{
				return this.m_BlendState3;
			}
			set
			{
				this.m_BlendState3 = value;
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x060032B5 RID: 12981 RVA: 0x000625AC File Offset: 0x000607AC
		// (set) Token: 0x060032B6 RID: 12982 RVA: 0x000625C4 File Offset: 0x000607C4
		public RenderTargetBlendState blendState4
		{
			get
			{
				return this.m_BlendState4;
			}
			set
			{
				this.m_BlendState4 = value;
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x060032B7 RID: 12983 RVA: 0x000625D0 File Offset: 0x000607D0
		// (set) Token: 0x060032B8 RID: 12984 RVA: 0x000625E8 File Offset: 0x000607E8
		public RenderTargetBlendState blendState5
		{
			get
			{
				return this.m_BlendState5;
			}
			set
			{
				this.m_BlendState5 = value;
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x060032B9 RID: 12985 RVA: 0x000625F4 File Offset: 0x000607F4
		// (set) Token: 0x060032BA RID: 12986 RVA: 0x0006260C File Offset: 0x0006080C
		public RenderTargetBlendState blendState6
		{
			get
			{
				return this.m_BlendState6;
			}
			set
			{
				this.m_BlendState6 = value;
			}
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x060032BB RID: 12987 RVA: 0x00062618 File Offset: 0x00060818
		// (set) Token: 0x060032BC RID: 12988 RVA: 0x00062630 File Offset: 0x00060830
		public RenderTargetBlendState blendState7
		{
			get
			{
				return this.m_BlendState7;
			}
			set
			{
				this.m_BlendState7 = value;
			}
		}

		// Token: 0x060032BD RID: 12989 RVA: 0x0006263C File Offset: 0x0006083C
		public bool Equals(BlendState other)
		{
			return this.m_BlendState0.Equals(other.m_BlendState0) && this.m_BlendState1.Equals(other.m_BlendState1) && this.m_BlendState2.Equals(other.m_BlendState2) && this.m_BlendState3.Equals(other.m_BlendState3) && this.m_BlendState4.Equals(other.m_BlendState4) && this.m_BlendState5.Equals(other.m_BlendState5) && this.m_BlendState6.Equals(other.m_BlendState6) && this.m_BlendState7.Equals(other.m_BlendState7) && this.m_SeparateMRTBlendStates == other.m_SeparateMRTBlendStates && this.m_AlphaToMask == other.m_AlphaToMask;
		}

		// Token: 0x060032BE RID: 12990 RVA: 0x0006270C File Offset: 0x0006090C
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is BlendState && this.Equals((BlendState)obj);
		}

		// Token: 0x060032BF RID: 12991 RVA: 0x00062744 File Offset: 0x00060944
		public override int GetHashCode()
		{
			int num = this.m_BlendState0.GetHashCode();
			num = (num * 397) ^ this.m_BlendState1.GetHashCode();
			num = (num * 397) ^ this.m_BlendState2.GetHashCode();
			num = (num * 397) ^ this.m_BlendState3.GetHashCode();
			num = (num * 397) ^ this.m_BlendState4.GetHashCode();
			num = (num * 397) ^ this.m_BlendState5.GetHashCode();
			num = (num * 397) ^ this.m_BlendState6.GetHashCode();
			num = (num * 397) ^ this.m_BlendState7.GetHashCode();
			num = (num * 397) ^ this.m_SeparateMRTBlendStates.GetHashCode();
			return (num * 397) ^ this.m_AlphaToMask.GetHashCode();
		}

		// Token: 0x060032C0 RID: 12992 RVA: 0x00062848 File Offset: 0x00060A48
		public static bool operator ==(BlendState left, BlendState right)
		{
			return left.Equals(right);
		}

		// Token: 0x060032C1 RID: 12993 RVA: 0x00062864 File Offset: 0x00060A64
		public static bool operator !=(BlendState left, BlendState right)
		{
			return !left.Equals(right);
		}

		// Token: 0x040010B0 RID: 4272
		private RenderTargetBlendState m_BlendState0;

		// Token: 0x040010B1 RID: 4273
		private RenderTargetBlendState m_BlendState1;

		// Token: 0x040010B2 RID: 4274
		private RenderTargetBlendState m_BlendState2;

		// Token: 0x040010B3 RID: 4275
		private RenderTargetBlendState m_BlendState3;

		// Token: 0x040010B4 RID: 4276
		private RenderTargetBlendState m_BlendState4;

		// Token: 0x040010B5 RID: 4277
		private RenderTargetBlendState m_BlendState5;

		// Token: 0x040010B6 RID: 4278
		private RenderTargetBlendState m_BlendState6;

		// Token: 0x040010B7 RID: 4279
		private RenderTargetBlendState m_BlendState7;

		// Token: 0x040010B8 RID: 4280
		private byte m_SeparateMRTBlendStates;

		// Token: 0x040010B9 RID: 4281
		private byte m_AlphaToMask;

		// Token: 0x040010BA RID: 4282
		private short m_Padding;
	}
}
