using System;

namespace System.Web.UI
{
	/// <summary>Defines an attribute that is used by ASP.NET server controls to indicate at design time whether nested content that is contained within a server control corresponds to controls or to properties of the server control. This class cannot be inherited.</summary>
	// Token: 0x0200018C RID: 396
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class PersistChildrenAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PersistChildrenAttribute" /> class using a Boolean value indicating whether to persist nested content as nested controls. </summary>
		/// <param name="persist">true to persist the nested content as nested controls; otherwise, false. </param>
		// Token: 0x06000F79 RID: 3961 RVA: 0x00028B5F File Offset: 0x00026D5F
		public PersistChildrenAttribute(bool persist)
		{
			this._persist = persist;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PersistChildrenAttribute" /> class using two Boolean values. One indicating whether to persist nested content as nested controls and the other indicating whether to use a custom persistence method.</summary>
		/// <param name="persist">true to persist nested content as nested controls; otherwise, false.</param>
		/// <param name="usesCustomPersistence">true to use customized persistence; otherwise, false.</param>
		// Token: 0x06000F7A RID: 3962 RVA: 0x00028B6E File Offset: 0x00026D6E
		public PersistChildrenAttribute(bool persist, bool usesCustomPersistence)
			: this(persist)
		{
			this._usesCustomPersistence = usesCustomPersistence;
		}

		/// <summary>Gets a value that indicates whether the nested content is persisted as nested controls at design time.</summary>
		/// <returns>true to persist nested content as nested controls; otherwise, false. The default is true.</returns>
		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06000F7B RID: 3963 RVA: 0x00028B7E File Offset: 0x00026D7E
		public bool Persist
		{
			get
			{
				return this._persist;
			}
		}

		/// <summary>Gets a value indicating whether the server control provides custom persistence of nested controls at design time. </summary>
		/// <returns>true to provide custom persistence of nested content; otherwise, false. The default is false.</returns>
		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x00028B86 File Offset: 0x00026D86
		public bool UsesCustomPersistence
		{
			get
			{
				return !this._persist && this._usesCustomPersistence;
			}
		}

		/// <summary>Serves as a hash function for the <see cref="T:System.Web.UI.PersistChildrenAttribute" /> class.</summary>
		/// <returns>A hash code for the <see cref="T:System.Web.UI.PersistChildrenAttribute" />.</returns>
		// Token: 0x06000F7D RID: 3965 RVA: 0x00028B98 File Offset: 0x00026D98
		public override int GetHashCode()
		{
			return this.Persist.GetHashCode();
		}

		/// <summary>Determines whether the specified object is equal to the current object.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to the current object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with the current object.</param>
		// Token: 0x06000F7E RID: 3966 RVA: 0x00028BB3 File Offset: 0x00026DB3
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && obj is PersistChildrenAttribute && ((PersistChildrenAttribute)obj).Persist == this._persist);
		}

		/// <summary>Returns a value indicating whether the value of the current instance of the <see cref="T:System.Web.UI.PersistChildrenAttribute" /> class is the default value of the derived clss.</summary>
		/// <returns>true if the value of the current instance of the <see cref="T:System.Web.UI.PersistChildrenAttribute" /> is the default instance; otherwise, false. </returns>
		// Token: 0x06000F7F RID: 3967 RVA: 0x00028BDB File Offset: 0x00026DDB
		public override bool IsDefaultAttribute()
		{
			return this.Equals(PersistChildrenAttribute.Default);
		}

		/// <summary>Indicates that nested content should persist as controls at design time. The <see cref="F:System.Web.UI.PersistChildrenAttribute.Yes" /> field is read-only.</summary>
		// Token: 0x04001318 RID: 4888
		public static readonly PersistChildrenAttribute Yes = new PersistChildrenAttribute(true);

		/// <summary>Indicates that nested content should not persist as nested controls at design time. This field is read-only.</summary>
		// Token: 0x04001319 RID: 4889
		public static readonly PersistChildrenAttribute No = new PersistChildrenAttribute(false);

		/// <summary>Indicates the default attribute state. The <see cref="F:System.Web.UI.PersistChildrenAttribute.Default" /> field is read-only.</summary>
		// Token: 0x0400131A RID: 4890
		public static readonly PersistChildrenAttribute Default = PersistChildrenAttribute.Yes;

		// Token: 0x0400131B RID: 4891
		private bool _persist;

		// Token: 0x0400131C RID: 4892
		private bool _usesCustomPersistence;
	}
}
