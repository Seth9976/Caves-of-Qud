using System;
using System.Runtime.Serialization;

namespace System.IO
{
	/// <summary>The exception that is thrown when a data stream is in an invalid format.</summary>
	// Token: 0x0200051C RID: 1308
	[Serializable]
	public sealed class InvalidDataException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.InvalidDataException" /> class.</summary>
		// Token: 0x06002A58 RID: 10840 RVA: 0x000918C6 File Offset: 0x0008FAC6
		public InvalidDataException()
			: base(global::Locale.GetText("Invalid data format."))
		{
			base.HResult = -2146233085;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.InvalidDataException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06002A59 RID: 10841 RVA: 0x000918E3 File Offset: 0x0008FAE3
		public InvalidDataException(string message)
			: base(message)
		{
			base.HResult = -2146233085;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.InvalidDataException" /> class with a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception.</param>
		// Token: 0x06002A5A RID: 10842 RVA: 0x000918F7 File Offset: 0x0008FAF7
		public InvalidDataException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146233085;
		}

		// Token: 0x06002A5B RID: 10843 RVA: 0x00055723 File Offset: 0x00053923
		private InvalidDataException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0400169C RID: 5788
		private const int Result = -2146233085;
	}
}
