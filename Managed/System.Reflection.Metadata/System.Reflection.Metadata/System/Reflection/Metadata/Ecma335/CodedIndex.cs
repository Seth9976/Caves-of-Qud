using System;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000EC RID: 236
	public static class CodedIndex
	{
		/// <summary>Calculates a HasCustomAttribute coded index for the specified handle.</summary>
		/// <param name="handle">
		///   <see cref="T:System.Reflection.Metadata.MethodDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.FieldDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.TypeReferenceHandle" />, <see cref="T:System.Reflection.Metadata.TypeDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.ParameterHandle" />, <see cref="T:System.Reflection.Metadata.InterfaceImplementationHandle" />, <see cref="T:System.Reflection.Metadata.MemberReferenceHandle" />, <see cref="T:System.Reflection.Metadata.ModuleDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.DeclarativeSecurityAttributeHandle" />, <see cref="T:System.Reflection.Metadata.PropertyDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.EventDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.StandaloneSignatureHandle" />, <see cref="T:System.Reflection.Metadata.ModuleReferenceHandle" />, <see cref="T:System.Reflection.Metadata.TypeSpecificationHandle" />, <see cref="T:System.Reflection.Metadata.AssemblyDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.AssemblyReferenceHandle" />, <see cref="T:System.Reflection.Metadata.AssemblyFileHandle" />, <see cref="T:System.Reflection.Metadata.ExportedTypeHandle" />, <see cref="T:System.Reflection.Metadata.ManifestResourceHandle" />, <see cref="T:System.Reflection.Metadata.GenericParameterHandle" />, <see cref="T:System.Reflection.Metadata.GenericParameterConstraintHandle" /> or <see cref="T:System.Reflection.Metadata.MethodSpecificationHandle" />.</param>
		/// <exception cref="T:System.ArgumentException">The handle type is unexpected.</exception>
		// Token: 0x06000909 RID: 2313 RVA: 0x00019CD2 File Offset: 0x00017ED2
		public static int HasCustomAttribute(EntityHandle handle)
		{
			return (handle.RowId << 5) | (int)CodedIndex.ToHasCustomAttributeTag(handle.Kind);
		}

		/// <summary>Calculates a HasConstant coded index for the specified handle.</summary>
		/// <param name="handle">
		///   <see cref="T:System.Reflection.Metadata.ParameterHandle" />, <see cref="T:System.Reflection.Metadata.FieldDefinitionHandle" />, or <see cref="T:System.Reflection.Metadata.PropertyDefinitionHandle" />.</param>
		/// <exception cref="T:System.ArgumentException">The handle type is unexpected.</exception>
		// Token: 0x0600090A RID: 2314 RVA: 0x00019CEA File Offset: 0x00017EEA
		public static int HasConstant(EntityHandle handle)
		{
			return (handle.RowId << 2) | (int)CodedIndex.ToHasConstantTag(handle.Kind);
		}

		/// <summary>Calculates a CustomAttributeType coded index for the specified handle.</summary>
		/// <param name="handle">
		///   <see cref="T:System.Reflection.Metadata.MethodDefinitionHandle" /> or <see cref="T:System.Reflection.Metadata.MemberReferenceHandle" />.</param>
		/// <exception cref="T:System.ArgumentException">The handle type is unexpected.</exception>
		// Token: 0x0600090B RID: 2315 RVA: 0x00019D02 File Offset: 0x00017F02
		public static int CustomAttributeType(EntityHandle handle)
		{
			return (handle.RowId << 3) | (int)CodedIndex.ToCustomAttributeTypeTag(handle.Kind);
		}

		/// <summary>Calculates a HasDeclSecurity coded index for the specified handle.</summary>
		/// <param name="handle">
		///   <see cref="T:System.Reflection.Metadata.TypeDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.MethodDefinitionHandle" />, or <see cref="T:System.Reflection.Metadata.AssemblyDefinitionHandle" />.</param>
		/// <exception cref="T:System.ArgumentException">The handle type is unexpected.</exception>
		// Token: 0x0600090C RID: 2316 RVA: 0x00019D1A File Offset: 0x00017F1A
		public static int HasDeclSecurity(EntityHandle handle)
		{
			return (handle.RowId << 2) | (int)CodedIndex.ToHasDeclSecurityTag(handle.Kind);
		}

		/// <summary>Calculates a HasFieldMarshal coded index for the specified handle.</summary>
		/// <param name="handle">
		///   <see cref="T:System.Reflection.Metadata.ParameterHandle" /> or <see cref="T:System.Reflection.Metadata.FieldDefinitionHandle" />.</param>
		/// <exception cref="T:System.ArgumentException">The handle type is unexpected.</exception>
		// Token: 0x0600090D RID: 2317 RVA: 0x00019D32 File Offset: 0x00017F32
		public static int HasFieldMarshal(EntityHandle handle)
		{
			return (handle.RowId << 1) | (int)CodedIndex.ToHasFieldMarshalTag(handle.Kind);
		}

		/// <summary>Calculates a HasSemantics coded index for the specified handle.</summary>
		/// <param name="handle">
		///   <see cref="T:System.Reflection.Metadata.EventDefinitionHandle" /> or <see cref="T:System.Reflection.Metadata.PropertyDefinitionHandle" />.</param>
		/// <exception cref="T:System.ArgumentException">The handle type is unexpected.</exception>
		// Token: 0x0600090E RID: 2318 RVA: 0x00019D4A File Offset: 0x00017F4A
		public static int HasSemantics(EntityHandle handle)
		{
			return (handle.RowId << 1) | (int)CodedIndex.ToHasSemanticsTag(handle.Kind);
		}

		/// <summary>Calculates an implementation coded index for the specified handle.</summary>
		/// <param name="handle">
		///   <see cref="T:System.Reflection.Metadata.AssemblyFileHandle" />, <see cref="T:System.Reflection.Metadata.ExportedTypeHandle" /> or <see cref="T:System.Reflection.Metadata.AssemblyReferenceHandle" />.</param>
		/// <exception cref="T:System.ArgumentException">The handle type is unexpected.</exception>
		// Token: 0x0600090F RID: 2319 RVA: 0x00019D62 File Offset: 0x00017F62
		public static int Implementation(EntityHandle handle)
		{
			return (handle.RowId << 2) | (int)CodedIndex.ToImplementationTag(handle.Kind);
		}

		/// <summary>Calculates a MemberForwarded coded index for the specified handle.</summary>
		/// <param name="handle">
		///   <see cref="T:System.Reflection.Metadata.FieldDefinition" />, <see cref="T:System.Reflection.Metadata.MethodDefinition" />.</param>
		/// <exception cref="T:System.ArgumentException">The handle type is unexpected.</exception>
		// Token: 0x06000910 RID: 2320 RVA: 0x00019D7A File Offset: 0x00017F7A
		public static int MemberForwarded(EntityHandle handle)
		{
			return (handle.RowId << 1) | (int)CodedIndex.ToMemberForwardedTag(handle.Kind);
		}

		/// <summary>Calculates a MemberRefParent coded index for the specified handle.</summary>
		/// <param name="handle">
		///   <see cref="T:System.Reflection.Metadata.TypeDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.TypeReferenceHandle" />, <see cref="T:System.Reflection.Metadata.ModuleReferenceHandle" />, <see cref="T:System.Reflection.Metadata.MethodDefinitionHandle" />, or <see cref="T:System.Reflection.Metadata.TypeSpecificationHandle" />.</param>
		/// <exception cref="T:System.ArgumentException">The handle type is unexpected.</exception>
		// Token: 0x06000911 RID: 2321 RVA: 0x00019D92 File Offset: 0x00017F92
		public static int MemberRefParent(EntityHandle handle)
		{
			return (handle.RowId << 3) | (int)CodedIndex.ToMemberRefParentTag(handle.Kind);
		}

		/// <summary>Calculates a MethodDefOrRef coded index for the specified handle.</summary>
		/// <param name="handle">
		///   <see cref="T:System.Reflection.Metadata.MethodDefinitionHandle" /> or <see cref="T:System.Reflection.Metadata.MemberReferenceHandle" />.</param>
		/// <exception cref="T:System.ArgumentException">The handle type is unexpected.</exception>
		// Token: 0x06000912 RID: 2322 RVA: 0x00019DAA File Offset: 0x00017FAA
		public static int MethodDefOrRef(EntityHandle handle)
		{
			return (handle.RowId << 1) | (int)CodedIndex.ToMethodDefOrRefTag(handle.Kind);
		}

		/// <summary>Calculates a ResolutionScope coded index for the specified handle.</summary>
		/// <param name="handle">
		///   <see cref="T:System.Reflection.Metadata.ModuleDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.ModuleReferenceHandle" />, <see cref="T:System.Reflection.Metadata.AssemblyReferenceHandle" /> or <see cref="T:System.Reflection.Metadata.TypeReferenceHandle" />.</param>
		/// <exception cref="T:System.ArgumentException">The handle type is unexpected.</exception>
		// Token: 0x06000913 RID: 2323 RVA: 0x00019DC2 File Offset: 0x00017FC2
		public static int ResolutionScope(EntityHandle handle)
		{
			return (handle.RowId << 2) | (int)CodedIndex.ToResolutionScopeTag(handle.Kind);
		}

		/// <summary>Calculates a TypeDefOrRef coded index for the specified handle.</summary>
		/// <param name="handle">
		///   <see cref="T:System.Reflection.Metadata.TypeDefinitionHandle" /> or <see cref="T:System.Reflection.Metadata.TypeReferenceHandle" />.</param>
		/// <exception cref="T:System.ArgumentException">The handle type is unexpected.</exception>
		// Token: 0x06000914 RID: 2324 RVA: 0x00019DDA File Offset: 0x00017FDA
		public static int TypeDefOrRef(EntityHandle handle)
		{
			return (handle.RowId << 2) | (int)CodedIndex.ToTypeDefOrRefTag(handle.Kind);
		}

		/// <summary>Calculates a TypeDefOrRefOrSpec coded index for the specified handle.</summary>
		/// <param name="handle">
		///   <see cref="T:System.Reflection.Metadata.TypeDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.TypeReferenceHandle" /> or <see cref="T:System.Reflection.Metadata.TypeSpecificationHandle" />.</param>
		/// <exception cref="T:System.ArgumentException">The handle type is unexpected.</exception>
		// Token: 0x06000915 RID: 2325 RVA: 0x00019DF2 File Offset: 0x00017FF2
		public static int TypeDefOrRefOrSpec(EntityHandle handle)
		{
			return (handle.RowId << 2) | (int)CodedIndex.ToTypeDefOrRefOrSpecTag(handle.Kind);
		}

		/// <summary>Calculates a TypeOrMethodDef coded index for the specified handle.</summary>
		/// <param name="handle">
		///   <see cref="T:System.Reflection.Metadata.TypeDefinitionHandle" /> or <see cref="T:System.Reflection.Metadata.MethodDefinitionHandle" />.</param>
		/// <exception cref="T:System.ArgumentException">The handle type is unexpected.</exception>
		// Token: 0x06000916 RID: 2326 RVA: 0x00019E0A File Offset: 0x0001800A
		public static int TypeOrMethodDef(EntityHandle handle)
		{
			return (handle.RowId << 1) | (int)CodedIndex.ToTypeOrMethodDefTag(handle.Kind);
		}

		/// <summary>Calculates a HasCustomDebugInformation coded index for the specified handle.</summary>
		/// <param name="handle">
		///   <see cref="T:System.Reflection.Metadata.MethodDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.FieldDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.TypeReferenceHandle" />, <see cref="T:System.Reflection.Metadata.TypeDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.ParameterHandle" />, <see cref="T:System.Reflection.Metadata.InterfaceImplementationHandle" />, <see cref="T:System.Reflection.Metadata.MemberReferenceHandle" />, <see cref="T:System.Reflection.Metadata.ModuleDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.DeclarativeSecurityAttributeHandle" />, <see cref="T:System.Reflection.Metadata.PropertyDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.EventDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.StandaloneSignatureHandle" />, <see cref="T:System.Reflection.Metadata.ModuleReferenceHandle" />, <see cref="T:System.Reflection.Metadata.TypeSpecificationHandle" />, <see cref="T:System.Reflection.Metadata.AssemblyDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.AssemblyReferenceHandle" />, <see cref="T:System.Reflection.Metadata.AssemblyFileHandle" />, <see cref="T:System.Reflection.Metadata.ExportedTypeHandle" />, <see cref="T:System.Reflection.Metadata.ManifestResourceHandle" />, <see cref="T:System.Reflection.Metadata.GenericParameterHandle" />, <see cref="T:System.Reflection.Metadata.GenericParameterConstraintHandle" />, <see cref="T:System.Reflection.Metadata.MethodSpecificationHandle" />, <see cref="T:System.Reflection.Metadata.DocumentHandle" />, <see cref="T:System.Reflection.Metadata.LocalScopeHandle" />, <see cref="T:System.Reflection.Metadata.LocalVariableHandle" />, <see cref="T:System.Reflection.Metadata.LocalConstantHandle" /> or <see cref="T:System.Reflection.Metadata.ImportScopeHandle" />.</param>
		/// <exception cref="T:System.ArgumentException">The handle type is unexpected.</exception>
		// Token: 0x06000917 RID: 2327 RVA: 0x00019E22 File Offset: 0x00018022
		public static int HasCustomDebugInformation(EntityHandle handle)
		{
			return (handle.RowId << 5) | (int)CodedIndex.ToHasCustomDebugInformationTag(handle.Kind);
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x00019E3C File Offset: 0x0001803C
		private static CodedIndex.HasCustomAttributeTag ToHasCustomAttributeTag(HandleKind kind)
		{
			switch (kind)
			{
			case HandleKind.ModuleDefinition:
				return CodedIndex.HasCustomAttributeTag.Module;
			case HandleKind.TypeReference:
				return CodedIndex.HasCustomAttributeTag.TypeRef;
			case HandleKind.TypeDefinition:
				return CodedIndex.HasCustomAttributeTag.TypeDef;
			case (HandleKind)3:
			case (HandleKind)5:
			case (HandleKind)7:
			case HandleKind.Constant:
			case HandleKind.CustomAttribute:
			case (HandleKind)13:
			case (HandleKind)15:
			case (HandleKind)16:
			case (HandleKind)18:
			case (HandleKind)19:
				break;
			case HandleKind.FieldDefinition:
				return CodedIndex.HasCustomAttributeTag.Field;
			case HandleKind.MethodDefinition:
				return CodedIndex.HasCustomAttributeTag.MethodDef;
			case HandleKind.Parameter:
				return CodedIndex.HasCustomAttributeTag.Param;
			case HandleKind.InterfaceImplementation:
				return CodedIndex.HasCustomAttributeTag.InterfaceImpl;
			case HandleKind.MemberReference:
				return CodedIndex.HasCustomAttributeTag.MemberRef;
			case HandleKind.DeclarativeSecurityAttribute:
				return CodedIndex.HasCustomAttributeTag.DeclSecurity;
			case HandleKind.StandaloneSignature:
				return CodedIndex.HasCustomAttributeTag.StandAloneSig;
			case HandleKind.EventDefinition:
				return CodedIndex.HasCustomAttributeTag.Event;
			default:
				switch (kind)
				{
				case HandleKind.PropertyDefinition:
					return CodedIndex.HasCustomAttributeTag.Property;
				case (HandleKind)24:
				case HandleKind.MethodImplementation:
					break;
				case HandleKind.ModuleReference:
					return CodedIndex.HasCustomAttributeTag.ModuleRef;
				case HandleKind.TypeSpecification:
					return CodedIndex.HasCustomAttributeTag.TypeSpec;
				default:
					switch (kind)
					{
					case HandleKind.AssemblyDefinition:
						return CodedIndex.HasCustomAttributeTag.Assembly;
					case HandleKind.AssemblyReference:
						return CodedIndex.HasCustomAttributeTag.AssemblyRef;
					case HandleKind.AssemblyFile:
						return CodedIndex.HasCustomAttributeTag.File;
					case HandleKind.ExportedType:
						return CodedIndex.HasCustomAttributeTag.ExportedType;
					case HandleKind.ManifestResource:
						return CodedIndex.HasCustomAttributeTag.ManifestResource;
					case HandleKind.GenericParameter:
						return CodedIndex.HasCustomAttributeTag.GenericParam;
					case HandleKind.MethodSpecification:
						return CodedIndex.HasCustomAttributeTag.MethodSpec;
					case HandleKind.GenericParameterConstraint:
						return CodedIndex.HasCustomAttributeTag.GenericParamConstraint;
					}
					break;
				}
				break;
			}
			Throw.InvalidArgument_UnexpectedHandleKind(kind);
			return CodedIndex.HasCustomAttributeTag.MethodDef;
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x00019F40 File Offset: 0x00018140
		private static CodedIndex.HasConstantTag ToHasConstantTag(HandleKind kind)
		{
			if (kind == HandleKind.FieldDefinition)
			{
				return CodedIndex.HasConstantTag.Field;
			}
			if (kind == HandleKind.Parameter)
			{
				return CodedIndex.HasConstantTag.Param;
			}
			if (kind != HandleKind.PropertyDefinition)
			{
				Throw.InvalidArgument_UnexpectedHandleKind(kind);
				return CodedIndex.HasConstantTag.Field;
			}
			return CodedIndex.HasConstantTag.Property;
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x00019F5F File Offset: 0x0001815F
		private static CodedIndex.CustomAttributeTypeTag ToCustomAttributeTypeTag(HandleKind kind)
		{
			if (kind == HandleKind.MethodDefinition)
			{
				return CodedIndex.CustomAttributeTypeTag.MethodDef;
			}
			if (kind != HandleKind.MemberReference)
			{
				Throw.InvalidArgument_UnexpectedHandleKind(kind);
				return (CodedIndex.CustomAttributeTypeTag)0;
			}
			return CodedIndex.CustomAttributeTypeTag.MemberRef;
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x00019F78 File Offset: 0x00018178
		private static CodedIndex.HasDeclSecurityTag ToHasDeclSecurityTag(HandleKind kind)
		{
			if (kind == HandleKind.TypeDefinition)
			{
				return CodedIndex.HasDeclSecurityTag.TypeDef;
			}
			if (kind == HandleKind.MethodDefinition)
			{
				return CodedIndex.HasDeclSecurityTag.MethodDef;
			}
			if (kind != HandleKind.AssemblyDefinition)
			{
				Throw.InvalidArgument_UnexpectedHandleKind(kind);
				return CodedIndex.HasDeclSecurityTag.TypeDef;
			}
			return CodedIndex.HasDeclSecurityTag.Assembly;
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00019F97 File Offset: 0x00018197
		private static CodedIndex.HasFieldMarshalTag ToHasFieldMarshalTag(HandleKind kind)
		{
			if (kind == HandleKind.FieldDefinition)
			{
				return CodedIndex.HasFieldMarshalTag.Field;
			}
			if (kind != HandleKind.Parameter)
			{
				Throw.InvalidArgument_UnexpectedHandleKind(kind);
				return CodedIndex.HasFieldMarshalTag.Field;
			}
			return CodedIndex.HasFieldMarshalTag.Param;
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00019FAF File Offset: 0x000181AF
		private static CodedIndex.HasSemanticsTag ToHasSemanticsTag(HandleKind kind)
		{
			if (kind == HandleKind.EventDefinition)
			{
				return CodedIndex.HasSemanticsTag.Event;
			}
			if (kind != HandleKind.PropertyDefinition)
			{
				Throw.InvalidArgument_UnexpectedHandleKind(kind);
				return CodedIndex.HasSemanticsTag.Event;
			}
			return CodedIndex.HasSemanticsTag.Property;
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00019FC9 File Offset: 0x000181C9
		private static CodedIndex.ImplementationTag ToImplementationTag(HandleKind kind)
		{
			switch (kind)
			{
			case HandleKind.AssemblyReference:
				return CodedIndex.ImplementationTag.AssemblyRef;
			case HandleKind.AssemblyFile:
				return CodedIndex.ImplementationTag.File;
			case HandleKind.ExportedType:
				return CodedIndex.ImplementationTag.ExportedType;
			}
			Throw.InvalidArgument_UnexpectedHandleKind(kind);
			return CodedIndex.ImplementationTag.File;
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x00019FF8 File Offset: 0x000181F8
		private static CodedIndex.MemberForwardedTag ToMemberForwardedTag(HandleKind kind)
		{
			if (kind == HandleKind.FieldDefinition)
			{
				return CodedIndex.MemberForwardedTag.Field;
			}
			if (kind != HandleKind.MethodDefinition)
			{
				Throw.InvalidArgument_UnexpectedHandleKind(kind);
				return CodedIndex.MemberForwardedTag.Field;
			}
			return CodedIndex.MemberForwardedTag.MethodDef;
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0001A010 File Offset: 0x00018210
		private static CodedIndex.MemberRefParentTag ToMemberRefParentTag(HandleKind kind)
		{
			if (kind <= HandleKind.TypeDefinition)
			{
				if (kind == HandleKind.TypeReference)
				{
					return CodedIndex.MemberRefParentTag.TypeRef;
				}
				if (kind == HandleKind.TypeDefinition)
				{
					return CodedIndex.MemberRefParentTag.TypeDef;
				}
			}
			else
			{
				if (kind == HandleKind.MethodDefinition)
				{
					return CodedIndex.MemberRefParentTag.MethodDef;
				}
				if (kind == HandleKind.ModuleReference)
				{
					return CodedIndex.MemberRefParentTag.ModuleRef;
				}
				if (kind == HandleKind.TypeSpecification)
				{
					return CodedIndex.MemberRefParentTag.TypeSpec;
				}
			}
			Throw.InvalidArgument_UnexpectedHandleKind(kind);
			return CodedIndex.MemberRefParentTag.TypeDef;
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0001A042 File Offset: 0x00018242
		private static CodedIndex.MethodDefOrRefTag ToMethodDefOrRefTag(HandleKind kind)
		{
			if (kind == HandleKind.MethodDefinition)
			{
				return CodedIndex.MethodDefOrRefTag.MethodDef;
			}
			if (kind != HandleKind.MemberReference)
			{
				Throw.InvalidArgument_UnexpectedHandleKind(kind);
				return CodedIndex.MethodDefOrRefTag.MethodDef;
			}
			return CodedIndex.MethodDefOrRefTag.MemberRef;
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0001A05B File Offset: 0x0001825B
		private static CodedIndex.ResolutionScopeTag ToResolutionScopeTag(HandleKind kind)
		{
			if (kind <= HandleKind.TypeReference)
			{
				if (kind == HandleKind.ModuleDefinition)
				{
					return CodedIndex.ResolutionScopeTag.Module;
				}
				if (kind == HandleKind.TypeReference)
				{
					return CodedIndex.ResolutionScopeTag.TypeRef;
				}
			}
			else
			{
				if (kind == HandleKind.ModuleReference)
				{
					return CodedIndex.ResolutionScopeTag.ModuleRef;
				}
				if (kind == HandleKind.AssemblyReference)
				{
					return CodedIndex.ResolutionScopeTag.AssemblyRef;
				}
			}
			Throw.InvalidArgument_UnexpectedHandleKind(kind);
			return CodedIndex.ResolutionScopeTag.Module;
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0001A086 File Offset: 0x00018286
		private static CodedIndex.TypeDefOrRefOrSpecTag ToTypeDefOrRefOrSpecTag(HandleKind kind)
		{
			if (kind == HandleKind.TypeReference)
			{
				return CodedIndex.TypeDefOrRefOrSpecTag.TypeRef;
			}
			if (kind == HandleKind.TypeDefinition)
			{
				return CodedIndex.TypeDefOrRefOrSpecTag.TypeDef;
			}
			if (kind != HandleKind.TypeSpecification)
			{
				Throw.InvalidArgument_UnexpectedHandleKind(kind);
				return CodedIndex.TypeDefOrRefOrSpecTag.TypeDef;
			}
			return CodedIndex.TypeDefOrRefOrSpecTag.TypeSpec;
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0001A0A5 File Offset: 0x000182A5
		private static CodedIndex.TypeDefOrRefTag ToTypeDefOrRefTag(HandleKind kind)
		{
			if (kind == HandleKind.TypeReference)
			{
				return CodedIndex.TypeDefOrRefTag.TypeRef;
			}
			if (kind == HandleKind.TypeDefinition)
			{
				return CodedIndex.TypeDefOrRefTag.TypeDef;
			}
			Throw.InvalidArgument_UnexpectedHandleKind(kind);
			return CodedIndex.TypeDefOrRefTag.TypeDef;
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0001A0BB File Offset: 0x000182BB
		private static CodedIndex.TypeOrMethodDefTag ToTypeOrMethodDefTag(HandleKind kind)
		{
			if (kind == HandleKind.TypeDefinition)
			{
				return CodedIndex.TypeOrMethodDefTag.TypeDef;
			}
			if (kind != HandleKind.MethodDefinition)
			{
				Throw.InvalidArgument_UnexpectedHandleKind(kind);
				return CodedIndex.TypeOrMethodDefTag.TypeDef;
			}
			return CodedIndex.TypeOrMethodDefTag.MethodDef;
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0001A0D4 File Offset: 0x000182D4
		private static CodedIndex.HasCustomDebugInformationTag ToHasCustomDebugInformationTag(HandleKind kind)
		{
			switch (kind)
			{
			case HandleKind.ModuleDefinition:
				return CodedIndex.HasCustomDebugInformationTag.Module;
			case HandleKind.TypeReference:
				return CodedIndex.HasCustomDebugInformationTag.TypeRef;
			case HandleKind.TypeDefinition:
				return CodedIndex.HasCustomDebugInformationTag.TypeDef;
			case (HandleKind)3:
			case (HandleKind)5:
			case (HandleKind)7:
			case HandleKind.Constant:
			case HandleKind.CustomAttribute:
			case (HandleKind)13:
			case (HandleKind)15:
			case (HandleKind)16:
			case (HandleKind)18:
			case (HandleKind)19:
				break;
			case HandleKind.FieldDefinition:
				return CodedIndex.HasCustomDebugInformationTag.Field;
			case HandleKind.MethodDefinition:
				return CodedIndex.HasCustomDebugInformationTag.MethodDef;
			case HandleKind.Parameter:
				return CodedIndex.HasCustomDebugInformationTag.Param;
			case HandleKind.InterfaceImplementation:
				return CodedIndex.HasCustomDebugInformationTag.InterfaceImpl;
			case HandleKind.MemberReference:
				return CodedIndex.HasCustomDebugInformationTag.MemberRef;
			case HandleKind.DeclarativeSecurityAttribute:
				return CodedIndex.HasCustomDebugInformationTag.DeclSecurity;
			case HandleKind.StandaloneSignature:
				return CodedIndex.HasCustomDebugInformationTag.StandAloneSig;
			case HandleKind.EventDefinition:
				return CodedIndex.HasCustomDebugInformationTag.Event;
			default:
				switch (kind)
				{
				case HandleKind.PropertyDefinition:
					return CodedIndex.HasCustomDebugInformationTag.Property;
				case HandleKind.ModuleReference:
					return CodedIndex.HasCustomDebugInformationTag.ModuleRef;
				case HandleKind.TypeSpecification:
					return CodedIndex.HasCustomDebugInformationTag.TypeSpec;
				case HandleKind.AssemblyDefinition:
					return CodedIndex.HasCustomDebugInformationTag.Assembly;
				case HandleKind.AssemblyReference:
					return CodedIndex.HasCustomDebugInformationTag.AssemblyRef;
				case HandleKind.AssemblyFile:
					return CodedIndex.HasCustomDebugInformationTag.File;
				case HandleKind.ExportedType:
					return CodedIndex.HasCustomDebugInformationTag.ExportedType;
				case HandleKind.ManifestResource:
					return CodedIndex.HasCustomDebugInformationTag.ManifestResource;
				case HandleKind.GenericParameter:
					return CodedIndex.HasCustomDebugInformationTag.GenericParam;
				case HandleKind.MethodSpecification:
					return CodedIndex.HasCustomDebugInformationTag.MethodSpec;
				case HandleKind.GenericParameterConstraint:
					return CodedIndex.HasCustomDebugInformationTag.GenericParamConstraint;
				case HandleKind.Document:
					return CodedIndex.HasCustomDebugInformationTag.Document;
				case HandleKind.LocalScope:
					return CodedIndex.HasCustomDebugInformationTag.LocalScope;
				case HandleKind.LocalVariable:
					return CodedIndex.HasCustomDebugInformationTag.LocalVariable;
				case HandleKind.LocalConstant:
					return CodedIndex.HasCustomDebugInformationTag.LocalConstant;
				case HandleKind.ImportScope:
					return CodedIndex.HasCustomDebugInformationTag.ImportScope;
				}
				break;
			}
			Throw.InvalidArgument_UnexpectedHandleKind(kind);
			return CodedIndex.HasCustomDebugInformationTag.MethodDef;
		}

		// Token: 0x020001EC RID: 492
		private enum HasCustomAttributeTag
		{
			// Token: 0x04000B39 RID: 2873
			MethodDef,
			// Token: 0x04000B3A RID: 2874
			Field,
			// Token: 0x04000B3B RID: 2875
			TypeRef,
			// Token: 0x04000B3C RID: 2876
			TypeDef,
			// Token: 0x04000B3D RID: 2877
			Param,
			// Token: 0x04000B3E RID: 2878
			InterfaceImpl,
			// Token: 0x04000B3F RID: 2879
			MemberRef,
			// Token: 0x04000B40 RID: 2880
			Module,
			// Token: 0x04000B41 RID: 2881
			DeclSecurity,
			// Token: 0x04000B42 RID: 2882
			Property,
			// Token: 0x04000B43 RID: 2883
			Event,
			// Token: 0x04000B44 RID: 2884
			StandAloneSig,
			// Token: 0x04000B45 RID: 2885
			ModuleRef,
			// Token: 0x04000B46 RID: 2886
			TypeSpec,
			// Token: 0x04000B47 RID: 2887
			Assembly,
			// Token: 0x04000B48 RID: 2888
			AssemblyRef,
			// Token: 0x04000B49 RID: 2889
			File,
			// Token: 0x04000B4A RID: 2890
			ExportedType,
			// Token: 0x04000B4B RID: 2891
			ManifestResource,
			// Token: 0x04000B4C RID: 2892
			GenericParam,
			// Token: 0x04000B4D RID: 2893
			GenericParamConstraint,
			// Token: 0x04000B4E RID: 2894
			MethodSpec,
			// Token: 0x04000B4F RID: 2895
			BitCount = 5
		}

		// Token: 0x020001ED RID: 493
		private enum HasConstantTag
		{
			// Token: 0x04000B51 RID: 2897
			Field,
			// Token: 0x04000B52 RID: 2898
			Param,
			// Token: 0x04000B53 RID: 2899
			Property,
			// Token: 0x04000B54 RID: 2900
			BitCount = 2
		}

		// Token: 0x020001EE RID: 494
		private enum CustomAttributeTypeTag
		{
			// Token: 0x04000B56 RID: 2902
			MethodDef = 2,
			// Token: 0x04000B57 RID: 2903
			MemberRef,
			// Token: 0x04000B58 RID: 2904
			BitCount = 3
		}

		// Token: 0x020001EF RID: 495
		private enum HasDeclSecurityTag
		{
			// Token: 0x04000B5A RID: 2906
			TypeDef,
			// Token: 0x04000B5B RID: 2907
			MethodDef,
			// Token: 0x04000B5C RID: 2908
			Assembly,
			// Token: 0x04000B5D RID: 2909
			BitCount = 2
		}

		// Token: 0x020001F0 RID: 496
		private enum HasFieldMarshalTag
		{
			// Token: 0x04000B5F RID: 2911
			Field,
			// Token: 0x04000B60 RID: 2912
			Param,
			// Token: 0x04000B61 RID: 2913
			BitCount = 1
		}

		// Token: 0x020001F1 RID: 497
		private enum HasSemanticsTag
		{
			// Token: 0x04000B63 RID: 2915
			Event,
			// Token: 0x04000B64 RID: 2916
			Property,
			// Token: 0x04000B65 RID: 2917
			BitCount = 1
		}

		// Token: 0x020001F2 RID: 498
		private enum ImplementationTag
		{
			// Token: 0x04000B67 RID: 2919
			File,
			// Token: 0x04000B68 RID: 2920
			AssemblyRef,
			// Token: 0x04000B69 RID: 2921
			ExportedType,
			// Token: 0x04000B6A RID: 2922
			BitCount = 2
		}

		// Token: 0x020001F3 RID: 499
		private enum MemberForwardedTag
		{
			// Token: 0x04000B6C RID: 2924
			Field,
			// Token: 0x04000B6D RID: 2925
			MethodDef,
			// Token: 0x04000B6E RID: 2926
			BitCount = 1
		}

		// Token: 0x020001F4 RID: 500
		private enum MemberRefParentTag
		{
			// Token: 0x04000B70 RID: 2928
			TypeDef,
			// Token: 0x04000B71 RID: 2929
			TypeRef,
			// Token: 0x04000B72 RID: 2930
			ModuleRef,
			// Token: 0x04000B73 RID: 2931
			MethodDef,
			// Token: 0x04000B74 RID: 2932
			TypeSpec,
			// Token: 0x04000B75 RID: 2933
			BitCount = 3
		}

		// Token: 0x020001F5 RID: 501
		private enum MethodDefOrRefTag
		{
			// Token: 0x04000B77 RID: 2935
			MethodDef,
			// Token: 0x04000B78 RID: 2936
			MemberRef,
			// Token: 0x04000B79 RID: 2937
			BitCount = 1
		}

		// Token: 0x020001F6 RID: 502
		private enum ResolutionScopeTag
		{
			// Token: 0x04000B7B RID: 2939
			Module,
			// Token: 0x04000B7C RID: 2940
			ModuleRef,
			// Token: 0x04000B7D RID: 2941
			AssemblyRef,
			// Token: 0x04000B7E RID: 2942
			TypeRef,
			// Token: 0x04000B7F RID: 2943
			BitCount = 2
		}

		// Token: 0x020001F7 RID: 503
		private enum TypeDefOrRefOrSpecTag
		{
			// Token: 0x04000B81 RID: 2945
			TypeDef,
			// Token: 0x04000B82 RID: 2946
			TypeRef,
			// Token: 0x04000B83 RID: 2947
			TypeSpec,
			// Token: 0x04000B84 RID: 2948
			BitCount = 2
		}

		// Token: 0x020001F8 RID: 504
		private enum TypeDefOrRefTag
		{
			// Token: 0x04000B86 RID: 2950
			TypeDef,
			// Token: 0x04000B87 RID: 2951
			TypeRef,
			// Token: 0x04000B88 RID: 2952
			BitCount
		}

		// Token: 0x020001F9 RID: 505
		private enum TypeOrMethodDefTag
		{
			// Token: 0x04000B8A RID: 2954
			TypeDef,
			// Token: 0x04000B8B RID: 2955
			MethodDef,
			// Token: 0x04000B8C RID: 2956
			BitCount = 1
		}

		// Token: 0x020001FA RID: 506
		private enum HasCustomDebugInformationTag
		{
			// Token: 0x04000B8E RID: 2958
			MethodDef,
			// Token: 0x04000B8F RID: 2959
			Field,
			// Token: 0x04000B90 RID: 2960
			TypeRef,
			// Token: 0x04000B91 RID: 2961
			TypeDef,
			// Token: 0x04000B92 RID: 2962
			Param,
			// Token: 0x04000B93 RID: 2963
			InterfaceImpl,
			// Token: 0x04000B94 RID: 2964
			MemberRef,
			// Token: 0x04000B95 RID: 2965
			Module,
			// Token: 0x04000B96 RID: 2966
			DeclSecurity,
			// Token: 0x04000B97 RID: 2967
			Property,
			// Token: 0x04000B98 RID: 2968
			Event,
			// Token: 0x04000B99 RID: 2969
			StandAloneSig,
			// Token: 0x04000B9A RID: 2970
			ModuleRef,
			// Token: 0x04000B9B RID: 2971
			TypeSpec,
			// Token: 0x04000B9C RID: 2972
			Assembly,
			// Token: 0x04000B9D RID: 2973
			AssemblyRef,
			// Token: 0x04000B9E RID: 2974
			File,
			// Token: 0x04000B9F RID: 2975
			ExportedType,
			// Token: 0x04000BA0 RID: 2976
			ManifestResource,
			// Token: 0x04000BA1 RID: 2977
			GenericParam,
			// Token: 0x04000BA2 RID: 2978
			GenericParamConstraint,
			// Token: 0x04000BA3 RID: 2979
			MethodSpec,
			// Token: 0x04000BA4 RID: 2980
			Document,
			// Token: 0x04000BA5 RID: 2981
			LocalScope,
			// Token: 0x04000BA6 RID: 2982
			LocalVariable,
			// Token: 0x04000BA7 RID: 2983
			LocalConstant,
			// Token: 0x04000BA8 RID: 2984
			ImportScope,
			// Token: 0x04000BA9 RID: 2985
			BitCount = 5
		}
	}
}
