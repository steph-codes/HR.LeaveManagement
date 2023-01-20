using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        //name of what's sought after and key is the parmeter/result needed that wasnt found
        public NotFoundException(string name, object key) : base($"{name} ({key}) was not found")
        {
            
        }
    }
}
