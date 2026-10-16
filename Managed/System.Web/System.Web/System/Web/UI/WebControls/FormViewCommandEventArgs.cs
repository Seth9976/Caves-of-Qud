using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.FormView.ItemCommand" /> event.</summary>
	// Token: 0x02000397 RID: 919
	public class FormViewCommandEventArgs : CommandEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FormViewCommandEventArgs" /> class.</summary>
		/// <param name="commandSource">The source of the command.</param>
		/// <param name="originalArgs">A <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> that contains event data.</param>
		// Token: 0x060024C4 RID: 9412 RVA: 0x0005DF2D File Offset: 0x0005C12D
		public FormViewCommandEventArgs(object commandSource, CommandEventArgs originalArgs)
			: base(originalArgs)
		{
			this.source = commandSource;
		}

		/// <summary>Gets the control that raised the event.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the control that raised the event.</returns>
		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x060024C5 RID: 9413 RVA: 0x0005DF3D File Offset: 0x0005C13D
		public object CommandSource
		{
			get
			{
				return this.source;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the control has handled the event.</summary>
		/// <returns>true if data-bound event code was skipped or has finished; otherwise, false.</returns>
		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x060024C6 RID: 9414 RVA: 0x0005DF48 File Offset: 0x0005C148
		// (set) Token: 0x060024C7 RID: 9415 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public bool Handled
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x040019D2 RID: 6610
		private object source;
	}
}
