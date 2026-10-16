using System;
using Unity;

namespace System.Diagnostics
{
	/// <summary>Provides data for the <see cref="E:System.Diagnostics.Process.OutputDataReceived" /> and <see cref="E:System.Diagnostics.Process.ErrorDataReceived" /> events.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000253 RID: 595
	public class DataReceivedEventArgs : EventArgs
	{
		// Token: 0x06001247 RID: 4679 RVA: 0x0004EA38 File Offset: 0x0004CC38
		internal DataReceivedEventArgs(string data)
		{
			this.data = data;
		}

		/// <summary>Gets the line of characters that was written to a redirected <see cref="T:System.Diagnostics.Process" /> output stream.</summary>
		/// <returns>The line that was written by an associated <see cref="T:System.Diagnostics.Process" /> to its redirected <see cref="P:System.Diagnostics.Process.StandardOutput" /> or <see cref="P:System.Diagnostics.Process.StandardError" /> stream.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06001248 RID: 4680 RVA: 0x0004EA47 File Offset: 0x0004CC47
		public string Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x00013BAE File Offset: 0x00011DAE
		internal DataReceivedEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000ABA RID: 2746
		private string data;
	}
}
