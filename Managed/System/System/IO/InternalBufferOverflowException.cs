using System;
using System.Runtime.Serialization;

namespace System.IO
{
	/// <summary>The exception thrown when the internal buffer overflows.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200051B RID: 1307
	[Serializable]
	public class InternalBufferOverflowException : SystemException
	{
		/// <summary>Initializes a new default instance of the <see cref="T:System.IO.InternalBufferOverflowException" /> class.</summary>
		// Token: 0x06002A54 RID: 10836 RVA: 0x000918B9 File Offset: 0x0008FAB9
		public InternalBufferOverflowException()
			: base("Internal buffer overflow occurred.")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.InternalBufferOverflowException" /> class with the error message to be displayed specified.</summary>
		/// <param name="message">The message to be given for the exception. </param>
		// Token: 0x06002A55 RID: 10837 RVA: 0x0002F140 File Offset: 0x0002D340
		public InternalBufferOverflowException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.IO.InternalBufferOverflowException" /> class that is serializable using the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> objects.</summary>
		/// <param name="info">The information required to serialize the T:System.IO.InternalBufferOverflowException object.</param>
		/// <param name="context">The source and destination of the serialized stream associated with the T:System.IO.InternalBufferOverflowException object.</param>
		// Token: 0x06002A56 RID: 10838 RVA: 0x00055723 File Offset: 0x00053923
		protected InternalBufferOverflowException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.InternalBufferOverflowException" /> class with the message to be displayed and the generated inner exception specified.</summary>
		/// <param name="message">The message to be given for the exception. </param>
		/// <param name="inner">The inner exception. </param>
		// Token: 0x06002A57 RID: 10839 RVA: 0x0002F175 File Offset: 0x0002D375
		public InternalBufferOverflowException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
