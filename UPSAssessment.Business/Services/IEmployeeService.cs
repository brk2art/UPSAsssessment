using UPSAssessment.Business.DTOs;

namespace UPSAssessment.Business.Services
{
    public interface IEmployeeService
    {
        List<EmployeeDto> GetAll(string name = "");
        bool Add(EmployeeDto employee);
    }
}
