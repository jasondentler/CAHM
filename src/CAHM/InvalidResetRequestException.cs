using System;

namespace CAHM
{
    public class InvalidResetRequestException : ApplicationException
    {

        public override string Message
        {
            get { return "Sorry. The password reset request is no longer valid."; }
        }

    }
}
