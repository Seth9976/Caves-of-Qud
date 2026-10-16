using System;
using System.Runtime.Serialization;
using System.Security;
using Unity;

namespace System.Runtime.CompilerServices
{
	/// <summary>Wraps an exception that does not derive from the <see cref="T:System.Exception" /> class. This class cannot be inherited.</summary>
	// Token: 0x02000803 RID: 2051
	[Serializable]
	public sealed class RuntimeWrappedException : Exception
	{
		// Token: 0x0600461D RID: 17949 RVA: 0x000E590E File Offset: 0x000E3B0E
		public RuntimeWrappedException(object thrownObject)
			: base("An object that does not derive from System.Exception has been wrapped in a RuntimeWrappedException.")
		{
			base.HResult = -2146233026;
			this._wrappedException = thrownObject;
		}

		// Token: 0x0600461E RID: 17950 RVA: 0x000E592D File Offset: 0x000E3B2D
		private RuntimeWrappedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._wrappedException = info.GetValue("WrappedException", typeof(object));
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with information about the exception.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null.</exception>
		// Token: 0x0600461F RID: 17951 RVA: 0x000E5952 File Offset: 0x000E3B52
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("WrappedException", this._wrappedException, typeof(object));
		}

		/// <summary>Gets the object that was wrapped by the <see cref="T:System.Runtime.CompilerServices.RuntimeWrappedException" /> object.</summary>
		/// <returns>The object that was wrapped by the <see cref="T:System.Runtime.CompilerServices.RuntimeWrappedException" /> object.</returns>
		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06004620 RID: 17952 RVA: 0x000E5977 File Offset: 0x000E3B77
		public object WrappedException
		{
			get
			{
				return this._wrappedException;
			}
		}

		// Token: 0x06004621 RID: 17953 RVA: 0x000173AD File Offset: 0x000155AD
		internal RuntimeWrappedException()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04002D40 RID: 11584
		private object _wrappedException;
	}
}
