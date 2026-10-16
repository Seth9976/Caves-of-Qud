using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;
using System.Threading;
using Unity;

namespace System.Web.SessionState
{
	/// <summary>Contains session-state values as well as session-level settings for the current request.</summary>
	// Token: 0x0200048F RID: 1167
	public class HttpSessionStateContainer : IHttpSessionState
	{
		/// <summary>Creates a new <see cref="T:System.Web.SessionState.HttpSessionStateContainer" /> object and initializes it with the specified settings and values.</summary>
		/// <param name="id">A session identifier for the new session. If null, an <see cref="T:System.ArgumentException" /> is thrown.</param>
		/// <param name="sessionItems">An <see cref="T:System.Web.SessionState.ISessionStateItemCollection" /> that contains the session values for the new session-state provider.</param>
		/// <param name="staticObjects">An <see cref="T:System.Web.HttpStaticObjectsCollection" /> that specifies the objects declared by &lt;object Runat="Server" Scope="Session"/&gt; tags within the ASP.NET application file Global.asax.</param>
		/// <param name="timeout">The amount of time, in minutes, allowed between requests before the session-state provider terminates the session.</param>
		/// <param name="newSession">true to indicate the session was created with the current request; otherwise, false. </param>
		/// <param name="cookieMode">The <see cref="P:System.Web.SessionState.HttpSessionStateContainer.CookieMode" /> for the new session-state provider.</param>
		/// <param name="mode">One of the <see cref="T:System.Web.SessionState.SessionStateMode" /> values that specifies the current session-state mode. </param>
		/// <param name="isReadonly">true to indicate the session is read-only; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="id" /> is null.</exception>
		// Token: 0x060034E7 RID: 13543 RVA: 0x00088CA4 File Offset: 0x00086EA4
		public HttpSessionStateContainer(string id, ISessionStateItemCollection sessionItems, HttpStaticObjectsCollection staticObjects, int timeout, bool newSession, HttpCookieMode cookieMode, SessionStateMode mode, bool isReadonly)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			this.sessionItems = sessionItems;
			this.id = id;
			this.staticObjects = staticObjects;
			this.timeout = timeout;
			this.newSession = newSession;
			this.cookieMode = cookieMode;
			this.mode = mode;
			this.isReadOnly = isReadonly;
			this.isCookieless = cookieMode == HttpCookieMode.UseUri;
		}

		/// <summary>Gets or sets the character-set identifier for the current session.</summary>
		/// <returns>The character-set identifier for the current session.</returns>
		// Token: 0x170010C9 RID: 4297
		// (get) Token: 0x060034E8 RID: 13544 RVA: 0x00088D10 File Offset: 0x00086F10
		// (set) Token: 0x060034E9 RID: 13545 RVA: 0x00088D44 File Offset: 0x00086F44
		public int CodePage
		{
			get
			{
				HttpContext httpContext = HttpContext.Current;
				if (httpContext == null)
				{
					return Encoding.Default.CodePage;
				}
				return httpContext.Response.ContentEncoding.CodePage;
			}
			set
			{
				HttpContext httpContext = HttpContext.Current;
				if (httpContext != null)
				{
					httpContext.Response.ContentEncoding = Encoding.GetEncoding(value);
				}
			}
		}

		/// <summary>Gets a value that indicates whether the application is configured for cookieless sessions.</summary>
		/// <returns>One of the <see cref="T:System.Web.HttpCookieMode" /> values that indicates whether the application is configured for cookieless sessions. The default is <see cref="F:System.Web.HttpCookieMode.UseCookies" />.</returns>
		// Token: 0x170010CA RID: 4298
		// (get) Token: 0x060034EA RID: 13546 RVA: 0x00088D6B File Offset: 0x00086F6B
		public HttpCookieMode CookieMode
		{
			get
			{
				return this.cookieMode;
			}
		}

