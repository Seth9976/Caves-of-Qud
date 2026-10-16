using System;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.StorePermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x0200029C RID: 668
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class StorePermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.StorePermissionAttribute" /> class with the specified security action.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x0600150E RID: 5390 RVA: 0x00055535 File Offset: 0x00053735
		public StorePermissionAttribute(SecurityAction action)
			: base(action)
		{
			this._flags = StorePermissionFlags.NoFlags;
		}

		/// <summary>Gets or sets the store permissions.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.StorePermissionFlags" /> values. The default is <see cref="F:System.Security.Permissions.StorePermissionFlags.NoFlags" />.</returns>
		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x0600150F RID: 5391 RVA: 0x00055545 File Offset: 0x00053745
		// (set) Token: 0x06001510 RID: 5392 RVA: 0x0005554D File Offset: 0x0005374D
		public StorePermissionFlags Flags
		{
			get
			{
				return this._flags;
			}
			set
			{
				if ((value & StorePermissionFlags.AllFlags) != value)
				{
					throw new ArgumentException(string.Format(global::Locale.GetText("Invalid flags {0}"), value), "StorePermissionFlags");
				}
				this._flags = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the code is permitted to add to a store.</summary>
		/// <returns>true if the ability to add to a store is allowed; otherwise, false.</returns>
		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06001511 RID: 5393 RVA: 0x00055580 File Offset: 0x00053780
		// (set) Token: 0x06001512 RID: 5394 RVA: 0x0005558E File Offset: 0x0005378E
		public bool AddToStore
		{
			get
			{
				return (this._flags & StorePermissionFlags.AddToStore) > StorePermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= StorePermissionFlags.AddToStore;
					return;
				}
				this._flags &= ~StorePermissionFlags.AddToStore;
			}
		}

		/// <summary>Gets or sets a value indicating whether the code is permitted to create a store.</summary>
		/// <returns>true if the ability to create a store is allowed; otherwise, false.</returns>
		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001513 RID: 5395 RVA: 0x000555B2 File Offset: 0x000537B2
		// (set) Token: 0x06001514 RID: 5396 RVA: 0x000555BF File Offset: 0x000537BF
		public bool CreateStore
		{
			get
			{
				return (this._flags & StorePermissionFlags.CreateStore) > StorePermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= StorePermissionFlags.CreateStore;
					return;
				}
				this._flags &= ~StorePermissionFlags.CreateStore;
			}
		}

		/// <summary>Gets or sets a value indicating whether the code is permitted to delete a store.</summary>
		/// <returns>true if the ability to delete a store is allowed; otherwise, false.</returns>
		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06001515 RID: 5397 RVA: 0x000555E2 File Offset: 0x000537E2
		// (set) Token: 0x06001516 RID: 5398 RVA: 0x000555EF File Offset: 0x000537EF
		public bool DeleteStore
		{
			get
			{
				return (this._flags & StorePermissionFlags.DeleteStore) > StorePermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= StorePermissionFlags.DeleteStore;
					return;
				}
				this._flags &= ~StorePermissionFlags.DeleteStore;
			}
		}

		/// <summary>Gets or sets a value indicating whether the code is permitted to enumerate the certificates in a store.</summary>
		/// <returns>true if the ability to enumerate certificates is allowed; otherwise, false.</returns>
		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06001517 RID: 5399 RVA: 0x00055612 File Offset: 0x00053812
		// (set) Token: 0x06001518 RID: 5400 RVA: 0x00055623 File Offset: 0x00053823
		public bool EnumerateCertificates
		{
			get
			{
				return (this._flags & StorePermissionFlags.EnumerateCertificates) > StorePermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= StorePermissionFlags.EnumerateCertificates;
					return;
				}
				this._flags &= ~StorePermissionFlags.EnumerateCertificates;
			}
		}

		/// <summary>Gets or sets a value indicating whether the code is permitted to enumerate stores.</summary>
		/// <returns>true if the ability to enumerate stores is allowed; otherwise, false.</returns>
		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06001519 RID: 5401 RVA: 0x0005564D File Offset: 0x0005384D
		// (set) Token: 0x0600151A RID: 5402 RVA: 0x0005565A File Offset: 0x0005385A
		public bool EnumerateStores
		{
			get
			{
				return (this._flags & StorePermissionFlags.EnumerateStores) > StorePermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= StorePermissionFlags.EnumerateStores;
					return;
				}
				this._flags &= ~StorePermissionFlags.EnumerateStores;
			}
		}

		/// <summary>Gets or sets a value indicating whether the code is permitted to open a store.</summary>
		/// <returns>true if the ability to open a store is allowed; otherwise, false.</returns>
		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x0600151B RID: 5403 RVA: 0x0005567D File Offset: 0x0005387D
		// (set) Token: 0x0600151C RID: 5404 RVA: 0x0005568B File Offset: 0x0005388B
		public bool OpenStore
		{
			get
			{
				return (this._flags & StorePermissionFlags.OpenStore) > StorePermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= StorePermissionFlags.OpenStore;
					return;
				}
				this._flags &= ~StorePermissionFlags.OpenStore;
			}
		}

		/// <summary>Gets or sets a value indicating whether the code is permitted to remove a certificate from a store.</summary>
		/// <returns>true if the ability to remove a certificate from a store is allowed; otherwise, false.</returns>
		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x0600151D RID: 5405 RVA: 0x000556AF File Offset: 0x000538AF
		// (set) Token: 0x0600151E RID: 5406 RVA: 0x000556BD File Offset: 0x000538BD
		public bool RemoveFromStore
		{
			get
			{
				return (this._flags & StorePermissionFlags.RemoveFromStore) > StorePermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= StorePermissionFlags.RemoveFromStore;
					return;
				}
				this._flags &= ~StorePermissionFlags.RemoveFromStore;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.StorePermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.StorePermission" /> that corresponds to the attribute.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600151F RID: 5407 RVA: 0x000556E4 File Offset: 0x000538E4
		public override IPermission CreatePermission()
		{
			StorePermission storePermission;
			if (base.Unrestricted)
			{
				storePermission = new StorePermission(PermissionState.Unrestricted);
			}
			else
			{
				storePermission = new StorePermission(this._flags);
			}
			return storePermission;
		}

		// Token: 0x04000BE7 RID: 3047
		private StorePermissionFlags _flags;
	}
}
