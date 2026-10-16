using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Identifies the types of connection points that a transformer supports. </summary>
	// Token: 0x020007B6 RID: 1974
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class WebPartTransformerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformerAttribute" /> class. </summary>
		/// <param name="consumerType">The <see cref="T:System.Type" /> of the interface supported by the consumer connection point.</param>
		/// <param name="providerType">The <see cref="T:System.Type" /> of the interface supported by the provider connection point.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="consumerType" /> or <paramref name="providerType" /> is not specified.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="consumerType" /> equals <paramref name="providerType" />.</exception>
		// Token: 0x06004FA0 RID: 20384 RVA: 0x00003946 File Offset: 0x00001B46
		public WebPartTransformerAttribute(Type consumerType, Type providerType)
		{
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the interface supported by the consumer connection point.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the interface supported by the consumer connection point.</returns>
		// Token: 0x17001845 RID: 6213
		// (get) Token: 0x06004FA1 RID: 20385 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public Type ConsumerType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the interface supported by the provider connection point.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the interface supported by the provider connection point.</returns>
		// Token: 0x17001846 RID: 6214
		// (get) Token: 0x06004FA2 RID: 20386 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public Type ProviderType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Returns the consumer type a transformer can accept on a connection point.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the consumer connection point.</returns>
		/// <param name="transformerType">The type of transformer.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="transformerType" /> is not specified.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="transformerType" /> is not an object derived from the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> class.</exception>
		// Token: 0x06004FA3 RID: 20387 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public static Type GetConsumerType(Type transformerType)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns the provider type a transformer can accept on a connection point.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the provider connection point.</returns>
		/// <param name="transformerType">The type of transformer.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="transformerType" /> is not specified.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="transformerType" /> is not an object derived from the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> class.</exception>
		// Token: 0x06004FA4 RID: 20388 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public static Type GetProviderType(Type transformerType)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
