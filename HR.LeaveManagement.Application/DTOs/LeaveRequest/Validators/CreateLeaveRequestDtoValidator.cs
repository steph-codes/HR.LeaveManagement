using FluentValidation;
using HR.LeaveManagement.Application.Persistence.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators
{
    public class CreateLeaveRequestDtoValidator : AbstractValidator<CreateLeaveRequestDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        public CreateLeaveRequestDtoValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;

            RuleFor(p => p.StartDate)
            .LessThan(p => p.EndDate).WithMessage("{PropertyName) must be before {ComparisonValue}");

            RuleFor(p => p.EndDate)
            .GreaterThan(p => p.EndDate).WithMessage("{PropertyName) must be before {ComparisonValue}");

            RuleFor(p => p.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(async (Id, token) =>
                {
                    var leaveTypeExists = await _leaveTypeRepository.Exists(Id);
                    return leaveTypeExists; 
                }).WithMessage("{PropertyName} does not exist. ");
           
        }
    }
}
