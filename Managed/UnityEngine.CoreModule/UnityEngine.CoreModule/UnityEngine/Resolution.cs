using System;
using System.ComponentModel;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000177 RID: 375
	[RequiredByNativeCode]
	public struct Resolution
	{
		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x00019580 File Offset: 0x00017780
		// (set) Token: 0x06000DA2 RID: 3490 RVA: 0x00019598 File Offset: 0x00017798
		public int width
		{
			get
			{
				return this.m_Width;
			}
			set
			{
				this.m_Width = value;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x000195A4 File Offset: 0x000177A4
		// (set) Token: 0x06000DA4 RID: 3492 RVA: 0x000195BC File Offset: 0x000177BC
		public int height
		{
			get
			{
				return this.m_Height;
			}
			set
			{
				this.m_Height = value;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x000195C8 File Offset: 0x000177C8
		// (set) Token: 0x06000DA6 RID: 3494 RVA: 0x000195E0 File Offset: 0x000177E0
		public RefreshRate refreshRateRatio
		{
			get
			{
				return this.m_RefreshRate;
			}
			set
			{
				this.m_RefreshRate = value;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x000195EC File Offset: 0x000177EC
		// (set) Token: 0x06000DA8 RID: 3496 RVA: 0x0001960F File Offset: 0x0001780F
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Resolution.refreshRate is obsolete. Use refreshRateRatio instead.", false)]
		public int refreshRate
		{
			get
			{
				return (int)Math.Round(this.m_RefreshRate.value);
			}
			set
			{
				this.m_RefreshRate.numerator = (uint)value;
				this.m_RefreshRate.denominator = 1U;
			}
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0001962C File Offset: 0x0001782C
		public override string ToString()
		{
			return UnityString.Format("{0} x {1} @ {2}Hz", new object[] { this.m_Width, this.m_Height, this.m_RefreshRate });
		}

		// Token: 0x040004CA RID: 1226
		private int m_Width;

		// Token: 0x040004CB RID: 1227
		private int m_Height;

		// Token: 0x040004CC RID: 1228
		private RefreshRate m_RefreshRate;
	}
}
