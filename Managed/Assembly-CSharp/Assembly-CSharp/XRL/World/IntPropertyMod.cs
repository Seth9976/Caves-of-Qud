using System;

namespace XRL.World
{
	// Token: 0x02000959 RID: 2393
	public struct IntPropertyMod : IDisposable
	{
		// Token: 0x060062EB RID: 25323 RVA: 0x001E601F File Offset: 0x001E421F
		public IntPropertyMod(GameObject Object, string Property, int Value)
		{
			this.Object = Object;
			this.Property = Property;
			this.Value = Value;
			Object.ModIntProperty(Property, Value, true);
		}

		// Token: 0x060062EC RID: 25324 RVA: 0x001E6040 File Offset: 0x001E4240
		public void Dispose()
		{
			if (this.Value == 0)
			{
				return;
			}
			this.Object.ModIntProperty(this.Property, this.Value * -1, true);
			this.Value = 0;
		}

		// Token: 0x04002E67 RID: 11879
		public GameObject Object;

		// Token: 0x04002E68 RID: 11880
		public string Property;

		// Token: 0x04002E69 RID: 11881
		public int Value;
	}
}
