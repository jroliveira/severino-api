﻿namespace Severino.Infrastructure.ErrorHandling
{
    using System;

    using Severino.Infrastructure.ErrorHandling.Exceptions;
    using Severino.Infrastructure.Monad;

    public sealed class ExceptionHandler
    {
        private static ExceptionHandler? errorHandler;

        private readonly bool isDevelopment;

        private ExceptionHandler(bool isDevelopment) => this.isDevelopment = isDevelopment;

        public static void NewExceptionHandler(bool isDevelopment) => errorHandler = new ExceptionHandler(isDevelopment);

        public static Try<Unit> HandleException(Exception exception) => HandleException<Unit>(exception);

        public static Try<TModel> HandleException<TModel>(Exception exception) => HandleException<TModel>(exception, false);

        public static Try<TModel> HandleException<TModel>(Exception exception, bool forceIsDevelopment) => exception switch
        {
            AlreadyExistsException alreadyExists => alreadyExists,
            ForbiddenException forbidden => forbidden,
            InvalidObjectException invalidObject => invalidObject,
            InvalidRequestException invalidRequest => invalidRequest,
            NotFoundException notFound => notFound,
            UnauthorizedException unauthorized => unauthorized,
            _ => new GenericException(exception, forceIsDevelopment || (errorHandler != null && errorHandler.isDevelopment)),
        };
    }
}
