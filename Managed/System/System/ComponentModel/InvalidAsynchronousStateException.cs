using System;
using System.Runtime.Serialization;

namespace System.ComponentModel
{
	/// <summary>Thrown when a thread on which an operation should execute no longer exists or has no message loop. </summary>
	// Token: 0x020003C6 RID: 966
	[Serializable]
	public class InvalidAsynchronousStateException : ArgumentException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidAsynchronousStateException" /> class. </summary>
		// Token: 0x06001F43 RID: 8003 RVA: 0x0006CDD9 File Offset: 0x0006AFD9
		public InvalidAsynchronousStateException()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidAsynchronousStateException" /> class with the specified detailed description.</summary>
		/// <param name="message">A detailed description of the error.</param>
		// Token: 0x06001F44 RID: 8004 RVA: 0x00068543 File Offset: 0x00066743
		public InvalidAsynchronousStateException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidAsynchronousStateException" /> class with the specified detailed description and the specified exception. </summary>
		/// <param name="message">A detailed description of the error.</param>
		/// <param name="innerException">A reference to the inner exception that is the cause of this exception.</param>
		// Token: 0x06001F45 RID: 8005 RVA: 0x0006854C File Offset: 0x0006674C
		public InvalidAsynchronousStateException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidAsynchronousStateException" /> class with the given <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" />. </summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used for deserialization.</param>
		/// <param name="context">The destination to be used for deserialization.</param>
		// Token: 0x06001F46 RID: 8006 RVA: 0x0006857C File Offset: 0x0006677C
		protected InvalidAsynchronousStateException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
