using System;

namespace Mono.Cecil
{
	// Token: 0x02000257 RID: 599
	public sealed class PInvokeInfo
	{
		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000E60 RID: 3680 RVA: 0x0002D898 File Offset: 0x0002BA98
		// (set) Token: 0x06000E61 RID: 3681 RVA: 0x0002D8A0 File Offset: 0x0002BAA0
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

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000E62 RID: 3682 RVA: 0x0002D8A9 File Offset: 0x0002BAA9
		// (set) Token: 0x06000E63 RID: 3683 RVA: 0x0002D8B1 File Offset: 0x0002BAB1
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

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000E64 RID: 3684 RVA: 0x0002D8BA File Offset: 0x0002BABA
		// (set) Token: 0x06000E65 RID: 3685 RVA: 0x0002D8C2 File Offset: 0x0002BAC2
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

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x0002D8CB File Offset: 0x0002BACB
		// (set) Token: 0x06000E67 RID: 3687 RVA: 0x0002D8D9 File Offset: 0x0002BAD9
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

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x0002D8EE File Offset: 0x0002BAEE
		// (set) Token: 0x06000E69 RID: 3689 RVA: 0x0002D8FD File Offset: 0x0002BAFD
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

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x0002D913 File Offset: 0x0002BB13
		// (set) Token: 0x06000E6B RID: 3691 RVA: 0x0002D922 File Offset: 0x0002BB22
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

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x0002D938 File Offset: 0x0002BB38
		// (set) Token: 0x06000E6D RID: 3693 RVA: 0x0002D947 File Offset: 0x0002BB47
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

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000E6E RID: 3694 RVA: 0x0002D95D File Offset: 0x0002BB5D
		// (set) Token: 0x06000E6F RID: 3695 RVA: 0x0002D96C File Offset: 0x0002BB6C
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

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000E70 RID: 3696 RVA: 0x0002D982 File Offset: 0x0002BB82
		// (set) Token: 0x06000E71 RID: 3697 RVA: 0x0002D991 File Offset: 0x0002BB91
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

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000E72 RID: 3698 RVA: 0x0002D9A7 File Offset: 0x0002BBA7
		// (set) Token: 0x06000E73 RID: 3699 RVA: 0x0002D9BE File Offset: 0x0002BBBE
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

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000E74 RID: 3700 RVA: 0x0002D9DC File Offset: 0x0002BBDC
		// (set) Token: 0x06000E75 RID: 3701 RVA: 0x0002D9F3 File Offset: 0x0002BBF3
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

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000E76 RID: 3702 RVA: 0x0002DA11 File Offset: 0x0002BC11
		// (set) Token: 0x06000E77 RID: 3703 RVA: 0x0002DA28 File Offset: 0x0002BC28
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

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000E78 RID: 3704 RVA: 0x0002DA46 File Offset: 0x0002BC46
		// (set) Token: 0x06000E79 RID: 3705 RVA: 0x0002DA5D File Offset: 0x0002BC5D
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

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000E7A RID: 3706 RVA: 0x0002DA7B File Offset: 0x0002BC7B
		// (set) Token: 0x06000E7B RID: 3707 RVA: 0x0002DA92 File Offset: 0x0002BC92
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

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000E7C RID: 3708 RVA: 0x0002DAB0 File Offset: 0x0002BCB0
		// (set) Token: 0x06000E7D RID: 3709 RVA: 0x0002DAC1 File Offset: 0x0002BCC1
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

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000E7E RID: 3710 RVA: 0x0002DAD9 File Offset: 0x0002BCD9
		// (set) Token: 0x06000E7F RID: 3711 RVA: 0x0002DAEA File Offset: 0x0002BCEA
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

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000E80 RID: 3712 RVA: 0x0002DB02 File Offset: 0x0002BD02
		// (set) Token: 0x06000E81 RID: 3713 RVA: 0x0002DB19 File Offset: 0x0002BD19
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

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000E82 RID: 3714 RVA: 0x0002DB37 File Offset: 0x0002BD37
		// (set) Token: 0x06000E83 RID: 3715 RVA: 0x0002DB4E File Offset: 0x0002BD4E
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

		// Token: 0x06000E84 RID: 3716 RVA: 0x0002DB6C File Offset: 0x0002BD6C
		public PInvokeInfo(PInvokeAttributes attributes, string entryPoint, ModuleReference module)
		{
			this.attributes = (ushort)attributes;
			this.entry_point = entryPoint;
			this.module = module;
		}

		// Token: 0x040004AD RID: 1197
		private ushort attributes;

		// Token: 0x040004AE RID: 1198
		private string entry_point;

		// Token: 0x040004AF RID: 1199
		private ModuleReference module;
	}
}
