namespace RMS.Services.Contracts
{
    using System;
    using System.Threading.Tasks;

    public interface ITeacherEventService
    {
        Task DeleteTeachersEventsByTeacherIdAsync(Guid teacherId);
    }
}
