using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MattProject.Database;
using MattProject.Models;

namespace MattProject.Api
{
    public class LoginController : ApiController
    {
        // GET: api/Login
        [HttpGet]
        [Route("getloginattempt/{logId:int}")]
        public Log GetLoginAttempt(int logId)
        {
            Log attempt;

            using (var db = new LogDataContext())
            {
                attempt = db.Logs.FirstOrDefault(x => x.LogId == logId);
            }

            return attempt;
        }

        // GET: api/Login/5
        [HttpGet]
        [Route("getnumberofloginattempts/{userId:int}")]
        public int GetNumberOfLogInAttempts(int userId)
        {
            int numberOfLogInAttempts;
            using (var db = new LogDataContext())
            {
                numberOfLogInAttempts = db.Logs.Count(x => x.UserId == userId);
            }
            
            return numberOfLogInAttempts;
        }

        //// POST: api/Login
        //public bool Post([FromBody]LoginPageModel model)
        //{
        //    var validCredentials = false;
        //    User user;

        //    using (var db = new UsersDataContext())
        //    {
        //        user = db.Users.FirstOrDefault(x => x.UserName == model.UserName && x.Password == model.Password);
        //    }

        //    if (user != null)
        //    {
        //        var log = new Log() {DateTime = DateTime.Now, UserId = user.Id };
                
        //        validCredentials = true;

        //        using (var db = new LogDataContext())
        //        {
        //            db.Logs.InsertOnSubmit(log);

        //            db.SubmitChanges();
        //        }
        //    }

        //    return validCredentials;
        //}

        // DELETE: api/Login/5
        [HttpDelete]
        public void DeleteLogInAttempt(int logId)
        {
            using (var db = new LogDataContext())
            {
                var log = db.Logs.FirstOrDefault(x => x.LogId == logId);
                if (log != null)
                {
                    db.Logs.DeleteOnSubmit(log);
                    db.SubmitChanges();
                }
            }
        }
    }
}
