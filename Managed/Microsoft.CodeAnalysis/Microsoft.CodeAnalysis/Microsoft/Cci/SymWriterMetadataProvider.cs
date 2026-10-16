using System;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using Microsoft.DiaSymReader;

namespace Microsoft.Cci
{
	// Token: 0x02000079 RID: 121
	internal sealed class SymWriterMetadataProvider : ISymWriterMetadataProvider
	{
		// Token: 0x0600033C RID: 828 RVA: 0x0000C80F File Offset: 0x0000AA0F
		internal SymWriterMetadataProvider(MetadataWriter writer)
		{
			this._writer = writer;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000C820 File Offset: 0x0000AA20
		public bool TryGetTypeDefinitionInfo(int typeDefinitionToken, out string namespaceName, out string typeName, out TypeAttributes attributes)
		{
			if (typeDefinitionToken == 0)
			{
				namespaceName = null;
				typeName = null;
				attributes = 0;
				return false;
			}
			ITypeDefinition typeDefinition = this._writer.GetTypeDefinition(typeDefinitionToken);
			if (this._lastTypeDef == typeDefinitionToken)
			{
				typeName = this._lastTypeDefName;
				namespaceName = this._lastTypeDefNamespace;
			}
			else
			{
				typeName = MetadataWriter.GetMangledName((INamedTypeReference)typeDefinition);
				INamespaceTypeDefinition namespaceTypeDefinition;
				if ((namespaceTypeDefinition = typeDefinition.AsNamespaceTypeDefinition(this._writer.Context)) != null)
				{
					namespaceName = namespaceTypeDefinition.NamespaceName;
				}
				else
				{
					namespaceName = null;
				}
				this._lastTypeDef = typeDefinitionToken;
				this._lastTypeDefName = typeName;
				this._lastTypeDefNamespace = namespaceName;
			}
			attributes = this._writer.GetTypeAttributes(typeDefinition.GetResolvedType(this._writer.Context));
			return true;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000C8CC File Offset: 0x0000AACC
		public bool TryGetMethodInfo(int methodDefinitionToken, out string methodName, out int declaringTypeToken)
		{
			IMethodDefinition methodDefinition = this._writer.GetMethodDefinition(methodDefinitionToken);
			methodName = methodDefinition.Name;
			declaringTypeToken = MetadataTokens.GetToken(this._writer.GetTypeHandle(methodDefinition.GetContainingType(this._writer.Context), true));
			return true;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000C914 File Offset: 0x0000AB14
		public bool TryGetEnclosingType(int nestedTypeToken, out int enclosingTypeToken)
		{
			INestedTypeReference nestedTypeReference = this._writer.GetNestedTypeReference(nestedTypeToken);
			if (nestedTypeReference == null)
			{
				enclosingTypeToken = 0;
				return false;
			}
			enclosingTypeToken = MetadataTokens.GetToken(this._writer.GetTypeHandle(nestedTypeReference.GetContainingType(this._writer.Context), true));
			return true;
		}

		// Token: 0x04000109 RID: 265
		private readonly MetadataWriter _writer;

		// Token: 0x0400010A RID: 266
		private int _lastTypeDef;

		// Token: 0x0400010B RID: 267
		private string _lastTypeDefName;

		// Token: 0x0400010C RID: 268
		private string _lastTypeDefNamespace;
	}
}