		/// <summary>Gets the number of items in the session-state collection.</summary>
		/// <returns>The number of items in the collection.</returns>
		// Token: 0x170010CB RID: 4299
		// (get) Token: 0x060034EB RID: 13547 RVA: 0x00088D73 File Offset: 0x00086F73
		public int Count
		{
			get
			{
				if (this.sessionItems != null)
				{
					return this.sessionItems.Count;
				}
				return 0;
			}
		}

		/// <summary>Gets a value indicating whether the current session has been abandoned.</summary>
		/// <returns>true if the current session has been abandoned; otherwise, false.</returns>
		// Token: 0x170010CC RID: 4300
		// (get) Token: 0x060034EC RID: 13548 RVA: 0x00088D8A File Offset: 0x00086F8A
		public bool IsAbandoned
		{
			get
			{
				return this.abandoned;
			}
		}

		/// <summary>Gets a value indicating whether the session ID is embedded in the URL or stored in an HTTP cookie.</summary>
		/// <returns>true if the session is embedded in the URL; otherwise, false.</returns>
		// Token: 0x170010CD RID: 4301
		// (get) Token: 0x060034ED RID: 13549 RVA: 0x00088D92 File Offset: 0x00086F92
		public bool IsCookieless
		{
			get
			{
				return this.isCookieless;
			}
		}

		/// <summary>Gets a value indicating whether the session was created with the current request.</summary>
		/// <returns>true if the session was created with the current request; otherwise, false.</returns>
		// Token: 0x170010CE RID: 4302
		// (get) Token: 0x060034EE RID: 13550 RVA: 0x00088D9A File Offset: 0x00086F9A
		public bool IsNewSession
		{
			get
			{
				return this.newSession;
			}
		}

		/// <summary>Gets a value indicating whether the session is read-only.</summary>
		/// <returns>true if the session is read-only; otherwise, false.</returns>
		// Token: 0x170010CF RID: 4303
		// (get) Token: 0x060034EF RID: 13551 RVA: 0x00088DA2 File Offset: 0x00086FA2
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection of session-state values is synchronized (thread safe).</summary>
		/// <returns>Always false, because thread-safe <see cref="T:System.Web.SessionState.HttpSessionStateContainer" /> objects are not supported.</returns>
		// Token: 0x170010D0 RID: 4304
		// (get) Token: 0x060034F0 RID: 13552 RVA: 0x00008A4D File Offset: 0x00006C4D
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010D1 RID: 4305
		object IHttpSessionState.this[int index]
		{
			get
			{
				if (this.sessionItems == null || this.sessionItems.Count == 0)
				{
					return null;
				}
				return this.sessionItems[index];
			}
			set
			{
				if (this.sessionItems != null)
				{
					this.sessionItems[index] = value;
				}
			}
		}

		// Token: 0x170010D2 RID: 4306
		object IHttpSessionState.this[string name]
		{
			get
			{
				if (this.sessionItems == null || this.sessionItems.Count == 0)
				{
					return null;
				}
				return this.sessionItems[name];
			}
			set
			{
				if (this.sessionItems != null)
				{
					this.sessionItems[name] = value;
				}
			}
		}

		// Token: 0x170010D3 RID: 4307
		// (get) Token: 0x060034F5 RID: 13557 RVA: 0x00088E22 File Offset: 0x00087022
		NameObjectCollectionBase.KeysCollection IHttpSessionState.Keys
		{
			get
			{
				if (this.sessionItems != null)
				{
					return this.sessionItems.Keys;
				}
				return null;
			}
		}

		/// <summary>Gets or sets the locale identifier (LCID) of the current session.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> instance that specifies the culture of the current session.</returns>
		// Token: 0x170010D4 RID: 4308
		// (get) Token: 0x060034F6 RID: 13558 RVA: 0x0003484F File Offset: 0x00032A4F
		// (set) Token: 0x060034F7 RID: 13559 RVA: 0x00034860 File Offset: 0x00032A60
		public int LCID
		{
			get
			{
				return Thread.CurrentThread.CurrentCulture.LCID;
			}
			set
			{
				Thread.CurrentThread.CurrentCulture = new CultureInfo(value);
			}
		}

