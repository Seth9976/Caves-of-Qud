using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	/// <summary>Provides an encoder for method signatures.</summary>
	// Token: 0x020000EF RID: 239
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct MethodSignatureEncoder
	{
		// Token: 0x170002CD RID: 717
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x0001A6A6 File Offset: 0x000188A6
		public BlobBuilder Builder { get; }

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x0001A6AE File Offset: 0x000188AE
		public bool HasVarArgs { get; }

		/// <param name="builder" />
		/// <param name="hasVarArgs" />
		// Token: 0x0600093C RID: 2364 RVA: 0x0001A6B6 File Offset: 0x000188B6
		public MethodSignatureEncoder(BlobBuilder builder, bool hasVarArgs)
		{
			this.Builder = builder;
			this.HasVarArgs = hasVarArgs;
		}

		/// <summary>Encodes the provided return type and parameters, which must be used in the order they appear in the parameter list.</summary>
		/// <param name="parameterCount">The number of parameters.</param>
		/// <param name="returnType">The method that is called first to encode the return types.</param>
		/// <param name="parameters">The method that is called second to encode the parameters.</param>
		// Token: 0x0600093D RID: 2365 RVA: 0x0001A6C8 File Offset: 0x000188C8
		public void Parameters(int parameterCount, out ReturnTypeEncoder returnType, out ParametersEncoder parameters)
		{
			if (parameterCount > 536870911)
			{
				Throw.ArgumentOutOfRange("parameterCount");
			}
			this.Builder.WriteCompressedInteger(parameterCount);
			returnType = new ReturnTypeEncoder(this.Builder);
			parameters = new ParametersEncoder(this.Builder, this.HasVarArgs);
		}

		/// <summary>Encodes the provided return type and parameters.</summary>
		/// <param name="parameterCount">The number of parameters.</param>
		/// <param name="returnType">The method that is called first to encode the return type.</param>
		/// <param name="parameters">The method that is called second to encode the parameters.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="returnType" /> or <paramref name="parameters" /> is <see langword="null" />.</exception>
		// Token: 0x0600093E RID: 2366 RVA: 0x0001A71C File Offset: 0x0001891C
		public void Parameters(int parameterCount, Action<ReturnTypeEncoder> returnType, Action<ParametersEncoder> parameters)
		{
			if (returnType == null)
			{
				Throw.ArgumentNull("returnType");
			}
			if (parameters == null)
			{
				Throw.ArgumentNull("parameters");
			}
			ReturnTypeEncoder returnTypeEncoder;
			ParametersEncoder parametersEncoder;
			this.Parameters(parameterCount, out returnTypeEncoder, out parametersEncoder);
			returnType.Invoke(returnTypeEncoder);
			parameters.Invoke(parametersEncoder);
		}
	}
}
