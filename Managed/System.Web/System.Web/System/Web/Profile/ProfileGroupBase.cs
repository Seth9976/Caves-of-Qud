using System;

namespace System.Web.Profile
{
	/// <summary>Provides untyped access to grouped ASP.NET profile property values.</summary>
	// Token: 0x020004FB RID: 1275
	public class ProfileGroupBase
	{
		/// <summary>Gets or sets a grouped profile property value indexed by the property name.</summary>
		/// <returns>The value of the specified grouped profile property.</returns>
		/// <param name="propertyName">The name of the grouped profile property.</param>
		// Token: 0x170011BF RID: 4543
		public object this[string propertyName]
		{
			get
			{
				return this._Parent[this._MyName + propertyName];
			}
			set
			{
				this._Parent[this._MyName + propertyName] = value;
			}
		}

		/// <summary>Gets the value of a grouped profile property.</summary>
		/// <returns>The value of the grouped profile property typed as object.</returns>
		/// <param name="propertyName">The name of the grouped profile property.</param>
		// Token: 0x060038E2 RID: 14562 RVA: 0x000972D9 File Offset: 0x000954D9
		public object GetPropertyValue(string propertyName)
		{
			return this._Parent[this._MyName + propertyName];
		}

		/// <summary>Sets the value of a grouped profile property.</summary>
		/// <param name="propertyName">The name of the grouped property to set.</param>
		/// <param name="propertyValue">The value to assign to the grouped property.</param>
		// Token: 0x060038E3 RID: 14563 RVA: 0x000972F2 File Offset: 0x000954F2
		public void SetPropertyValue(string propertyName, object propertyValue)
		{
			this._Parent[this._MyName + propertyName] = propertyValue;
		}

		/// <summary>Creates an instance of the <see cref="T:System.Web.Profile.ProfileGroupBase" /> class.</summary>
		// Token: 0x060038E4 RID: 14564 RVA: 0x0009730C File Offset: 0x0009550C
		public ProfileGroupBase()
		{
			this._Parent = null;
			this._MyName = null;
		}

		/// <summary>Used by ASP.NET to initialize the grouped profile property values and information.</summary>
		/// <param name="parent">The class that inherits <see cref="T:System.Web.Profile.ProfileBase" /> that is assigned to the <see cref="P:System.Web.HttpContext.Profile" /> property.</param>
		/// <param name="myName">The name of the profile property group.</param>
		// Token: 0x060038E5 RID: 14565 RVA: 0x00097322 File Offset: 0x00095522
		public void Init(ProfileBase parent, string myName)
		{
			if (this._Parent == null)
			{
				this._Parent = parent;
				this._MyName = myName + ".";
			}
		}

		// Token: 0x04001F05 RID: 7941
		private string _MyName;

		// Token: 0x04001F06 RID: 7942
		private ProfileBase _Parent;
	}
}
