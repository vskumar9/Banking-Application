using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;

namespace BankApplicationAPI.Repository
{
    public class LoanTypeRepository : ILoanType
    {
        public Task<bool> CreateLoanTypeAsync(LoanType loanType)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteLoanTypeAsync(LoanType loanType)
        {
            throw new NotImplementedException();
        }

        public Task<LoanType> GetLoanTypeAsync(int? LoanTypeId = null, string? LoanTypeName = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LoanType>> GetLoanTypeByLoanTypeIdAsync(int LoanTypeId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LoanType>> GetLoanTypesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<LoanType> UpdateLoanTypeAsync(LoanType loanType)
        {
            throw new NotImplementedException();
        }
    }
}
