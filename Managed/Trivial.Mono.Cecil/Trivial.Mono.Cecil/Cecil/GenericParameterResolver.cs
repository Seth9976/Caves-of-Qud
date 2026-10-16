using System;
using Trivial.Mono.Cecil.Cil;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000047 RID: 71
	internal sealed class GenericParameterResolver
	{
		// Token: 0x06000250 RID: 592 RVA: 0x00006620 File Offset: 0x00004820
		internal static TypeReference ResolveReturnTypeIfNeeded(MethodReference methodReference)
		{
			if (methodReference.DeclaringType.IsArray && methodReference.Name == "Get")
			{
				return methodReference.ReturnType;
			}
			GenericInstanceMethod genericInstanceMethod = methodReference as GenericInstanceMethod;
			GenericInstanceType genericInstanceType = methodReference.DeclaringType as GenericInstanceType;
			if (genericInstanceMethod == null && genericInstanceType == null)
			{
				return methodReference.ReturnType;
			}
			return GenericParameterResolver.ResolveIfNeeded(genericInstanceMethod, genericInstanceType, methodReference.ReturnType);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00006680 File Offset: 0x00004880
		internal static TypeReference ResolveFieldTypeIfNeeded(FieldReference fieldReference)
		{
			return GenericParameterResolver.ResolveIfNeeded(null, fieldReference.DeclaringType as GenericInstanceType, fieldReference.FieldType);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000669C File Offset: 0x0000489C
		internal static TypeReference ResolveParameterTypeIfNeeded(MethodReference method, ParameterReference parameter)
		{
			GenericInstanceMethod genericInstanceMethod = method as GenericInstanceMethod;
			GenericInstanceType genericInstanceType = method.DeclaringType as GenericInstanceType;
			if (genericInstanceMethod == null && genericInstanceType == null)
			{
				return parameter.ParameterType;
			}
			return GenericParameterResolver.ResolveIfNeeded(genericInstanceMethod, genericInstanceType, parameter.ParameterType);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x000066D8 File Offset: 0x000048D8
		internal static TypeReference ResolveVariableTypeIfNeeded(MethodReference method, VariableReference variable)
		{
			GenericInstanceMethod genericInstanceMethod = method as GenericInstanceMethod;
			GenericInstanceType genericInstanceType = method.DeclaringType as GenericInstanceType;
			if (genericInstanceMethod == null && genericInstanceType == null)
			{
				return variable.VariableType;
			}
			return GenericParameterResolver.ResolveIfNeeded(genericInstanceMethod, genericInstanceType, variable.VariableType);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00006714 File Offset: 0x00004914
		private static TypeReference ResolveIfNeeded(IGenericInstance genericInstanceMethod, IGenericInstance declaringGenericInstanceType, TypeReference parameterType)
		{
			ByReferenceType byReferenceType = parameterType as ByReferenceType;
			if (byReferenceType != null)
			{
				return GenericParameterResolver.ResolveIfNeeded(genericInstanceMethod, declaringGenericInstanceType, byReferenceType);
			}
			ArrayType arrayType = parameterType as ArrayType;
			if (arrayType != null)
			{
				return GenericParameterResolver.ResolveIfNeeded(genericInstanceMethod, declaringGenericInstanceType, arrayType);
			}
			GenericInstanceType genericInstanceType = parameterType as GenericInstanceType;
			if (genericInstanceType != null)
			{
				return GenericParameterResolver.ResolveIfNeeded(genericInstanceMethod, declaringGenericInstanceType, genericInstanceType);
			}
			GenericParameter genericParameter = parameterType as GenericParameter;
			if (genericParameter != null)
			{
				return GenericParameterResolver.ResolveIfNeeded(genericInstanceMethod, declaringGenericInstanceType, genericParameter);
			}
			RequiredModifierType requiredModifierType = parameterType as RequiredModifierType;
			if (requiredModifierType != null && GenericParameterResolver.ContainsGenericParameters(requiredModifierType))
			{
				return GenericParameterResolver.ResolveIfNeeded(genericInstanceMethod, declaringGenericInstanceType, requiredModifierType.ElementType);
			}
			if (GenericParameterResolver.ContainsGenericParameters(parameterType))
			{
				throw new Exception("Unexpected generic parameter.");
			}
			return parameterType;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x000067A5 File Offset: 0x000049A5
		private static TypeReference ResolveIfNeeded(IGenericInstance genericInstanceMethod, IGenericInstance genericInstanceType, GenericParameter genericParameterElement)
		{
			if (genericParameterElement.MetadataType != MetadataType.MVar)
			{
				return genericInstanceType.GenericArguments[genericParameterElement.Position];
			}
			if (genericInstanceMethod == null)
			{
				return genericParameterElement;
			}
			return genericInstanceMethod.GenericArguments[genericParameterElement.Position];
		}

		// Token: 0x06000256 RID: 598 RVA: 0x000067D9 File Offset: 0x000049D9
		private static ArrayType ResolveIfNeeded(IGenericInstance genericInstanceMethod, IGenericInstance genericInstanceType, ArrayType arrayType)
		{
			return new ArrayType(GenericParameterResolver.ResolveIfNeeded(genericInstanceMethod, genericInstanceType, arrayType.ElementType), arrayType.Rank);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x000067F3 File Offset: 0x000049F3
		private static ByReferenceType ResolveIfNeeded(IGenericInstance genericInstanceMethod, IGenericInstance genericInstanceType, ByReferenceType byReferenceType)
		{
			return new ByReferenceType(GenericParameterResolver.ResolveIfNeeded(genericInstanceMethod, genericInstanceType, byReferenceType.ElementType));
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00006808 File Offset: 0x00004A08
		private static GenericInstanceType ResolveIfNeeded(IGenericInstance genericInstanceMethod, IGenericInstance genericInstanceType, GenericInstanceType genericInstanceType1)
		{
			if (!GenericParameterResolver.ContainsGenericParameters(genericInstanceType1))
			{
				return genericInstanceType1;
			}
			GenericInstanceType genericInstanceType2 = new GenericInstanceType(genericInstanceType1.ElementType);
			foreach (TypeReference typeReference in genericInstanceType1.GenericArguments)
			{
				if (!typeReference.IsGenericParameter)
				{
					genericInstanceType2.GenericArguments.Add(GenericParameterResolver.ResolveIfNeeded(genericInstanceMethod, genericInstanceType, typeReference));
				}
				else
				{
					GenericParameter genericParameter = (GenericParameter)typeReference;
					GenericParameterType type = genericParameter.Type;
					if (type != GenericParameterType.Type)
					{
						if (type == GenericParameterType.Method)
						{
							if (genericInstanceMethod == null)
							{
								genericInstanceType2.GenericArguments.Add(genericParameter);
							}
							else
							{
								genericInstanceType2.GenericArguments.Add(genericInstanceMethod.GenericArguments[genericParameter.Position]);
							}
						}
					}
					else
					{
						if (genericInstanceType == null)
						{
							throw new NotSupportedException();
						}
						genericInstanceType2.GenericArguments.Add(genericInstanceType.GenericArguments[genericParameter.Position]);
					}
				}
			}
			return genericInstanceType2;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x000068FC File Offset: 0x00004AFC
		private static bool ContainsGenericParameters(TypeReference typeReference)
		{
			if (typeReference is GenericParameter)
			{
				return true;
			}
			ArrayType arrayType = typeReference as ArrayType;
			if (arrayType != null)
			{
				return GenericParameterResolver.ContainsGenericParameters(arrayType.ElementType);
			}
			PointerType pointerType = typeReference as PointerType;
			if (pointerType != null)
			{
				return GenericParameterResolver.ContainsGenericParameters(pointerType.ElementType);
			}
			ByReferenceType byReferenceType = typeReference as ByReferenceType;
			if (byReferenceType != null)
			{
				return GenericParameterResolver.ContainsGenericParameters(byReferenceType.ElementType);
			}
			SentinelType sentinelType = typeReference as SentinelType;
			if (sentinelType != null)
			{
				return GenericParameterResolver.ContainsGenericParameters(sentinelType.ElementType);
			}
			PinnedType pinnedType = typeReference as PinnedType;
			if (pinnedType != null)
			{
				return GenericParameterResolver.ContainsGenericParameters(pinnedType.ElementType);
			}
			RequiredModifierType requiredModifierType = typeReference as RequiredModifierType;
			if (requiredModifierType != null)
			{
				return GenericParameterResolver.ContainsGenericParameters(requiredModifierType.ElementType);
			}
			GenericInstanceType genericInstanceType = typeReference as GenericInstanceType;
			if (genericInstanceType != null)
			{
				foreach (TypeReference typeReference2 in genericInstanceType.GenericArguments)
				{
					if (GenericParameterResolver.ContainsGenericParameters(typeReference2))
					{
						return true;
					}
				}
				return false;
			}
			if (typeReference is TypeSpecification)
			{
				throw new NotSupportedException();
			}
			return false;
		}
	}
}
