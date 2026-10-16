using System;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000123 RID: 291
	internal static class EnumConversions
	{
		// Token: 0x0600185E RID: 6238 RVA: 0x000830A0 File Offset: 0x000812A0
		internal static DeclarationKind ToDeclarationKind(this SyntaxKind kind)
		{
			if (kind <= SyntaxKind.DelegateDeclaration)
			{
				if (kind == SyntaxKind.NamespaceDeclaration)
				{
					return DeclarationKind.Namespace;
				}
				switch (kind)
				{
				case SyntaxKind.ClassDeclaration:
					return DeclarationKind.Class;
				case SyntaxKind.StructDeclaration:
					return DeclarationKind.Struct;
				case SyntaxKind.InterfaceDeclaration:
					return DeclarationKind.Interface;
				case SyntaxKind.EnumDeclaration:
					return DeclarationKind.Enum;
				case SyntaxKind.DelegateDeclaration:
					return DeclarationKind.Delegate;
				}
			}
			else
			{
				if (kind == SyntaxKind.RecordDeclaration)
				{
					return DeclarationKind.Record;
				}
				if (kind == SyntaxKind.RecordStructDeclaration)
				{
					return DeclarationKind.RecordStruct;
				}
			}
			throw ExceptionUtilities.UnexpectedValue(kind);
		}
	}
}
