using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.Cci
{
	// Token: 0x020000BF RID: 191
	internal abstract class ReferenceIndexerBase : MetadataVisitor
	{
		// Token: 0x06000629 RID: 1577 RVA: 0x00016F73 File Offset: 0x00015173
		internal ReferenceIndexerBase(EmitContext context)
			: base(context)
		{
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00016F92 File Offset: 0x00015192
		public override void Visit(IAssemblyReference assemblyReference)
		{
			if (assemblyReference != this.Context.Module.GetContainingAssembly(this.Context))
			{
				this.RecordAssemblyReference(assemblyReference);
			}
		}

		// Token: 0x0600062B RID: 1579
		protected abstract void RecordAssemblyReference(IAssemblyReference assemblyReference);

		// Token: 0x0600062C RID: 1580 RVA: 0x00016FB4 File Offset: 0x000151B4
		public override void Visit(ICustomModifier customModifier)
		{
			this.typeReferenceNeedsToken = true;
			this.Visit(customModifier.GetModifier(this.Context));
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00016FCF File Offset: 0x000151CF
		public override void Visit(IEventDefinition eventDefinition)
		{
			this.typeReferenceNeedsToken = true;
			this.Visit(eventDefinition.GetType(this.Context));
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00016FEC File Offset: 0x000151EC
		public override void Visit(IFieldReference fieldReference)
		{
			if (!this._alreadySeen.Add(new IReferenceOrISignature(fieldReference)))
			{
				return;
			}
			IUnitReference definingUnitReference = MetadataWriter.GetDefiningUnitReference(fieldReference.GetContainingType(this.Context), this.Context);
			if (definingUnitReference != null && definingUnitReference == this.Context.Module)
			{
				return;
			}
			this.Visit(fieldReference);
			this.Visit(fieldReference.GetType(this.Context));
			this.ReserveFieldToken(fieldReference);
		}

		// Token: 0x0600062F RID: 1583
		protected abstract void ReserveFieldToken(IFieldReference fieldReference);

		// Token: 0x06000630 RID: 1584 RVA: 0x00017057 File Offset: 0x00015257
		public override void Visit(IFileReference fileReference)
		{
			this.RecordFileReference(fileReference);
		}

		// Token: 0x06000631 RID: 1585
		protected abstract void RecordFileReference(IFileReference fileReference);

		// Token: 0x06000632 RID: 1586 RVA: 0x00017060 File Offset: 0x00015260
		public override void Visit(IGenericMethodInstanceReference genericMethodInstanceReference)
		{
			base.Visit(genericMethodInstanceReference.GetGenericArguments(this.Context));
			this.Visit(genericMethodInstanceReference.GetGenericMethod(this.Context));
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00017086 File Offset: 0x00015286
		public override void Visit(IGenericParameter genericParameter)
		{
			base.Visit(genericParameter.GetAttributes(this.Context));
			this.VisitTypeReferencesThatNeedTokens(genericParameter.GetConstraints(this.Context));
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x000170AC File Offset: 0x000152AC
		public override void Visit(IGenericTypeInstanceReference genericTypeInstanceReference)
		{
			INestedTypeReference asNestedTypeReference = genericTypeInstanceReference.AsNestedTypeReference;
			if (asNestedTypeReference != null)
			{
				ITypeReference containingType = asNestedTypeReference.GetContainingType(this.Context);
				if (containingType.AsGenericTypeInstanceReference != null || containingType.AsSpecializedNestedTypeReference != null)
				{
					this.Visit(asNestedTypeReference.GetContainingType(this.Context));
				}
			}
			this.Visit(genericTypeInstanceReference.GetGenericType(this.Context));
			base.Visit(genericTypeInstanceReference.GetGenericArguments(this.Context));
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0001711B File Offset: 0x0001531B
		public override void Visit(IMarshallingInformation marshallingInformation)
		{
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001711D File Offset: 0x0001531D
		public override void Visit(IMethodDefinition method)
		{
			base.Visit(method);
			this.ProcessMethodBody(method);
		}

		// Token: 0x06000637 RID: 1591
		protected abstract void ProcessMethodBody(IMethodDefinition method);

		// Token: 0x06000638 RID: 1592 RVA: 0x00017130 File Offset: 0x00015330
		public override void Visit(IMethodReference methodReference)
		{
			IGenericMethodInstanceReference asGenericMethodInstanceReference = methodReference.AsGenericMethodInstanceReference;
			if (asGenericMethodInstanceReference != null)
			{
				this.Visit(asGenericMethodInstanceReference);
				return;
			}
			if (!this._alreadySeen.Add(new IReferenceOrISignature(methodReference)))
			{
				return;
			}
			IUnitReference definingUnitReference = MetadataWriter.GetDefiningUnitReference(methodReference.GetContainingType(this.Context), this.Context);
			if (definingUnitReference != null && definingUnitReference == this.Context.Module && !methodReference.AcceptsExtraArguments)
			{
				return;
			}
			this.Visit(methodReference);
			ISpecializedMethodReference asSpecializedMethodReference = methodReference.AsSpecializedMethodReference;
			this.VisitSignature(((asSpecializedMethodReference != null) ? asSpecializedMethodReference.UnspecializedVersion : null) ?? methodReference);
			if (methodReference.AcceptsExtraArguments)
			{
				base.Visit(methodReference.ExtraParameters);
			}
			this.ReserveMethodToken(methodReference);
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x000171D4 File Offset: 0x000153D4
		public void VisitSignature(ISignature signature)
		{
			this.Visit(signature.GetType(this.Context));
			base.Visit(signature.GetParameters(this.Context));
			base.Visit(signature.RefCustomModifiers);
			base.Visit(signature.ReturnValueCustomModifiers);
		}

		// Token: 0x0600063A RID: 1594
		protected abstract void ReserveMethodToken(IMethodReference methodReference);

		// Token: 0x0600063B RID: 1595
		public abstract override void Visit(CommonPEModuleBuilder module);

		// Token: 0x0600063C RID: 1596 RVA: 0x00017212 File Offset: 0x00015412
		public override void Visit(IModuleReference moduleReference)
		{
			if (moduleReference != this.Context.Module)
			{
				this.RecordModuleReference(moduleReference);
			}
		}

		// Token: 0x0600063D RID: 1597
		protected abstract void RecordModuleReference(IModuleReference moduleReference);

		// Token: 0x0600063E RID: 1598
		public abstract override void Visit(IPlatformInvokeInformation platformInvokeInformation);

		// Token: 0x0600063F RID: 1599 RVA: 0x0001722C File Offset: 0x0001542C
		public override void Visit(INamespaceTypeReference namespaceTypeReference)
		{
			if (!this.typeReferenceNeedsToken && namespaceTypeReference.TypeCode != PrimitiveTypeCode.NotPrimitive)
			{
				return;
			}
			this.RecordTypeReference(namespaceTypeReference);
			IUnitReference unit = namespaceTypeReference.GetUnit(this.Context);
			IAssemblyReference assemblyReference = unit as IAssemblyReference;
			if (assemblyReference != null)
			{
				this.Visit(assemblyReference);
				return;
			}
			IModuleReference moduleReference = unit as IModuleReference;
			if (moduleReference != null)
			{
				assemblyReference = moduleReference.GetContainingAssembly(this.Context);
				if (assemblyReference != null && assemblyReference != this.Context.Module.GetContainingAssembly(this.Context))
				{
					this.Visit(assemblyReference);
					return;
				}
				this.Visit(moduleReference);
			}
		}

		// Token: 0x06000640 RID: 1600
		protected abstract void RecordTypeReference(ITypeReference typeReference);

		// Token: 0x06000641 RID: 1601 RVA: 0x000172B4 File Offset: 0x000154B4
		public override void Visit(INestedTypeReference nestedTypeReference)
		{
			if (!this.typeReferenceNeedsToken && nestedTypeReference.AsSpecializedNestedTypeReference != null)
			{
				return;
			}
			this.RecordTypeReference(nestedTypeReference);
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x000172CE File Offset: 0x000154CE
		public override void Visit(IPropertyDefinition propertyDefinition)
		{
			base.Visit(propertyDefinition.RefCustomModifiers);
			base.Visit(propertyDefinition.ReturnValueCustomModifiers);
			this.Visit(propertyDefinition.GetType(this.Context));
			base.Visit(propertyDefinition.Parameters);
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00017308 File Offset: 0x00015508
		public override void Visit(ManagedResource resourceReference)
		{
			base.Visit(resourceReference.Attributes);
			IFileReference externalFile = resourceReference.ExternalFile;
			if (externalFile != null)
			{
				this.Visit(externalFile);
			}
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00017332 File Offset: 0x00015532
		public override void Visit(SecurityAttribute securityAttribute)
		{
			this.Visit(securityAttribute.Attribute);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00017344 File Offset: 0x00015544
		public void VisitTypeDefinitionNoMembers(ITypeDefinition typeDefinition)
		{
			base.Visit(typeDefinition.GetAttributes(this.Context));
			ITypeReference baseClass = typeDefinition.GetBaseClass(this.Context);
			if (baseClass != null)
			{
				this.typeReferenceNeedsToken = true;
				this.Visit(baseClass);
			}
			base.Visit(typeDefinition.GetExplicitImplementationOverrides(this.Context));
			if (typeDefinition.HasDeclarativeSecurity)
			{
				base.Visit(typeDefinition.SecurityAttributes);
			}
			this.VisitTypeReferencesThatNeedTokens(typeDefinition.Interfaces(this.Context));
			if (typeDefinition.IsGeneric)
			{
				base.Visit(typeDefinition.GenericParameters);
			}
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x000173D0 File Offset: 0x000155D0
		public override void Visit(ITypeDefinition typeDefinition)
		{
			this.VisitTypeDefinitionNoMembers(typeDefinition);
			base.Visit(typeDefinition.GetEvents(this.Context));
			base.Visit(typeDefinition.GetFields(this.Context));
			base.Visit(typeDefinition.GetMethods(this.Context));
			base.VisitNestedTypes(typeDefinition.GetNestedTypes(this.Context));
			base.Visit(typeDefinition.GetProperties(this.Context));
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00017440 File Offset: 0x00015640
		public void VisitTypeReferencesThatNeedTokens(IEnumerable<TypeReferenceWithAttributes> refsWithAttributes)
		{
			foreach (TypeReferenceWithAttributes typeReferenceWithAttributes in refsWithAttributes)
			{
				base.Visit(typeReferenceWithAttributes.Attributes);
				this.VisitTypeReferencesThatNeedTokens(typeReferenceWithAttributes.TypeRef);
			}
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x000174A0 File Offset: 0x000156A0
		private void VisitTypeReferencesThatNeedTokens(ITypeReference typeReference)
		{
			this.typeReferenceNeedsToken = true;
			this.Visit(typeReference);
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x000174B0 File Offset: 0x000156B0
		public override void Visit(ITypeMemberReference typeMemberReference)
		{
			this.RecordTypeMemberReference(typeMemberReference);
			this.typeReferenceNeedsToken = true;
			this.Visit(typeMemberReference.GetContainingType(this.Context));
		}

		// Token: 0x0600064A RID: 1610
		protected abstract void RecordTypeMemberReference(ITypeMemberReference typeMemberReference);

		// Token: 0x0600064B RID: 1611 RVA: 0x000174D4 File Offset: 0x000156D4
		public override void Visit(IArrayTypeReference arrayTypeReference)
		{
			ITypeReference typeReference = arrayTypeReference.GetElementType(this.Context);
			while (this.VisitTypeReference(typeReference))
			{
				if (!(typeReference is IArrayTypeReference))
				{
					base.DispatchAsReference(typeReference);
					return;
				}
				typeReference = ((IArrayTypeReference)typeReference).GetElementType(this.Context);
			}
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0001751C File Offset: 0x0001571C
		public override void Visit(IPointerTypeReference pointerTypeReference)
		{
			ITypeReference typeReference = pointerTypeReference.GetTargetType(this.Context);
			while (this.VisitTypeReference(typeReference))
			{
				if (!(typeReference is IPointerTypeReference))
				{
					base.DispatchAsReference(typeReference);
					return;
				}
				typeReference = ((IPointerTypeReference)typeReference).GetTargetType(this.Context);
			}
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00017563 File Offset: 0x00015763
		public override void Visit(ITypeReference typeReference)
		{
			if (this.VisitTypeReference(typeReference))
			{
				base.DispatchAsReference(typeReference);
			}
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00017578 File Offset: 0x00015778
		private bool VisitTypeReference(ITypeReference typeReference)
		{
			if (this._alreadySeen.Add(new IReferenceOrISignature(typeReference)))
			{
				INestedTypeReference asNestedTypeReference = typeReference.AsNestedTypeReference;
				if (this.typeReferenceNeedsToken || asNestedTypeReference != null || (typeReference.TypeCode == PrimitiveTypeCode.NotPrimitive && typeReference.AsNamespaceTypeReference != null))
				{
					ISpecializedNestedTypeReference specializedNestedTypeReference = ((asNestedTypeReference != null) ? asNestedTypeReference.AsSpecializedNestedTypeReference : null);
					if (specializedNestedTypeReference != null)
					{
						INestedTypeReference unspecializedVersion = specializedNestedTypeReference.GetUnspecializedVersion(this.Context);
						if (this._alreadyHasToken.Add(new IReferenceOrISignature(unspecializedVersion)))
						{
							this.RecordTypeReference(unspecializedVersion);
						}
					}
					if (this.typeReferenceNeedsToken && this._alreadyHasToken.Add(new IReferenceOrISignature(typeReference)))
					{
						this.RecordTypeReference(typeReference);
					}
					if (asNestedTypeReference != null)
					{
						this.typeReferenceNeedsToken = typeReference.AsSpecializedNestedTypeReference == null;
						this.Visit(asNestedTypeReference.GetContainingType(this.Context));
					}
				}
				this.typeReferenceNeedsToken = false;
				return true;
			}
			if (!this.typeReferenceNeedsToken)
			{
				return false;
			}
			this.typeReferenceNeedsToken = false;
			if (!this._alreadyHasToken.Add(new IReferenceOrISignature(typeReference)))
			{
				return false;
			}
			this.RecordTypeReference(typeReference);
			return false;
		}

		// Token: 0x040001F4 RID: 500
		private readonly HashSet<IReferenceOrISignature> _alreadySeen = new HashSet<IReferenceOrISignature>();

		// Token: 0x040001F5 RID: 501
		private readonly HashSet<IReferenceOrISignature> _alreadyHasToken = new HashSet<IReferenceOrISignature>();

		// Token: 0x040001F6 RID: 502
		protected bool typeReferenceNeedsToken;
	}
}
