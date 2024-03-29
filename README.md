# ShopifyBackendChallenge

This project was done for the Shopify Backend Challenge (2021) - https://docs.google.com/document/d/1eg3sJTOwtyFhDopKedRD6142CFkDfWp1QvRKXNTPIOc/edit

When I began this project, I did so with the goal to become more familiar with two specific technologies: Docker, and JWT (JSON Web Tokens). I wanted to learn more about Docker
so that in the future I would be in a better position to be able to containerize my applications so that in order to take advantage of technologies like Kubernetes.
I wanted to learn more about JWT web tokens simply due to the fact that I've always just used cookies and wanted to try something different!

Here is a basic deployment of my project:

![Alt text](https://imgur.com/unMqqvl.png)

Here is a UML diagram for the classes within my project:

![Alt_test](https://user-images.githubusercontent.com/49849803/137232382-701d3c92-cae1-4be2-adfa-07db7b3ac9c2.png)

To create the Web API itself I used ASP.NET 5.0, and used the Entity Framework to serve as an ORM to simplify the process of dealing with the SQL database, where I stored 
the image metadata. I then used a Docker Volume's filesystem in order to store the actual images. When accessing an image, I access the Metadata first, which contains the image's uri on the file system, which will then be used to access the image.

The user is then able to perform the following tasks:

1) Add their images and their metadata to the repository
2) Request their images or their image metadata
3) Search for public images through the use of a tag

# Security

Users can specify whether or not their images are public, and if they are private then only they will be able to access them. The JWT contains the user id which will be used to
determine if the user is authorized to view those images.

One thing to note about registration: the user's password is not stored in the database. A generated hash and salt are stored instead, and when the user tries to request a JWT, 
the password they provide is used to generate a new hash which is compared against the one stored in the database.

# Getting Started

1) Ensure that you have Docker and Docker Compose are installed, and that you are using Linux containers
2) From the ShopifyBackendChallenge directory run "docker-compose build", and then "docker-compose up". This will ensure that the web server, database and docker volume are set up and ready to use.

# How to use

1) Use the base URL: http://localhost:8080/
2) Following the API below you can register a new user. There is a test user ready for use (username: "test", password: "test") if you wished to skip this step. Note, if you try to register under a username that is already used you will receive an error.
3) You will then need authenticate in order to receive your JWT so you can access be authorized to use the other routes for the API. 
4) Using your JWT, you can then add images, request your metadata and images, and search for any public images through the use of a tag.

# Testing

All tests are run during docker-compose build, so if the tests fail the build will also fail.

# API Details

All API details can be found on http://localhost:8080/index.html as the application is documented with SwaggerUI.

You can also watch this video for a quick demonstration for using the API with Postman - https://youtu.be/FuduoGazBzw

![image](https://user-images.githubusercontent.com/49849803/137361101-d6db8fde-0001-4462-82f9-3bc2d5552e51.png)

# Future Work

Seeing as I found out about this challenge within a week of the due date, I didn't include all the functionality that I wanted. Here are some future areas for me to expand:

1) JWT Improvements: Currently I am not using a refresh token for when the JWT expires. Another area to improve would be isolate authentication into its own service, where only the authentication service would be allowed to generate new tokens. The tokens would then be encrypyed using RSA, where the authentication service would use a private key to encrypt the tokens and any other services would be able to use the public key to verify that the token came from the authentication service. This not only ensures that only the authentication service can generate new tokens, but it reduces the liklihood of the private key being compromised as only 1 service has access to it.

2) I want to implement pagination for when the user is accessing their images. If the user is storing 100s of images then it would be prohibitevely expensive for the server to 
be sending all of those images with a single request. Instead, I could send a handful at a time, and when the user wants others they just need to change the offset.

3) Currently search is very fragile as it relies on the tags specified by the user. Another way to search for images would be to use a service that classified these images using machine learning, and then using those classifications to generate our own tags in case the ones provided by the user are not particularly useful. Another method could be to use image hashingto try and determine which images appear to be visually similiar. 

4) I want to learn more about working in environments like AWS or Azure, so if I have time I might try and host it on one of those services.
