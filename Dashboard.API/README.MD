# NinjaOne Dashboard API

## Overview

This project implements a dashboard-focused API layer on top of a mocked NinjaOne environment.

The solution consists of two projects:

```text
Solution
│
├── NinjaOne.MockAPI
│
└── Dashboard.API
```

### NinjaOne.MockAPI

The Mock API simulates NinjaOne REST APIs by serving static JSON data.

Responsibilities:

- Mimic NinjaOne API endpoints
- Return static JSON responses
- No business logic
- No database dependencies
- Can be replaced with real NinjaOne APIs in the future

### Dashboard.API

The Dashboard API acts as an aggregation layer.

Responsibilities:

- Consume Mock API endpoints using HttpClient
- Aggregate and transform data
- Apply dashboard-specific business logic
- Return UI-ready dashboard DTOs
- Support filtering, searching, and pagination

---

# Architecture

```text
Dashboard UI
     │
     ▼
Dashboard.API
     │
     ▼
NinjaOne.MockAPI
```

All business logic resides in Dashboard.API.

MockAPI acts as an external data source.

---

# Dashboard Endpoints

The dashboard is powered by six aggregated endpoints.

## 1. Overview Metrics

```http
GET /api/dashboard/overview-metrics
```

Provides:

- Total Devices
- Online Devices
- Offline Devices
- Critical Alerts
- Patch Compliance
- Open Tickets
- Security Score
- Device Health Distribution
- Operating System Distribution
- KPI Trend Indicators

---

## 2. Asset List

```http
GET /api/dashboard/asset-list
```

Provides:

- Device Inventory Table
- Device Name
- Device Type
- Device Status
- Health Status
- Organization
- Location
- Last Contact
- Device Tags

Supports:

### Search

```http
GET /api/dashboard/asset-list?search=Server
```

### Status Filter

```http
GET /api/dashboard/asset-list?status=Online
```

### Pagination

```http
GET /api/dashboard/asset-list?pageNumber=1&pageSize=10
```

### Combined Example

```http
GET /api/dashboard/asset-list?search=Acme&status=Online&pageNumber=1&pageSize=10
```

---

## 3. Alerts Summary

```http
GET /api/dashboard/alerts-summary
```

Provides:

- Total Alerts
- Critical Alerts
- High Alerts
- Medium Alerts
- Low Alerts
- Alert Severity Distribution
- Alert Severity Donut Chart Data
- Alert Trend Indicators

---

## 4. Patch Summary

```http
GET /api/dashboard/patch-summary
```

Provides:

- Total Patches
- Installed Patches
- Pending Patches
- Failed Patches
- Critical Patches
- Patch Compliance Score
- Patch Status Distribution
- Patch Severity Distribution
- Patch Trend Indicators

---

## 5. Antivirus Summary

```http
GET /api/dashboard/antivirus-summary
```

Provides:

- Protected Devices
- Unprotected Devices
- Antivirus Coverage Percentage
- Antivirus Health Score
- Active Protection Count
- Out-of-Date Definitions Count
- Vendor Distribution
- Endpoint Protection Distribution
- Antivirus Device Inventory

---

## 6. Tickets Summary

```http
GET /api/dashboard/tickets-summary
```

Provides:

- Open Tickets
- Critical Tickets
- High Tickets
- Medium Tickets
- Low Tickets
- Ticket Priority Distribution
- Ticket Status Distribution
- Recent Tickets
- Ticket Trend Indicators

---

# Mock API Endpoints

The Dashboard API consumes the following mocked NinjaOne endpoints:

```http
GET /v2/devices
GET /v2/alerts
GET /v2/activities
GET /v2/organizations
GET /v2/locations
GET /v2/users
GET /v2/queries/antivirus-status
GET /v2/queries/software
GET /v2/queries/os-patch-installs
GET /v2/ticketing/trigger/boards
GET /v2/ticketing/trigger/board/{boardId}/run
```

---

# Dashboard Features Covered

## KPI Cards

- Device KPIs
- Patch KPIs
- Alert KPIs
- Antivirus KPIs
- Ticket KPIs

## Charts & Visualizations

- Device Health Distribution
- OS Platform Distribution
- Patch Status Distribution
- Patch Severity Distribution
- Alert Severity Distribution
- Antivirus Vendor Distribution
- Endpoint Protection Distribution
- Ticket Priority Distribution
- Ticket Status Distribution

## Tables

- Asset Inventory Table
- Antivirus Device Inventory
- Recent Tickets Table

## Data Operations

- Searching
- Filtering
- Pagination
- Aggregation
- Trend Indicators

---

# Running the Solution

## Restore Packages

```bash
dotnet restore
```

## Build Solution

```bash
dotnet build
```

## Run Mock API

```bash
dotnet run --project NinjaOne.MockAPI
```

## Run Dashboard API

```bash
dotnet run --project Dashboard.API
```

---

# Design Decisions

## Why Separate MockAPI?

Separating the Mock API from the Dashboard API creates a clean integration boundary.

Benefits:

- Mimics real-world API integration
- Allows independent testing
- Enables easy replacement with real NinjaOne APIs
- Keeps dashboard logic isolated

## Why Only Six Dashboard Endpoints?

The dashboard is view-driven rather than resource-driven.

Instead of exposing raw NinjaOne resources, Dashboard.API returns aggregated dashboard-ready responses that minimize frontend API calls and simplify UI development.

## Why Put Business Logic in Dashboard.API?

MockAPI is treated as an external service.

Dashboard.API owns:

- Aggregation
- KPI calculations
- Trend calculations
- Search
- Filtering
- Pagination
- Dashboard-specific transformations

---

# Assumptions

- Mock API data is static JSON
- No database is required
- Authentication is out of scope
- Trend values are sample dashboard metrics
- Dashboard.API is designed to support replacement with real NinjaOne APIs in the future

---

# Project Status

```text
Overview Metrics     Complete
Asset List           Complete
Alerts Summary       Complete
Patch Summary        Complete
Antivirus Summary    Complete
Tickets Summary      Complete
```

Dashboard Coverage: 100%
