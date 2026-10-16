using System;
using System.ComponentModel.Design;
using System.Reflection;

namespace System.Resources
{
	// Token: 0x0200001D RID: 29
	internal class ByteArrayFromResXHandler : ResXDataNodeHandler, IWritableHandler
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00003A6D File Offset: 0x00001C6D
		public ByteArrayFromResXHandler(string data)
		{
			this.dataString = data;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003A7C File Offset: 0x00001C7C
		public override object GetValue(ITypeResolutionService typeResolver)
		{
			return Convert.FromBase64String(this.dataString);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003A7C File Offset: 0x00001C7C
		public override object GetValue(AssemblyName[] assemblyNames)
		{
			return Convert.FromBase64String(this.dataString);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003A89 File Offset: 0x00001C89
		public override string GetValueTypeName(ITypeResolutionService typeResolver)
		{
			return base.ResolveType(typeof(byte[]).AssemblyQualifiedName, typeResolver).AssemblyQualifiedName;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003AA6 File Offset: 0x00001CA6
		public override string GetValueTypeName(AssemblyName[] assemblyNames)
		{
			return typeof(byte[]).AssemblyQualifiedName;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003AB7 File Offset: 0x00001CB7
		public string DataString
		{
			get
			{
				return this.dataString;
			}
		}

		// Token: 0x04000D65 RID: 3429
		private string dataString;
	}
}
