using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Reflection.Metadata
{
	/// <summary>The exception that is thrown when an attempt to write metadata exceeds a limit given by the format specification. For example, when the heap size limit is exceeded.</summary>
	// Token: 0x0200004B RID: 75
	[NullableContext(2)]
	[Nullable(0)]
	[Serializable]
	public class ImageFormatLimitationException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.Metadata.ImageFormatLimitationException" /> class.</summary>
		// Token: 0x060002DF RID: 735 RVA: 0x00008F69 File Offset: 0x00007169
		public ImageFormatLimitationException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.Metadata.ImageFormatLimitationException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for this exception.</param>
		// Token: 0x060002E0 RID: 736 RVA: 0x00008F71 File Offset: 0x00007171
		public ImageFormatLimitationException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.Metadata.ImageFormatLimitationException" /> class with a specified error message and the exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for this exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null" /> if no inner exception is specified.</param>
		// Token: 0x060002E1 RID: 737 RVA: 0x00008F7A File Offset: 0x0000717A
		public ImageFormatLimitationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.Metadata.ImageFormatLimitationException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x060002E2 RID: 738 RVA: 0x00008F84 File Offset: 0x00007184
		[NullableContext(1)]
		protected ImageFormatLimitationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
