using System;

namespace CAHM
{
    public class AccountNotFoundException : ApplicationException
    {

        public override string Message
        {
            get { return "The specified account does not exist."; }
        }

    }
}
