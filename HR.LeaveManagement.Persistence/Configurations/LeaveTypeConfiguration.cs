using HR.LeaveManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Persistence.Configurations;

public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
{
    public void Configure(EntityTypeBuilder<LeaveType> builder)
    {
        builder.HasData(
            new LeaveType()
            {
                Id = 1,
                Name = "Vacation",
                DefaultDays = 0,
                DateCreated = DateTime.Now.Date,
                DateModified = DateTime.Now.Date
            });

        builder
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
