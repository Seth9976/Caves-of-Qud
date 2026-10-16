using System;

namespace System.Web.Services.Configuration
{
	// Token: 0x02000147 RID: 327
	internal class TypeAndName
	{
		// Token: 0x060009F6 RID: 2550 RVA: 0x00043D81 File Offset: 0x00041F81
		public TypeAndName(string name)
		{
			this.type = Type.GetType(name, true, true);
			this.name = name;
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00043D9E File Offset: 0x00041F9E
		public TypeAndName(Type type)
		{
			this.type = type;
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00043DAD File Offset: 0x00041FAD
		public override int GetHashCode()
		{
			return this.type.GetHashCode();
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00043DBA File Offset: 0x00041FBA
		public override bool Equals(object comparand)
		{
			return this.type.Equals(((TypeAndName)comparand).type);
		}

		// Token: 0x040005B1 RID: 1457
		public readonly Type type;

		// Token: 0x040005B2 RID: 1458
		public readonly string name;
	}
}
