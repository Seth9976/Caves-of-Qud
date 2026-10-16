using System;
using System.Threading;
using Mono.Cecil.Metadata;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x0200026E RID: 622
	public sealed class TypeDefinition : TypeReference, IMemberDefinition, ICustomAttributeProvider, IMetadataTokenProvider, ISecurityDeclarationProvider
	{
		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x0002E38A File Offset: 0x0002C58A
		// (set) Token: 0x06000EE2 RID: 3810 RVA: 0x0002E392 File Offset: 0x0002C592
		public TypeAttributes Attributes
		{
			get
			{
				return (TypeAttributes)this.attributes;
			}
			set
			{
				if (base.IsWindowsRuntimeProjection && (uint)((ushort)value) != this.attributes)
				{
					throw new InvalidOperationException();
				}
				this.attributes = (uint)value;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x0002E3B3 File Offset: 0x0002C5B3
		// (set) Token: 0x06000EE4 RID: 3812 RVA: 0x0002E3BB File Offset: 0x0002C5BB
		public TypeReference BaseType
		{
			get
			{
				return this.base_type;
			}
			set
			{
				this.base_type = value;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x0002E3C4 File Offset: 0x0002C5C4
		// (set) Token: 0x06000EE6 RID: 3814 RVA: 0x0002E3CC File Offset: 0x0002C5CC
		public override string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				if (base.IsWindowsRuntimeProjection && value != base.Name)
				{
					throw new InvalidOperationException();
				}
				base.Name = value;
			}
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x0002E3F4 File Offset: 0x0002C5F4
		private void ResolveLayout()
		{
			if (!base.HasImage)
			{
				this.packing_size = -1;
				this.class_size = -1;
				return;
			}
			object syncRoot = this.Module.SyncRoot;
			lock (syncRoot)
			{
				if (this.packing_size == -2 && this.class_size == -2)
				{
					Row<short, int> row = this.Module.Read<TypeDefinition, Row<short, int>>(this, (TypeDefinition type, MetadataReader reader) => reader.ReadTypeLayout(type));
					this.packing_size = row.Col1;
					this.class_size = row.Col2;
				}
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000EE8 RID: 3816 RVA: 0x0002E4A4 File Offset: 0x0002C6A4
		public bool HasLayoutInfo
		{
			get
			{
				if (this.packing_size >= 0 || this.class_size >= 0)
				{
					return true;
				}
				this.ResolveLayout();
				return this.packing_size >= 0 || this.class_size >= 0;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x0002E4D7 File Offset: 0x0002C6D7
		// (set) Token: 0x06000EEA RID: 3818 RVA: 0x0002E500 File Offset: 0x0002C700
		public short PackingSize
		{
			get
			{
				if (this.packing_size >= 0)
				{
					return this.packing_size;
				}
				this.ResolveLayout();
				if (this.packing_size < 0)
				{
					return -1;
				}
				return this.packing_size;
			}
			set
			{
				this.packing_size = value;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000EEB RID: 3819 RVA: 0x0002E509 File Offset: 0x0002C709
		// (set) Token: 0x06000EEC RID: 3820 RVA: 0x0002E532 File Offset: 0x0002C732
		public int ClassSize
		{
			get
			{
				if (this.class_size >= 0)
				{
					return this.class_size;
				}
				this.ResolveLayout();
				if (this.class_size < 0)
				{
					return -1;
				}
				return this.class_size;
			}
			set
			{
				this.class_size = value;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000EED RID: 3821 RVA: 0x0002E53C File Offset: 0x0002C73C
		public bool HasInterfaces
		{
			get
			{
				if (this.interfaces != null)
				{
					return this.interfaces.Count > 0;
				}
				if (base.HasImage)
				{
					return this.Module.Read<TypeDefinition, bool>(this, (TypeDefinition type, MetadataReader reader) => reader.HasInterfaces(type));
				}
				return false;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x0002E598 File Offset: 0x0002C798
		public Collection<InterfaceImplementation> Interfaces
		{
			get
			{
				if (this.interfaces != null)
				{
					return this.interfaces;
				}
				if (base.HasImage)
				{
					return this.Module.Read<TypeDefinition, InterfaceImplementationCollection>(ref this.interfaces, this, (TypeDefinition type, MetadataReader reader) => reader.ReadInterfaces(type));
				}
				Interlocked.CompareExchange<InterfaceImplementationCollection>(ref this.interfaces, new InterfaceImplementationCollection(this), null);
				return this.interfaces;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000EEF RID: 3823 RVA: 0x0002E608 File Offset: 0x0002C808
		public bool HasNestedTypes
		{
			get
			{
				if (this.nested_types != null)
				{
					return this.nested_types.Count > 0;
				}
				if (base.HasImage)
				{
					return this.Module.Read<TypeDefinition, bool>(this, (TypeDefinition type, MetadataReader reader) => reader.HasNestedTypes(type));
				}
				return false;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x0002E664 File Offset: 0x0002C864
		public Collection<TypeDefinition> NestedTypes
		{
			get
			{
				if (this.nested_types != null)
				{
					return this.nested_types;
				}
				if (base.HasImage)
				{
					return this.Module.Read<TypeDefinition, Collection<TypeDefinition>>(ref this.nested_types, this, (TypeDefinition type, MetadataReader reader) => reader.ReadNestedTypes(type));
				}
				Interlocked.CompareExchange<Collection<TypeDefinition>>(ref this.nested_types, new MemberDefinitionCollection<TypeDefinition>(this), null);
				return this.nested_types;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x0002E6D3 File Offset: 0x0002C8D3
		public bool HasMethods
		{
			get
			{
				if (this.methods != null)
				{
					return this.methods.Count > 0;
				}
				return base.HasImage && this.methods_range.Length > 0U;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000EF2 RID: 3826 RVA: 0x0002E704 File Offset: 0x0002C904
		public Collection<MethodDefinition> Methods
		{
			get
			{
				if (this.methods != null)
				{
					return this.methods;
				}
				if (base.HasImage)
				{
					return this.Module.Read<TypeDefinition, Collection<MethodDefinition>>(ref this.methods, this, (TypeDefinition type, MetadataReader reader) => reader.ReadMethods(type));
				}
				Interlocked.CompareExchange<Collection<MethodDefinition>>(ref this.methods, new MemberDefinitionCollection<MethodDefinition>(this), null);
				return this.methods;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x0002E773 File Offset: 0x0002C973
		public bool HasFields
		{
			get
			{
				if (this.fields != null)
				{
					return this.fields.Count > 0;
				}
				return base.HasImage && this.fields_range.Length > 0U;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x0002E7A4 File Offset: 0x0002C9A4
		public Collection<FieldDefinition> Fields
		{
			get
			{
				if (this.fields != null)
				{
					return this.fields;
				}
				if (base.HasImage)
				{
					return this.Module.Read<TypeDefinition, Collection<FieldDefinition>>(ref this.fields, this, (TypeDefinition type, MetadataReader reader) => reader.ReadFields(type));
				}
				Interlocked.CompareExchange<Collection<FieldDefinition>>(ref this.fields, new MemberDefinitionCollection<FieldDefinition>(this), null);
				return this.fields;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x0002E814 File Offset: 0x0002CA14
		public bool HasEvents
		{
			get
			{
				if (this.events != null)
				{
					return this.events.Count > 0;
				}
				if (base.HasImage)
				{
					return this.Module.Read<TypeDefinition, bool>(this, (TypeDefinition type, MetadataReader reader) => reader.HasEvents(type));
				}
				return false;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x0002E870 File Offset: 0x0002CA70
		public Collection<EventDefinition> Events
		{
			get
			{
				if (this.events != null)
				{
					return this.events;
				}
				if (base.HasImage)
				{
					return this.Module.Read<TypeDefinition, Collection<EventDefinition>>(ref this.events, this, (TypeDefinition type, MetadataReader reader) => reader.ReadEvents(type));
				}
				Interlocked.CompareExchange<Collection<EventDefinition>>(ref this.events, new MemberDefinitionCollection<EventDefinition>(this), null);
				return this.events;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x0002E8E0 File Offset: 0x0002CAE0
		public bool HasProperties
		{
			get
			{
				if (this.properties != null)
				{
					return this.properties.Count > 0;
				}
				if (base.HasImage)
				{
					return this.Module.Read<TypeDefinition, bool>(this, (TypeDefinition type, MetadataReader reader) => reader.HasProperties(type));
				}
				return false;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x0002E93C File Offset: 0x0002CB3C
		public Collection<PropertyDefinition> Properties
		{
			get
			{
				if (this.properties != null)
				{
					return this.properties;
				}
				if (base.HasImage)
				{
					return this.Module.Read<TypeDefinition, Collection<PropertyDefinition>>(ref this.properties, this, (TypeDefinition type, MetadataReader reader) => reader.ReadProperties(type));
				}
				Interlocked.CompareExchange<Collection<PropertyDefinition>>(ref this.properties, new MemberDefinitionCollection<PropertyDefinition>(this), null);
				return this.properties;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000EF9 RID: 3833 RVA: 0x0002E9AB File Offset: 0x0002CBAB
		public bool HasSecurityDeclarations
		{
			get
			{
				if (this.security_declarations != null)
				{
					return this.security_declarations.Count > 0;
				}
				return this.GetHasSecurityDeclarations(this.Module);
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000EFA RID: 3834 RVA: 0x0002E9D0 File Offset: 0x0002CBD0
		public Collection<SecurityDeclaration> SecurityDeclarations
		{
			get
			{
				return this.security_declarations ?? this.GetSecurityDeclarations(ref this.security_declarations, this.Module);
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000EFB RID: 3835 RVA: 0x0002E9EE File Offset: 0x0002CBEE
		public bool HasCustomAttributes
		{
			get
			{
				if (this.custom_attributes != null)
				{
					return this.custom_attributes.Count > 0;
				}
				return this.GetHasCustomAttributes(this.Module);
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x0002EA13 File Offset: 0x0002CC13
		public Collection<CustomAttribute> CustomAttributes
		{
			get
			{
				return this.custom_attributes ?? this.GetCustomAttributes(ref this.custom_attributes, this.Module);
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x0002EA31 File Offset: 0x0002CC31
		public override bool HasGenericParameters
		{
			get
			{
				if (this.generic_parameters != null)
				{
					return this.generic_parameters.Count > 0;
				}
				return this.GetHasGenericParameters(this.Module);
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x0002EA56 File Offset: 0x0002CC56
		public override Collection<GenericParameter> GenericParameters
		{
			get
			{
				return this.generic_parameters ?? this.GetGenericParameters(ref this.generic_parameters, this.Module);
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000EFF RID: 3839 RVA: 0x0002EA74 File Offset: 0x0002CC74
		// (set) Token: 0x06000F00 RID: 3840 RVA: 0x0002EA83 File Offset: 0x0002CC83
		public bool IsNotPublic
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7U, 0U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7U, 0U, value);
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x0002EA99 File Offset: 0x0002CC99
		// (set) Token: 0x06000F02 RID: 3842 RVA: 0x0002EAA8 File Offset: 0x0002CCA8
		public bool IsPublic
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7U, 1U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7U, 1U, value);
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000F03 RID: 3843 RVA: 0x0002EABE File Offset: 0x0002CCBE
		// (set) Token: 0x06000F04 RID: 3844 RVA: 0x0002EACD File Offset: 0x0002CCCD
		public bool IsNestedPublic
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7U, 2U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7U, 2U, value);
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000F05 RID: 3845 RVA: 0x0002EAE3 File Offset: 0x0002CCE3
		// (set) Token: 0x06000F06 RID: 3846 RVA: 0x0002EAF2 File Offset: 0x0002CCF2
		public bool IsNestedPrivate
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7U, 3U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7U, 3U, value);
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x0002EB08 File Offset: 0x0002CD08
		// (set) Token: 0x06000F08 RID: 3848 RVA: 0x0002EB17 File Offset: 0x0002CD17
		public bool IsNestedFamily
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7U, 4U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7U, 4U, value);
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000F09 RID: 3849 RVA: 0x0002EB2D File Offset: 0x0002CD2D
		// (set) Token: 0x06000F0A RID: 3850 RVA: 0x0002EB3C File Offset: 0x0002CD3C
		public bool IsNestedAssembly
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7U, 5U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7U, 5U, value);
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000F0B RID: 3851 RVA: 0x0002EB52 File Offset: 0x0002CD52
		// (set) Token: 0x06000F0C RID: 3852 RVA: 0x0002EB61 File Offset: 0x0002CD61
		public bool IsNestedFamilyAndAssembly
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7U, 6U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7U, 6U, value);
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000F0D RID: 3853 RVA: 0x0002EB77 File Offset: 0x0002CD77
		// (set) Token: 0x06000F0E RID: 3854 RVA: 0x0002EB86 File Offset: 0x0002CD86
		public bool IsNestedFamilyOrAssembly
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7U, 7U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7U, 7U, value);
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000F0F RID: 3855 RVA: 0x0002EB9C File Offset: 0x0002CD9C
		// (set) Token: 0x06000F10 RID: 3856 RVA: 0x0002EBAC File Offset: 0x0002CDAC
		public bool IsAutoLayout
		{
			get
			{
				return this.attributes.GetMaskedAttributes(24U, 0U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(24U, 0U, value);
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000F11 RID: 3857 RVA: 0x0002EBC3 File Offset: 0x0002CDC3
		// (set) Token: 0x06000F12 RID: 3858 RVA: 0x0002EBD3 File Offset: 0x0002CDD3
		public bool IsSequentialLayout
		{
			get
			{
				return this.attributes.GetMaskedAttributes(24U, 8U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(24U, 8U, value);
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000F13 RID: 3859 RVA: 0x0002EBEA File Offset: 0x0002CDEA
		// (set) Token: 0x06000F14 RID: 3860 RVA: 0x0002EBFB File Offset: 0x0002CDFB
		public bool IsExplicitLayout
		{
			get
			{
				return this.attributes.GetMaskedAttributes(24U, 16U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(24U, 16U, value);
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000F15 RID: 3861 RVA: 0x0002EC13 File Offset: 0x0002CE13
		// (set) Token: 0x06000F16 RID: 3862 RVA: 0x0002EC23 File Offset: 0x0002CE23
		public bool IsClass
		{
			get
			{
				return this.attributes.GetMaskedAttributes(32U, 0U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(32U, 0U, value);
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000F17 RID: 3863 RVA: 0x0002EC3A File Offset: 0x0002CE3A
		// (set) Token: 0x06000F18 RID: 3864 RVA: 0x0002EC4B File Offset: 0x0002CE4B
		public bool IsInterface
		{
			get
			{
				return this.attributes.GetMaskedAttributes(32U, 32U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(32U, 32U, value);
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000F19 RID: 3865 RVA: 0x0002EC63 File Offset: 0x0002CE63
		// (set) Token: 0x06000F1A RID: 3866 RVA: 0x0002EC75 File Offset: 0x0002CE75
		public bool IsAbstract
		{
			get
			{
				return this.attributes.GetAttributes(128U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(128U, value);
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000F1B RID: 3867 RVA: 0x0002EC8E File Offset: 0x0002CE8E
		// (set) Token: 0x06000F1C RID: 3868 RVA: 0x0002ECA0 File Offset: 0x0002CEA0
		public bool IsSealed
		{
			get
			{
				return this.attributes.GetAttributes(256U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(256U, value);
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000F1D RID: 3869 RVA: 0x0002ECB9 File Offset: 0x0002CEB9
		// (set) Token: 0x06000F1E RID: 3870 RVA: 0x0002ECCB File Offset: 0x0002CECB
		public bool IsSpecialName
		{
			get
			{
				return this.attributes.GetAttributes(1024U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(1024U, value);
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000F1F RID: 3871 RVA: 0x0002ECE4 File Offset: 0x0002CEE4
		// (set) Token: 0x06000F20 RID: 3872 RVA: 0x0002ECF6 File Offset: 0x0002CEF6
		public bool IsImport
		{
			get
			{
				return this.attributes.GetAttributes(4096U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(4096U, value);
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x0002ED0F File Offset: 0x0002CF0F
		// (set) Token: 0x06000F22 RID: 3874 RVA: 0x0002ED21 File Offset: 0x0002CF21
		public bool IsSerializable
		{
			get
			{
				return this.attributes.GetAttributes(8192U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(8192U, value);
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000F23 RID: 3875 RVA: 0x0002ED3A File Offset: 0x0002CF3A
		// (set) Token: 0x06000F24 RID: 3876 RVA: 0x0002ED4C File Offset: 0x0002CF4C
		public bool IsWindowsRuntime
		{
			get
			{
				return this.attributes.GetAttributes(16384U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(16384U, value);
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000F25 RID: 3877 RVA: 0x0002ED65 File Offset: 0x0002CF65
		// (set) Token: 0x06000F26 RID: 3878 RVA: 0x0002ED78 File Offset: 0x0002CF78
		public bool IsAnsiClass
		{
			get
			{
				return this.attributes.GetMaskedAttributes(196608U, 0U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(196608U, 0U, value);
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000F27 RID: 3879 RVA: 0x0002ED92 File Offset: 0x0002CF92
		// (set) Token: 0x06000F28 RID: 3880 RVA: 0x0002EDA9 File Offset: 0x0002CFA9
		public bool IsUnicodeClass
		{
			get
			{
				return this.attributes.GetMaskedAttributes(196608U, 65536U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(196608U, 65536U, value);
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000F29 RID: 3881 RVA: 0x0002EDC7 File Offset: 0x0002CFC7
		// (set) Token: 0x06000F2A RID: 3882 RVA: 0x0002EDDE File Offset: 0x0002CFDE
		public bool IsAutoClass
		{
			get
			{
				return this.attributes.GetMaskedAttributes(196608U, 131072U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(196608U, 131072U, value);
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000F2B RID: 3883 RVA: 0x0002EDFC File Offset: 0x0002CFFC
		// (set) Token: 0x06000F2C RID: 3884 RVA: 0x0002EE0E File Offset: 0x0002D00E
		public bool IsBeforeFieldInit
		{
			get
			{
				return this.attributes.GetAttributes(1048576U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(1048576U, value);
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000F2D RID: 3885 RVA: 0x0002EE27 File Offset: 0x0002D027
		// (set) Token: 0x06000F2E RID: 3886 RVA: 0x0002EE39 File Offset: 0x0002D039
		public bool IsRuntimeSpecialName
		{
			get
			{
				return this.attributes.GetAttributes(2048U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(2048U, value);
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000F2F RID: 3887 RVA: 0x0002EE52 File Offset: 0x0002D052
		// (set) Token: 0x06000F30 RID: 3888 RVA: 0x0002EE64 File Offset: 0x0002D064
		public bool HasSecurity
		{
			get
			{
				return this.attributes.GetAttributes(262144U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(262144U, value);
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000F31 RID: 3889 RVA: 0x0002EE7D File Offset: 0x0002D07D
		public bool IsEnum
		{
			get
			{
				return this.base_type != null && this.base_type.IsTypeOf("System", "Enum");
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000F32 RID: 3890 RVA: 0x0002EEA0 File Offset: 0x0002D0A0
		// (set) Token: 0x06000F33 RID: 3891 RVA: 0x00003B32 File Offset: 0x00001D32
		public override bool IsValueType
		{
			get
			{
				return this.base_type != null && (this.base_type.IsTypeOf("System", "Enum") || (this.base_type.IsTypeOf("System", "ValueType") && !this.IsTypeOf("System", "Enum")));
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000F34 RID: 3892 RVA: 0x0002EEFC File Offset: 0x0002D0FC
		public override bool IsPrimitive
		{
			get
			{
				ElementType elementType;
				return MetadataSystem.TryGetPrimitiveElementType(this, out elementType) && elementType.IsPrimitive();
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000F35 RID: 3893 RVA: 0x0002EF1C File Offset: 0x0002D11C
		public override MetadataType MetadataType
		{
			get
			{
				ElementType elementType;
				if (MetadataSystem.TryGetPrimitiveElementType(this, out elementType))
				{
					return (MetadataType)elementType;
				}
				return base.MetadataType;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000F36 RID: 3894 RVA: 0x00018941 File Offset: 0x00016B41
		public override bool IsDefinition
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000F37 RID: 3895 RVA: 0x0002EF3B File Offset: 0x0002D13B
		// (set) Token: 0x06000F38 RID: 3896 RVA: 0x0002EF48 File Offset: 0x0002D148
		public new TypeDefinition DeclaringType
		{
			get
			{
				return (TypeDefinition)base.DeclaringType;
			}
			set
			{
				base.DeclaringType = value;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000F39 RID: 3897 RVA: 0x0002EF51 File Offset: 0x0002D151
		// (set) Token: 0x06000F3A RID: 3898 RVA: 0x0002736E File Offset: 0x0002556E
		internal new TypeDefinitionProjection WindowsRuntimeProjection
		{
			get
			{
				return (TypeDefinitionProjection)this.projection;
			}
			set
			{
				this.projection = value;
			}
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x0002EF5E File Offset: 0x0002D15E
		public TypeDefinition(string @namespace, string name, TypeAttributes attributes)
			: base(@namespace, name)
		{
			this.attributes = (uint)attributes;
			this.token = new MetadataToken(TokenType.TypeDef);
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x0002EF8F File Offset: 0x0002D18F
		public TypeDefinition(string @namespace, string name, TypeAttributes attributes, TypeReference baseType)
			: this(@namespace, name, attributes)
		{
			this.BaseType = baseType;
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x0002EFA4 File Offset: 0x0002D1A4
		protected override void ClearFullName()
		{
			base.ClearFullName();
			if (!this.HasNestedTypes)
			{
				return;
			}
			Collection<TypeDefinition> nestedTypes = this.NestedTypes;
			for (int i = 0; i < nestedTypes.Count; i++)
			{
				nestedTypes[i].ClearFullName();
			}
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x00018380 File Offset: 0x00016580
		public override TypeDefinition Resolve()
		{
			return this;
		}

		// Token: 0x0400052B RID: 1323
		private uint attributes;

		// Token: 0x0400052C RID: 1324
		private TypeReference base_type;

		// Token: 0x0400052D RID: 1325
		internal Range fields_range;

		// Token: 0x0400052E RID: 1326
		internal Range methods_range;

		// Token: 0x0400052F RID: 1327
		private short packing_size = -2;

		// Token: 0x04000530 RID: 1328
		private int class_size = -2;

		// Token: 0x04000531 RID: 1329
		private InterfaceImplementationCollection interfaces;

		// Token: 0x04000532 RID: 1330
		private Collection<TypeDefinition> nested_types;

		// Token: 0x04000533 RID: 1331
		private Collection<MethodDefinition> methods;

		// Token: 0x04000534 RID: 1332
		private Collection<FieldDefinition> fields;

		// Token: 0x04000535 RID: 1333
		private Collection<EventDefinition> events;

		// Token: 0x04000536 RID: 1334
		private Collection<PropertyDefinition> properties;

		// Token: 0x04000537 RID: 1335
		private Collection<CustomAttribute> custom_attributes;

		// Token: 0x04000538 RID: 1336
		private Collection<SecurityDeclaration> security_declarations;
	}
}
