using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Net.Mail
{
	/// <summary>The exception that is thrown when e-mail is sent using an <see cref="T:System.Net.Mail.SmtpClient" /> and cannot be delivered to all recipients.</summary>
	// Token: 0x02000813 RID: 2067
	[Serializable]
	public class SmtpFailedRecipientsException : SmtpFailedRecipientException, ISerializable
	{
		/// <summary>Initializes an empty instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> class.</summary>
		// Token: 0x0600421D RID: 16925 RVA: 0x000E48C9 File Offset: 0x000E2AC9
		public SmtpFailedRecipientsException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> class with the specified <see cref="T:System.String" />.</summary>
		/// <param name="message">The exception message.</param>
		// Token: 0x0600421E RID: 16926 RVA: 0x000E48D1 File Offset: 0x000E2AD1
		public SmtpFailedRecipientsException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> class with the specified <see cref="T:System.String" /> and inner <see cref="T:System.Exception" />.</summary>
		/// <param name="message">The exception message.</param>
		/// <param name="innerException">The inner exception.</param>
		// Token: 0x0600421F RID: 16927 RVA: 0x000E48DA File Offset: 0x000E2ADA
		public SmtpFailedRecipientsException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> class with the specified <see cref="T:System.String" /> and array of type <see cref="T:System.Net.Mail.SmtpFailedRecipientException" />.</summary>
		/// <param name="message">The exception message.</param>
		/// <param name="innerExceptions">The array of recipients with delivery errors.</param>
		// Token: 0x06004220 RID: 16928 RVA: 0x000E48E4 File Offset: 0x000E2AE4
		public SmtpFailedRecipientsException(string message, SmtpFailedRecipientException[] innerExceptions)
			: base(message)
		{
			this.innerExceptions = innerExceptions;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance that contains the information required to serialize the new <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> instance. </param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source of the serialized stream that is associated with the new <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> instance. </param>
		// Token: 0x06004221 RID: 16929 RVA: 0x000E48F4 File Offset: 0x000E2AF4
		protected SmtpFailedRecipientsException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.innerExceptions = (SmtpFailedRecipientException[])info.GetValue("innerExceptions", typeof(SmtpFailedRecipientException[]));
		}

		/// <summary>Gets one or more <see cref="T:System.Net.Mail.SmtpFailedRecipientException" />s that indicate the e-mail recipients with SMTP delivery errors.</summary>
		/// <returns>An array of type <see cref="T:System.Net.Mail.SmtpFailedRecipientException" /> that lists the recipients with delivery errors.</returns>
		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x06004222 RID: 16930 RVA: 0x000E492C File Offset: 0x000E2B2C
		public SmtpFailedRecipientException[] InnerExceptions
		{
			get
			{
				return this.innerExceptions;
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data that is needed to serialize the <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" />.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used. </param>
		/// <param name="streamingContext">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> to be used. </param>
		// Token: 0x06004223 RID: 16931 RVA: 0x000E4934 File Offset: 0x000E2B34
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			if (serializationInfo == null)
			{
				throw new ArgumentNullException("serializationInfo");
			}
			base.GetObjectData(serializationInfo, streamingContext);
			serializationInfo.AddValue("innerExceptions", this.innerExceptions);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> class from the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> instances.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the information required to serialize the new <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" />. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source of the serialized stream that is associated with the new <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" />. </param>
		// Token: 0x06004224 RID: 16932 RVA: 0x000A77F6 File Offset: 0x000A59F6
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this.GetObjectData(info, context);
		}

		// Token: 0x040027A2 RID: 10146
		private SmtpFailedRecipientException[] innerExceptions;
	}
}
