# CSharp Web Server

A simple C# Web Server created for educational purpuses

Steps to create a similar web server from scrach:

1. Choose the localhost IP Address("127.0.0.1") and a free local port
2. Create a TcpListener and accept icomming client request asynchrnously
3. Write a valid HTTP response and convert it to a byte array
4. Add Content-Type and Content-Length headers (be careful with UTF-8 characters)
5. Read the request in chunks (1024 bytes each) and store it in a StreamBuilder
6. Extract Separate Server and HTTP classes
7. Parse the HTTP request
8. Create routing table which should allow variuos HTTP methods
9. Make sure the HTTP server can populate the routing table
10. Create specific HTTP response classes - TextResponse, for example
11. Implement the toString method for the HTTP response class
12. Implement the routing table for the storing and retrieving request mapping
13. Use the routing table in the HTTP server  for the actual request matching