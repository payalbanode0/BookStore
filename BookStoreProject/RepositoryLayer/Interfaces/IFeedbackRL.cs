using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IFeedbackRL
    {
        public FeedBackModel AddFeedback(FeedBackModel feedbackModel, int userId);
        public List<DisplayFeedback> GetAllFeedback(int bookId);
    }
}
