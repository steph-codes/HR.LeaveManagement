using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.DTOs.LeaveType.Validators
{
    public class CreateLeaveTypeDtoValidator : AbstractValidator<CreateLeaveTypeDto>
    {
        public CreateLeaveTypeDtoValidator()
        {
            //include or inherits rules from ILeaveTypeValidators , Also you can add Custom Rules too
            Include(new ILeaveTypeDtoValidator());
        }
    }
}
