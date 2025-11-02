using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveTypeCommand;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Domain.Interfaces;
using HR.LeaveManagement.Domain.Models;
using Moq;
using Shouldly;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveTypes
{
    public class CreateLeaveTypeCommandHandlerTest
    {
        private Mock<ILeaveTypeRepository> _mockRepo;
        private List<LeaveType> _leaveTypes;
        private IMapper _mapper;
        private CreateLeaveTypeCommandValidator _validator;

        public CreateLeaveTypeCommandHandlerTest()
        {

            _mockRepo = new Mock<ILeaveTypeRepository>();

            _leaveTypes =
                [
                    new LeaveType()
                    {
                        Id = 1,
                        DateCreated = DateTime.Now,
                        DateModified = DateTime.Now,
                        Name = "Sick",
                    }
                ];

            _mockRepo.Setup(x => x.CreateAsync(It.IsAny<LeaveType>()))
             .Returns((LeaveType leaveType) =>
             {
                 _leaveTypes.Add(leaveType);
                 return Task.CompletedTask;
             });

            _mockRepo.Setup(x => x.IsLeaveTypeNameUniqueAsync(It.IsAny<string>())).ReturnsAsync(true);

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.AddProfile<LeaveTypeProfile>();
            });

            _mapper = mapperConfiguration.CreateMapper();
            _validator = new CreateLeaveTypeCommandValidator(_mockRepo.Object);
        }

        [Fact]
        public async Task WhenCreateLeaveTypes_CreateOne()
        {
            var command = new CreateLeaveTypeCommand() { Name = "A", DefaultDays = 20 };

            var sut = new CreateLeaveTypeCommandHandler(_mockRepo.Object, _mapper, _validator);

            await sut.Handle(command, CancellationToken.None);

            _mockRepo.Verify(x => x.CreateAsync(It.IsAny<LeaveType>()), Times.Once);
            _leaveTypes.Count().ShouldBe(2);
            _leaveTypes.ShouldContain(x => x.Name == "A");
        }


    }
}
