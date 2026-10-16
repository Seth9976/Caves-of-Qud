using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Reflection.Emit
{
	// Token: 0x02000919 RID: 2329
	[StructLayout(LayoutKind.Sequential)]
	internal class ArrayType : SymbolType
	{
		// Token: 0x06004F53 RID: 20307 RVA: 0x000F994A File Offset: 0x000F7B4A
		internal ArrayType(Type elementType, int rank)
			: base(elementType)
		{
			this.rank = rank;
		}

		// Token: 0x06004F54 RID: 20308 RVA: 0x000F995A File Offset: 0x000F7B5A
		internal int GetEffectiveRank()
		{
			return this.rank;
		}

		// Token: 0x06004F55 RID: 20309 RVA: 0x000F9964 File Offset: 0x000F7B64
		internal override Type InternalResolve()
		{
			Type type = this.m_baseType.InternalResolve();
			if (this.rank == 0)
			{
				return type.MakeArrayType();
			}
			return type.MakeArrayType(this.rank);
		}

		// Token: 0x06004F56 RID: 20310 RVA: 0x000F9998 File Offset: 0x000F7B98
		internal override Type RuntimeResolve()
		{
			Type type = this.m_baseType.RuntimeResolve();
			if (this.rank == 0)
			{
				return type.MakeArrayType();
			}
			return type.MakeArrayType(this.rank);
		}

		// Token: 0x06004F57 RID: 20311 RVA: 0x000040F7 File Offset: 0x000022F7
		protected override bool IsArrayImpl()
		{
			return true;
		}

		// Token: 0x06004F58 RID: 20312 RVA: 0x000F99CC File Offset: 0x000F7BCC
		public override int GetArrayRank()
		{
			if (this.rank != 0)
			{
				return this.rank;
			}
			return 1;
		}

		// Token: 0x06004F59 RID: 20313 RVA: 0x000F99E0 File Offset: 0x000F7BE0
		internal override string FormatName(string elementName)
		{
			if (elementName == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(elementName);
			stringBuilder.Append("[");
			for (int i = 1; i < this.rank; i++)
			{
				stringBuilder.Append(",");
			}
			if (this.rank == 1)
			{
				stringBuilder.Append("*");
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x04003134 RID: 12596
		private int rank;
	}
}
