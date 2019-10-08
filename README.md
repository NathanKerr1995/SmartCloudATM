# SmartCloudATM
I hope you enjoy my ATM project. Here’s a few things before you get started.
This project can be run locally however this will require some setup effort (discussed below). Or you can check out the project by clicking http://atmnathankerr.eu-west-2.elasticbeanstalk.com/Index.aspx hosted with Amazon Web Services.

<h2> Checking out the hosted project </h2>

When exploring the project on it's hosted web address there's not much required except a web browser. There are a few things to note that may come in handy

• There's a reset page. Due to me not being able to edit the user's balance using the API I decided to do the initial pin check with the API, after that though I use a Database for the users balance so I modify it whenever a withdrawal is made. The reset page updates the users balance back to £220 along with the ATM's notes.<br />
• Any email functionality you come across will send an email to me as it is my email in the Database

<h2> Running the project locally </h2>

<h5> Prerequisites </h5>

• Visual Studio (2017 or later)

<h5> Installing </h5>

• Run the SLN file to open the project in Visual Studio

<h5> Known Bugs </h5>

I'm proud of this project but I understand it is not without flaws. There are some bugs / features missing that I was unable to deliver.  I have kept them in the project however because I would like to return to these bugs are correct them.

•	Local: When clicking on any sound icon it creates a constant server loop, user is required to exit and re-execute the app<br />
•	Hosted: On the hosted site the sound icon doesn’t play any sound and stilll creates a constant server loop, user is required to close   the web page and open a new one<br />
•	Feature: I would have liked to create a sql job which runs daily to check if the total amount of a note type drops below a certain        number i.e. if there are less than 3 £5 notes then email supplier.

Thank you for taking the time to review this project. 
