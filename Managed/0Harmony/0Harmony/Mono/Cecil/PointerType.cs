using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x02000258 RID: 600
	internal sealed class PointerType : TypeSpecification
	{
		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000E85 RID: 3717 RVA: 0x0002DB89 File Offset: 0x0002BD89
		public override string Name
		{
			get
			{
				return base.Name + "*";
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000E86 RID: 3718 RVA: 0x0002DB9B File Offset: 0x0002BD9B
		public override string FullName
		{
			get
			{
				return base.FullName + "*";
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000E87 RID: 3719 RVA: 0x00018318 File Offset: 0x00016518
		// (set) Token: 0x06000E88 RID: 3720 RVA: 0x000189AE File Offset: 0x00016BAE
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

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000E89 RID: 3721 RVA: 0x00018941 File Offset: 0x00016B41
		public override bool IsPointer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x0002DBAD File Offset: 0x0002BDAD
		public PointerType(TypeReference type)
			: base(type)
		{
			Mixin.CheckType(type);
			this.etype = Mono.Cecil.Metadata.ElementType.Ptr;
		}
	}
}
