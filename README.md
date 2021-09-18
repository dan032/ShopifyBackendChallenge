# ShopifyBackendChallenge

This project was done for the Shopify Backend Challenge (2021) - https://docs.google.com/document/d/1eg3sJTOwtyFhDopKedRD6142CFkDfWp1QvRKXNTPIOc/edit

When I started this project, I began with the goal to become more familiar with two specific technologies: Docker, and JWT (JSON Web Tokens). Specifically, 
I wanted to ensure that this project was capable of running on anyone's machine as long as they had Docker installed (with the caveat that they are using Linux containers).
I wanted to learn more about JWT web tokens simply due to the fact that I've always just used cookies and wanted to try something different!

Here is a basic deployment of my project:

![Alt text](https://i.imgur.com/NR6oBkl.png)

To create the Web API itself I used ASP.NET Core 3.1, and used the Entity Framework to serve as an ORM to simplify the process of dealing with the SQL database, where I stored 
the image metadata. I then used a Docker Volume's filesystem in order to store the actual images. The user is then able to perform the following tasks:

1) Add their images to the repository
2) Request their images or their image metadata
3) Search for public images through the use of a tag

# Security

Users can specify whether or not their images are public, and if they are private then only they will be able to access them. The JWT contains the user id which will be used to
determine if the user is authorized to view those images.

One thing to note about registration: the user's password is not stored in the database. A generated hash and salt are stored instead, and when the user tries to request a JWT, 
the password they provide is used to generate a new hash which is compared against the one stored in the database.

# Getting Started

1) Ensure that you have Docker and Docker Compose installed, and that you are using Linux containers
2) From the ShopifyBackendChallenge directory run "docker-compose build", and then "docker-compose up". This will ensure that the web server, database and docker volume are set up and ready to use.

# How to use

1) Use the base URL: https://localhost:8081/
2) Following the API below you can register a new user. There is a test user ready for use however (username: "test", password: "test") if you wished to skip this step.
3) You will then need authenticate in order to receive your JWT so you can access be authorized to use the other routes for the API. 
4) Using your JWT, you can then add images, request your metadata and images, and search for any public images through the use of a tag.

# Testing

To run tests you will need to be on a Windows machine and have dotnet core and sql server installed. From the ShopifyBackendChallenge directory run "dotnet test ShopifyBackendChallenge.Tests".

# API Details

All API details can be found on https://localhost:8081/index.html as the application is documented with SwaggerUI.

You can also watch this video for a quick demonstration for using the API with Postman - https://youtu.be/3xI3L0l4RD8

![Alt text](https://imgur.com/NoYX2T5.png)

# Future Work

Seeing as I found out about this challenge within a week of the due date, I didn't include all the functionality that I wanted. Here are some future areas for me to expand:

1) JWT Improvements: Currently I am not using a refresh token for when the JWT expires. Another area to improve would be isolate authentication into its own service, where only the authentication service would be allowed to generate new tokens. The tokens would then be encrypyed using RSA, where the authentication service would use a private key to encrypt the tokens and any other services would be able to use the public key to verify that the tokencame from the authentication service. This not only ensures that only the authentication service can generate new tokens, but it reduces the liklihood of the private key being compromised as only 1 service has access to it.

2) I want to implement pagination for when the user is accessing their images. If the user is storing 100s of images then it would be prohibitevely expensive for the server to 
be sending all of those images with a single request. Instead, I could send a handful at a time, and when the user wants others they just need to change the offset.

3) Currently search is very fragile as it relies on the tags specified by the user. Another way to search for images would be to use a service that classified these images using machine
learning, and then using those classifications to generate our own tags in case the ones provided by the user are not particularly useful. Another method could be to use image hashing
to try and determine which images appear to be visually similiar. 
