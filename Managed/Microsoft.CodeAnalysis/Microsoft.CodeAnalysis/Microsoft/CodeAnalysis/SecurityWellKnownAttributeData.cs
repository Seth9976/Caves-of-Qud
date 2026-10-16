using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CodeGen;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000233 RID: 563
	internal sealed class SecurityWellKnownAttributeData
	{
		// Token: 0x060015F2 RID: 5618 RVA: 0x00045374 File Offset: 0x00043574
		public void SetSecurityAttribute(int attributeIndex, DeclarativeSecurityAction action, int totalSourceAttributes)
		{
			if (this._lazySecurityActions == null)
			{
				Interlocked.CompareExchange<byte[]>(ref this._lazySecurityActions, new byte[totalSourceAttributes], null);
			}
			this._lazySecurityActions[attributeIndex] = (byte)action;
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x0004539B File Offset: 0x0004359B
		public void SetPathForPermissionSetAttributeFixup(int attributeIndex, string resolvedFilePath, int totalSourceAttributes)
		{
			if (this._lazyPathsForPermissionSetFixup == null)
			{
				Interlocked.CompareExchange<string[]>(ref this._lazyPathsForPermissionSetFixup, new string[totalSourceAttributes], null);
			}
			this._lazyPathsForPermissionSetFixup[attributeIndex] = resolvedFilePath;
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x000453C1 File Offset: 0x000435C1
		public IEnumerable<SecurityAttribute> GetSecurityAttributes<T>(ImmutableArray<T> customAttributes) where T : ICustomAttribute
		{
			if (this._lazySecurityActions != null)
			{
				int num;
				for (int i = 0; i < customAttributes.Length; i = num + 1)
				{
					if (this._lazySecurityActions[i] != 0)
					{
						DeclarativeSecurityAction declarativeSecurityAction = (DeclarativeSecurityAction)this._lazySecurityActions[i];
						ICustomAttribute customAttribute = customAttributes[i];
						string[] lazyPathsForPermissionSetFixup = this._lazyPathsForPermissionSetFixup;
						if (((lazyPathsForPermissionSetFixup != null) ? lazyPathsForPermissionSetFixup[i] : null) != null)
						{
							customAttribute = new PermissionSetAttributeWithFileReference(customAttribute, this._lazyPathsForPermissionSetFixup[i]);
						}
						yield return new SecurityAttribute(declarativeSecurityAction, customAttribute);
					}
					num = i;
				}
			}
			yield break;
		}

		// Token: 0x04000913 RID: 2323
		private byte[] _lazySecurityActions;

		// Token: 0x04000914 RID: 2324
		private string[] _lazyPathsForPermissionSetFixup;
	}
}
