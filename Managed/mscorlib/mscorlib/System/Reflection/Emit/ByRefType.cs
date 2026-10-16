using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200091A RID: 2330
	[StructLayout(LayoutKind.Sequential)]
	internal class ByRefType : SymbolType
	{
		// Token: 0x06004F5A RID: 20314 RVA: 0x000F9A49 File Offset: 0x000F7C49
		internal ByRefType(Type elementType)
			: base(elementType)
		{
		}

		// Token: 0x06004F5B RID: 20315 RVA: 0x000F9A52 File Offset: 0x000F7C52
		internal override Type InternalResolve()
		{
			return this.m_baseType.InternalResolve().MakeByRefType();
		}

		// Token: 0x06004F5C RID: 20316 RVA: 0x000040F7 File Offset: 0x000022F7
		protected override bool IsByRefImpl()
		{
			return true;
		}

		// Token: 0x06004F5D RID: 20317 RVA: 0x000F9A64 File Offset: 0x000F7C64
		internal override string FormatName(string elementName)
		{
			if (elementName == null)
			{
				return null;
			}
			return elementName + "&";
		}

		// Token: 0x06004F5E RID: 20318 RVA: 0x000F9A76 File Offset: 0x000F7C76
		public override Type MakeArrayType()
		{
			throw new ArgumentException("Cannot create an array type of a byref type");
		}

		// Token: 0x06004F5F RID: 20319 RVA: 0x000F9A76 File Offset: 0x000F7C76
		public override Type MakeArrayType(int rank)
		{
			throw new ArgumentException("Cannot create an array type of a byref type");
		}

		// Token: 0x06004F60 RID: 20320 RVA: 0x000F9A82 File Offset: 0x000F7C82
		public override Type MakeByRefType()
		{
			throw new ArgumentException("Cannot create a byref type of an already byref type");
		}

		// Token: 0x06004F61 RID: 20321 RVA: 0x000F9A8E File Offset: 0x000F7C8E
		public override Type MakePointerType()
		{
			throw new ArgumentException("Cannot create a pointer type of a byref type");
		}
	}
}
