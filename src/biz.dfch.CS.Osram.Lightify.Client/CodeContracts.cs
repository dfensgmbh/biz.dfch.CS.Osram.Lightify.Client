using System.Diagnostics.Contracts;

namespace biz.dfch.CS.Osram.Lightify.Client
{
    public class CodeContracts
    {
        public void BoolMethod(bool itMustBeTrue)
        {
            Contract.Requires(itMustBeTrue);
        }

        public void StringMethod(string itMustNotBeNullOrEmptyOrWhiteSpace)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(itMustNotBeNullOrEmptyOrWhiteSpace));
        }
    }
}
