using System;
using System.Reflection;

namespace Microsoft.CSharp
{
	// Token: 0x02000142 RID: 322
	internal sealed class CSharpTypeAttributeConverter : CSharpModifierAttributeConverter
	{
		// Token: 0x0600088B RID: 2187 RVA: 0x0001E76C File Offset: 0x0001C96C
		private CSharpTypeAttributeConverter()
		{
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x0001E7B9 File Offset: 0x0001C9B9
		public static CSharpTypeAttributeConverter Default { get; } = new CSharpTypeAttributeConverter();

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x0001E7C0 File Offset: 0x0001C9C0
		protected override string[] Names { get; } = new string[] { "Public", "Internal" };

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600088E RID: 2190 RVA: 0x0001E7C8 File Offset: 0x0001C9C8
		protected override object[] Values { get; } = new object[]
		{
			TypeAttributes.Public,
			TypeAttributes.NotPublic
		};

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x0001E7D0 File Offset: 0x0001C9D0
		protected override object DefaultValue
		{
			get
			{
				return TypeAttributes.NotPublic;
			}
		}
	}
}
