using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200005A RID: 90
	public interface IModifierType
	{
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060002A9 RID: 681
		TypeReference ModifierType { get; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060002AA RID: 682
		TypeReference ElementType { get; }
	}
}
