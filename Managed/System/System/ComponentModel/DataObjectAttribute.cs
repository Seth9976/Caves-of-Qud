using System;

namespace System.ComponentModel
{
	/// <summary>Identifies a type as an object suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object. This class cannot be inherited.</summary>
	// Token: 0x02000397 RID: 919
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DataObjectAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectAttribute" /> class. </summary>
		// Token: 0x06001E15 RID: 7701 RVA: 0x0006B4C2 File Offset: 0x000696C2
		public DataObjectAttribute()
			: this(true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectAttribute" /> class and indicates whether an object is suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object.</summary>
		/// <param name="isDataObject">true if the object is suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object; otherwise, false.</param>
		// Token: 0x06001E16 RID: 7702 RVA: 0x0006B4CB File Offset: 0x000696CB
		public DataObjectAttribute(bool isDataObject)
		{
			this.IsDataObject = isDataObject;
		}

		/// <summary>Gets a value indicating whether an object should be considered suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object at design time.</summary>
		/// <returns>true if the object should be considered suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object; otherwise, false.</returns>
		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001E17 RID: 7703 RVA: 0x0006B4DA File Offset: 0x000696DA
		public bool IsDataObject { get; }

		/// <summary>Determines whether this instance of <see cref="T:System.ComponentModel.DataObjectAttribute" /> fits the pattern of another object.</summary>
		/// <returns>true if this instance is the same as the instance specified by the <paramref name="obj" /> parameter; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance of <see cref="T:System.ComponentModel.DataObjectAttribute" />. </param>
		// Token: 0x06001E18 RID: 7704 RVA: 0x0006B4E4 File Offset: 0x000696E4
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DataObjectAttribute dataObjectAttribute = obj as DataObjectAttribute;
			return dataObjectAttribute != null && dataObjectAttribute.IsDataObject == this.IsDataObject;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001E19 RID: 7705 RVA: 0x0006B514 File Offset: 0x00069714
		public override int GetHashCode()
		{
			return this.IsDataObject.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>true if the current value of the attribute is the default; otherwise, false.</returns>
		// Token: 0x06001E1A RID: 7706 RVA: 0x0006B52F File Offset: 0x0006972F
		public override bool IsDefaultAttribute()
		{
			return this.Equals(DataObjectAttribute.Default);
		}

		/// <summary>Indicates that the class is suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object at design time. This field is read-only.</summary>
		// Token: 0x04000F28 RID: 3880
		public static readonly DataObjectAttribute DataObject = new DataObjectAttribute(true);

		/// <summary>Indicates that the class is not suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object at design time. This field is read-only.</summary>
		// Token: 0x04000F29 RID: 3881
		public static readonly DataObjectAttribute NonDataObject = new DataObjectAttribute(false);

		/// <summary>Represents the default value of the <see cref="T:System.ComponentModel.DataObjectAttribute" /> class, which indicates that the class is suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object at design time. This field is read-only.</summary>
		// Token: 0x04000F2A RID: 3882
		public static readonly DataObjectAttribute Default = DataObjectAttribute.NonDataObject;
	}
}
