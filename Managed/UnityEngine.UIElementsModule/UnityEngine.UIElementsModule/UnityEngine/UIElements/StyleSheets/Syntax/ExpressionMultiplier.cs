using System;

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
	// Token: 0x020006FA RID: 1786
	internal struct ExpressionMultiplier
	{
		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x06003376 RID: 13174 RVA: 0x000C8BAC File Offset: 0x000C6DAC
		// (set) Token: 0x06003377 RID: 13175 RVA: 0x000C8BC4 File Offset: 0x000C6DC4
		public ExpressionMultiplierType type
		{
			get
			{
				return this.m_Type;
			}
			set
			{
				this.SetType(value);
			}
		}

		// Token: 0x06003378 RID: 13176 RVA: 0x000C8BD0 File Offset: 0x000C6DD0
		public ExpressionMultiplier(ExpressionMultiplierType type = ExpressionMultiplierType.None)
		{
			this.m_Type = type;
			this.min = (this.max = 1);
			this.SetType(type);
		}

		// Token: 0x06003379 RID: 13177 RVA: 0x000C8C00 File Offset: 0x000C6E00
		private void SetType(ExpressionMultiplierType value)
		{
			this.m_Type = value;
			switch (value)
			{
			case ExpressionMultiplierType.ZeroOrMore:
				this.min = 0;
				this.max = 100;
				return;
			case ExpressionMultiplierType.OneOrMore:
			case ExpressionMultiplierType.OneOrMoreComma:
			case ExpressionMultiplierType.GroupAtLeastOne:
				this.min = 1;
				this.max = 100;
				return;
			case ExpressionMultiplierType.ZeroOrOne:
				this.min = 0;
				this.max = 1;
				return;
			}
			this.min = (this.max = 1);
		}

		// Token: 0x04001A67 RID: 6759
		public const int Infinity = 100;

		// Token: 0x04001A68 RID: 6760
		private ExpressionMultiplierType m_Type;

		// Token: 0x04001A69 RID: 6761
		public int min;

		// Token: 0x04001A6A RID: 6762
		public int max;
	}
}
