using System;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000270 RID: 624
	internal readonly struct NullableContextState
	{
		// Token: 0x17000F9B RID: 3995
		// (get) Token: 0x06003B77 RID: 15223 RVA: 0x00102ED3 File Offset: 0x001010D3
		internal int Position { get; }

		// Token: 0x17000F9C RID: 3996
		// (get) Token: 0x06003B78 RID: 15224 RVA: 0x00102EDB File Offset: 0x001010DB
		internal NullableContextState.State WarningsState { get; }

		// Token: 0x17000F9D RID: 3997
		// (get) Token: 0x06003B79 RID: 15225 RVA: 0x00102EE3 File Offset: 0x001010E3
		internal NullableContextState.State AnnotationsState { get; }

		// Token: 0x06003B7A RID: 15226 RVA: 0x00102EEB File Offset: 0x001010EB
		internal NullableContextState(int position, NullableContextState.State warningsState, NullableContextState.State annotationsState)
		{
			this.Position = position;
			this.WarningsState = warningsState;
			this.AnnotationsState = annotationsState;
		}

		// Token: 0x02000785 RID: 1925
		internal enum State : byte
		{
			// Token: 0x040023BF RID: 9151
			Unknown,
			// Token: 0x040023C0 RID: 9152
			Disabled,
			// Token: 0x040023C1 RID: 9153
			Enabled,
			// Token: 0x040023C2 RID: 9154
			ExplicitlyRestored
		}
	}
}
