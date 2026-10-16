using System;
using System.Runtime.Serialization;
using System.Security;

namespace System.Threading
{
	/// <summary>The exception that is thrown when the post-phase action of a <see cref="T:System.Threading.Barrier" /> fails</summary>
	// Token: 0x0200017F RID: 383
	[Serializable]
	public class BarrierPostPhaseException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.BarrierPostPhaseException" /> class with a system-supplied message that describes the error.</summary>
		// Token: 0x06000A28 RID: 2600 RVA: 0x0002C3DB File Offset: 0x0002A5DB
		public BarrierPostPhaseException()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.BarrierPostPhaseException" /> class with the specified inner exception.</summary>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		// Token: 0x06000A29 RID: 2601 RVA: 0x0002C3E4 File Offset: 0x0002A5E4
		public BarrierPostPhaseException(Exception innerException)
			: this(null, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.BarrierPostPhaseException" /> class with a specified message that describes the error.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x06000A2A RID: 2602 RVA: 0x0002C3EE File Offset: 0x0002A5EE
		public BarrierPostPhaseException(string message)
			: this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.BarrierPostPhaseException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000A2B RID: 2603 RVA: 0x0002C3F8 File Offset: 0x0002A5F8
		public BarrierPostPhaseException(string message, Exception innerException)
			: base((message == null) ? SR.GetString("The postPhaseAction failed with an exception.") : message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.BarrierPostPhaseException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06000A2C RID: 2604 RVA: 0x0002C411 File Offset: 0x0002A611
		[SecurityCritical]
		protected BarrierPostPhaseException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
