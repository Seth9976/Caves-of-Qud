using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the values that control the behavior of the AutoComplete feature in a <see cref="T:System.Web.UI.WebControls.TextBox" /> control.</summary>
	// Token: 0x0200027A RID: 634
	public enum AutoCompleteType
	{
		/// <summary>No category is associated with the <see cref="T:System.Web.UI.WebControls.TextBox" /> control. All <see cref="T:System.Web.UI.WebControls.TextBox" /> controls with the same <see cref="P:System.Web.UI.Control.ID" /> share the same value list.</summary>
		// Token: 0x04001648 RID: 5704
		None,
		/// <summary>The AutoComplete feature is disabled for the <see cref="T:System.Web.UI.WebControls.TextBox" /> control.</summary>
		// Token: 0x04001649 RID: 5705
		Disabled,
		/// <summary>The phone number for a mobile-phone category.</summary>
		// Token: 0x0400164A RID: 5706
		Cellular,
		/// <summary>The name of a business category.</summary>
		// Token: 0x0400164B RID: 5707
		Company,
		/// <summary>A department within a business category.</summary>
		// Token: 0x0400164C RID: 5708
		Department,
		/// <summary>The name to display for the user category.</summary>
		// Token: 0x0400164D RID: 5709
		DisplayName,
		/// <summary>The user's e-mail address category.</summary>
		// Token: 0x0400164E RID: 5710
		Email,
		/// <summary>The first name category.</summary>
		// Token: 0x0400164F RID: 5711
		FirstName,
		/// <summary>The gender of the user category.</summary>
		// Token: 0x04001650 RID: 5712
		Gender,
		/// <summary>The city for a home address category.</summary>
		// Token: 0x04001651 RID: 5713
		HomeCity,
		/// <summary>The country/region for a home address category.</summary>
		// Token: 0x04001652 RID: 5714
		HomeCountryRegion,
		/// <summary>The fax number for a home address category.</summary>
		// Token: 0x04001653 RID: 5715
		HomeFax,
		/// <summary>The phone number for a home address category.</summary>
		// Token: 0x04001654 RID: 5716
		HomePhone,
		/// <summary>The state for a home address category.</summary>
		// Token: 0x04001655 RID: 5717
		HomeState,
		/// <summary>The street for a home address category.</summary>
		// Token: 0x04001656 RID: 5718
		HomeStreetAddress,
		/// <summary>The ZIP code for a home address category.</summary>
		// Token: 0x04001657 RID: 5719
		HomeZipCode,
		/// <summary>The URL to a Web site category.</summary>
		// Token: 0x04001658 RID: 5720
		Homepage,
		/// <summary>The user's job title category.</summary>
		// Token: 0x04001659 RID: 5721
		JobTitle,
		/// <summary>The last name category.</summary>
		// Token: 0x0400165A RID: 5722
		LastName,
		/// <summary>The user's middle name category.</summary>
		// Token: 0x0400165B RID: 5723
		MiddleName,
		/// <summary>Any supplemental information to include in the form category.</summary>
		// Token: 0x0400165C RID: 5724
		Notes,
		/// <summary>The location of the business office category.</summary>
		// Token: 0x0400165D RID: 5725
		Office,
		/// <summary>The phone number for a pager category.</summary>
		// Token: 0x0400165E RID: 5726
		Pager,
		/// <summary>The city for a business address category.</summary>
		// Token: 0x0400165F RID: 5727
		BusinessCity,
		/// <summary>The country/region for a business address category.</summary>
		// Token: 0x04001660 RID: 5728
		BusinessCountryRegion,
		/// <summary>The fax number for a business address category.</summary>
		// Token: 0x04001661 RID: 5729
		BusinessFax,
		/// <summary>The phone number for a business address category.</summary>
		// Token: 0x04001662 RID: 5730
		BusinessPhone,
		/// <summary>The state for a business address category.</summary>
		// Token: 0x04001663 RID: 5731
		BusinessState,
		/// <summary>The street for a business address category.</summary>
		// Token: 0x04001664 RID: 5732
		BusinessStreetAddress,
		/// <summary>The URL to a business Web site category.</summary>
		// Token: 0x04001665 RID: 5733
		BusinessUrl,
		/// <summary>The ZIP code for a business address category.</summary>
		// Token: 0x04001666 RID: 5734
		BusinessZipCode,
		/// <summary>The keyword or keywords with which to search a Web page or Web site category.</summary>
		// Token: 0x04001667 RID: 5735
		Search,
		/// <summary>The AutoComplete feature is enabled for the <see cref="T:System.Web.UI.WebControls.TextBox" /> control.</summary>
		// Token: 0x04001668 RID: 5736
		Enabled
	}
}
