 Real-Time Chat Application with Azure
 Overview

This project is a real-time chat application built with ASP.NET Core, Angular, and Azure services.
It allows users to send and receive messages instantly using Azure SignalR Service, store messages in Azure SQL Database, and analyze message sentiment using Azure AI Language.

 Features
 Real-time messaging using Azure SignalR
 Persistent message storage in Azure SQL Database
 Sentiment analysis (Positive / Neutral / Negative)
 Modern chat UI with message bubbles
 Auto-scroll to latest message
 Username prompt on first visit (stored in localStorage)
 Deployed to Azure
 Architecture

Backend
ASP.NET Core (.NET 9)
SignalR Hub
Entity Framework Core
Azure SignalR Service
Azure SQL Database
Azure AI Language (Sentiment Analysis)
Frontend
Angular (standalone components)
SignalR client
Reactive UI with signals
Custom CSS chat interface
 
 How It Works
User enters a username on the frontend
Angular connects to SignalR Hub
User sends a message
Backend:
analyzes sentiment using Azure AI Language
saves message to Azure SQL
broadcasts message via SignalR
All connected clients receive and display the message in real time

 Database Schema
Table: messages
Column	Type	Description
Id	int	Primary key
UserName	nvarchar	Sender name
Text	nvarchar	Message text
Sentiment	nvarchar	Sentiment label
CreatedAtUtc	datetime2	Timestamp (UTC)
☁️ Azure Services Used
Azure App Service (Backend hosting)
Azure Static Web Apps (Frontend hosting)
Azure SignalR Service
Azure SQL Database
Azure AI Language (Sentiment Analysis)

 Configuration
 Environment Variables (App Service)
Connection Strings
DefaultConnection = <Azure SQL connection string>
App Settings
Azure__SignalR__ConnectionString = <SignalR connection string>
Azure__Language__Endpoint = <Azure Language endpoint>
Azure__Language__Key = <Azure Language key>

 Running Locally
Backend
cd Backend/ChatApplication
dotnet restore
dotnet run
Frontend
cd Frontend
npm install
ng serve

 Deployment
Backend
Deployed to Azure App Service via GitHub Actions
Frontend
Deployed to Azure Static Web Apps
 Sentiment Analysis

Messages are analyzed using Azure AI Language service.
Each message is classified as:

Positive
Neutral
Negative

The result is stored in the database and displayed in the UI.

 UI Features
Message bubbles (left/right alignment)
Auto-scroll to latest message
Username display
Sentiment label visualization
Responsive layout
Authentication
👨‍💻 Author

Vadim
.NET & Angular Developer
