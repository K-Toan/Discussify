dotnet ef migrations add <Name> --output-dir Infrastructure/Migrations

# Let's save the README content into a file called README.md

readme_content = """
# Discussify API Documentation

Welcome to the Discussify API Documentation! This document provides an overview of each endpoint available in the Discussify API, describing the HTTP method, endpoint, and request/response formats.

## Table of Contents

1. [API Gateway](#api-gateway)
   - [Login](#login)
   - [Register](#register)
2. [IdentityService](#identityservice)
   - [Login](#login-1)
   - [Register](#register-1)
   - [Update User](#update-user)
3. [PostService](#postservice)
   - [Get Posts](#get-posts)
   - [Get Post By Id](#get-post-by-id)
   - [Get Post By Author Id](#get-post-by-author-id)
   - [Create Post](#create-post)
   - [Update Post](#update-post)
   - [Delete Post](#delete-post)
4. [CommentService](#commentservice)
   - [Get Comments](#get-comments)
   - [Get Comments By PostId](#get-comments-by-postid)
   - [Create Comment](#create-comment)
   - [Update Comment](#update-comment)
   - [Delete Comment](#delete-comment)
5. [Interaction Service](#interaction-service)
   - [Get Target Interactions](#get-target-interactions)
   - [Perform Interaction](#perform-interaction)
6. [Subscription Service](#subscription-service)
   - [Get Subscriptions](#get-subscriptions)
   - [Get User Subscriptions](#get-user-subscriptions)
   - [Join Community](#join-community)
   - [Leave Community](#leave-community)
   - [Get Communities](#get-communities)
   - [Get Community By Id](#get-community-by-id)
   - [Create Community](#create-community)

---

## API Gateway

### Login
- **Method:** `POST`
- **Endpoint:** `/login`
- **Description:** Authenticates a user with their username and password.
- **Request Body:**
  ```json
  {
    "username": "your_username",
    "password": "your_password"
  }
