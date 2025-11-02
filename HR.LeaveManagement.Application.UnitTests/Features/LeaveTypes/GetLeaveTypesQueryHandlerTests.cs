using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Castle.Core.Logging;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Domain.Interfaces;
using HR.LeaveManagement.Domain.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveTypes
{
    public class GetLeaveTypesQueryHandlerTests
    {
        private Mock<ILeaveTypeRepository> _mockRepo;
        private IMapper _mapper;
        public GetLeaveTypesQueryHandlerTests()
        {
            _mockRepo = new Mock<ILeaveTypeRepository>();

            List<LeaveType> leaveTypes =
                [
                    new LeaveType()
                    {
                        Id = 1,
                        DateCreated = DateTime.Now,
                        DateModified = DateTime.Now,
                        Name = "Sick",
                    }
                ];

            _mockRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(leaveTypes);

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.AddProfile<LeaveTypeProfile>();
            });

            _mapper = mapperConfiguration.CreateMapper();
        }


        [Fact]
        public async Task WhenHandle_ReturnListOfLeaveTypes()
        {
            var getLeaveTypesQueryHandler = new GetLeaveTypesQueryHandler(_mockRepo.Object, _mapper, Mock.Of<ILogger<GetLeaveTypesQueryHandler>>());

            var leaveTypes = await getLeaveTypesQueryHandler.Handle(new GetLeaveTypesQuery(), CancellationToken.None);
            leaveTypes.ShouldBeOfType<List<LeaveTypeDto>>();
            leaveTypes.Count.ShouldBe(1);
        }
    }
}
