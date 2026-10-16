using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.Emit;
using Roslyn.Utilities;

namespace Microsoft.Cci
{
	// Token: 0x020000AC RID: 172
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class MetadataVisitor
	{
		// Token: 0x06000476 RID: 1142 RVA: 0x0000E0B6 File Offset: 0x0000C2B6
		public MetadataVisitor(EmitContext context)
		{
			this.Context = context;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000E0C5 File Offset: 0x0000C2C5
		public virtual void Visit(IArrayTypeReference arrayTypeReference)
		{
			this.Visit(arrayTypeReference.GetElementType(this.Context));
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000E0DC File Offset: 0x0000C2DC
		public void Visit(IEnumerable<IAssemblyReference> assemblyReferences)
		{
			foreach (IAssemblyReference assemblyReference in assemblyReferences)
			{
				this.Visit(assemblyReference);
			}
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000E124 File Offset: 0x0000C324
		public virtual void Visit(IAssemblyReference assemblyReference)
		{
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000E128 File Offset: 0x0000C328
		public void Visit(IEnumerable<ICustomAttribute> customAttributes)
		{
			foreach (ICustomAttribute customAttribute in customAttributes)
			{
				this.Visit(customAttribute);
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0000E170 File Offset: 0x0000C370
		public virtual void Visit(ICustomAttribute customAttribute)
		{
			IMethodReference methodReference = customAttribute.Constructor(this.Context, false);
			if (methodReference == null)
			{
				return;
			}
			this.Visit(customAttribute.GetArguments(this.Context));
			this.Visit(methodReference);
			this.Visit(customAttribute.GetNamedArguments(this.Context));
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0000E1C4 File Offset: 0x0000C3C4
		public void Visit([Nullable(new byte[] { 0, 1 })] ImmutableArray<ICustomModifier> customModifiers)
		{
			foreach (ICustomModifier customModifier in customModifiers)
			{
				this.Visit(customModifier);
			}
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0000E1F3 File Offset: 0x0000C3F3
		public virtual void Visit(ICustomModifier customModifier)
		{
			this.Visit(customModifier.GetModifier(this.Context));
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0000E208 File Offset: 0x0000C408
		public void Visit(IEnumerable<IEventDefinition> events)
		{
			foreach (IEventDefinition eventDefinition in events)
			{
				this.Visit(eventDefinition);
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000E250 File Offset: 0x0000C450
		public virtual void Visit(IEventDefinition eventDefinition)
		{
			this.Visit(eventDefinition.GetAccessors(this.Context));
			this.Visit(eventDefinition.GetType(this.Context));
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0000E278 File Offset: 0x0000C478
		public void Visit(IEnumerable<IFieldDefinition> fields)
		{
			foreach (IFieldDefinition fieldDefinition in fields)
			{
				this.Visit(fieldDefinition);
			}
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0000E2C0 File Offset: 0x0000C4C0
		public virtual void Visit(IFieldDefinition fieldDefinition)
		{
			MetadataConstant compileTimeValue = fieldDefinition.GetCompileTimeValue(this.Context);
			IMarshallingInformation marshallingInformation = fieldDefinition.MarshallingInformation;
			if (compileTimeValue != null)
			{
				this.Visit(compileTimeValue);
			}
			if (marshallingInformation != null)
			{
				this.Visit(marshallingInformation);
			}
			this.Visit(fieldDefinition.GetType(this.Context));
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0000E307 File Offset: 0x0000C507
		public virtual void Visit(IFieldReference fieldReference)
		{
			this.Visit(fieldReference);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0000E310 File Offset: 0x0000C510
		public void Visit(IEnumerable<IFileReference> fileReferences)
		{
			foreach (IFileReference fileReference in fileReferences)
			{
				this.Visit(fileReference);
			}
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0000E358 File Offset: 0x0000C558
		public virtual void Visit(IFileReference fileReference)
		{
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000E35A File Offset: 0x0000C55A
		public virtual void Visit(IGenericMethodInstanceReference genericMethodInstanceReference)
		{
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000E35C File Offset: 0x0000C55C
		public void Visit(IEnumerable<IGenericMethodParameter> genericParameters)
		{
			foreach (IGenericMethodParameter genericMethodParameter in genericParameters)
			{
				this.Visit(genericMethodParameter);
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000E3A4 File Offset: 0x0000C5A4
		public virtual void Visit(IGenericMethodParameter genericMethodParameter)
		{
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000E3A6 File Offset: 0x0000C5A6
		public virtual void Visit(IGenericMethodParameterReference genericMethodParameterReference)
		{
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000E3A8 File Offset: 0x0000C5A8
		public virtual void Visit(IGenericParameter genericParameter)
		{
			this.Visit(genericParameter.GetAttributes(this.Context));
			this.Visit(genericParameter.GetConstraints(this.Context));
			genericParameter.Dispatch(this);
		}

		// Token: 0x0600048A RID: 1162
		public abstract void Visit(IGenericTypeInstanceReference genericTypeInstanceReference);

		// Token: 0x0600048B RID: 1163 RVA: 0x0000E3D8 File Offset: 0x0000C5D8
		public void Visit(IEnumerable<IGenericParameter> genericParameters)
		{
			foreach (IGenericParameter genericParameter in genericParameters)
			{
				IGenericTypeParameter genericTypeParameter = (IGenericTypeParameter)genericParameter;
				this.Visit(genericTypeParameter);
			}
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000E428 File Offset: 0x0000C628
		public virtual void Visit(IGenericTypeParameter genericTypeParameter)
		{
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000E42A File Offset: 0x0000C62A
		public virtual void Visit(IGenericTypeParameterReference genericTypeParameterReference)
		{
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000E42C File Offset: 0x0000C62C
		public virtual void Visit(IGlobalFieldDefinition globalFieldDefinition)
		{
			this.Visit(globalFieldDefinition);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000E435 File Offset: 0x0000C635
		public virtual void Visit(IGlobalMethodDefinition globalMethodDefinition)
		{
			this.Visit(globalMethodDefinition);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000E440 File Offset: 0x0000C640
		public void Visit([Nullable(new byte[] { 0, 1 })] ImmutableArray<ILocalDefinition> localDefinitions)
		{
			foreach (ILocalDefinition localDefinition in localDefinitions)
			{
				this.Visit(localDefinition);
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000E46F File Offset: 0x0000C66F
		public virtual void Visit(ILocalDefinition localDefinition)
		{
			this.Visit(localDefinition.CustomModifiers);
			this.Visit(localDefinition.Type);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0000E489 File Offset: 0x0000C689
		public virtual void Visit(IMarshallingInformation marshallingInformation)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0000E490 File Offset: 0x0000C690
		public virtual void Visit(MetadataConstant constant)
		{
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0000E492 File Offset: 0x0000C692
		public virtual void Visit(MetadataCreateArray createArray)
		{
			this.Visit(createArray.ElementType);
			this.Visit(createArray.Elements);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0000E4B4 File Offset: 0x0000C6B4
		public void Visit(IEnumerable<IMetadataExpression> expressions)
		{
			foreach (IMetadataExpression metadataExpression in expressions)
			{
				this.Visit(metadataExpression);
			}
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000E4FC File Offset: 0x0000C6FC
		public virtual void Visit(IMetadataExpression expression)
		{
			this.Visit(expression.Type);
			expression.Dispatch(this);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0000E514 File Offset: 0x0000C714
		public void Visit(IEnumerable<IMetadataNamedArgument> namedArguments)
		{
			foreach (IMetadataNamedArgument metadataNamedArgument in namedArguments)
			{
				this.Visit(metadataNamedArgument);
			}
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0000E55C File Offset: 0x0000C75C
		public virtual void Visit(IMetadataNamedArgument namedArgument)
		{
			this.Visit(namedArgument.ArgumentValue);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0000E56A File Offset: 0x0000C76A
		public virtual void Visit(MetadataTypeOf typeOf)
		{
			if (typeOf.TypeToGet != null)
			{
				this.Visit(typeOf.TypeToGet);
			}
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0000E580 File Offset: 0x0000C780
		public virtual void Visit(IMethodBody methodBody)
		{
			foreach (LocalScope localScope in methodBody.LocalScopes)
			{
				this.Visit(localScope.Constants);
			}
			this.Visit(methodBody.LocalVariables);
			this.Visit(methodBody.ExceptionRegions);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0000E5D4 File Offset: 0x0000C7D4
		public void Visit(IEnumerable<IMethodDefinition> methods)
		{
			foreach (IMethodDefinition methodDefinition in methods)
			{
				this.Visit(methodDefinition);
			}
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0000E61C File Offset: 0x0000C81C
		public virtual void Visit(IMethodDefinition method)
		{
			this.Visit(method.GetReturnValueAttributes(this.Context));
			this.Visit(method.RefCustomModifiers);
			this.Visit(method.ReturnValueCustomModifiers);
			if (method.HasDeclarativeSecurity)
			{
				this.Visit(method.SecurityAttributes);
			}
			if (method.IsGeneric)
			{
				this.Visit(method.GenericParameters);
			}
			this.Visit(method.GetType(this.Context));
			this.Visit(method.Parameters);
			if (method.IsPlatformInvoke)
			{
				this.Visit(method.PlatformInvokeData);
			}
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0000E6B0 File Offset: 0x0000C8B0
		public void Visit(IEnumerable<MethodImplementation> methodImplementations)
		{
			foreach (MethodImplementation methodImplementation in methodImplementations)
			{
				this.Visit(methodImplementation);
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0000E6F8 File Offset: 0x0000C8F8
		public virtual void Visit(MethodImplementation methodImplementation)
		{
			this.Visit(methodImplementation.ImplementedMethod);
			this.Visit(methodImplementation.ImplementingMethod);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0000E714 File Offset: 0x0000C914
		public void Visit(IEnumerable<IMethodReference> methodReferences)
		{
			foreach (IMethodReference methodReference in methodReferences)
			{
				this.Visit(methodReference);
			}
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0000E75C File Offset: 0x0000C95C
		public virtual void Visit(IMethodReference methodReference)
		{
			IGenericMethodInstanceReference asGenericMethodInstanceReference = methodReference.AsGenericMethodInstanceReference;
			if (asGenericMethodInstanceReference != null)
			{
				this.Visit(asGenericMethodInstanceReference);
				return;
			}
			this.Visit(methodReference);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0000E782 File Offset: 0x0000C982
		public virtual void Visit(IModifiedTypeReference modifiedTypeReference)
		{
			this.Visit(modifiedTypeReference.CustomModifiers);
			this.Visit(modifiedTypeReference.UnmodifiedType);
		}

		// Token: 0x060004A2 RID: 1186
		public abstract void Visit(CommonPEModuleBuilder module);

		// Token: 0x060004A3 RID: 1187 RVA: 0x0000E79C File Offset: 0x0000C99C
		public void Visit(IEnumerable<IModuleReference> moduleReferences)
		{
			foreach (IModuleReference moduleReference in moduleReferences)
			{
				this.Visit(moduleReference);
			}
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0000E7E4 File Offset: 0x0000C9E4
		public virtual void Visit(IModuleReference moduleReference)
		{
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0000E7E8 File Offset: 0x0000C9E8
		public void Visit(IEnumerable<INamedTypeDefinition> types)
		{
			foreach (INamedTypeDefinition namedTypeDefinition in types)
			{
				this.Visit(namedTypeDefinition);
			}
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0000E830 File Offset: 0x0000CA30
		public virtual void Visit(INamespaceTypeDefinition namespaceTypeDefinition)
		{
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0000E832 File Offset: 0x0000CA32
		public virtual void Visit(INamespaceTypeReference namespaceTypeReference)
		{
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0000E834 File Offset: 0x0000CA34
		public void VisitNestedTypes(IEnumerable<INamedTypeDefinition> nestedTypes)
		{
			foreach (INamedTypeDefinition namedTypeDefinition in nestedTypes)
			{
				ITypeDefinitionMember typeDefinitionMember = (ITypeDefinitionMember)namedTypeDefinition;
				this.Visit(typeDefinitionMember);
			}
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0000E884 File Offset: 0x0000CA84
		public virtual void Visit(INestedTypeDefinition nestedTypeDefinition)
		{
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0000E886 File Offset: 0x0000CA86
		public virtual void Visit(INestedTypeReference nestedTypeReference)
		{
			this.Visit(nestedTypeReference.GetContainingType(this.Context));
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0000E89C File Offset: 0x0000CA9C
		public void Visit([Nullable(new byte[] { 0, 1 })] ImmutableArray<ExceptionHandlerRegion> exceptionRegions)
		{
			foreach (ExceptionHandlerRegion exceptionHandlerRegion in exceptionRegions)
			{
				this.Visit(exceptionHandlerRegion);
			}
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0000E8CC File Offset: 0x0000CACC
		public virtual void Visit(ExceptionHandlerRegion exceptionRegion)
		{
			ITypeReference exceptionType = exceptionRegion.ExceptionType;
			if (exceptionType != null)
			{
				this.Visit(exceptionType);
			}
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0000E8EC File Offset: 0x0000CAEC
		public void Visit([Nullable(new byte[] { 0, 1 })] ImmutableArray<IParameterDefinition> parameters)
		{
			foreach (IParameterDefinition parameterDefinition in parameters)
			{
				this.Visit(parameterDefinition);
			}
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0000E91C File Offset: 0x0000CB1C
		public virtual void Visit(IParameterDefinition parameterDefinition)
		{
			IMarshallingInformation marshallingInformation = parameterDefinition.MarshallingInformation;
			this.Visit(parameterDefinition.GetAttributes(this.Context));
			this.Visit(parameterDefinition.RefCustomModifiers);
			this.Visit(parameterDefinition.CustomModifiers);
			MetadataConstant defaultValue = parameterDefinition.GetDefaultValue(this.Context);
			if (defaultValue != null)
			{
				this.Visit(defaultValue);
			}
			if (marshallingInformation != null)
			{
				this.Visit(marshallingInformation);
			}
			this.Visit(parameterDefinition.GetType(this.Context));
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0000E990 File Offset: 0x0000CB90
		public void Visit([Nullable(new byte[] { 0, 1 })] ImmutableArray<IParameterTypeInformation> parameterTypeInformations)
		{
			foreach (IParameterTypeInformation parameterTypeInformation in parameterTypeInformations)
			{
				this.Visit(parameterTypeInformation);
			}
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0000E9BF File Offset: 0x0000CBBF
		public virtual void Visit(IParameterTypeInformation parameterTypeInformation)
		{
			this.Visit(parameterTypeInformation.RefCustomModifiers);
			this.Visit(parameterTypeInformation.CustomModifiers);
			this.Visit(parameterTypeInformation.GetType(this.Context));
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0000E9EB File Offset: 0x0000CBEB
		public virtual void Visit(IPlatformInvokeInformation platformInvokeInformation)
		{
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0000E9ED File Offset: 0x0000CBED
		public virtual void Visit(IPointerTypeReference pointerTypeReference)
		{
			this.Visit(pointerTypeReference.GetTargetType(this.Context));
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0000EA04 File Offset: 0x0000CC04
		public virtual void Visit(IFunctionPointerTypeReference functionPointerTypeReference)
		{
			this.Visit(functionPointerTypeReference.Signature.RefCustomModifiers);
			this.Visit(functionPointerTypeReference.Signature.ReturnValueCustomModifiers);
			this.Visit(functionPointerTypeReference.Signature.GetType(this.Context));
			foreach (IParameterTypeInformation parameterTypeInformation in functionPointerTypeReference.Signature.GetParameters(this.Context))
			{
				this.Visit(parameterTypeInformation);
			}
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0000EA80 File Offset: 0x0000CC80
		public void Visit(IEnumerable<IPropertyDefinition> properties)
		{
			foreach (IPropertyDefinition propertyDefinition in properties)
			{
				this.Visit(propertyDefinition);
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0000EAC8 File Offset: 0x0000CCC8
		public virtual void Visit(IPropertyDefinition propertyDefinition)
		{
			this.Visit(propertyDefinition.GetAccessors(this.Context));
			this.Visit(propertyDefinition.Parameters);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0000EAE8 File Offset: 0x0000CCE8
		public void Visit(IEnumerable<ManagedResource> resources)
		{
			foreach (ManagedResource managedResource in resources)
			{
				this.Visit(managedResource);
			}
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0000EB30 File Offset: 0x0000CD30
		public virtual void Visit(ManagedResource resource)
		{
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0000EB32 File Offset: 0x0000CD32
		public virtual void Visit(SecurityAttribute securityAttribute)
		{
			this.Visit(securityAttribute.Attribute);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0000EB44 File Offset: 0x0000CD44
		public void Visit(IEnumerable<SecurityAttribute> securityAttributes)
		{
			foreach (SecurityAttribute securityAttribute in securityAttributes)
			{
				this.Visit(securityAttribute);
			}
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0000EB8C File Offset: 0x0000CD8C
		public void Visit(IEnumerable<ITypeDefinitionMember> typeMembers)
		{
			foreach (ITypeDefinitionMember typeDefinitionMember in typeMembers)
			{
				this.Visit(typeDefinitionMember);
			}
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0000EBD4 File Offset: 0x0000CDD4
		public void Visit(IEnumerable<ITypeDefinition> types)
		{
			foreach (ITypeDefinition typeDefinition in types)
			{
				this.Visit(typeDefinition);
			}
		}

		// Token: 0x060004BC RID: 1212
		public abstract void Visit(ITypeDefinition typeDefinition);

		// Token: 0x060004BD RID: 1213 RVA: 0x0000EC1C File Offset: 0x0000CE1C
		public virtual void Visit(ITypeDefinitionMember typeMember)
		{
			ITypeDefinition typeDefinition = typeMember as INestedTypeDefinition;
			if (typeDefinition != null)
			{
				this.Visit(typeDefinition);
				return;
			}
			this.Visit(typeMember.GetAttributes(this.Context));
			typeMember.Dispatch(this);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0000EC54 File Offset: 0x0000CE54
		public virtual void Visit(ITypeMemberReference typeMemberReference)
		{
			if (typeMemberReference.AsDefinition(this.Context) == null)
			{
				this.Visit(typeMemberReference.GetAttributes(this.Context));
			}
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0000EC78 File Offset: 0x0000CE78
		public void Visit(IEnumerable<ITypeReference> typeReferences)
		{
			foreach (ITypeReference typeReference in typeReferences)
			{
				this.Visit(typeReference);
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0000ECC0 File Offset: 0x0000CEC0
		public void Visit(IEnumerable<TypeReferenceWithAttributes> typeRefsWithAttributes)
		{
			foreach (TypeReferenceWithAttributes typeReferenceWithAttributes in typeRefsWithAttributes)
			{
				this.Visit(typeReferenceWithAttributes.TypeRef);
				this.Visit(typeReferenceWithAttributes.Attributes);
			}
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0000ED20 File Offset: 0x0000CF20
		public virtual void Visit(ITypeReference typeReference)
		{
			this.DispatchAsReference(typeReference);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0000ED2C File Offset: 0x0000CF2C
		protected void DispatchAsReference(ITypeReference typeReference)
		{
			INamespaceTypeReference asNamespaceTypeReference = typeReference.AsNamespaceTypeReference;
			if (asNamespaceTypeReference != null)
			{
				this.Visit(asNamespaceTypeReference);
				return;
			}
			IGenericTypeInstanceReference asGenericTypeInstanceReference = typeReference.AsGenericTypeInstanceReference;
			if (asGenericTypeInstanceReference != null)
			{
				this.Visit(asGenericTypeInstanceReference);
				return;
			}
			INestedTypeReference asNestedTypeReference = typeReference.AsNestedTypeReference;
			if (asNestedTypeReference != null)
			{
				this.Visit(asNestedTypeReference);
				return;
			}
			IArrayTypeReference arrayTypeReference = typeReference as IArrayTypeReference;
			if (arrayTypeReference != null)
			{
				this.Visit(arrayTypeReference);
				return;
			}
			IGenericTypeParameterReference asGenericTypeParameterReference = typeReference.AsGenericTypeParameterReference;
			if (asGenericTypeParameterReference != null)
			{
				this.Visit(asGenericTypeParameterReference);
				return;
			}
			IGenericMethodParameterReference asGenericMethodParameterReference = typeReference.AsGenericMethodParameterReference;
			if (asGenericMethodParameterReference != null)
			{
				this.Visit(asGenericMethodParameterReference);
				return;
			}
			IPointerTypeReference pointerTypeReference = typeReference as IPointerTypeReference;
			if (pointerTypeReference != null)
			{
				this.Visit(pointerTypeReference);
				return;
			}
			IFunctionPointerTypeReference functionPointerTypeReference = typeReference as IFunctionPointerTypeReference;
			if (functionPointerTypeReference != null)
			{
				this.Visit(functionPointerTypeReference);
				return;
			}
			IModifiedTypeReference modifiedTypeReference = typeReference as IModifiedTypeReference;
			if (modifiedTypeReference != null)
			{
				this.Visit(modifiedTypeReference);
				return;
			}
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0000EDEC File Offset: 0x0000CFEC
		public void Visit(IEnumerable<IUnitReference> unitReferences)
		{
			foreach (IUnitReference unitReference in unitReferences)
			{
				this.Visit(unitReference);
			}
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0000EE34 File Offset: 0x0000D034
		public virtual void Visit(IUnitReference unitReference)
		{
			this.DispatchAsReference(unitReference);
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0000EE40 File Offset: 0x0000D040
		private void DispatchAsReference(IUnitReference unitReference)
		{
			IAssemblyReference assemblyReference = unitReference as IAssemblyReference;
			if (assemblyReference != null)
			{
				this.Visit(assemblyReference);
				return;
			}
			IModuleReference moduleReference = unitReference as IModuleReference;
			if (moduleReference != null)
			{
				this.Visit(moduleReference);
				return;
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0000EE71 File Offset: 0x0000D071
		public virtual void Visit(IWin32Resource win32Resource)
		{
		}

		// Token: 0x040001AC RID: 428
		public readonly EmitContext Context;
	}
}
