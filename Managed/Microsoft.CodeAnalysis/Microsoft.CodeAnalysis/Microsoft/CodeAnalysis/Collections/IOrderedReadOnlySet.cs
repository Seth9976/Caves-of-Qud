using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Collections
{
	// Token: 0x0200049D RID: 1181
	internal interface IOrderedReadOnlySet<[Nullable(2)] T> : IReadOnlySet<T>, IReadOnlyList<T>, IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>
	{
	}
}
