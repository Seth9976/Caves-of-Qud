using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002DC RID: 732
	public sealed class ConstantDebugInformation : DebugInformation
	{
		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x060012A3 RID: 4771 RVA: 0x0003CA4E File Offset: 0x0003AC4E
		// (set) Token: 0x060012A4 RID: 4772 RVA: 0x0003CA56 File Offset: 0x0003AC56
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x060012A5 RID: 4773 RVA: 0x0003CA5F File Offset: 0x0003AC5F
		// (set) Token: 0x060012A6 RID: 4774 RVA: 0x0003CA67 File Offset: 0x0003AC67
		public TypeReference ConstantType
		{
			get
			{
				return this.constant_type;
			}
			set
			{
				this.constant_type = value;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x060012A7 RID: 4775 RVA: 0x0003CA70 File Offset: 0x0003AC70
		// (set) Token: 0x060012A8 RID: 4776 RVA: 0x0003CA78 File Offset: 0x0003AC78
		public object Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x0003CA81 File Offset: 0x0003AC81
		public ConstantDebugInformation(string name, TypeReference constant_type, object value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.name = name;
			this.constant_type = constant_type;
			this.value = value;
			this.token = new MetadataToken(TokenType.LocalConstant);
		}

		// Token: 0x0400096E RID: 2414
		private string name;

		// Token: 0x0400096F RID: 2415
		private TypeReference constant_type;

		// Token: 0x04000970 RID: 2416
		private object value;
	}
}
