using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x02000241 RID: 577
	internal sealed class OptionalModifierType : TypeSpecification, IModifierType
	{
		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x0002BD79 File Offset: 0x00029F79
		// (set) Token: 0x06000D47 RID: 3399 RVA: 0x0002BD81 File Offset: 0x00029F81
		public TypeReference ModifierType
		{
			get
			{
				return this.modifier_type;
			}
			set
			{
				this.modifier_type = value;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x0002BD8A File Offset: 0x00029F8A
		public override string Name
		{
			get
			{
				return base.Name + this.Suffix;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000D49 RID: 3401 RVA: 0x0002BD9D File Offset: 0x00029F9D
		public override string FullName
		{
			get
			{
				return base.FullName + this.Suffix;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x0002BDB0 File Offset: 0x00029FB0
		private string Suffix
		{
			get
			{
				string text = " modopt(";
				TypeReference typeReference = this.modifier_type;
				return text + ((typeReference != null) ? typeReference.ToString() : null) + ")";
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000D4B RID: 3403 RVA: 0x00018318 File Offset: 0x00016518
		// (set) Token: 0x06000D4C RID: 3404 RVA: 0x000189AE File Offset: 0x00016BAE
		public override bool IsValueType
		{
			get
			{
				return false;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000D4D RID: 3405 RVA: 0x00018941 File Offset: 0x00016B41
		public override bool IsOptionalModifier
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x0002BDD3 File Offset: 0x00029FD3
		public override bool ContainsGenericParameter
		{
			get
			{
				return this.modifier_type.ContainsGenericParameter || base.ContainsGenericParameter;
			}
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0002BDEC File Offset: 0x00029FEC
		public OptionalModifierType(TypeReference modifierType, TypeReference type)
			: base(type)
		{
			if (modifierType == null)
			{
				throw new ArgumentNullException(Mixin.Argument.modifierType.ToString());
			}
			Mixin.CheckType(type);
			this.modifier_type = modifierType;
			this.etype = Mono.Cecil.Metadata.ElementType.CModOpt;
		}

		// Token: 0x040003E5 RID: 997
		private TypeReference modifier_type;
	}
}
