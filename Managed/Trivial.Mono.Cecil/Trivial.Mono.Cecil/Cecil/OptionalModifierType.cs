using System;
using Trivial.Mono.Cecil.Metadata;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000097 RID: 151
	public sealed class OptionalModifierType : TypeSpecification, IModifierType
	{
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x000143E2 File Offset: 0x000125E2
		// (set) Token: 0x0600064D RID: 1613 RVA: 0x000143EA File Offset: 0x000125EA
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

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x000143F3 File Offset: 0x000125F3
		public override string Name
		{
			get
			{
				return base.Name + this.Suffix;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x00014406 File Offset: 0x00012606
		public override string FullName
		{
			get
			{
				return base.FullName + this.Suffix;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x00014419 File Offset: 0x00012619
		private string Suffix
		{
			get
			{
				return " modopt(" + this.modifier_type + ")";
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x000025EF File Offset: 0x000007EF
		// (set) Token: 0x06000652 RID: 1618 RVA: 0x00002B65 File Offset: 0x00000D65
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

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public override bool IsOptionalModifier
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x00014430 File Offset: 0x00012630
		public override bool ContainsGenericParameter
		{
			get
			{
				return this.modifier_type.ContainsGenericParameter || base.ContainsGenericParameter;
			}
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00014448 File Offset: 0x00012648
		public OptionalModifierType(TypeReference modifierType, TypeReference type)
			: base(type)
		{
			if (modifierType == null)
			{
				throw new ArgumentNullException(Mixin.Argument.modifierType.ToString());
			}
			Mixin.CheckType(type);
			this.modifier_type = modifierType;
			this.etype = Trivial.Mono.Cecil.Metadata.ElementType.CModOpt;
		}

		// Token: 0x04000206 RID: 518
		private TypeReference modifier_type;
	}
}
