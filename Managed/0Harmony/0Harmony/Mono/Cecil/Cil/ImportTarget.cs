using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002DE RID: 734
	public sealed class ImportTarget
	{
		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x060012AA RID: 4778 RVA: 0x0003CABC File Offset: 0x0003ACBC
		// (set) Token: 0x060012AB RID: 4779 RVA: 0x0003CAC4 File Offset: 0x0003ACC4
		public string Namespace
		{
			get
			{
				return this.@namespace;
			}
			set
			{
				this.@namespace = value;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x060012AC RID: 4780 RVA: 0x0003CACD File Offset: 0x0003ACCD
		// (set) Token: 0x060012AD RID: 4781 RVA: 0x0003CAD5 File Offset: 0x0003ACD5
		public TypeReference Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x060012AE RID: 4782 RVA: 0x0003CADE File Offset: 0x0003ACDE
		// (set) Token: 0x060012AF RID: 4783 RVA: 0x0003CAE6 File Offset: 0x0003ACE6
		public AssemblyNameReference AssemblyReference
		{
			get
			{
				return this.reference;
			}
			set
			{
				this.reference = value;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x060012B0 RID: 4784 RVA: 0x0003CAEF File Offset: 0x0003ACEF
		// (set) Token: 0x060012B1 RID: 4785 RVA: 0x0003CAF7 File Offset: 0x0003ACF7
		public string Alias
		{
			get
			{
				return this.alias;
			}
			set
			{
				this.alias = value;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x060012B2 RID: 4786 RVA: 0x0003CB00 File Offset: 0x0003AD00
		// (set) Token: 0x060012B3 RID: 4787 RVA: 0x0003CB08 File Offset: 0x0003AD08
		public ImportTargetKind Kind
		{
			get
			{
				return this.kind;
			}
			set
			{
				this.kind = value;
			}
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x0003CB11 File Offset: 0x0003AD11
		public ImportTarget(ImportTargetKind kind)
		{
			this.kind = kind;
		}

		// Token: 0x0400097B RID: 2427
		internal ImportTargetKind kind;

		// Token: 0x0400097C RID: 2428
		internal string @namespace;

		// Token: 0x0400097D RID: 2429
		internal TypeReference type;

		// Token: 0x0400097E RID: 2430
		internal AssemblyNameReference reference;

		// Token: 0x0400097F RID: 2431
		internal string alias;
	}
}
