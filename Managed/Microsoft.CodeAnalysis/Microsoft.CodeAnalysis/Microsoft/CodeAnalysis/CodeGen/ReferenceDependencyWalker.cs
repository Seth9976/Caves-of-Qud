using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004DF RID: 1247
	[NullableContext(1)]
	[Nullable(0)]
	internal static class ReferenceDependencyWalker
	{
		// Token: 0x06003248 RID: 12872 RVA: 0x0008EA40 File Offset: 0x0008CC40
		public static void VisitReference(IReference reference, EmitContext context)
		{
			ITypeReference typeReference = reference as ITypeReference;
			if (typeReference != null)
			{
				ReferenceDependencyWalker.VisitTypeReference(typeReference, context);
				return;
			}
			IMethodReference methodReference = reference as IMethodReference;
			if (methodReference != null)
			{
				ReferenceDependencyWalker.VisitMethodReference(methodReference, context);
				return;
			}
			IFieldReference fieldReference = reference as IFieldReference;
			if (fieldReference != null)
			{
				ReferenceDependencyWalker.VisitFieldReference(fieldReference, context);
				return;
			}
		}

		// Token: 0x06003249 RID: 12873 RVA: 0x0008EA84 File Offset: 0x0008CC84
		private static void VisitTypeReference(ITypeReference typeReference, EmitContext context)
		{
			IArrayTypeReference arrayTypeReference = typeReference as IArrayTypeReference;
			if (arrayTypeReference != null)
			{
				ReferenceDependencyWalker.VisitTypeReference(arrayTypeReference.GetElementType(context), context);
				return;
			}
			IPointerTypeReference pointerTypeReference = typeReference as IPointerTypeReference;
			if (pointerTypeReference != null)
			{
				ReferenceDependencyWalker.VisitTypeReference(pointerTypeReference.GetTargetType(context), context);
				return;
			}
			IModifiedTypeReference modifiedTypeReference = typeReference as IModifiedTypeReference;
			if (modifiedTypeReference != null)
			{
				foreach (ICustomModifier customModifier in modifiedTypeReference.CustomModifiers)
				{
					ReferenceDependencyWalker.VisitTypeReference(customModifier.GetModifier(context), context);
				}
				ReferenceDependencyWalker.VisitTypeReference(modifiedTypeReference.UnmodifiedType, context);
				return;
			}
			INestedTypeReference asNestedTypeReference = typeReference.AsNestedTypeReference;
			if (asNestedTypeReference != null)
			{
				ReferenceDependencyWalker.VisitTypeReference(asNestedTypeReference.GetContainingType(context), context);
			}
			IGenericTypeInstanceReference asGenericTypeInstanceReference = typeReference.AsGenericTypeInstanceReference;
			if (asGenericTypeInstanceReference != null)
			{
				foreach (ITypeReference typeReference2 in asGenericTypeInstanceReference.GetGenericArguments(context))
				{
					ReferenceDependencyWalker.VisitTypeReference(typeReference2, context);
				}
			}
			IFunctionPointerTypeReference functionPointerTypeReference = typeReference as IFunctionPointerTypeReference;
			if (functionPointerTypeReference != null)
			{
				ReferenceDependencyWalker.VisitSignature(functionPointerTypeReference.Signature, context);
			}
		}

		// Token: 0x0600324A RID: 12874 RVA: 0x0008EB70 File Offset: 0x0008CD70
		private static void VisitMethodReference(IMethodReference methodReference, EmitContext context)
		{
			ReferenceDependencyWalker.VisitTypeReference(methodReference.GetContainingType(context), context);
			IGenericMethodInstanceReference asGenericMethodInstanceReference = methodReference.AsGenericMethodInstanceReference;
			if (asGenericMethodInstanceReference != null)
			{
				foreach (ITypeReference typeReference in asGenericMethodInstanceReference.GetGenericArguments(context))
				{
					ReferenceDependencyWalker.VisitTypeReference(typeReference, context);
				}
				methodReference = asGenericMethodInstanceReference.GetGenericMethod(context);
			}
			ISpecializedMethodReference asSpecializedMethodReference = methodReference.AsSpecializedMethodReference;
			if (asSpecializedMethodReference != null)
			{
				methodReference = asSpecializedMethodReference.UnspecializedVersion;
			}
			ReferenceDependencyWalker.VisitSignature(methodReference, context);
			if (methodReference.AcceptsExtraArguments)
			{
				ReferenceDependencyWalker.VisitParameters(methodReference.ExtraParameters, context);
			}
		}

		// Token: 0x0600324B RID: 12875 RVA: 0x0008EC0C File Offset: 0x0008CE0C
		internal static void VisitSignature(ISignature signature, EmitContext context)
		{
			ReferenceDependencyWalker.VisitParameters(signature.GetParameters(context), context);
			ReferenceDependencyWalker.VisitTypeReference(signature.GetType(context), context);
			foreach (ICustomModifier customModifier in signature.RefCustomModifiers)
			{
				ReferenceDependencyWalker.VisitTypeReference(customModifier.GetModifier(context), context);
			}
			foreach (ICustomModifier customModifier2 in signature.ReturnValueCustomModifiers)
			{
				ReferenceDependencyWalker.VisitTypeReference(customModifier2.GetModifier(context), context);
			}
		}

		// Token: 0x0600324C RID: 12876 RVA: 0x0008EC90 File Offset: 0x0008CE90
		private static void VisitParameters([Nullable(new byte[] { 0, 1 })] ImmutableArray<IParameterTypeInformation> parameters, EmitContext context)
		{
			foreach (IParameterTypeInformation parameterTypeInformation in parameters)
			{
				ReferenceDependencyWalker.VisitTypeReference(parameterTypeInformation.GetType(context), context);
				foreach (ICustomModifier customModifier in parameterTypeInformation.RefCustomModifiers)
				{
					ReferenceDependencyWalker.VisitTypeReference(customModifier.GetModifier(context), context);
				}
				foreach (ICustomModifier customModifier2 in parameterTypeInformation.CustomModifiers)
				{
					ReferenceDependencyWalker.VisitTypeReference(customModifier2.GetModifier(context), context);
				}
			}
		}

		// Token: 0x0600324D RID: 12877 RVA: 0x0008ED20 File Offset: 0x0008CF20
		private static void VisitFieldReference(IFieldReference fieldReference, EmitContext context)
		{
			ReferenceDependencyWalker.VisitTypeReference(fieldReference.GetContainingType(context), context);
			ISpecializedFieldReference asSpecializedFieldReference = fieldReference.AsSpecializedFieldReference;
			if (asSpecializedFieldReference != null)
			{
				fieldReference = asSpecializedFieldReference.UnspecializedVersion;
			}
			ReferenceDependencyWalker.VisitTypeReference(fieldReference.GetType(context), context);
		}
	}
}
