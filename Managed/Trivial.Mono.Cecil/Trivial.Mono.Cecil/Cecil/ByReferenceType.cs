using System;
using Trivial.Mono.Cecil.Metadata;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200001A RID: 26
	public sealed class ByReferenceType : TypeSpecification
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00004039 File Offset: 0x00002239
		public override string Name
		{
			get
			{
				return base.Name + "&";
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x0000404B File Offset: 0x0000224B
		public override string FullName
		{
			get
			{
				return base.FullName + "&";
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x000025EF File Offset: 0x000007EF
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00002B65 File Offset: 0x00000D65
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

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public override bool IsByReference
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000405D File Offset: 0x0000225D
		public ByReferenceType(TypeReference type)
			: base(type)
		{
			Mixin.CheckType(type);
			this.etype = Trivial.Mono.Cecil.Metadata.ElementType.ByRef;
		}
	}
}
