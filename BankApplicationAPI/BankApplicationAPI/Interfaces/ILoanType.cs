using BankApplicationAPI.Models;

namespace BankApplicationAPI.Interfaces
{
    public interface ILoanType
    {
        Task<IEnumerable<LoanType>> GetLoanTypesAsync();
        Task<LoanType> GetLoanTypeByLoanTypeIdAsync(int LoanTypeId);
        Task<LoanType> UpdateLoanTypeAsync(LoanType loanType);
        Task<Boolean> DeleteLoanTypeAsync(LoanType loanType);
        Task<Boolean> CreateLoanTypeAsync(LoanType loanType);
        Task<IEnumerable<LoanType>> GetLoanTypeAsync(int? LoanTypeId = null,
                                      string? LoanTypeName = null);
    }
}
