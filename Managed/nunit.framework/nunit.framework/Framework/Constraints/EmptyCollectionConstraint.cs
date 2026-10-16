using System;
using System.Collections;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000103 RID: 259
	public class EmptyCollectionConstraint : CollectionConstraint
	{
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x000139B7 File Offset: 0x00011BB7
		public override string Description
		{
			get
			{
				return "<empty>";
			}
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x000139BE File Offset: 0x00011BBE
		protected override bool Matches(IEnumerable collection)
		{
			return CollectionConstraint.IsEmpty(collection);
		}
	}
}
