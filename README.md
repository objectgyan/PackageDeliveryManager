# PackageDeliveryManager: This is a POC project for tracking the delivery, it built on Clean Architecture

## DeliveryManager.API
This project provides the necessary API to perfor different operation on package and receipient, also it allow the package's bar code to be generated. It really on JWT based authenicatio to validate the API calls. For demo we can use the Bearer token aa **eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidGVzdCIsImV4cCI6MTcwOTY2NDMwOCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzI4NCIsImF1ZCI6IioifQ.8KCnuiE0fX76hJcdHiBdYET_PZUhO0FTrD3QrKxbeRI**

## DeliveryManager.Core
Provides the necessary interfaces and domain level logic, like bar code generation

## DeliveryManager.Infrastructure
Provides a way to store the data in InMemory database and provides implementation of the different repositories
