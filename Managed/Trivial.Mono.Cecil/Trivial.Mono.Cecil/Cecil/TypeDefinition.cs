using System;
using Trivial.Mono.Cecil.Metadata;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x020000BC RID: 188
	public sealed class TypeDefinition : TypeReference, IMemberDefinition, ICustomAttributeProvider, IMetadataTokenProvider, ISecurityDeclarationProvider
	{
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x00017ADE File Offset: 0x00015CDE
		// (set) Token: 0x06000796 RID: 1942 RVA: 0x00017AE6 File Offset: 0x00015CE6
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

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x00017B07 File Offset: 0x00015D07
		// (set) Token: 0x06000798 RID: 1944 RVA: 0x00017B0F File Offset: 0x00015D0F
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

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x00017B18 File Offset: 0x00015D18
		// (set) Token: 0x0600079A RID: 1946 RVA: 0x00017B20 File Offset: 0x00015D20
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

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x00017B45 File Offset: 0x00015D45
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

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x00017B78 File Offset: 0x00015D78
		// (set) Token: 0x0600079D RID: 1949 RVA: 0x00017BA1 File Offset: 0x00015DA1
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

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x00017BAA File Offset: 0x00015DAA
		// (set) Token: 0x0600079F RID: 1951 RVA: 0x00017BD3 File Offset: 0x00015DD3
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

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x00017BDC File Offset: 0x00015DDC
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

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x00017C38 File Offset: 0x00015E38
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
				return this.interfaces = new InterfaceImplementationCollection(this);
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x00017CA0 File Offset: 0x00015EA0
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

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x00017CFC File Offset: 0x00015EFC
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
				return this.nested_types = new MemberDefinitionCollection<TypeDefinition>(this);
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x00017D61 File Offset: 0x00015F61
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

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x00017D94 File Offset: 0x00015F94
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
				return this.methods = new MemberDefinitionCollection<MethodDefinition>(this);
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x00017DF9 File Offset: 0x00015FF9
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

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x00017E2C File Offset: 0x0001602C
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
				return this.fields = new MemberDefinitionCollection<FieldDefinition>(this);
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x00017E94 File Offset: 0x00016094
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

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x00017EF0 File Offset: 0x000160F0
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
				return this.events = new MemberDefinitionCollection<EventDefinition>(this);
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x00017F58 File Offset: 0x00016158
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

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x00017FB4 File Offset: 0x000161B4
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
				return this.properties = new MemberDefinitionCollection<PropertyDefinition>(this);
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x00018019 File Offset: 0x00016219
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

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x0001803E File Offset: 0x0001623E
		public Collection<SecurityDeclaration> SecurityDeclarations
		{
			get
			{
				return this.security_declarations ?? this.GetSecurityDeclarations(ref this.security_declarations, this.Module);
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x0001805C File Offset: 0x0001625C
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

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x00018081 File Offset: 0x00016281
		public Collection<CustomAttribute> CustomAttributes
		{
			get
			{
				return this.custom_attributes ?? this.GetCustomAttributes(ref this.custom_attributes, this.Module);
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x0001809F File Offset: 0x0001629F
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

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x000180C4 File Offset: 0x000162C4
		public override Collection<GenericParameter> GenericParameters
		{
			get
			{
				return this.generic_parameters ?? this.GetGenericParameters(ref this.generic_parameters, this.Module);
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x000180E2 File Offset: 0x000162E2
		// (set) Token: 0x060007B3 RID: 1971 RVA: 0x000180F1 File Offset: 0x000162F1
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

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x00018107 File Offset: 0x00016307
		// (set) Token: 0x060007B5 RID: 1973 RVA: 0x00018116 File Offset: 0x00016316
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

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x0001812C File Offset: 0x0001632C
		// (set) Token: 0x060007B7 RID: 1975 RVA: 0x0001813B File Offset: 0x0001633B
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

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x00018151 File Offset: 0x00016351
		// (set) Token: 0x060007B9 RID: 1977 RVA: 0x00018160 File Offset: 0x00016360
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

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x00018176 File Offset: 0x00016376
		// (set) Token: 0x060007BB RID: 1979 RVA: 0x00018185 File Offset: 0x00016385
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

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x0001819B File Offset: 0x0001639B
		// (set) Token: 0x060007BD RID: 1981 RVA: 0x000181AA File Offset: 0x000163AA
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

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x000181C0 File Offset: 0x000163C0
		// (set) Token: 0x060007BF RID: 1983 RVA: 0x000181CF File Offset: 0x000163CF
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

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x000181E5 File Offset: 0x000163E5
		// (set) Token: 0x060007C1 RID: 1985 RVA: 0x000181F4 File Offset: 0x000163F4
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

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x0001820A File Offset: 0x0001640A
		// (set) Token: 0x060007C3 RID: 1987 RVA: 0x0001821A File Offset: 0x0001641A
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

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x00018231 File Offset: 0x00016431
		// (set) Token: 0x060007C5 RID: 1989 RVA: 0x00018241 File Offset: 0x00016441
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

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x00018258 File Offset: 0x00016458
		// (set) Token: 0x060007C7 RID: 1991 RVA: 0x00018269 File Offset: 0x00016469
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

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x00018281 File Offset: 0x00016481
		// (set) Token: 0x060007C9 RID: 1993 RVA: 0x00018291 File Offset: 0x00016491
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

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x000182A8 File Offset: 0x000164A8
		// (set) Token: 0x060007CB RID: 1995 RVA: 0x000182B9 File Offset: 0x000164B9
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

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x000182D1 File Offset: 0x000164D1
		// (set) Token: 0x060007CD RID: 1997 RVA: 0x000182E3 File Offset: 0x000164E3
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

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x000182FC File Offset: 0x000164FC
		// (set) Token: 0x060007CF RID: 1999 RVA: 0x0001830E File Offset: 0x0001650E
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

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x00018327 File Offset: 0x00016527
		// (set) Token: 0x060007D1 RID: 2001 RVA: 0x00018339 File Offset: 0x00016539
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

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x00018352 File Offset: 0x00016552
		// (set) Token: 0x060007D3 RID: 2003 RVA: 0x00018364 File Offset: 0x00016564
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

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x0001837D File Offset: 0x0001657D
		// (set) Token: 0x060007D5 RID: 2005 RVA: 0x0001838F File Offset: 0x0001658F
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

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x000183A8 File Offset: 0x000165A8
		// (set) Token: 0x060007D7 RID: 2007 RVA: 0x000183BA File Offset: 0x000165BA
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

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x000183D3 File Offset: 0x000165D3
		// (set) Token: 0x060007D9 RID: 2009 RVA: 0x000183E6 File Offset: 0x000165E6
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

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x00018400 File Offset: 0x00016600
		// (set) Token: 0x060007DB RID: 2011 RVA: 0x00018417 File Offset: 0x00016617
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

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x00018435 File Offset: 0x00016635
		// (set) Token: 0x060007DD RID: 2013 RVA: 0x0001844C File Offset: 0x0001664C
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

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x0001846A File Offset: 0x0001666A
		// (set) Token: 0x060007DF RID: 2015 RVA: 0x0001847C File Offset: 0x0001667C
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

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x00018495 File Offset: 0x00016695
		// (set) Token: 0x060007E1 RID: 2017 RVA: 0x000184A7 File Offset: 0x000166A7
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

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x000184C0 File Offset: 0x000166C0
		// (set) Token: 0x060007E3 RID: 2019 RVA: 0x000184D2 File Offset: 0x000166D2
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

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x000184EB File Offset: 0x000166EB
		public bool IsEnum
		{
			get
			{
				return this.base_type != null && this.base_type.IsTypeOf("System", "Enum");
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x0001850C File Offset: 0x0001670C
		public override bool IsValueType
		{
			get
			{
				return this.base_type != null && (this.base_type.IsTypeOf("System", "Enum") || (this.base_type.IsTypeOf("System", "ValueType") && !this.IsTypeOf("System", "Enum")));
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x00018568 File Offset: 0x00016768
		public override bool IsPrimitive
		{
			get
			{
				ElementType elementType;
				return MetadataSystem.TryGetPrimitiveElementType(this, out elementType) && elementType.IsPrimitive();
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x00018588 File Offset: 0x00016788
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

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public override bool IsDefinition
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x000185A7 File Offset: 0x000167A7
		// (set) Token: 0x060007EA RID: 2026 RVA: 0x000185B4 File Offset: 0x000167B4
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

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x000185BD File Offset: 0x000167BD
		// (set) Token: 0x060007EC RID: 2028 RVA: 0x000056A4 File Offset: 0x000038A4
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

		// Token: 0x060007ED RID: 2029 RVA: 0x000185CC File Offset: 0x000167CC
		private void ResolveLayout()
		{
			if (this.packing_size == -2 && this.class_size == -2)
			{
				if (!base.HasImage)
				{
					this.packing_size = -1;
					this.class_size = -1;
					return;
				}
				Row<short, int> row = this.Module.Read<TypeDefinition, Row<short, int>>(this, (TypeDefinition type, MetadataReader reader) => reader.ReadTypeLayout(type));
				this.packing_size = row.Col1;
				this.class_size = row.Col2;
			}
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00018648 File Offset: 0x00016848
		public TypeDefinition(string @namespace, string name, TypeAttributes attributes)
			: base(@namespace, name)
		{
			this.attributes = (uint)attributes;
			this.token = new MetadataToken(TokenType.TypeDef);
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00018679 File Offset: 0x00016879
		public TypeDefinition(string @namespace, string name, TypeAttributes attributes, TypeReference baseType)
			: this(@namespace, name, attributes)
		{
			this.BaseType = baseType;
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0001868C File Offset: 0x0001688C
		protected override void ClearFullName()
		{
			base.ClearFullName();
			if (this.HasNestedTypes)
			{
				Collection<TypeDefinition> nestedTypes = this.NestedTypes;
				for (int i = 0; i < nestedTypes.Count; i++)
				{
					nestedTypes[i].ClearFullName();
				}
			}
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00002654 File Offset: 0x00000854
		public override TypeDefinition Resolve()
		{
			return this;
		}

		// Token: 0x040002C4 RID: 708
		private uint attributes;

		// Token: 0x040002C5 RID: 709
		private TypeReference base_type;

		// Token: 0x040002C6 RID: 710
		internal Range fields_range;

		// Token: 0x040002C7 RID: 711
		internal Range methods_range;

		// Token: 0x040002C8 RID: 712
		private short packing_size = -2;

		// Token: 0x040002C9 RID: 713
		private int class_size = -2;

		// Token: 0x040002CA RID: 714
		private InterfaceImplementationCollection interfaces;

		// Token: 0x040002CB RID: 715
		private Collection<TypeDefinition> nested_types;

		// Token: 0x040002CC RID: 716
		private Collection<MethodDefinition> methods;

		// Token: 0x040002CD RID: 717
		private Collection<FieldDefinition> fields;

		// Token: 0x040002CE RID: 718
		private Collection<EventDefinition> events;

		// Token: 0x040002CF RID: 719
		private Collection<PropertyDefinition> properties;

		// Token: 0x040002D0 RID: 720
		private Collection<CustomAttribute> custom_attributes;

		// Token: 0x040002D1 RID: 721
		private Collection<SecurityDeclaration> security_declarations;
	}
}
