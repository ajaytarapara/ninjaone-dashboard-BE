# NinjaOne Mock API Data Mapping

## devices.json

Endpoint: GET /v2/devices

Used By:

- Overview Metrics
- Asset List

NinjaOne Section/Tag: Devices

---

## alerts.json

Endpoint: GET /v2/alerts

Used By:

- Alerts Summary
- Antivirus Summary

NinjaOne Section/Tag: System

---

## antivirus-status.json

Endpoint: GET /v2/queries/antivirus-status

Used By:

- Antivirus Summary

NinjaOne Section/Tag: Queries

---

## software.json

Endpoint: GET /v2/queries/software

Used By:

- Antivirus Summary

NinjaOne Section/Tag: Queries

---

## os-patches.json

## os-patch-installs.json

Endpoint: GET /v2/queries/os-patch-installs

Used By:

- Patch Summary

NinjaOne Section/Tag: Queries

---

## activities.json

Endpoint: GET /v2/activities

Used By:

- Overview Metrics
- Alerts Summary

NinjaOne Section/Tag: System

---

## organizations.json

Endpoint: GET /v2/organizations

Used By:

- Overview Metrics
- Asset List
- Patch Summary
- Compliance by Organization

NinjaOne Section/Tag: System

## locations.json

Endpoint: GET /v2/locations

Used By:

- Asset List
- Overview Metrics
- Assets by Location (Future)

NinjaOne Section/Tag: System

## users.json

Endpoint: GET /v2/users

Used By:

- Asset List
- Device Ownership
- Assigned Technician Information

NinjaOne Section/Tag: System
