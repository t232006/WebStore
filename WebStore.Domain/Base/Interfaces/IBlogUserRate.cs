using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.Base.Interfaces
{
    public interface IBlogUserRate
    {
        int VoteNumber(int BlogID);
        decimal AverageRate(int BlogID);
        byte? UserVote(string? UserID, int BlogID);
        void Invote(string UserID, int BlogID, byte Rate);
    }
}
