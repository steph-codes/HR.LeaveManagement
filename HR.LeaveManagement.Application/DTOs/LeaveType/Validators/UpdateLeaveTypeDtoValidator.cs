using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.DTOs.LeaveType.Validators
{
    public class UpdateLeaveTypeDtoValidator : AbstractValidator<LeaveTypeDto>
    {
       
        public UpdateLeaveTypeDtoValidator()
        {
            
            RuleFor(p => p.Name)
                            .NotEmpty().WithMessage("{PropertyName} is required.")
                            .NotNull()
                            .MaximumLength(50).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

            RuleFor(p => p.DefaultDays)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be at Least 1. ")
                .LessThan(100).WithMessage("{PropertyName} must be less than {ComparisonValue}. ");

            RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
        }

    }
    
}
