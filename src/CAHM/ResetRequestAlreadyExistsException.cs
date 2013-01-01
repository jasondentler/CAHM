using System;

namespace CAHM
{
    public class ResetRequestAlreadyExistsException : ApplicationException
    {

        public override string Message
        {
            get { return string.Format("For security purposes, you may only reset your password once per hour."); }
        }

    }
}
