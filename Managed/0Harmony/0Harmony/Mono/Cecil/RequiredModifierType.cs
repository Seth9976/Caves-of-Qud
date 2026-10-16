using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x02000242 RID: 578
	internal sealed class RequiredModifierType : TypeSpecification, IModifierType
	{
		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x0002BE2E File Offset: 0x0002A02E
		// (set) Token: 0x06000D51 RID: 3409 RVA: 0x0002BE36 File Offset: 0x0002A036
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

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000D52 RID: 3410 RVA: 0x0002BE3F File Offset: 0x0002A03F
		public override string Name
		{
			get
			{
				return base.Name + this.Suffix;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000D53 RID: 3411 RVA: 0x0002BE52 File Offset: 0x0002A052
		public override string FullName
		{
			get
			{
				return base.FullName + this.Suffix;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x0002BE65 File Offset: 0x0002A065
		private string Suffix
		{
			get
			{
				string text = " modreq(";
				TypeReference typeReference = this.modifier_type;
				return text + ((typeReference != null) ? typeReference.ToString() : null) + ")";
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x00018318 File Offset: 0x00016518
		// (set) Token: 0x06000D56 RID: 3414 RVA: 0x000189AE File Offset: 0x00016BAE
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

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000D57 RID: 3415 RVA: 0x00018941 File Offset: 0x00016B41
		public override bool IsRequiredModifier
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000D58 RID: 3416 RVA: 0x0002BE88 File Offset: 0x0002A088
		public override bool ContainsGenericParameter
		{
			get
			{
				return this.modifier_type.ContainsGenericParameter || base.ContainsGenericParameter;
			}
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0002BEA0 File Offset: 0x0002A0A0
		public RequiredModifierType(TypeReference modifierType, TypeReference type)
			: base(type)
		{
			if (modifierType == null)
			{
				throw new ArgumentNullException(Mixin.Argument.modifierType.ToString());
			}
			Mixin.CheckType(type);
			this.modifier_type = modifierType;
			this.etype = Mono.Cecil.Metadata.ElementType.CModReqD;
		}

		// Token: 0x040003E6 RID: 998
		private TypeReference modifier_type;
	}
}
