using System;

namespace Mono.Cecil
{
	// Token: 0x02000206 RID: 518
	public class FieldReference : MemberReference
	{
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x0002783C File Offset: 0x00025A3C
		// (set) Token: 0x06000B06 RID: 2822 RVA: 0x00027844 File Offset: 0x00025A44
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

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x0002784D File Offset: 0x00025A4D
		public override string FullName
		{
			get
			{
				return this.field_type.FullName + " " + base.MemberFullName();
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000B08 RID: 2824 RVA: 0x0002786A File Offset: 0x00025A6A
		public override bool ContainsGenericParameter
		{
			get
			{
				return this.field_type.ContainsGenericParameter || base.ContainsGenericParameter;
			}
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00027881 File Offset: 0x00025A81
		internal FieldReference()
		{
			this.token = new MetadataToken(TokenType.MemberRef);
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00027899 File Offset: 0x00025A99
		public FieldReference(string name, TypeReference fieldType)
			: base(name)
		{
			Mixin.CheckType(fieldType, Mixin.Argument.fieldType);
			this.field_type = fieldType;
			this.token = new MetadataToken(TokenType.MemberRef);
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x000278C1 File Offset: 0x00025AC1
		public FieldReference(string name, TypeReference fieldType, TypeReference declaringType)
			: this(name, fieldType)
		{
			Mixin.CheckType(declaringType, Mixin.Argument.declaringType);
			this.DeclaringType = declaringType;
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x000278DA File Offset: 0x00025ADA
		protected override IMemberDefinition ResolveDefinition()
		{
			return this.Resolve();
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x000278E2 File Offset: 0x00025AE2
		public new virtual FieldDefinition Resolve()
		{
			ModuleDefinition module = this.Module;
			if (module == null)
			{
				throw new NotSupportedException();
			}
			return module.Resolve(this);
		}

		// Token: 0x04000328 RID: 808
		private TypeReference field_type;
	}
}
