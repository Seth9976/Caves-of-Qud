using System;
using System.Collections;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	// Token: 0x02000129 RID: 297
	internal class SoapParameters
	{
		// Token: 0x060008E1 RID: 2273 RVA: 0x0003C8C4 File Offset: 0x0003AAC4
		internal SoapParameters(XmlMembersMapping request, XmlMembersMapping response, string[] parameterOrder, CodeIdentifiers identifiers)
		{
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			SoapParameters.AddMappings(arrayList, request);
			if (response != null)
			{
				SoapParameters.AddMappings(arrayList2, response);
			}
			if (parameterOrder != null)
			{
				foreach (string text in parameterOrder)
				{
					XmlMemberMapping xmlMemberMapping = SoapParameters.FindMapping(arrayList, text);
					SoapParameter soapParameter = new SoapParameter();
					if (xmlMemberMapping != null)
					{
						if (SoapParameters.RemoveByRefMapping(arrayList2, xmlMemberMapping))
						{
							soapParameter.codeFlags = CodeFlags.IsByRef;
						}
						soapParameter.mapping = xmlMemberMapping;
						arrayList.Remove(xmlMemberMapping);
						this.AddParameter(soapParameter);
					}
					else
					{
						XmlMemberMapping xmlMemberMapping2 = SoapParameters.FindMapping(arrayList2, text);
						if (xmlMemberMapping2 != null)
						{
							soapParameter.codeFlags = CodeFlags.IsOut;
							soapParameter.mapping = xmlMemberMapping2;
							arrayList2.Remove(xmlMemberMapping2);
							this.AddParameter(soapParameter);
						}
					}
				}
			}
			foreach (object obj in arrayList)
			{
				XmlMemberMapping xmlMemberMapping3 = (XmlMemberMapping)obj;
				SoapParameter soapParameter2 = new SoapParameter();
				if (SoapParameters.RemoveByRefMapping(arrayList2, xmlMemberMapping3))
				{
					soapParameter2.codeFlags = CodeFlags.IsByRef;
				}
				soapParameter2.mapping = xmlMemberMapping3;
				this.AddParameter(soapParameter2);
			}
			if (arrayList2.Count > 0)
			{
				if (!((XmlMemberMapping)arrayList2[0]).CheckSpecified)
				{
					this.ret = (XmlMemberMapping)arrayList2[0];
					arrayList2.RemoveAt(0);
				}
				foreach (object obj2 in arrayList2)
				{
					XmlMemberMapping xmlMemberMapping4 = (XmlMemberMapping)obj2;
					this.AddParameter(new SoapParameter
					{
						mapping = xmlMemberMapping4,
						codeFlags = CodeFlags.IsOut
					});
				}
			}
			foreach (object obj3 in this.parameters)
			{
				SoapParameter soapParameter3 = (SoapParameter)obj3;
				soapParameter3.name = identifiers.MakeUnique(CodeIdentifier.MakeValid(soapParameter3.mapping.MemberName));
			}
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0003CB18 File Offset: 0x0003AD18
		private void AddParameter(SoapParameter parameter)
		{
			this.parameters.Add(parameter);
			if (parameter.mapping.CheckSpecified)
			{
				this.checkSpecifiedCount++;
			}
			if (parameter.IsByRef)
			{
				this.inParameters.Add(parameter);
				this.outParameters.Add(parameter);
				if (parameter.mapping.CheckSpecified)
				{
					this.inCheckSpecifiedCount++;
					this.outCheckSpecifiedCount++;
					return;
				}
			}
			else if (parameter.IsOut)
			{
				this.outParameters.Add(parameter);
				if (parameter.mapping.CheckSpecified)
				{
					this.outCheckSpecifiedCount++;
					return;
				}
			}
			else
			{
				this.inParameters.Add(parameter);
				if (parameter.mapping.CheckSpecified)
				{
					this.inCheckSpecifiedCount++;
				}
			}
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0003CBF4 File Offset: 0x0003ADF4
		private static bool RemoveByRefMapping(ArrayList responseList, XmlMemberMapping requestMapping)
		{
			XmlMemberMapping xmlMemberMapping = SoapParameters.FindMapping(responseList, requestMapping.ElementName);
			if (xmlMemberMapping == null)
			{
				return false;
			}
			if (requestMapping.TypeFullName != xmlMemberMapping.TypeFullName)
			{
				return false;
			}
			if (requestMapping.Namespace != xmlMemberMapping.Namespace)
			{
				return false;
			}
			if (requestMapping.MemberName != xmlMemberMapping.MemberName)
			{
				return false;
			}
			responseList.Remove(xmlMemberMapping);
			return true;
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0003CC5C File Offset: 0x0003AE5C
		private static void AddMappings(ArrayList mappingsList, XmlMembersMapping mappings)
		{
			for (int i = 0; i < mappings.Count; i++)
			{
				mappingsList.Add(mappings[i]);
			}
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0003CC88 File Offset: 0x0003AE88
		private static XmlMemberMapping FindMapping(ArrayList mappingsList, string elementName)
		{
			foreach (object obj in mappingsList)
			{
				XmlMemberMapping xmlMemberMapping = (XmlMemberMapping)obj;
				if (xmlMemberMapping.ElementName == elementName)
				{
					return xmlMemberMapping;
				}
			}
			return null;
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x0003CCEC File Offset: 0x0003AEEC
		internal XmlMemberMapping Return
		{
			get
			{
				return this.ret;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x0003CCF4 File Offset: 0x0003AEF4
		internal IList Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x0003CCFC File Offset: 0x0003AEFC
		internal IList InParameters
		{
			get
			{
				return this.inParameters;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x0003CD04 File Offset: 0x0003AF04
		internal IList OutParameters
		{
			get
			{
				return this.outParameters;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060008EA RID: 2282 RVA: 0x0003CD0C File Offset: 0x0003AF0C
		internal int CheckSpecifiedCount
		{
			get
			{
				return this.checkSpecifiedCount;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x0003CD14 File Offset: 0x0003AF14
		internal int InCheckSpecifiedCount
		{
			get
			{
				return this.inCheckSpecifiedCount;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060008EC RID: 2284 RVA: 0x0003CD1C File Offset: 0x0003AF1C
		internal int OutCheckSpecifiedCount
		{
			get
			{
				return this.outCheckSpecifiedCount;
			}
		}

		// Token: 0x0400054D RID: 1357
		private XmlMemberMapping ret;

		// Token: 0x0400054E RID: 1358
		private ArrayList parameters = new ArrayList();

		// Token: 0x0400054F RID: 1359
		private ArrayList inParameters = new ArrayList();

		// Token: 0x04000550 RID: 1360
		private ArrayList outParameters = new ArrayList();

		// Token: 0x04000551 RID: 1361
		private int checkSpecifiedCount;

		// Token: 0x04000552 RID: 1362
		private int inCheckSpecifiedCount;

		// Token: 0x04000553 RID: 1363
		private int outCheckSpecifiedCount;
	}
}
