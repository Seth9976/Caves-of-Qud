using System;
using System.Collections;
using System.IO;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides the base class for storing serialization data for the <see cref="T:System.ComponentModel.Design.Serialization.ComponentSerializationService" />.</summary>
	// Token: 0x02000496 RID: 1174
	public abstract class SerializationStore : IDisposable
	{
		/// <summary>Gets a collection of errors that occurred during serialization or deserialization.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains errors that occurred during serialization or deserialization.</returns>
		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06002566 RID: 9574
		public abstract ICollection Errors { get; }

		/// <summary>Closes the serialization store.</summary>
		// Token: 0x06002567 RID: 9575
		public abstract void Close();

		/// <summary>Saves the store to the given stream.</summary>
		/// <param name="stream">The stream to which the store will be serialized.</param>
		// Token: 0x06002568 RID: 9576
		public abstract void Save(Stream stream);

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" />.</summary>
		// Token: 0x06002569 RID: 9577 RVA: 0x0008335B File Offset: 0x0008155B
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x0600256A RID: 9578 RVA: 0x00083364 File Offset: 0x00081564
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}
	}
}
