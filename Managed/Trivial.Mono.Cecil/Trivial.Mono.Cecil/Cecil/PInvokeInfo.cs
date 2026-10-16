using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200009F RID: 159
	public sealed class PInvokeInfo
	{
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x000148BA File Offset: 0x00012ABA
		// (set) Token: 0x0600068C RID: 1676 RVA: 0x000148C2 File Offset: 0x00012AC2
		public PInvokeAttributes Attributes
		{
			get
			{
				return (PInvokeAttributes)this.attributes;
			}
			set
			{
				this.attributes = (ushort)value;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x000148CB File Offset: 0x00012ACB
		// (set) Token: 0x0600068E RID: 1678 RVA: 0x000148D3 File Offset: 0x00012AD3
		public string EntryPoint
		{
			get
			{
				return this.entry_point;
			}
			set
			{
				this.entry_point = value;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x000148DC File Offset: 0x00012ADC
		// (set) Token: 0x06000690 RID: 1680 RVA: 0x000148E4 File Offset: 0x00012AE4
		public ModuleReference Module
		{
			get
			{
				return this.module;
			}
			set
			{
				this.module = value;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x000148ED File Offset: 0x00012AED
		// (set) Token: 0x06000692 RID: 1682 RVA: 0x000148FB File Offset: 0x00012AFB
		public bool IsNoMangle
		{
			get
			{
				return this.attributes.GetAttributes(1);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(1, value);
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x00014910 File Offset: 0x00012B10
		// (set) Token: 0x06000694 RID: 1684 RVA: 0x0001491F File Offset: 0x00012B1F
		public bool IsCharSetNotSpec
		{
			get
			{
				return this.attributes.GetMaskedAttributes(6, 0U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(6, 0U, value);
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x00014935 File Offset: 0x00012B35
		// (set) Token: 0x06000696 RID: 1686 RVA: 0x00014944 File Offset: 0x00012B44
		public bool IsCharSetAnsi
		{
			get
			{
				return this.attributes.GetMaskedAttributes(6, 2U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(6, 2U, value);
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x0001495A File Offset: 0x00012B5A
		// (set) Token: 0x06000698 RID: 1688 RVA: 0x00014969 File Offset: 0x00012B69
		public bool IsCharSetUnicode
		{
			get
			{
				return this.attributes.GetMaskedAttributes(6, 4U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(6, 4U, value);
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x0001497F File Offset: 0x00012B7F
		// (set) Token: 0x0600069A RID: 1690 RVA: 0x0001498E File Offset: 0x00012B8E
		public bool IsCharSetAuto
		{
			get
			{
				return this.attributes.GetMaskedAttributes(6, 6U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(6, 6U, value);
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x000149A4 File Offset: 0x00012BA4
		// (set) Token: 0x0600069C RID: 1692 RVA: 0x000149B3 File Offset: 0x00012BB3
		public bool SupportsLastError
		{
			get
			{
				return this.attributes.GetAttributes(64);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(64, value);
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x000149C9 File Offset: 0x00012BC9
		// (set) Token: 0x0600069E RID: 1694 RVA: 0x000149E0 File Offset: 0x00012BE0
		public bool IsCallConvWinapi
		{
			get
			{
				return this.attributes.GetMaskedAttributes(1792, 256U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(1792, 256U, value);
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x000149FE File Offset: 0x00012BFE
		// (set) Token: 0x060006A0 RID: 1696 RVA: 0x00014A15 File Offset: 0x00012C15
		public bool IsCallConvCdecl
		{
			get
			{
				return this.attributes.GetMaskedAttributes(1792, 512U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(1792, 512U, value);
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x00014A33 File Offset: 0x00012C33
		// (set) Token: 0x060006A2 RID: 1698 RVA: 0x00014A4A File Offset: 0x00012C4A
		public bool IsCallConvStdCall
		{
			get
			{
				return this.attributes.GetMaskedAttributes(1792, 768U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(1792, 768U, value);
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x00014A68 File Offset: 0x00012C68
		// (set) Token: 0x060006A4 RID: 1700 RVA: 0x00014A7F File Offset: 0x00012C7F
		public bool IsCallConvThiscall
		{
			get
			{
				return this.attributes.GetMaskedAttributes(1792, 1024U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(1792, 1024U, value);
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x00014A9D File Offset: 0x00012C9D
		// (set) Token: 0x060006A6 RID: 1702 RVA: 0x00014AB4 File Offset: 0x00012CB4
		public bool IsCallConvFastcall
		{
			get
			{
				return this.attributes.GetMaskedAttributes(1792, 1280U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(1792, 1280U, value);
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x00014AD2 File Offset: 0x00012CD2
		// (set) Token: 0x060006A8 RID: 1704 RVA: 0x00014AE3 File Offset: 0x00012CE3
		public bool IsBestFitEnabled
		{
			get
			{
				return this.attributes.GetMaskedAttributes(48, 16U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(48, 16U, value);
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x00014AFB File Offset: 0x00012CFB
		// (set) Token: 0x060006AA RID: 1706 RVA: 0x00014B0C File Offset: 0x00012D0C
		public bool IsBestFitDisabled
		{
			get
			{
				return this.attributes.GetMaskedAttributes(48, 32U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(48, 32U, value);
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x00014B24 File Offset: 0x00012D24
		// (set) Token: 0x060006AC RID: 1708 RVA: 0x00014B3B File Offset: 0x00012D3B
		public bool IsThrowOnUnmappableCharEnabled
		{
			get
			{
				return this.attributes.GetMaskedAttributes(12288, 4096U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(12288, 4096U, value);
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x00014B59 File Offset: 0x00012D59
		// (set) Token: 0x060006AE RID: 1710 RVA: 0x00014B70 File Offset: 0x00012D70
		public bool IsThrowOnUnmappableCharDisabled
		{
			get
			{
				return this.attributes.GetMaskedAttributes(12288, 8192U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(12288, 8192U, value);
			}
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00014B8E File Offset: 0x00012D8E
		public PInvokeInfo(PInvokeAttributes attributes, string entryPoint, ModuleReference module)
		{
			this.attributes = (ushort)attributes;
			this.entry_point = entryPoint;
			this.module = module;
		}

		// Token: 0x0400022F RID: 559
		private ushort attributes;

		// Token: 0x04000230 RID: 560
		private string entry_point;

		// Token: 0x04000231 RID: 561
		private ModuleReference module;
	}
}
