# SmartCloudATM
I hope you enjoy my ATM project. Here’s a few things before you get started.
This project can be run locally however this will require some setup effort (discussed below). Or you can check out the project by clicking http://atmnathankerr.eu-west-2.elasticbeanstalk.com/Index.aspx hosted with Amazon Web Services.

<h2> Checking out the hosted project </h2>

When exploring the project on its hosted web address there's not much required except a web browser. There are a few things to note that may come in handy

• There's a reset page. Due to me not being able to edit the user's balance using the API I decided to do the initial pin check with the API, after that though I use a Database for the users balance so I modify it whenever a withdrawal is made. The reset page updates the users balance back to £220 along with the ATM's notes.<br />
http://atmnathankerr.eu-west-2.elasticbeanstalk.com/Reset.aspx <br />
• Any email functionality you come across will send an email to me as it is my email in the Database

<h2> Running the project locally </h2>

<h5> Prerequisites </h5>

• Visual Studio (2017 or later)

<h5> Installing </h5>

• Download and unzip the file from GitHub<br />
• Run the SLN file to open the project in Visual Studio<br />

There should be an option in Visual Studio (Manage NuGet Packages, popup with restore button) to install all NuGet packages from project but in case there isn't here they are:

• NuGet Packages Download<br />
• Antlr by Sam Harwell, Terence Parr<br />
• AspNet.ScriptManager.bootstrap by Chris Sfanos<br />
• AspNet.ScriptManager.jQuery by Damian Edwards<br />
• bootstrap by Twitter<br />
• interactjs.TypeScript.DefinitelyTyped by Jason Jarrett<br />
• jQuery by jQuery Foundation, Inc<br />
• Microsoft.AspNet.FriendlyUrls by Microsoft<br />
• Microsoft.AspNet.FriendlyUrls.Core by Microsoft<br />
• Microsoft.AspNet.ScriptManager.MsAjax by Microsoft<br />
• Microsoft.AspNet.ScriptManager.WebForms by Microsoft<br />
• Microsoft.AspNet.Web.Optimization by Microsoft<br />
• Microsoft.AspNet.Web.Optimization.WebForms by Microsoft<br />
• Microsoft.CodeDom.Providers.DotNetCompilerPlatform by Microsoft<br />
• Microsoft.Web.Infrastructure by Microsoft<br />
• Modernizr by Faruk Ates, Paul Irish, Alex Sexton<br />
• MSTest.TestAdapter  by Microsoft<br />
• MSTest.TestFramework  by Microsoft<br />
• Newtonsoft.Json by James Newton-King<br />
• ScriptsCs.Contracts by Glenn Block, Justin Rusbatch, Filip Wojcieszyn<br />
• WebGrease by webgrease@microsoft.com<br />

• Rebuild the project<br />
• If you have downloaded all packages then all that's left is to run the project with IIS Express using your preferred browser<br />

Also the Reset.aspx works here too.

<h5> Known Bugs </h5>

I'm proud of this project but I understand it is not without flaws. There are some bugs / features missing that I was unable to deliver.  I have kept them in the project however because I would like to return to these bugs and correct them.

•	Local: When clicking on any sound icon it creates a constant server loop, user is required to exit and re-execute the app<br />
•	Hosted: On the hosted site the sound icon doesn’t play any sound and still creates a constant server loop, user is required to close   the web page and open a new one<br />
• Project does not work with Internet Explorer<br />
• Drag and drop bug with Edge<br />
•	Feature: I would have liked to create a SQL job which runs daily to check if the total amount of a note type drops below a certain        number i.e. if there are less than 3 £5 notes then email supplier.

Thank you for taking the time to review this project. 
