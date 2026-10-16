using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000187 RID: 391
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class DocumentationProvider
	{
		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001027 RID: 4135 RVA: 0x0002F21A File Offset: 0x0002D41A
		public static DocumentationProvider Default { get; } = new DocumentationProvider.NullDocumentationProvider();

		// Token: 0x06001029 RID: 4137
		[return: Nullable(2)]
		protected internal abstract string GetDocumentationForSymbol(string documentationMemberID, CultureInfo preferredCulture, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x0600102A RID: 4138
		[NullableContext(2)]
		public abstract override bool Equals(object obj);

		// Token: 0x0600102B RID: 4139
		public abstract override int GetHashCode();

		// Token: 0x020005AF RID: 1455
		[NullableContext(0)]
		private class NullDocumentationProvider : DocumentationProvider
		{
			// Token: 0x060035EA RID: 13802 RVA: 0x0009813C File Offset: 0x0009633C
			[NullableContext(1)]
			protected internal override string GetDocumentationForSymbol(string documentationMemberID, CultureInfo preferredCulture, CancellationToken cancellationToken = default(CancellationToken))
			{
				return "";
			}

			// Token: 0x060035EB RID: 13803 RVA: 0x00098143 File Offset: 0x00096343
			[NullableContext(2)]
			public override bool Equals(object obj)
			{
				return this == obj;
			}

			// Token: 0x060035EC RID: 13804 RVA: 0x00098149 File Offset: 0x00096349
			public override int GetHashCode()
			{
				return RuntimeHelpers.GetHashCode(this);
			}
		}
	}
}
