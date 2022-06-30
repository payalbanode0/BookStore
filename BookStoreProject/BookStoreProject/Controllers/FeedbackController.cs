using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStoreProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : Controller
    {
        private readonly IFeedbackBL feedbackBL;

        public FeedbackController(IFeedbackBL feedbackBL)
        {
            this.feedbackBL = feedbackBL;
        }
        [Authorize(Roles = Role.User)]
        [HttpPost("AddFeedback")]
        public IActionResult AddFeedback(FeedBackModel feedbackmodel)
        {
            try
            {
               int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = this.feedbackBL.AddFeedback(feedbackmodel, UserId);
                if (result!= null)
                {
                    return this.Ok(new { success = true, message = "feedback added successfull", response = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = " soory!!! Unable to add feedback failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }
        [HttpGet("Get/{BookId}")]
        public IActionResult GetFeedback(int bookId)
        {
            try
            {
                var result = this.feedbackBL.GetAllFeedback(bookId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Successfully Feedback For Given Book Id Fetched ANd Displayed ", Response = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = " Enter Correct BookId" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
    }
}
