using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200055F RID: 1375
	public struct StencilState : IEquatable<StencilState>
	{
		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x0600350D RID: 13581 RVA: 0x000683AC File Offset: 0x000665AC
		public static StencilState defaultValue
		{
			get
			{
				return new StencilState(true, byte.MaxValue, byte.MaxValue, CompareFunction.Always, StencilOp.Keep, StencilOp.Keep, StencilOp.Keep);
			}
		}

		// Token: 0x0600350E RID: 13582 RVA: 0x000683D4 File Offset: 0x000665D4
		public StencilState(bool enabled = true, byte readMask = 255, byte writeMask = 255, CompareFunction compareFunction = CompareFunction.Always, StencilOp passOperation = StencilOp.Keep, StencilOp failOperation = StencilOp.Keep, StencilOp zFailOperation = StencilOp.Keep)
		{
			this = new StencilState(enabled, readMask, writeMask, compareFunction, passOperation, failOperation, zFailOperation, compareFunction, passOperation, failOperation, zFailOperation);
		}

		// Token: 0x0600350F RID: 13583 RVA: 0x000683FC File Offset: 0x000665FC
		public StencilState(bool enabled, byte readMask, byte writeMask, CompareFunction compareFunctionFront, StencilOp passOperationFront, StencilOp failOperationFront, StencilOp zFailOperationFront, CompareFunction compareFunctionBack, StencilOp passOperationBack, StencilOp failOperationBack, StencilOp zFailOperationBack)
		{
			this.m_Enabled = Convert.ToByte(enabled);
			this.m_ReadMask = readMask;
			this.m_WriteMask = writeMask;
			this.m_Padding = 0;
			this.m_CompareFunctionFront = (byte)compareFunctionFront;
			this.m_PassOperationFront = (byte)passOperationFront;
			this.m_FailOperationFront = (byte)failOperationFront;
			this.m_ZFailOperationFront = (byte)zFailOperationFront;
			this.m_CompareFunctionBack = (byte)compareFunctionBack;
			this.m_PassOperationBack = (byte)passOperationBack;
			this.m_FailOperationBack = (byte)failOperationBack;
			this.m_ZFailOperationBack = (byte)zFailOperationBack;
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06003510 RID: 13584 RVA: 0x00068474 File Offset: 0x00066674
		// (set) Token: 0x06003511 RID: 13585 RVA: 0x00068491 File Offset: 0x00066691
		public bool enabled
		{
			get
			{
				return Convert.ToBoolean(this.m_Enabled);
			}
			set
			{
				this.m_Enabled = Convert.ToByte(value);
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06003512 RID: 13586 RVA: 0x000684A0 File Offset: 0x000666A0
		// (set) Token: 0x06003513 RID: 13587 RVA: 0x000684B8 File Offset: 0x000666B8
		public byte readMask
		{
			get
			{
				return this.m_ReadMask;
			}
			set
			{
				this.m_ReadMask = value;
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06003514 RID: 13588 RVA: 0x000684C4 File Offset: 0x000666C4
		// (set) Token: 0x06003515 RID: 13589 RVA: 0x000684DC File Offset: 0x000666DC
		public byte writeMask
		{
			get
			{
				return this.m_WriteMask;
			}
			set
			{
				this.m_WriteMask = value;
			}
		}

		// Token: 0x06003516 RID: 13590 RVA: 0x000684E6 File Offset: 0x000666E6
		public void SetCompareFunction(CompareFunction value)
		{
			this.compareFunctionFront = value;
			this.compareFunctionBack = value;
		}

		// Token: 0x06003517 RID: 13591 RVA: 0x000684F9 File Offset: 0x000666F9
		public void SetPassOperation(StencilOp value)
		{
			this.passOperationFront = value;
			this.passOperationBack = value;
		}

		// Token: 0x06003518 RID: 13592 RVA: 0x0006850C File Offset: 0x0006670C
		public void SetFailOperation(StencilOp value)
		{
			this.failOperationFront = value;
			this.failOperationBack = value;
		}

		// Token: 0x06003519 RID: 13593 RVA: 0x0006851F File Offset: 0x0006671F
		public void SetZFailOperation(StencilOp value)
		{
			this.zFailOperationFront = value;
			this.zFailOperationBack = value;
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x0600351A RID: 13594 RVA: 0x00068534 File Offset: 0x00066734
		// (set) Token: 0x0600351B RID: 13595 RVA: 0x0006854C File Offset: 0x0006674C
		public CompareFunction compareFunctionFront
		{
			get
			{
				return (CompareFunction)this.m_CompareFunctionFront;
			}
			set
			{
				this.m_CompareFunctionFront = (byte)value;
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x0600351C RID: 13596 RVA: 0x00068558 File Offset: 0x00066758
		// (set) Token: 0x0600351D RID: 13597 RVA: 0x00068570 File Offset: 0x00066770
		public StencilOp passOperationFront
		{
			get
			{
				return (StencilOp)this.m_PassOperationFront;
			}
			set
			{
				this.m_PassOperationFront = (byte)value;
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x0600351E RID: 13598 RVA: 0x0006857C File Offset: 0x0006677C
		// (set) Token: 0x0600351F RID: 13599 RVA: 0x00068594 File Offset: 0x00066794
		public StencilOp failOperationFront
		{
			get
			{
				return (StencilOp)this.m_FailOperationFront;
			}
			set
			{
				this.m_FailOperationFront = (byte)value;
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06003520 RID: 13600 RVA: 0x000685A0 File Offset: 0x000667A0
		// (set) Token: 0x06003521 RID: 13601 RVA: 0x000685B8 File Offset: 0x000667B8
		public StencilOp zFailOperationFront
		{
			get
			{
				return (StencilOp)this.m_ZFailOperationFront;
			}
			set
			{
				this.m_ZFailOperationFront = (byte)value;
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06003522 RID: 13602 RVA: 0x000685C4 File Offset: 0x000667C4
		// (set) Token: 0x06003523 RID: 13603 RVA: 0x000685DC File Offset: 0x000667DC
		public CompareFunction compareFunctionBack
		{
			get
			{
				return (CompareFunction)this.m_CompareFunctionBack;
			}
			set
			{
				this.m_CompareFunctionBack = (byte)value;
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06003524 RID: 13604 RVA: 0x000685E8 File Offset: 0x000667E8
		// (set) Token: 0x06003525 RID: 13605 RVA: 0x00068600 File Offset: 0x00066800
		public StencilOp passOperationBack
		{
			get
			{
				return (StencilOp)this.m_PassOperationBack;
			}
			set
			{
				this.m_PassOperationBack = (byte)value;
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06003526 RID: 13606 RVA: 0x0006860C File Offset: 0x0006680C
		// (set) Token: 0x06003527 RID: 13607 RVA: 0x00068624 File Offset: 0x00066824
		public StencilOp failOperationBack
		{
			get
			{
				return (StencilOp)this.m_FailOperationBack;
			}
			set
			{
				this.m_FailOperationBack = (byte)value;
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06003528 RID: 13608 RVA: 0x00068630 File Offset: 0x00066830
		// (set) Token: 0x06003529 RID: 13609 RVA: 0x00068648 File Offset: 0x00066848
		public StencilOp zFailOperationBack
		{
			get
			{
				return (StencilOp)this.m_ZFailOperationBack;
			}
			set
			{
				this.m_ZFailOperationBack = (byte)value;
			}
		}

		// Token: 0x0600352A RID: 13610 RVA: 0x00068654 File Offset: 0x00066854
		public bool Equals(StencilState other)
		{
			return this.m_Enabled == other.m_Enabled && this.m_ReadMask == other.m_ReadMask && this.m_WriteMask == other.m_WriteMask && this.m_CompareFunctionFront == other.m_CompareFunctionFront && this.m_PassOperationFront == other.m_PassOperationFront && this.m_FailOperationFront == other.m_FailOperationFront && this.m_ZFailOperationFront == other.m_ZFailOperationFront && this.m_CompareFunctionBack == other.m_CompareFunctionBack && this.m_PassOperationBack == other.m_PassOperationBack && this.m_FailOperationBack == other.m_FailOperationBack && this.m_ZFailOperationBack == other.m_ZFailOperationBack;
		}

		// Token: 0x0600352B RID: 13611 RVA: 0x0006870C File Offset: 0x0006690C
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is StencilState && this.Equals((StencilState)obj);
		}

		// Token: 0x0600352C RID: 13612 RVA: 0x00068744 File Offset: 0x00066944
		public override int GetHashCode()
		{
			int num = this.m_Enabled.GetHashCode();
			num = (num * 397) ^ this.m_ReadMask.GetHashCode();
			num = (num * 397) ^ this.m_WriteMask.GetHashCode();
			num = (num * 397) ^ this.m_CompareFunctionFront.GetHashCode();
			num = (num * 397) ^ this.m_PassOperationFront.GetHashCode();
			num = (num * 397) ^ this.m_FailOperationFront.GetHashCode();
			num = (num * 397) ^ this.m_ZFailOperationFront.GetHashCode();
			num = (num * 397) ^ this.m_CompareFunctionBack.GetHashCode();
			num = (num * 397) ^ this.m_PassOperationBack.GetHashCode();
			num = (num * 397) ^ this.m_FailOperationBack.GetHashCode();
			return (num * 397) ^ this.m_ZFailOperationBack.GetHashCode();
		}

		// Token: 0x0600352D RID: 13613 RVA: 0x0006882C File Offset: 0x00066A2C
		public static bool operator ==(StencilState left, StencilState right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600352E RID: 13614 RVA: 0x00068848 File Offset: 0x00066A48
		public static bool operator !=(StencilState left, StencilState right)
		{
			return !left.Equals(right);
		}

		// Token: 0x040011D7 RID: 4567
		private byte m_Enabled;

		// Token: 0x040011D8 RID: 4568
		private byte m_ReadMask;

		// Token: 0x040011D9 RID: 4569
		private byte m_WriteMask;

		// Token: 0x040011DA RID: 4570
		private byte m_Padding;

		// Token: 0x040011DB RID: 4571
		private byte m_CompareFunctionFront;

		// Token: 0x040011DC RID: 4572
		private byte m_PassOperationFront;

		// Token: 0x040011DD RID: 4573
		private byte m_FailOperationFront;

		// Token: 0x040011DE RID: 4574
		private byte m_ZFailOperationFront;

		// Token: 0x040011DF RID: 4575
		private byte m_CompareFunctionBack;

		// Token: 0x040011E0 RID: 4576
		private byte m_PassOperationBack;

		// Token: 0x040011E1 RID: 4577
		private byte m_FailOperationBack;

		// Token: 0x040011E2 RID: 4578
		private byte m_ZFailOperationBack;
	}
}
