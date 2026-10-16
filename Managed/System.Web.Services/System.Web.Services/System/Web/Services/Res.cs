using System;

namespace System.Web.Services
{
	// Token: 0x02000018 RID: 24
	internal static class Res
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00002B10 File Offset: 0x00000D10
		public static string GetString(string name)
		{
			return name;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002B10 File Offset: 0x00000D10
		public static string GetString(string name, params object[] args)
		{
			return name;
		}

		// Token: 0x04000085 RID: 133
		public const string WebConfigInvalidExtensionPriority = "{0} is an invalid value for the extension priority. The priority must be nonnegative.";

		// Token: 0x04000086 RID: 134
		public const string ConfigKeyNotFoundInElementCollection = "No elements matching the key {0} were found in the ConfigurationElementCollection.";

		// Token: 0x04000087 RID: 135
		public const string ConfigKeysDoNotMatch = "The key does not match the indexer key. Key on element (expected value): {0}. Key provided to indexer: {1}.";

		// Token: 0x04000088 RID: 136
		public const string Invalid_priority_group_value = "The Group attribute must be set to a valid PriorityGroup value.";

		// Token: 0x04000089 RID: 137
		public const string WebSchemaNotFound = "Schema not found.";

		// Token: 0x0400008A RID: 138
		public const string WebReflectionError = "Error reflecting {0}.{1}.";

		// Token: 0x0400008B RID: 139
		public const string WebInvalidMethodName = "{0} Web Service method name is not valid.";

		// Token: 0x0400008C RID: 140
		public const string WebInvalidMethodNameCase = "Invalid method name '{0}',  method names are case sensitive.   The method name '{1}' with the same name but different casing was found.";

		// Token: 0x0400008D RID: 141
		public const string WebInvalidRequestFormat = "Request format is invalid.";

		// Token: 0x0400008E RID: 142
		public const string WebInvalidRequestFormatDetails = "Request format is invalid: {0}.";

		// Token: 0x0400008F RID: 143
		public const string WebMethodStatic = "Web Service methods cannot be static.";

		// Token: 0x04000090 RID: 144
		public const string WebMethodMissingParams = "The last two parameters of method {0}.{1} must be of type {2} and {3}.";

		// Token: 0x04000091 RID: 145
		public const string WebBadOutParameter = "The {0} parameter of the {1}.{2} method must be an input-only parameter.";

		// Token: 0x04000092 RID: 146
		public const string WebInOutParameter = "The {0} parameter of the {1}.{2} method must be an output-only parameter.";

		// Token: 0x04000093 RID: 147
		public const string WebAsyncMissingEnd = "Asynchronous method {0}.{1} does not have a corresponding {2} method.";

		// Token: 0x04000094 RID: 148
		public const string WebMissingPath = "Path property must be set before calling the Send method.";

		// Token: 0x04000095 RID: 149
		public const string WebResponseKnownError = "The request failed with HTTP status {0}: {1}.";

		// Token: 0x04000096 RID: 150
		public const string WebResponseUnknownError = "The request failed with the error message:";

		// Token: 0x04000097 RID: 151
		public const string WebResponseUnknownErrorEmptyBody = "The request failed with an empty response.";

		// Token: 0x04000098 RID: 152
		public const string WebResponseContent = "Client found response content type of '{0}', but expected '{1}'.";

		// Token: 0x04000099 RID: 153
		public const string WebBadStreamState = "Properties and methods of the stream can't be accessed in the BeforeSerialize stage.";

		// Token: 0x0400009A RID: 154
		public const string WebResponseBadXml = "Response is not well-formed XML.";

		// Token: 0x0400009B RID: 155
		public const string WebCannotUnderstandHeader = "SOAP header {0} was not understood.";

		// Token: 0x0400009C RID: 156
		public const string WebMissingHeader = "Missing required header '{0}'.";

		// Token: 0x0400009D RID: 157
		public const string WebNoReturnValue = "There is no return value.";

		// Token: 0x0400009E RID: 158
		public const string WebCannotAccessValue = "The value can not be accessed because there was an exception during message processing.";

		// Token: 0x0400009F RID: 159
		public const string WebCannotAccessValueStage = "The value may not be accessed in message stage {0}.";

		// Token: 0x040000A0 RID: 160
		public const string WebInvalidBindingPlacement = "The Binding property of {0} may not be specified in this context.";

		// Token: 0x040000A1 RID: 161
		public const string WebInvalidBindingName = "The Binding property '{0}' does not match interface binding '{1}'.";

		// Token: 0x040000A2 RID: 162
		public const string WebBothMethodAttrs = "A web service method can't have both a SoapDocumentMethodAttribute and a SoapRpcMethodAttribute. Use one or the other.";

		// Token: 0x040000A3 RID: 163
		public const string WebBothServiceAttrs = "A web service can't have both a SoapDocumentServiceAttribute and a SoapRpcServiceAttribute. Use one or the other.";

		// Token: 0x040000A4 RID: 164
		public const string WebOneWayOutParameters = "One-way methods cannot have out or ref parameters.";

		// Token: 0x040000A5 RID: 165
		public const string WebOneWayReturnValue = "One-way methods cannot have return values.";

		// Token: 0x040000A6 RID: 166
		public const string WebReflectionErrorMethod = "Method {0}.{1} can not be reflected.";

		// Token: 0x040000A7 RID: 167
		public const string WebMultiDimArray = "Multi-dimensional arrays are not supported. Use a jagged array instead.";

		// Token: 0x040000A8 RID: 168
		public const string WebHeaderMissing = "The header property/field {0}.{1} is missing or not public.";

		// Token: 0x040000A9 RID: 169
		public const string WebHeaderStatic = "The header property/field {0}.{1} cannot be static.";

		// Token: 0x040000AA RID: 170
		public const string WebHeaderRead = "The header property/field {0}.{1} could not be  read.";

		// Token: 0x040000AB RID: 171
		public const string WebHeaderWrite = "The header property/field {0}.{1} could not be written to.";

		// Token: 0x040000AC RID: 172
		public const string WebHeaderType = "The header property/field {0}.{1} must be of type SoapHeader or a derived type, or an array of SoapHeader or SoapUnknownHeader.";

		// Token: 0x040000AD RID: 173
		public const string WebHeaderOneWayOut = "The header property/field {0}.{1} cannot be an out header because the method is one-way.";

		// Token: 0x040000AE RID: 174
		public const string WebHeaderInvalidMustUnderstand = "'{0}' is not a valid value for the field EncodedMustUnderstand. Valid values are '0', '1', 'true', and 'false'.";

		// Token: 0x040000AF RID: 175
		public const string WebMultiplyDeclaredHeaderTypes = "An operation can have only one in or out header of a given type. {0}.{1} conflicts with another header field of the same type and direction.";

		// Token: 0x040000B0 RID: 176
		public const string WebHttpHeader = "Server did not recognize the value of HTTP Header {0}: {1}.";

		// Token: 0x040000B1 RID: 177
		public const string WebRequestContent = "Server found request content type to be '{0}', but expected '{1}'.";

		// Token: 0x040000B2 RID: 178
		public const string WebRequestUnableToRead = "Server was unable to read request.";

		// Token: 0x040000B3 RID: 179
		public const string WebRequestUnableToProcess = "Server was unable to process request.";

		// Token: 0x040000B4 RID: 180
		public const string WebMissingParameter = "Missing parameter: {0}.";

		// Token: 0x040000B5 RID: 181
		public const string WebUnrecognizedRequestFormat = "Request format is unrecognized.";

		// Token: 0x040000B6 RID: 182
		public const string WebUnrecognizedRequestFormatUrl = "Request format is unrecognized for URL unexpectedly ending in '{0}'.";

		// Token: 0x040000B7 RID: 183
		public const string WebTimeout = "The timeout period expired before the response was received.";

		// Token: 0x040000B8 RID: 184
		public const string WebMissingHelpContext = "HttpContext is not available.   This class can only be used in the context of an ASP.NET request.";

		// Token: 0x040000B9 RID: 185
		public const string WebMissingCustomAttribute = "{0} must have the {1} custom attribute.";

		// Token: 0x040000BA RID: 186
		public const string WebMissingClientProtocol = "ClientProtocol must be set first.";

		// Token: 0x040000BB RID: 187
		public const string WebResolveMissingClientProtocol = "ClientProtocol property must be set before calling Resolve.";

		// Token: 0x040000BC RID: 188
		public const string WebPathNotFound = "Path:{0} could not be found.";

		// Token: 0x040000BD RID: 189
		public const string WebMissingResource = "The document at the url {0} was not recognized as a known document type.\\r\\nThe error message from each known type may help you fix the problem:";

		// Token: 0x040000BE RID: 190
		public const string WebContractReferenceName = "WSDL Document";

		// Token: 0x040000BF RID: 191
		public const string WebShemaReferenceName = "XML Schema";

		// Token: 0x040000C0 RID: 192
		public const string WebDiscoveryDocumentReferenceName = "DISCO Document";

		// Token: 0x040000C1 RID: 193
		public const string WebMissingDocument = "Discovery document at the URL {0} could not be found.";

		// Token: 0x040000C2 RID: 194
		public const string WebInvalidContentType = "The document format is not recognized (the content type is '{0}').";

		// Token: 0x040000C3 RID: 195
		public const string WebInvalidFormat = "The document format is not recognized.";

		// Token: 0x040000C4 RID: 196
		public const string WebInvalidEnvelopeNamespace = "Possible SOAP version mismatch: Envelope namespace {0} was unexpected. Expecting {1}.";

		// Token: 0x040000C5 RID: 197
		public const string WebResultNotXml = "Result was not XML.";

		// Token: 0x040000C6 RID: 198
		public const string WebDescriptionMissingItem = "Element {0} named {1} from namespace {2} is missing.";

		// Token: 0x040000C7 RID: 199
		public const string WebDescriptionMissing = "Cannot find definition for {0}.  Service Description with namespace {1} is missing.";

		// Token: 0x040000C8 RID: 200
		public const string WebDescriptionPartElementRequired = "Part '{0}' from message '{1}' in namespace '{2}' is missing the element attribute. The element attribute is required for headers when Use=Literal.";

		// Token: 0x040000C9 RID: 201
		public const string WebDescriptionPartTypeRequired = "Part '{0}' from message '{1}' in namespace '{2}' is missing the type attribute. The type attribute is required for headers when Use=Encoded.";

		// Token: 0x040000CA RID: 202
		public const string WebDescriptionPartElementWarning = "Part '{0}' from message '{1}' in namespace '{2}' defines both the element and type attributes. Only the element attribute is used for headers when Use=Literal.";

		// Token: 0x040000CB RID: 203
		public const string WebDescriptionPartTypeWarning = "Part '{0}' from message '{1}' in namespace '{2}' defines both the element and type attributes. Only the type attribute is used for headers when Use=Encoded.";

		// Token: 0x040000CC RID: 204
		public const string WebDescriptionTooManyMessages = "No more than one input and one output message may be specified.";

		// Token: 0x040000CD RID: 205
		public const string WebDescriptionHeaderAndBodyUseMismatch = "The value of use defined on the soap:header element must match that defined on the soap:body element.";

		// Token: 0x040000CE RID: 206
		public const string WebQNamePrefixUndefined = "Namespace prefix '{0}' not defined";

		// Token: 0x040000CF RID: 207
		public const string WebNegativeValue = "The value for '{0}' cannot be negative.";

		// Token: 0x040000D0 RID: 208
		public const string WebEmptyRef = "Invalid empty reference of type '{0}' encountered in the document at URI '{1}'.";

		// Token: 0x040000D1 RID: 209
		public const string WebNullRef = "Invalid null web reference encountered in the discovery document.";

		// Token: 0x040000D2 RID: 210
		public const string WebRefInvalidAttribute = "Invalid web reference encountered. Missing required '{0}' attribute.";

		// Token: 0x040000D3 RID: 211
		public const string WebRefInvalidAttribute2 = "Invalid empty reference of type '{0}' encountered. Missing required '{1}' attribute.";

		// Token: 0x040000D4 RID: 212
		public const string WebInvalidDocType = "Invalid Document type. Expecting '{0}', got '{1}'.\\r\\nDocument URI is '{2}'.";

		// Token: 0x040000D5 RID: 213
		public const string WebDiscoRefReport = "Report from '{0}' is '{1}'.";

		// Token: 0x040000D6 RID: 214
		public const string WebTextMatchMissingPattern = "Each match element must have a pattern attribute containing a valid regular expression.";

		// Token: 0x040000D7 RID: 215
		public const string WebTextMatchIgnoredTypeWarning = "Optional attribute 'type' was ignored because the match does not contain sub-matches.";

		// Token: 0x040000D8 RID: 216
		public const string WebTextMatchBadCaptureIndex = "{0} is not a valid capture index for match '{1}'. The highest valid capture index for this match is {2}.";

		// Token: 0x040000D9 RID: 217
		public const string WebTextMatchBadGroupIndex = "{0} is not a valid group index for match '{1}'. The highest valid group index for this match is {2}.";

		// Token: 0x040000DA RID: 218
		public const string WebServiceDescriptionIgnoredOptional = "The optional WSDL extension element '{0}' from namespace '{1}' was not handled.";

		// Token: 0x040000DB RID: 219
		public const string WebServiceDescriptionIgnoredRequired = "The required WSDL extension element '{0}' from namespace '{1}' was not handled.";

		// Token: 0x040000DC RID: 220
		public const string WebDuplicateFormatExtension = "More than one format extension named '{0}' was specified. Each format extension must have a unique name.";

		// Token: 0x040000DD RID: 221
		public const string WebDuplicateOperationMessage = "More than one operation message named '{0}' was specified. Each operation message must have a unique name.";

		// Token: 0x040000DE RID: 222
		public const string WebDuplicateImport = "More than one import named '{0}' was specified. Each import must have a unique name.";

		// Token: 0x040000DF RID: 223
		public const string WebDuplicateMessage = "More than one message named '{0}' was specified. Each message must have a unique name.";

		// Token: 0x040000E0 RID: 224
		public const string WebDuplicatePort = "More than one port named '{0}' was specified. Each port must have a unique name.";

		// Token: 0x040000E1 RID: 225
		public const string WebDuplicatePortType = "More than one port type named '{0}' was specified. Each port type must have a unique name.";

		// Token: 0x040000E2 RID: 226
		public const string WebDuplicateBinding = "More than one binding named '{0}' was specified. Each binding must have a unique name.";

		// Token: 0x040000E3 RID: 227
		public const string WebDuplicateService = "More than one service named '{0}' was specified. Each service must have a unique name.";

		// Token: 0x040000E4 RID: 228
		public const string WebDuplicateMessagePart = "More than one message part named '{0}' was specified. Each message part must have a unique name.";

		// Token: 0x040000E5 RID: 229
		public const string WebDuplicateOperationBinding = "More than one operation binding named '{0}' was specified. Each operation binding must have a unique name.";

		// Token: 0x040000E6 RID: 230
		public const string WebDuplicateFaultBinding = "More than one fault binding named '{0}' was specified. Each fault binding must have a unique name.";

		// Token: 0x040000E7 RID: 231
		public const string WebDuplicateOperation = "More than one operation named '{0}' was specified. Each operation must have a unique name.";

		// Token: 0x040000E8 RID: 232
		public const string WebDuplicateOperationFault = "More than one operation fault named '{0}' was specified. Each operation fault must have a unique name.";

		// Token: 0x040000E9 RID: 233
		public const string WebDuplicateUnknownElement = "More than one {0} named '{1}' was specified. Each {0} must have a unique name.";

		// Token: 0x040000EA RID: 234
		public const string WebUnknownEncodingStyle = "The encoding style '{0}' is not supported.";

		// Token: 0x040000EB RID: 235
		public const string WebSoap11EncodingStyleNotSupported1 = "The SOAP 1.1 encoding style is not supported for operations that use SOAP 1.2. Use the URI '{0}' to refer to the SOAP 1.2 encoding style.";

		// Token: 0x040000EC RID: 236
		public const string WebNullAsyncResultInBegin = "For server-side asynchronous method calls, your Begin method must return a valid IAsyncResult. Your Begin method cannot return null.";

		// Token: 0x040000ED RID: 237
		public const string WebNullAsyncResultInEnd = "For asynchronous method calls, you must supply a non-null IAsyncResult instance when you call the End method.";

		// Token: 0x040000EE RID: 238
		public const string WebAsyncTransaction = "Transactions are not supported for asynchronous method calls. Provide a synchronous version of the method or unset the TransactionOption property of the WebMethodAttribute.";

		// Token: 0x040000EF RID: 239
		public const string WebConfigExtensionError = "There was an exception running the extensions specified in the config file.";

		// Token: 0x040000F0 RID: 240
		public const string WebExtensionError = "There was an exception running the extensions specified in the config file (or via an attribute).";

		// Token: 0x040000F1 RID: 241
		public const string WebChangeTypeFailed = "Cannot convert {0} to {1}.";

		// Token: 0x040000F2 RID: 242
		public const string WebClientBindingAttributeRequired = "WebServiceBindingAttribute is required on proxy classes.";

		// Token: 0x040000F3 RID: 243
		public const string WebHeaderInvalidRelay = "'{0}' is not a valid value for the 'relay' attribute. Valid values are '0', '1', 'true', and 'false'.";

		// Token: 0x040000F4 RID: 244
		public const string WebVirtualDisoRoot = "Web Directory '{0}' must be same or below to '{1}'.";

		// Token: 0x040000F5 RID: 245
		public const string WebWsiContentTypeEncoding = "Input message does not conform to Simple SOAP Binding Profile Version 1.0. Requirement R1012: A MESSAGE MUST serialize the envelope using either UTF-8 or UTF-16 character encoding.";

		// Token: 0x040000F6 RID: 246
		public const string WebWsiViolation = "Service '{0}' does not conform to WS-I Basic Profile v1.1. Please examine each of the normative statement violations below. To turn off conformance check set the ConformanceClaims property on corresponding WebServiceBinding attribute to WsiClaims.None.\\r\\n{1}";

		// Token: 0x040000F7 RID: 247
		public const string WebNullReaderForMessage = "The GetReaderForMessage method cannot return null.";

		// Token: 0x040000F8 RID: 248
		public const string WebNullWriterForMessage = "The GetWriterForMessage method cannot return null.";

		// Token: 0x040000F9 RID: 249
		public const string NeedConcreteType = "Cannot create an instance of an interface {0}. Please specify a concrete implementation of the interface instead.";

		// Token: 0x040000FA RID: 250
		public const string WebUnknownElement = "The element was not expected in this context: {0}.";

		// Token: 0x040000FB RID: 251
		public const string WebUnknownElement1 = "An unexpected element was encountered: {0}. No elements were expected in this scope.";

		// Token: 0x040000FC RID: 252
		public const string WebUnknownElement2 = "The element was not expected in this context: {0}. Expected elements: {1}.";

		// Token: 0x040000FD RID: 253
		public const string WebUnknownAttribute = "An unexpected attribute was encountered: {0}='{1}'.";

		// Token: 0x040000FE RID: 254
		public const string WebUnknownAttribute2 = "An unexpected attribute was encountered: {0}='{1}'. No attributes were expected in this scope.";

		// Token: 0x040000FF RID: 255
		public const string WebUnknownAttribute3 = "The attribute was not expected in this context: {0}='{1}'. Expected attributes: {2}.";

		// Token: 0x04000100 RID: 256
		public const string WebSuppressedExceptionMessage = "An error occurred on the server.";

		// Token: 0x04000101 RID: 257
		public const string CannotRunInPartialTrustOutsideAspNet = "Cannot run in partial trust outside the ASP.NET environment.";

		// Token: 0x04000102 RID: 258
		public const string WebServiceContext = "The ASP.NET context object for the current request.";

		// Token: 0x04000103 RID: 259
		public const string WebServiceSession = "The ASP.NET session object for the current request.";

		// Token: 0x04000104 RID: 260
		public const string WebServiceServer = "The ASP.NET utility object for the current request.";

		// Token: 0x04000105 RID: 261
		public const string WebServiceUser = "The ASP.NET user object for the current request.  The object is used for authorization.";

		// Token: 0x04000106 RID: 262
		public const string WebServiceSoapVersion = "The version of the SOAP protocol used for the request.";

		// Token: 0x04000107 RID: 263
		public const string ClientProtocolAllowAutoRedirect = "Enable automatic handling of server redirects.";

		// Token: 0x04000108 RID: 264
		public const string ClientProtocolCookieContainer = "A container for all cookies received from servers in the current session.";

		// Token: 0x04000109 RID: 265
		public const string ClientProtocolPreAuthenticate = "Enables pre authentication of the request.";

		// Token: 0x0400010A RID: 266
		public const string ClientProtocolClientCertificates = "The client certificates that will be sent to the server, if the server requests them.";

		// Token: 0x0400010B RID: 267
		public const string ClientProtocolUrl = "The base URL to the server to use for requests.";

		// Token: 0x0400010C RID: 268
		public const string ClientProtocolEncoding = "The encoding to use for requests.";

		// Token: 0x0400010D RID: 269
		public const string ClientProtocolTimeout = "Sets the timeout in milliseconds to be used for synchronous calls.  The default of -1 means infinite.";

		// Token: 0x0400010E RID: 270
		public const string ClientProtocolUserAgent = "Sets the user agent http header for the request.";

		// Token: 0x0400010F RID: 271
		public const string ClientProtocolSoapVersion = "The version of the SOAP protocol to use for requests. By default, SOAP 1.1 will be used.";

		// Token: 0x04000110 RID: 272
		public const string ClientProtocolEnableDecompression = "This allows user to easily leverage HTTP compression that's built into IIS 6.";

		// Token: 0x04000111 RID: 273
		public const string XmlLang = "en";

		// Token: 0x04000112 RID: 274
		public const string Rxxxx = "SOAP 1.1 binding was not found";

		// Token: 0x04000113 RID: 275
		public const string BindingMissingAttribute = "Binding '{0}' from namespace '{1}' missing required '{2}' attribute.";

		// Token: 0x04000114 RID: 276
		public const string BindingInvalidAttribute = "Binding '{0}' from namespace '{1}' has invalid attribute: {2}='{3}'.";

		// Token: 0x04000115 RID: 277
		public const string OperationFlowNotification = "Operation '{0}' on portType '{1}' from namespace '{2}' defined as Notification.";

		// Token: 0x04000116 RID: 278
		public const string OperationFlowSolicitResponse = "Operation '{0}' on portType '{1}' from namespace '{2}' defined as Solicit-Response.";

		// Token: 0x04000117 RID: 279
		public const string PortTypeOperationMissing = "Operation '{0}' on binding '{1}' from namespace '{2}' has no matching operation on portType '{3}' from namespace '{4}'.";

		// Token: 0x04000118 RID: 280
		public const string BindingMultipleParts = "One of the extensibility elements on Binding '{0}' from namespace '{1}' has more then one part listed in the '{2}' attribute.";

		// Token: 0x04000119 RID: 281
		public const string InputElement = "Input element {0} of operation '{1}' on portType '{2}' from namespace '{3}'. {4}";

		// Token: 0x0400011A RID: 282
		public const string OutputElement = "Output element {0} of operation '{1}' on portType '{2}' from namespace '{3}'. {4}";

		// Token: 0x0400011B RID: 283
		public const string Fault = "soapbind:fault '{0}' on operation '{1}' on portType '{2}' from namespace '{3}'. {4}";

		// Token: 0x0400011C RID: 284
		public const string HeaderFault = "{0} on operation '{1}' on portType '{2}' from namespace '{3}'. {4}";

		// Token: 0x0400011D RID: 285
		public const string Binding = "Binding '{0}' from namespace '{1}'.";

		// Token: 0x0400011E RID: 286
		public const string Operation = "Operation '{0}' on portType '{1}' from namespace '{2}'.";

		// Token: 0x0400011F RID: 287
		public const string OperationBinding = "Operation '{0}' on binding '{1}' from namespace '{2}'.";

		// Token: 0x04000120 RID: 288
		public const string FaultBinding = "Fault '{0}' on operation '{1}' on binding '{2}' from namespace '{3}'.";

		// Token: 0x04000121 RID: 289
		public const string Description = "Service description with targetNamespace='{0}'.";

		// Token: 0x04000122 RID: 290
		public const string Element = "{0} from service description with targetNamespace='{1}'.";

		// Token: 0x04000123 RID: 291
		public const string Port = "Port '{0}' on service {1} from namespace='{2}'.";

		// Token: 0x04000124 RID: 292
		public const string Part = "Part '{0}' of message '{1}' from service description with targetNamespace='{2}'.";

		// Token: 0x04000125 RID: 293
		public const string OperationMissingBinding = "Operation '{0}' on portType '{1}' from namespace '{2}' has no matching binding.";

		// Token: 0x04000126 RID: 294
		public const string UriValueRelative = "Value '{0}' is not an absolute URI.";

		// Token: 0x04000127 RID: 295
		public const string HelpGeneratorInternalError = "The XML Web service help page encountered an internal error.";

		// Token: 0x04000128 RID: 296
		public const string OperationOverload = "Binding '{0}' from namespace '{1}' contains operations with matching wire signatures. Wire signature of message '{2}' matches message '{3}'.";

		// Token: 0x04000129 RID: 297
		public const string WireSignature = "Input message '{0}' from namespace '{1}' has wire signature '{2}:{3}'.";

		// Token: 0x0400012A RID: 298
		public const string WireSignatureEmpty = "Input message '{0}' from namespace '{1}' has no elements (empty wire signature)";

		// Token: 0x0400012B RID: 299
		public const string WsdlInstanceValidationDetails = "Warning: {0} Line {1}, position {2}.";

		// Token: 0x0400012C RID: 300
		public const string WhenUsingAMessageStyleOfParametersAsDocument0 = "When using a Bare parameter style and a service RoutingStyle of RequestElement, the method must have exactly one parameter.";

		// Token: 0x0400012D RID: 301
		public const string TheMethodsAndUseTheSameSoapActionWhenTheService3 = "The methods {0} and {1} use the same SOAPAction '{2}'. When the RoutingStyle of the XML Web service is SoapAction, SOAPAction values must be unique across methods on the XML Web service. You can change the SOAPAction with the Action parameter to the SoapDocumentMethod or SoapRpcMethod attributes, or you can specify a RoutingStyle of RequestElement on the XML Web service.";

		// Token: 0x0400012E RID: 302
		public const string TheMethodDoesNotHaveARequestElementEither1 = "The method {0} does not have a request element. You can use a RoutingStyle of SoapAction on the XML Web service, use the Wrapped parameter style on the method, or add a parameter to the method.";

		// Token: 0x0400012F RID: 303
		public const string TheMethodsAndUseTheSameRequestElementXmlns4 = "The methods {0} and {1} use the same request element <{2} xmlns='{3}'>. When the RoutingStyle of the XML Web service is RequestElement, request elements must be unique across methods on the XML Web service. If you are using the Rpc style, you can change the message name with the RequestElementName and RequestElementNamespace parameters to SoapRpcMethodAttribute. For document style using wrapped parameter style, use the RequestElementName and RequestElementNamespace parameters to SoapDocumentMethodAttribute. For the bare parameter style, you must change the element that represents the parameter. Alternatively, you can specify a RoutingStyle of SoapAction on the XML Web service.";

		// Token: 0x04000130 RID: 304
		public const string TheMethodsAndUseTheSameRequestElementAndSoapActionXmlns6 = "The methods {0} and {1} use the same request element <{2} xmlns='{3}'> and the methods {0} and {4} use the same SOAPAction '{5}'. Each method on the XML Web service must have a unique SOAPAction or request element. If you are using the Rpc style, you can change the message name with the RequestElementName and RequestElementNamespace parameters to SoapRpcMethodAttribute. For document style using wrapped parameter style, use the RequestElementName and RequestElementNamespace parameters to SoapDocumentMethodAttribute. For the bare parameter style, you must change the element that represents the parameter. Alternatively, you can specify a unique SoapAction for each method using the Action parameter to SoapRpcMethodAttribute or SoapDocumentMethodAttribute.";

		// Token: 0x04000131 RID: 305
		public const string TheRootElementForTheRequestCouldNotBeDetermined0 = "The root element for the request could not be determined. When RoutingStyle is set to RequestElement, SoapExtensions configured via an attribute on the method cannot modify the request stream before it is read. The extension must be configured via the SoapExtensionTypes element in web.config, or the request must arrive at the server as clear text.";

		// Token: 0x04000132 RID: 306
		public const string TheRequestElementXmlnsWasNotRecognized2 = "The request element <{0} xmlns='{1}'> was not recognized.";

		// Token: 0x04000133 RID: 307
		public const string ServiceDescriptionWasNotFound0 = "XML Web service description was not found.";

		// Token: 0x04000134 RID: 308
		public const string internalError0 = "An internal error occurred.";

		// Token: 0x04000135 RID: 309
		public const string DiscoveryIsNotPossibleBecauseTypeIsMissing1 = "Discovery is not possible because type '{0}' is missing a WebServiceBinding attribute.";

		// Token: 0x04000136 RID: 310
		public const string TheBindingNamedFromNamespaceWasNotFoundIn3 = "The binding named '{0}' from namespace '{1}' was not found in the discovery document found at '{2}'.";

		// Token: 0x04000137 RID: 311
		public const string Missing2 = "Missing {0}.{1}";

		// Token: 0x04000138 RID: 312
		public const string MissingHttpOperationElement0 = "Missing http:operation element.";

		// Token: 0x04000139 RID: 313
		public const string MessageHasNoParts1 = "Message '{0}' has no parts.";

		// Token: 0x0400013A RID: 314
		public const string DuplicateInputOutputNames0 = "Duplicate input/output names.";

		// Token: 0x0400013B RID: 315
		public const string MissingBinding0 = "The operation has no matching binding. Check if the operation, input and output names in the Binding section match with the corresponding names in the PortType section.";

		// Token: 0x0400013C RID: 316
		public const string MissingInputBinding0 = "Missing input binding.";

		// Token: 0x0400013D RID: 317
		public const string MissingOutputBinding0 = "Missing output binding.";

		// Token: 0x0400013E RID: 318
		public const string UnableToImportOperation1 = "Unable to import operation '{0}'.";

		// Token: 0x0400013F RID: 319
		public const string UnableToImportBindingFromNamespace2 = "Unable to import binding '{0}' from namespace '{1}'.";

		// Token: 0x04000140 RID: 320
		public const string TheOperationFromNamespaceHadInvalidSyntax3 = "The operation '{0}' on portType '{1}' from namespace '{2}' had the following syntax error:  {3}";

		// Token: 0x04000141 RID: 321
		public const string TheOperationBindingFromNamespaceHadInvalid3 = "The operation binding '{0}' from namespace '{1}' had invalid syntax.  {2}";

		// Token: 0x04000142 RID: 322
		public const string IfAppSettingBaseUrlArgumentIsSpecifiedThen0 = "If 'appSettingBaseUrl' argument is specified then 'url' argument must also be specified.";

		// Token: 0x04000143 RID: 323
		public const string MissingMessagePartForMessageFromNamespace3 = "Missing message part '{0}' for message '{1}' from namespace '{2}'.";

		// Token: 0x04000144 RID: 324
		public const string MissingMessage2 = "Missing message '{0}' from namespace '{1}'.";

		// Token: 0x04000145 RID: 325
		public const string OnlyXmlElementsOrTypesDerivingFromServiceDescriptionFormatExtension0 = "Only XmlElements or types deriving from ServiceDescriptionFormatExtension may be added.";

		// Token: 0x04000146 RID: 326
		public const string OnlyOperationInputOrOperationOutputTypes = "Only objects of type OperationInput or OperationOutput may be added.";

		// Token: 0x04000147 RID: 327
		public const string ProtocolWithNameIsNotRecognized1 = "Protocol with name '{0}' is not recognized.";

		// Token: 0x04000148 RID: 328
		public const string BothAndUseTheMessageNameUseTheMessageName3 = "Both {0} and {1} use the message name '{2}'.  Use the MessageName property of the WebMethod custom attribute to specify unique message names for the methods.";

		// Token: 0x04000149 RID: 329
		public const string MissingSoapOperationBinding0 = "Missing soap:operation binding.";

		// Token: 0x0400014A RID: 330
		public const string OnlyOneWebServiceBindingAttributeMayBeSpecified1 = "Only one WebServiceBinding attribute may be specified on type '{0}'.";

		// Token: 0x0400014B RID: 331
		public const string ContractOverride = "Method {0} on type {1} is an implementation of the service contract interface {2}. Service Description altering attributes cannot be specified on service implementation.  Please place custom attribute on the interface declaration: interface: {2}, method: {3}, attribute: {4}.";

		// Token: 0x0400014C RID: 332
		public const string TypeIsMissingWebServiceBindingAttributeThat2 = "Type '{0}' is missing WebServiceBinding attribute that defines a binding named '{1}'.";

		// Token: 0x0400014D RID: 333
		public const string MultipleBindingsWithSameName2 = "Type '{0}' has multiple WebServiceBinding attributes that define a binding named '{1}'.";

		// Token: 0x0400014E RID: 334
		public const string RequiredXmlFormatExtensionAttributeIsMissing1 = "Required XmlFormatExtension attribute is missing on class {0}.";

		// Token: 0x0400014F RID: 335
		public const string TheSyntaxOfTypeMayNotBeExtended1 = "The syntax of type {0} may not be extended.";

		// Token: 0x04000150 RID: 336
		public const string ThereIsNoSoapTransportImporterThatUnderstands1 = "There is no SoapTransportImporter that understands the transport '{0}'.";

		// Token: 0x04000151 RID: 337
		public const string MissingSoapBodyInputBinding0 = "Missing soap:body input binding.";

		// Token: 0x04000152 RID: 338
		public const string MissingSoapBodyOutputBinding0 = "Missing soap:body output binding.";

		// Token: 0x04000153 RID: 339
		public const string TheEncodingIsNotSupported1 = "The encoding '{0}' is not supported.";

		// Token: 0x04000154 RID: 340
		public const string SpecifyingAnElementForUseEncodedMessageParts0 = "Specifying an element for use=encoded message parts is not supported.";

		// Token: 0x04000155 RID: 341
		public const string EachMessagePartInAnUseEncodedMessageMustSpecify0 = "Each message part in an use=encoded message must specify a type.";

		// Token: 0x04000156 RID: 342
		public const string SpecifyingATypeForUseLiteralMessagesIs0 = "Specifying a type for use=literal messages is not supported.";

		// Token: 0x04000157 RID: 343
		public const string SpecifyingATypeForUseLiteralMessagesIsAny = "Specifying a type for use=literal messages is not supported. Type name='{0}' from targetNamespace='{1}' cannot be used as top-level any element.";

		// Token: 0x04000158 RID: 344
		public const string EachMessagePartInAUseLiteralMessageMustSpecify0 = "Each message part in a use=literal message must specify an element.";

		// Token: 0x04000159 RID: 345
		public const string EachMessagePartInRpcUseLiteralMessageMustSpecify0 = "Each message part in a use=literal style=rpc message must specify a type.";

		// Token: 0x0400015A RID: 346
		public const string NoInputMIMEFormatsWereRecognized0 = "No input MIME formats were recognized.";

		// Token: 0x0400015B RID: 347
		public const string NoInputHTTPFormatsWereRecognized0 = "No input HTTP formats were recognized.";

		// Token: 0x0400015C RID: 348
		public const string NoOutputMIMEFormatsWereRecognized0 = "No output MIME formats were recognized.";

		// Token: 0x0400015D RID: 349
		public const string MissingMatchElement0 = "Missing match element.";

		// Token: 0x0400015E RID: 350
		public const string SolicitResponseIsNotSupported0 = "SolicitResponse is not supported.";

		// Token: 0x0400015F RID: 351
		public const string RequestResponseIsNotSupported0 = "RequestResponse is not supported.";

		// Token: 0x04000160 RID: 352
		public const string OneWayIsNotSupported0 = "OneWay is not supported.";

		// Token: 0x04000161 RID: 353
		public const string NotificationIsNotSupported0 = "Notification is not supported.";

		// Token: 0x04000162 RID: 354
		public const string WebMissingBodyElement = "Request format is invalid: Missing required soap:Body element.";

		// Token: 0x04000163 RID: 355
		public const string WebMissingEnvelopeElement = "Request format is invalid: Missing required soap:Envelope element.";

		// Token: 0x04000164 RID: 356
		public const string UnableToHandleRequestActionNotRecognized1 = "Unable to handle request. The action '{0}' was not recognized.";

		// Token: 0x04000165 RID: 357
		public const string UnableToHandleRequestActionRequired0 = "Unable to handle request without a valid action parameter. Please supply a valid soap action.";

		// Token: 0x04000166 RID: 358
		public const string UnableToHandleRequest0 = "Unable to handle request.";

		// Token: 0x04000167 RID: 359
		public const string FailedToHandleRequest0 = "Failed to handle request.";

		// Token: 0x04000168 RID: 360
		public const string CodeGenSupportReferenceParameters = "Declaring parameters to be 'out' or 'ref' is not supported by {0}.";

		// Token: 0x04000169 RID: 361
		public const string CodeGenSupportParameterAttributes = "Custom attributes on parameter declarations are not supported by {0}.";

		// Token: 0x0400016A RID: 362
		public const string CodeGenSupportReturnTypeAttributes = "Custom attributes on return value are not supported by {0}.";

		// Token: 0x0400016B RID: 363
		public const string TheBinding0FromNamespace1WasIgnored2 = "The binding '{0}' from namespace '{1}' was ignored.  {2}";

		// Token: 0x0400016C RID: 364
		public const string TheOperation0FromNamespace1WasIgnored2 = "The operation '{0}' from namespace '{1}' was ignored.  {2}";

		// Token: 0x0400016D RID: 365
		public const string TheOperationBinding0FromNamespace1WasIgnored = "The operation binding '{0}' from namespace '{1}' was ignored.  {2}";

		// Token: 0x0400016E RID: 366
		public const string NoMethodsWereFoundInTheWSDLForThisProtocol = "No methods were found in the WSDL for this protocol.";

		// Token: 0x0400016F RID: 367
		public const string ThereWasAnErrorDuringAsyncProcessing = "There was an error during async processing.";

		// Token: 0x04000170 RID: 368
		public const string CanTCallTheEndMethodOfAnAsyncCallMoreThan = "Can't call the end method of an async call more than once.";

		// Token: 0x04000171 RID: 369
		public const string AsyncDuplicateUserState = "There was an error during asynchronous processing. Unique state object is required for multiple asynchronous simultaneous operations to be outstanding.";

		// Token: 0x04000172 RID: 370
		public const string StreamDoesNotSeek = "This stream does not support seeking operations.";

		// Token: 0x04000173 RID: 371
		public const string StreamDoesNotRead = "This stream does not support read operations.";

		// Token: 0x04000174 RID: 372
		public const string ElementTypeMustBeObjectOrSoapReflectedException = "The elementType must be one of SoapReflectedExtension or Object.";

		// Token: 0x04000175 RID: 373
		public const string ElementTypeMustBeObjectOrSoapExtensionOrSoapReflectedException = "The elementType must be one of SoapExtension, SoapReflectedExtension, or Object.";

		// Token: 0x04000176 RID: 374
		public const string ProtocolDoesNotAsyncSerialize = "The protocol does not support async serialization.";

		// Token: 0x04000177 RID: 375
		public const string ThereWasAnErrorDownloading0 = "There was an error downloading '{0}'.";

		// Token: 0x04000178 RID: 376
		public const string TheHTMLDocumentDoesNotContainDiscoveryInformation = "The HTML document does not contain Web service discovery information.";

		// Token: 0x04000179 RID: 377
		public const string TheDocumentWasNotRecognizedAsAKnownDocumentType = "The document was not recognized as a known document type (WSDL, XML Schema, or Discovery document) for the following reason: \\n  - {0}";

		// Token: 0x0400017A RID: 378
		public const string TheDocumentWasUnderstoodButContainsErrors = "The document was understood, but it could not be processed.";

		// Token: 0x0400017B RID: 379
		public const string TheWSDLDocumentContainsLinksThatCouldNotBeResolved = "The WSDL document contains links that could not be resolved.";

		// Token: 0x0400017C RID: 380
		public const string TheSchemaDocumentContainsLinksThatCouldNotBeResolved = "The XML schema document contains links that could not be resolved.";

		// Token: 0x0400017D RID: 381
		public const string CanTSpecifyElementOnEncodedMessagePartsPart = "The element attribute is not allowed on encoded message parts. The erroneous part is named '{0}' in message '{1}'.";

		// Token: 0x0400017E RID: 382
		public const string indexMustBeBetweenAnd0Inclusive = "index must be between 0 and {0}, inclusive.";

		// Token: 0x0400017F RID: 383
		public const string BPConformanceHeaderFault = "WebMethod {0} on WebService {1} claims conformance. Using SOAP header with {2}={3}.{4} is not allowed on conformant methods. ";

		// Token: 0x04000180 RID: 384
		public const string WsdlGenRpcLitAnonimousType = "Method {0}.{1} can not be reflected. Parameter '{2}' may not be used with Rpc\\\\Literal SOAP messages because it is declared using an anonymous schema type, only top-level named types can be used with rpc\\\\literal SOAP messages.";

		// Token: 0x04000181 RID: 385
		public const string StackTraceEnd = "--- End of inner exception stack trace ---";

		// Token: 0x04000182 RID: 386
		public const string CodeRemarks = "<remarks/>";

		// Token: 0x04000183 RID: 387
		public const string CodegenWarningDetails = "CODEGEN: {0}";

		// Token: 0x04000184 RID: 388
		public const string SchemaValidationError = "Schema validation error: {0}";

		// Token: 0x04000185 RID: 389
		public const string SchemaValidationWarning = "Schema validation warning: {0}";

		// Token: 0x04000186 RID: 390
		public const string SchemaSyntaxErrorDetails = "Schema with targetNamespace='{0}' has invalid syntax. {1} Line {2}, position {3}.";

		// Token: 0x04000187 RID: 391
		public const string InitFailed = "Initialization failure. Please review input options and documents for validity.";

		// Token: 0x04000188 RID: 392
		public const string XmlSchemaElementReference = "Element reference '{0}' declared in schema type '{1}' from namespace '{2}'.";

		// Token: 0x04000189 RID: 393
		public const string XmlSchemaAttributeReference = "Attribute reference '{0}' declared in schema type '{1}' from namespace '{2}'.";

		// Token: 0x0400018A RID: 394
		public const string XmlSchemaItem = "Schema item '{1}' from namespace '{0}'. {2}";

		// Token: 0x0400018B RID: 395
		public const string XmlSchemaNamedItem = "Schema item '{1}' named '{2}' from namespace '{0}' is invalid. {3}";

		// Token: 0x0400018C RID: 396
		public const string XmlSchemaContentDef = "Check content definition of schema type '{0}' from namespace '{1}'. {2}";

		// Token: 0x0400018D RID: 397
		public const string XmlSchema = "Schema with targetNamespace='{0}' has invalid syntax. {1}";

		// Token: 0x0400018E RID: 398
		public const string TraceCallEnter = "Calling {0}\\r\\n    Caller: {1}";

		// Token: 0x0400018F RID: 399
		public const string TraceCallEnterDetails = "Calling {0}\\r\\n    Method: {2}\\r\\n    Caller: {1}";

		// Token: 0x04000190 RID: 400
		public const string TraceCallExit = "Return from {0}\\r\\n    Caller: {1}";

		// Token: 0x04000191 RID: 401
		public const string TraceExceptionThrown = "Exception thrown in {0}.\\r\\n    {1}: {2}";

		// Token: 0x04000192 RID: 402
		public const string TraceExceptionCought = "Exception caught in {0}.\\r\\n    {1}: {2}";

		// Token: 0x04000193 RID: 403
		public const string TraceExceptionIgnored = "Exception ignored in {0}.\\r\\n    {1}: {2}";

		// Token: 0x04000194 RID: 404
		public const string TraceExceptionDetails = "Exception Details:\\r\\n{0}";

		// Token: 0x04000195 RID: 405
		public const string TracePostWorkItemIn = "Calling WorkItem.Post({0})";

		// Token: 0x04000196 RID: 406
		public const string TracePostWorkItemOut = "Return from WorkItem.Post({0})";

		// Token: 0x04000197 RID: 407
		public const string TraceUserHostName = "Request Host Name: {0}";

		// Token: 0x04000198 RID: 408
		public const string TraceUserHostAddress = "Request Host Address: {0}";

		// Token: 0x04000199 RID: 409
		public const string TraceUrl = "Request Url: [{0}] {1}";

		// Token: 0x0400019A RID: 410
		public const string TraceUrlReferrer = "Http referrer: {0}";

		// Token: 0x0400019B RID: 411
		public const string TraceCreateSerializer = "XmlSerializer [Create XmlSerializer]";

		// Token: 0x0400019C RID: 412
		public const string TraceWriteRequest = "XmlSerializer [Write Request]";

		// Token: 0x0400019D RID: 413
		public const string TraceWriteResponse = "XmlSerializer [Write Response]";

		// Token: 0x0400019E RID: 414
		public const string TraceWriteHeaders = "XmlSerializer [Write SOAP Headers]";

		// Token: 0x0400019F RID: 415
		public const string TraceReadRequest = "XmlSerializer [Read Request]";

		// Token: 0x040001A0 RID: 416
		public const string TraceReadResponse = "XmlSerializer [Read Response]";

		// Token: 0x040001A1 RID: 417
		public const string TraceReadHeaders = "XmlSerializer [Read SOAP Headers]";
	}
}
