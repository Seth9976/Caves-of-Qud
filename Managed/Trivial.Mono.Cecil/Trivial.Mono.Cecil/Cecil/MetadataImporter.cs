using System;
using Trivial.Mono.Cecil.Metadata;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200006F RID: 111
	public class MetadataImporter : IMetadataImporter
	{
		// Token: 0x0600036C RID: 876 RVA: 0x0000A4B7 File Offset: 0x000086B7
		public MetadataImporter(ModuleDefinition module)
		{
			Mixin.CheckModule(module);
			this.module = module;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000A4CC File Offset: 0x000086CC
		private TypeReference ImportType(TypeReference type, ImportGenericContext context)
		{
			if (type.IsTypeSpecification())
			{
				return this.ImportTypeSpecification(type, context);
			}
			TypeReference typeReference = new TypeReference(type.Namespace, type.Name, this.module, this.ImportScope(type.Scope), type.IsValueType);
			MetadataSystem.TryProcessPrimitiveTypeReference(typeReference);
			if (type.IsNested)
			{
				typeReference.DeclaringType = this.ImportType(type.DeclaringType, context);
			}
			if (type.HasGenericParameters)
			{
				MetadataImporter.ImportGenericParameters(typeReference, type);
			}
			return typeReference;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000A548 File Offset: 0x00008748
		private IMetadataScope ImportScope(IMetadataScope scope)
		{
			switch (scope.MetadataScopeType)
			{
			case MetadataScopeType.AssemblyNameReference:
				return this.ImportAssemblyName((AssemblyNameReference)scope);
			case MetadataScopeType.ModuleReference:
				throw new NotImplementedException();
			case MetadataScopeType.ModuleDefinition:
				if (scope == this.module)
				{
					return scope;
				}
				return this.ImportAssemblyName(((ModuleDefinition)scope).Assembly.Name);
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000A5AC File Offset: 0x000087AC
		private AssemblyNameReference ImportAssemblyName(AssemblyNameReference name)
		{
			AssemblyNameReference assemblyNameReference;
			if (this.module.TryGetAssemblyNameReference(name, out assemblyNameReference))
			{
				return assemblyNameReference;
			}
			assemblyNameReference = new AssemblyNameReference(name.Name, name.Version)
			{
				Culture = name.Culture,
				HashAlgorithm = name.HashAlgorithm,
				IsRetargetable = name.IsRetargetable,
				IsWindowsRuntime = name.IsWindowsRuntime
			};
			byte[] array = ((!name.PublicKeyToken.IsNullOrEmpty<byte>()) ? new byte[name.PublicKeyToken.Length] : Empty<byte>.Array);
			if (array.Length != 0)
			{
				Buffer.BlockCopy(name.PublicKeyToken, 0, array, 0, array.Length);
			}
			assemblyNameReference.PublicKeyToken = array;
			this.module.AssemblyReferences.Add(assemblyNameReference);
			return assemblyNameReference;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000A660 File Offset: 0x00008860
		private static void ImportGenericParameters(IGenericParameterProvider imported, IGenericParameterProvider original)
		{
			Collection<GenericParameter> genericParameters = original.GenericParameters;
			Collection<GenericParameter> genericParameters2 = imported.GenericParameters;
			for (int i = 0; i < genericParameters.Count; i++)
			{
				genericParameters2.Add(new GenericParameter(genericParameters[i].Name, imported));
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000A6A4 File Offset: 0x000088A4
		private TypeReference ImportTypeSpecification(TypeReference type, ImportGenericContext context)
		{
			ElementType etype = type.etype;
			switch (etype)
			{
			case ElementType.Ptr:
			{
				PointerType pointerType = (PointerType)type;
				return new PointerType(this.ImportType(pointerType.ElementType, context));
			}
			case ElementType.ByRef:
			{
				ByReferenceType byReferenceType = (ByReferenceType)type;
				return new ByReferenceType(this.ImportType(byReferenceType.ElementType, context));
			}
			case ElementType.ValueType:
			case ElementType.Class:
			case ElementType.TypedByRef:
			case (ElementType)23:
			case ElementType.I:
			case ElementType.U:
			case (ElementType)26:
			case ElementType.Object:
				break;
			case ElementType.Var:
			{
				GenericParameter genericParameter = (GenericParameter)type;
				if (genericParameter.DeclaringType == null)
				{
					throw new InvalidOperationException();
				}
				return context.TypeParameter(genericParameter.DeclaringType.FullName, genericParameter.Position);
			}
			case ElementType.Array:
			{
				ArrayType arrayType = (ArrayType)type;
				ArrayType arrayType2 = new ArrayType(this.ImportType(arrayType.ElementType, context));
				if (arrayType.IsVector)
				{
					return arrayType2;
				}
				Collection<ArrayDimension> dimensions = arrayType.Dimensions;
				Collection<ArrayDimension> dimensions2 = arrayType2.Dimensions;
				dimensions2.Clear();
				for (int i = 0; i < dimensions.Count; i++)
				{
					ArrayDimension arrayDimension = dimensions[i];
					dimensions2.Add(new ArrayDimension(arrayDimension.LowerBound, arrayDimension.UpperBound));
				}
				return arrayType2;
			}
			case ElementType.GenericInst:
			{
				GenericInstanceType genericInstanceType = (GenericInstanceType)type;
				GenericInstanceType genericInstanceType2 = new GenericInstanceType(this.ImportType(genericInstanceType.ElementType, context));
				Collection<TypeReference> genericArguments = genericInstanceType.GenericArguments;
				Collection<TypeReference> genericArguments2 = genericInstanceType2.GenericArguments;
				for (int j = 0; j < genericArguments.Count; j++)
				{
					genericArguments2.Add(this.ImportType(genericArguments[j], context));
				}
				return genericInstanceType2;
			}
			case ElementType.FnPtr:
			{
				FunctionPointerType functionPointerType = (FunctionPointerType)type;
				FunctionPointerType functionPointerType2 = new FunctionPointerType
				{
					HasThis = functionPointerType.HasThis,
					ExplicitThis = functionPointerType.ExplicitThis,
					CallingConvention = functionPointerType.CallingConvention,
					ReturnType = this.ImportType(functionPointerType.ReturnType, context)
				};
				if (!functionPointerType.HasParameters)
				{
					return functionPointerType2;
				}
				for (int k = 0; k < functionPointerType.Parameters.Count; k++)
				{
					functionPointerType2.Parameters.Add(new ParameterDefinition(this.ImportType(functionPointerType.Parameters[k].ParameterType, context)));
				}
				return functionPointerType2;
			}
			case ElementType.SzArray:
			{
				ArrayType arrayType3 = (ArrayType)type;
				return new ArrayType(this.ImportType(arrayType3.ElementType, context));
			}
			case ElementType.MVar:
			{
				GenericParameter genericParameter2 = (GenericParameter)type;
				if (genericParameter2.DeclaringMethod == null)
				{
					throw new InvalidOperationException();
				}
				return context.MethodParameter(context.NormalizeMethodName(genericParameter2.DeclaringMethod), genericParameter2.Position);
			}
			case ElementType.CModReqD:
			{
				RequiredModifierType requiredModifierType = (RequiredModifierType)type;
				return new RequiredModifierType(this.ImportType(requiredModifierType.ModifierType, context), this.ImportType(requiredModifierType.ElementType, context));
			}
			case ElementType.CModOpt:
			{
				OptionalModifierType optionalModifierType = (OptionalModifierType)type;
				return new OptionalModifierType(this.ImportType(optionalModifierType.ModifierType, context), this.ImportType(optionalModifierType.ElementType, context));
			}
			default:
				if (etype == ElementType.Sentinel)
				{
					SentinelType sentinelType = (SentinelType)type;
					return new SentinelType(this.ImportType(sentinelType.ElementType, context));
				}
				if (etype == ElementType.Pinned)
				{
					PinnedType pinnedType = (PinnedType)type;
					return new PinnedType(this.ImportType(pinnedType.ElementType, context));
				}
				break;
			}
			throw new NotSupportedException(type.etype.ToString());
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000A9EC File Offset: 0x00008BEC
		private FieldReference ImportField(FieldReference field, ImportGenericContext context)
		{
			TypeReference typeReference = this.ImportType(field.DeclaringType, context);
			context.Push(typeReference);
			FieldReference fieldReference;
			try
			{
				fieldReference = new FieldReference
				{
					Name = field.Name,
					DeclaringType = typeReference,
					FieldType = this.ImportType(field.FieldType, context)
				};
			}
			finally
			{
				context.Pop();
			}
			return fieldReference;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000AA58 File Offset: 0x00008C58
		private MethodReference ImportMethod(MethodReference method, ImportGenericContext context)
		{
			if (method.IsGenericInstance)
			{
				return this.ImportMethodSpecification(method, context);
			}
			TypeReference typeReference = this.ImportType(method.DeclaringType, context);
			MethodReference methodReference = new MethodReference
			{
				Name = method.Name,
				HasThis = method.HasThis,
				ExplicitThis = method.ExplicitThis,
				DeclaringType = typeReference,
				CallingConvention = method.CallingConvention
			};
			if (method.HasGenericParameters)
			{
				MetadataImporter.ImportGenericParameters(methodReference, method);
			}
			context.Push(methodReference);
			MethodReference methodReference2;
			try
			{
				methodReference.ReturnType = this.ImportType(method.ReturnType, context);
				if (!method.HasParameters)
				{
					methodReference2 = methodReference;
				}
				else
				{
					Collection<ParameterDefinition> parameters = method.Parameters;
					ParameterDefinitionCollection parameterDefinitionCollection = (methodReference.parameters = new ParameterDefinitionCollection(methodReference, parameters.Count));
					for (int i = 0; i < parameters.Count; i++)
					{
						parameterDefinitionCollection.Add(new ParameterDefinition(this.ImportType(parameters[i].ParameterType, context)));
					}
					methodReference2 = methodReference;
				}
			}
			finally
			{
				context.Pop();
			}
			return methodReference2;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000AB6C File Offset: 0x00008D6C
		private MethodSpecification ImportMethodSpecification(MethodReference method, ImportGenericContext context)
		{
			if (!method.IsGenericInstance)
			{
				throw new NotSupportedException();
			}
			GenericInstanceMethod genericInstanceMethod = (GenericInstanceMethod)method;
			GenericInstanceMethod genericInstanceMethod2 = new GenericInstanceMethod(this.ImportMethod(genericInstanceMethod.ElementMethod, context));
			Collection<TypeReference> genericArguments = genericInstanceMethod.GenericArguments;
			Collection<TypeReference> genericArguments2 = genericInstanceMethod2.GenericArguments;
			for (int i = 0; i < genericArguments.Count; i++)
			{
				genericArguments2.Add(this.ImportType(genericArguments[i], context));
			}
			return genericInstanceMethod2;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000ABDA File Offset: 0x00008DDA
		public virtual TypeReference ImportReference(TypeReference type, IGenericParameterProvider context)
		{
			Mixin.CheckType(type);
			return this.ImportType(type, ImportGenericContext.For(context));
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000ABEF File Offset: 0x00008DEF
		public virtual FieldReference ImportReference(FieldReference field, IGenericParameterProvider context)
		{
			Mixin.CheckField(field);
			return this.ImportField(field, ImportGenericContext.For(context));
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000AC04 File Offset: 0x00008E04
		public virtual MethodReference ImportReference(MethodReference method, IGenericParameterProvider context)
		{
			Mixin.CheckMethod(method);
			return this.ImportMethod(method, ImportGenericContext.For(context));
		}

		// Token: 0x040000ED RID: 237
		private readonly ModuleDefinition module;
	}
}
