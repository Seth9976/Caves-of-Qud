using System;
using System.Reflection;

namespace Microsoft.VisualBasic
{
	// Token: 0x0200013C RID: 316
	internal sealed class VBTypeAttributeConverter : VBModifierAttributeConverter
	{
		// Token: 0x060007CA RID: 1994 RVA: 0x0001853C File Offset: 0x0001673C
		private VBTypeAttributeConverter()
		{
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060007CB RID: 1995 RVA: 0x00018589 File Offset: 0x00016789
		public static VBTypeAttributeConverter Default { get; } = new VBTypeAttributeConverter();

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x00018590 File Offset: 0x00016790
		protected override string[] Names { get; } = new string[] { "Public", "Friend" };

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x00018598 File Offset: 0x00016798
		protected override object[] Values { get; } = new object[]
		{
			TypeAttributes.Public,
			TypeAttributes.NotPublic
		};

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x000185A0 File Offset: 0x000167A0
		protected override object DefaultValue
		{
			get
			{
				return TypeAttributes.Public;
			}
		}
	}
}
