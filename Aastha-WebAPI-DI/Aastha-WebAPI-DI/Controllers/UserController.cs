using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Repository;

namespace Aastha_WebAPI_DI.Controllers
{
    public class UserController : ApiController
    {
        private IUserRepo _iuserRepo;

        public UserController(IUserRepo userRepo)
        {
            //object parameters will be injected into this controller and we can use below object for calling the method
            //_iproductRepo = productRepo;
            _iuserRepo = userRepo;
        }

        [HttpGet]
        public IEnumerable<UserTable> LoadAllUsers()
        {
            using (AasthaDBEntities entities = new AasthaDBEntities())
            {
                return entities.UserTables.ToList();
            }
        }

        [Route("api/Login", Name = "Login")]
        [BasicAuthentication]
        [HttpGet]
        public bool Login(string username, string password)
        {
            return _iuserRepo.Login(username, password);
        }

        //public UserTable GetUsers(int id)
        //{
        //    using (AasthaDBEntities entities = new AasthaDBEntities())
        //    {
        //        return entities.UserTables.FirstOrDefault(e => e.UserId == id);
        //    }
        //}
        public HttpResponseMessage GetUsersandMessage(int id)
        {
            using (AasthaDBEntities entities = new AasthaDBEntities())
            {
                var entity = entities.UserTables.FirstOrDefault(e => e.UserId == id);

                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id" + id.ToString() + "not found");
                }
            }
        }

        public HttpResponseMessage Post([FromBody] UserTable user)
        {
            try
            {
                using (AasthaDBEntities entities = new AasthaDBEntities())
                {
                    entities.UserTables.Add(user);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, user);
                    message.Headers.Location = new Uri(Request.RequestUri + user.UserId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex); 
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            using(AasthaDBEntities entities = new AasthaDBEntities())
            {
                try
                {
                    var entity = entities.UserTables.FirstOrDefault(e => e.UserId == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id" + id.ToString() + "not found to delete");
                    }
                    else
                    {
                        entities.UserTables.Remove(entity);
                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
                catch(Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
        }

        public HttpResponseMessage Put(int id, [FromBody] UserTable user)
        {
            using (AasthaDBEntities entities = new AasthaDBEntities())
            {
                try
                {
                    var entity = entities.UserTables.FirstOrDefault(e => e.UserId == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id" + id.ToString() + "not found to update");
                    }
                    else
                    {
                        entity.UserName = user.UserName;
                        entity.Password = user.Password;
                        entity.UserType = user.UserType;
                        entity.Role = user.Role;
                        entity.CreatedBy = user.CreatedBy;
                        entity.CreatedDate = user.CreatedDate;
                        entity.IsActive = user.IsActive;

                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
        }
    }
}
