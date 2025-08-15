# ProductCode API
Backend (.NET 8) → https://github.com/kiki-ananyaluck/ProductCodeApi

Frontend (Angular) → https://github.com/kiki-ananyaluck/ProductCodeClient

## 📖 Overview
ProductCode API เป็น Backend API สำหรับจัดการรหัสสินค้า (Product Code)  
พัฒนาโดยใช้ **.NET 8** พร้อมโครงสร้าง Clean Architecture

### โครงสร้างโปรเจกต์
- `ProductCodeApi.Api` → API Layer (Controllers, Startup)
- `ProductCodeApi.Application` → Business Logic
- `ProductCodeApi.Domain` → Entities, Models
- `ProductCodeApi.Infrastructure` → Database Access, EF Core

---

## 🚀 Requirements
- .NET 8 SDK
- SQL Server หรือ Connection String อื่น
- Git

---

## 📦 Installation & Run (Local)
```bash
# 1. Clone โปรเจกต์
git clone https://github.com/kiki-ananyaluck/ProductCodeApi.git
cd ProductCodeApi

# 2. Run API
dotnet run --project ProductCodeApi.Api

