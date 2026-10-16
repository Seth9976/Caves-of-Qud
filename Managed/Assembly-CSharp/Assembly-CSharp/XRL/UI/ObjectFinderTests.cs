using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using XRL.World;

namespace XRL.UI
{
	// Token: 0x0200147E RID: 5246
	public class ObjectFinderTests
	{
		// Token: 0x0600CE91 RID: 52881 RVA: 0x004B227C File Offset: 0x004B047C
		[TestCase(new object[] { })]
		public void TestCaseOne()
		{
			ObjectFinder objectFinder = ObjectFinder.Reset();
			ObjectFinder.Context context = new ObjectFinderTests.TestContext(objectFinder);
			List<GameObject> list = new List<GameObject>
			{
				new GameObject(),
				new GameObject(),
				new GameObject()
			};
			list[0].DisplayName = "One";
			list[1].DisplayName = "two";
			list[2].DisplayName = "a definitely unexpected three";
			objectFinder.UpdateContext(context, list);
			objectFinder.UpdateFilter(false);
			List<GameObject> list2 = new List<GameObject>(from x in objectFinder.readItems(false)
				select x.Item1);
			Assert.AreEqual(list[2].DisplayName, list2[0].DisplayName);
		}

		// Token: 0x02001D98 RID: 7576
		public class TestContext : ObjectFinder.Context
		{
			// Token: 0x0600F4BC RID: 62652 RVA: 0x00553C16 File Offset: 0x00551E16
			public TestContext(ObjectFinder finder)
				: base(finder)
			{
			}

			// Token: 0x0600F4BD RID: 62653 RVA: 0x00553C1F File Offset: 0x00551E1F
			public override string GetDisplayName()
			{
				return "TestContext";
			}
		}
	}
}
