using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Purchasing.Interfaces;
using UnityEngine.Purchasing.Models;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000021 RID: 33
	internal class ProductDetailsQueryResponse : IProductDetailsQueryResponse
	{
		// Token: 0x06000090 RID: 144 RVA: 0x0000378C File Offset: 0x0000198C
		protected override void Finalize()
		{
			try
			{
				foreach (ValueTuple<IGoogleBillingResult, IEnumerable<AndroidJavaObject>> valueTuple in this.m_Responses)
				{
					IEnumerable<AndroidJavaObject> item = valueTuple.Item2;
					if (item != null)
					{
						foreach (AndroidJavaObject androidJavaObject in item)
						{
							if (androidJavaObject != null)
							{
								androidJavaObject.Dispose();
							}
						}
					}
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003828 File Offset: 0x00001A28
		public void AddResponse(IGoogleBillingResult billingResult, IEnumerable<AndroidJavaObject> productDetails)
		{
			this.m_Responses.Add(new ValueTuple<IGoogleBillingResult, IEnumerable<AndroidJavaObject>>(billingResult, productDetails.Select<AndroidJavaObject, AndroidJavaObject>((AndroidJavaObject product) => product.CloneReference()).ToList<AndroidJavaObject>()));
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003868 File Offset: 0x00001A68
		public List<AndroidJavaObject> ProductDetails()
		{
			return this.m_Responses.Where<ValueTuple<IGoogleBillingResult, IEnumerable<AndroidJavaObject>>>((ValueTuple<IGoogleBillingResult, IEnumerable<AndroidJavaObject>> response) => response.Item1.responseCode == GoogleBillingResponseCode.Ok).SelectMany<ValueTuple<IGoogleBillingResult, IEnumerable<AndroidJavaObject>>, AndroidJavaObject>((ValueTuple<IGoogleBillingResult, IEnumerable<AndroidJavaObject>> response) => response.Item2).ToList<AndroidJavaObject>();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000038C8 File Offset: 0x00001AC8
		public bool IsRecoverable()
		{
			return this.m_Responses.Select<ValueTuple<IGoogleBillingResult, IEnumerable<AndroidJavaObject>>, IGoogleBillingResult>((ValueTuple<IGoogleBillingResult, IEnumerable<AndroidJavaObject>> response) => response.Item1).Any<IGoogleBillingResult>(new Func<IGoogleBillingResult, bool>(ProductDetailsQueryResponse.IsRecoverable));
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003908 File Offset: 0x00001B08
		public IGoogleBillingResult GetGoogleBillingResult()
		{
			return this.m_Responses.Select<ValueTuple<IGoogleBillingResult, IEnumerable<AndroidJavaObject>>, IGoogleBillingResult>((ValueTuple<IGoogleBillingResult, IEnumerable<AndroidJavaObject>> response) => response.Item1).FirstOrDefault<IGoogleBillingResult>((IGoogleBillingResult response) => response.responseCode > GoogleBillingResponseCode.Ok);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003963 File Offset: 0x00001B63
		private static bool IsRecoverable(IGoogleBillingResult billingResult)
		{
			return billingResult.responseCode == GoogleBillingResponseCode.ServiceUnavailable || billingResult.responseCode == GoogleBillingResponseCode.DeveloperError;
		}

		// Token: 0x04000047 RID: 71
		private readonly ConcurrentBag<ValueTuple<IGoogleBillingResult, IEnumerable<AndroidJavaObject>>> m_Responses = new ConcurrentBag<ValueTuple<IGoogleBillingResult, IEnumerable<AndroidJavaObject>>>();
	}
}
