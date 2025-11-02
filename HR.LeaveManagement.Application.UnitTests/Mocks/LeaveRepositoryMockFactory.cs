using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.LeaveManagement.Domain.Interfaces;
using HR.LeaveManagement.Domain.Models;
using Moq;

namespace HR.LeaveManagement.Application.UnitTests.Mocks
{
    public class LeaveRepositoryMockFactory
    {
        public static Mock<ILeaveTypeRepository> Get()
        {

            var mockRepo = new Mock<ILeaveTypeRepository>();

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


            mockRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(leaveTypes);
            mockRepo.Setup(x =>
            x.CreateAsync(It.IsAny<LeaveType>()))
                .Returns((LeaveType leaveType) =>
                {
                    leaveTypes.Add(leaveType);
                    return Task.CompletedTask;
                });

            return mockRepo;
        }
    }
}
