using System;
using Trivial.Mono.Cecil.Metadata;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000044 RID: 68
	public sealed class GenericParameter : TypeReference, ICustomAttributeProvider, IMetadataTokenProvider
	{
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000222 RID: 546 RVA: 0x000061A3 File Offset: 0x000043A3
		// (set) Token: 0x06000223 RID: 547 RVA: 0x000061AB File Offset: 0x000043AB
		public GenericParameterAttributes Attributes
		{
			get
			{
				return (GenericParameterAttributes)this.attributes;
			}
			set
			{
				this.attributes = (ushort)value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000224 RID: 548 RVA: 0x000061B4 File Offset: 0x000043B4
		public int Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000225 RID: 549 RVA: 0x000061BC File Offset: 0x000043BC
		public GenericParameterType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000226 RID: 550 RVA: 0x000061C4 File Offset: 0x000043C4
		public IGenericParameterProvider Owner
		{
			get
			{
				return this.owner;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000227 RID: 551 RVA: 0x000061CC File Offset: 0x000043CC
		public bool HasConstraints
		{
			get
			{
				if (this.constraints != null)
				{
					return this.constraints.Count > 0;
				}
				if (base.HasImage)
				{
					return this.Module.Read<GenericParameter, bool>(this, (GenericParameter generic_parameter, MetadataReader reader) => reader.HasGenericConstraints(generic_parameter));
				}
				return false;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00006228 File Offset: 0x00004428
		public Collection<TypeReference> Constraints
		{
			get
			{
				if (this.constraints != null)
				{
					return this.constraints;
				}
				if (base.HasImage)
				{
					return this.Module.Read<GenericParameter, Collection<TypeReference>>(ref this.constraints, this, (GenericParameter generic_parameter, MetadataReader reader) => reader.ReadGenericConstraints(generic_parameter));
				}
				return this.constraints = new Collection<TypeReference>();
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000628C File Offset: 0x0000448C
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

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600022A RID: 554 RVA: 0x000062B1 File Offset: 0x000044B1
		public Collection<CustomAttribute> CustomAttributes
		{
			get
			{
				return this.custom_attributes ?? this.GetCustomAttributes(ref this.custom_attributes, this.Module);
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600022B RID: 555 RVA: 0x000062CF File Offset: 0x000044CF
		// (set) Token: 0x0600022C RID: 556 RVA: 0x00002B65 File Offset: 0x00000D65
		public override IMetadataScope Scope
		{
			get
			{
				if (this.owner == null)
				{
					return null;
				}
				if (this.owner.GenericParameterType != GenericParameterType.Method)
				{
					return ((TypeReference)this.owner).Scope;
				}
				return ((MethodReference)this.owner).DeclaringType.Scope;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000630F File Offset: 0x0000450F
		// (set) Token: 0x0600022E RID: 558 RVA: 0x00002B65 File Offset: 0x00000D65
		public override TypeReference DeclaringType
		{
			get
			{
				return this.owner as TypeReference;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000631C File Offset: 0x0000451C
		public MethodReference DeclaringMethod
		{
			get
			{
				return this.owner as MethodReference;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00006329 File Offset: 0x00004529
		public override ModuleDefinition Module
		{
			get
			{
				return this.module ?? this.owner.Module;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00006340 File Offset: 0x00004540
		public override string Name
		{
			get
			{
				if (!string.IsNullOrEmpty(base.Name))
				{
					return base.Name;
				}
				return base.Name = ((this.type == GenericParameterType.Method) ? "!!" : "!") + this.position;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00004111 File Offset: 0x00002311
		// (set) Token: 0x06000233 RID: 563 RVA: 0x00002B65 File Offset: 0x00000D65
		public override string Namespace
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000638F File Offset: 0x0000458F
		public override string FullName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public override bool IsGenericParameter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public override bool ContainsGenericParameter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00006397 File Offset: 0x00004597
		public override MetadataType MetadataType
		{
			get
			{
				return (MetadataType)this.etype;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000639F File Offset: 0x0000459F
		// (set) Token: 0x06000239 RID: 569 RVA: 0x000063AE File Offset: 0x000045AE
		public bool IsNonVariant
		{
			get
			{
				return this.attributes.GetMaskedAttributes(3, 0U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(3, 0U, value);
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600023A RID: 570 RVA: 0x000063C4 File Offset: 0x000045C4
		// (set) Token: 0x0600023B RID: 571 RVA: 0x000063D3 File Offset: 0x000045D3
		public bool IsCovariant
		{
			get
			{
				return this.attributes.GetMaskedAttributes(3, 1U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(3, 1U, value);
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600023C RID: 572 RVA: 0x000063E9 File Offset: 0x000045E9
		// (set) Token: 0x0600023D RID: 573 RVA: 0x000063F8 File Offset: 0x000045F8
		public bool IsContravariant
		{
			get
			{
				return this.attributes.GetMaskedAttributes(3, 2U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(3, 2U, value);
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000640E File Offset: 0x0000460E
		// (set) Token: 0x0600023F RID: 575 RVA: 0x0000641C File Offset: 0x0000461C
		public bool HasReferenceTypeConstraint
		{
			get
			{
				return this.attributes.GetAttributes(4);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(4, value);
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00006431 File Offset: 0x00004631
		// (set) Token: 0x06000241 RID: 577 RVA: 0x0000643F File Offset: 0x0000463F
		public bool HasNotNullableValueTypeConstraint
		{
			get
			{
				return this.attributes.GetAttributes(8);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(8, value);
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00006454 File Offset: 0x00004654
		// (set) Token: 0x06000243 RID: 579 RVA: 0x00006463 File Offset: 0x00004663
		public bool HasDefaultConstructorConstraint
		{
			get
			{
				return this.attributes.GetAttributes(16);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(16, value);
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00006479 File Offset: 0x00004679
		public GenericParameter(IGenericParameterProvider owner)
			: this(string.Empty, owner)
		{
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00006488 File Offset: 0x00004688
		public GenericParameter(string name, IGenericParameterProvider owner)
			: base(string.Empty, name)
		{
			if (owner == null)
			{
				throw new ArgumentNullException();
			}
			this.position = -1;
			this.owner = owner;
			this.type = owner.GenericParameterType;
			this.etype = GenericParameter.ConvertGenericParameterType(this.type);
			this.token = new MetadataToken(TokenType.GenericParam);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000064E8 File Offset: 0x000046E8
		internal GenericParameter(int position, GenericParameterType type, ModuleDefinition module)
			: base(string.Empty, string.Empty)
		{
			Mixin.CheckModule(module);
			this.position = position;
			this.type = type;
			this.etype = GenericParameter.ConvertGenericParameterType(type);
			this.module = module;
			this.token = new MetadataToken(TokenType.GenericParam);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000653C File Offset: 0x0000473C
		private static ElementType ConvertGenericParameterType(GenericParameterType type)
		{
			if (type == GenericParameterType.Type)
			{
				return ElementType.Var;
			}
			if (type != GenericParameterType.Method)
			{
				throw new ArgumentOutOfRangeException();
			}
			return ElementType.MVar;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00005FCD File Offset: 0x000041CD
		public override TypeDefinition Resolve()
		{
			return null;
		}

		// Token: 0x0400008D RID: 141
		internal int position;

		// Token: 0x0400008E RID: 142
		internal GenericParameterType type;

		// Token: 0x0400008F RID: 143
		internal IGenericParameterProvider owner;

		// Token: 0x04000090 RID: 144
		private ushort attributes;

		// Token: 0x04000091 RID: 145
		private Collection<TypeReference> constraints;

		// Token: 0x04000092 RID: 146
		private Collection<CustomAttribute> custom_attributes;
	}
}
