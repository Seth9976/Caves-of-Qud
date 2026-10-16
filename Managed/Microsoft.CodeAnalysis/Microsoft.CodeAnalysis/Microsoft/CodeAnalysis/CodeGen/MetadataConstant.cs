using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Cci;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004D0 RID: 1232
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class MetadataConstant : IMetadataExpression
	{
		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x06003190 RID: 12688 RVA: 0x0008DE0B File Offset: 0x0008C00B
		public ITypeReference Type { get; }

		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x06003191 RID: 12689 RVA: 0x0008DE13 File Offset: 0x0008C013
		[Nullable(2)]
		public object Value
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x06003192 RID: 12690 RVA: 0x0008DE1B File Offset: 0x0008C01B
		public MetadataConstant(ITypeReference type, [Nullable(2)] object value)
		{
			this.Type = type;
			this.Value = value;
		}

		// Token: 0x06003193 RID: 12691 RVA: 0x0008DE31 File Offset: 0x0008C031
		void IMetadataExpression.Dispatch(MetadataVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06003194 RID: 12692 RVA: 0x0008DE3A File Offset: 0x0008C03A
		[NullableContext(2)]
		[Conditional("DEBUG")]
		internal static void AssertValidConstant(object value)
		{
		}
	}
}
