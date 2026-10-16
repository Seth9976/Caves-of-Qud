using System;

namespace Mono.Cecil
{
	// Token: 0x02000287 RID: 647
	public enum TokenType : uint
	{
		// Token: 0x040005CF RID: 1487
		Module,
		// Token: 0x040005D0 RID: 1488
		TypeRef = 16777216U,
		// Token: 0x040005D1 RID: 1489
		TypeDef = 33554432U,
		// Token: 0x040005D2 RID: 1490
		Field = 67108864U,
		// Token: 0x040005D3 RID: 1491
		Method = 100663296U,
		// Token: 0x040005D4 RID: 1492
		Param = 134217728U,
		// Token: 0x040005D5 RID: 1493
		InterfaceImpl = 150994944U,
		// Token: 0x040005D6 RID: 1494
		MemberRef = 167772160U,
		// Token: 0x040005D7 RID: 1495
		CustomAttribute = 201326592U,
		// Token: 0x040005D8 RID: 1496
		Permission = 234881024U,
		// Token: 0x040005D9 RID: 1497
		Signature = 285212672U,
		// Token: 0x040005DA RID: 1498
		Event = 335544320U,
		// Token: 0x040005DB RID: 1499
		Property = 385875968U,
		// Token: 0x040005DC RID: 1500
		ModuleRef = 436207616U,
		// Token: 0x040005DD RID: 1501
		TypeSpec = 452984832U,
		// Token: 0x040005DE RID: 1502
		Assembly = 536870912U,
		// Token: 0x040005DF RID: 1503
		AssemblyRef = 587202560U,
		// Token: 0x040005E0 RID: 1504
		File = 637534208U,
		// Token: 0x040005E1 RID: 1505
		ExportedType = 654311424U,
		// Token: 0x040005E2 RID: 1506
		ManifestResource = 671088640U,
		// Token: 0x040005E3 RID: 1507
		GenericParam = 704643072U,
		// Token: 0x040005E4 RID: 1508
		MethodSpec = 721420288U,
		// Token: 0x040005E5 RID: 1509
		GenericParamConstraint = 738197504U,
		// Token: 0x040005E6 RID: 1510
		Document = 805306368U,
		// Token: 0x040005E7 RID: 1511
		MethodDebugInformation = 822083584U,
		// Token: 0x040005E8 RID: 1512
		LocalScope = 838860800U,
		// Token: 0x040005E9 RID: 1513
		LocalVariable = 855638016U,
		// Token: 0x040005EA RID: 1514
		LocalConstant = 872415232U,
		// Token: 0x040005EB RID: 1515
		ImportScope = 889192448U,
		// Token: 0x040005EC RID: 1516
		StateMachineMethod = 905969664U,
		// Token: 0x040005ED RID: 1517
		CustomDebugInformation = 922746880U,
		// Token: 0x040005EE RID: 1518
		String = 1879048192U
	}
}
