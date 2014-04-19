Sticking Place
==============

>Macbeth:
>> If we should fail?

>Lady Macbeth:
>> We fail?  
>> But screw your courage to the sticking place,  
>> And we'll not fail.  

Sticking Place is yet another "junk drawer" C# library of useful extensions and helpers.

TestHelpers
-----------
Helpers here are meant for unit testing.

*  __SqlExceptionHelper__: Create SqlException objects with whatever message and error number you'd like. See SqlExceptionHelperTests for more information.

WebHelpers
----------
Helpers for ASP.NET

* __HttpExtensions::SetAuthCookie__: On the surface, it seems like Forms Auth only lets you put a string (username) into a cookie. You can put arbitrary objects in there, but you have to jump through some extra hoops to do it. SetAuthCookie jumps through the hoops.
* __HttpExtensions::GetAuthCookie__: Get the arbitrary object back out of the Forms Auth cookie without all the hoop jumping.