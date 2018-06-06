# JobBoard-MongoDB
A .NET Core Web app with an emphasis on back-end functionality,configured to use ASP.NET Core identity to use and store user data in MongoDB, where users can create CVs, apply and search for and/or create jobs entries

<h1>Usage</h1>
To get things running you will need a MongoDB instance up and running and you will need to edit the connectionstring in the appsettings.json file as well, as explained in the steps below: 

<h4>1. Please see https://docs.mongodb.com/tutorials/install-mongodb-on-windows/ on how to set up a MongoDB client.</h4>
<h4>2. After setting up MongoDB, make sure you've started mongod.exe before running the project.</h4>
<h4>3. Your connection string should usually look something like this:</h4>
   <p>
            "ConnectionStrings": {  
     <br>   "DefaultConnection": "mongodb://localhost/", 
     <br>   "DatabaseName": "DatabaseName",
     <br>   "IsSSL": false 
     <br> } 
   </p>
