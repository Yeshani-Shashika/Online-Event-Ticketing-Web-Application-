# Online-Event-Ticketing-Web-Application-
A full-stack ASP.NET MVC web application developed for StarEvents Pvt Ltd to manage online event ticket booking, secure payments, and QR-coded e-ticket validation.  The system includes role-based dashboards for Customers, Organizers, and Admins with real-time sales tracking and analytics.
🎟️ StarEvents – Online Event Ticketing Web Application

✨ Overview

StarEvents is a full-featured enterprise-level online event ticketing platform developed for StarEvents Pvt Ltd to digitize and streamline the management of concerts, theatre shows, and cultural events in Sri Lanka.

The system provides a secure, scalable, and user-friendly solution for:

🎫 Online ticket booking

💳 Secure payment processing

📱 QR-based e-ticket validation

📊 Real-time analytics & reporting

👥 Role-based dashboards (Customer, Organizer, Admin)

Built using ASP.NET MVC, C#, and SQL Server, the application follows modern software engineering principles with clean architecture and strong OOP implementation.

🏗️ System Architecture
🔹 Technology Stack
Layer	Technology
Frontend	HTML, CSS, JavaScript, Bootstrap
Backend	ASP.NET MVC (C#)
Database	SQL Server
ORM	Entity Framework
Development Tool	Visual Studio 2022
🔹 Architectural Pattern

Model–View–Controller (MVC)

Layered Architecture

High Cohesion & Loose Coupling

Interface-based Abstraction

🚀 Core Features
👤 Customer Module

Secure registration & authentication

Event search (by category, date, location)

Real-time ticket availability

Online payment gateway integration

QR-coded e-ticket generation

Booking history & loyalty system

🎭 Organizer Module

Event creation & management

Ticket pricing & capacity control

Revenue tracking dashboard

Promotional code management

Downloadable sales reports

🛠️ Admin Module

User & role management

Venue & event approval

System activity monitoring

Revenue analytics

Full reporting system (Sales, Users, Events)

🔐 Security & Performance

🔒 Encrypted authentication & transactions

🛡️ Role-Based Access Control (RBAC)

⚡ Optimized database queries & indexing

📈 Scalable structure supporting high concurrent users

💾 Normalized database (3NF)

🗄️ Database Structure

Key Entities:

Users

Roles

Events

Venues

Tickets

Bookings

Payments

Promotions

SystemLogs

Primary & Foreign Keys ensure referential integrity and performance optimization.

🧠 OOP Principles Applied

Encapsulation → Protected data access via services

Abstraction → Interfaces for services (e.g., ITicketService)

Inheritance → Role-based user extensions

High Cohesion → Clear responsibility per class

Loose Coupling → Dependency injection & interface usage

🧪 Testing Strategy

✔ Unit Testing
✔ Integration Testing
✔ User Acceptance Testing
✔ Validation & Exception Handling

All major functional scenarios (Login, Booking, Payment, QR Validation) successfully tested.

📦 Installation Guide
# Clone repository
git clone https://github.com/your-username/starevents-ticketing.git

Steps

Open solution in Visual Studio 2022

Configure connection string in appsettings.json or Web.config

Execute provided SQL scripts or run migrations

Build & Run

📊 Business Impact

✔ Eliminates manual ticket handling
✔ Prevents overselling with real-time tracking
✔ Reduces processing time by automation
✔ Enhances customer convenience
✔ Provides data-driven decision making

🔮 Future Enhancements

📱 Cross-platform mobile application

🤖 AI-based event recommendation engine

💬 WhatsApp & local payment gateway integration

📢 Push notifications & smart alerts

📜 License

This project is developed for academic and educational purposes.
