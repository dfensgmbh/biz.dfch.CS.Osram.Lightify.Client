using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
