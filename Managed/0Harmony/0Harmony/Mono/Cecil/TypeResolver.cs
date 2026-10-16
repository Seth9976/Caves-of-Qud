using System;
using Mono.Cecil.Cil;

namespace Mono.Cecil
{
	// Token: 0x02000278 RID: 632
	internal sealed class TypeResolver
	{
		// Token: 0x06000FB3 RID: 4019 RVA: 0x000305F3 File Offset: 0x0002E7F3
		public static TypeResolver For(TypeReference typeReference)
		{
			if (!typeReference.IsGenericInstance)
			{
				return new TypeResolver();
			}
			return new TypeResolver((GenericInstanceType)typeReference);
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x0003060E File Offset: 0x0002E80E
		public static TypeResolver For(TypeReference typeReference, MethodReference methodReference)
		{
			return new TypeResolver(typeReference as GenericInstanceType, methodReference as GenericInstanceMethod);
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x00002AED File Offset: 0x00000CED
		public TypeResolver()
		{
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x00030621 File Offset: 0x0002E821
		public TypeResolver(GenericInstanceType typeDefinitionContext)
		{
			this._typeDefinitionContext = typeDefinitionContext;
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x00030630 File Offset: 0x0002E830
		public TypeResolver(GenericInstanceMethod methodDefinitionContext)
		{
			this._methodDefinitionContext = methodDefinitionContext;
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x0003063F File Offset: 0x0002E83F
		public TypeResolver(GenericInstanceType typeDefinitionContext, GenericInstanceMethod methodDefinitionContext)
		{
			this._typeDefinitionContext = typeDefinitionContext;
			this._methodDefinitionContext = methodDefinitionContext;
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x00030658 File Offset: 0x0002E858
		public MethodReference Resolve(MethodReference method)
		{
			MethodReference methodReference = method;
			if (this.IsDummy())
			{
				return methodReference;
			}
			TypeReference typeReference = this.Resolve(method.DeclaringType);
			GenericInstanceMethod genericInstanceMethod = method as GenericInstanceMethod;
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

		// Token: 0x06000FBA RID: 4026 RVA: 0x000308A0 File Offset: 0x0002EAA0
		public FieldReference Resolve(FieldReference field)
		{
			TypeReference typeReference = this.Resolve(field.DeclaringType);
			if (typeReference == field.DeclaringType)
			{
				return field;
			}
			return new FieldReference(field.Name, field.FieldType, typeReference);
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x000308D7 File Offset: 0x0002EAD7
		public TypeReference ResolveReturnType(MethodReference method)
		{
			return this.Resolve(GenericParameterResolver.ResolveReturnTypeIfNeeded(method));
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x000308E5 File Offset: 0x0002EAE5
		public TypeReference ResolveParameterType(MethodReference method, ParameterReference parameter)
		{
			return this.Resolve(GenericParameterResolver.ResolveParameterTypeIfNeeded(method, parameter));
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x000308F4 File Offset: 0x0002EAF4
		public TypeReference ResolveVariableType(MethodReference method, VariableReference variable)
		{
			return this.Resolve(GenericParameterResolver.ResolveVariableTypeIfNeeded(method, variable));
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x00030903 File Offset: 0x0002EB03
		public TypeReference ResolveFieldType(FieldReference field)
		{
			return this.Resolve(GenericParameterResolver.ResolveFieldTypeIfNeeded(field));
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x00030911 File Offset: 0x0002EB11
		public TypeReference Resolve(TypeReference typeReference)
		{
			return this.Resolve(typeReference, true);
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x0003091C File Offset: 0x0002EB1C
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
					throw new NotSupportedException(string.Format("The type {0} cannot be resolved correctly.", typeReference.FullName));
				}
				return typeReference;
			}
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x00030B58 File Offset: 0x0002ED58
		internal TypeResolver Nested(GenericInstanceMethod genericInstanceMethod)
		{
			return new TypeResolver(this._typeDefinitionContext as GenericInstanceType, genericInstanceMethod);
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x00030B6C File Offset: 0x0002ED6C
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

		// Token: 0x06000FC3 RID: 4035 RVA: 0x00030BDC File Offset: 0x0002EDDC
		private TypeReference HandleOwnerlessInvalidILCode(GenericParameter genericParameter)
		{
			if (genericParameter.Type == GenericParameterType.Method && this._typeDefinitionContext != null && genericParameter.Position < this._typeDefinitionContext.GenericArguments.Count)
			{
				return this._typeDefinitionContext.GenericArguments[genericParameter.Position];
			}
			return genericParameter.Module.TypeSystem.Object;
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x00030C39 File Offset: 0x0002EE39
		private bool IsDummy()
		{
			return this._typeDefinitionContext == null && this._methodDefinitionContext == null;
		}

		// Token: 0x0400057F RID: 1407
		private readonly IGenericInstance _typeDefinitionContext;

		// Token: 0x04000580 RID: 1408
		private readonly IGenericInstance _methodDefinitionContext;
	}
}
