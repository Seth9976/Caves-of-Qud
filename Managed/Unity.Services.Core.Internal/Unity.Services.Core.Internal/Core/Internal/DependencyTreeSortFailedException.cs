using System;
using System.Collections.Generic;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000041 RID: 65
	internal class DependencyTreeSortFailedException : Exception
	{
		// Token: 0x0600011C RID: 284 RVA: 0x00003034 File Offset: 0x00001234
		public DependencyTreeSortFailedException(DependencyTree tree, ICollection<int> target)
			: base(DependencyTreeSortFailedException.CreateExceptionMessage(tree, target, null))
		{
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00003044 File Offset: 0x00001244
		public DependencyTreeSortFailedException(DependencyTree tree, ICollection<int> target, Exception inner)
			: base(DependencyTreeSortFailedException.CreateExceptionMessage(tree, target, inner), inner)
		{
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00003058 File Offset: 0x00001258
		private static string CreateExceptionMessage(DependencyTree tree, ICollection<int> target, Exception inner = null)
		{
			string text = tree.ToJson(target);
			return "Failed to sort tree! It is likely there is a missing required dependency:\n" + text + ((inner != null) ? ("\n Error: " + inner.Message) : string.Empty);
		}
	}
}
