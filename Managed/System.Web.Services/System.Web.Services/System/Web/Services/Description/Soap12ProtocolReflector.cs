using System;
using System.Collections;
using System.Web.Services.Protocols;
using System.Xml;

namespace System.Web.Services.Description
{
	// Token: 0x0200011B RID: 283
	internal class Soap12ProtocolReflector : SoapProtocolReflector
	{
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x00002B4F File Offset: 0x00000D4F
		internal override WsiProfiles ConformsTo
		{
			get
			{
				return WsiProfiles.None;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x0003BE8B File Offset: 0x0003A08B
		public override string ProtocolName
		{
			get
			{
				return "Soap12";
			}
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0003BF87 File Offset: 0x0003A187
		protected override void BeginClass()
		{
			this.requestElements = new Hashtable();
			this.actions = new Hashtable();
			this.soap11PortType = null;
			base.BeginClass();
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0003BFAC File Offset: 0x0003A1AC
		protected override bool ReflectMethod()
		{
			if (base.ReflectMethod())
			{
				if (base.Binding != null)
				{
					this.soap11PortType = base.SoapMethod.portType;
					if (this.soap11PortType != base.Binding.Type)
					{
						base.HeaderMessages.Clear();
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0003C000 File Offset: 0x0003A200
		protected override void EndClass()
		{
			if (base.PortType == null || base.Binding == null)
			{
				return;
			}
			if (this.soap11PortType != null && this.soap11PortType != base.Binding.Type)
			{
				foreach (object obj in base.PortType.Operations)
				{
					foreach (object obj2 in ((Operation)obj).Messages)
					{
						OperationMessage operationMessage = (OperationMessage)obj2;
						ServiceDescription serviceDescription = base.GetServiceDescription(operationMessage.Message.Namespace);
						if (serviceDescription != null)
						{
							Message message = serviceDescription.Messages[operationMessage.Message.Name];
							if (message != null)
							{
								serviceDescription.Messages.Remove(message);
							}
						}
					}
				}
				base.Binding.Type = this.soap11PortType;
				base.PortType.ServiceDescription.PortTypes.Remove(base.PortType);
			}
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0003C14C File Offset: 0x0003A34C
		protected override SoapBinding CreateSoapBinding(SoapBindingStyle style)
		{
			return new Soap12Binding
			{
				Transport = "http://schemas.xmlsoap.org/soap/http",
				Style = style
			};
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0003C168 File Offset: 0x0003A368
		protected override SoapAddressBinding CreateSoapAddressBinding(string serviceUrl)
		{
			Soap12AddressBinding soapAddress = new Soap12AddressBinding();
			soapAddress.Location = serviceUrl;
			if (base.UriFixups != null)
			{
				base.UriFixups.Add(delegate(Uri current)
				{
					soapAddress.Location = DiscoveryServerType.CombineUris(current, soapAddress.Location);
				});
			}
			return soapAddress;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0003C1B8 File Offset: 0x0003A3B8
		protected override SoapOperationBinding CreateSoapOperationBinding(SoapBindingStyle style, string action)
		{
			Soap12OperationBinding soap12OperationBinding = new Soap12OperationBinding();
			soap12OperationBinding.SoapAction = action;
			soap12OperationBinding.Style = style;
			soap12OperationBinding.Method = base.SoapMethod;
			this.DealWithAmbiguity(action, base.SoapMethod.requestElementName.ToString(), soap12OperationBinding);
			return soap12OperationBinding;
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0003C200 File Offset: 0x0003A400
		protected override SoapBodyBinding CreateSoapBodyBinding(SoapBindingUse use, string ns)
		{
			Soap12BodyBinding soap12BodyBinding = new Soap12BodyBinding();
			soap12BodyBinding.Use = use;
			if (use == SoapBindingUse.Encoded)
			{
				soap12BodyBinding.Encoding = "http://www.w3.org/2003/05/soap-encoding";
			}
			soap12BodyBinding.Namespace = ns;
			return soap12BodyBinding;
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0003C231 File Offset: 0x0003A431
		protected override SoapHeaderBinding CreateSoapHeaderBinding(XmlQualifiedName message, string partName, SoapBindingUse use)
		{
			return this.CreateSoapHeaderBinding(message, partName, null, use);
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0003C240 File Offset: 0x0003A440
		protected override SoapHeaderBinding CreateSoapHeaderBinding(XmlQualifiedName message, string partName, string ns, SoapBindingUse use)
		{
			Soap12HeaderBinding soap12HeaderBinding = new Soap12HeaderBinding();
			soap12HeaderBinding.Message = message;
			soap12HeaderBinding.Part = partName;
			soap12HeaderBinding.Namespace = ns;
			soap12HeaderBinding.Use = use;
			if (use == SoapBindingUse.Encoded)
			{
				soap12HeaderBinding.Encoding = "http://www.w3.org/2003/05/soap-encoding";
			}
			return soap12HeaderBinding;
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0003C284 File Offset: 0x0003A484
		private void DealWithAmbiguity(string action, string requestElement, Soap12OperationBinding operation)
		{
			Soap12OperationBinding soap12OperationBinding = (Soap12OperationBinding)this.actions[action];
			if (soap12OperationBinding != null)
			{
				operation.DuplicateBySoapAction = soap12OperationBinding;
				soap12OperationBinding.DuplicateBySoapAction = operation;
				this.CheckOperationDuplicates(soap12OperationBinding);
			}
			else
			{
				this.actions[action] = operation;
			}
			Soap12OperationBinding soap12OperationBinding2 = (Soap12OperationBinding)this.requestElements[requestElement];
			if (soap12OperationBinding2 != null)
			{
				operation.DuplicateByRequestElement = soap12OperationBinding2;
				soap12OperationBinding2.DuplicateByRequestElement = operation;
				this.CheckOperationDuplicates(soap12OperationBinding2);
			}
			else
			{
				this.requestElements[requestElement] = operation;
			}
			this.CheckOperationDuplicates(operation);
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0003C30C File Offset: 0x0003A50C
		private void CheckOperationDuplicates(Soap12OperationBinding operation)
		{
			if (operation.DuplicateByRequestElement == null)
			{
				operation.SoapActionRequired = false;
				return;
			}
			if (operation.DuplicateBySoapAction != null)
			{
				throw new InvalidOperationException(Res.GetString("The methods {0} and {1} use the same request element <{2} xmlns='{3}'> and the methods {0} and {4} use the same SOAPAction '{5}'. Each method on the XML Web service must have a unique SOAPAction or request element. If you are using the Rpc style, you can change the message name with the RequestElementName and RequestElementNamespace parameters to SoapRpcMethodAttribute. For document style using wrapped parameter style, use the RequestElementName and RequestElementNamespace parameters to SoapDocumentMethodAttribute. For the bare parameter style, you must change the element that represents the parameter. Alternatively, you can specify a unique SoapAction for each method using the Action parameter to SoapRpcMethodAttribute or SoapDocumentMethodAttribute.", new object[]
				{
					operation.Method.name,
					operation.DuplicateByRequestElement.Method.name,
					operation.Method.requestElementName.Name,
					operation.Method.requestElementName.Namespace,
					operation.DuplicateBySoapAction.Method.name,
					operation.Method.action
				}));
			}
			operation.SoapActionRequired = true;
		}

		// Token: 0x04000523 RID: 1315
		private Hashtable requestElements;

		// Token: 0x04000524 RID: 1316
		private Hashtable actions;

		// Token: 0x04000525 RID: 1317
		private XmlQualifiedName soap11PortType;
	}
}
