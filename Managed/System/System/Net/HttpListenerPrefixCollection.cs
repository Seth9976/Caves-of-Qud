using System;
using System.Collections;
using System.Collections.Generic;
using Unity;

namespace System.Net
{
	/// <summary>Represents the collection used to store Uniform Resource Identifier (URI) prefixes for <see cref="T:System.Net.HttpListener" /> objects.</summary>
	// Token: 0x0200068F RID: 1679
	public class HttpListenerPrefixCollection : ICollection<string>, IEnumerable<string>, IEnumerable
	{
		// Token: 0x060034F7 RID: 13559 RVA: 0x000B9128 File Offset: 0x000B7328
		internal HttpListenerPrefixCollection(HttpListener listener)
		{
			this.prefixes = new List<string>();
			base..ctor();
			this.listener = listener;
		}

		/// <summary>Gets the number of prefixes contained in the collection.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the number of prefixes in this collection. </returns>
		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x060034F8 RID: 13560 RVA: 0x000B9142 File Offset: 0x000B7342
		public int Count
		{
			get
			{
				return this.prefixes.Count;
			}
		}

		/// <summary>Gets a value that indicates whether access to the collection is read-only.</summary>
		/// <returns>Always returns false.</returns>
		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x060034F9 RID: 13561 RVA: 0x00003062 File Offset: 0x00001262
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether access to the collection is synchronized (thread-safe).</summary>
		/// <returns>This property always returns false.</returns>
		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x060034FA RID: 13562 RVA: 0x00003062 File Offset: 0x00001262
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Adds a Uniform Resource Identifier (URI) prefix to the collection.</summary>
		/// <param name="uriPrefix">A <see cref="T:System.String" /> that identifies the URI information that is compared in incoming requests. The prefix must be terminated with a forward slash ("/").</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uriPrefix" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="uriPrefix" /> does not use the http:// or https:// scheme. These are the only schemes supported for <see cref="T:System.Net.HttpListener" /> objects. -or-<paramref name="uriPrefix" /> is not a correctly formatted URI prefix. Make sure the string is terminated with a "/".</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.HttpListener" /> associated with this collection is closed.</exception>
		/// <exception cref="T:System.Net.HttpListenerException">A Windows function call failed. Check the exception's <see cref="P:System.Net.HttpListenerException.ErrorCode" /> property to determine the cause of the exception. This exception is thrown if another <see cref="T:System.Net.HttpListener" /> has already added the prefix <paramref name="uriPrefix" />.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060034FB RID: 13563 RVA: 0x000B9150 File Offset: 0x000B7350
		public void Add(string uriPrefix)
		{
			this.listener.CheckDisposed();
			ListenerPrefix.CheckUri(uriPrefix);
			if (this.prefixes.Contains(uriPrefix))
			{
				return;
			}
			this.prefixes.Add(uriPrefix);
			if (this.listener.IsListening)
			{
				EndPointManager.AddPrefix(uriPrefix, this.listener);
			}
		}

		/// <summary>Removes all the Uniform Resource Identifier (URI) prefixes from the collection.</summary>
		/// <exception cref="T:System.Net.HttpListenerException">A Windows function call failed. Check the exception's <see cref="P:System.Net.HttpListenerException.ErrorCode" /> property to determine the cause of the exception.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.HttpListener" /> associated with this collection is closed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060034FC RID: 13564 RVA: 0x000B91A2 File Offset: 0x000B73A2
		public void Clear()
		{
			this.listener.CheckDisposed();
			this.prefixes.Clear();
			if (this.listener.IsListening)
			{
				EndPointManager.RemoveListener(this.listener);
			}
		}

