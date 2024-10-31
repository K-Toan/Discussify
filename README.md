dotnet ef migrations add <Name> --output-dir Infrastructure/Migrations

# Discussify API Documentation

Welcome to the Discussify API Documentation! This document provides an overview of each endpoint available in the Discussify API, describing the HTTP method, endpoint, and request/response formats.

## Table of Contents

1. [API Gateway](#api-gateway)
   - [Login](#login)
   - [Register](#register)
2. [IdentityMicroservice](#identityservice)
   - [Login](#login-1)
   - [Register](#register-1)
   - [Update User](#update-user)
3. [PostMicroservice](#postservice)
   - [Get Posts](#get-posts)
   - [Get Post By Id](#get-post-by-id)
   - [Get Post By Author Id](#get-post-by-author-id)
   - [Create Post](#create-post)
   - [Update Post](#update-post)
   - [Delete Post](#delete-post)
4. [CommentMicroservice](#commentservice)
   - [Get Comments](#get-comments)
   - [Get Comments By PostId](#get-comments-by-postid)
   - [Create Comment](#create-comment)
   - [Update Comment](#update-comment)
   - [Delete Comment](#delete-comment)
5. [Interaction Microservice](#interaction-service)
   - [Get Target Interactions](#get-target-interactions)
   - [Perform Interaction](#perform-interaction)
6. [Subscription Microservice](#subscription-service)
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
```

### IdentityMicroservice

### Login
- **Method:**  POST
- **Endpoint:**  `/api/authentication/login`
- **Description:**  Authenticates the user in the Identity Microservice.
- **Request Body:**  Same as Login in API Gateway.


### Register
- **Method:**  POST
- **Endpoint:**  `/api/authentication/register`
- **Description:**  Registers a new user in the Identity Microservice.
- **Request Body:**  Same as Register in API Gateway.


### Update User
- **Method:**  PUT
- **Endpoint:**  `/api/users/{userId}`
- **Description:**  Updates the user’s information.
- **Request Body:** 
```json
{
    "userName": "new_username",
    "email": "new_email"
}
```

### PostMicroservice


### Get Posts
- **Method:**  GET
- **Endpoint:**  `/api/posts`
- **Description:**  Retrieves a list of all posts.


### Get Post By Id
- **Method:**  GET
- **Endpoint:**  `/api/posts/{postId}`
- **Description:**  Retrieves a post by its ID.


### Get Post By Author Id
- **Method:**  GET
- **Endpoint:**  `/api/posts/author/{authorId}`
- **Description:**  Retrieves all posts by a specific author.


### Create Post
- **Method:**  POST
- **Endpoint:**  `/api/posts`
- **Description:**  Creates a new post.
- **Request Body:**
```json
{
  "authorId": 1,
  "title": "Your Post Title",
  "content": "Your Post Content"
}
```

### Update Post
- **Method:**  PUT
- **Endpoint:**  `/api/posts/{postId}`
- **Description:**  Updates an existing post.
- **Request Body:**
```json
{
  "title": "Updated Title",
  "content": "Updated Content"
}
```

### Delete Post
- **Method:**  DELETE
- **Endpoint:**  `/api/posts/{postId}`
- **Description:**  Deletes a post by its ID.

### CommentMicroservice

### Get Comments
- **Method:**  GET
- **Endpoint:**  `/api/comments`
- **Description:**  Retrieves all comments.

### Get Comments By PostId
- **Method:**  GET
- **Endpoint:**  `/api/posts/{postId}/comments`
- **Description:**  Retrieves comments for a specific post.

### Create Comment
- **Method:**  POST
- **Endpoint:**  `/api/comments`
- **Description:**  Creates a new comment.
- **Request Body:**
```json
{
  "parentCommentId": 1,
  "postId": 3,
  "userId": 1,
  "content": "Comment Content"
}
```

### Update Comment
- **Method:**  PUT
- **Endpoint:**  `/api/comments/{commentId}`
- **Description:**  Updates an existing comment.
- **Request Body:**  Similar to Create Comment.

### Delete Comment
- **Method:**  DELETE
- **Endpoint:**  `/api/comments/{commentId}`
- **Description:**  Deletes a comment by its ID.

### Interaction Microservice

### Get Target Interactions
- **Method:**  GET
- **Endpoint:**  `/api/interactions`
- **Description:**  Retrieves interaction data for a target.

### Perform Interaction
- **Method:**  POST
- **Endpoint:**  `/api/interactions`
- **Description:**  Records an interaction with a post or comment.
- **Request Body:**
```json
{
  "userId": 1,
  "postId": 1,
  "commentId": 1,
  "type": 0
}
```

### Subscription Microservice

### Get Subscriptions
- **Method:**  GET
- **Endpoint:**  `/api/subscriptions`
- **Description:**  Retrieves all subscriptions.

### Get User Subscriptions
- **Method:**  GET
- **Endpoint:**  `/api/subscriptions/{userId}`
- **Description:**  Retrieves subscriptions for a specific user.

### Join Community
- **Method:**  POST
- **Endpoint:**  `/api/subscriptions`
- **Description:**  Joins a user to a community.
- **Request Body:**
```json
{
  "userId": 1,
  "communityId": 1
}
```

### Leave Community
- **Method:**  DELETE
- **Endpoint:**  `/api/subscriptions/{userId}/communities/{communityId}`
- **Description:**  Removes a user from a community.

### Get Communities
- **Method:**  GET
- **Endpoint:**  `/api/communities`
- **Description:**  Retrieves all communities.

### Get Community By Id
- **Method:**  GET
- **Endpoint:**  `/api/communities/{communityId}`
- **Description:**  Retrieves a specific community by ID.

### Create Community
- **Method:**  POST
- **Endpoint:**  `/api/communities`
- **Description:**  Creates a new community.
- **Request Body:**
```json
{
  "authorId": 1,
  "name": "Community Name",
  "description": "Community Description"
}