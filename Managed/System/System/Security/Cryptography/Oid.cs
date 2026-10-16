using System;
using Internal.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Represents a cryptographic object identifier. This class cannot be inherited.</summary>
	// Token: 0x020002B8 RID: 696
	public sealed class Oid
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Oid" /> class.</summary>
		// Token: 0x060015CC RID: 5580 RVA: 0x0000219B File Offset: 0x0000039B
		public Oid()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Oid" /> class using a string value of an <see cref="T:System.Security.Cryptography.Oid" /> object.</summary>
		/// <param name="oid">An object identifier.</param>
		// Token: 0x060015CD RID: 5581 RVA: 0x000575C8 File Offset: 0x000557C8
		public Oid(string oid)
		{
			string text = OidLookup.ToOid(oid, OidGroup.All, false);
			if (text == null)
			{
				text = oid;
			}
			this.Value = text;
			this._group = OidGroup.All;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Oid" /> class using the specified value and friendly name.</summary>
		/// <param name="value">The dotted number of the identifier.</param>
		/// <param name="friendlyName">The friendly name of the identifier.</param>
		// Token: 0x060015CE RID: 5582 RVA: 0x000575F7 File Offset: 0x000557F7
		public Oid(string value, string friendlyName)
		{
			this._value = value;
			this._friendlyName = friendlyName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Oid" /> class using the specified <see cref="T:System.Security.Cryptography.Oid" /> object.</summary>
		/// <param name="oid">The object identifier information to use to create the new object identifier.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="oid " />is null.</exception>
		// Token: 0x060015CF RID: 5583 RVA: 0x0005760D File Offset: 0x0005580D
		public Oid(Oid oid)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			this._value = oid._value;
			this._friendlyName = oid._friendlyName;
			this._group = oid._group;
		}

		/// <summary>Creates an <see cref="T:System.Security.Cryptography.Oid" /> object from an OID friendly name by searching the specified group.</summary>
		/// <returns>An object that represents the specified OID.</returns>
		/// <param name="friendlyName">The friendly name of the identifier.</param>
		/// <param name="group">The group to search in.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="friendlyName " /> is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The OID was not found.</exception>
		// Token: 0x060015D0 RID: 5584 RVA: 0x00057647 File Offset: 0x00055847
		public static Oid FromFriendlyName(string friendlyName, OidGroup group)
		{
			if (friendlyName == null)
			{
				throw new ArgumentNullException("friendlyName");
			}
			string text = OidLookup.ToOid(friendlyName, group, false);
			if (text == null)
			{
				throw new CryptographicException("No OID value matches this name.");
			}
			return new Oid(text, friendlyName, group);
		}

		/// <summary>Creates an <see cref="T:System.Security.Cryptography.Oid" /> object by using the specified OID value and group.</summary>
		/// <returns>A new instance of an <see cref="T:System.Security.Cryptography.Oid" /> object.</returns>
		/// <param name="oidValue">The OID value.</param>
		/// <param name="group">The group to search in.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="oidValue" /> is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The friendly name for the OID value was not found.</exception>
		// Token: 0x060015D1 RID: 5585 RVA: 0x00057674 File Offset: 0x00055874
		public static Oid FromOidValue(string oidValue, OidGroup group)
		{
			if (oidValue == null)
			{
				throw new ArgumentNullException("oidValue");
			}
			string text = OidLookup.ToFriendlyName(oidValue, group, false);
			if (text == null)
			{
				throw new CryptographicException("The OID value is invalid.");
			}
			return new Oid(oidValue, text, group);
		}

		/// <summary>Gets or sets the dotted number of the identifier.</summary>
		/// <returns>The dotted number of the identifier.</returns>
		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x060015D2 RID: 5586 RVA: 0x000576AE File Offset: 0x000558AE
		// (set) Token: 0x060015D3 RID: 5587 RVA: 0x000576B6 File Offset: 0x000558B6
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		/// <summary>Gets or sets the friendly name of the identifier.</summary>
		/// <returns>The friendly name of the identifier.</returns>
		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x060015D4 RID: 5588 RVA: 0x000576BF File Offset: 0x000558BF
		// (set) Token: 0x060015D5 RID: 5589 RVA: 0x000576F0 File Offset: 0x000558F0
		public string FriendlyName
		{
			get
			{
				if (this._friendlyName == null && this._value != null)
				{
					this._friendlyName = OidLookup.ToFriendlyName(this._value, this._group, true);
				}
				return this._friendlyName;
			}
			set
			{
				this._friendlyName = value;
				if (this._friendlyName != null)
				{
					string text = OidLookup.ToOid(this._friendlyName, this._group, true);
					if (text != null)
					{
						this._value = text;
					}
				}
			}
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x00057729 File Offset: 0x00055929
		private Oid(string value, string friendlyName, OidGroup group)
		{
			this._value = value;
			this._friendlyName = friendlyName;
			this._group = group;
		}

		// Token: 0x04000C54 RID: 3156
		private string _value;

		// Token: 0x04000C55 RID: 3157
		private string _friendlyName;

		// Token: 0x04000C56 RID: 3158
		private OidGroup _group;
	}
}
