using System;
using Trivial.Mono.Cecil.Metadata;

namespace Trivial.Mono.Cecil
{
	// Token: 0x020000AA RID: 170
	public sealed class RequiredModifierType : TypeSpecification, IModifierType
	{
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x00015A67 File Offset: 0x00013C67
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x00015A6F File Offset: 0x00013C6F
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

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x00015A78 File Offset: 0x00013C78
		public override string Name
		{
			get
			{
				return base.Name + this.Suffix;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x00015A8B File Offset: 0x00013C8B
		public override string FullName
		{
			get
			{
				return base.FullName + this.Suffix;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x00015A9E File Offset: 0x00013C9E
		private string Suffix
		{
			get
			{
				return " modreq(" + this.modifier_type + ")";
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x000025EF File Offset: 0x000007EF
		// (set) Token: 0x0600071B RID: 1819 RVA: 0x00002B65 File Offset: 0x00000D65
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

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public override bool IsRequiredModifier
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x00015AB5 File Offset: 0x00013CB5
		public override bool ContainsGenericParameter
		{
			get
			{
				return this.modifier_type.ContainsGenericParameter || base.ContainsGenericParameter;
			}
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00015ACC File Offset: 0x00013CCC
		public RequiredModifierType(TypeReference modifierType, TypeReference type)
			: base(type)
		{
			if (modifierType == null)
			{
				throw new ArgumentNullException(Mixin.Argument.modifierType.ToString());
			}
			Mixin.CheckType(type);
			this.modifier_type = modifierType;
			this.etype = Trivial.Mono.Cecil.Metadata.ElementType.CModReqD;
		}

		// Token: 0x04000252 RID: 594
		private TypeReference modifier_type;
	}
}
