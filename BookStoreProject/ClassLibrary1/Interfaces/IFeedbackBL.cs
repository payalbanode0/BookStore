using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IFeedbackBL
    {
        public FeedBackModel AddFeedback(FeedBackModel feedbackModel, int userId);
        public List<DisplayFeedback> GetAllFeedback(int bookId);
    }
}