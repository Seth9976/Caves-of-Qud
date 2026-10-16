using System;
using Mono.Cecil.Cil;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x02000211 RID: 529
	internal sealed class GenericParameterResolver
	{
		// Token: 0x06000B79 RID: 2937 RVA: 0x000281CC File Offset: 0x000263CC
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

		// Token: 0x06000B7A RID: 2938 RVA: 0x0002822C File Offset: 0x0002642C
		internal static TypeReference ResolveFieldTypeIfNeeded(FieldReference fieldReference)
		{
			return GenericParameterResolver.ResolveIfNeeded(null, fieldReference.DeclaringType as GenericInstanceType, fieldReference.FieldType);
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x00028248 File Offset: 0x00026448
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

		// Token: 0x06000B7C RID: 2940 RVA: 0x00028284 File Offset: 0x00026484
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

		// Token: 0x06000B7D RID: 2941 RVA: 0x000282C0 File Offset: 0x000264C0
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

		// Token: 0x06000B7E RID: 2942 RVA: 0x00028351 File Offset: 0x00026551
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

		// Token: 0x06000B7F RID: 2943 RVA: 0x00028385 File Offset: 0x00026585
		private static ArrayType ResolveIfNeeded(IGenericInstance genericInstanceMethod, IGenericInstance genericInstanceType, ArrayType arrayType)
		{
			return new ArrayType(GenericParameterResolver.ResolveIfNeeded(genericInstanceMethod, genericInstanceType, arrayType.ElementType), arrayType.Rank);
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0002839F File Offset: 0x0002659F
		private static ByReferenceType ResolveIfNeeded(IGenericInstance genericInstanceMethod, IGenericInstance genericInstanceType, ByReferenceType byReferenceType)
		{
			return new ByReferenceType(GenericParameterResolver.ResolveIfNeeded(genericInstanceMethod, genericInstanceType, byReferenceType.ElementType));
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x000283B4 File Offset: 0x000265B4
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

		// Token: 0x06000B82 RID: 2946 RVA: 0x000284A8 File Offset: 0x000266A8
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
				using (Collection<TypeReference>.Enumerator enumerator = genericInstanceType.GenericArguments.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (GenericParameterResolver.ContainsGenericParameters(enumerator.Current))
						{
							return true;
						}
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
