using System;
using Trivial.Mono.Cecil.Cil;

namespace Trivial.Mono.Cecil
{
	// Token: 0x020000C7 RID: 199
	internal sealed class TypeResolver
	{
		// Token: 0x06000850 RID: 2128 RVA: 0x00019C81 File Offset: 0x00017E81
		public static TypeResolver For(TypeReference typeReference)
		{
			if (!typeReference.IsGenericInstance)
			{
				return new TypeResolver();
			}
			return new TypeResolver((GenericInstanceType)typeReference);
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00019C9C File Offset: 0x00017E9C
		public static TypeResolver For(TypeReference typeReference, MethodReference methodReference)
		{
			return new TypeResolver(typeReference as GenericInstanceType, methodReference as GenericInstanceMethod);
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00002F5F File Offset: 0x0000115F
		public TypeResolver()
		{
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00019CAF File Offset: 0x00017EAF
		public TypeResolver(GenericInstanceType typeDefinitionContext)
		{
			this._typeDefinitionContext = typeDefinitionContext;
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00019CBE File Offset: 0x00017EBE
		public TypeResolver(GenericInstanceMethod methodDefinitionContext)
		{
			this._methodDefinitionContext = methodDefinitionContext;
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00019CCD File Offset: 0x00017ECD
		public TypeResolver(GenericInstanceType typeDefinitionContext, GenericInstanceMethod methodDefinitionContext)
		{
			this._typeDefinitionContext = typeDefinitionContext;
			this._methodDefinitionContext = methodDefinitionContext;
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00019CE4 File Offset: 0x00017EE4
		public MethodReference Resolve(MethodReference method)
		{
			if (this.IsDummy())
			{
				return method;
			}
			TypeReference typeReference = this.Resolve(method.DeclaringType);
			GenericInstanceMethod genericInstanceMethod = method as GenericInstanceMethod;
			MethodReference methodReference;
			if (genericInstanceMethod != null)
			{
				methodReference = new MethodReference(method.Name, method.ReturnType, typeReference);
				foreach (ParameterDefinition parameterDefinition in method.Parameters)
				{
					methodReference.Parameters.Add(new ParameterDefinition(parameterDefinition.Name, parameterDefinition.Attributes, parameterDefinition.ParameterType));
				}
				foreach (GenericParameter genericParameter in genericInstanceMethod.ElementMethod.GenericParameters)
				{
					methodReference.GenericParameters.Add(new GenericParameter(genericParameter.Name, methodReference));
				}
				methodReference.HasThis = method.HasThis;
				GenericInstanceMethod genericInstanceMethod2 = new GenericInstanceMethod(methodReference);
				foreach (TypeReference typeReference2 in genericInstanceMethod.GenericArguments)
				{
					genericInstanceMethod2.GenericArguments.Add(this.Resolve(typeReference2));
				}
				methodReference = genericInstanceMethod2;
			}
			else
			{
				methodReference = new MethodReference(method.Name, method.ReturnType, typeReference);
				foreach (GenericParameter genericParameter2 in method.GenericParameters)
				{
					methodReference.GenericParameters.Add(new GenericParameter(genericParameter2.Name, methodReference));
				}
				foreach (ParameterDefinition parameterDefinition2 in method.Parameters)
				{
					methodReference.Parameters.Add(new ParameterDefinition(parameterDefinition2.Name, parameterDefinition2.Attributes, parameterDefinition2.ParameterType));
				}
				methodReference.HasThis = method.HasThis;
			}
			return methodReference;
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00019F2C File Offset: 0x0001812C
		public FieldReference Resolve(FieldReference field)
		{
			TypeReference typeReference = this.Resolve(field.DeclaringType);
			if (typeReference == field.DeclaringType)
			{
				return field;
			}
			return new FieldReference(field.Name, field.FieldType, typeReference);
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00019F63 File Offset: 0x00018163
		public TypeReference ResolveReturnType(MethodReference method)
		{
			return this.Resolve(GenericParameterResolver.ResolveReturnTypeIfNeeded(method));
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00019F71 File Offset: 0x00018171
		public TypeReference ResolveParameterType(MethodReference method, ParameterReference parameter)
		{
			return this.Resolve(GenericParameterResolver.ResolveParameterTypeIfNeeded(method, parameter));
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00019F80 File Offset: 0x00018180
		public TypeReference ResolveVariableType(MethodReference method, VariableReference variable)
		{
			return this.Resolve(GenericParameterResolver.ResolveVariableTypeIfNeeded(method, variable));
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00019F8F File Offset: 0x0001818F
		public TypeReference ResolveFieldType(FieldReference field)
		{
			return this.Resolve(GenericParameterResolver.ResolveFieldTypeIfNeeded(field));
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00019F9D File Offset: 0x0001819D
		public TypeReference Resolve(TypeReference typeReference)
		{
			return this.Resolve(typeReference, true);
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00019FA8 File Offset: 0x000181A8
		public TypeReference Resolve(TypeReference typeReference, bool includeTypeDefinitions)
		{
			if (this.IsDummy())
			{
				return typeReference;
			}
			if (this._typeDefinitionContext != null && this._typeDefinitionContext.GenericArguments.Contains(typeReference))
			{
				return typeReference;
			}
			if (this._methodDefinitionContext != null && this._methodDefinitionContext.GenericArguments.Contains(typeReference))
			{
				return typeReference;
			}
			GenericParameter genericParameter = typeReference as GenericParameter;
			if (genericParameter != null)
			{
				if (this._typeDefinitionContext != null && this._typeDefinitionContext.GenericArguments.Contains(genericParameter))
				{
					return genericParameter;
				}
				if (this._methodDefinitionContext != null && this._methodDefinitionContext.GenericArguments.Contains(genericParameter))
				{
					return genericParameter;
				}
				return this.ResolveGenericParameter(genericParameter);
			}
			else
			{
				ArrayType arrayType = typeReference as ArrayType;
				if (arrayType != null)
				{
					return new ArrayType(this.Resolve(arrayType.ElementType), arrayType.Rank);
				}
				PointerType pointerType = typeReference as PointerType;
				if (pointerType != null)
				{
					return new PointerType(this.Resolve(pointerType.ElementType));
				}
				ByReferenceType byReferenceType = typeReference as ByReferenceType;
				if (byReferenceType != null)
				{
					return new ByReferenceType(this.Resolve(byReferenceType.ElementType));
				}
				PinnedType pinnedType = typeReference as PinnedType;
				if (pinnedType != null)
				{
					return new PinnedType(this.Resolve(pinnedType.ElementType));
				}
				GenericInstanceType genericInstanceType = typeReference as GenericInstanceType;
				if (genericInstanceType != null)
				{
					GenericInstanceType genericInstanceType2 = new GenericInstanceType(genericInstanceType.ElementType);
					foreach (TypeReference typeReference2 in genericInstanceType.GenericArguments)
					{
						genericInstanceType2.GenericArguments.Add(this.Resolve(typeReference2));
					}
					return genericInstanceType2;
				}
				RequiredModifierType requiredModifierType = typeReference as RequiredModifierType;
				if (requiredModifierType != null)
				{
					return this.Resolve(requiredModifierType.ElementType, includeTypeDefinitions);
				}
				if (includeTypeDefinitions)
				{
					TypeDefinition typeDefinition = typeReference as TypeDefinition;
					if (typeDefinition != null && typeDefinition.HasGenericParameters)
					{
						GenericInstanceType genericInstanceType3 = new GenericInstanceType(typeDefinition);
						foreach (GenericParameter genericParameter2 in typeDefinition.GenericParameters)
						{
							genericInstanceType3.GenericArguments.Add(this.Resolve(genericParameter2));
						}
						return genericInstanceType3;
					}
				}
				if (typeReference is TypeSpecification)
				{
					throw new NotSupportedException("The type " + typeReference.FullName + " cannot be resolved correctly.");
				}
				return typeReference;
			}
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0001A1E8 File Offset: 0x000183E8
		internal TypeResolver Nested(GenericInstanceMethod genericInstanceMethod)
		{
			return new TypeResolver(this._typeDefinitionContext as GenericInstanceType, genericInstanceMethod);
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0001A1FC File Offset: 0x000183FC
		private TypeReference ResolveGenericParameter(GenericParameter genericParameter)
		{
			if (genericParameter.Owner == null)
			{
				return this.HandleOwnerlessInvalidILCode(genericParameter);
			}
			if (!(genericParameter.Owner is MemberReference))
			{
				throw new NotSupportedException();
			}
			if (genericParameter.Type == GenericParameterType.Type)
			{
				return this._typeDefinitionContext.GenericArguments[genericParameter.Position];
			}
			if (this._methodDefinitionContext == null)
			{
				return genericParameter;
			}
			return this._methodDefinitionContext.GenericArguments[genericParameter.Position];
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0001A26C File Offset: 0x0001846C
		private TypeReference HandleOwnerlessInvalidILCode(GenericParameter genericParameter)
		{
			if (genericParameter.Type == GenericParameterType.Method && this._typeDefinitionContext != null && genericParameter.Position < this._typeDefinitionContext.GenericArguments.Count)
			{
				return this._typeDefinitionContext.GenericArguments[genericParameter.Position];
			}
			return genericParameter.Module.TypeSystem.Object;
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0001A2C9 File Offset: 0x000184C9
		private bool IsDummy()
		{
			return this._typeDefinitionContext == null && this._methodDefinitionContext == null;
		}

		// Token: 0x040002F8 RID: 760
		private readonly IGenericInstance _typeDefinitionContext;

		// Token: 0x040002F9 RID: 761
		private readonly IGenericInstance _methodDefinitionContext;
	}
}
