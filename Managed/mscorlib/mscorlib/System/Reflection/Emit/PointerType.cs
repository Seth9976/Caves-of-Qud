using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200091B RID: 2331
	[StructLayout(LayoutKind.Sequential)]
	internal class PointerType : SymbolType
	{
		// Token: 0x06004F62 RID: 20322 RVA: 0x000F9A49 File Offset: 0x000F7C49
		internal PointerType(Type elementType)
			: base(elementType)
		{
		}

		// Token: 0x06004F63 RID: 20323 RVA: 0x000F9A9A File Offset: 0x000F7C9A
		internal override Type InternalResolve()
		{
			return this.m_baseType.InternalResolve().MakePointerType();
		}

		// Token: 0x06004F64 RID: 20324 RVA: 0x000040F7 File Offset: 0x000022F7
		protected override bool IsPointerImpl()
		{
			return true;
		}

		// Token: 0x06004F65 RID: 20325 RVA: 0x000F9AAC File Offset: 0x000F7CAC
		internal override string FormatName(string elementName)
		{
			if (elementName == null)
			{
				return null;
			}
			return elementName + "*";
		}
	}
}
