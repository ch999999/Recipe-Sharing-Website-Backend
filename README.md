This is the backend code repository for a website that users can use to easily create and share recipes (ie. a recipe sharing website). A live implementation is available at https://recipekamu.vercel.app . Instsructions for basic use of the website are found on the [`home page`](https://recipekamu.vercel.app) of the website. The software architecture of the website is explained below.

## Architecture Overview
![Software Architecture](https://github.com/ch999999/RecipesFrontend/blob/master/Extra_Images/architecture.PNG) <br />
*Simple diagram showing the general architecture of the current implementation*

## Frontend
The frontend is written using the [`Next.js`](https://nextjs.org/) framework for React. The current implementation uses [`Vercel`](https://vercel.com/) for deployment. The frontend source code can be found here: https://github.com/ch999999/Recipe-Sharing-Website-Frontend

## Backend
The backend consists of an ASP.NET Core API app, a PostgreSQL database and a [`AWS S3 Bucket`](https://aws.amazon.com/s3/) for storing images. The current implementation uses [`Microsoft Azure`](https://azure.microsoft.com/) to deploy both the API app and the Postgre database.

## Database schema
![Created using https://sql.toad.cz/](https://github.com/ch999999/Recipe-Sharing-Website-Backend/blob/master/Schema_Documentation/ERD.PNG) <br />

## Feedback and Bug Reports
Feedback, suggestions and bugs can be reported to recipekamufeedback@outlook.com at this time.
