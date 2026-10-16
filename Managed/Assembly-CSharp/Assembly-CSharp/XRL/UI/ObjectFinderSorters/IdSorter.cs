using System;
using System.Runtime.CompilerServices;
using XRL.World;

namespace XRL.UI.ObjectFinderSorters
{
	// Token: 0x020014DB RID: 5339
	public class IdSorter : ObjectFinder.Sorter
	{
		// Token: 0x0600D102 RID: 53506 RVA: 0x004DA514 File Offset: 0x004D8714
		public override string GetDisplayName()
		{
			return "Id";
		}

		// Token: 0x0600D103 RID: 53507 RVA: 0x004DA51B File Offset: 0x004D871B
		public override int Compare([TupleElementNames(new string[] { "go", "context" })] ValueTuple<GameObject, ObjectFinder.Context> a, [TupleElementNames(new string[] { "go", "context" })] ValueTuple<GameObject, ObjectFinder.Context> b)
		{
			return string.Compare(a.Item1.HasID ? a.Item1.ID : null, b.Item1.HasID ? b.Item1.ID : null);
		}
	}
}
