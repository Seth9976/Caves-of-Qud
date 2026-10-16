using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Provides the base functionality for creating event providers that require buffering.</summary>
	// Token: 0x0200073D RID: 1853
	public abstract class BufferedWebEventProvider : WebEventProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.BufferedWebEventProvider" /> class. </summary>
		// Token: 0x06004C55 RID: 19541 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected BufferedWebEventProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a value indicating the buffering mode used by the provider.</summary>
		/// <returns>The name of the buffering mode. </returns>
		// Token: 0x1700178B RID: 6027
		// (get) Token: 0x06004C56 RID: 19542 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string BufferMode
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a value indicating whether the provider is in buffered mode.</summary>
		/// <returns>true if the provider is in buffered mode; otherwise, false. The default is true.</returns>
		// Token: 0x1700178C RID: 6028
		// (get) Token: 0x06004C57 RID: 19543 RVA: 0x000C853C File Offset: 0x000C673C
		public bool UseBuffering
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Moves the events from the provider's buffer into the event log. </summary>
		// Token: 0x06004C58 RID: 19544 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override void Flush()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Processes the event passed to the provider.</summary>
		/// <param name="eventRaised">The <see cref="T:System.Web.Management.WebBaseEvent" /> object to process.</param>
		// Token: 0x06004C59 RID: 19545 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override void ProcessEvent(WebBaseEvent eventRaised)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Processes the buffered events.</summary>
		/// <param name="flushInfo">A <see cref="T:System.Web.Management.WebEventBufferFlushInfo" /> that contains buffering information.</param>
		// Token: 0x06004C5A RID: 19546
		public abstract void ProcessEventFlush(WebEventBufferFlushInfo flushInfo);

		/// <summary>Performs tasks associated with shutting down the provider.</summary>
		// Token: 0x06004C5B RID: 19547 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override void Shutdown()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
