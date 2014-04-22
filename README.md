Sticking Place
==============

>**Macbeth**:
>> If we should fail?

>**Lady Macbeth**:
>> We fail?  

>> But screw your courage to the sticking place,  

>> And we'll not fail.  

Sticking Place is yet another "junk drawer" C# library of useful extensions and helpers.

SqlHelpers
-----------
Helpers for ADO.NET/SQL

*  __SqlExceptionHelper__: Create SqlException objects with whatever message and error number you'd like. See SqlExceptionHelperTests for more information. This should only be used for testing, I can't think of a good reason to use it otherwise.

WebHelpers
----------
Helpers for ASP.NET / ASP.NET MVC

* __HttpExtensions::SetAuthCookie__: On the surface, it seems like Forms Auth only lets you put a string (username) into the auth cookie. You can put arbitrary objects in there, but you have to jump through some extra hoops to do it. SetAuthCookie jumps through the hoops.
* __HttpExtensions::GetAuthCookie__: Get the arbitrary object back out of the Forms Auth cookie without all the hoop jumping.
* __ModelStateDictionaryExtensions::GetAllErrors__: A convenience extension to walk through the whole MVC ModelState errors tree. Good for unit testing sometimes too.
* __HtmlHelperExtensions::ActionImage: A link with an image in it. You can specify routevalue and don't forget the alt!
* __HtmlHelperExtensions::IsSelected__: In an ASP.NET View layout page, I will often have a bunch of nav links that need to be highlighted when the controller is "active". For instance, on a menu I have 3 links: "Home", "Invoices", "Locations", each going to an action in their respective controllers. When an action in the "Users" controller is executed, I want the "Users" link to be visually highlighted by putting a CSS class on the link. To make this easy, I'll use the IsSelected extension like below. ReSharper and/or Visual Studio may not be happy with it, but it's valid Razor. It assumes a default CSS class of "active", but you can pass in another one.
```
<ul class="nav navbar-nav">
    <li@(@Html.IsSelected("Home"))>@Html.ActionLink("Home", "Index", "Home")</li>` 
    <li@(@Html.IsSelected("Invoices"))>@Html.ActionLink("Invoices", "Index", "Invoices")</li>
    <li@(@Html.IsSelected("Locations"))>@Html.ActionLink("Locations", "Index", "Locations")</li>
</ul>
```
* __HtmlHelperExtensions::StateDropDownListFor, ProvicesDropDownListFor, TerritoriesDropDownListFor, StatesAndTerritoriesDropDownListFor, StatesAndProvincesDropDownListFor, StatesAndProvincesAndTerritoriesDropDownListFor__: MVC Helpers to show dropdowns for U.S. States, Canadian provinces, and U.S. territories, and various permuations. Otherwise, it works just like Html.DropDownListFor. No offense intended to Canadians.
* __StatesProvincesTerritories__: This is the source data used for all of the above, broken up into States (which included Washington D.C.), Provinces, and Territories.


StringHelpers
-------------
Helpers for string manipulation

* __StringExtensions::ToMD5__: Hash a string to an MD5 string. [You should probably not be using MD5 for anything involving important](http://www.zdnet.com/blog/security/md5-password-scrambler-no-longer-safe/12317), but for legacy/conversion reasons you may need to have this handy.

Roadmap
=======
* There is a really hacky unit test for Html::ActionImage that I only *mostly* understand. It could use improvement. Unit test organization in general could use some improvement.
* I have some other mocking helpers that I use frequently with Telerik JustMock, but they aren't general enough (yet) to put in here.

>**Macbeth**: 
>> Away, and mock the time with fairest show. 

>> False face must hide what the false heart doth know. 
