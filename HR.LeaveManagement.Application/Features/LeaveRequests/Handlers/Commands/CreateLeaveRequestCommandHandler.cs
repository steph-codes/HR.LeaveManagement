using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Contracts.Infrastructure;
using HR.LeaveManagement.Application.Models;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class CreateLeaveRequestCommandHandler :IRequestHandler<CreateLeaveRequestCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IEmailSender _emailSender;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveRequestCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, ILeaveRequestRepository leaveRequestRepository, IEmailSender emailSender )
        {
            _mapper = mapper;
            _leaveRequestRepository = leaveRequestRepository;
            _emailSender = emailSender;
            _leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveRequestDtoValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveRequestDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }

            //we are going to use UpdateLeaverequestDto as the LeaveRequestDto here not Create becaus we need to render the id at the end and create doesnt have that yet
            var leaveRequest = _mapper.Map<LeaveRequest>(request.LeaveRequestDto);

            leaveRequest = await _leaveRequestRepository.Add(leaveRequest);

            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = leaveRequest.Id;

            //send mail note D: Interpolation  means longDate

            var email = new Email
            {
                To = "employee@org.com",
                Body = $"Your Leave request For {request.LeaveRequestDto.StartDate:D} to {request.LeaveRequestDto.EndDate:D} " +
                $" has been submitted successfully.",
                Subject = "Leave Request Submitted"
            };


            try
            {
                await _emailSender.SendEmail(email);
            }
            catch (Exception ex)
            {
                
            }
            return response;

        }
    }
}
