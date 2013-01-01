using System;

namespace CAHM
{
    public class AccountAlreadyExistsException : ApplicationException
    {

        public override string Message
        {
            get { return "An account for this email address already exists."; }
        }
    }
}
