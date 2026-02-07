<h1>🍽️ Restaurant Management App (MAUI + .NET API)!)</h1>
<h1> Ongoing project !</h1>
A cross-platform Restaurant Management mobile application built with .NET MAUI, consuming a .NET Web API backed by MongoDB.
The app allows users to manage restaurants, daily menus, GPS coordinates, authentication, and multilingual UI.

This project is being developed as a real-world portfolio project, showcasing modern mobile + backend architecture.

<h1>✨ Features</h1>
<ul>
  <li>📱 Mobile App (MAUI)</li>

  <li>🔐 User authentication (Login & Register)</li>

  <li>🌍 Multi-language support (dynamic language switching)</li>

  <li>🏪 Restaurant list (CRUD)</li>

  <li>📍 GPS location capture (latitude & longitude)</li>

  <li>🔄 Pull-to-refresh</li>

  <li>🧭 Shell navigation</li>

  <li>📦 MVVM architecture</li>
</ul>
<h1>🌐 Backend API (.NET)</h1>
<ul>
  <li>RESTful API with Swagger</li>

  <li>MongoDB integration (IMongoCollection)</li>

  <li>Unique restaurant name validation</li>

  <li>Conflict handling (409 responses)</li>

  <li>JWT-based authentication</li>
</ul>
<h1>🧱 Architecture Overview</h1>
<ul>
MAUI App
  
  ├── Views (XAML)
  
  ├── ViewModels (MVVM)
  
  ├── Services (API clients)
  
  └── Localization (RESX)
  
        ↓
        
.NET Web API

  ├── Controllers
  
  ├── DTOs
  
  ├── Services
  
  └── MongoDB
</ul>
<h1>🛠️ Technologies Used</h1>

Mobile
<ul>
  <li>.NET MAUI</li>

  <li>MVVM pattern</li>

  <li>Shell Navigation</li>

  <li>CommunityToolkit.MVVM</li>

  <li>Geolocation API</li>

  <li>Localization (RESX)</li>
</ul>
Backend
<ul>
  <li>ASP.NET Core Web API</li>

  <li>MongoDB</li>

  <li>Swagger / OpenAPI</li>

  <li>JWT Authentication</li>

  <li>Docker (optional)</li>
</ul>
<h1>🌍 Localization</h1>>

The app supports dynamic language switching at runtime:
<ul>
  <li>English</li>

  <li>Portuguese</li>
</ul>
Localization is implemented using .resx resource files and updates UI instantly without restarting the app.

<h1>📍 GPS Integration</h1>

Users can retrieve their current GPS location with a single tap:
<ul>
  <li>Requests runtime permissions</li>

  <li>Shows loading feedback while fetching location</li>

  <li>Automatically fills latitude & longitude fields</li>
</ul>
<h1>🚀 Getting Started</h1>
<ul>
  Backend API
    
  dotnet restore
  
  dotnet run
  
  Swagger available at:
  
  http://localhost:xxxx/swagger
</ul>
<h1>MAUI App</h1>

  <li>Open the MAUI solution in Visual Studio</li>

  <li>Restore NuGet packages</li>

  <li>Select target platform (Android / iOS / Windows)</li>

  <li>Run</li>
</ul>
<h1>👤 Author</h1>

Arnaldo
<dl>
  <dt>📍 Portugal</dt>
  <dt>💼 Project developed for learning and demonstrating development with MAUI + .NET API</dt>
</dl>


<h1>✨ Example images</h1>
<img width="1910" height="1016" alt="vstudio_ex1" src="https://github.com/user-attachments/assets/dc513eae-228b-4505-9169-9312469715d9" />

<img width="1901" height="1015" alt="vstudio_ex2" src="https://github.com/user-attachments/assets/99a27516-b9b8-4823-a9e4-db45e334de9a" />

<img width="1902" height="1021" alt="vstudio_ex3" src="https://github.com/user-attachments/assets/12d05f28-4b65-4ec8-8ab9-569295c9b976" />

<img width="1100" height="513" alt="API_ex1" src="https://github.com/user-attachments/assets/34a0fcf1-e838-42de-a103-c77aeedfde99" />

<img width="1823" height="897" alt="MongoDB_ex1" src="https://github.com/user-attachments/assets/f88f4595-fead-4251-8b99-4e8cfd155eee" />

<img width="1845" height="907" alt="MongoDB_ex2" src="https://github.com/user-attachments/assets/4a452103-8404-4900-9666-b44e7d14daf8" />


