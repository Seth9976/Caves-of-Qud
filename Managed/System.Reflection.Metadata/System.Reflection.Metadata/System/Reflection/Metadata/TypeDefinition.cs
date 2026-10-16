using System;
using System.Collections.Immutable;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x020000D9 RID: 217
	public readonly struct TypeDefinition
	{
		// Token: 0x060007D2 RID: 2002 RVA: 0x00012B97 File Offset: 0x00010D97
		[NullableContext(1)]
		internal TypeDefinition(MetadataReader reader, uint treatmentAndRowId)
		{
			this._reader = reader;
			this._treatmentAndRowId = treatmentAndRowId;
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x00012BA7 File Offset: 0x00010DA7
		private int RowId
		{
			get
			{
				return (int)(this._treatmentAndRowId & 16777215U);
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x00012BB5 File Offset: 0x00010DB5
		private TypeDefTreatment Treatment
		{
			get
			{
				return (TypeDefTreatment)(this._treatmentAndRowId >> 24);
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x00012BC1 File Offset: 0x00010DC1
		private TypeDefinitionHandle Handle
		{
			get
			{
				return TypeDefinitionHandle.FromRowId(this.RowId);
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x00012BCE File Offset: 0x00010DCE
		public TypeAttributes Attributes
		{
			get
			{
				if (this.Treatment == TypeDefTreatment.None)
				{
					return this._reader.TypeDefTable.GetFlags(this.Handle);
				}
				return this.GetProjectedFlags();
			}
		}

		/// <summary>Gets a value that indicates whether this is a nested type.</summary>
		/// <returns>
		///   <see langword="true" /> if it is a nested type, <see langword="false" /> otherwise.</returns>
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x00012BF5 File Offset: 0x00010DF5
		public bool IsNested
		{
			get
			{
				return this.Attributes.IsNested();
			}
		}

		/// <summary>Gets the name of the type.</summary>
		/// <returns>The name of the type.</returns>
		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x00012C02 File Offset: 0x00010E02
		public StringHandle Name
		{
			get
			{
				if (this.Treatment == TypeDefTreatment.None)
				{
					return this._reader.TypeDefTable.GetName(this.Handle);
				}
				return this.GetProjectedName();
			}
		}

		/// <summary>Gets the full name of the namespace where the type is defined.</summary>
		/// <returns>The full name of the namespace where the type is defined, or a handle whose <see cref="P:System.Reflection.Metadata.StringHandle.IsNil" /> property is <see langword="true" /> if the type is nested or defined in a root namespace.</returns>
		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x00012C29 File Offset: 0x00010E29
		public StringHandle Namespace
		{
			get
			{
				if (this.Treatment == TypeDefTreatment.None)
				{
					return this._reader.TypeDefTable.GetNamespace(this.Handle);
				}
				return this.GetProjectedNamespaceString();
			}
		}

		/// <summary>Gets the definition handle of the namespace where the type is defined.</summary>
		/// <returns>The definition handle of the namespace where the type is defined, or a handle whose <see cref="P:System.Reflection.Metadata.NamespaceDefinitionHandle.IsNil" /> property is <see langword="true" />  if the type is nested or defined in a root namespace.</returns>
		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x00012C50 File Offset: 0x00010E50
		public NamespaceDefinitionHandle NamespaceDefinition
		{
			get
			{
				if (this.Treatment == TypeDefTreatment.None)
				{
					return this._reader.TypeDefTable.GetNamespaceDefinition(this.Handle);
				}
				return this.GetProjectedNamespace();
			}
		}

		/// <summary>Gets the base type of the type definition: either <see cref="T:System.Reflection.Metadata.TypeSpecificationHandle" />, <see cref="T:System.Reflection.Metadata.TypeReferenceHandle" /> or <see cref="T:System.Reflection.Metadata.TypeDefinitionHandle" />.</summary>
		/// <returns>The base type of the type definition.</returns>
		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x00012C77 File Offset: 0x00010E77
		public EntityHandle BaseType
		{
			get
			{
				if (this.Treatment == TypeDefTreatment.None)
				{
					return this._reader.TypeDefTable.GetExtends(this.Handle);
				}
				return this.GetProjectedBaseType();
			}
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00012CA0 File Offset: 0x00010EA0
		public TypeLayout GetLayout()
		{
			int num = this._reader.ClassLayoutTable.FindRow(this.Handle);
			if (num == 0)
			{
				return default(TypeLayout);
			}
			uint classSize = this._reader.ClassLayoutTable.GetClassSize(num);
			if ((long)classSize != (long)((ulong)classSize))
			{
				throw new BadImageFormatException(SR.InvalidTypeSize);
			}
			int packingSize = (int)this._reader.ClassLayoutTable.GetPackingSize(num);
			return new TypeLayout((int)classSize, packingSize);
		}

		/// <summary>Returns the enclosing type of a specified nested type.</summary>
		/// <returns>The enclosing type of the specified nested type, or a handle a handle whose <see cref="P:System.Reflection.Metadata.TypeDefinitionHandle.IsNil" /> property is <see langword="true" /> if the type is not nested.</returns>
		// Token: 0x060007DD RID: 2013 RVA: 0x00012D0D File Offset: 0x00010F0D
		public TypeDefinitionHandle GetDeclaringType()
		{
			return this._reader.NestedClassTable.FindEnclosingType(this.Handle);
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00012D25 File Offset: 0x00010F25
		public GenericParameterHandleCollection GetGenericParameters()
		{
			return this._reader.GenericParamTable.FindGenericParametersForType(this.Handle);
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00012D3D File Offset: 0x00010F3D
		public MethodDefinitionHandleCollection GetMethods()
		{
			return new MethodDefinitionHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00012D50 File Offset: 0x00010F50
		public FieldDefinitionHandleCollection GetFields()
		{
			return new FieldDefinitionHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00012D63 File Offset: 0x00010F63
		public PropertyDefinitionHandleCollection GetProperties()
		{
			return new PropertyDefinitionHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00012D76 File Offset: 0x00010F76
		public EventDefinitionHandleCollection GetEvents()
		{
			return new EventDefinitionHandleCollection(this._reader, this.Handle);
		}

		/// <summary>Returns an array of types nested in the specified type.</summary>
		/// <returns>An immutable array of type definition handles that represent types nested in the specified type.</returns>
		// Token: 0x060007E3 RID: 2019 RVA: 0x00012D89 File Offset: 0x00010F89
		public ImmutableArray<TypeDefinitionHandle> GetNestedTypes()
		{
			return this._reader.GetNestedTypes(this.Handle);
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00012D9C File Offset: 0x00010F9C
		public MethodImplementationHandleCollection GetMethodImplementations()
		{
			return new MethodImplementationHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00012DAF File Offset: 0x00010FAF
		public InterfaceImplementationHandleCollection GetInterfaceImplementations()
		{
			return new InterfaceImplementationHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00012DC2 File Offset: 0x00010FC2
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00012DDA File Offset: 0x00010FDA
		public DeclarativeSecurityAttributeHandleCollection GetDeclarativeSecurityAttributes()
		{
			return new DeclarativeSecurityAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00012DF4 File Offset: 0x00010FF4
		private TypeAttributes GetProjectedFlags()
		{
			TypeAttributes typeAttributes = this._reader.TypeDefTable.GetFlags(this.Handle);
			TypeDefTreatment treatment = this.Treatment;
			switch (treatment & TypeDefTreatment.KindMask)
			{
			case TypeDefTreatment.NormalNonAttribute:
				typeAttributes |= 20480;
				break;
			case TypeDefTreatment.NormalAttribute:
				typeAttributes |= 16640;
				break;
			case TypeDefTreatment.UnmangleWinRTName:
				typeAttributes = (typeAttributes & -1025) | 1;
				break;
			case TypeDefTreatment.PrefixWinRTName:
				typeAttributes = (typeAttributes & -2) | 4096;
				break;
			case TypeDefTreatment.RedirectedToClrType:
				typeAttributes = (typeAttributes & -2) | 4096;
				break;
			case TypeDefTreatment.RedirectedToClrAttribute:
				typeAttributes &= -2;
				break;
			}
			if ((treatment & TypeDefTreatment.MarkAbstractFlag) != TypeDefTreatment.None)
			{
				typeAttributes |= 128;
			}
			if ((treatment & TypeDefTreatment.MarkInternalFlag) != TypeDefTreatment.None)
			{
				typeAttributes &= -2;
			}
			return typeAttributes;
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00012EA0 File Offset: 0x000110A0
		private StringHandle GetProjectedName()
		{
			StringHandle name = this._reader.TypeDefTable.GetName(this.Handle);
			TypeDefTreatment typeDefTreatment = this.Treatment & TypeDefTreatment.KindMask;
			StringHandle stringHandle;
			if (typeDefTreatment != TypeDefTreatment.UnmangleWinRTName)
			{
				if (typeDefTreatment != TypeDefTreatment.PrefixWinRTName)
				{
					stringHandle = name;
				}
				else
				{
					stringHandle = name.WithWinRTPrefix();
				}
			}
			else
			{
				stringHandle = name.SuffixRaw("<CLR>".Length);
			}
			return stringHandle;
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00012EF9 File Offset: 0x000110F9
		private NamespaceDefinitionHandle GetProjectedNamespace()
		{
			return this._reader.TypeDefTable.GetNamespaceDefinition(this.Handle);
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00012F11 File Offset: 0x00011111
		private StringHandle GetProjectedNamespaceString()
		{
			return this._reader.TypeDefTable.GetNamespace(this.Handle);
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00012F29 File Offset: 0x00011129
		private EntityHandle GetProjectedBaseType()
		{
			return this._reader.TypeDefTable.GetExtends(this.Handle);
		}

		// Token: 0x040004E1 RID: 1249
		private readonly MetadataReader _reader;

		// Token: 0x040004E2 RID: 1250
		private readonly uint _treatmentAndRowId;
	}
}
