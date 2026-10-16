using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.BackgroundWorker.DoWork" /> event handler.</summary>
	// Token: 0x02000416 RID: 1046
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class DoWorkEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DoWorkEventArgs" /> class.</summary>
		/// <param name="argument">Specifies an argument for an asynchronous operation.</param>
		// Token: 0x060021BC RID: 8636 RVA: 0x00073498 File Offset: 0x00071698
		public DoWorkEventArgs(object argument)
		{
			this.argument = argument;
		}

		/// <summary>Gets a value that represents the argument of an asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the argument of an asynchronous operation.</returns>
		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x060021BD RID: 8637 RVA: 0x000734A7 File Offset: 0x000716A7
		[SRDescription("Argument passed into the worker handler from BackgroundWorker.RunWorkerAsync.")]
		public object Argument
		{
			get
			{
				return this.argument;
			}
		}

		/// <summary>Gets or sets a value that represents the result of an asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the result of an asynchronous operation.</returns>
		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x060021BE RID: 8638 RVA: 0x000734AF File Offset: 0x000716AF
		// (set) Token: 0x060021BF RID: 8639 RVA: 0x000734B7 File Offset: 0x000716B7
		[SRDescription("Result from the worker function.")]
		public object Result
		{
			get
			{
				return this.result;
			}
			set
			{
				this.result = value;
			}
		}

		// Token: 0x04001047 RID: 4167
		private object result;

		// Token: 0x04001048 RID: 4168
		private object argument;
	}
}
