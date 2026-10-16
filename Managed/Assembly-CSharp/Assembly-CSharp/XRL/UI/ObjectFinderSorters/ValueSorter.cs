using System;
using System.Runtime.CompilerServices;
using XRL.UI.ObjectFinderContexts;
using XRL.World;

namespace XRL.UI.ObjectFinderSorters
{
	// Token: 0x020014DC RID: 5340
	public class ValueSorter : ObjectFinder.Sorter
	{
		// Token: 0x0600D105 RID: 53509 RVA: 0x004DA560 File Offset: 0x004D8760
		public override string GetDisplayName()
		{
			return "Value";
		}

		// Token: 0x0600D106 RID: 53510 RVA: 0x004DA568 File Offset: 0x004D8768
		public override int Compare([TupleElementNames(new string[] { "go", "context" })] ValueTuple<GameObject, ObjectFinder.Context> a, [TupleElementNames(new string[] { "go", "context" })] ValueTuple<GameObject, ObjectFinder.Context> b)
		{
			if (a.Item2 != b.Item2)
			{
				if (a.Item2 is AutogotItems)
				{
					return -1;
				}
				if (b.Item2 is AutogotItems)
				{
					return 1;
				}
			}
			return b.Item1.ValueEach.CompareTo(a.Item1.ValueEach);
		}
	}
}
