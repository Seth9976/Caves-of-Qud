using System;
using Trivial.Mono.Cecil.Metadata;

namespace Trivial.Mono.Cecil
{
	// Token: 0x020000A0 RID: 160
	public sealed class PointerType : TypeSpecification
	{
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x00014BAB File Offset: 0x00012DAB
		public override string Name
		{
			get
			{
				return base.Name + "*";
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x00014BBD File Offset: 0x00012DBD
		public override string FullName
		{
			get
			{
				return base.FullName + "*";
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x000025EF File Offset: 0x000007EF
		// (set) Token: 0x060006B3 RID: 1715 RVA: 0x00002B65 File Offset: 0x00000D65
		public override bool IsValueType
		{
			get
			{
				return false;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public override bool IsPointer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00014BCF File Offset: 0x00012DCF
		public PointerType(TypeReference type)
			: base(type)
		{
			Mixin.CheckType(type);
			this.etype = Trivial.Mono.Cecil.Metadata.ElementType.Ptr;
		}
	}
}
