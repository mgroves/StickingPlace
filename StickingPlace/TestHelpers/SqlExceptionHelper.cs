using System;
using System.Data.SqlClient;
using System.Reflection;

namespace StickingPlace.TestHelpers
{
    public static class SqlExceptionHelper
    {
        public static SqlException CreateException(string message = "SQL Exception Message StickingPlace", int errorNumber = 99)
        {
            return CreateSqlException(message, errorNumber);
        }

        static SqlException CreateSqlException(string errorMessage, int errorNumber)
        {
            SqlErrorCollection collection = GetErrorCollection();
            SqlError error = GetError(errorNumber, errorMessage);

            MethodInfo addMethod = collection.GetType().GetMethod("Add", BindingFlags.NonPublic | BindingFlags.Instance);
            addMethod.Invoke(collection, new object[] {error});

            var types = new[] {typeof (string), typeof (SqlErrorCollection), typeof(Exception), typeof(Guid)};
            var parameters = new object[] {errorMessage, collection, null, Guid.NewGuid()};

            ConstructorInfo constructor = typeof (SqlException).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, types, null);

            var exception = (SqlException) constructor.Invoke(parameters);

            return exception;
        }

        static SqlError GetError(int errorCode, string message)
        {
            var parameters = new object[]
            {
                errorCode, (byte) 0, (byte) 10, "server", message, "procedure", 0
            };
            var types = new Type[]
            {
                typeof (int), typeof (byte), typeof (byte), typeof (string), typeof (string),
                typeof (string), typeof (int)
            };

            ConstructorInfo constructor = typeof (SqlError).
                GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, types, null);
            var error = (SqlError) constructor.Invoke(parameters);
            return error;
        }

        static SqlErrorCollection GetErrorCollection()
        {
            ConstructorInfo constructor = typeof (SqlErrorCollection).
                GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] {}, null);
            var collection = (SqlErrorCollection) constructor.Invoke(new object[] {});
            return collection;
        }
    }
}