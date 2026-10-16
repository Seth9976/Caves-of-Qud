using System;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x020000F9 RID: 249
	public sealed class ConstantDebugInformation : DebugInformation
	{
		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x000223BD File Offset: 0x000205BD
		// (set) Token: 0x06000A0C RID: 2572 RVA: 0x000223C5 File Offset: 0x000205C5
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

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x000223CE File Offset: 0x000205CE
		// (set) Token: 0x06000A0E RID: 2574 RVA: 0x000223D6 File Offset: 0x000205D6
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

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000A0F RID: 2575 RVA: 0x000223DF File Offset: 0x000205DF
		// (set) Token: 0x06000A10 RID: 2576 RVA: 0x000223E7 File Offset: 0x000205E7
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

		// Token: 0x06000A11 RID: 2577 RVA: 0x000223F0 File Offset: 0x000205F0
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

		// Token: 0x04000508 RID: 1288
		private string name;

		// Token: 0x04000509 RID: 1289
		private TypeReference constant_type;

		// Token: 0x0400050A RID: 1290
		private object value;
	}
}
