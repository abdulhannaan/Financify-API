using BLL.Dtos;
using Utility.Commons;
using Data.Models;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Utility.Enumerations;
using AHDBLL.Dtos.Request;
using System.Data;

namespace BLL.Services
{
    public class JoinUsService
    {
        public ApiResponseMessage Save(JoinU data)
        {
            var response = new ApiResponseMessage();

            try
            {
                JoinU obj = new JoinU();
                using (var joinUsData = new JoinUsRepository())
                {
                    obj.FullName = data.FullName;
                    obj.IsActive = true;
                    obj.Subject = data.Subject;
                    obj.EmailAddress = data.EmailAddress;
                    obj.Body = data.Body;
                    obj.MobileNo = data.MobileNo;
                    obj.FileName = data.FileName;
                    obj.JobId = data.JobId;
                    obj.CreatedOn = DateTime.Now;
                    joinUsData.Add(obj);
                }
                response.Status = HttpStatusCode.OK;
                response.Message = "Saved successfully!";
                response.Response = obj;

            }
            catch (Exception ex)
            {
                response.Status = HttpStatusCode.InternalServerError;
                response.Message = "An Error has occured!";
                response.Response = false;
            }
            return response;
        }

        public List<JoinU> List()
        {
            try
            {
                using (var joinData = new JoinUsRepository())
                {
                    return joinData.List();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ApiResponseMessage Delete(int joinId)
        {
            var response = new ApiResponseMessage();
            try
            {
                JoinU join = new JoinU();
                using (var joinData = new JoinUsRepository())
                {
                    join = joinData.Get(joinId);

                    if (join != null && join.Id > 0)
                    {
                        join.IsActive = false;
                        join.UpdatedOn = DateTime.Now;
                        joinData.Update(join);

                        response.Status = HttpStatusCode.OK;
                        response.Message = "Delete successfully!";
                        response.Response = true;
                    }
                    else
                    { 
                        response.Status = HttpStatusCode.OK;
                        response.Message = "No Record Found!";
                        response.Response = false;
                }
                }
            }
            
            catch (Exception ex)
            {
                response.Status = HttpStatusCode.InternalServerError;
                response.Message = "An Error has occured!";
                response.Response = false;
            }
            return response;
        }


        public JoinU View(int Id)
        {
            try
            {
                JoinU join = new JoinU();

                using (var joinData = new JoinUsRepository())
                {
                    join = joinData.Get(Id);
                    return join;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
