using System;
using System.Configuration;
using Unity;

namespace System.Net.Configuration
{
	/// <summary>Represents the <see cref="T:System.Net.HttpListener" /> timeouts element in the configuration file. This class cannot be inherited.</summary>
	// Token: 0x02000879 RID: 2169
	public sealed class HttpListenerTimeoutsElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.HttpListenerTimeoutsElement" /> class.</summary>
		// Token: 0x060044B3 RID: 17587 RVA: 0x00013BAE File Offset: 0x00011DAE
		public HttpListenerTimeoutsElement()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the time, in seconds, allowed for the <see cref="T:System.Net.HttpListener" />  to drain the entity body on a Keep-Alive connection.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The time, in seconds, allowed for the <see cref="T:System.Net.HttpListener" />  to drain the entity body on a Keep-Alive connection.</returns>
		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x060044B4 RID: 17588 RVA: 0x000ED120 File Offset: 0x000EB320
		public TimeSpan DrainEntityBody
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(TimeSpan);
			}
		}

		/// <summary>Gets the time, in seconds, allowed for the request entity body to arrive.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The time, in seconds, allowed for the request entity body to arrive.</returns>
		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x060044B5 RID: 17589 RVA: 0x000ED13C File Offset: 0x000EB33C
		public TimeSpan EntityBody
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(TimeSpan);
			}
		}

		/// <summary>Gets the time, in seconds, allowed for the <see cref="T:System.Net.HttpListener" /> to parse the request header.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The time, in seconds, allowed for the <see cref="T:System.Net.HttpListener" /> to parse the request header.</returns>
		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x060044B6 RID: 17590 RVA: 0x000ED158 File Offset: 0x000EB358
		public TimeSpan HeaderWait
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(TimeSpan);
			}
		}

		/// <summary>Gets the time, in seconds, allowed for an idle connection.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The time, in seconds, allowed for an idle connection.</returns>
		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x060044B7 RID: 17591 RVA: 0x000ED174 File Offset: 0x000EB374
		public TimeSpan IdleConnection
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(TimeSpan);
			}
		}

		/// <summary>Gets the minimum send rate, in bytes-per-second, for the response.</summary>
		/// <returns>Returns <see cref="T:System.Int64" />.The minimum send rate, in bytes-per-second, for the response.</returns>
		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x060044B8 RID: 17592 RVA: 0x000ED190 File Offset: 0x000EB390
		public long MinSendBytesPerSecond
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0L;
			}
		}

		/// <summary>Gets the time, in seconds, allowed for the request to remain in the request queue before the <see cref="T:System.Net.HttpListener" /> picks it up.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The time, in seconds, allowed for the request to remain in the request queue before the <see cref="T:System.Net.HttpListener" /> picks it up.</returns>
		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x060044B9 RID: 17593 RVA: 0x000ED1AC File Offset: 0x000EB3AC
		public TimeSpan RequestQueue
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(TimeSpan);
			}
		}
	}
}
