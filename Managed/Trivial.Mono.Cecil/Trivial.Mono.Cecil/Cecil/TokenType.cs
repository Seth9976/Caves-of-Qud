using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x020000BA RID: 186
	public enum TokenType : uint
	{
		// Token: 0x04000285 RID: 645
		Module,
		// Token: 0x04000286 RID: 646
		TypeRef = 16777216U,
		// Token: 0x04000287 RID: 647
		TypeDef = 33554432U,
		// Token: 0x04000288 RID: 648
		Field = 67108864U,
		// Token: 0x04000289 RID: 649
		Method = 100663296U,
		// Token: 0x0400028A RID: 650
		Param = 134217728U,
		// Token: 0x0400028B RID: 651
		InterfaceImpl = 150994944U,
		// Token: 0x0400028C RID: 652
		MemberRef = 167772160U,
		// Token: 0x0400028D RID: 653
		CustomAttribute = 201326592U,
		// Token: 0x0400028E RID: 654
		Permission = 234881024U,
		// Token: 0x0400028F RID: 655
		Signature = 285212672U,
		// Token: 0x04000290 RID: 656
		Event = 335544320U,
		// Token: 0x04000291 RID: 657
		Property = 385875968U,
		// Token: 0x04000292 RID: 658
		ModuleRef = 436207616U,
		// Token: 0x04000293 RID: 659
		TypeSpec = 452984832U,
		// Token: 0x04000294 RID: 660
		Assembly = 536870912U,
		// Token: 0x04000295 RID: 661
		AssemblyRef = 587202560U,
		// Token: 0x04000296 RID: 662
		File = 637534208U,
		// Token: 0x04000297 RID: 663
		ExportedType = 654311424U,
		// Token: 0x04000298 RID: 664
		ManifestResource = 671088640U,
		// Token: 0x04000299 RID: 665
		GenericParam = 704643072U,
		// Token: 0x0400029A RID: 666
		MethodSpec = 721420288U,
		// Token: 0x0400029B RID: 667
		GenericParamConstraint = 738197504U,
		// Token: 0x0400029C RID: 668
		Document = 805306368U,
		// Token: 0x0400029D RID: 669
		MethodDebugInformation = 822083584U,
		// Token: 0x0400029E RID: 670
		LocalScope = 838860800U,
		// Token: 0x0400029F RID: 671
		LocalVariable = 855638016U,
		// Token: 0x040002A0 RID: 672
		LocalConstant = 872415232U,
		// Token: 0x040002A1 RID: 673
		ImportScope = 889192448U,
		// Token: 0x040002A2 RID: 674
		StateMachineMethod = 905969664U,
		// Token: 0x040002A3 RID: 675
		CustomDebugInformation = 922746880U,
		// Token: 0x040002A4 RID: 676
		String = 1879048192U
	}
}
