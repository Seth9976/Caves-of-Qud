using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Microsoft.DiaSymReader
{
	// Token: 0x020000F1 RID: 241
	internal sealed class SymWriterMetadataAdapter : MetadataAdapterBase
	{
		// Token: 0x0600084E RID: 2126 RVA: 0x00018758 File Offset: 0x00016958
		public SymWriterMetadataAdapter(ISymWriterMetadataProvider metadataProvider)
		{
			this._metadataProvider = metadataProvider;
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00018767 File Offset: 0x00016967
		public unsafe override int GetTokenFromSig(byte* voidPointerSig, int byteCountSig)
		{
			return 285212672;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00018770 File Offset: 0x00016970
		public unsafe override int GetTypeDefProps(int typeDef, [Out] char* qualifiedName, int qualifiedNameBufferLength, [Out] int* qualifiedNameLength, [Out] TypeAttributes* attributes, [Out] int* baseType)
		{
			string text;
			string text2;
			TypeAttributes typeAttributes;
			if (!this._metadataProvider.TryGetTypeDefinitionInfo(typeDef, out text, out text2, out typeAttributes))
			{
				return -2147024809;
			}
			if (qualifiedNameLength != null || qualifiedName != null)
			{
				InteropUtilities.CopyQualifiedTypeName(qualifiedName, qualifiedNameBufferLength, qualifiedNameLength, text, text2);
			}
			if (attributes != null)
			{
				*(int*)attributes = typeAttributes;
			}
			return 0;
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x000187B8 File Offset: 0x000169B8
		public unsafe override int GetTypeRefProps(int typeRef, [Out] int* resolutionScope, [Out] char* qualifiedName, int qualifiedNameBufferLength, [Out] int* qualifiedNameLength)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x000187BF File Offset: 0x000169BF
		public override int GetNestedClassProps(int nestedClass, out int enclosingClass)
		{
			if (!this._metadataProvider.TryGetEnclosingType(nestedClass, out enclosingClass))
			{
				return -2147467259;
			}
			return 0;
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x000187D8 File Offset: 0x000169D8
		public unsafe override int GetMethodProps(int methodDef, [Out] int* declaringTypeDef, [Out] char* name, int nameBufferLength, [Out] int* nameLength, [Out] MethodAttributes* attributes, [Out] byte** signature, [Out] int* signatureLength, [Out] int* relativeVirtualAddress, [Out] MethodImplAttributes* implAttributes)
		{
			string text;
			int num;
			if (!this._metadataProvider.TryGetMethodInfo(methodDef, out text, out num))
			{
				return -2147024809;
			}
			if (name != null || nameLength != null)
			{
				int num2 = Math.Min(text.Length, nameBufferLength - 1);
				if (nameLength != null)
				{
					*nameLength = num2;
				}
				if (name != null && nameBufferLength > 0)
				{
					char* ptr = name;
					for (int i = 0; i < num2; i++)
					{
						*ptr = text.get_Chars(i);
						ptr++;
					}
					*ptr = '\0';
				}
			}
			if (declaringTypeDef != null)
			{
				*declaringTypeDef = num;
			}
			return 0;
		}

		// Token: 0x0400024C RID: 588
		private readonly ISymWriterMetadataProvider _metadataProvider;
	}
}
