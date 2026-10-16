using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000213 RID: 531
	public enum Accessibility
	{
		// Token: 0x04000792 RID: 1938
		NotApplicable,
		// Token: 0x04000793 RID: 1939
		Private,
		// Token: 0x04000794 RID: 1940
		ProtectedAndInternal,
		// Token: 0x04000795 RID: 1941
		ProtectedAndFriend = 2,
		// Token: 0x04000796 RID: 1942
		Protected,
		// Token: 0x04000797 RID: 1943
		Internal,
		// Token: 0x04000798 RID: 1944
		Friend = 4,
		// Token: 0x04000799 RID: 1945
		ProtectedOrInternal,
		// Token: 0x0400079A RID: 1946
		ProtectedOrFriend = 5,
		// Token: 0x0400079B RID: 1947
		Public
	}
}
