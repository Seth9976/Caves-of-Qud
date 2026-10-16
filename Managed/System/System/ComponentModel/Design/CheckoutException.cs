using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.ComponentModel.Design
{
	/// <summary>The exception that is thrown when an attempt to check out a file that is checked into a source code management program is canceled or fails.</summary>
	// Token: 0x02000441 RID: 1089
	[Serializable]
	public class CheckoutException : ExternalException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CheckoutException" /> class with no associated message or error code.</summary>
		// Token: 0x06002389 RID: 9097 RVA: 0x00080FC7 File Offset: 0x0007F1C7
		public CheckoutException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CheckoutException" /> class with the specified message.</summary>
		/// <param name="message">A message describing the exception. </param>
		// Token: 0x0600238A RID: 9098 RVA: 0x00080FCF File Offset: 0x0007F1CF
		public CheckoutException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CheckoutException" /> class with the specified message and error code.</summary>
		/// <param name="message">A message describing the exception. </param>
		/// <param name="errorCode">The error code to pass. </param>
		// Token: 0x0600238B RID: 9099 RVA: 0x00080FD8 File Offset: 0x0007F1D8
		public CheckoutException(string message, int errorCode)
			: base(message, errorCode)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CheckoutException" /> class using the specified serialization data and context. </summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used for deserialization.</param>
		/// <param name="context">The destination to be used for deserialization.</param>
		// Token: 0x0600238C RID: 9100 RVA: 0x00080FE2 File Offset: 0x0007F1E2
		protected CheckoutException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CheckoutException" /> class with the specified detailed description and the specified exception. </summary>
		/// <param name="message">A detailed description of the error.</param>
		/// <param name="innerException">A reference to the inner exception that is the cause of this exception.</param>
		// Token: 0x0600238D RID: 9101 RVA: 0x00080FEC File Offset: 0x0007F1EC
		public CheckoutException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x040010CE RID: 4302
		private const int E_ABORT = -2147467260;

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CheckoutException" /> class that specifies that the check out was canceled. This field is read-only.</summary>
		// Token: 0x040010CF RID: 4303
		public static readonly CheckoutException Canceled = new CheckoutException("The checkout was canceled by the user.", -2147467260);
	}
}
