using System;

namespace AiUnity.Common.Attributes
{
	// Token: 0x020002E8 RID: 744
	public class EnumSymbolAttribute : Attribute
	{
		// Token: 0x06001982 RID: 6530 RVA: 0x000817D9 File Offset: 0x0007F9D9
		public EnumSymbolAttribute(string enumSymbol)
		{
			this.EnumSymbol = enumSymbol;
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x000817E8 File Offset: 0x0007F9E8
		public override string ToString()
		{
			return this.EnumSymbol;
		}

		// Token: 0x04000EEC RID: 3820
		public readonly string EnumSymbol;
	}
}
