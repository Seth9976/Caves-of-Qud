using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001EB RID: 491
	// (Invoke) Token: 0x0600141E RID: 5150
	internal delegate bool EditCallback<T>(GeneratorEditContext context, T edit) where T : PendingEdit;
}
