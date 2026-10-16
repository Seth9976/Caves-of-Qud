using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Unity;

namespace System.Web.SessionState
{
	/// <summary>Provides helper methods used by session-state modules and session-state store providers to manage session information for an ASP.NET application. This class cannot be inherited.</summary>
	// Token: 0x020004A3 RID: 1187
	public static class SessionStateUtility
	{
		/// <summary>Applies the session data to the context for the current request.</summary>
		/// <param name="context">The <see cref="T:System.Web.HttpContext" /> object to which to add the <see cref="T:System.Web.SessionState.HttpSessionState" /> object.</param>
		/// <param name="container">The <see cref="T:System.Web.SessionState.IHttpSessionState" /> implementation instance to add to the specified HTTP context.</param>
		/// <exception cref="T:System.Web.HttpException">An <see cref="T:System.Web.SessionState.HttpSessionState" /> object for the current session has already been added to the specified <paramref name="context" />.</exception>
		// Token: 0x060035D0 RID: 13776 RVA: 0x0008BA68 File Offset: 0x00089C68
		public static void AddHttpSessionStateToContext(HttpContext context, IHttpSessionState container)
		{
			if (context == null || container == null)
			{
				return;
			}
			if (context.Session != null)
			{
				throw new HttpException("An HttpSessionState object for the current session has already been added to the specified context.");
			}
			HttpSessionState httpSessionState = new HttpSessionState(container);
			context.SetSession(httpSessionState);
		}

		/// <summary>Retrieves session data from the context for the current request.</summary>
		/// <returns>An <see cref="T:System.Web.SessionState.IHttpSessionState" /> implementation instance populated with session data from the current request.</returns>
		/// <param name="context">The <see cref="T:System.Web.HttpContext" /> from which to retrieve session data.</param>
		// Token: 0x060035D1 RID: 13777 RVA: 0x0008BAA0 File Offset: 0x00089CA0
		public static IHttpSessionState GetHttpSessionStateFromContext(HttpContext context)
		{
			HttpSessionState session;
			if (context == null || (session = context.Session) == null)
			{
				return null;
			}
			return session.Container;
		}

		/// <summary>Gets a reference to the static objects collection for the specified context.</summary>
		/// <returns>An <see cref="T:System.Web.HttpStaticObjectsCollection" /> collection populated with the <see cref="P:System.Web.SessionState.HttpSessionState.StaticObjects" /> property value for the specified <see cref="T:System.Web.HttpContext" />.</returns>
		/// <param name="context">The <see cref="T:System.Web.HttpContext" /> from which to get the static objects collection.</param>
		// Token: 0x060035D2 RID: 13778 RVA: 0x0008BAC4 File Offset: 0x00089CC4
		public static HttpStaticObjectsCollection GetSessionStaticObjects(HttpContext context)
		{
			HttpSessionState session;
			if (context == null || (session = context.Session) == null)
			{
				return null;
			}
			return session.Container.StaticObjects;
		}

		/// <summary>Executes the Session_OnEnd event defined in the Global.asax file for the ASP.NET application.</summary>
		/// <param name="session">The <see cref="T:System.Web.SessionState.IHttpSessionState" /> implementation instance for the session that has ended.</param>
		/// <param name="eventSource">The event source object to supply to the Session_OnEnd event.</param>
		/// <param name="eventArgs">The <see cref="T:System.EventArgs" /> object to supply to the Session_OnEnd event.</param>
		// Token: 0x060035D3 RID: 13779 RVA: 0x0008BAEB File Offset: 0x00089CEB
		public static void RaiseSessionEnd(IHttpSessionState session, object eventSource, EventArgs eventArgs)
		{
			HttpApplicationFactory.InvokeSessionEnd(new HttpSessionState(session), eventSource, eventArgs);
		}

		/// <summary>Removes session data from the specified context.</summary>
		/// <param name="context">The <see cref="T:System.Web.HttpContext" /> from which to remove session data.</param>
		// Token: 0x060035D4 RID: 13780 RVA: 0x0008BAFA File Offset: 0x00089CFA
		public static void RemoveHttpSessionStateFromContext(HttpContext context)
		{
			if (context == null)
			{
				return;
			}
			context.SetSession(null);
		}

		/// <summary>Gets or sets a serialization surrogate selector that is used for session serialization customization.</summary>
		/// <returns>A serialization surrogate selector.</returns>
		// Token: 0x170010FA RID: 4346
		// (get) Token: 0x060035D5 RID: 13781 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x060035D6 RID: 13782 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public static ISurrogateSelector SerializationSurrogateSelector
		{
			[CompilerGenerated]
			[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[CompilerGenerated]
			[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x060035D7 RID: 13783 RVA: 0x0008BB08 File Offset: 0x00089D08
		public static bool IsSessionStateReadOnly(HttpContext context)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		// Token: 0x060035D8 RID: 13784 RVA: 0x0008BB24 File Offset: 0x00089D24
		public static bool IsSessionStateRequired(HttpContext context)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}
	}
}
