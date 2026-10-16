using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.Emit;
using Roslyn.Utilities;

namespace Microsoft.Cci
{
	// Token: 0x020000BE RID: 190
	internal abstract class ReferenceIndexer : ReferenceIndexerBase
	{
		// Token: 0x0600061B RID: 1563 RVA: 0x00016C63 File Offset: 0x00014E63
		internal ReferenceIndexer(MetadataWriter metadataWriter)
			: base(metadataWriter.Context)
		{
			this.metadataWriter = metadataWriter;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00016C84 File Offset: 0x00014E84
		public override void Visit(CommonPEModuleBuilder module)
		{
			base.Visit(module.GetSourceAssemblyAttributes(this.Context.IsRefAssembly));
			base.Visit(module.GetSourceAssemblySecurityAttributes());
			base.Visit(module.GetAssemblyReferences(this.Context));
			base.Visit(module.GetSourceModuleAttributes());
			base.Visit(module.GetTopLevelTypeDefinitions(this.Context));
			foreach (ExportedType exportedType in module.GetExportedTypes(this.Context.Diagnostics))
			{
				this.VisitExportedType(exportedType.Type);
			}
			base.Visit(module.GetResources(this.Context));
			this.VisitImports(module.GetImports());
			base.Visit(module.GetFiles(this.Context));
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00016D54 File Offset: 0x00014F54
		private void VisitExportedType(ITypeReference exportedType)
		{
			IUnitReference definingUnitReference = MetadataWriter.GetDefiningUnitReference(exportedType, this.Context);
			IAssemblyReference assemblyReference = definingUnitReference as IAssemblyReference;
			if (assemblyReference != null)
			{
				this.Visit(assemblyReference);
				return;
			}
			assemblyReference = ((IModuleReference)definingUnitReference).GetContainingAssembly(this.Context);
			if (assemblyReference != null && assemblyReference != this.Context.Module.GetContainingAssembly(this.Context))
			{
				this.Visit(assemblyReference);
			}
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00016DB8 File Offset: 0x00014FB8
		public void VisitMethodBodyReference(IReference reference)
		{
			ITypeReference typeReference = reference as ITypeReference;
			if (typeReference != null)
			{
				this.typeReferenceNeedsToken = true;
				this.Visit(typeReference);
				return;
			}
			IFieldReference fieldReference = reference as IFieldReference;
			if (fieldReference != null)
			{
				if (fieldReference.IsContextualNamedEntity)
				{
					((IContextualNamedEntity)fieldReference).AssociateWithMetadataWriter(this.metadataWriter);
				}
				this.Visit(fieldReference);
				return;
			}
			IMethodReference methodReference = reference as IMethodReference;
			if (methodReference != null)
			{
				this.Visit(methodReference);
			}
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00016E1A File Offset: 0x0001501A
		protected override void RecordAssemblyReference(IAssemblyReference assemblyReference)
		{
			this.metadataWriter.GetAssemblyReferenceHandle(assemblyReference);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00016E2C File Offset: 0x0001502C
		protected override void ProcessMethodBody(IMethodDefinition method)
		{
			if (method.HasBody() && !this.metadataWriter.MetadataOnly)
			{
				IMethodBody body = method.GetBody(this.Context);
				if (body != null)
				{
					this.Visit(body);
					for (IImportScope importScope = body.ImportScope; importScope != null; importScope = importScope.Parent)
					{
						if (!this._alreadySeenScopes.Add(importScope))
						{
							return;
						}
						this.VisitImports(importScope.GetUsedNamespaces());
					}
					return;
				}
				if (!this.metadataWriter.MetadataOnly)
				{
					throw ExceptionUtilities.Unreachable;
				}
			}
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00016EA8 File Offset: 0x000150A8
		private void VisitImports(ImmutableArray<UsedNamespaceOrType> imports)
		{
			foreach (UsedNamespaceOrType usedNamespaceOrType in imports)
			{
				if (usedNamespaceOrType.TargetAssemblyOpt != null)
				{
					this.Visit(usedNamespaceOrType.TargetAssemblyOpt);
				}
				if (usedNamespaceOrType.TargetTypeOpt != null)
				{
					this.typeReferenceNeedsToken = true;
					this.Visit(usedNamespaceOrType.TargetTypeOpt);
				}
			}
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00016EFF File Offset: 0x000150FF
		protected override void RecordTypeReference(ITypeReference typeReference)
		{
			this.metadataWriter.GetTypeHandle(typeReference, true);
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00016F0F File Offset: 0x0001510F
		protected override void RecordTypeMemberReference(ITypeMemberReference typeMemberReference)
		{
			this.metadataWriter.GetMemberReferenceHandle(typeMemberReference);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00016F1E File Offset: 0x0001511E
		protected override void RecordFileReference(IFileReference fileReference)
		{
			this.metadataWriter.GetAssemblyFileHandle(fileReference);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00016F2D File Offset: 0x0001512D
		protected override void ReserveMethodToken(IMethodReference methodReference)
		{
			this.metadataWriter.GetMethodHandle(methodReference);
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00016F3C File Offset: 0x0001513C
		protected override void ReserveFieldToken(IFieldReference fieldReference)
		{
			this.metadataWriter.GetFieldHandle(fieldReference);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00016F4B File Offset: 0x0001514B
		protected override void RecordModuleReference(IModuleReference moduleReference)
		{
			this.metadataWriter.GetModuleReferenceHandle(moduleReference.Name);
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00016F5F File Offset: 0x0001515F
		public override void Visit(IPlatformInvokeInformation platformInvokeInformation)
		{
			this.metadataWriter.GetModuleReferenceHandle(platformInvokeInformation.ModuleName);
		}

		// Token: 0x040001F2 RID: 498
		protected readonly MetadataWriter metadataWriter;

		// Token: 0x040001F3 RID: 499
		private readonly HashSet<IImportScope> _alreadySeenScopes = new HashSet<IImportScope>();
	}
}
