# TcpFromScratch

A functional HTTP server built from raw TCP in C# — no ASP.NET, no frameworks, no magic.

Built by [Mihail Mihov](https://github.com/m0sh0) as a deep-dive into how HTTP actually works under the hood.

---

## What is this?

Most developers use frameworks like ASP.NET Core without knowing what happens underneath. This project is the answer to that question — a working HTTP server built from scratch using only `TcpListener`, `NetworkStream`, and raw byte manipulation.

It includes a fully functional in-memory Todo REST API, a custom HTTP method, and a built-in HTTP client that talks to the server — all written from the ground up.

---

## Features

- Raw TCP server using `TcpListener`
- HTTP/1.1 request parsing (method, path, headers, body)
- HTTP response formatting with correct status codes and headers
- Multi-client support using threads
- In-memory Todo CRUD API
- Custom `COOKIE` HTTP method that returns a random cookie fact
- Built-in `MyHttpClient` — a TCP-based HTTP client
- Clean architecture with proper separation of concerns

---

## Project Structure

```
MyTCPServer/
├── Client/
│   └── MyHttpClient.cs       — TCP-based HTTP client
├── Core/
│   ├── Server.cs             — listener loop, spawns threads
│   ├── ClientHandler.cs      — reads requests, writes responses
│   └── Router.cs             — routes requests to endpoints
├── EndPoints/
│   ├── TodoEndPoints.cs      — todo route handlers
│   └── CookieEndPoints.cs    — cookie route handler
├── Factory/
│   └── ResponseFactory.cs    — named response helpers
├── Http/
│   ├── HttpRequest.cs        — parsed request object
│   ├── Response.cs           — response object
│   └── ResponseCreator.cs    — formats raw HTTP response string
├── Models/
│   ├── Todo.cs               — todo record
│   └── CreateTodoRequest.cs  — POST request model
├── Services/
│   ├── ITodoService.cs
│   └── TodoService.cs        — business logic
├── Stores/
│   ├── ITodoStore.cs
│   └── TodoStore.cs          — thread-safe in-memory store
└── Program.cs
```

---

## Getting Started

**Requirements:** .NET 8 or later

```bash
git clone https://github.com/m0sh0/TcpFromScratch
cd TcpFromScratch/MyTCPServer
dotnet run
```

Server starts on port `8081`.

---

## API Endpoints

### Todo

| Method | Path | Description |
|---|---|---|
| GET | /todos | Get all todos |
| GET | /todos/{id} | Get a todo by id |
| POST | /todos | Create a new todo |
| DELETE | /todos/{id} | Delete a todo |

### Custom

| Method | Path | Description |
|---|---|---|
| COOKIE | /cookie | Get a random cookie fact |

---

## Example Usage

**Get all todos:**
```bash
curl http://localhost:8081/todos
```

**Create a todo:**
```bash
curl -X POST http://localhost:8081/todos \
  -H "Content-Type: application/json" \
  -d '{"title": "Buy milk", "isCompleted": false}'
```

**Delete a todo:**
```bash
curl -X DELETE http://localhost:8081/todos/{id}
```

**Custom COOKIE method:**
```bash
curl -X COOKIE http://localhost:8081/cookie
```

---

## Why build this?

Frameworks are great — but understanding what they abstract is better. This project was built to deeply understand:

- How TCP connections work
- What an HTTP request and response actually look like as raw bytes
- How routing, request parsing, and response formatting work at the lowest level
- How to handle concurrency with multiple clients

After building this, using ASP.NET Core feels a lot less magical.

---

## Author

**Mihail Mihov** — 17 y/o developer from Bulgaria
- GitHub: [@m0sh0](https://github.com/m0sh0)
- Building: [RoomIQ](https://github.com/m0sh0) — an AI-powered interior design app
