using System;
using System.Collections;
using System.Collections.Specialized;
using System.Security.Permissions;
using Unity;

namespace System.Web.SessionState
{
	/// <summary>Provides access to session-state values as well as session-level settings and lifetime management methods.</summary>
	// Token: 0x0200048E RID: 1166
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HttpSessionState : ICollection, IEnumerable
	{
		// Token: 0x060034C6 RID: 13510 RVA: 0x00088B0F File Offset: 0x00086D0F
		internal HttpSessionState(IHttpSessionState container)
		{
			this.container = container;
		}

		// Token: 0x170010B7 RID: 4279
		// (get) Token: 0x060034C7 RID: 13511 RVA: 0x00088B1E File Offset: 0x00086D1E
		internal IHttpSessionState Container
		{
			get
			{
				return this.container;
			}
		}

		/// <summary>Gets or sets the character-set identifier for the current session.</summary>
		/// <returns>The character-set identifier for the current session.</returns>
		// Token: 0x170010B8 RID: 4280
		// (get) Token: 0x060034C8 RID: 13512 RVA: 0x00088B26 File Offset: 0x00086D26
		// (set) Token: 0x060034C9 RID: 13513 RVA: 0x00088B33 File Offset: 0x00086D33
		public int CodePage
		{
			get
			{
				return this.container.CodePage;
			}
			set
			{
				this.container.CodePage = value;
			}
		}

		/// <summary>Gets a reference to the current session-state object.</summary>
		/// <returns>The current <see cref="T:System.Web.SessionState.HttpSessionState" />.</returns>
		// Token: 0x170010B9 RID: 4281
		// (get) Token: 0x060034CA RID: 13514 RVA: 0x00002058 File Offset: 0x00000258
		public HttpSessionState Contents
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets a value that indicates whether the application is configured for cookieless sessions.</summary>
		/// <returns>One of the <see cref="T:System.Web.HttpCookieMode" /> values that indicate whether the application is configured for cookieless sessions. The default is <see cref="F:System.Web.HttpCookieMode.UseCookies" />.</returns>
		// Token: 0x170010BA RID: 4282
		// (get) Token: 0x060034CB RID: 13515 RVA: 0x00088B41 File Offset: 0x00086D41
		public HttpCookieMode CookieMode
		{
			get
			{
				if (this.IsCookieless)
				{
					return HttpCookieMode.UseUri;
				}
				return HttpCookieMode.UseCookies;
			}
		}

		/// <summary>Gets the number of items in the session-state collection.</summary>
		/// <returns>The number of items in the collection.</returns>
		// Token: 0x170010BB RID: 4283
		// (get) Token: 0x060034CC RID: 13516 RVA: 0x00088B4E File Offset: 0x00086D4E
		public int Count
		{
			get
			{
				return this.container.Count;
			}
		}

		/// <summary>Gets a value indicating whether the session ID is embedded in the URL or stored in an HTTP cookie.</summary>
		/// <returns>true if the session is embedded in the URL; otherwise, false.</returns>
		// Token: 0x170010BC RID: 4284
		// (get) Token: 0x060034CD RID: 13517 RVA: 0x00088B5B File Offset: 0x00086D5B
		public bool IsCookieless
		{
			get
			{
				return this.container.IsCookieless;
			}
		}

		/// <summary>Gets a value indicating whether the session was created with the current request.</summary>
		/// <returns>true if the session was created with the current request; otherwise, false.</returns>
		// Token: 0x170010BD RID: 4285
		// (get) Token: 0x060034CE RID: 13518 RVA: 0x00088B68 File Offset: 0x00086D68
		public bool IsNewSession
		{
			get
			{
				return this.container.IsNewSession;
			}
		}

		/// <summary>Gets a value indicating whether the session is read-only.</summary>
		/// <returns>true if the session is read-only; otherwise, false.</returns>
		// Token: 0x170010BE RID: 4286
		// (get) Token: 0x060034CF RID: 13519 RVA: 0x00088B75 File Offset: 0x00086D75
		public bool IsReadOnly
		{
			get
			{
				return this.container.IsReadOnly;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection of session-state values is synchronized (thread safe).</summary>
		/// <returns>true if access to the collection is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x170010BF RID: 4287
		// (get) Token: 0x060034D0 RID: 13520 RVA: 0x00088B82 File Offset: 0x00086D82
		public bool IsSynchronized
		{
			get
			{
				return this.container.IsSynchronized;
			}
		}

		/// <summary>Gets or sets a session value by name.</summary>
		/// <returns>The session-state value with the specified name, or null if the item does not exist.</returns>
		/// <param name="name">The key name of the session value. </param>
		// Token: 0x170010C0 RID: 4288
		public object this[string name]
		{
			get
			{
				return this.container[name];
			}
			set
			{
				this.container[name] = value;
			}
		}

		/// <summary>Gets or sets a session value by numerical index.</summary>
		/// <returns>The session-state value stored at the specified index, or null if the item does not exist.</returns>
		/// <param name="index">The numerical index of the session value. </param>
		// Token: 0x170010C1 RID: 4289
		public object this[int index]
		{
			get
			{
				return this.container[index];
			}
			set
			{
				this.container[index] = value;
			}
		}

		/// <summary>Gets a collection of the keys for all values stored in the session-state collection.</summary>
		/// <returns>The <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> that contains all the session keys.</returns>
		// Token: 0x170010C2 RID: 4290
		// (get) Token: 0x060034D5 RID: 13525 RVA: 0x00088BC9 File Offset: 0x00086DC9
		public NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				return this.container.Keys;
			}
		}

		/// <summary>Gets or sets the locale identifier (LCID) of the current session.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> instance that specifies the culture of the current session.</returns>
		// Token: 0x170010C3 RID: 4291
		// (get) Token: 0x060034D6 RID: 13526 RVA: 0x00088BD6 File Offset: 0x00086DD6
		// (set) Token: 0x060034D7 RID: 13527 RVA: 0x00088BE3 File Offset: 0x00086DE3
		public int LCID
		{
			get
			{
				return this.container.LCID;
			}
			set
			{
				this.container.LCID = value;
			}
		}

		/// <summary>Gets the current session-state mode.</summary>
		/// <returns>One of the <see cref="T:System.Web.SessionState.SessionStateMode" /> values.</returns>
		// Token: 0x170010C4 RID: 4292
		// (get) Token: 0x060034D8 RID: 13528 RVA: 0x00088BF1 File Offset: 0x00086DF1
		public SessionStateMode Mode
		{
			get
			{
				return this.container.Mode;
			}
		}

		/// <summary>Gets the unique identifier for the session.</summary>
		/// <returns>The unique session identifier.</returns>
		// Token: 0x170010C5 RID: 4293
		// (get) Token: 0x060034D9 RID: 13529 RVA: 0x00088BFE File Offset: 0x00086DFE
		public string SessionID
		{
			get
			{
				return this.container.SessionID;
			}
		}

		/// <summary>Gets a collection of objects declared by &lt;object Runat="Server" Scope="Session"/&gt; tags within the ASP.NET application file Global.asax.</summary>
		/// <returns>An <see cref="T:System.Web.HttpStaticObjectsCollection" /> containing objects declared in the Global.asax file.</returns>
		// Token: 0x170010C6 RID: 4294
		// (get) Token: 0x060034DA RID: 13530 RVA: 0x00088C0B File Offset: 0x00086E0B
		public HttpStaticObjectsCollection StaticObjects
		{
			get
			{
				return this.container.StaticObjects;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection of session-state values.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x170010C7 RID: 4295
		// (get) Token: 0x060034DB RID: 13531 RVA: 0x00088C18 File Offset: 0x00086E18
		public object SyncRoot
		{
			get
			{
				return this.container.SyncRoot;
			}
		}

		/// <summary>Gets and sets the amount of time, in minutes, allowed between requests before the session-state provider terminates the session.</summary>
		/// <returns>The time-out period, in minutes.</returns>
		// Token: 0x170010C8 RID: 4296
		// (get) Token: 0x060034DC RID: 13532 RVA: 0x00088C25 File Offset: 0x00086E25
		// (set) Token: 0x060034DD RID: 13533 RVA: 0x00088C32 File Offset: 0x00086E32
		public int Timeout
		{
			get
			{
				return this.container.Timeout;
			}
			set
			{
				this.container.Timeout = value;
			}
		}

		/// <summary>Cancels the current session.</summary>
		// Token: 0x060034DE RID: 13534 RVA: 0x00088C40 File Offset: 0x00086E40
		public void Abandon()
		{
			this.container.Abandon();
		}

		/// <summary>Adds a new item to the session-state collection.</summary>
		/// <param name="name">The name of the item to add to the session-state collection. </param>
		/// <param name="value">The value of the item to add to the session-state collection. </param>
		// Token: 0x060034DF RID: 13535 RVA: 0x00088C4D File Offset: 0x00086E4D
		public void Add(string name, object value)
		{
			this.container.Add(name, value);
		}

		/// <summary>Removes all keys and values from the session-state collection.</summary>
		// Token: 0x060034E0 RID: 13536 RVA: 0x00088C5C File Offset: 0x00086E5C
		public void Clear()
		{
			this.container.Clear();
		}

		/// <summary>Copies the collection of session-state values to a one-dimensional array, starting at the specified index in the array.</summary>
		/// <param name="array">The <see cref="T:System.Array" /> that receives the session values. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> from which copying starts. </param>
		// Token: 0x060034E1 RID: 13537 RVA: 0x00088C69 File Offset: 0x00086E69
		public void CopyTo(Array array, int index)
		{
			this.container.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that can be used to read all the session-state variable names in the current session.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can iterate through the variable names in the session-state collection.</returns>
		// Token: 0x060034E2 RID: 13538 RVA: 0x00088C78 File Offset: 0x00086E78
		public IEnumerator GetEnumerator()
		{
			return this.container.GetEnumerator();
		}

		/// <summary>Deletes an item from the session-state collection.</summary>
		/// <param name="name">The name of the item to delete from the session-state collection. </param>
		// Token: 0x060034E3 RID: 13539 RVA: 0x00088C85 File Offset: 0x00086E85
		public void Remove(string name)
		{
			this.container.Remove(name);
		}

		/// <summary>Removes all keys and values from the session-state collection.</summary>
		// Token: 0x060034E4 RID: 13540 RVA: 0x00088C5C File Offset: 0x00086E5C
		public void RemoveAll()
		{
			this.container.Clear();
		}

		/// <summary>Deletes an item at a specified index from the session-state collection.</summary>
		/// <param name="index">The index of the item to remove from the session-state collection. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.- or -<paramref name="index" /> is equal to or greater than <see cref="P:System.Web.SessionState.HttpSessionState.Count" />.</exception>
		// Token: 0x060034E5 RID: 13541 RVA: 0x00088C93 File Offset: 0x00086E93
		public void RemoveAt(int index)
		{
			this.container.RemoveAt(index);
		}

		// Token: 0x060034E6 RID: 13542 RVA: 0x0000B3D8 File Offset: 0x000095D8
		internal HttpSessionState()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001D42 RID: 7490
		private IHttpSessionState container;
	}
}
