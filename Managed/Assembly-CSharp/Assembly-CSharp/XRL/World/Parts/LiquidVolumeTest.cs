using System;
using NUnit.Framework;

namespace XRL.World.Parts
{
	// Token: 0x02000DD3 RID: 3539
	public class LiquidVolumeTest
	{
		// Token: 0x06009017 RID: 36887 RVA: 0x0033FA8C File Offset: 0x0033DC8C
		[TestCase(new object[] { "water", 1, "water", 1, "water" })]
		[TestCase(new object[] { "water", 1, "salt", 1, "water-500,salt-500" })]
		[TestCase(new object[] { "water-500,salt-500", 10, "blood", 5, "water-334,salt-333,blood-333" })]
		[TestCase(new object[] { "water-500,salt-500", 10, "water-500,salt-500", -1, "water-500,salt-500" })]
		[TestCase(new object[] { "water-500,salt-500", 10, "water", -5, "salt" })]
		[TestCase(new object[] { "water-500,salt-500", 20, "water", -5, "water-333,salt-667" })]
		[TestCase(new object[] { "water-333,salt-667", 15, "water", 5, "water-499,salt-501" })]
		[TestCase(new object[] { "water", 1, "salt", -1, null })]
		public void MixWith(string dstLiquid, int dstAmount, string srcLiquid, int srcAmount, string resLiquid)
		{
			this.dstVol.InitialLiquid = dstLiquid;
			this.dstVol.Volume = dstAmount;
			this.srcVol.InitialLiquid = srcLiquid;
			this.srcVol.Volume = srcAmount;
			this.dstVol.MixWith(this.srcVol, null, null, null, false);
			Assert.AreEqual(resLiquid, this.dstVol.GetLiquidDesignation(false));
			Assert.AreEqual(dstAmount + srcAmount, this.dstVol.Volume);
		}

		// Token: 0x06009018 RID: 36888 RVA: 0x0033FB1C File Offset: 0x0033DD1C
		[TestCase(new object[] { "water", 2, 1, "water" })]
		[TestCase(new object[] { "oil-500,water-500", 2, 1, "oil" })]
		public void UseDramsByEvaporativity(string liquid, int amount, int useAmount, string resLiquid)
		{
			this.dstVol.InitialLiquid = liquid;
			this.dstVol.Volume = amount;
			this.dstVol.UseDramsByEvaporativity(useAmount);
			Assert.AreEqual(resLiquid, this.dstVol.GetLiquidDesignation(false));
			Assert.AreEqual(amount - useAmount, this.dstVol.Volume);
		}

		// Token: 0x04003C1B RID: 15387
		public LiquidVolume dstVol = new LiquidVolume();

		// Token: 0x04003C1C RID: 15388
		public LiquidVolume srcVol = new LiquidVolume();
	}
}
