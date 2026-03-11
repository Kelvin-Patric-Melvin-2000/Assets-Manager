# Vehicle Health Tracker

Production-ready MVP for bike/motorcycle owners to track fuel, mileage, services, part replacements, and document expiry reminders.

## Tech Stack
- **Frontend:** Vue 3 + Vue Router + Axios + CSS
- **Backend:** ASP.NET Core 8 Web API (Clean architecture style: Controllers → Services → Repositories)
- **Database:** PostgreSQL
- **Auth:** JWT

## Project Structure
- `backend/` .NET solution with layered architecture
- `backend/db/schema.sql` PostgreSQL schema (manual bootstrap option)
- `frontend/` Vue SPA with route-based pages

## Backend Setup
1. Ensure PostgreSQL is running.
2. Update `backend/src/VehicleHealthTracker.Api/appsettings.json` connection string and JWT settings.
3. Run API:
   ```bash
   cd backend/src/VehicleHealthTracker.Api
   dotnet restore
   dotnet run
   ```

### API Endpoints
- `POST /api/auth/register`
- `POST /api/auth/login`
- `GET/POST/PUT/DELETE /api/vehicles`
- `POST /api/mileage`, `GET /api/mileage/{vehicleId}`
- `POST /api/fuel`, `GET /api/fuel/{vehicleId}`, `GET /api/fuel/analytics/{vehicleId}`
- `POST /api/services`, `GET /api/services/{vehicleId}`
- `POST /api/documents`, `GET /api/documents/{vehicleId}`
- `POST /api/parts`, `GET /api/parts/{vehicleId}`
- `GET /api/dashboard`

## Frontend Setup
```bash
cd frontend
npm install
npm run dev
```
Set optional `.env` with:
```bash
VITE_API_BASE_URL=http://localhost:5000/api
```

## MVP Notes
- Users only access their own vehicle data.
- Dashboard includes reminders + fuel analytics summary.
- Vehicle detail page provides tabs-like grouped sections for mileage/fuel/services/docs/parts.
- Pagination enabled on vehicle listing endpoint.

