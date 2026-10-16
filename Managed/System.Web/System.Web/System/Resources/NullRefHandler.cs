using System;
using System.ComponentModel.Design;
using System.Reflection;

namespace System.Resources
{
	// Token: 0x02000020 RID: 32
	internal class NullRefHandler : ResXDataNodeHandler, IWritableHandler
	{
		// Token: 0x06000085 RID: 133 RVA: 0x00003BE7 File Offset: 0x00001DE7
		public NullRefHandler(string _dataString)
		{
			this.dataString = _dataString;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003BF6 File Offset: 0x00001DF6
		public override object GetValue(ITypeResolutionService typeResolver)
		{
			return null;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003BF6 File Offset: 0x00001DF6
		public override object GetValue(AssemblyName[] assemblyNames)
		{
			return null;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003BF9 File Offset: 0x00001DF9
		public override string GetValueTypeName(ITypeResolutionService typeResolver)
		{
			return typeof(object).AssemblyQualifiedName;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003BF9 File Offset: 0x00001DF9
		public override string GetValueTypeName(AssemblyName[] assemblyNames)
		{
			return typeof(object).AssemblyQualifiedName;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00003C0A File Offset: 0x00001E0A
		public string DataString
		{
			get
			{
				return this.dataString;
			}
		}

		// Token: 0x04000D68 RID: 3432
		private string dataString;
	}
}
