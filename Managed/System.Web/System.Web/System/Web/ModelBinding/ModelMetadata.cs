using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a container for metadata for a model. </summary>
	// Token: 0x020006EF RID: 1775
	public class ModelMetadata
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelMetadata" /> class.</summary>
		/// <param name="provider">The provider object.</param>
		/// <param name="containerType">The type of the container, or null to create metadata for the model type.</param>
		/// <param name="modelAccessor">The model accessor.</param>
		/// <param name="modelType">The type of the model.</param>
		/// <param name="propertyName">The name of the property, or null to create metadata for the model type.</param>
		// Token: 0x06004B00 RID: 19200 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ModelMetadata(ModelMetadataProvider provider, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a collection that contains additional metadata about the model.</summary>
		/// <returns>A collection that contains additional metadata about the model.</returns>
		// Token: 0x17001739 RID: 5945
		// (get) Token: 0x06004B01 RID: 19201 RVA: 0x0000FA7F File Offset: 0x0000DC7F
		public virtual Dictionary<string, object> AdditionalValues
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the type of the container. </summary>
		/// <returns>The type of the container, or null if there is no container.</returns>
		// Token: 0x1700173A RID: 5946
		// (get) Token: 0x06004B02 RID: 19202 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public Type ContainerType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets a value that determines whether empty strings that are submitted in forms should be converted to null.</summary>
		/// <returns>true if empty strings should be converted to null; otherwise, false. The default value is true.</returns>
		// Token: 0x1700173B RID: 5947
		// (get) Token: 0x06004B03 RID: 19203 RVA: 0x000C806C File Offset: 0x000C626C
		// (set) Token: 0x06004B04 RID: 19204 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual bool ConvertEmptyStringToNull
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the name of the data type.</summary>
		/// <returns>The name of the data type.</returns>
		// Token: 0x1700173C RID: 5948
		// (get) Token: 0x06004B05 RID: 19205 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004B06 RID: 19206 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual string DataTypeName
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets text that describes the model.</summary>
		/// <returns>The description text.</returns>
		// Token: 0x1700173D RID: 5949
		// (get) Token: 0x06004B07 RID: 19207 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004B08 RID: 19208 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual string Description
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a format string that should be applied when the model is displayed in display mode (as opposed to in edit mode).</summary>
		/// <returns>The format string.</returns>
		// Token: 0x1700173E RID: 5950
		// (get) Token: 0x06004B09 RID: 19209 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004B0A RID: 19210 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual string DisplayFormatString
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the text to use in UI when the name of the model is displayed. </summary>
		/// <returns>The text to use as the name of the model.</returns>
		// Token: 0x1700173F RID: 5951
		// (get) Token: 0x06004B0B RID: 19211 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004B0C RID: 19212 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual string DisplayName
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the format string to use in UI in edit mode.</summary>
		/// <returns>The format string.</returns>
		// Token: 0x17001740 RID: 5952
		// (get) Token: 0x06004B0D RID: 19213 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004B0E RID: 19214 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual string EditFormatString
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that determines whether surrounding HTML should be hidden.</summary>
		/// <returns>true if surrounding HTML should be hidden; otherwise, false.</returns>
		// Token: 0x17001741 RID: 5953
		// (get) Token: 0x06004B0F RID: 19215 RVA: 0x000C8088 File Offset: 0x000C6288
		// (set) Token: 0x06004B10 RID: 19216 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual bool HideSurroundingHtml
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a value that specifies whether the model is a complex type.</summary>
		/// <returns>true if the model is a complex type; otherwise, false.</returns>
		// Token: 0x17001742 RID: 5954
		// (get) Token: 0x06004B11 RID: 19217 RVA: 0x000C80A4 File Offset: 0x000C62A4
		public virtual bool IsComplexType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets a value that specifies whether the model is a nullable value type.</summary>
		/// <returns>true if the model is a nullable value type; otherwise, false.</returns>
		// Token: 0x17001743 RID: 5955
		// (get) Token: 0x06004B12 RID: 19218 RVA: 0x000C80C0 File Offset: 0x000C62C0
		public bool IsNullableValueType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets or sets a value that specifies whether the model can be updated.</summary>
		/// <returns>true if the model is read-only; otherwise, false The default is false.</returns>
		// Token: 0x17001744 RID: 5956
		// (get) Token: 0x06004B13 RID: 19219 RVA: 0x000C80DC File Offset: 0x000C62DC
		// (set) Token: 0x06004B14 RID: 19220 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual bool IsReadOnly
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that specifies whether the property is required.</summary>
		/// <returns>true if the property is required; otherwise, false. The default is true for non-nullable value types and false for all other types.</returns>
		// Token: 0x17001745 RID: 5957
		// (get) Token: 0x06004B15 RID: 19221 RVA: 0x000C80F8 File Offset: 0x000C62F8
		// (set) Token: 0x06004B16 RID: 19222 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual bool IsRequired
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the model object.</summary>
		/// <returns>The model object.</returns>
		// Token: 0x17001746 RID: 5958
		// (get) Token: 0x06004B17 RID: 19223 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004B18 RID: 19224 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public object Model
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

		/// <summary>Gets the type of the model.</summary>
		/// <returns>The type of the model.</returns>
		// Token: 0x17001747 RID: 5959
		// (get) Token: 0x06004B19 RID: 19225 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public Type ModelType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the string that should be displayed when the model is null.</summary>
		/// <returns>The string to display when the model is null.</returns>
		// Token: 0x17001748 RID: 5960
		// (get) Token: 0x06004B1A RID: 19226 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004B1B RID: 19227 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual string NullDisplayText
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets an integer that indicates the order in which to display this property relative to other properties.</summary>
		/// <returns>The relative order in which to display this property.</returns>
		// Token: 0x17001749 RID: 5961
		// (get) Token: 0x06004B1C RID: 19228 RVA: 0x000C8114 File Offset: 0x000C6314
		// (set) Token: 0x06004B1D RID: 19229 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual int Order
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a collection of model metadata objects that describe the properties of the model.</summary>
		/// <returns>Metadata for all properties in the model.</returns>
		// Token: 0x1700174A RID: 5962
		// (get) Token: 0x06004B1E RID: 19230 RVA: 0x0000FA7F File Offset: 0x0000DC7F
		public virtual IEnumerable<ModelMetadata> Properties
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the name of the property.</summary>
		/// <returns>The name of the property if the model is a property; otherwise, null.</returns>
		// Token: 0x1700174B RID: 5963
		// (get) Token: 0x06004B1F RID: 19231 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string PropertyName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets the provider object for this metadata.</summary>
		/// <returns>The provider object for this metadata.</returns>
		// Token: 0x1700174C RID: 5964
		// (get) Token: 0x06004B20 RID: 19232 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004B21 RID: 19233 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected ModelMetadataProvider Provider
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that specifies whether request validation is enabled.</summary>
		/// <returns>true if request validation is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x1700174D RID: 5965
		// (get) Token: 0x06004B22 RID: 19234 RVA: 0x000C8130 File Offset: 0x000C6330
		// (set) Token: 0x06004B23 RID: 19235 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual bool RequestValidationEnabled
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a short version of the display name.</summary>
		/// <returns>The text to display when a short version of the model name is required.</returns>
		// Token: 0x1700174E RID: 5966
		// (get) Token: 0x06004B24 RID: 19236 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004B25 RID: 19237 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual string ShortDisplayName
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that specifies whether the model should be displayed in the UI in display mode (as opposed to edit mode).</summary>
		/// <returns>true if the model should be displayed in display mode; otherwise, false. The default is true.</returns>
		// Token: 0x1700174F RID: 5967
		// (get) Token: 0x06004B26 RID: 19238 RVA: 0x000C814C File Offset: 0x000C634C
		// (set) Token: 0x06004B27 RID: 19239 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual bool ShowForDisplay
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that specifies whether the property should be displayed in edit mode (as opposed to display mode).</summary>
		/// <returns>true if the property should be displayed in edit mode; otherwise, false. The default is true.</returns>
		// Token: 0x17001750 RID: 5968
		// (get) Token: 0x06004B28 RID: 19240 RVA: 0x000C8168 File Offset: 0x000C6368
		// (set) Token: 0x06004B29 RID: 19241 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual bool ShowForEdit
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets text to display for the model when the model represents a complex object</summary>
		/// <returns>Text to display in UI for a complex object. The default value is determined by calling the <see cref="M:System.Web.ModelBinding.ModelMetadata.GetSimpleDisplayText" /> method.</returns>
		// Token: 0x17001751 RID: 5969
		// (get) Token: 0x06004B2A RID: 19242 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004B2B RID: 19243 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual string SimpleDisplayText
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

		/// <summary>Gets or sets a value that indicates what template (data control) should be used in UI for the model.</summary>
		/// <returns>A value that indicates what template (data control) should be used in UI for the model.</returns>
		// Token: 0x17001752 RID: 5970
		// (get) Token: 0x06004B2C RID: 19244 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004B2D RID: 19245 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual string TemplateHint
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets watermark text for a text box.</summary>
		/// <returns>The watermark text.</returns>
		// Token: 0x17001753 RID: 5971
		// (get) Token: 0x06004B2E RID: 19246 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004B2F RID: 19247 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual string Watermark
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Returns text to display as the name of the model in UI.</summary>
		/// <returns>Text to display as the name of the model in UI. The default is the value of the <see cref="P:System.Web.ModelBinding.ModelMetadata.DisplayName" /> property if that value is not null. If the <see cref="P:System.Web.ModelBinding.ModelMetadata.DisplayName" /> property is null, the default value is the value of the <see cref="P:System.Web.ModelBinding.ModelMetadata.PropertyName" /> property. If the <see cref="P:System.Web.ModelBinding.ModelMetadata.PropertyName" /> property is null, the default value comes from the <see cref="P:System.Reflection.MemberInfo.Name" /> property of the <see cref="P:System.Web.ModelBinding.ModelMetadata.ModelType" /> property.</returns>
		// Token: 0x06004B30 RID: 19248 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string GetDisplayName()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns text to display for the model when the model represents a complex object.</summary>
		/// <returns>The property value is derived by examining the following sequence of related values until a return value is found.If the <see cref="P:System.Web.ModelBinding.ModelMetadata.SimpleDisplayText" /> property value is not null, that value is returned.If the <see cref="P:System.Web.ModelBinding.ModelMetadata.Model" /> property is null, the <see cref="P:System.Web.ModelBinding.ModelMetadata.NullDisplayText" /> property value is returned. If an attempt to convert the <see cref="P:System.Web.ModelBinding.ModelMetadata.Model" /> property to a string returns null, an empty string is returned. If the string conversion of the <see cref="P:System.Web.ModelBinding.ModelMetadata.Model" /> property value is the same as the <see cref="P:System.Type.FullName" /> property of the <see cref="P:System.Web.ModelBinding.ModelMetadata.Model" /> property type, that value is returned. If there are no properties in the <see cref="P:System.Web.ModelBinding.ModelMetadata.Properties" /> collection, an empty string is returned.If none of the preceding tests have returned a value, the return value comes from the first property in the <see cref="P:System.Web.ModelBinding.ModelMetadata.Properties" /> collection. If the <see cref="P:System.Web.ModelBinding.ModelMetadata.Model" /> property of the first property is null, the <see cref="P:System.Web.ModelBinding.ModelMetadata.NullDisplayText" /> property value is returned; otherwise the result of converting the first property's <see cref="P:System.Web.ModelBinding.ModelMetadata.Model" /> property to a string is returned.</returns>
		// Token: 0x06004B31 RID: 19249 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected virtual string GetSimpleDisplayText()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a collection of validators that apply to the model.</summary>
		/// <returns>A collection of validators.</returns>
		/// <param name="context">The model binding execution context.</param>
		// Token: 0x06004B32 RID: 19250 RVA: 0x0000FA7F File Offset: 0x0000DC7F
		public virtual IEnumerable<ModelValidator> GetValidators(ModelBindingExecutionContext context)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>An integer value that is always set to 10000. </summary>
		// Token: 0x040025DE RID: 9694
		public const int DefaultOrder = 10000;
	}
}
