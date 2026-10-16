using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000039 RID: 57
	public class FieldReference : MemberReference
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00005C15 File Offset: 0x00003E15
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x00005C1D File Offset: 0x00003E1D
		public TypeReference FieldType
		{
			get
			{
				return this.field_type;
			}
			set
			{
				this.field_type = value;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00005C26 File Offset: 0x00003E26
		public override string FullName
		{
			get
			{
				return this.field_type.FullName + " " + base.MemberFullName();
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00005C43 File Offset: 0x00003E43
		public override bool ContainsGenericParameter
		{
			get
			{
				return this.field_type.ContainsGenericParameter || base.ContainsGenericParameter;
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00005C5A File Offset: 0x00003E5A
		internal FieldReference()
		{
			this.token = new MetadataToken(TokenType.MemberRef);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00005C72 File Offset: 0x00003E72
		public FieldReference(string name, TypeReference fieldType)
			: base(name)
		{
			Mixin.CheckType(fieldType, Mixin.Argument.fieldType);
			this.field_type = fieldType;
			this.token = new MetadataToken(TokenType.MemberRef);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00005C9A File Offset: 0x00003E9A
		public FieldReference(string name, TypeReference fieldType, TypeReference declaringType)
			: this(name, fieldType)
		{
			Mixin.CheckType(declaringType, Mixin.Argument.declaringType);
			this.DeclaringType = declaringType;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00005CB3 File Offset: 0x00003EB3
		protected override IMemberDefinition ResolveDefinition()
		{
			return this.Resolve();
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00005CBC File Offset: 0x00003EBC
		public new virtual FieldDefinition Resolve()
		{
			ModuleDefinition module = this.Module;
			if (module == null)
			{
				throw new NotSupportedException();
			}
			return module.Resolve(this);
		}

		// Token: 0x04000082 RID: 130
		private TypeReference field_type;
	}
}
