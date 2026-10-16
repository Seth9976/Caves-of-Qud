using System;
using NUnit.Framework;
using UnityEngine;

namespace XRL.World
{
	// Token: 0x02000881 RID: 2177
	public class PopulationManagerTest
	{
		// Token: 0x06005657 RID: 22103 RVA: 0x0019AD90 File Offset: 0x00198F90
		[TestCase("Tier1", 1, 1)]
		[TestCase("Tier2", 1, 2)]
		[TestCase("Tier3", 1, 3)]
		[TestCase("Tier1", 2, 1)]
		[TestCase("Tier2", 2, 2)]
		[TestCase("Tier3", 2, 3)]
		[TestCase("Tier3", 2, 3)]
		[TestCase("Tier{zonetier}", 1, 1)]
		[TestCase("Tier{zonetier}", 2, 2)]
		[TestCase("Tier{zonetier}", 3, 3)]
		[TestCase("Tier{zonetier+1}", 1, 2)]
		[TestCase("Tier{zonetier+2}", 1, 3)]
		[TestCase("Tier{zonetier+3}", 1, 4)]
		[TestCase("Tier{zonetier+1}", 7, 8)]
		[TestCase("Tier{zonetier+1}", 8, 8)]
		[TestCase("Tier{zonetier-1}", 2, 1)]
		[TestCase("Tier{zonetier-1}", 1, 1)]
		[TestCase("Tier{zonetier-2}", 1, 1)]
		[TestCase("Tier{zonetier-2}", 3, 1)]
		[TestCase("Tier{zonetier-2}", 4, 2)]
		public void ResolveTier(string spec, int zoneTier, int expected)
		{
			ZoneManager.zoneGenerationContextTier = zoneTier;
			int num;
			int num2;
			Assert.IsTrue(PopulationManager.ResolveTier(spec, out num, out num2));
			Assert.AreEqual(expected, num);
			Assert.AreEqual(expected, num2);
		}

		// Token: 0x06005658 RID: 22104 RVA: 0x0019ADD4 File Offset: 0x00198FD4
		[TestCase(new object[] { "Tier1-2", 1, 1, 2 })]
		[TestCase(new object[] { "Tier1-3", 1, 1, 3 })]
		[TestCase(new object[] { "Tier1-4", 1, 1, 4 })]
		[TestCase(new object[] { "Tier2-4", 1, 2, 4 })]
		[TestCase(new object[] { "Tier3-7", 1, 3, 7 })]
		[TestCase(new object[] { "Tier1-{zonetier+1}", 2, 1, 3 })]
		[TestCase(new object[] { "Tier{zonetier}-{zonetier+1}", 1, 1, 2 })]
		[TestCase(new object[] { "Tier{zonetier}-{zonetier+2}", 1, 1, 3 })]
		[TestCase(new object[] { "Tier{zonetier-1}-{zonetier+1}", 3, 2, 4 })]
		[TestCase(new object[] { "Tier{zonetier-1}-{zonetier+2}", 3, 2, 5 })]
		[TestCase(new object[] { "Tier{zonetier-2}-{zonetier+4}", 4, 2, 8 })]
		[TestCase(new object[] { "Tier{zonetier+3}-7", 2, 5, 7 })]
		[TestCase(new object[] { "Tier{zonetier} - {zonetier+1}", 3, 3, 4 })]
		public void ResolveTierRange(string spec, int zoneTier, int expectedLow, int expectedHigh)
		{
			ZoneManager.zoneGenerationContextTier = zoneTier;
			int num;
			int num2;
			Assert.IsTrue(PopulationManager.ResolveTier(spec, out num, out num2));
			Assert.AreEqual(expectedLow, num);
			Assert.AreEqual(expectedHigh, num2);
		}

		// Token: 0x06005659 RID: 22105 RVA: 0x0019AE1C File Offset: 0x0019901C
		[TestCase("TierX", LogType.Error, "Bad tier specification")]
		[TestCase("TierX-2", LogType.Error, "Bad low tier specification")]
		[TestCase("Tier2-X", LogType.Error, "Bad high tier specification")]
		[TestCase("1", LogType.Error, "Tier specification does not start with Tier")]
		[TestCase("1-2", LogType.Error, "Tier specification does not start with Tier")]
		[TestCase("Tier{zonetier/2}", LogType.Error, "Bad zone tier offset")]
		[TestCase("Tier{zonetier*2}", LogType.Error, "Bad zone tier offset")]
		[TestCase("Tier{zonetier + 1}", LogType.Error, "Bad zone tier offset")]
		[TestCase("Tier{zonetier - 1}", LogType.Error, "Bad zone tier offset")]
		[TestCase("Tier{zonetier} * {zonetier+1}", LogType.Error, "Bad tier specification")]
		public void ResolveTierBadFormats(string spec, LogType type, string Message)
		{
			ZoneManager.zoneGenerationContextTier = 1;
			int num;
			int num2;
			Assert.IsFalse(PopulationManager.ResolveTier(spec, out num, out num2));
		}
	}
}
