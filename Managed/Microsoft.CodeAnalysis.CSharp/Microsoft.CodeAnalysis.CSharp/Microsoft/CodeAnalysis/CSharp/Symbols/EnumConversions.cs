using System;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004DD RID: 1245
	internal static class EnumConversions
	{
		// Token: 0x06006935 RID: 26933 RVA: 0x0018A790 File Offset: 0x00188990
		internal static TypeKind ToTypeKind(this DeclarationKind kind)
		{
			switch (kind)
			{
			case DeclarationKind.Class:
			case DeclarationKind.Script:
			case DeclarationKind.ImplicitClass:
			case DeclarationKind.SimpleProgram:
			case DeclarationKind.Record:
				return TypeKind.Class;
			case DeclarationKind.Interface:
				return TypeKind.Interface;
			case DeclarationKind.Struct:
			case DeclarationKind.RecordStruct:
				return TypeKind.Struct;
			case DeclarationKind.Enum:
				return TypeKind.Enum;
			case DeclarationKind.Delegate:
				return TypeKind.Delegate;
			case DeclarationKind.Submission:
				return TypeKind.Submission;
			default:
				throw ExceptionUtilities.UnexpectedValue(kind);
			}
		}
	}
}
