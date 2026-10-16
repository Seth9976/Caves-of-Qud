using System;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Web.Caching
{
	/// <summary>Represents a managed delegate that can be called to insert dynamically generated output into an output-cache response. </summary>
	// Token: 0x02000691 RID: 1681
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Unrestricted)]
	[Serializable]
	public class SubstitutionResponseElement : ResponseElement
	{
		/// <summary>Gets a reference to the substitution callback method.</summary>
		/// <returns>A callback method reference.</returns>
		// Token: 0x17001618 RID: 5656
		// (get) Token: 0x0600477A RID: 18298 RVA: 0x000C731D File Offset: 0x000C551D
		// (set) Token: 0x0600477B RID: 18299 RVA: 0x000C7325 File Offset: 0x000C5525
		public HttpResponseSubstitutionCallback Callback { get; private set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.SubstitutionResponseElement" /> class.</summary>
		/// <param name="callback">The static substitution callback that was set as part of the response for an output-cached page.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callback" /> is null. </exception>
		// Token: 0x0600477C RID: 18300 RVA: 0x000C7330 File Offset: 0x000C5530
		public SubstitutionResponseElement(HttpResponseSubstitutionCallback callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			this.Callback = callback;
			MethodInfo method = callback.Method;
			this.typeName = method.DeclaringType.AssemblyQualifiedName;
			this.methodName = method.Name;
		}

		// Token: 0x0600477D RID: 18301 RVA: 0x000C737C File Offset: 0x000C557C
		[OnDeserialized]
		private void ObjectDeserialized(StreamingContext context)
		{
			Type type = Type.GetType(this.typeName, true);
			this.Callback = Delegate.CreateDelegate(typeof(HttpResponseSubstitutionCallback), type, this.methodName, false, true) as HttpResponseSubstitutionCallback;
		}

		// Token: 0x040025BE RID: 9662
		private string typeName;

		// Token: 0x040025BF RID: 9663
		private string methodName;
	}
}
