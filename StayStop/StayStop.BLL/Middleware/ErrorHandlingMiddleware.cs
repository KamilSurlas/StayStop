﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StayStop.BLL_EF.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace StayStop.BLL.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {

        public ErrorHandlingMiddleware()
        {

        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (InvalidManagerToRemove exc)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(exc.Message);
            }
            catch (ContentNotFoundException exc)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(exc.Message);
            }
            catch (InvalidReservationDate exc)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(exc.Message);
            }
            catch (RoomIsAlreadyActive exc)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(exc.Message);
            }
            catch (ReservationAlreadyHasOpinion exc)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(exc.Message);
            }
            catch (DbUpdateException exc)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Operation has failed! Something went wrong during database operation");
            }
            catch (Exception exc)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Ups, something went wrong");
            }

        }
    }
}