		/// <summary>Returns a <see cref="T:System.Boolean" /> value that indicates whether the specified prefix is contained in the collection.</summary>
		/// <returns>true if this collection contains the prefix specified by <paramref name="uriPrefix" />; otherwise, false.</returns>
		/// <param name="uriPrefix">A <see cref="T:System.String" /> that contains the Uniform Resource Identifier (URI) prefix to test.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uriPrefix" /> is null.</exception>
		// Token: 0x060034FD RID: 13565 RVA: 0x000B91D2 File Offset: 0x000B73D2
		public bool Contains(string uriPrefix)
		{
			this.listener.CheckDisposed();
			return this.prefixes.Contains(uriPrefix);
		}

		/// <summary>Copies the contents of an <see cref="T:System.Net.HttpListenerPrefixCollection" /> to the specified string array. </summary>
		/// <param name="array">The one dimensional string array that receives the Uniform Resource Identifier (URI) prefix strings in this collection.</param>
		/// <param name="offset">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> has more than one dimension.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">This collection contains more elements than can be stored in <paramref name="array" /> starting at <paramref name="offset" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.HttpListener" /> associated with this collection is closed.</exception>
		// Token: 0x060034FE RID: 13566 RVA: 0x000B91EB File Offset: 0x000B73EB
		public void CopyTo(string[] array, int offset)
		{
			this.listener.CheckDisposed();
			this.prefixes.CopyTo(array, offset);
		}

		/// <summary>Copies the contents of an <see cref="T:System.Net.HttpListenerPrefixCollection" /> to the specified array. </summary>
		/// <param name="array">The one dimensional <see cref="T:System.Array" /> that receives the Uniform Resource Identifier (URI) prefix strings in this collection.</param>
		/// <param name="offset">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> has more than one dimension.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">This collection contains more elements than can be stored in <paramref name="array" /> starting at <paramref name="offset" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.HttpListener" /> associated with this collection is closed.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="array" /> cannot store string values.</exception>
		// Token: 0x060034FF RID: 13567 RVA: 0x000B9205 File Offset: 0x000B7405
		public void CopyTo(Array array, int offset)
		{
			this.listener.CheckDisposed();
			((ICollection)this.prefixes).CopyTo(array, offset);
		}

		/// <summary>Returns an object that can be used to iterate through the collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface and provides access to the strings in this collection.</returns>
		// Token: 0x06003500 RID: 13568 RVA: 0x000B921F File Offset: 0x000B741F
		public IEnumerator<string> GetEnumerator()
		{
			return this.prefixes.GetEnumerator();
		}

		/// <summary>Returns an object that can be used to iterate through the collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface and provides access to the strings in this collection.</returns>
		// Token: 0x06003501 RID: 13569 RVA: 0x000B921F File Offset: 0x000B741F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.prefixes.GetEnumerator();
		}

		/// <summary>Removes the specified Uniform Resource Identifier (URI) from the list of prefixes handled by the <see cref="T:System.Net.HttpListener" /> object.</summary>
		/// <returns>true if the <paramref name="uriPrefix" /> was found in the <see cref="T:System.Net.HttpListenerPrefixCollection" /> and removed; otherwise false.</returns>
		/// <param name="uriPrefix">A <see cref="T:System.String" /> that contains the URI prefix to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uriPrefix" /> is null.</exception>
		/// <exception cref="T:System.Net.HttpListenerException">A Windows function call failed. To determine the cause of the exception, check the exception's error code.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.HttpListener" /> associated with this collection is closed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003502 RID: 13570 RVA: 0x000B9234 File Offset: 0x000B7434
		public bool Remove(string uriPrefix)
		{
			this.listener.CheckDisposed();
			if (uriPrefix == null)
			{
				throw new ArgumentNullException("uriPrefix");
			}
			bool flag = this.prefixes.Remove(uriPrefix);
			if (flag && this.listener.IsListening)
			{
				EndPointManager.RemovePrefix(uriPrefix, this.listener);
			}
			return flag;
		}

		// Token: 0x06003503 RID: 13571 RVA: 0x00013BAE File Offset: 0x00011DAE
		internal HttpListenerPrefixCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001F09 RID: 7945
		private List<string> prefixes;

		// Token: 0x04001F0A RID: 7946
		private HttpListener listener;
	}
}
