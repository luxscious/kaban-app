# 📋 Kanban Board App

A modern Kanban board application built with **ASP.NET Core 8.0** as a learning project to explore .NET web development fundamentals.

![.NET](https://img.shields.io/badge/.NET-8.0-blue)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core%209.0-green)
![SQLite](https://img.shields.io/badge/Database-SQLite-lightblue)
![TailwindCSS](https://img.shields.io/badge/Styling-TailwindCSS-38B2AC)

## 🎯 Learning Objectives

This project was created to learn and demonstrate:

### **Core .NET Concepts**

- ✅ **ASP.NET Core MVC** - Model-View-Controller architecture
- ✅ **Entity Framework Core** - ORM for database operations
- ✅ **Dependency Injection** - Built-in DI container
- ✅ **Razor Views** - Server-side rendering with C#
- ✅ **Model Binding** - Automatic binding of request data to models
- ✅ **Data Annotations** - Model validation and constraints

### **Web Development Skills**

- ✅ **RESTful APIs** - AJAX endpoints for dynamic updates
- ✅ **JavaScript Modules** - ES6 modules and modern JS
- ✅ **Drag & Drop API** - Native HTML5 drag and drop
- ✅ **Responsive Design** - Mobile-first approach with TailwindCSS
- ✅ **Database Migrations** - Code-first database development

## 🚀 Features

### **Kanban Board Functionality**

- **5-Column Layout**: Backlog → Ready → In Progress → In Review → Done
- **Drag & Drop**: Move tasks between columns with visual feedback
- **Real-time Updates**: Task counts update instantly
- **Visual Feedback**: Loading states, hover effects, notifications

### **Task Management**

- **Create Tasks**: Add new tasks with title and description
- **Status Tracking**: Automatic status updates via drag & drop
- **Task Counts**: Live count badges for each column
- **Persistent Storage**: SQLite database for data persistence

## 🏗️ Architecture

### **Backend (ASP.NET Core)**

```
Controllers/
├── HomeController.cs      # Main dashboard
└── TasksController.cs     # Task CRUD operations + AJAX endpoints

Models/
├── TaskItem.cs           # Task entity model
├── TaskStatus.cs         # Status enumeration
└── ErrorViewModel.cs     # Error handling

Data/
└── AppDbContext.cs       # Entity Framework context

Views/
├── Home/
│   └── Index.cshtml      # Kanban board UI
└── Shared/
    └── _Layout.cshtml    # Base layout template
```

### **Frontend (Modern JavaScript)**

```
js/
├── kaban.js              # Main application controller
└── modules/
    ├── dragdrop.js       # Drag & drop functionality
    └── api.js            # API utilities & notifications
```

### **Database Schema**

```sql
Tasks Table:
- Id (Primary Key)
- Title (Required)
- Description (Required)
- Status (Enum: Backlog, Ready, InProgress, InReview, Done)
- CreatedDate (DateTime)
```

## 🛠️ Technologies Used

| Category        | Technology                   | Purpose                         |
| --------------- | ---------------------------- | ------------------------------- |
| **Backend**     | ASP.NET Core 8.0             | Web framework                   |
| **ORM**         | Entity Framework Core 9.0    | Database operations             |
| **Database**    | SQLite                       | Local development database      |
| **Frontend**    | Razor Views + JavaScript ES6 | Server + Client rendering       |
| **Styling**     | TailwindCSS 4.1              | Utility-first CSS framework     |
| **Build Tools** | .NET CLI, npm                | Project building & dependencies |

## 📦 Setup & Installation

### **Prerequisites**

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (for TailwindCSS)
- Code editor (VS Code recommended)

### **1. Clone the Repository**

```bash
git clone <repository-url>
cd KabanApp
```

### **2. Install Dependencies**

```bash
# .NET packages
dotnet restore

# Node.js packages (for TailwindCSS)
npm install
```

### **3. Setup Database**

```bash
# Create initial migration (if not exists)
dotnet ef migrations add InitialCreate

# Apply migrations to create database
dotnet ef database update
```

### **4. Build CSS**

```bash
# Compile TailwindCSS
npx tailwindcss -i ./input.css -o ./wwwroot/css/output.css --watch
```

### **5. Run the Application**

```bash
dotnet run
```

Navigate to `https://localhost:5179` or `http://localhost:5179`

## 🔧 Development Workflow

### **Adding New Features**

1. **Model Changes**: Update models in `Models/`
2. **Migration**: Run `dotnet ef migrations add <MigrationName>`
3. **Database**: Apply with `dotnet ef database update`
4. **Controller**: Add/modify actions in controllers
5. **View**: Update Razor views for UI changes
6. **JavaScript**: Add client-side functionality in `js/modules/`

### **Database Commands**

```bash
# Add new migration
dotnet ef migrations add <MigrationName>

# Update database
dotnet ef database update

# Remove last migration
dotnet ef migrations remove

# Reset database
dotnet ef database drop
dotnet ef database update
```

### **CSS Development**

```bash
# Watch mode for CSS changes
npx tailwindcss -i ./input.css -o ./wwwroot/css/output.css --watch

# Production build
npx tailwindcss -i ./input.css -o ./wwwroot/css/output.css --minify
```

## 📚 Key Learning Takeaways

### **1. ASP.NET Core MVC Pattern**

- **Controllers** handle HTTP requests and business logic
- **Models** represent data and business rules
- **Views** handle presentation layer with Razor syntax

### **2. Entity Framework Core**

- **Code-First** approach with migrations
- **DbContext** as the bridge between models and database
- **LINQ** for querying data in a type-safe manner

### **3. Modern Web Development**

- **Progressive Enhancement** - works without JavaScript, enhanced with it
- **Separation of Concerns** - clear boundaries between layers
- **API Design** - RESTful endpoints for AJAX operations

### **4. JavaScript Architecture**

- **ES6 Modules** for code organization
- **Event-Driven Programming** for user interactions
- **Async/Await** for API calls and better UX

## 🎓 Skills Demonstrated

- [x] **Backend Development** with ASP.NET Core
- [x] **Database Design** and Entity Framework
- [x] **RESTful API** development
- [x] **Frontend JavaScript** with ES6+ features
- [x] **Responsive Web Design** with TailwindCSS
- [x] **Version Control** with Git
- [x] **Project Structure** and organization
- [x] **Error Handling** and user feedback
- [x] **Performance Considerations** (minimal JS, efficient queries)

## 🔮 Future Enhancements

- [ ] **User Authentication** (ASP.NET Core Identity)
- [ ] **Task Categories** and filtering
- [ ] **Due Dates** and calendar integration
- [ ] **File Attachments** for tasks
- [ ] **API Documentation** (Swagger/OpenAPI)
- [ ] **Deployment** (Docker, Azure)

## 📖 Resources Used

- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/core/)
- [TailwindCSS Documentation](https://tailwindcss.com/docs)
- [MDN Web Docs - Drag and Drop API](https://developer.mozilla.org/en-US/docs/Web/API/HTML_Drag_and_Drop_API)

## 📄 License

This project is for educational purposes. Feel free to use it as a learning reference or starting point for your own projects.

---

**Created as a .NET learning project** 🚀
