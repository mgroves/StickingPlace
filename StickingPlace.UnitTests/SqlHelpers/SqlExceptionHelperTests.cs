using System.Data.SqlClient;
using NUnit.Framework;
using StickingPlace.TestHelpers;

namespace StickingPlace.UnitTests.SqlHelpers
{
    [TestFixture]
    public class SqlExceptionHelperTests
    {
        [Test]
        public void SqlExceptionHelper_should_return_a_new_SqlException_object()
        {
            var sqlExceptionObject = SqlExceptionHelper.CreateException();
            Assert.That(sqlExceptionObject, Is.Not.Null);
            Assert.That(sqlExceptionObject, Is.InstanceOf<SqlException>());
        }
        
        [Test]
        public void SqlExceptionHelper_SqlException_have_an_arbitrary_default_message_and_error_number()
        {
            var sqlExceptionObject = SqlExceptionHelper.CreateException();
            Assert.That(sqlExceptionObject.Number, Is.EqualTo(99));
            Assert.That(sqlExceptionObject.Message, Is.EqualTo("SQL Exception Message StickingPlace"));
        }
        
        [Test]
        public void With_SqlExceptionHelper_you_can_specify_whatever_message_and_or_error_number_you_want()
        {
            const string myMessage = "Hello, world!";
            const int myNumber = 123;
            var sqlExceptionObject = SqlExceptionHelper.CreateException(errorNumber: myNumber, message: myMessage);
            Assert.That(sqlExceptionObject.Number, Is.EqualTo(myNumber));
            Assert.That(sqlExceptionObject.Message, Is.EqualTo(myMessage));
        }
    }
}