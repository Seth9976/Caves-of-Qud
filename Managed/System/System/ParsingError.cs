using System;

namespace System
{
	// Token: 0x0200015D RID: 349
	internal enum ParsingError
	{
		// Token: 0x0400064E RID: 1614
		None,
		// Token: 0x0400064F RID: 1615
		BadFormat,
		// Token: 0x04000650 RID: 1616
		BadScheme,
		// Token: 0x04000651 RID: 1617
		BadAuthority,
		// Token: 0x04000652 RID: 1618
		EmptyUriString,
		// Token: 0x04000653 RID: 1619
		LastRelativeUriOkErrIndex = 4,
		// Token: 0x04000654 RID: 1620
		SchemeLimit,
		// Token: 0x04000655 RID: 1621
		SizeLimit,
		// Token: 0x04000656 RID: 1622
		MustRootedPath,
		// Token: 0x04000657 RID: 1623
		BadHostName,
		// Token: 0x04000658 RID: 1624
		NonEmptyHost,
		// Token: 0x04000659 RID: 1625
		BadPort,
		// Token: 0x0400065A RID: 1626
		BadAuthorityTerminator,
		// Token: 0x0400065B RID: 1627
		CannotCreateRelative
	}
}
