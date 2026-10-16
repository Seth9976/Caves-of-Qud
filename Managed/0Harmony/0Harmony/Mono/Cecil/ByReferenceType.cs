using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x0200025D RID: 605
	internal sealed class ByReferenceType : TypeSpecification
	{
		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000EB4 RID: 3764 RVA: 0x0002E066 File Offset: 0x0002C266
		public override string Name
		{
			get
			{
				return base.Name + "&";
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000EB5 RID: 3765 RVA: 0x0002E078 File Offset: 0x0002C278
		public override string FullName
		{
			get
			{
				return base.FullName + "&";
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x00018318 File Offset: 0x00016518
		// (set) Token: 0x06000EB7 RID: 3767 RVA: 0x000189AE File Offset: 0x00016BAE
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

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x00018941 File Offset: 0x00016B41
		public override bool IsByReference
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x0002E08A File Offset: 0x0002C28A
		public ByReferenceType(TypeReference type)
			: base(type)
		{
			Mixin.CheckType(type);
			this.etype = Mono.Cecil.Metadata.ElementType.ByRef;
		}
	}
}
