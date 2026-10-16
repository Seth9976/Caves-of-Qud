using System;

namespace System.Web.Util
{
	// Token: 0x02000116 RID: 278
	internal static class ExceptionUtil
	{
		// Token: 0x06000DE9 RID: 3561 RVA: 0x0002563C File Offset: 0x0002383C
		internal static ArgumentException ParameterInvalid(string parameter)
		{
			return new ArgumentException(global::SR.GetString("The parameter '{0}' is invalid.", new object[] { parameter }), parameter);
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x00025658 File Offset: 0x00023858
		internal static ArgumentException ParameterNullOrEmpty(string parameter)
		{
			return new ArgumentException(global::SR.GetString("The string parameter '{0}' cannot be null or empty.", new object[] { parameter }), parameter);
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x00025674 File Offset: 0x00023874
		internal static ArgumentException PropertyInvalid(string property)
		{
			return new ArgumentException(global::SR.GetString("The value assigned to property '{0}' is invalid.", new object[] { property }), property);
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x00025690 File Offset: 0x00023890
		internal static ArgumentException PropertyNullOrEmpty(string property)
		{
			return new ArgumentException(global::SR.GetString("The value assigned to property '{0}' cannot be null or empty.", new object[] { property }), property);
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x000256AC File Offset: 0x000238AC
		internal static InvalidOperationException UnexpectedError(string methodName)
		{
			return new InvalidOperationException(global::SR.GetString("An unexpected error occurred in '{0}'.", new object[] { methodName }));
		}
	}
}