		/// <summary>Gets the current session-state mode.</summary>
		/// <returns>One of the <see cref="T:System.Web.SessionState.SessionStateMode" /> values.</returns>
		// Token: 0x170010D5 RID: 4309
		// (get) Token: 0x060034F8 RID: 13560 RVA: 0x00088E39 File Offset: 0x00087039
		public SessionStateMode Mode
		{
			get
			{
				return this.mode;
			}
		}

		/// <summary>Gets the unique identifier for the session.</summary>
		/// <returns>The unique session identifier.</returns>
		// Token: 0x170010D6 RID: 4310
		// (get) Token: 0x060034F9 RID: 13561 RVA: 0x00088E41 File Offset: 0x00087041
		public string SessionID
		{
			get
			{
				return this.id;
			}
		}

		/// <summary>Gets a collection of objects declared by &lt;object Runat="Server" Scope="Session"/&gt; tags within the ASP.NET application file Global.asax.</summary>
		/// <returns>An <see cref="T:System.Web.HttpStaticObjectsCollection" /> containing objects declared in the Global.asax file.</returns>
		// Token: 0x170010D7 RID: 4311
		// (get) Token: 0x060034FA RID: 13562 RVA: 0x00088E49 File Offset: 0x00087049
		public HttpStaticObjectsCollection StaticObjects
		{
			get
			{
				return this.staticObjects;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection of session-state values.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x170010D8 RID: 4312
		// (get) Token: 0x060034FB RID: 13563 RVA: 0x00002058 File Offset: 0x00000258
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets and sets the amount of time, in minutes, allowed between requests before the session-state provider terminates the session.</summary>
		/// <returns>The time-out period, in minutes.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt was made to set the <see cref="P:System.Web.SessionState.HttpSessionStateContainer.Timeout" /> value to an integer value less than 1.- or -An attempt was made to set the <see cref="P:System.Web.SessionState.HttpSessionStateContainer.Timeout" /> value to an integer value greater than the maximum allowed when <see cref="P:System.Web.SessionState.HttpSessionState.Mode" /> is set to <see cref="F:System.Web.SessionState.SessionStateMode.InProc" /> or <see cref="F:System.Web.SessionState.SessionStateMode.StateServer" />. The maximum allowed is 525,600 (one year). </exception>
		// Token: 0x170010D9 RID: 4313
		// (get) Token: 0x060034FC RID: 13564 RVA: 0x00088E51 File Offset: 0x00087051
		// (set) Token: 0x060034FD RID: 13565 RVA: 0x00088E59 File Offset: 0x00087059
		public int Timeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentException("The argument to SetTimeout must be greater than 0.");
				}
				this.timeout = value;
			}
		}

		// Token: 0x060034FE RID: 13566 RVA: 0x00088E71 File Offset: 0x00087071
		internal void SetNewSession(bool value)
		{
			this.newSession = value;
		}

		/// <summary>Marks the current session as abandoned.</summary>
		// Token: 0x060034FF RID: 13567 RVA: 0x00088E7A File Offset: 0x0008707A
		public void Abandon()
		{
			this.abandoned = true;
		}

		/// <summary>Adds a new item to the session-state collection.</summary>
		/// <param name="name">The name of the item to add to the session-state collection. </param>
		/// <param name="value">The value of the item to add to the session-state collection. </param>
		// Token: 0x06003500 RID: 13568 RVA: 0x00088E83 File Offset: 0x00087083
		public void Add(string name, object value)
		{
			if (this.sessionItems == null)
			{
				return;
			}
			this.sessionItems[name] = value;
		}

		/// <summary>Removes all values and keys from the session-state collection.</summary>
		// Token: 0x06003501 RID: 13569 RVA: 0x00088E9B File Offset: 0x0008709B
		public void Clear()
		{
			if (this.sessionItems == null)
			{
				return;
			}
			this.sessionItems.Clear();
		}

		/// <summary>Copies the collection of session-state values to a one-dimensional array, starting at the specified index in the array.</summary>
		/// <param name="array">The <see cref="T:System.Array" /> that receives the session values. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> from which copying starts. </param>
		// Token: 0x06003502 RID: 13570 RVA: 0x00088EB4 File Offset: 0x000870B4
		public void CopyTo(Array array, int index)
		{
			if (this.sessionItems == null)
			{
				return;
			}
			NameObjectCollectionBase.KeysCollection keys = this.sessionItems.Keys;
			for (int i = 0; i < keys.Count; i++)
			{
				array.SetValue(keys.Get(i), i + index);
			}
		}

		/// <summary>Returns an enumerator that can be used to read all the session-state variable names in the current session.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can iterate through the variable names in the session-state collection.</returns>
		// Token: 0x06003503 RID: 13571 RVA: 0x00088EF7 File Offset: 0x000870F7
		public IEnumerator GetEnumerator()
		{
			if (this.sessionItems == null)
			{
				return null;
			}
			return this.sessionItems.GetEnumerator();
		}

		/// <summary>Deletes an item from the session-state collection.</summary>
		/// <param name="name">The name of the item to delete from the session-state collection. </param>
		// Token: 0x06003504 RID: 13572 RVA: 0x00088F0E File Offset: 0x0008710E
		public void Remove(string name)
		{
			if (this.sessionItems == null)
			{
				return;
			}
			this.sessionItems.Remove(name);
		}

		/// <summary>Clears all session-state values.</summary>
		// Token: 0x06003505 RID: 13573 RVA: 0x00088E9B File Offset: 0x0008709B
		public void RemoveAll()
		{
			if (this.sessionItems == null)
			{
				return;
			}
			this.sessionItems.Clear();
		}

		/// <summary>Deletes an item at a specified index from the session-state collection.</summary>
		/// <param name="index">The index of the item to remove from the session-state collection. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.- or -<paramref name="index" /> is equal to or greater than <see cref="P:System.Web.SessionState.HttpSessionStateContainer.Count" />.</exception>
		// Token: 0x06003506 RID: 13574 RVA: 0x00088F25 File Offset: 0x00087125
		public void RemoveAt(int index)
		{
			if (this.sessionItems == null)
			{
				return;
			}
			this.sessionItems.RemoveAt(index);
		}

		// Token: 0x06003507 RID: 13575 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public object get_Item(int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06003508 RID: 13576 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void set_Item(int index, object value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets a session value by name.</summary>
		/// <returns>The session-state value with the specified name.</returns>
		/// <param name="name">The key name of the session value. </param>
		// Token: 0x170010DA RID: 4314
		// (get) Token: 0x06003509 RID: 13577 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x0600350A RID: 13578 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public object Item
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a collection of the keys for all values stored in the session-state collection.</summary>
		/// <returns>The <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> that contains all the session keys.</returns>
		// Token: 0x170010DB RID: 4315
		// (get) Token: 0x0600350B RID: 13579 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x04001D43 RID: 7491
		private string id;

		// Token: 0x04001D44 RID: 7492
		private HttpStaticObjectsCollection staticObjects;

		// Token: 0x04001D45 RID: 7493
		private int timeout;

		// Token: 0x04001D46 RID: 7494
		private bool newSession;

		// Token: 0x04001D47 RID: 7495
		private bool isCookieless;

		// Token: 0x04001D48 RID: 7496
		private SessionStateMode mode;

		// Token: 0x04001D49 RID: 7497
		private bool isReadOnly;

		// Token: 0x04001D4A RID: 7498
		internal bool abandoned;

		// Token: 0x04001D4B RID: 7499
		private ISessionStateItemCollection sessionItems;

		// Token: 0x04001D4C RID: 7500
		private HttpCookieMode cookieMode;
	}
}
