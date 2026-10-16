using System;

namespace Sheeter
{
	// Token: 0x02000313 RID: 787
	public class RowBlueprint
	{
		// Token: 0x04000F57 RID: 3927
		[Field(Key = "Name")]
		public string Blueprint;

		// Token: 0x04000F58 RID: 3928
		[Property(Key = "CachedDisplayNameStripped")]
		public string DisplayName;

		// Token: 0x04000F59 RID: 3929
		[Tag]
		public string Creature;

		// Token: 0x04000F5A RID: 3930
		[Tag]
		public string Shield;

		// Token: 0x04000F5B RID: 3931
		[Tag]
		[Tag(Key = "ShowMeleeWeaponStats")]
		public string MeleeWeapon;

		// Token: 0x04000F5C RID: 3932
		[Tag(Key = "Gender", Value = "nonspecific")]
		public string Nonspecific;

		// Token: 0x04000F5D RID: 3933
		[IntProperty(Value = 1)]
		public string Bleeds;

		// Token: 0x04000F5E RID: 3934
		[Tag]
		[Part]
		public string Armor;

		// Token: 0x04000F5F RID: 3935
		[Tag(Key = "SemanticFood")]
		[Part]
		public string Food;

		// Token: 0x04000F60 RID: 3936
		[Tag]
		public string NaturalGear;
	}
}
