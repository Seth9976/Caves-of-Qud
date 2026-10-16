using System;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x02000114 RID: 276
	public sealed class ImportTarget
	{
		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x00022E8A File Offset: 0x0002108A
		// (set) Token: 0x06000A93 RID: 2707 RVA: 0x00022E92 File Offset: 0x00021092
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

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x00022E9B File Offset: 0x0002109B
		// (set) Token: 0x06000A95 RID: 2709 RVA: 0x00022EA3 File Offset: 0x000210A3
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

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000A96 RID: 2710 RVA: 0x00022EAC File Offset: 0x000210AC
		// (set) Token: 0x06000A97 RID: 2711 RVA: 0x00022EB4 File Offset: 0x000210B4
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

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x00022EBD File Offset: 0x000210BD
		// (set) Token: 0x06000A99 RID: 2713 RVA: 0x00022EC5 File Offset: 0x000210C5
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

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x00022ECE File Offset: 0x000210CE
		// (set) Token: 0x06000A9B RID: 2715 RVA: 0x00022ED6 File Offset: 0x000210D6
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

		// Token: 0x06000A9C RID: 2716 RVA: 0x00022EDF File Offset: 0x000210DF
		public ImportTarget(ImportTargetKind kind)
		{
			this.kind = kind;
		}

		// Token: 0x04000566 RID: 1382
		internal ImportTargetKind kind;

		// Token: 0x04000567 RID: 1383
		internal string @namespace;

		// Token: 0x04000568 RID: 1384
		internal TypeReference type;

		// Token: 0x04000569 RID: 1385
		internal AssemblyNameReference reference;

		// Token: 0x0400056A RID: 1386
		internal string alias;
	}
}
